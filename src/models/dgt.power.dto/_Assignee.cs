using System.Text.Json.Serialization;

namespace dgt.power.dto;

public abstract class Assignee
{
    [JsonPropertyName("owner")] public string? Owner { get; init; }
}
