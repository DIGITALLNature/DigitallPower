// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Logic;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Spectre.Console;

namespace dgt.power.codegeneration.Templates.dotnet;

public class DotNetEntityViewModelBuilder(
    EntityMetadata entity,
    Func<string, EntityMetadata> retrieveEntityMetadata,
    DotNetCodeGenerationConfig config,
    int systemLanguage,
    IAnsiConsole? console = null)
{
    private readonly IAnsiConsole _console = console ?? AnsiConsole.Console;
    private readonly Dictionary<string, List<string>> _usedTokens = new();

    public Dictionary<string, object?> Build()
    {
        var entitySchemaName = Formatter.CamelCase(entity.SchemaName);
        var isFramework = config.Output.Target == DotNetTarget.Framework;
        var editableReadOnlyProperties = config.Output.EditableReadOnly;

        var keyAttribute = entity.Attributes.Single(a => a.LogicalName == entity.PrimaryIdAttribute);

        var result = new Dictionary<string, object?>
        {
            ["NameSpace"] = config.Namespace,
            ["EntitySchemaName"] = entitySchemaName,
            ["EntityLogicalName"] = entity.LogicalName,
            ["PrimaryIdAttribute"] = entity.PrimaryIdAttribute,
            ["PrimaryNameAttribute"] = entity.PrimaryNameAttribute,
            ["HasPrimaryNameAttribute"] = entity.PrimaryNameAttribute != null,
            ["ObjectTypeCode"] = entity.ObjectTypeCode,
            ["IsFramework"] = isFramework,
            ["Virtual"] = config.Output.Virtual ? "virtual " : "",
            ["IncludeEntityTypeCode"] = config.Output.Include.EntityTypeCode,
            ["IncludeNavigationProperties"] = config.Output.Include.NavigationProps,
            ["IncludeOptions"] = config.Output.Include.Options,
            ["IncludeLogicalNames"] = config.Output.Include.LogicalNames,
            ["IncludeAlternateKeys"] = config.Output.Include.AlternateKeys,
            ["IncludeRelations"] = config.Output.Include.Relations,
            ["IncludeContext"] = config.Output.Include.Context,
            ["Summary"] = BuildSummary(GetLocalizedLabel(entity.Description), 1),
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
        return Filter(entity.Attributes).Select(attr =>
        {
            var attrName = Unique(PreventBadToken(Formatter.CamelCase(attr.SchemaName)), "A" + entity.LogicalName);
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
        return entity.OneToManyRelationships
            .Where(attr => config.Entities.Names.Contains(attr.ReferencingEntity))
            .OrderBy(r => r.SchemaName)
            .Select(attr =>
            {
                var attrName = Unique(PreventBadToken(Formatter.CamelCase(attr.SchemaName)),
                    "N" + entity.LogicalName);
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
        return FilterOptions(entity.Attributes).Select(optionField =>
        {
            var name = Unique(Formatter.CamelCase(optionField.SchemaName), "O" + entity.LogicalName);
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
        return Filter(entity.Attributes).Select(object (attr) =>
        {
            var name = Unique(Formatter.CamelCase(attr.SchemaName), "L" + entity.LogicalName);
            return new Dictionary<string, object> { ["Name"] = name, ["LogicalName"] = attr.LogicalName };
        }).ToArray();
    }

    private object[] BuildAlternateKeys()
    {
        if (entity.Keys == null || entity.Keys.Length == 0)
            return [];

        return entity.Keys
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
        return entity.OneToManyRelationships
            .OrderBy(r => r.SchemaName)
            .Select(object (attr) =>
            {
                var name = Unique(Formatter.CamelCase(attr.SchemaName), "ROTM" + entity.LogicalName);
                return new Dictionary<string, object> { ["Name"] = name, ["SchemaName"] = attr.SchemaName };
            }).ToArray();
    }

    private object[] BuildManyToOneRelationships()
    {
        return entity.ManyToOneRelationships
            .OrderBy(r => r.SchemaName)
            .Select(object (attr) =>
            {
                var name = Unique(Formatter.CamelCase(attr.SchemaName), "RMTO" + entity.LogicalName);
                return new Dictionary<string, object> { ["Name"] = name, ["SchemaName"] = attr.SchemaName };
            }).ToArray();
    }

    private object[] BuildManyToManyRelationships()
    {
        return entity.ManyToManyRelationships
            .OrderBy(r => r.SchemaName)
            .Select(object (attr) =>
            {
                var name = Unique(Formatter.CamelCase(attr.SchemaName), "RMTM" + entity.LogicalName);
                return new Dictionary<string, object> { ["Name"] = name, ["SchemaName"] = attr.SchemaName };
            }).ToArray();
    }

    private string Unique(string value, string scope)
    {
        if (!_usedTokens.ContainsKey(scope)) _usedTokens.Add(scope, new List<string>());

        if (_usedTokens[scope].Contains(value) || value == Formatter.CamelCase(entity.SchemaName))
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
            .Select(o => new OptionField(o, config.Language == null, systemLanguage));
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
        return label == null ? string.Empty : Formatter.GetLocalizedLabel(label, config.Language == null, systemLanguage);
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
        return retrieveEntityMetadata.Invoke(entityLogicalName).SchemaName;
    }
}

// ReSharper disable UnusedAutoPropertyAccessor.Global
public class DotNetAttributeModel
{
    public string Name { get; init; } = "";
    public string LogicalName { get; init; } = "";
    public string CSharpType { get; init; } = "";
    public bool IsValidForRead { get; init; }
    public bool HasSetter { get; init; }
    public bool IsPartyList { get; init; }
    public bool IsPrimaryId { get; init; }
    public string Summary { get; init; } = "";
}

public class DotNetNavigationPropertyModel
{
    public string Name { get; init; } = "";
    public string SchemaName { get; init; } = "";
    public string ReferencingEntitySchemaName { get; init; } = "";
}

public class DotNetOptionFieldModel
{
    public string Name { get; init; } = "";
    public string AttributeType { get; init; } = "";
    public IReadOnlyList<DotNetOptionModel> Options { get; init; } = [];
    public string StructLabel { get; init; } = "";
    public string FalseLabel { get; init; } = "";
    public string TrueLabel { get; init; } = "";
}

public class DotNetOptionModel
{
    public string Label { get; init; } = "";
    public int? Value { get; init; }
}
