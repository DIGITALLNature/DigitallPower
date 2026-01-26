// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Constants;
using dgt.power.codegeneration.Model;

namespace dgt.power.codegeneration.Templates.tsl.ViewModels
{
    public record BpfControlViewModel
    {        
        public string DataFieldName { get; set; }
        public string DefinitelyTypedControlType { get; set; }
        public string DefinitelyTypedAttributeType { get; set; }

        public BpfControlViewModel(BpfControlDetail bpfControlDetail) => ToViewModel(bpfControlDetail);

        public BpfControlViewModel ToViewModel(BpfControlDetail bpfControlDetail)
        {
            DataFieldName = bpfControlDetail.DataFieldName;
            switch (bpfControlDetail.ClassId)
            {
                case ControlClassIds.CheckBox: //CheckBox
                    DefinitelyTypedControlType = "Xrm.Controls.BooleanControl";
                    DefinitelyTypedAttributeType = "Xrm.Attributes.BooleanAttribute";
                    return this;
                case ControlClassIds.DateTime: // DateTime)
                    DefinitelyTypedControlType = "Xrm.Controls.DateControl";
                    DefinitelyTypedAttributeType = "Xrm.Attributes.DateAttribute";
                    return this;
                case ControlClassIds.DecimalClass: //Decimal
                case ControlClassIds.Duration: //Duration
                case ControlClassIds.FloatClass: //Float
                case ControlClassIds.IntegerClass: //Integer
                case ControlClassIds.MoneyClass: //MoneyValue
                    DefinitelyTypedAttributeType = "Xrm.Attributes.NumberAttribute";
                    DefinitelyTypedControlType = "Xrm.Controls.NumberControl";
                    return this;
                case ControlClassIds.EmailAddress: //EmailAddress
                case ControlClassIds.EmailBody: //EmailBody
                case ControlClassIds.Notes: //Notes
                case ControlClassIds.TextArea: //TextArea
                case ControlClassIds.TextBox: //TextBox
                case ControlClassIds.Url: //Url
                    DefinitelyTypedAttributeType = "Xrm.Attributes.StringAttribute";
                    DefinitelyTypedControlType = "Xrm.Controls.StringControl";
                    return this;
                case ControlClassIds.Iframe: //IFrame?
                    DefinitelyTypedAttributeType = "Xrm.Attributes.StringAttribute";
                    DefinitelyTypedControlType = "Xrm.Controls.IframeControl";
                    return this;
                case ControlClassIds.LookUp: //Lookup
                case ControlClassIds.PartyListLookup: //PartyListLookup
                case ControlClassIds.RegardingLookup: //RegardingLookup
                    DefinitelyTypedAttributeType = "Xrm.Attributes.LookupAttribute";
                    DefinitelyTypedControlType = "Xrm.Controls.LookupControl";
                    return this;
                case ControlClassIds.PickList: //Picklist
                case ControlClassIds.RadioButton: //Radio buttons
                case ControlClassIds.StatusReason: //StatusReason
                    DefinitelyTypedAttributeType = "Xrm.Attributes.OptionSetAttribute";
                    DefinitelyTypedControlType = "Xrm.Controls.OptionSetControl";
                    return this;
                case ControlClassIds.MultiPickList: //MultiPicklist
                    DefinitelyTypedAttributeType = "Xrm.Attributes.MultiSelectOptionSetAttribute";
                    DefinitelyTypedControlType = "Xrm.Controls.MultiSelectOptionSetControl";
                    return this;
                case ControlClassIds.KnowledgeBaseSearch: //KnowledgeBaseSearch?
                    DefinitelyTypedAttributeType = "Xrm.Attributes.Attribute<Xrm.Attributes.AttributeValues>";
                    DefinitelyTypedControlType = "Xrm.Controls.KbSearchControl";
                    return this;
                case ControlClassIds.Timer: //Timer
                case ControlClassIds.Language: //Language
                case ControlClassIds.QuickView: //QuickView
                case ControlClassIds.TickerSymbol: //TickerSymbol
                case ControlClassIds.MapClass: //Map
                case ControlClassIds.TimeZonePicklist: //TimeZonePicklist
                    DefinitelyTypedAttributeType = "Xrm.Attributes.Attribute<Xrm.Attributes.AttributeValues>";
                    DefinitelyTypedControlType = "Xrm.Controls.StandardControl";
                    return this;
                default:
                    DefinitelyTypedAttributeType = "Xrm.Attributes.Attribute<Xrm.Attributes.AttributeValues>";
                    DefinitelyTypedControlType = "Xrm.Controls.Control";
                    return this;
            }
        }
    }
}
