// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Logic;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;

namespace dgt.power.codegeneration.Templates.tsl;

public class BaseTemplate(EntityMetadata entityMetadata, int systemLanguage, bool useBaseLanguage)
{
    private readonly Dictionary<string, List<string>> _usedTokens = new();

    internal static string Summary(string description, int indent)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            return string.Empty;
        }

        return "/// <summary>" +
               $"{Environment.NewLine}" +
               $"{new string('\t', indent)}/// {description.Replace("\n", $"\n{new string('\t', indent)}/// ").Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Trim()}" +
               $"{Environment.NewLine}" +
               $"{new string('\t', indent)}/// </summary>" +
               $"{Environment.NewLine}";
    }
    internal string GetLocalizedLabel(Label label) => Formatter.GetLocalizedLabel(label, useBaseLanguage, systemLanguage);

    internal EntityLightFormTemplate.TypeScriptType GetTypeScriptTypes(AttributeMetadata attr)
    {
        // TODO: Detect multiselect optionsets
        switch (attr.AttributeType)
        {
           case AttributeTypeCode.Boolean:
               return new EntityLightFormTemplate.TypeScriptType
               {
                   DefinitelyTypedAttributeType = "Xrm.Attributes.BooleanAttribute",
                   DefinitelyTypedControlType = "Xrm.Controls.BooleanControl",
                   DefinitelyType = "Optionset",
               };
            case AttributeTypeCode.DateTime:
                return new EntityLightFormTemplate.TypeScriptType
                {
                    DefinitelyTypedAttributeType = "Xrm.Attributes.DateAttribute",
                    DefinitelyTypedControlType = "Xrm.Controls.DateControl",
                    DefinitelyType = GetDateTimeType(attr as DateTimeAttributeMetadata),
                };
            case AttributeTypeCode.Decimal:
                return new EntityLightFormTemplate.TypeScriptType
                {
                    DefinitelyTypedAttributeType = "Xrm.Attributes.NumberAttribute",
                    DefinitelyTypedControlType = "Xrm.Controls.NumberControl",
                    DefinitelyType ="Decimal",
                };
            case AttributeTypeCode.Double:
                return new EntityLightFormTemplate.TypeScriptType
                {
                    DefinitelyTypedAttributeType = "Xrm.Attributes.NumberAttribute",
                    DefinitelyTypedControlType = "Xrm.Controls.NumberControl",
                    DefinitelyType ="Double",
                };
            case AttributeTypeCode.Money:
            case AttributeTypeCode.Integer:
                return new EntityLightFormTemplate.TypeScriptType
                {
                    DefinitelyTypedAttributeType = "Xrm.Attributes.NumberAttribute",
                    DefinitelyTypedControlType = "Xrm.Controls.NumberControl",
                    DefinitelyType ="Integer",
                };
            case AttributeTypeCode.BigInt:
                return new EntityLightFormTemplate.TypeScriptType
            {
                DefinitelyTypedAttributeType = "Xrm.Attributes.NumberAttribute",
                DefinitelyTypedControlType = "Xrm.Controls.NumberControl",
                DefinitelyType ="BigInt",
            };
            case AttributeTypeCode.Customer:
            case AttributeTypeCode.PartyList:
            case AttributeTypeCode.Owner:
            case AttributeTypeCode.Lookup:
                return new EntityLightFormTemplate.TypeScriptType
                {
                    DefinitelyTypedAttributeType = "Xrm.Attributes.LookupAttribute",
                    DefinitelyTypedControlType = "Xrm.Controls.LookupControl",
                    DefinitelyType ="Lookup",
                };
            case AttributeTypeCode.String:
            case AttributeTypeCode.Memo:
                return new EntityLightFormTemplate.TypeScriptType
                {
                    DefinitelyTypedAttributeType = "Xrm.Attributes.StringAttribute",
                    DefinitelyTypedControlType = "Xrm.Controls.StringControl",
                    DefinitelyType = "String",
                };
           case AttributeTypeCode.Uniqueidentifier:
               return new EntityLightFormTemplate.TypeScriptType
               {
                   DefinitelyTypedAttributeType = "Xrm.Attributes.Attribute",
                   DefinitelyTypedControlType = "Xrm.Controls.Control",
                   DefinitelyType = "Guid",
               };
            case AttributeTypeCode.State:
            case AttributeTypeCode.Status:
            case AttributeTypeCode.Picklist:
                return new EntityLightFormTemplate.TypeScriptType
                {
                    DefinitelyTypedAttributeType = "Xrm.Attributes.OptionSetAttribute",
                    DefinitelyTypedControlType = "Xrm.Controls.OptionSetControl",
                    DefinitelyType = "OptionSet",
                };
            default:
                if (attr is MultiSelectPicklistAttributeMetadata)
                {
                    return new EntityLightFormTemplate.TypeScriptType
                    {
                        DefinitelyTypedAttributeType = "Xrm.Attributes.MultiSelectOptionSetAttribute",
                        DefinitelyTypedControlType = "Xrm.Controls.MultiSelectOptionSetControl",
                        DefinitelyType = "MultiSelectOptionSet",
                    };
                }

                return new EntityLightFormTemplate.TypeScriptType
                {
                    DefinitelyTypedAttributeType = "Xrm.Attributes.Attribute",
                    DefinitelyTypedControlType = "Xrm.Controls.Control",
                    DefinitelyType ="Attribute",
                };

        }
    }

    internal string GetDateTimeType(DateTimeAttributeMetadata? attr)
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

    internal string Unique(string value, string scope)
    {
        if (!_usedTokens.ContainsKey(scope)) _usedTokens.Add(scope, new List<string>());

        if (_usedTokens[scope].Contains(value) || value == $"{entityMetadata.SchemaName.ToCamelCase()}")
            return Unique(value + "_", scope);

        _usedTokens[scope].Add(value);
        return value;
    }
}
