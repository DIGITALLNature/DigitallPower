// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using Microsoft.Xrm.Sdk.Metadata;

namespace dgt.power.codegeneration.Templates.ts;

internal sealed class TsTypeSet
{
    private TsTypeSet(string xrmType) : this(xrmType, xrmType, xrmType)
    {
    }

    private TsTypeSet(string xrmType, string attributeType) : this(xrmType, attributeType, attributeType)
    {
    }

    private TsTypeSet(string xrmType, string attributeType, string controlType)
    {
        Attribute = $"Xrm.Attributes.{attributeType}Attribute";
        Control = $"Xrm.Controls.{controlType}Control";
        XrmEnum = $"XrmEnum.AttributeType.{xrmType}";
    }

    public string Attribute { get; }
    public string Control { get; }
    public string XrmEnum { get; }

    public static TsTypeSet ConvertType(AttributeTypeCode? code, string logicalname)
    {
        switch (code)
        {
            case AttributeTypeCode.Boolean:
                return new TsTypeSet("Boolean", "Boolean", "Standard");
            case AttributeTypeCode.DateTime:
                return new TsTypeSet("DateTime", "Date");
            case AttributeTypeCode.Decimal:
                return new TsTypeSet("Decimal", "Number");
            case AttributeTypeCode.Double:
                return new TsTypeSet("Double", "Number");
            case AttributeTypeCode.Integer:
            case AttributeTypeCode.BigInt:
                return new TsTypeSet("Integer", "Number");
            case AttributeTypeCode.Lookup:
            case AttributeTypeCode.Customer:
            case AttributeTypeCode.Owner:
            case AttributeTypeCode.PartyList:
            case AttributeTypeCode.Uniqueidentifier:
                return new TsTypeSet("Lookup");
            case AttributeTypeCode.Memo:
                return new TsTypeSet("Memo", "String");
            case AttributeTypeCode.Money:
                return new TsTypeSet("Money", "Number");
            case AttributeTypeCode.String:
            case AttributeTypeCode.EntityName:
                return new TsTypeSet("String");
            case AttributeTypeCode.Picklist:
            case AttributeTypeCode.State:
            case AttributeTypeCode.Status:
                return new TsTypeSet("OptionSet");
            case AttributeTypeCode.Virtual:
                return new TsTypeSet("MultiOptionSet", "OptionSet");
            default:
                throw new ArgumentOutOfRangeException($"{code} not supportet for {logicalname} - fatal");
        }
    }
}