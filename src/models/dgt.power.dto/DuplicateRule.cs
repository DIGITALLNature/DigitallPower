#pragma warning disable CS8618
using System.Text.Json.Serialization;

namespace dgt.power.dto;

public sealed class DuplicateRule
{
    [JsonPropertyName("duplicateruleid")] public required Guid DuplicateRuleId { get; set; }

    [JsonPropertyName("name")] public required string Name { get; set; }

    [JsonPropertyName("baseentityname")] public required string BaseEntityName { get; set; }

    [JsonPropertyName("matchingentityname")]
    public required string MatchingEntityName { get; set; }

    [JsonPropertyName("iscasesensitive")] public required bool IsCaseSensitive { get; set; }

    [JsonPropertyName("excludeinactiverecords")]
    public required bool ExcludeInactiveRecords { get; set; }

    [JsonPropertyName("duplicate_rule_conditions")]
    public required List<DuplicateRuleCondition> DuplicateRuleConditions { get; set; } = new();
}
