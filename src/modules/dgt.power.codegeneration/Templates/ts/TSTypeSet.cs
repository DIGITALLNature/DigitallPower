using Microsoft.Xrm.Sdk.Metadata;

namespace dgt.power.codegeneration.Templates.ts
{
    internal class TSTypeSet
    {
        private TSTypeSet(string xrmType) : this(xrmType, xrmType, xrmType)
        {
        }

        private TSTypeSet(string xrmType, string attributeType) : this(xrmType, attributeType, attributeType)
        {
        }

        private TSTypeSet(string xrmType, string attributeType, string controlType)
        {
            Attribute = $"Xrm.Attributes.{attributeType}Attribute";
            Control = $"Xrm.Controls.{controlType}Control";
            XrmEnum = $"XrmEnum.AttributeType.{xrmType}";
        }

        public string Attribute { get; }
        public string Control { get; }
        public string XrmEnum { get; }

        public static TSTypeSet ConvertType(AttributeTypeCode? code, string logicalname)
        {
            switch (code)
            {
                case AttributeTypeCode.Boolean:
                    return new TSTypeSet("Boolean", "Boolean", "Standard");
                case AttributeTypeCode.DateTime:
                    return new TSTypeSet("DateTime", "Date");
                case AttributeTypeCode.Decimal:
                    return new TSTypeSet("Decimal", "Number");
                case AttributeTypeCode.Double:
                    return new TSTypeSet("Double", "Number");
                case AttributeTypeCode.Integer:
                case AttributeTypeCode.BigInt:
                    return new TSTypeSet("Integer", "Number");
                case AttributeTypeCode.Lookup:
                case AttributeTypeCode.Customer:
                case AttributeTypeCode.Owner:
                case AttributeTypeCode.PartyList:
                case AttributeTypeCode.Uniqueidentifier:
                    return new TSTypeSet("Lookup");
                case AttributeTypeCode.Memo:
                    return new TSTypeSet("Memo", "String");
                case AttributeTypeCode.Money:
                    return new TSTypeSet("Money", "Number");
                case AttributeTypeCode.String:
                case AttributeTypeCode.EntityName:
                    return new TSTypeSet("String");
                case AttributeTypeCode.Picklist:
                case AttributeTypeCode.State:
                case AttributeTypeCode.Status:
                    return new TSTypeSet("OptionSet");
                case AttributeTypeCode.Virtual:
                    return new TSTypeSet("MultiOptionSet", "OptionSet");
                default:
                    throw new ArgumentOutOfRangeException($"{code} not supportet for {logicalname} - fatal");
            }
        }
    }
}