// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text.RegularExpressions;
using Azure.Core;
using Azure.Identity;
using dgt.power.common.Connections;
using dgt.power.common.Exceptions;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.PowerPlatform.Dataverse.Client.Utils;
using Spectre.Console;

namespace dgt.power.common.Logic;

public sealed partial class XrmConnection(
    IConnectionStore connectionStore,
    CredentialFactory credentialFactory,
    ConnectionInvocationOptions invocationOptions,
    IAnsiConsole console)
    : IXrmConnection
{
    public async Task<IOrganizationServiceAsync2> ConnectAsync()
    {
        if (!string.IsNullOrWhiteSpace(invocationOptions.ConnectionString))
        {
            WriteAdHocConnectionNotice(invocationOptions.ConnectionString);
            return await ConnectUsingAsync(new CrmConnector(invocationOptions.ConnectionString, console), "ad-hoc");
        }

        var (name, definition) = ResolveConnection();
        var credential = credentialFactory.Create(
            name,
            definition,
            invocationOptions.NonInteractive,
            invocationOptions.AllowUnencryptedStorage);
        return await ConnectUsingAsync(
            new CredentialConnector(definition.Url, credential, invocationOptions.NonInteractive),
            name);
    }

    public async Task<bool> CheckAuthAsync()
    {
        if (!string.IsNullOrWhiteSpace(invocationOptions.ConnectionString))
        {
            return true;
        }

        var (name, definition) = ResolveConnection();
        var credential = credentialFactory.Create(
            name,
            definition,
            nonInteractive: true,
            allowUnencryptedStorage: invocationOptions.AllowUnencryptedStorage);
        var scope = new TokenRequestContext([CredentialConnector.GetScope(definition.Url)]);

        try
        {
            await credential.GetTokenAsync(scope, CancellationToken.None);
            return true;
        }
        catch (AuthenticationRequiredException)
        {
            return false;
        }
        catch (Exception exception) when (exception is AuthenticationFailedException or CredentialUnavailableException)
        {
            return false;
        }
    }

    public async Task RefreshAuthAsync()
    {
        var (name, definition) = ResolveConnection();
        if (definition is not InteractiveConnection and not DeviceCodeConnection)
        {
            return;
        }

        var freshLoginDefinition = definition switch
        {
            InteractiveConnection interactive => interactive with { AuthenticationRecord = null },
            DeviceCodeConnection deviceCode => deviceCode with { AuthenticationRecord = null },
            _ => definition
        };
        var authenticationRecord = await credentialFactory.AuthenticateAsync(
            freshLoginDefinition,
            invocationOptions.AllowUnencryptedStorage,
            CancellationToken.None);
        ConnectionDefinition updated = definition switch
        {
            InteractiveConnection interactive => interactive with { AuthenticationRecord = authenticationRecord },
            DeviceCodeConnection deviceCode => deviceCode with { AuthenticationRecord = authenticationRecord },
            _ => throw new InvalidOperationException("Only user connections can be refreshed interactively.")
        };
        connectionStore.Upsert(name, updated, makeCurrent: false);
    }

    private (string Name, ConnectionDefinition Definition) ResolveConnection()
    {
        var name = invocationOptions.ConnectionName ?? connectionStore.Current;
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new MissingConnectionException(
                "No connection is selected. Use 'dgtp connection select <name>' or 'dgtp connection create'.");
        }

        var definition = connectionStore.Find(name);
        if (definition is null)
        {
            throw new MissingConnectionException(
                $"Connection '{name}' was not found. Create it with 'dgtp connection create'.");
        }

        return (name, definition);
    }

    private async Task<IOrganizationServiceAsync2> ConnectUsingAsync(IConnector connector, string name)
    {
        try
        {
            var service = await connector.CreateOrganizationServiceProxyAsync();
            await CheckWhoAmIAsync(service);
            return service;
        }
#pragma warning disable CA1031 // Wrap connector failures with the selected connection context.
        catch (Exception exception)
        {
            throw new FailedConnectionException(name, exception);
        }
#pragma warning restore CA1031
    }

    private async Task CheckWhoAmIAsync(IOrganizationServiceAsync2 service)
    {
        var userId = ((WhoAmIResponse)await service.ExecuteAsync(new WhoAmIRequest())).UserId;
        console.MarkupLine($"WhoAmI: [bold]{userId:D}[/]");
    }

    private void WriteAdHocConnectionNotice(string connectionString)
    {
        var match = ConnectionUrlRegex().Match(connectionString);
        var url = match.Success && Uri.TryCreate(match.Groups[1].Value.Trim(), UriKind.Absolute, out var uri)
            ? $" for {uri.GetLeftPart(UriPartial.Authority)}"
            : string.Empty;
        console.MarkupLine($"Using ad-hoc connection string (not persisted){Markup.Escape(url)}");
    }

    [GeneratedRegex(@"(?:^|;)\s*(?:Url|ServiceUri)\s*=\s*([^;]+)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant)]
    private static partial Regex ConnectionUrlRegex();

    private sealed class CredentialConnector(string url, TokenCredential credential, bool nonInteractive) : IConnector
    {
        public Task<IOrganizationServiceAsync2> CreateOrganizationServiceProxyAsync()
        {
            var uri = new Uri(url);
            var scope = new TokenRequestContext([GetScope(url)]);
            var service = new ServiceClient(uri, async _ => await GetTokenAsync(credential, scope));
            if (!service.IsReady)
            {
                throw new DataverseConnectionException($"XRM Connection Failed: {service.LastError}", service.LastException);
            }

            return Task.FromResult<IOrganizationServiceAsync2>(service);
        }

        private async Task<string> GetTokenAsync(TokenCredential tokenCredential, TokenRequestContext context)
        {
            try
            {
                return (await tokenCredential.GetTokenAsync(context, CancellationToken.None)).Token;
            }
            catch (AuthenticationRequiredException exception) when (nonInteractive)
            {
                throw new InteractiveLoginRequiredException(new Uri(url).Authority, exception);
            }
        }

        public static string GetScope(string url)
        {
            var uri = new Uri(url);
            return $"{uri.GetLeftPart(UriPartial.Authority)}/.default";
        }
    }
}
