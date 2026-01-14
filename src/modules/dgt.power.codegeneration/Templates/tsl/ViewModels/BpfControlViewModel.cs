// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

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
                case "B0C6723A-8503-4FD7-BB28-C8A06AC933C2": //CheckBox
                    DefinitelyTypedAttributeType = "Xrm.Attributes.BooleanAttribute";
                    DefinitelyTypedControlType = "Xrm.Controls.BooleanControl";
                    return this;
                case "5B773807-9FB2-42DB-97C3-7A91EFF8ADFF": // DateTime)
                    DefinitelyTypedAttributeType = "Xrm.Attributes.DateAttribute";
                    DefinitelyTypedControlType = "Xrm.Controls.DateControl";
                    return this;
                case "C3EFE0C3-0EC6-42BE-8349-CBD9079DFD8E": //Decimal
                case "AA987274-CE4E-4271-A803-66164311A958": //Duration
                case "0D2C745A-E5A8-4C8F-BA63-C6D3BB604660": //Float
                case "C6D124CA-7EDA-4A60-AEA9-7FB8D318B68F": //Integer
                case "533B9E00-756B-4312-95A0-DC888637AC78": //MoneyValue
                    DefinitelyTypedAttributeType = "Xrm.Attributes.NumberAttribute";
                    DefinitelyTypedControlType = "Xrm.Controls.NumberControl";
                    return this;
                case "ADA2203E-B4CD-49BE-9DDF-234642B43B52": //EmailAddress
                case "6F3FB987-393B-4D2D-859F-9D0F0349B6AD": //EmailBody
                case "FD2A7985-3187-444E-908D-6624B21F69C0": //IFrame?
                case "671A9387-CA5A-4D1E-8AB7-06E39DDCF6B5": //Language
                case "06375649-C143-495E-A496-C962E5B4488E": //Notes
                case "E0DECE4B-6FC8-4A8F-A065-082708572369": //TextArea
                case "4273EDBD-AC1D-40D3-9FB2-095C621B552D": //TextBox
                case "71716B6C-711E-476C-8AB8-5D11542BFB47": //Url
                    DefinitelyTypedAttributeType = "Xrm.Attributes.StringAttribute";
                    DefinitelyTypedControlType = "Xrm.Controls.StringControl";
                    return this;
                case "270BD3DB-D9AF-4782-9025-509E298DEC0A": //Lookup
                case "CBFB742C-14E7-4A17-96BB-1A13F7F64AA2": //PartyListLookup
                case "3EF39988-22BB-4F0B-BBBE-64B5A3748AEE": //Picklist
                case "F3015350-44A2-4AA0-97B5-00166532B5E9": //RegardingLookup
                    DefinitelyTypedAttributeType = "Xrm.Attributes.LookupAttribute";
                    DefinitelyTypedControlType = "Xrm.Controls.LookupControl";
                    return this;
                case "5D68B988-0661-4DB2-BC3E-17598AD3BE6C": //StatusReason
                case "7C624A0B-F59E-493D-9583-638D34759266": //TimeZonePicklist
                case "5C5600E0-1D6E-4205-A272-BE80DA87FD42": //QuickView?
                    DefinitelyTypedAttributeType = "Xrm.Attributes.OptionSetAttribute";
                    DefinitelyTypedControlType = "Xrm.Controls.OptionSetControl";
                    return this;
                case "4AA28AB7-9C13-4F57-A73D-AD894D048B5F": //MultiPicklist
                    DefinitelyTypedAttributeType = "Xrm.Attributes.MultiSelectOptionSetAttribute";
                    DefinitelyTypedControlType = "Xrm.Controls.MultiSelectOptionSetControl";
                    return this;
                case "1E1FC551-F7A8-43AF-AC34-A8DC35C7B6D4": //TickerSymbol?
                case "62B0DF79-0464-470F-8AF7-4483CFEA0C7D": //Map
                case "E7A81278-8635-4D9E-8D4D-59480B391C5B": //Subgrid?
                case "9C5CA0A1-AB4D-4781-BE7E-8DFBE867B87E": //Timer?
                case "E616A57F-20E0-4534-8662-A101B5DDF4E0": //KnowledgeBaseSearch?
                    DefinitelyTypedAttributeType = "Xrm.Attributes.Attribute";
                    DefinitelyTypedControlType = "Xrm.Controls.Control";
                    return this;
                default:
                    DefinitelyTypedAttributeType = "Xrm.Attributes.Attribute";
                    DefinitelyTypedControlType = "Xrm.Controls.Control";
                    return this;
            }
        }
    }
}
