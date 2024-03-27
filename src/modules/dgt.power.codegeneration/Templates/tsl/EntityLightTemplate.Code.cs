// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Logic;
using dgt.power.codegeneration.Templates.ts;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;

namespace dgt.power.codegeneration.Templates.tsl;

public partial class EntityLightTemplate : ITemplate
{
    private readonly Dictionary<string, List<string>> _usedTokens = new();
    private readonly string TypingPath;
    private readonly EntityMetadata EntityMetadata;
    private readonly bool _useBaseLanguage;
    private readonly int _systemLanguage;

    public EntityLightTemplate(string typingPath, EntityMetadata entity, CodeGenerationConfig cfg, int systemLanguage)
    {
        TypingPath = typingPath;
        EntityMetadata = entity;
        _useBaseLanguage = cfg.UseBaseLanguage;
        _systemLanguage = systemLanguage;
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
    private string GetLocalizedLabel(Label label)
    {
        return Formatter.GetLocalizedLabel(label, _useBaseLanguage, _systemLanguage);
    }

    private TypeScriptType GetTypeScriptTypes(AttributeMetadata attr)
    {
        switch (attr.AttributeType)
        {
           case AttributeTypeCode.Boolean:
               return new TypeScriptType
               {
                   DefinitelyTypedAttributeType = "Xrm.Attributes.BooleanAttribute",
                   DefinitelyTypedControlType = "Xrm.Controls.BooleanAttribute",
                   DefinitelyType = "Optionset"
               };
            case AttributeTypeCode.DateTime:
                return new TypeScriptType
                {
                    DefinitelyTypedAttributeType = "Xrm.Attributes.DateAttribute",
                    DefinitelyTypedControlType = "Xrm.Controls.DateAttribute",
                    DefinitelyType = GetDateTimeType(attr as DateTimeAttributeMetadata)
                };
            case AttributeTypeCode.Decimal:
                return new TypeScriptType
                {
                    DefinitelyTypedAttributeType = "Xrm.Attributes.NumberAttribute",
                    DefinitelyTypedControlType = "Xrm.Controls.NumberAttribute",
                    DefinitelyType ="Decimal"
                };
            case AttributeTypeCode.Double:
                return new TypeScriptType
                {
                    DefinitelyTypedAttributeType = "Xrm.Attributes.NumberAttribute",
                    DefinitelyTypedControlType = "Xrm.Controls.NumberAttribute",
                    DefinitelyType ="Double"
                };
            case AttributeTypeCode.Integer:
                return new TypeScriptType
                {
                    DefinitelyTypedAttributeType = "Xrm.Attributes.NumberAttribute",
                    DefinitelyTypedControlType = "Xrm.Controls.NumberAttribute",
                    DefinitelyType ="Integer"
                };
            case AttributeTypeCode.BigInt:
                return new TypeScriptType
            {
                DefinitelyTypedAttributeType = "Xrm.Attributes.NumberAttribute",
                DefinitelyTypedControlType = "Xrm.Controls.NumberAttribute",
                DefinitelyType ="BigInt"
            };
            case AttributeTypeCode.Lookup:
                return new TypeScriptType
                {
                    DefinitelyTypedAttributeType = "Xrm.Attributes.LookupAttribute",
                    DefinitelyTypedControlType = "Xrm.Controls.LookupAttribute",
                    DefinitelyType ="Lookup"
                };
            default:
                return new TypeScriptType
                {
                    DefinitelyTypedAttributeType = "Xrm.Attributes.Attribute",
                    DefinitelyTypedControlType = "Xrm.Controls.Attribute",
                    DefinitelyType ="Attribute"
                };

        }
    }

    private string GetDateTimeType(DateTimeAttributeMetadata? attr)
    {
        if (attr.DateTimeBehavior == DateTimeBehavior.DateOnly)
        {
            if(attr.Format == DateTimeFormat.DateOnly) return "DateOnly:DateOnly";
            return "DateOnly:UserLocal";
        }

        if (attr.DateTimeBehavior == DateTimeBehavior.TimeZoneIndependent)
        {
            return "DateOnly:TimeZoneIndependent";
        }

        return "DateAndTime:UserLocal";

    }

    private IEnumerable<AttributeMetadata> Filter(AttributeMetadata[] attributes)
    {
        var filter = attributes
            .Where(a => (a.IsValidForGrid == true || a.IsValidForForm == true || a.IsValidODataAttribute == true || a.IsPrimaryId == true) && (a.IsValidForCreate == true || a.IsValidForUpdate == true || a.IsValidForRead == true))
            .Where(a => !a.LogicalName.Contains("entityimage"))
            .Where(a => a.AttributeType != AttributeTypeCode.ManagedProperty);

        return filter.OrderBy(a => a.LogicalName);
    }

    private static string CamelCase(string phrase)
    {
        return Formatter.CamelCase(phrase);
    }

    private string Unique(string value, string scope)
    {
        if (!_usedTokens.ContainsKey(scope)) _usedTokens.Add(scope, new List<string>());

        if (_usedTokens[scope].Contains(value) || value == $"{CamelCase(EntityMetadata.SchemaName)}")
            return Unique(value + "_", scope);

        _usedTokens[scope].Add(value);
        return value;
    }

    private static string Sanitize(string value, bool allowWhitespace = false, bool allowSafeStringChars = false, bool allowFirstNumber = false)
    {
        return Formatter.Sanitize(value, allowWhitespace, allowSafeStringChars, allowFirstNumber);
    }
    public string GenerateTemplate() => TransformText();

    internal struct TypeScriptType(string definitelyTypedAttributeType, string definitelyTypedControlType, string definitelyType)
    {
        public string DefinitelyTypedAttributeType = definitelyTypedAttributeType;
        public string DefinitelyTypedControlType = definitelyTypedControlType;
        public string DefinitelyType = definitelyType;
    }
}

