// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using System.Text.Json.Serialization;

namespace dgt.power.common.Connections;

public sealed record ClientSecretConnection : ConnectionDefinition
{
    [JsonIgnore]
    public override string TypeDisplayName => "Client Secret";

    public required string TenantId { get; init; }

    public required string ClientId { get; init; }
}
