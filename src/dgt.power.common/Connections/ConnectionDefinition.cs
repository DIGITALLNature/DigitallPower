// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using System.Text.Json.Serialization;

namespace dgt.power.common.Connections;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(InteractiveConnection), "interactive")]
[JsonDerivedType(typeof(DeviceCodeConnection), "deviceCode")]
[JsonDerivedType(typeof(ClientSecretConnection), "clientSecret")]
[JsonDerivedType(typeof(ClientCertificateConnection), "clientCertificate")]
[JsonDerivedType(typeof(AzureDevOpsFederatedConnection), "azureDevOpsFederated")]
public abstract record ConnectionDefinition
{
#pragma warning disable CA1056, S3996 // Serialized as the URL string used by the Dataverse connection model.
    public required string Url { get; init; }
#pragma warning restore CA1056, S3996

    [JsonIgnore]
    public abstract string TypeDisplayName { get; }
}
