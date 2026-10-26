// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using System.Text.Json.Serialization;

namespace dgt.power.common.Connections;

public sealed record AzureDevOpsFederatedConnection : ConnectionDefinition
{
    [JsonIgnore]
    public override string TypeDisplayName => "Azure DevOps Federated";

    public required string TenantId { get; init; }

    public required string ClientId { get; init; }

    public required string ServiceConnectionId { get; init; }

    // Persisted as connection metadata through System.Text.Json.
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public string? ServiceConnectionName { get; init; }
}
