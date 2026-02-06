// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Constants;
using Microsoft.Xrm.Sdk.Metadata;

namespace dgt.power.codegeneration.Templates.tsl.ViewModels;

public record AttributeMetadataViewModel
{
    public string DefinitelyType { get; set; }
    public string DefinitelyTypedAttributeType { get; set; }
    public string DefinitelyTypedControlType { get; set; }
    public bool IsPrimaryId { get; set; }
    public string LogicalName { get; set; }
    public string NativeType { get; set; }
    public string XrmMockTypeAttributeType { get; set; }
    public string XrmMockControlType { get; set; }

    public string RequiredLevel { get; set; }

    public OptionMetadataCollection Options { get; set; }
    public string SchemaName { get; set; }

    public AttributeMetadataViewModel(AttributeMetadata attributeMetadata) => ToViewModel(attributeMetadata);

    public AttributeMetadataViewModel ToViewModel(AttributeMetadata attributeMetadata)
    {
        LogicalName = attributeMetadata.LogicalName;
        SchemaName = attributeMetadata.SchemaName;
        IsPrimaryId = attributeMetadata.IsPrimaryId.GetValueOrDefault();
        RequiredLevel = MapAttributeRequiredLevel(attributeMetadata.RequiredLevel);

        switch (attributeMetadata.AttributeType)
        {
            case AttributeTypeCode.Boolean:
                DefinitelyTypedAttributeType = ControlClassNames.XrmTypesAttributeClass.BooleanAttr;
                DefinitelyTypedControlType = ControlClassNames.XrmTypesControlClass.BooleanCtl;
                DefinitelyType = "Boolean";
                NativeType = "boolean";
                XrmMockTypeAttributeType = XrmMock.Attributes.BooleanAttribute;
                XrmMockControlType = XrmMock.Control.BooleanControl;
                break;
            case AttributeTypeCode.DateTime:
                DefinitelyTypedAttributeType = ControlClassNames.XrmTypesAttributeClass.DateAttr;
                DefinitelyTypedControlType = ControlClassNames.XrmTypesControlClass.DateCtl;
                DefinitelyType = GetDateTimeType(attributeMetadata as DateTimeAttributeMetadata);
                NativeType = "string";
                XrmMockTypeAttributeType = XrmMock.Attributes.StringAttribute;
                XrmMockControlType = XrmMock.Control.StringControl;
                break;
            case AttributeTypeCode.Decimal:
                DefinitelyTypedAttributeType = ControlClassNames.XrmTypesAttributeClass.NumberAtt;
                DefinitelyTypedControlType = ControlClassNames.XrmTypesControlClass.NumberCtl;
                DefinitelyType = "Decimal";
                NativeType = "number";
                XrmMockTypeAttributeType = XrmMock.Attributes.NumberAttribute;
                XrmMockControlType = XrmMock.Control.NumberControl;
                break;
            case AttributeTypeCode.Double:
                DefinitelyTypedAttributeType = ControlClassNames.XrmTypesAttributeClass.NumberAtt;
                DefinitelyTypedControlType = ControlClassNames.XrmTypesControlClass.NumberCtl;
                DefinitelyType = "Double";
                NativeType = "number";
                XrmMockTypeAttributeType = XrmMock.Attributes.NumberAttribute;
                XrmMockControlType = XrmMock.Control.NumberControl;
                break;
            case AttributeTypeCode.Money:
            case AttributeTypeCode.Integer:
                DefinitelyTypedAttributeType = ControlClassNames.XrmTypesAttributeClass.NumberAtt;
                DefinitelyTypedControlType = ControlClassNames.XrmTypesControlClass.NumberCtl;
                DefinitelyType = "Integer";
                NativeType = "number";
                XrmMockTypeAttributeType = XrmMock.Attributes.NumberAttribute;
                XrmMockControlType = XrmMock.Control.NumberControl;
                break;
            case AttributeTypeCode.BigInt:
                DefinitelyTypedAttributeType = ControlClassNames.XrmTypesAttributeClass.NumberAtt;
                DefinitelyTypedControlType = ControlClassNames.XrmTypesControlClass.NumberCtl;
                DefinitelyType = "BigInt";
                NativeType = "number";
                XrmMockTypeAttributeType = XrmMock.Attributes.NumberAttribute;
                XrmMockControlType = XrmMock.Control.NumberControl;
                break;                
            case AttributeTypeCode.Customer:
            case AttributeTypeCode.PartyList:
            case AttributeTypeCode.Owner:
            case AttributeTypeCode.Lookup:
                DefinitelyTypedAttributeType = ControlClassNames.XrmTypesAttributeClass.LookupAtt;
                DefinitelyTypedControlType = ControlClassNames.XrmTypesControlClass.LookupCtl;
                DefinitelyType = "Lookup";
                NativeType = "any";
                XrmMockTypeAttributeType = XrmMock.Attributes.LookupAttribute;
                XrmMockControlType = XrmMock.Control.LookupControl;
                break;
            case AttributeTypeCode.String:
            case AttributeTypeCode.Memo:
                DefinitelyTypedAttributeType = ControlClassNames.XrmTypesAttributeClass.StringAtt;
                DefinitelyTypedControlType = ControlClassNames.XrmTypesControlClass.StringCtl;
                DefinitelyType = "String";
                NativeType = "string";
                XrmMockTypeAttributeType = XrmMock.Attributes.StringAttribute;
                XrmMockControlType = XrmMock.Control.StringControl;
                break;
            case AttributeTypeCode.Uniqueidentifier:
                DefinitelyTypedAttributeType = ControlClassNames.XrmTypesAttributeClass.AttributeWithNoVal;
                DefinitelyTypedControlType = ControlClassNames.XrmTypesControlClass.ControlWithNoVal;
                DefinitelyType = "Guid";
                NativeType = "string";
                XrmMockTypeAttributeType = XrmMock.Attributes.StringAttribute;
                XrmMockControlType = XrmMock.Control.StringControl;
                break;
            case AttributeTypeCode.State:
            case AttributeTypeCode.Status:
            case AttributeTypeCode.Picklist:
                DefinitelyTypedAttributeType = ControlClassNames.XrmTypesAttributeClass.OptionAtt;
                DefinitelyTypedControlType = ControlClassNames.XrmTypesControlClass.OptionCtl;
                DefinitelyType = "OptionSet";
                NativeType = "number";
                XrmMockTypeAttributeType = XrmMock.Attributes.OptionSetAttribute;
                XrmMockControlType = XrmMock.Control.OptionSetControl;
                break;
            default:
                if (attributeMetadata is MultiSelectPicklistAttributeMetadata)
                {
                    DefinitelyTypedAttributeType = ControlClassNames.XrmTypesAttributeClass.MultiselectAtt;
                    DefinitelyTypedControlType = ControlClassNames.XrmTypesControlClass.MultiselectCtl;
                    DefinitelyType = "MultiSelectOptionSet";
                    NativeType = "string";
                    XrmMockTypeAttributeType = XrmMock.Attributes.MultiSetAttribute;
                    XrmMockControlType = XrmMock.Control.MultiSetControl;
                    break;
                }
                if (attributeMetadata is FileAttributeMetadata)
                {
                    DefinitelyTypedAttributeType = ControlClassNames.XrmTypesAttributeClass.AttributeGeneric;
                    DefinitelyTypedControlType = ControlClassNames.XrmTypesControlClass.StandardControl;
                    DefinitelyType = "Attribute";
                    NativeType = "string";
                    XrmMockTypeAttributeType = XrmMock.Attributes.StringAttribute;
                    XrmMockControlType = XrmMock.Control.StringControl;
                    break;
                }
                DefinitelyTypedAttributeType = ControlClassNames.XrmTypesAttributeClass.AttributeWithNoVal;
                DefinitelyTypedControlType = ControlClassNames.XrmTypesControlClass.ControlWithNoVal;
                DefinitelyType = "Attribute";
                NativeType = "any";
                XrmMockTypeAttributeType = XrmMock.Attributes.StringAttribute;
                XrmMockControlType = XrmMock.Control.StringControl;

                break;
        }
        if (attributeMetadata is EnumAttributeMetadata enumAttributeMetadata)
        {
            Options = enumAttributeMetadata.OptionSet.Options;
        }
        return this;
    }

    private static string MapAttributeRequiredLevel(AttributeRequiredLevelManagedProperty requiredLevel)
    {
        switch(requiredLevel.Value)
        {
            case AttributeRequiredLevel.None:
                return XrmMock.RequiredLevel.None;
            case AttributeRequiredLevel.ApplicationRequired:
            case AttributeRequiredLevel.SystemRequired:
                return XrmMock.RequiredLevel.Required;
            case AttributeRequiredLevel.Recommended:
                return XrmMock.RequiredLevel.Recommended;
        }
        return XrmMock.RequiredLevel.None;
    }

    private static string GetDateTimeType(DateTimeAttributeMetadata? attr)
    {

        if (attr?.DateTimeBehavior == DateTimeBehavior.DateOnly && attr?.Format == DateTimeFormat.DateOnly)
        {
            return "DateOnly:DateOnly";
        }

        if (attr?.DateTimeBehavior == DateTimeBehavior.TimeZoneIndependent)
        {
            return "DateOnly:TimeZoneIndependent";
        }

        return "DateAndTime:UserLocal";
    }

}

