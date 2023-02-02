#pragma warning disable CS8618
using System.Text.Json.Serialization;

namespace dgt.power.dto;

public sealed class TeamTemplate
{
    [JsonPropertyName("teamtemplateid")] public required Guid TeamTemplateId { get; init; }

    [JsonPropertyName("teamtemplatename")] public required string TeamTemplateName { get; init; }

    [JsonPropertyName("description")] public string? Description { get; init; }

    [JsonPropertyName("issystem")] public bool? IsSystem { get; init; }

    [JsonPropertyName("defaultaccessrightsmask")]
    public int? DefaultAccessRightsMask { get; init; }

    [JsonPropertyName("entity")] public required string Entity { get; init; }
}
