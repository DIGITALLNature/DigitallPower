// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Logic;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Spectre.Console;

namespace dgt.power.codegeneration.Templates.dotnet;

public class DotNetEntityViewModelBuilder
{
    private readonly EntityMetadata _entity;
    private readonly DotNetCodeGenerationConfig _config;
    private readonly int _systemLanguage;
    private readonly Func<string, EntityMetadata> _retrieveEntityMetadata;
    private readonly IAnsiConsole _console;
    private readonly Dictionary<string, List<string>> _usedTokens = new();

    public DotNetEntityViewModelBuilder(
        EntityMetadata entity,
        Func<string, EntityMetadata> retrieveEntityMetadata,
        DotNetCodeGenerationConfig config,
        int systemLanguage,
        IAnsiConsole? console = null)
    {
        _entity = entity;
        _retrieveEntityMetadata = retrieveEntityMetadata;
        _config = config;
        _systemLanguage = systemLanguage;
        _console = console ?? AnsiConsole.Console;
    }

    public Dictionary<string, object?> Build()
    {
        var entitySchemaName = Formatter.CamelCase(_entity.SchemaName);
        var isFramework = _config.Output.Target == DotNetTarget.Framework;
        var editableReadOnlyProperties = _config.Output.EditableReadOnly;

        var keyAttribute = _entity.Attributes.Single(a => a.LogicalName == _entity.PrimaryIdAttribute);

        var result = new Dictionary<string, object?>
        {
            ["NameSpace"] = _config.Namespace,
            ["EntitySchemaName"] = entitySchemaName,
            ["EntityLogicalName"] = _entity.LogicalName,
            ["PrimaryIdAttribute"] = _entity.PrimaryIdAttribute,
            ["PrimaryNameAttribute"] = _entity.PrimaryNameAttribute,
            ["HasPrimaryNameAttribute"] = _entity.PrimaryNameAttribute != null,
            ["ObjectTypeCode"] = _entity.ObjectTypeCode,
            ["IsFramework"] = isFramework,
            ["Virtual"] = _config.Output.Virtual ? "virtual " : "",
            ["IncludeEntityTypeCode"] = _config.Output.Include.EntityTypeCode,
            ["IncludeNavigationProperties"] = _config.Output.Include.NavigationProps,
            ["IncludeOptions"] = _config.Output.Include.Options,
            ["IncludeLogicalNames"] = _config.Output.Include.LogicalNames,
            ["IncludeAlternateKeys"] = _config.Output.Include.AlternateKeys,
            ["IncludeRelations"] = _config.Output.Include.Relations,
            ["IncludeContext"] = _config.Output.Include.Context,
            ["Summary"] = BuildSummary(GetLocalizedLabel(_entity.Description), 1),
            ["KeyAttributeSchemaName"] = PreventBadToken(Formatter.CamelCase(keyAttribute.SchemaName)),
            ["KeyAttributeIsValidForCreate"] = keyAttribute.IsValidForCreate.GetValueOrDefault(),
            ["Attributes"] = BuildAttributes(editableReadOnlyProperties, isFramework),
            ["NavigationProperties"] = BuildNavigationProperties(),
            ["OptionFields"] = BuildOptionFields(),
            ["LogicalNameEntries"] = BuildLogicalNameEntries(),
            ["AlternateKeys"] = BuildAlternateKeys(),
            ["OneToManyRelationships"] = BuildOneToManyRelationships(),
            ["ManyToOneRelationships"] = BuildManyToOneRelationships(),
            ["ManyToManyRelationships"] = BuildManyToManyRelationships()
        };

        return result;
    }

    private object[] BuildAttributes(bool editableReadOnlyProperties, bool isFramework)
    {
        return Filter(_entity.Attributes).Select(attr =>
        {
            var attrName = Unique(PreventBadToken(Formatter.CamelCase(attr.SchemaName)), "A" + _entity.LogicalName);
            return new DotNetAttributeModel
            {
                Name = attrName,
                LogicalName = attr.LogicalName,
                CSharpType = ConvertType(attr.AttributeType, attr.AttributeTypeName?.Value, isFramework),
                IsValidForRead = attr.IsValidForRead == true,
                HasSetter = HasSetter(attr, editableReadOnlyProperties),
                IsPartyList = attr.AttributeType == AttributeTypeCode.PartyList,
                IsPrimaryId = attr.IsPrimaryId == true,
                Summary = BuildSummary(GetLocalizedLabel(attr.Description), 2)
            };
        }).ToArray<object>();
    }

    private object[] BuildNavigationProperties()
    {
        return _entity.OneToManyRelationships
            .Where(attr => _config.Entities.Names.Contains(attr.ReferencingEntity))
            .OrderBy(r => r.SchemaName)
            .Select(attr =>
            {
                var attrName = Unique(PreventBadToken(Formatter.CamelCase(attr.SchemaName)),
                    "N" + _entity.LogicalName);
                return new DotNetNavigationPropertyModel
                {
                    Name = attrName,
                    SchemaName = attr.SchemaName,
                    ReferencingEntitySchemaName = Formatter.CamelCase(RetrieveSchemaName(attr.ReferencingEntity))
                };
            }).ToArray<object>();
    }

    private object[] BuildOptionFields()
    {
        return FilterOptions(_entity.Attributes).Select(optionField =>
        {
            var name = Unique(Formatter.CamelCase(optionField.SchemaName), "O" + _entity.LogicalName);
            var attributeType = optionField.AttributeType switch
            {
                AttributeTypeCode.Picklist => "Picklist",
                AttributeTypeCode.Virtual => "Virtual",
                AttributeTypeCode.Status => "Status",
                AttributeTypeCode.State => "State",
                _ => "Boolean"
            };

            var falseLabel = "";
            var trueLabel = "";
            var structLabel = name;

            if (attributeType == "Boolean")
            {
                falseLabel = Formatter.Sanitize(Formatter.CamelCase(optionField.Options[0].Label));
                trueLabel = Formatter.Sanitize(Formatter.CamelCase(optionField.Options[1].Label));
                if (structLabel.Equals(falseLabel, StringComparison.OrdinalIgnoreCase))
                {
                    falseLabel += "_";
                }
                if (structLabel.Equals(trueLabel, StringComparison.OrdinalIgnoreCase))
                {
                    trueLabel += "_";
                }
                if (trueLabel.Equals(falseLabel, StringComparison.OrdinalIgnoreCase))
                {
                    trueLabel += "_true";
                    falseLabel += "_false";
                }
            }

            return new DotNetOptionFieldModel
            {
                Name = name,
                AttributeType = attributeType,
                Options = optionField.Options.Select(o => new DotNetOptionModel
                {
                    Label = Formatter.Sanitize(Formatter.CamelCase(o.Label)),
                    Value = o.Value
                }).ToArray(),
                StructLabel = structLabel,
                FalseLabel = falseLabel,
                TrueLabel = trueLabel
            };
        }).ToArray<object>();
    }

    private object[] BuildLogicalNameEntries()
    {
        return Filter(_entity.Attributes).Select(object (attr) =>
        {
            var name = Unique(Formatter.CamelCase(attr.SchemaName), "L" + _entity.LogicalName);
            return new Dictionary<string, object> { ["Name"] = name, ["LogicalName"] = attr.LogicalName };
        }).ToArray();
    }

    private object[] BuildAlternateKeys()
    {
        if (_entity.Keys == null || _entity.Keys.Length == 0)
            return [];

        return _entity.Keys
            .OrderBy(key => key.LogicalName)
            .Select(object (key) =>
            {
                var name = Unique(
                    Formatter.Sanitize(Formatter.CamelCase(GetLocalizedLabel(key.DisplayName))),
                    "K" + key.LogicalName);
                return new Dictionary<string, object> { ["Name"] = name, ["LogicalName"] = MaskDoubleQuote(key.LogicalName) };
            }).ToArray();
    }

    private object[] BuildOneToManyRelationships()
    {
        return _entity.OneToManyRelationships
            .OrderBy(r => r.SchemaName)
            .Select(object (attr) =>
            {
                var name = Unique(Formatter.CamelCase(attr.SchemaName), "ROTM" + _entity.LogicalName);
                return new Dictionary<string, object> { ["Name"] = name, ["SchemaName"] = attr.SchemaName };
            }).ToArray();
    }

    private object[] BuildManyToOneRelationships()
    {
        return _entity.ManyToOneRelationships
            .OrderBy(r => r.SchemaName)
            .Select(object (attr) =>
            {
                var name = Unique(Formatter.CamelCase(attr.SchemaName), "RMTO" + _entity.LogicalName);
                return new Dictionary<string, object> { ["Name"] = name, ["SchemaName"] = attr.SchemaName };
            }).ToArray();
    }

    private object[] BuildManyToManyRelationships()
    {
        return _entity.ManyToManyRelationships
            .OrderBy(r => r.SchemaName)
            .Select(object (attr) =>
            {
                var name = Unique(Formatter.CamelCase(attr.SchemaName), "RMTM" + _entity.LogicalName);
                return new Dictionary<string, object> { ["Name"] = name, ["SchemaName"] = attr.SchemaName };
            }).ToArray();
    }

    private string Unique(string value, string scope)
    {
        if (!_usedTokens.ContainsKey(scope)) _usedTokens.Add(scope, new List<string>());

        if (_usedTokens[scope].Contains(value) || value == Formatter.CamelCase(_entity.SchemaName))
        {
            _console.MarkupLine($"[red]Warning:[/] multiple entries for: {value} ({scope})");
            return Unique(value + "_", scope);
        }

        _usedTokens[scope].Add(value);
        return value;
    }

    private static string ConvertType(AttributeTypeCode? code, string? attributeTypeName, bool isFramework)
    {
        if (isFramework) return NonNullableConvertType(code, attributeTypeName);

        return code switch
        {
            AttributeTypeCode.BigInt => "long?",
            AttributeTypeCode.Boolean => "bool?",
            AttributeTypeCode.DateTime => "DateTime?",
            AttributeTypeCode.Customer or AttributeTypeCode.Lookup or AttributeTypeCode.Owner => "EntityReference?",
            AttributeTypeCode.Decimal => "decimal?",
            AttributeTypeCode.Money => "Money?",
            AttributeTypeCode.Double => "double?",
            AttributeTypeCode.Picklist or AttributeTypeCode.State or AttributeTypeCode.Status => "OptionSetValue?",
            AttributeTypeCode.Uniqueidentifier => "Guid?",
            AttributeTypeCode.String or AttributeTypeCode.Memo or AttributeTypeCode.EntityName => "string?",
            AttributeTypeCode.Integer => "int?",
            AttributeTypeCode.PartyList => "IEnumerable<ActivityParty>?",
            AttributeTypeCode.ManagedProperty => "BooleanManagedProperty?",
            _ => attributeTypeName switch
            {
                "MultiSelectPicklistType" => "Microsoft.Xrm.Sdk.OptionSetValueCollection?",
                "ImageType" => "byte[]?",
                "FileType" => "Guid?",
                "VirtualType" => "string?",
                _ => "dynamic"
            }
        };
    }

    private static string NonNullableConvertType(AttributeTypeCode? code, string? attributeTypeName)
    {
        return code switch
        {
            AttributeTypeCode.BigInt => "long?",
            AttributeTypeCode.Boolean => "bool?",
            AttributeTypeCode.DateTime => "DateTime?",
            AttributeTypeCode.Customer or AttributeTypeCode.Lookup or AttributeTypeCode.Owner => "EntityReference",
            AttributeTypeCode.Decimal => "decimal?",
            AttributeTypeCode.Money => "Money",
            AttributeTypeCode.Double => "double?",
            AttributeTypeCode.Picklist or AttributeTypeCode.State or AttributeTypeCode.Status => "OptionSetValue",
            AttributeTypeCode.Uniqueidentifier => "Guid?",
            AttributeTypeCode.String or AttributeTypeCode.Memo or AttributeTypeCode.EntityName => "string",
            AttributeTypeCode.Integer => "int?",
            AttributeTypeCode.PartyList => "IEnumerable<ActivityParty>",
            AttributeTypeCode.ManagedProperty => "BooleanManagedProperty",
            _ => attributeTypeName switch
            {
                "MultiSelectPicklistType" => "Microsoft.Xrm.Sdk.OptionSetValueCollection",
                "ImageType" => "byte[]",
                "FileType" => "Guid?",
                "VirtualType" => "string",
                _ => "dynamic"
            }
        };
    }

    private static IEnumerable<AttributeMetadata> Filter(AttributeMetadata[] attributes)
    {
        return attributes
            .Where(IsReadableAttribute)
            .OrderByDescending(a => a.IsPrimaryId)
            .ThenBy(a => a.LogicalName);
    }

    private IEnumerable<OptionField> FilterOptions(AttributeMetadata[] attributes)
    {
        return attributes
            .Where(IsReadableAttribute)
            .Where(IsSupportedOptionField)
            .OrderBy(a => a.LogicalName)
            .Select(o => new OptionField(o, _config.Language == null, _systemLanguage));
    }

    private static bool HasSetter(AttributeMetadata attribute, bool editableReadOnlyProperties)
    {
        if (editableReadOnlyProperties)
        {
            return true;
        }

        return attribute.IsValidForUpdate == true || attribute.IsValidForCreate == true;
    }

    private static bool IsReadableAttribute(AttributeMetadata attribute)
    {
        var isReadable = attribute.IsValidForCreate == true
                         || attribute.IsValidForUpdate == true
                         || attribute.IsValidForRead == true;
        if (!isReadable)
        {
            return false;
        }

        return attribute.AttributeOf == null || attribute.IsValidODataAttribute;
    }

    private static bool IsSupportedOptionField(AttributeMetadata attribute)
    {
        return attribute.AttributeType switch
        {
            AttributeTypeCode.Picklist or AttributeTypeCode.State or AttributeTypeCode.Status or AttributeTypeCode.Boolean => true,
            AttributeTypeCode.Virtual => attribute.AttributeTypeName?.Value == "MultiSelectPicklistType",
            _ => false
        };
    }

    private string GetLocalizedLabel(Label? label)
    {
        return label == null ? string.Empty : Formatter.GetLocalizedLabel(label, _config.Language == null, _systemLanguage);
    }

    private static string PreventBadToken(string value)
    {
        return value.Replace("Attributes", "AttributesField", StringComparison.Ordinal);
    }

    private static string MaskDoubleQuote(string value)
    {
        return Formatter.MaskDoubleQuote(value);
    }

    private static string BuildSummary(string description, int indent)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            return string.Empty;
        }

        return "/// <summary>" +
               $"{Environment.NewLine}" +
               $"{new string('\t', indent)}/// {description.Replace("\n", $"\n{new string('\t', indent)}/// ", StringComparison.Ordinal).Replace("&", "&amp;", StringComparison.Ordinal).Replace("<", "&lt;", StringComparison.Ordinal).Replace(">", "&gt;", StringComparison.Ordinal).Trim()}" +
               $"{Environment.NewLine}" +
               $"{new string('\t', indent)}/// </summary>";
    }

    private string RetrieveSchemaName(string entityLogicalName)
    {
        return _retrieveEntityMetadata.Invoke(entityLogicalName).SchemaName;
    }
}

// ReSharper disable UnusedAutoPropertyAccessor.Global
public class DotNetAttributeModel
{
    public string Name { get; set; } = "";
    public string LogicalName { get; set; } = "";
    public string CSharpType { get; set; } = "";
    public bool IsValidForRead { get; set; }
    public bool HasSetter { get; set; }
    public bool IsPartyList { get; set; }
    public bool IsPrimaryId { get; set; }
    public string Summary { get; set; } = "";
}

public class DotNetNavigationPropertyModel
{
    public string Name { get; set; } = "";
    public string SchemaName { get; set; } = "";
    public string ReferencingEntitySchemaName { get; set; } = "";
}

public class DotNetOptionFieldModel
{
    public string Name { get; set; } = "";
    public string AttributeType { get; set; } = "";
    public IReadOnlyList<DotNetOptionModel> Options { get; set; } = [];
    public string StructLabel { get; set; } = "";
    public string FalseLabel { get; set; } = "";
    public string TrueLabel { get; set; } = "";
}

public class DotNetOptionModel
{
    public string Label { get; set; } = "";
    public int? Value { get; set; }
}
