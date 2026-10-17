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

public sealed partial class DataverseConnection(
    IConnectionStore connectionStore,
    CredentialFactory credentialFactory,
    ConnectionInvocationContext invocationContext,
    IAnsiConsole console)
    : IDataverseConnection, IDisposable
{
    private readonly SemaphoreSlim _gate = new(1, 1);
    private IOrganizationServiceAsync2? _service;

    /// <inheritdoc />
    public async Task<IOrganizationServiceAsync2> GetServiceAsync()
    {
        await _gate.WaitAsync();
        try
        {
            // Only a successful connect is cached, so a failed one is retried on the next call.
            return _service ??= await ConnectCoreAsync();
        }
        finally
        {
            _gate.Release();
        }
    }

    public void Dispose()
    {
        if(_service is IDisposable disposable)
        {
            disposable.Dispose();
        }
        _gate.Dispose();
    }

    private async Task<IOrganizationServiceAsync2> ConnectCoreAsync()
    {
        if (!string.IsNullOrWhiteSpace(invocationContext.ConnectionString))
        {
            var url = ExtractUrlFromConnectionString(invocationContext.ConnectionString);
            return await ConnectUsingAsync(
                new DataverseConnectionStringConnector(invocationContext.ConnectionString, console),
                "ad-hoc",
                url,
                "connection string");
        }

        var (name, definition) = ResolveConnection();
        var credential = credentialFactory.Create(
            name,
            definition,
            invocationContext.NonInteractive,
            invocationContext.AllowUnencryptedStorage);
        return await ConnectUsingAsync(
            new CredentialConnector(definition.Url, credential, invocationContext.NonInteractive),
            name,
            definition.Url,
            GetConnectionTypeName(definition));
    }

    /// <inheritdoc />
    public async Task<bool> CheckAuthAsync()
    {
        if (!string.IsNullOrWhiteSpace(invocationContext.ConnectionString))
        {
            return true;
        }

        var (name, definition) = ResolveConnection();
        var scope = new TokenRequestContext([CredentialConnector.GetScope(definition.Url)]);

        try
        {
            var credential = credentialFactory.Create(
                name,
                definition,
                nonInteractive: true,
                allowUnencryptedStorage: invocationContext.AllowUnencryptedStorage);
            await credential.GetTokenAsync(scope, CancellationToken.None);
            return true;
        }
        catch (AuthenticationRequiredException)
        {
            return false;
        }
        catch (Exception exception) when (exception is AuthenticationFailedException
                                              or CredentialUnavailableException
                                              or MissingConnectionException)
        {
            return false;
        }
    }

    /// <inheritdoc />
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
            nonInteractive: false,
            invocationContext.AllowUnencryptedStorage,
            CancellationToken.None);
        ConnectionDefinition updated = definition switch
        {
            InteractiveConnection interactive => interactive with { AuthenticationRecord = authenticationRecord },
            DeviceCodeConnection deviceCode => deviceCode with { AuthenticationRecord = authenticationRecord },
            _ => throw new InvalidOperationException("Only user connections can be refreshed interactively.")
        };
        connectionStore.Upsert(name, updated, makeCurrent: false);
    }

    private ResolvedConnection ResolveConnection()
    {
        var name = invocationContext.ConnectionName ?? connectionStore.Current;
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

        return new ResolvedConnection(connectionStore.ResolveName(name), definition);
    }

    private async Task<IOrganizationServiceAsync2> ConnectUsingAsync(
        IConnector connector,
        string name,
        string url,
        string connectionType)
    {
        return await console.Status().StartAsync(
            $"Connecting to {Markup.Escape(url)} ({Markup.Escape(connectionType)})...",
            async ctx =>
            {
                try
                {
                    var service = await connector.CreateOrganizationServiceProxyAsync();
                    await CheckWhoAmIAsync(service);
                    ctx.Status = $"[green]{Emoji.Known.CheckMark}[/] Connected to {Markup.Escape(url)}";
                    return service;
                }
#pragma warning disable CA1031 // Wrap connector failures with the selected connection context.
                catch (Exception exception)
                {
                    ctx.Status = $"[red]{Emoji.Known.CrossMark}[/] Connection to {Markup.Escape(url)} failed: {Markup.Escape(exception.Message)}";
                    throw new FailedConnectionException(name, exception);
                }
#pragma warning restore CA1031
            });
    }

    private static async Task CheckWhoAmIAsync(IOrganizationServiceAsync2 service)
    {
        // WhoAmI serves as connection health check - no output needed
        await service.ExecuteAsync(new WhoAmIRequest());
    }

    private static string ExtractUrlFromConnectionString(string connectionString)
    {
        var match = ConnectionUrlRegex().Match(connectionString);
        if (match.Success && Uri.TryCreate(match.Groups[1].Value.Trim(), UriKind.Absolute, out var uri))
        {
            return uri.GetLeftPart(UriPartial.Authority);
        }
        return connectionString;
    }

    private static string GetConnectionTypeName(ConnectionDefinition definition) =>
        definition.GetType().Name;

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
                throw new DataverseConnectionException($"Dataverse connection failed: {service.LastError}", service.LastException);
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
