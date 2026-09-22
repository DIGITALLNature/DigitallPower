// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text.Json.Serialization;

namespace dgt.power.common.Logic;

[JsonDerivedType(typeof(TokenIdentity),typeDiscriminator:"token")]
[JsonDerivedType(typeof(AzureDevOpsFederatedIdentity),typeDiscriminator:"azdo-federated")]
#pragma warning disable CA1724 // "Identity" predates the Azure.Identity package reference introduced for AzurePipelinesCredential; renaming is a breaking change to the persisted profile format.
public class Identity
#pragma warning restore CA1724
{
    public required string ConnectionString { get; init; }

    /// <summary>
    /// Preserved for backward compatibility when reading existing profile files.
    /// Has no effect — removed as a user-facing option in favour of OS-level TLS configuration.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? SecurityProtocol { get; init; }

    /// <summary>
    /// Preserved for backward compatibility when reading existing profile files.
    /// Has no effect — removed as a user-facing option.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool Insecure { get; init; }
}
