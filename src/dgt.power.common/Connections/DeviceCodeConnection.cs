// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using System.Text.Json;
using System.Text.Json.Serialization;

namespace dgt.power.common.Connections;

public sealed record DeviceCodeConnection : ConnectionDefinition
{
    [JsonIgnore]
    public override string TypeDisplayName => "Device Code";

    public string? TenantId { get; init; }

    public JsonElement? AuthenticationRecord { get; init; }
}
