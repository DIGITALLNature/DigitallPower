// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using Azure.Identity;
using dgt.power.common.Exceptions;

namespace dgt.power.common.Logic;

internal static class AzurePipelinesCredentialFactory
{
    private const string SystemAccessTokenEnvironmentVariable = "SYSTEM_ACCESSTOKEN";
    private const string SystemOidcRequestUriEnvironmentVariable = "SYSTEM_OIDCREQUESTURI";
    private const string SystemCollectionUriEnvironmentVariable = "SYSTEM_COLLECTIONURI";
    private const string SystemTeamProjectIdEnvironmentVariable = "SYSTEM_TEAMPROJECTID";
    private const string SystemHostTypeEnvironmentVariable = "SYSTEM_HOSTTYPE";
    private const string SystemPlanIdEnvironmentVariable = "SYSTEM_PLANID";
    private const string SystemJobIdEnvironmentVariable = "SYSTEM_JOBID";

    public static AzurePipelinesCredential Create(
        string tenantId,
        string clientId,
        string serviceConnectionId,
        string? systemAccessToken = null)
    {
        systemAccessToken ??= Environment.GetEnvironmentVariable(SystemAccessTokenEnvironmentVariable);
        if (string.IsNullOrWhiteSpace(systemAccessToken))
        {
            throw new MissingConnectionException(
                $"Environment variable '{SystemAccessTokenEnvironmentVariable}' is not set. " +
                "Expose it to this pipeline step with 'env: SYSTEM_ACCESSTOKEN: $(System.AccessToken)'.");
        }

        SetOidcRequestUri();
        return new AzurePipelinesCredential(tenantId, clientId, serviceConnectionId, systemAccessToken);
    }

    /// <summary>
    /// Derives the Azure Pipelines OIDC request endpoint from predefined job variables so callers
    /// do not depend on an unrelated task populating SYSTEM_OIDCREQUESTURI.
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
        if (!string.IsNullOrWhiteSpace(value))
        {
            return value;
        }

        throw new MissingConnectionException(
            $"Environment variable '{name}' is not set. This variable is provided automatically " +
            "by Azure Pipelines; ensure this step runs inside an Azure Pipelines job.");
    }
}
