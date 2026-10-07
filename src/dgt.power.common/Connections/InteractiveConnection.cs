// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text.Json;

namespace dgt.power.common.Connections;

public sealed record InteractiveConnection : ConnectionDefinition
{
    public string? TenantId { get; init; }

    public JsonElement? AuthenticationRecord { get; init; }
}
