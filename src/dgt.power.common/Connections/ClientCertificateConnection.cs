// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

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
