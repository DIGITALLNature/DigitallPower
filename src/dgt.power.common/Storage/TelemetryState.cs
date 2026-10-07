// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text.Json;
using System.Text.Json.Serialization;

namespace dgt.power.common.Storage;

internal sealed class TelemetryState
{
    public bool Enabled { get; set; } = true;

    public string? InstallId { get; set; }

    [JsonExtensionData]
    public Dictionary<string, JsonElement>? AdditionalProperties { get; set; }
}
