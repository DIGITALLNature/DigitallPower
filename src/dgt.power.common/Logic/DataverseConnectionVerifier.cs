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

        if (!service.IsReady)
        {
#pragma warning disable S2302 // "connection" is part of the error message, not a parameter name.
            throw new DataverseConnectionException(
                $"Dataverse connection failed: {service.LastError}",
                service.LastException);
#pragma warning restore S2302
        }

        await service.ExecuteAsync(new WhoAmIRequest(), cancellationToken);
    }
}
