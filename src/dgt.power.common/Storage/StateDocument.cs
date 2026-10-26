// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using System.Text.Json;
using System.Text.Json.Serialization;

namespace dgt.power.common.Storage;

internal sealed class StateDocument
{
    public int SchemaVersion { get; init; } = 1;

    public TelemetryState Telemetry { get; init; } = new();

    public UpdateState Updates { get; init; } = new();

    [JsonExtensionData]
    public Dictionary<string, JsonElement>? AdditionalProperties { get; init; }
}
