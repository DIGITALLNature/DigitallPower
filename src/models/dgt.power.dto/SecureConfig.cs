// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

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
