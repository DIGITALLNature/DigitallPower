// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using Microsoft.Xrm.Sdk.Metadata;

namespace dgt.power.solution.Base;

internal sealed record FieldProtectionDecision(
    Guid MetadataId,
    string EntityName,
    string LogicalName,
    int? SourceType,
    AttributeTypeCode? AttributeType,
    bool RequiresUpdate,
    string Reason)
{
    public string Identity => $"{EntityName}.{LogicalName}";

    public string CalculationType => SourceType switch
    {
        1 => "Calculated",
        2 => "Rollup",
        3 => "Power Fx Formula",
        _ => "Non-calculation"
    };

    public AttributeMetadata CreateUpdate()
    {
        AttributeMetadata metadata = AttributeType switch
        {
            AttributeTypeCode.String => new StringAttributeMetadata(),
            AttributeTypeCode.Integer => new IntegerAttributeMetadata(),
            AttributeTypeCode.Decimal => new DecimalAttributeMetadata(),
            AttributeTypeCode.Double => new DoubleAttributeMetadata(),
            AttributeTypeCode.Money => new MoneyAttributeMetadata(),
            AttributeTypeCode.DateTime => new DateTimeAttributeMetadata(),
            AttributeTypeCode.Boolean => new BooleanAttributeMetadata(),
            AttributeTypeCode.Picklist => new PicklistAttributeMetadata(),
            _ => throw new InvalidOperationException($"Unsupported calculation storage type for {Identity}: {AttributeType}")
        };
        metadata.MetadataId = MetadataId;
        metadata.LogicalName = LogicalName;
        metadata.IsCustomizable = new Microsoft.Xrm.Sdk.BooleanManagedProperty(false);
        return metadata;
    }
}
