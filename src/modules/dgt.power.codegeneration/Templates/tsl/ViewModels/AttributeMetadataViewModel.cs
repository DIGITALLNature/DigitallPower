// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using Microsoft.Xrm.Sdk.Metadata;

namespace dgt.power.codegeneration.Templates.tsl.ViewModels;

public record AttributeMetadataViewModel
{
    public AttributeMetadataViewModel(AttributeMetadata attributeMetadata)
    {
        LogicalName = attributeMetadata.LogicalName;
        SchemaName = attributeMetadata.SchemaName;

        IsPrimaryId = attributeMetadata.IsPrimaryId.GetValueOrDefault();

        switch (attributeMetadata.AttributeType)
        {
            case AttributeTypeCode.Boolean:
                DefinitelyTypedAttributeType = "Xrm.Attributes.BooleanAttribute";
                DefinitelyTypedControlType = "Xrm.Controls.BooleanControl";
                DefinitelyType = "Boolean";
                NativeType = "boolean";
                break;
            case AttributeTypeCode.DateTime:
                DefinitelyTypedAttributeType = "Xrm.Attributes.DateAttribute";
                DefinitelyTypedControlType = "Xrm.Controls.DateControl";
                DefinitelyType = GetDateTimeType(attributeMetadata as DateTimeAttributeMetadata);
                NativeType = "string";
                break;
            case AttributeTypeCode.Decimal:
                DefinitelyTypedAttributeType = "Xrm.Attributes.NumberAttribute";
                DefinitelyTypedControlType = "Xrm.Controls.NumberControl";
                DefinitelyType = "Decimal";
                NativeType = "number";
                break;
            case AttributeTypeCode.Double:
                DefinitelyTypedAttributeType = "Xrm.Attributes.NumberAttribute";
                DefinitelyTypedControlType = "Xrm.Controls.NumberControl";
                DefinitelyType = "Double";
                NativeType = "number";
                break;
            case AttributeTypeCode.Money:
            case AttributeTypeCode.Integer:
                DefinitelyTypedAttributeType = "Xrm.Attributes.NumberAttribute";
                DefinitelyTypedControlType = "Xrm.Controls.NumberControl";
                DefinitelyType = "Integer";
                NativeType = "number";
                break;
            case AttributeTypeCode.BigInt:
                DefinitelyTypedAttributeType = "Xrm.Attributes.NumberAttribute";
                DefinitelyTypedControlType = "Xrm.Controls.NumberControl";
                DefinitelyType = "BigInt";
                NativeType = "number";
                break;
            case AttributeTypeCode.Customer:
            case AttributeTypeCode.PartyList:
            case AttributeTypeCode.Owner:
            case AttributeTypeCode.Lookup:
                DefinitelyTypedAttributeType = "Xrm.Attributes.LookupAttribute";
                DefinitelyTypedControlType = "Xrm.Controls.LookupControl";
                DefinitelyType = "Lookup";
                NativeType = "any";
                break;
            case AttributeTypeCode.String:
            case AttributeTypeCode.Memo:

                DefinitelyTypedAttributeType = "Xrm.Attributes.StringAttribute";
                DefinitelyTypedControlType = "Xrm.Controls.StringControl";
                DefinitelyType = "String";
                NativeType = "string";
                break;
            case AttributeTypeCode.Uniqueidentifier:
                DefinitelyTypedAttributeType = "Xrm.Attributes.Attribute";
                DefinitelyTypedControlType = "Xrm.Controls.Control";
                DefinitelyType = "Guid";
                NativeType = "string";
                break;
            case AttributeTypeCode.State:
            case AttributeTypeCode.Status:
            case AttributeTypeCode.Picklist:
                DefinitelyTypedAttributeType = "Xrm.Attributes.OptionSetAttribute";
                DefinitelyTypedControlType = "Xrm.Controls.OptionSetControl";
                DefinitelyType = "OptionSet";
                NativeType = "number";
                break;
            default:
                if (attributeMetadata is MultiSelectPicklistAttributeMetadata)
                {
                    DefinitelyTypedAttributeType = "Xrm.Attributes.MultiSelectOptionSetAttribute";
                    DefinitelyTypedControlType = "Xrm.Controls.MultiSelectOptionSetControl";
                    DefinitelyType = "MultiSelectOptionSet";
                    NativeType = "string";
                    break;
                }

                DefinitelyTypedAttributeType = "Xrm.Attributes.Attribute";
                DefinitelyTypedControlType = "Xrm.Controls.Control";
                DefinitelyType = "Attribute";
                NativeType = "any";

                break;
        }

        if (attributeMetadata is EnumAttributeMetadata enumAttributeMetadata)
        {
            Options = enumAttributeMetadata.OptionSet.Options;
        }
    }

    public string NativeType { get; set; }

    public OptionMetadataCollection Options { get; set; }

    public string SchemaName { get; set; }

    private string GetDateTimeType(DateTimeAttributeMetadata? attr)
    {
        if (attr.DateTimeBehavior == DateTimeBehavior.DateOnly)
        {
            if (attr.Format == DateTimeFormat.DateOnly)
            {
                return "DateOnly:DateOnly";
            }

            return "DateOnly:UserLocal";
        }

        if (attr.DateTimeBehavior == DateTimeBehavior.TimeZoneIndependent)
        {
            return "DateOnly:TimeZoneIndependent";
        }

        return "DateAndTime:UserLocal";
    }

    public string DefinitelyType { get; init; }

    public string DefinitelyTypedControlType { get; init; }

    public string LogicalName { get; init; }

    public string DefinitelyTypedAttributeType { get; init; }

    public bool IsPrimaryId { get; init; }
}
