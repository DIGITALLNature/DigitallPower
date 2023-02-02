#pragma warning disable CS8618
using System.Text.Json.Serialization;

namespace dgt.power.dto;

public sealed class OutlookTemplate
{
    [JsonPropertyName("Name")] public required string Name { get; init; }

    [JsonPropertyName("FetchXml")] public required string FetchXml { get; init; }

    [JsonPropertyName("Entity")] public required string Entity { get; init; }

    [JsonPropertyName("IsDefault")] public required bool IsDefault { get; init; }

    [JsonPropertyName("Description")] public required string Description { get; init; }
}
