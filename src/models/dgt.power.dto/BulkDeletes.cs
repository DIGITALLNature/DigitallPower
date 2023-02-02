using System.Text.Json.Serialization;

namespace dgt.power.dto;

public sealed class BulkDeletes
{
    [JsonPropertyName("bulk_deletes")]
    [JsonRequired]
    public List<BulkDelete> Deletes { get; init; } = new();
}
