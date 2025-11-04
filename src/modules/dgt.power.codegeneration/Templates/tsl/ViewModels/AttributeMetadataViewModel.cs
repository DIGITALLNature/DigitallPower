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

        switch (attributeMetadata.AttributeType)
        {
            case AttributeTypeCode.Boolean:
                DefinitelyTypedAttributeType = "Xrm.Attributes.BooleanAttribute";
                DefinitelyTypedControlType = "Xrm.Controls.BooleanControl";
                DefinitelyType = "Optionset";
                break;
            case AttributeTypeCode.DateTime:
                DefinitelyTypedAttributeType = "Xrm.Attributes.DateAttribute";
                DefinitelyTypedControlType = "Xrm.Controls.DateControl";
                DefinitelyType = GetDateTimeType(attributeMetadata as DateTimeAttributeMetadata);
                break;
            case AttributeTypeCode.Decimal:
                DefinitelyTypedAttributeType = "Xrm.Attributes.NumberAttribute";
                DefinitelyTypedControlType = "Xrm.Controls.NumberControl";
                DefinitelyType = "Decimal";
                break;
            case AttributeTypeCode.Double:
                DefinitelyTypedAttributeType = "Xrm.Attributes.NumberAttribute";
                DefinitelyTypedControlType = "Xrm.Controls.NumberControl";
                DefinitelyType = "Double";
                break;
            case AttributeTypeCode.Money:
            case AttributeTypeCode.Integer:
                DefinitelyTypedAttributeType = "Xrm.Attributes.NumberAttribute";
                DefinitelyTypedControlType = "Xrm.Controls.NumberControl";
                DefinitelyType = "Integer";
                break;
            case AttributeTypeCode.BigInt:
                DefinitelyTypedAttributeType = "Xrm.Attributes.NumberAttribute";
                DefinitelyTypedControlType = "Xrm.Controls.NumberControl";
                DefinitelyType = "BigInt";
                break;
            case AttributeTypeCode.Customer:
            case AttributeTypeCode.PartyList:
            case AttributeTypeCode.Owner:
            case AttributeTypeCode.Lookup:
                DefinitelyTypedAttributeType = "Xrm.Attributes.LookupAttribute";
                DefinitelyTypedControlType = "Xrm.Controls.LookupControl";
                DefinitelyType = "Lookup";
                break;
            case AttributeTypeCode.String:
            case AttributeTypeCode.Memo:

                DefinitelyTypedAttributeType = "Xrm.Attributes.StringAttribute";
                DefinitelyTypedControlType = "Xrm.Controls.StringControl";
                DefinitelyType = "String";
                break;
            case AttributeTypeCode.Uniqueidentifier:
                DefinitelyTypedAttributeType = "Xrm.Attributes.Attribute";
                DefinitelyTypedControlType = "Xrm.Controls.Control";
                DefinitelyType = "Guid";
                break;
            case AttributeTypeCode.State:
            case AttributeTypeCode.Status:
            case AttributeTypeCode.Picklist:
                DefinitelyTypedAttributeType = "Xrm.Attributes.OptionSetAttribute";
                DefinitelyTypedControlType = "Xrm.Controls.OptionSetControl";
                DefinitelyType = "OptionSet";
                break;
            default:
                if (attributeMetadata is MultiSelectPicklistAttributeMetadata)
                {
                    DefinitelyTypedAttributeType = "Xrm.Attributes.MultiSelectOptionSetAttribute";
                    DefinitelyTypedControlType = "Xrm.Controls.MultiSelectOptionSetControl";
                    DefinitelyType = "MultiSelectOptionSet";
                    break;
                }

                DefinitelyTypedAttributeType = "Xrm.Attributes.Attribute";
                DefinitelyTypedControlType = "Xrm.Controls.Control";
                DefinitelyType = "Attribute";

                break;
        }

        if (attributeMetadata is PicklistAttributeMetadata picklistAttributeMetadata)
        {
            Options = picklistAttributeMetadata.OptionSet.Options;
        }
    }

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
}
