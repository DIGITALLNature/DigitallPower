using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Logic;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Spectre.Console;

namespace dgt.power.codegeneration.Templates.dotnet
{
    public partial class EntityTemplate
    {
        private readonly bool _suppressContext;
        private readonly bool _withDebuggerNonUserCode;
        private readonly bool _editableReadOnlyProperties;
        private readonly bool _withVirtual;
        private readonly bool _suppressRelations;
        private readonly bool _suppressNavigationProperties;
        private readonly bool _suppressEntityTypeCode;
        private readonly bool _suppressAlternateKeys;
        private readonly bool _useBaseLanguage;
        private readonly int _systemLanguage;
        private string DebuggerNonUserCodeUsing => _withDebuggerNonUserCode ? $"{Environment.NewLine}using System.Diagnostics;" : string.Empty;
        private string Virtual => _withVirtual ? "virtual " : string.Empty;
        private readonly bool _suppressLogicalNames;
        private readonly bool _suppressOptions;
        private readonly Dictionary<string, List<string>> _usedTokens = new Dictionary<string, List<string>>();
        private readonly EntityMetadata EntityMetadata;
        private readonly Func<string, EntityMetadata> _retrieveEntityMetadata;
        private readonly string[] _configEntities;
        private readonly string NameSpace;
        private readonly bool _useClassic;
        private bool HasPrimaryNameAttribute => EntityMetadata.PrimaryNameAttribute != null;

        public EntityTemplate(EntityMetadata entity, Func<string, EntityMetadata> retrieveEntityMetadata, CodeGenerationConfig config, int systemLanguage)
        {
            EntityMetadata = entity;
            _retrieveEntityMetadata = retrieveEntityMetadata;
            _configEntities = config.Entities.ToArray();
            NameSpace = config.NameSpace;
            _suppressOptions = config.SuppressOptions;
            _suppressLogicalNames = config.SuppressLogicalNames;
            _suppressContext = config.SuppressContext;
            _withDebuggerNonUserCode = !config.NonDebuggerNonUserCode;
            _editableReadOnlyProperties = config.EditableReadOnlyProperties;
            _withVirtual = config.Virtual;
            _suppressRelations = config.SuppressRelations;
            _suppressNavigationProperties = config.SuppressNavigationProperties;
            _suppressEntityTypeCode = config.SuppressEntityTypeCode;
            _suppressAlternateKeys = config.SuppressAlternateKeys;
            _useBaseLanguage = config.UseBaseLanguage;
            _useClassic = config.SuppressNullableSupport;
            _systemLanguage = systemLanguage;
        }

        private static string CamelCase(string phrase)
        {
            return Formatter.CamelCase(phrase);
        }

        private string PascalCase(string phrase)
        {
            return Formatter.PascalCase(phrase);
        }

        private string Unique(string value, string scope)
        {
            if (!_usedTokens.ContainsKey(scope)) _usedTokens.Add(scope, new List<string>());

            //if (_usedTokens[scope].Any(s => s.Equals(value, StringComparison.OrdinalIgnoreCase)) || value == $"{CamelCase(EntityMetadata.SchemaName)}")
            if (_usedTokens[scope].Contains(value) || value == $"{CamelCase(EntityMetadata.SchemaName)}")
            {
                AnsiConsole.MarkupLine($"[red]Warning:[/] multiple entries for: {value} ({scope})");
                return Unique(value + "_", scope);
            }

            _usedTokens[scope].Add(value);
            return value;
        }

        private string ConvertType(AttributeTypeCode? code, string attributeTypeName)
        {
            if (_useClassic)
            {
                return NonNullableConvertType(code, attributeTypeName);
            }

            switch (code)
            {
                case AttributeTypeCode.BigInt:
                    return "long?";
                case AttributeTypeCode.Boolean:
                    return "bool?";
                case AttributeTypeCode.DateTime:
                    return "DateTime?";
                case AttributeTypeCode.Customer:
                case AttributeTypeCode.Lookup:
                case AttributeTypeCode.Owner:
                    return "EntityReference?";
                case AttributeTypeCode.Decimal:
                    return "decimal?";
                case AttributeTypeCode.Money:
                    return "Money?";
                case AttributeTypeCode.Double:
                    return "double?";
                case AttributeTypeCode.Picklist:
                case AttributeTypeCode.State:
                case AttributeTypeCode.Status:
                    return "OptionSetValue?";
                case AttributeTypeCode.Uniqueidentifier:
                    return "Guid?";
                case AttributeTypeCode.String:
                case AttributeTypeCode.Memo:
                case AttributeTypeCode.EntityName:
                    return "string?";
                case AttributeTypeCode.Integer:
                    return "int?";
                case AttributeTypeCode.PartyList:
                    return "IEnumerable<ActivityParty>?";
                case AttributeTypeCode.ManagedProperty:
                    return "BooleanManagedProperty?";
                default:
                    switch (attributeTypeName)
                    {
                        case "MultiSelectPicklistType":
                            return "Microsoft.Xrm.Sdk.OptionSetValueCollection?";
                        case "ImageType":
                            return "byte[]?";
                        case "FileType":
                            return "Guid?";
                        case "VirtualType":
                            return "string?";
                    }
                    return "dynamic";
            }
        }

        private static string NonNullableConvertType(AttributeTypeCode? code, string attributeTypeName)
        {
            switch (code)
            {
                case AttributeTypeCode.BigInt:
                    return "long?";
                case AttributeTypeCode.Boolean:
                    return "bool?";
                case AttributeTypeCode.DateTime:
                    return "DateTime?";
                case AttributeTypeCode.Customer:
                case AttributeTypeCode.Lookup:
                case AttributeTypeCode.Owner:
                    return "EntityReference";
                case AttributeTypeCode.Decimal:
                    return "decimal?";
                case AttributeTypeCode.Money:
                    return "Money";
                case AttributeTypeCode.Double:
                    return "double?";
                case AttributeTypeCode.Picklist:
                case AttributeTypeCode.State:
                case AttributeTypeCode.Status:
                    return "OptionSetValue";
                case AttributeTypeCode.Uniqueidentifier:
                    return "Guid?";
                case AttributeTypeCode.String:
                case AttributeTypeCode.Memo:
                case AttributeTypeCode.EntityName:
                    return "string";
                case AttributeTypeCode.Integer:
                    return "int?";
                case AttributeTypeCode.PartyList:
                    return "IEnumerable<ActivityParty>";
                case AttributeTypeCode.ManagedProperty:
                    return "BooleanManagedProperty";
                default:
                    switch (attributeTypeName)
                    {
                        case "MultiSelectPicklistType":
                            return "Microsoft.Xrm.Sdk.OptionSetValueCollection";
                        case "ImageType":
                            return "byte[]";
                        case "FileType":
                            return "Guid?";
                        case "VirtualType":
                            return "string";
                    }
                    return "dynamic";
            }
        }

        private static string PreventBadToken(string value)
        {
            return value.Replace("Attributes", "AttributesField");
        }

        private static string Sanitize(string? value)
        {
            return Formatter.Sanitize(value);
        }

        private static IEnumerable<AttributeMetadata> Filter(AttributeMetadata[] attributes)
        {
            return attributes
                .OrderByDescending(a => a.IsPrimaryId)
                .ThenBy(a => a.LogicalName)
                .Where(a => a.IsValidForCreate == true || a.IsValidForUpdate == true || a.IsValidForRead == true && a.IsValidODataAttribute);
        }

        private IEnumerable<OptionField> FilterOptions(AttributeMetadata[] attributes)
        {
            return attributes.OrderBy(a => a.LogicalName).Where(a =>
                (a.IsValidForCreate == true || a.IsValidForUpdate == true ||
                 a.IsValidForRead == true)
                && (a.AttributeType == AttributeTypeCode.Picklist || a.AttributeType == AttributeTypeCode.State ||
                    a.AttributeType == AttributeTypeCode.Status || a.AttributeType == AttributeTypeCode.Boolean ||
                    (a.AttributeType == AttributeTypeCode.Virtual && a.AttributeTypeName?.Value == "MultiSelectPicklistType")))
                .Select(o => new OptionField(o, _useBaseLanguage, _systemLanguage));
        }

        private string GetLocalizedLabel(Label label)
        {
            return Formatter.GetLocalizedLabel(label, _useBaseLanguage, _systemLanguage);
        }

        private static string MaskDoubleQuote(string value)
        {
            return Formatter.MaskDoubleQuote(value);
        }

        private static string Summary(string description, int indent)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                return string.Empty;
            }

            return "/// <summary>" +
                   $"{Environment.NewLine}" +
                   $"{new string('\t', indent)}/// {description.Replace("\n", $"\n{new string('\t', indent)}/// ").Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Trim()}" +
                   $"{Environment.NewLine}" +
                   $"{new string('\t', indent)}/// </summary>";
        }

        private string DebuggerNonUserCode(int indent)
        {
            return _withDebuggerNonUserCode ? $"[DebuggerNonUserCode]{Environment.NewLine}{new string('\t', indent)}" : string.Empty;
        }

        private string RetrieveSchemaName(string entityLogicalName)
        {
            var metadata = _retrieveEntityMetadata.Invoke(entityLogicalName);

            return metadata.SchemaName;
        }
    }
}
