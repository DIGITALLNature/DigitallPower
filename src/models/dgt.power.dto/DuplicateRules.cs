using System.Text.Json.Serialization;

namespace dgt.power.dto;

public sealed class DuplicateRules : Assignee
{
    [JsonPropertyName("duplicate_rules")]
    [JsonRequired]
    public List<DuplicateRule> Rules { get; set; } = new();
}
