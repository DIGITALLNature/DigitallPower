// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using dgt.power.common.Exceptions;

// ReSharper disable ClassNeverInstantiated.Local
// ReSharper disable UnusedAutoPropertyAccessor.Local
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local
// The nested DTOs below are only ever populated by System.Text.Json via reflection when
// deserializing the Azure DevOps REST API response, so ReSharper can't see them being used.

namespace dgt.power.common.Logic;

/// <summary>
/// The non-secret values of an Azure DevOps service connection needed to open an
/// <see cref="AzureDevOpsFederatedIdentity"/> connection, as resolved from its name.
/// </summary>
#pragma warning disable CA1056, S3996, CA1054 // Url is intentionally a string, not Uri, to mirror AzureDevOpsFederatedIdentity/CreateConnectionSettings.
public sealed record ResolvedServiceConnection(string Url, string TenantId, string ClientId, string ServiceConnectionId);
#pragma warning restore CA1056, S3996, CA1054

/// <summary>
/// Resolves an Azure DevOps Power Platform service connection (type <c>powerplatform-spn</c>) by
/// name to its non-secret <see cref="ResolvedServiceConnection"/> values, via the Azure DevOps
/// REST API (<c>GET .../_apis/serviceendpoint/endpoints?endpointNames=...</c>). This lets users
/// pass just a service connection name to <c>dgtp connection create --azure-devops-federated</c>
/// instead of separately resolving and passing tenant/application/service-connection IDs
/// themselves (e.g. via an external pipeline template).
/// </summary>
public static class AzureDevOpsServiceConnectionResolver
{
    private const string SystemAccessTokenEnvironmentVariable = "SYSTEM_ACCESSTOKEN";
    private const string CollectionUriEnvironmentVariable = "SYSTEM_TEAMFOUNDATIONCOLLECTIONURI";
    private const string TeamProjectIdEnvironmentVariable = "SYSTEM_TEAMPROJECTID";
    private const string ApiVersion = "7.1";
    private const string EndpointType = "powerplatform-spn";

    // A single, shared HttpClient instance is intentional (avoids socket exhaustion under
    // repeated short-lived instantiation); this resolver is only ever used from a one-off CLI
    // invocation, so there is no need for IHttpClientFactory/DI wiring here.
    private static readonly HttpClient s_httpClient = new();

    public static async Task<ResolvedServiceConnection> ResolveAsync(string serviceConnectionName, CancellationToken cancellationToken)
    {
        var systemAccessToken = RequireEnvironmentVariable(
            SystemAccessTokenEnvironmentVariable,
            "Expose it to this pipeline step with 'env: SYSTEM_ACCESSTOKEN: $(System.AccessToken)'.");
        var collectionUri = RequireEnvironmentVariable(CollectionUriEnvironmentVariable, null);
        var teamProjectId = RequireEnvironmentVariable(TeamProjectIdEnvironmentVariable, null);

        var requestUri =
            $"{collectionUri.TrimEnd('/')}/{teamProjectId}/_apis/serviceendpoint/endpoints" +
            $"?endpointNames={Uri.EscapeDataString(serviceConnectionName)}&type={EndpointType}&api-version={ApiVersion}";

        using var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", systemAccessToken);

        using var response = await s_httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
        if (!response.IsSuccessStatusCode)
        {
            throw new ServiceConnectionResolutionException(
                $"Failed to resolve Azure DevOps service connection '{serviceConnectionName}' " +
                $"({(int)response.StatusCode} {response.ReasonPhrase}). Ensure the pipeline's build identity " +
                "(usually 'Project Collection Build Service') has Reader access to the service connection, " +
                "or use --tenant/--application-id/--service-connection-id instead.");
        }

        var body = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
        var payload = JsonSerializer.Deserialize<ServiceEndpointListResponse>(body);
        var endpoints = payload?.Value ?? [];

        if (endpoints.Count == 0)
        {
            throw new ServiceConnectionResolutionException(
                $"No Azure DevOps service connection named '{serviceConnectionName}' was found in this project. " +
                "Check the name, or use --tenant/--application-id/--service-connection-id instead.");
        }

        if (endpoints.Count > 1)
        {
            var ids = string.Join(", ", endpoints.Select(e => e.Id));
            throw new ServiceConnectionResolutionException(
                $"Found {endpoints.Count} Azure DevOps service connections named '{serviceConnectionName}' " +
                $"(ids: {ids}). The name is ambiguous (service connections can share a name across folders) " +
                "— use --tenant/--application-id/--service-connection-id instead.");
        }

        var endpoint = endpoints[0];
        var parameters = endpoint.Authorization?.Parameters ?? new Dictionary<string, string>();

        if (!parameters.TryGetValue("tenantid", out var tenantId) || string.IsNullOrWhiteSpace(tenantId))
        {
            throw new ServiceConnectionResolutionException(
                $"Azure DevOps service connection '{serviceConnectionName}' has no 'tenantid' authorization parameter. " +
                "It may not be configured for Workload Identity Federation.");
        }

        if (!parameters.TryGetValue("serviceprincipalid", out var clientId) || string.IsNullOrWhiteSpace(clientId))
        {
            throw new ServiceConnectionResolutionException(
                $"Azure DevOps service connection '{serviceConnectionName}' has no 'serviceprincipalid' authorization parameter. " +
                "It may not be configured for Workload Identity Federation.");
        }

        return new ResolvedServiceConnection(endpoint.Url, tenantId, clientId, endpoint.Id);
    }

    private static string RequireEnvironmentVariable(string name, string? hint)
    {
        var value = Environment.GetEnvironmentVariable(name);
        if (!string.IsNullOrWhiteSpace(value))
        {
            return value;
        }

        var message = $"Environment variable '{name}' is not set.";
        throw new ServiceConnectionResolutionException(hint is null ? message : $"{message} {hint}");
    }

    private sealed class ServiceEndpointListResponse
    {
        [JsonPropertyName("value")]
        public List<ServiceEndpointDto> Value { get; init; } = [];
    }

    private sealed class ServiceEndpointDto
    {
        [JsonPropertyName("id")]
        public string Id { get; init; } = string.Empty;

        [JsonPropertyName("url")]
#pragma warning disable CA1056, S3996, CA1054 // mirrors ResolvedServiceConnection.Url, kept as string
        public string Url { get; init; } = string.Empty;
#pragma warning restore CA1056, S3996, CA1054

        [JsonPropertyName("authorization")]
        public ServiceEndpointAuthorizationDto? Authorization { get; init; }
    }

    private sealed class ServiceEndpointAuthorizationDto
    {
        [JsonPropertyName("parameters")]
        public Dictionary<string, string> Parameters { get; init; } = new();
    }
}
