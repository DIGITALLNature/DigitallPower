using System.Text.Json.Serialization;

namespace dgt.power.dto;

public sealed class RoutingRuleItem
{
    [JsonPropertyName("Name")] public required string Name { get; init; }

    [JsonPropertyName("RoutingRuleItemId")]
    public required Guid RoutingRuleItemId { get; init; }

    [JsonPropertyName("RoutingRuleId")] public required Guid RoutingRuleId { get; set; }

    [JsonPropertyName("RoutedQueueId")] public Guid? RoutedQueueId { get; init; }

    [JsonPropertyName("AssignObjectIdType")]
    public string? AssignObjectIdType { get; init; }

    [JsonPropertyName("AssignObjectIdName")]
    public string? AssignObjectIdName { get; init; }

    [JsonPropertyName("MsdynRouteto")] public int? MsdynRouteto { get; init; }
}
