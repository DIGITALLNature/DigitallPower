// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text.RegularExpressions;
using Azure.Core;
using Azure.Identity;
using dgt.power.common.Connections;
using dgt.power.common.Exceptions;
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
    private readonly Dictionary<Guid, ServiceClient> _callerServices = new();
    private IDataverseConnector? _connector;
    private string? _selectedUrl;
    private string? _selectedType;
    private bool _disposed;

    /// <inheritdoc />
    public async Task<IOrganizationServiceAsync2> GetServiceAsync()
    {
        await _gate.WaitAsync();
        try
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            // Only a successful connect is cached, so a failed one is retried on the next call.
            return _service ??= await ConnectCoreAsync();
        }
        finally
        {
            _gate.Release();
        }
    }

    /// <inheritdoc />
    public Task<IOrganizationServiceAsync2> GetServiceAsync(Guid callerId)
    {
        if (callerId == Guid.Empty)
        {
            throw new ArgumentException("A Dataverse systemuser ID is required.", nameof(callerId));
        }

        return GetCallerServiceAsync(callerId);
    }

    private async Task<IOrganizationServiceAsync2> GetCallerServiceAsync(Guid callerId)
    {
        await _gate.WaitAsync();
        try
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            _service ??= await ConnectCoreAsync();
            if (_callerServices.TryGetValue(callerId, out var cached))
            {
                return cached;
            }

            ServiceClient? client = null;
            var published = false;
            try
            {
                client = await _connector!.CreateServiceClientAsync();
                DataverseConnectionVerifier.EnsureReady(client);
                client.CallerId = callerId;
                if (client.CallerId != callerId)
                {
                    throw new DataverseConnectionException("Dataverse caller configuration failed.");
                }
                _callerServices.Add(callerId, client);
                published = true;
                return client;
            }
            finally
            {
                if (!published)
                {
                    client?.Dispose();
                }
            }
        }
        finally
        {
            _gate.Release();
        }
    }

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }
        _disposed = true;
        if (_service is IDisposable disposable)
        {
            disposable.Dispose();
        }
        foreach (var service in _callerServices.Values)
        {
            service.Dispose();
        }
        _callerServices.Clear();
        _gate.Dispose();
    }

    private async Task<IOrganizationServiceAsync2> ConnectCoreAsync()
    {
        if (_connector is null)
        {
            if (!string.IsNullOrWhiteSpace(invocationContext.ConnectionString))
            {
                _selectedUrl = ExtractUrlFromConnectionString(invocationContext.ConnectionString)?
                    .GetComponents(UriComponents.SchemeAndServer, UriFormat.UriEscaped)
                    ?? "Dataverse environment";
                _selectedType = "Connection String";
                _connector = new ConnectionStringConnector(invocationContext.ConnectionString);
            }
            else
            {
                var resolved = ResolveConnection();
                _selectedUrl = resolved.Definition.Url;
                _selectedType = resolved.Definition.TypeDisplayName;
                _connector = new CredentialConnector(credentialFactory, resolved, invocationContext);
            }
        }

        return await ConnectUsingAsync(_connector, _selectedUrl!, _selectedType!);
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

    internal async Task<IOrganizationServiceAsync2> ConnectUsingAsync(
        IDataverseConnector connector,
        string url,
        string connectionType)
    {
        var target = $"{Markup.Escape(url)} ({Markup.Escape(connectionType)})";
        ServiceClient? service = null;
        var verified = false;
        try
        {
            service = await connector.CreateServiceClientAsync();
            await DataverseConnectionVerifier.VerifyAsync(service, CancellationToken.None);
            console.MarkupLine($"[green]{Emoji.Known.CheckMark}[/] Connected to {target}");
            verified = true;
            return service;
        }
        catch (Exception exception) when (exception is not OperationCanceledException)
        {
            console.MarkupLine($"[red]{Emoji.Known.CrossMark}[/] Connection to {target} failed");
            throw;
        }
        finally
        {
            if (!verified)
            {
                service?.Dispose();
            }
        }
    }

    private static Uri? ExtractUrlFromConnectionString(string connectionString)
    {
        var match = ConnectionUrlRegex().Match(connectionString);
        if (match.Success && Uri.TryCreate(match.Groups[1].Value.Trim(), UriKind.Absolute, out var uri))
        {
            return uri;
        }
        return null;
    }

    [GeneratedRegex(@"(?:^|;)\s*(?:Url|ServiceUri)\s*=\s*([^;]+)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant)]
    private static partial Regex ConnectionUrlRegex();
}
