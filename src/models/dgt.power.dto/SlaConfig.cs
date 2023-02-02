using System.Text.Json.Serialization;

namespace dgt.power.dto;

public sealed class SlaConfig : Assignee
{
    //TODO: make mandatory in the future
    [JsonPropertyName("Name")] public string? Name { get; init; }

    [JsonPropertyName("SlaId")] public required Guid? SlaId { get; init; }

    [JsonPropertyName("BusinessHours")] public required Guid? BusinessHours { get; init; }

    [JsonPropertyName("Active")] public required bool? Active { get; init; }

    //TODO: add in the future
    //[JsonPropertyName("PrimaryEntity")]
    //internal string PrimaryEntity { get; set; }
}
