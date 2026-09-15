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
/// — no client secret is ever stored or transmitted. The OIDC token request URL
/// (<c>SYSTEM_OIDCREQUESTURI</c>) is derived from standard predefined job variables rather than
/// relying on a built-in task to populate it - see <see cref="SetOidcRequestUri"/>.
/// </summary>
internal sealed class AzurePipelinesConnector : IConnector
{
    private const string SystemAccessTokenEnvironmentVariable = "SYSTEM_ACCESSTOKEN";
    private const string SystemOidcRequestUriEnvironmentVariable = "SYSTEM_OIDCREQUESTURI";
    private const string SystemCollectionUriEnvironmentVariable = "SYSTEM_COLLECTIONURI";
    private const string SystemTeamProjectIdEnvironmentVariable = "SYSTEM_TEAMPROJECTID";
    private const string SystemHostTypeEnvironmentVariable = "SYSTEM_HOSTTYPE";
    private const string SystemPlanIdEnvironmentVariable = "SYSTEM_PLANID";
    private const string SystemJobIdEnvironmentVariable = "SYSTEM_JOBID";

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

        SetOidcRequestUri();

        _credential = new AzurePipelinesCredential(
            identity.TenantId,
            identity.ClientId,
            identity.ServiceConnectionId,
            systemAccessToken);
    }

    /// <summary>
    /// <see cref="AzurePipelinesCredential"/> reads its OIDC token endpoint from the
    /// <c>SYSTEM_OIDCREQUESTURI</c> environment variable, which Azure DevOps only populates
    /// automatically for a handful of built-in tasks (for example <c>AzureCLI@2</c> and
    /// <c>AzurePowerShell@5</c>) that declare an ARM service connection input. A plain script
    /// step — which is how dgtp is invoked — never gets it for free.
    ///
    /// Rather than requiring an extra pipeline task purely to make Azure DevOps populate this
    /// variable, dgtp derives the same URL itself from predefined job variables that Azure
    /// DevOps always exposes as environment variables (no <c>env:</c> mapping required, unlike
    /// <c>SYSTEM_ACCESSTOKEN</c>): the URL shape mirrors the Azure DevOps REST API's
    /// <see href="https://learn.microsoft.com/en-us/rest/api/azure/devops/distributedtask/oidctoken/create">OIDC token creation endpoint</see>.
    ///
    /// This always overwrites any pre-existing value rather than trusting one left behind by an
    /// earlier task in the same job (e.g. an <c>AzureCLI@2</c> step for an unrelated ARM service
    /// connection) — its provenance and continued validity for this job can't be verified, while
    /// the value derived here is always correct for the current job.
    /// </summary>
    private static void SetOidcRequestUri()
    {
        var collectionUri = RequireEnvironmentVariable(SystemCollectionUriEnvironmentVariable);
        var teamProjectId = RequireEnvironmentVariable(SystemTeamProjectIdEnvironmentVariable);
        var hostType = RequireEnvironmentVariable(SystemHostTypeEnvironmentVariable);
        var planId = RequireEnvironmentVariable(SystemPlanIdEnvironmentVariable);
        var jobId = RequireEnvironmentVariable(SystemJobIdEnvironmentVariable);

        var oidcRequestUri =
            $"{collectionUri}{teamProjectId}/_apis/distributedtask/hubs/{hostType}/plans/{planId}/jobs/{jobId}/oidctoken";
        Environment.SetEnvironmentVariable(SystemOidcRequestUriEnvironmentVariable, oidcRequestUri);
    }

    private static string RequireEnvironmentVariable(string name)
    {
        var value = Environment.GetEnvironmentVariable(name);
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new MissingConnectionException(
                $"Environment variable '{name}' is not set. This variable is provided automatically " +
                "by Azure Pipelines; ensure this step runs inside an Azure Pipelines job.");
        }

        return value;
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
