// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Constants;
using dgt.power.codegeneration.Model;

namespace dgt.power.codegeneration.Templates.tsl.ViewModels;
// ReSharper disable UnusedAutoPropertyAccessor.Global

public record BpfControlViewModel
{
    public string DataFieldName { get; set; } = string.Empty;
    public string DefinitelyTypedControlType { get; set; } = string.Empty;
    public string DefinitelyTypedAttributeType { get; set; } = string.Empty;

    public string XrmMockControlType { get; set; } = string.Empty;
    public string XrmMockAttributeType { get; set; } = string.Empty;

    public BpfControlViewModel(BpfControlDetail bpfControlDetail) => ToViewModel(bpfControlDetail);

    public BpfControlViewModel ToViewModel(BpfControlDetail bpfControlDetail)
    {
        ArgumentNullException.ThrowIfNull(bpfControlDetail);
        DataFieldName = bpfControlDetail.DataFieldName;
        switch (bpfControlDetail.ClassId)
        {
            case ControlClassNames.XrmClassId.CheckBox: //CheckBox
                DefinitelyTypedControlType = ControlClassNames.XrmTypesControlClass.BooleanCtl;
                DefinitelyTypedAttributeType = ControlClassNames.XrmTypesAttributeClass.BooleanAttr;
                XrmMockControlType = XrmMock.Control.BooleanControl;
                XrmMockAttributeType = XrmMock.Attributes.BooleanAttribute;
                return this;
            case ControlClassNames.XrmClassId.DateTime: // DateTime)
                DefinitelyTypedControlType = ControlClassNames.XrmTypesControlClass.DateCtl;
                DefinitelyTypedAttributeType = ControlClassNames.XrmTypesAttributeClass.DateAttr;
                XrmMockControlType = XrmMock.Control.DateControl;
                XrmMockAttributeType = XrmMock.Attributes.DateAttribute;
                return this;
            case ControlClassNames.XrmClassId.DecimalClass: //Decimal
            case ControlClassNames.XrmClassId.Duration: //Duration
            case ControlClassNames.XrmClassId.FloatClass: //Float
            case ControlClassNames.XrmClassId.IntegerClass: //Integer
            case ControlClassNames.XrmClassId.MoneyClass: //MoneyValue
                DefinitelyTypedAttributeType = ControlClassNames.XrmTypesAttributeClass.NumberAtt;
                DefinitelyTypedControlType = ControlClassNames.XrmTypesControlClass.NumberCtl;
                XrmMockControlType = XrmMock.Control.NumberControl;
                XrmMockAttributeType = XrmMock.Attributes.NumberAttribute;
                return this;
            case ControlClassNames.XrmClassId.EmailAddress: //EmailAddress
            case ControlClassNames.XrmClassId.EmailBody: //EmailBody
            case ControlClassNames.XrmClassId.Notes: //Notes
            case ControlClassNames.XrmClassId.TextArea: //TextArea
            case ControlClassNames.XrmClassId.TextBox: //TextBox
            case ControlClassNames.XrmClassId.Url: //Url
                DefinitelyTypedAttributeType = ControlClassNames.XrmTypesAttributeClass.StringAtt;
                DefinitelyTypedControlType = ControlClassNames.XrmTypesControlClass.StringCtl;
                XrmMockControlType = XrmMock.Control.StringControl;
                XrmMockAttributeType = XrmMock.Attributes.StringAttribute;
                return this;
            case ControlClassNames.XrmClassId.Iframe: //IFrame?
                DefinitelyTypedAttributeType = ControlClassNames.XrmTypesAttributeClass.StringAtt;
                DefinitelyTypedControlType = ControlClassNames.XrmTypesControlClass.IframeControl;
                XrmMockControlType = XrmMock.Control.StringControl;
                XrmMockAttributeType = XrmMock.Attributes.StringAttribute;
                return this;
            case ControlClassNames.XrmClassId.LookUp: //Lookup
            case ControlClassNames.XrmClassId.PartyListLookup: //PartyListLookup
            case ControlClassNames.XrmClassId.RegardingLookup: //RegardingLookup
                DefinitelyTypedAttributeType = ControlClassNames.XrmTypesAttributeClass.LookupAtt;
                DefinitelyTypedControlType = ControlClassNames.XrmTypesControlClass.LookupCtl;
                XrmMockControlType = XrmMock.Control.LookupControl;
                XrmMockAttributeType = XrmMock.Attributes.LookupAttribute;
                return this;
            case ControlClassNames.XrmClassId.PickList: //Picklist
            case ControlClassNames.XrmClassId.RadioButton: //Radio buttons
            case ControlClassNames.XrmClassId.StatusReason: //StatusReason
                DefinitelyTypedAttributeType = ControlClassNames.XrmTypesAttributeClass.OptionAtt;
                DefinitelyTypedControlType = ControlClassNames.XrmTypesControlClass.OptionCtl;
                XrmMockControlType = XrmMock.Control.OptionSetControl;
                XrmMockAttributeType = XrmMock.Attributes.OptionSetAttribute;
                return this;
            case ControlClassNames.XrmClassId.MultiPickList: //MultiPicklist
                DefinitelyTypedAttributeType = ControlClassNames.XrmTypesAttributeClass.MultiselectAtt;
                DefinitelyTypedControlType = ControlClassNames.XrmTypesControlClass.MultiselectCtl;
                XrmMockControlType = XrmMock.Control.MultiSetControl;
                XrmMockAttributeType = XrmMock.Attributes.MultiSetAttribute;
                return this;
            case ControlClassNames.XrmClassId.KnowledgeBaseSearch: //KnowledgeBaseSearch?
                DefinitelyTypedAttributeType = ControlClassNames.XrmTypesAttributeClass.AttributeGeneric;
                DefinitelyTypedControlType = ControlClassNames.XrmTypesControlClass.KbSearchControl;
                return this;
            case ControlClassNames.XrmClassId.Timer: //Timer
            case ControlClassNames.XrmClassId.Language: //Language
            case ControlClassNames.XrmClassId.QuickView: //QuickView
            case ControlClassNames.XrmClassId.TickerSymbol: //TickerSymbol
            case ControlClassNames.XrmClassId.MapClass: //Map
            case ControlClassNames.XrmClassId.TimeZonePicklist: //TimeZonePicklist
                DefinitelyTypedAttributeType = ControlClassNames.XrmTypesAttributeClass.AttributeGeneric;
                DefinitelyTypedControlType = ControlClassNames.XrmTypesControlClass.StandardControl;
                XrmMockControlType = XrmMock.Control.QuickViewControl;
                XrmMockAttributeType = XrmMock.Attributes.StringAttribute;
                return this;
            default:
                DefinitelyTypedAttributeType = ControlClassNames.XrmTypesAttributeClass.AttributeGeneric;
                DefinitelyTypedControlType = ControlClassNames.XrmTypesControlClass.ControlWithNoVal;
                XrmMockControlType = XrmMock.Control.StringControl;
                XrmMockAttributeType = XrmMock.Attributes.StringAttribute;
                return this;
        }
    }
}
