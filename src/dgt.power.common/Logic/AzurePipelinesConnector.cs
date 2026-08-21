// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using Azure.Core;
using Azure.Identity;
using dgt.power.common.Exceptions;
using Microsoft.PowerPlatform.Dataverse.Client;

namespace dgt.power.common.Logic;

/// <summary>
/// Connects to Dataverse using an Azure DevOps Workload Identity Federation (OIDC) service
/// connection. A fresh Entra ID access token is acquired on every call via
/// <see cref="AzurePipelinesCredential"/>, which exchanges the pipeline job's short-lived OIDC
/// token (obtained using the <c>SYSTEM_ACCESSTOKEN</c> environment variable) for an access token
/// — no client secret is ever stored or transmitted.
/// </summary>
internal sealed class AzurePipelinesConnector : IConnector
{
    private const string SystemAccessTokenEnvironmentVariable = "SYSTEM_ACCESSTOKEN";

    private readonly Uri _uri;
    private readonly string[] _scopes;
    private readonly AzurePipelinesCredential _credential;

    internal AzurePipelinesConnector(AzureDevOpsFederatedIdentity identity)
    {
        _uri = new Uri(identity.ConnectionString);
        _scopes = [$"{_uri.Scheme}{Uri.SchemeDelimiter}{_uri.Authority}/.default"];

        var systemAccessToken = Environment.GetEnvironmentVariable(SystemAccessTokenEnvironmentVariable);
        if (string.IsNullOrWhiteSpace(systemAccessToken))
        {
            throw new MissingConnectionException(
                $"Environment variable '{SystemAccessTokenEnvironmentVariable}' is not set. " +
                "Expose it to this pipeline step with 'env: SYSTEM_ACCESSTOKEN: $(System.AccessToken)'.");
        }

        _credential = new AzurePipelinesCredential(
            identity.TenantId,
            identity.ClientId,
            identity.ServiceConnectionId,
            systemAccessToken);
    }

    public Task<IOrganizationServiceAsync2> CreateOrganizationServiceProxyAsync()
    {
        return Task.FromResult<IOrganizationServiceAsync2>(new ServiceClient(_uri, GetTokenAsync));
    }

    public async Task<string> GetTokenAsync(string instanceUri)
    {
        var token = await _credential.GetTokenAsync(new TokenRequestContext(_scopes), CancellationToken.None);
        return token.Token;
    }

    /// <summary>
    /// Attempts to acquire a token without opening a browser or throwing. Returns <c>true</c>
    /// if the OIDC token exchange succeeds (<c>SYSTEM_ACCESSTOKEN</c> is present and the
    /// service connection accepts the federated credential), <c>false</c> otherwise. There is
    /// no interactive fallback for this identity type — a failure means the pipeline
    /// configuration needs to be fixed, not that a user needs to re-authenticate.
    /// </summary>
    internal static async Task<bool> TryAcquireTokenSilentAsync(AzureDevOpsFederatedIdentity identity)
    {
        try
        {
            var connector = new AzurePipelinesConnector(identity);
            await connector.GetTokenAsync(string.Empty);
            return true;
        }
        catch (Exception ex) when (ex is MissingConnectionException or AuthenticationFailedException or CredentialUnavailableException)
        {
            return false;
        }
    }
}
