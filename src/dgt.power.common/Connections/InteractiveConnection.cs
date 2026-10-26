// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using System.Text.Json;
using System.Text.Json.Serialization;

namespace dgt.power.common.Connections;

public sealed record InteractiveConnection : ConnectionDefinition
{
    [JsonIgnore]
    public override string TypeDisplayName => "Interactive";

    public string? TenantId { get; init; }

    public JsonElement? AuthenticationRecord { get; init; }
}
