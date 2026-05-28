// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Constants;
using dgt.power.codegeneration.Model;

namespace dgt.power.codegeneration.Templates.tsl.ViewModels;
// ReSharper disable UnusedAutoPropertyAccessor.Global

public class FormControlViewModel
{
    public string AttributeName { get; set; }
    public string ControlName { get; set; }

    public string MockXrmControlType { get; set; }

    public string DefinitelyTypedControlType { get; set; }

    public bool IsVisible { get; set; }
    public bool IsDisabled { get; set; }

    public FormControlViewModel(FormXmlControlData formData) {
        AttributeName = formData.DataFieldName;
        ControlName = formData.ControlId;
        IsDisabled = formData.IsDisabled;
        IsVisible = formData.IsVisible;
        if (formData.IsWebResource)
        {
            DefinitelyTypedControlType = ControlClassNames.XrmTypesControlClass.FramedControl;
            MockXrmControlType = XrmMock.Control.StringControl;
            return;
        }
        DefinitelyTypedControlType = MapDefinitelyTypedControlType(formData.ClassId, formData.CustomControlClass);

        MockXrmControlType = MapXrmMockControlType(formData.ClassId, formData.CustomControlClass);
    }

    private static string MapDefinitelyTypedControlType(string classId, string? customClassId)
    {
        switch (classId)
        {
            case ControlClassNames.XrmClassId.CheckBox: // CheckBox
                return ControlClassNames.XrmTypesControlClass.BooleanCtl;
            case ControlClassNames.XrmClassId.DateTime: // DateTime
                return ControlClassNames.XrmTypesControlClass.DateCtl;
            case ControlClassNames.XrmClassId.DecimalClass: // Decimal
            case ControlClassNames.XrmClassId.Duration: // Duration
            case ControlClassNames.XrmClassId.FloatClass: // Float
            case ControlClassNames.XrmClassId.IntegerClass: // Integer
            case ControlClassNames.XrmClassId.MoneyClass: // MoneyValue
                return ControlClassNames.XrmTypesControlClass.NumberCtl;
            case ControlClassNames.XrmClassId.EmailAddress: // EmailAddress
            case ControlClassNames.XrmClassId.EmailBody: // EmailBody
            case ControlClassNames.XrmClassId.Notes: // Notes
            case ControlClassNames.XrmClassId.TextArea: // TextArea
            case ControlClassNames.XrmClassId.TextBox: // TextBox
            case ControlClassNames.XrmClassId.Url: // Url
                return ControlClassNames.XrmTypesControlClass.StringCtl;
            case ControlClassNames.XrmClassId.Iframe:// IFrame
                return ControlClassNames.XrmTypesControlClass.IframeControl;
            case ControlClassNames.XrmClassId.LookUp: // Lookup
            case ControlClassNames.XrmClassId.PartyListLookup: // PartyListLookup
            case ControlClassNames.XrmClassId.RegardingLookup: // RegardingLookup
                return ControlClassNames.XrmTypesControlClass.LookupCtl;
            case ControlClassNames.XrmClassId.PickList: // Picklist
            case ControlClassNames.XrmClassId.RadioButton: // RadioButtons
            case ControlClassNames.XrmClassId.StatusReason: // StatusReason
                return ControlClassNames.XrmTypesControlClass.OptionCtl;
            case ControlClassNames.XrmClassId.SubGrid: // Subgrid
                return ControlClassNames.XrmTypesControlClass.GridControl;
            case ControlClassNames.XrmClassId.MultiPickList: // MultiPicklist
                return ControlClassNames.XrmTypesControlClass.MultiselectCtl;
            case ControlClassNames.XrmClassId.KnowledgeBaseSearch: // KnowledgeBaseSearch
                return ControlClassNames.XrmTypesControlClass.KbSearchControl;
            case ControlClassNames.XrmClassId.QuickView: // QuickView
                return ControlClassNames.XrmTypesControlClass.QuickFormControl;
            case ControlClassNames.XrmClassId.Timer: // Timer
            case ControlClassNames.XrmClassId.Language: // Language
            case ControlClassNames.XrmClassId.TickerSymbol: // TickerSymbol
            case ControlClassNames.XrmClassId.MapClass: // Map
            case ControlClassNames.XrmClassId.TimeZonePicklist: // TimeZonePicklist
                return ControlClassNames.XrmTypesControlClass.StandardControl;
            case ControlClassNames.XrmClassId.CustomControl: // CustomControl
                if (!string.IsNullOrEmpty(customClassId))
                {
                    return MapDefinitelyTypedControlType(customClassId, null);
                }
                return ControlClassNames.XrmTypesControlClass.StandardControl;
            default:
                return ControlClassNames.XrmTypesControlClass.ControlWithNoVal;
        }
    }

    private static string MapXrmMockControlType(string classId, string? customClassId)
    {
        switch (classId)
        {
            case ControlClassNames.XrmClassId.CheckBox: // CheckBox
                return XrmMock.Control.BooleanControl;
            case ControlClassNames.XrmClassId.DateTime: // DateTime
                return XrmMock.Control.DateControl;
            case ControlClassNames.XrmClassId.DecimalClass: // Decimal
            case ControlClassNames.XrmClassId.Duration: // Duration
            case ControlClassNames.XrmClassId.FloatClass: // Float
            case ControlClassNames.XrmClassId.IntegerClass: // Integer
            case ControlClassNames.XrmClassId.MoneyClass: // MoneyValue
                return XrmMock.Control.NumberControl;
            case ControlClassNames.XrmClassId.EmailAddress: // EmailAddress
            case ControlClassNames.XrmClassId.EmailBody: // EmailBody
            case ControlClassNames.XrmClassId.Notes: // Notes
            case ControlClassNames.XrmClassId.TextArea: // TextArea
            case ControlClassNames.XrmClassId.TextBox: // TextBox
            case ControlClassNames.XrmClassId.Url: // Url
                return XrmMock.Control.StringControl;
            case ControlClassNames.XrmClassId.LookUp: // Lookup
            case ControlClassNames.XrmClassId.PartyListLookup: // PartyListLookup
            case ControlClassNames.XrmClassId.RegardingLookup: // RegardingLookup
                return XrmMock.Control.LookupControl;
            case ControlClassNames.XrmClassId.PickList: // Picklist
            case ControlClassNames.XrmClassId.RadioButton: // RadioButtons
            case ControlClassNames.XrmClassId.StatusReason: // StatusReason
                return XrmMock.Control.OptionSetControl;
            case ControlClassNames.XrmClassId.SubGrid: // Subgrid
                return XrmMock.Control.GridControl;
            case ControlClassNames.XrmClassId.MultiPickList: // MultiPicklist
                return XrmMock.Control.MultiSetControl;
            case ControlClassNames.XrmClassId.KnowledgeBaseSearch: // KnowledgeBaseSearch
            case ControlClassNames.XrmClassId.QuickView: // QuickView
                return XrmMock.Control.QuickViewControl;
            case ControlClassNames.XrmClassId.Timer: // Timer
            case ControlClassNames.XrmClassId.Language: // Language
            case ControlClassNames.XrmClassId.TickerSymbol: // TickerSymbol
            case ControlClassNames.XrmClassId.MapClass: // Map
            case ControlClassNames.XrmClassId.TimeZonePicklist: // TimeZonePicklist
            case ControlClassNames.XrmClassId.Iframe:// IFrame
            case ControlClassNames.XrmClassId.CustomControl: // CustomControl
                if (!string.IsNullOrEmpty(customClassId))
                {
                    return MapXrmMockControlType(customClassId, null);
                }
                return XrmMock.Control.StringControl;
            default:
                return XrmMock.Control.StringControl;
        }
    }
}
