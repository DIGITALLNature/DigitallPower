// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Model;

namespace dgt.power.codegeneration.Templates.tsl.ViewModels
{
    public class FormControlViewModel
    {
        public string AttributeName { get; set; }
        public string ControlName { get; set; }

        public string DefinitelyTypedControlType { get; set; }

        public FormControlViewModel(FormXmlControlData formData) => ToViewModel(formData);

        public FormControlViewModel ToViewModel(FormXmlControlData formData)
        {
            AttributeName = formData.DataFieldName;
            ControlName = formData.ControlId;
            if (formData.IsWebResource)
            {
                DefinitelyTypedControlType = "Xrm.Controls.FramedControl";
                return this;
            }
            DefinitelyTypedControlType = MapDefinitelyTypedControlType(formData.ClassId, formData.CustomControlClass);
            return this;
        }

        private static string MapDefinitelyTypedControlType(string classId, string? customClassId)
        {
            switch (classId)
            {
                case "B0C6723A-8503-4FD7-BB28-C8A06AC933C2": // CheckBox
                    return "Xrm.Controls.BooleanControl";
                case "5B773807-9FB2-42DB-97C3-7A91EFF8ADFF": // DateTime
                    return "Xrm.Controls.DateControl";
                case "C3EFE0C3-0EC6-42BE-8349-CBD9079DFD8E": // Decimal
                case "AA987274-CE4E-4271-A803-66164311A958": // Duration
                case "0D2C745A-E5A8-4C8F-BA63-C6D3BB604660": // Float
                case "C6D124CA-7EDA-4A60-AEA9-7FB8D318B68F": // Integer
                case "533B9E00-756B-4312-95A0-DC888637AC78": // MoneyValue
                    return "Xrm.Controls.NumberControl";
                case "ADA2203E-B4CD-49BE-9DDF-234642B43B52": // EmailAddress
                case "6F3FB987-393B-4D2D-859F-9D0F0349B6AD": // EmailBody
                case "06375649-C143-495E-A496-C962E5B4488E": // Notes
                case "E0DECE4B-6FC8-4A8F-A065-082708572369": // TextArea
                case "4273EDBD-AC1D-40D3-9FB2-095C621B552D": // TextBox
                case "71716B6C-711E-476C-8AB8-5D11542BFB47": // Url
                    return "Xrm.Controls.StringControl";
                case "FD2A7985-3187-444E-908D-6624B21F69C0": // IFrame
                    return "Xrm.Controls.IframeControl";
                case "270BD3DB-D9AF-4782-9025-509E298DEC0A": // Lookup
                case "CBFB742C-14E7-4A17-96BB-1A13F7F64AA2": // PartyListLookup
                case "F3015350-44A2-4AA0-97B5-00166532B5E9": // RegardingLookup
                    return "Xrm.Controls.LookupControl";
                case "3EF39988-22BB-4F0B-BBBE-64B5A3748AEE": // Picklist
                case "67FAC785-CD58-4F9F-ABB3-4B7DDC6ED5ED": // RadioButtons
                case "5D68B988-0661-4DB2-BC3E-17598AD3BE6C": // StatusReason
                    return "Xrm.Controls.OptionSetControl";
                case "E7A81278-8635-4D9E-8D4D-59480B391C5B": // Subgrid
                    return "Xrm.Controls.GridControl";
                case "4AA28AB7-9C13-4F57-A73D-AD894D048B5F": // MultiPicklist
                    return "Xrm.Controls.MultiSelectOptionSetControl";
                case "E616A57F-20E0-4534-8662-A101B5DDF4E0": // KnowledgeBaseSearch
                    return "Xrm.Controls.KbSearchControl";
                case "9C5CA0A1-AB4D-4781-BE7E-8DFBE867B87E": // Timer
                case "671A9387-CA5A-4D1E-8AB7-06E39DDCF6B5": // Language
                case "5C5600E0-1D6E-4205-A272-BE80DA87FD42": // QuickView
                case "1E1FC551-F7A8-43AF-AC34-A8DC35C7B6D4": // TickerSymbol
                case "62B0DF79-0464-470F-8AF7-4483CFEA0C7D": // Map
                case "7C624A0B-F59E-493D-9583-638D34759266": // TimeZonePicklist
                    return "Xrm.Controls.StandardControl";
                case "F9A8A302-114E-466A-B582-6771B2AE0D92": // CustomControl
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
