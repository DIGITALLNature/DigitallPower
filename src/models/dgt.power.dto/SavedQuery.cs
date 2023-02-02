using System.Text.Json.Serialization;

namespace dgt.power.dto;

public sealed class SavedQuery
{
    [JsonPropertyName("disabled_outlook_templates")]
    public ICollection<string> DisabledOutlookTemplates { get; init; } = new List<string>();

    [JsonPropertyName("outlook_templates")]
    public ICollection<OutlookTemplate> OutlookTemplates { get; init; } = new List<OutlookTemplate>();
}
