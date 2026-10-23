// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using Azure.Core;
using dgt.power.common.Connections;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.PowerPlatform.Dataverse.Client.Utils;

namespace dgt.power.common.Logic;

public sealed class DataverseConnectionVerifier(CredentialFactory credentialFactory) : IConnectionVerifier
{
    public Task VerifyAsync(
        string connectionName,
        ConnectionDefinition connection,
        bool allowUnencryptedStorage,
        CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionName);
        ArgumentNullException.ThrowIfNull(connection);

        return VerifyCoreAsync(connectionName, connection, allowUnencryptedStorage, cancellationToken);
    }

    private async Task VerifyCoreAsync(string connectionName, ConnectionDefinition connection, bool allowUnencryptedStorage, CancellationToken cancellationToken)
    {
        var credential = credentialFactory.Create(
            connectionName,
            connection,
            nonInteractive: true,
            allowUnencryptedStorage);
        var uri = new Uri(connection.Url);
        var scope = new TokenRequestContext([$"{uri.GetLeftPart(UriPartial.Authority)}/.default"]);
        using var service = new ServiceClient(
            uri,
            async _ => (await credential.GetTokenAsync(scope, cancellationToken)).Token);

        await VerifyAsync(service, cancellationToken);
    }

    internal static Task VerifyAsync(ServiceClient service, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(service);
        return VerifyCoreAsync(service, cancellationToken);
    }

    private static async Task VerifyCoreAsync(ServiceClient service, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        EnsureReady(service);
        await service.ExecuteAsync(new WhoAmIRequest(), cancellationToken);
    }

    internal static void EnsureReady(ServiceClient service)
    {
        if (!service.IsReady)
        {
#pragma warning disable S2302 // "connection" is part of the error message, not a parameter name.
            var message = $"Dataverse connection failed: {service.LastError}";
#pragma warning restore S2302
            throw service.LastException is { } exception
                ? new DataverseConnectionException(message, exception)
                : new DataverseConnectionException(message);
        }

    }
}
