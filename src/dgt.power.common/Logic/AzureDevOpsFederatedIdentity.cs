// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.common.Logic;

/// <summary>
/// Identity backed by an Azure DevOps Workload Identity Federation (OIDC) service connection.
/// No client secret is stored or required: an access token is derived at connect time via
/// <see cref="Azure.Identity.AzurePipelinesCredential"/>, which exchanges the pipeline job's
/// short-lived OIDC token for an Entra ID access token using the federated credential trust
/// configured on the service connection's app registration.
/// </summary>
public class AzureDevOpsFederatedIdentity : Identity
{
    /// <summary>
    /// The Entra ID tenant that hosts the app registration used by the service connection.
    /// </summary>
    public required string TenantId { get; init; }

    /// <summary>
    /// The application (client) ID of the app registration used by the service connection.
    /// </summary>
    public required string ClientId { get; init; }

    /// <summary>
    /// The GUID of the Azure DevOps service connection (visible in its URL under
    /// Project Settings → Service connections), used to scope the OIDC token request.
    /// </summary>
    public required string ServiceConnectionId { get; init; }
}
