// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Constants;
using dgt.power.codegeneration.Model;

namespace dgt.power.codegeneration.Templates.tsl.ViewModels
{
    public class FormControlViewModel
    {
        public string AttributeName { get; set; }
        public string ControlName { get; set; }

        public string DefinitelyTypedControlType { get; set; }

        public FormControlViewModel(FormXmlControlData formData) {
            AttributeName = formData.DataFieldName;
            ControlName = formData.ControlId;
            if (formData.IsWebResource)
            {
                DefinitelyTypedControlType = "Xrm.Controls.FramedControl";
                return;
            }
            DefinitelyTypedControlType = MapDefinitelyTypedControlType(formData.ClassId, formData.CustomControlClass);
        }

        private static string MapDefinitelyTypedControlType(string classId, string? customClassId)
        {
            switch (classId)
            {
                case ControlClassIds.CheckBox: // CheckBox
                    return "Xrm.Controls.BooleanControl";
                case ControlClassIds.DateTime: // DateTime
                    return "Xrm.Controls.DateControl";
                case ControlClassIds.DecimalClass: // Decimal
                case ControlClassIds.Duration: // Duration
                case ControlClassIds.FloatClass: // Float
                case ControlClassIds.IntegerClass: // Integer
                case ControlClassIds.MoneyClass: // MoneyValue
                    return "Xrm.Controls.NumberControl";
                case ControlClassIds.EmailAddress: // EmailAddress
                case ControlClassIds.EmailBody: // EmailBody
                case ControlClassIds.Notes: // Notes
                case ControlClassIds.TextArea: // TextArea
                case ControlClassIds.TextBox: // TextBox
                case ControlClassIds.Url: // Url
                    return "Xrm.Controls.StringControl";
                case ControlClassIds.Iframe:// IFrame
                    return "Xrm.Controls.IframeControl";
                case ControlClassIds.LookUp: // Lookup
                case ControlClassIds.PartyListLookup: // PartyListLookup
                case ControlClassIds.RegardingLookup: // RegardingLookup
                    return "Xrm.Controls.LookupControl";
                case ControlClassIds.PickList: // Picklist
                case ControlClassIds.RadioButton: // RadioButtons
                case ControlClassIds.StatusReason: // StatusReason
                    return "Xrm.Controls.OptionSetControl";
                case ControlClassIds.SubGrid: // Subgrid
                    return "Xrm.Controls.GridControl";
                case ControlClassIds.MultiPickList: // MultiPicklist
                    return "Xrm.Controls.MultiSelectOptionSetControl";
                case ControlClassIds.KnowledgeBaseSearch: // KnowledgeBaseSearch
                    return "Xrm.Controls.KbSearchControl";
                case ControlClassIds.QuickView: // QuickView
                    return "Xrm.Control.QuickFormControl";
                case ControlClassIds.Timer: // Timer
                case ControlClassIds.Language: // Language
                case ControlClassIds.TickerSymbol: // TickerSymbol
                case ControlClassIds.MapClass: // Map
                case ControlClassIds.TimeZonePicklist: // TimeZonePicklist
                    return "Xrm.Controls.StandardControl";
                case ControlClassIds.CustomControl: // CustomControl
                    if (!string.IsNullOrEmpty(customClassId))
                    {
                        return MapDefinitelyTypedControlType(customClassId, null);
                    }
                    return "Xrm.Controls.StandardControl";
                default:
                    return "Xrm.Controls.Control";
            }
        }

    }
}
