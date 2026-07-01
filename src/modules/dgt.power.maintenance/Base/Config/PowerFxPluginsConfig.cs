// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text.Json.Serialization;

namespace dgt.power.maintenance.Base.Config;
// ReSharper disable UnusedAutoPropertyAccessor.Global

public class PowerFxPluginsConfig
{
    [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
    [JsonPropertyName("filter")] public IReadOnlyList<string> FilterAttributes { get; set; } = [];
    [JsonPropertyName("message")] public MessageType Message { get; set; }

    [JsonIgnore] public string? MessageName => Enum.GetName(Message);

    public enum MessageType
    {
        Create,
        Update,
        Delete
    }
}
