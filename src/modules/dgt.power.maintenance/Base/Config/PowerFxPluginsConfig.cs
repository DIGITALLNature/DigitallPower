// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text.Json.Serialization;

namespace dgt.power.maintenance.Base.Config;

public class PowerFxPluginsConfigs : List<PowerFxPluginsConfig>
{

}

public class PowerFxPluginsConfig
{
    [JsonPropertyName("name")] public string Name { get; set; }
    [JsonPropertyName("filter")] public string[] FilterAttributes { get; set; } = Array.Empty<string>();
    [JsonPropertyName("message")] public MessageType Message { get; set; }

    [JsonIgnore] public string? MessageName => Enum.GetName(Message);

    public enum MessageType
    {
        Create,
        Update,
        Delete
    }
}


