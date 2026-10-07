// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text.Json;
using System.Text.Json.Serialization;

namespace dgt.power.common.Storage;

internal sealed class StateDocument
{
    public int SchemaVersion { get; set; } = 1;

    public TelemetryState Telemetry { get; set; } = new();

    public UpdateState Updates { get; set; } = new();

    [JsonExtensionData]
    public Dictionary<string, JsonElement>? AdditionalProperties { get; set; }
}
