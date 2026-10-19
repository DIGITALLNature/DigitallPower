// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text.Json.Serialization;

namespace dgt.power.common.Connections;

public sealed record ClientCertificateConnection : ConnectionDefinition
{
    [JsonIgnore]
    public override string TypeDisplayName => "Client Certificate";

    public required string TenantId { get; init; }

    public required string ClientId { get; init; }

    public string? Thumbprint { get; init; }

    public string? CertificatePath { get; init; }
}
