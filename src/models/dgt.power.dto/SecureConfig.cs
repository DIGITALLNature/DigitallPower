#pragma warning disable CS8618
using System.Text.Json.Serialization;

namespace dgt.power.dto;

public sealed class SecureConfig
{
    [JsonPropertyName("plugin_step")]
    [JsonRequired]
    public string PluginStep { get; init; }

    [JsonPropertyName("secure_config")] public string? Data { get; init; }
}
