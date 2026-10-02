// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using Azure.Core;
using dgt.power.common.Connections;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.PowerPlatform.Dataverse.Client.Utils;
using Microsoft.Xrm.Sdk;

namespace dgt.power.common.Logic;

public sealed class DataverseConnectionVerifier(CredentialFactory credentialFactory) : IConnectionVerifier
{
    public async Task VerifyAsync(
        string connectionName,
        ConnectionDefinition connection,
        bool allowUnencryptedStorage,
        CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionName);
        ArgumentNullException.ThrowIfNull(connection);

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
            throw new DataverseConnectionException(
                $"XRM Connection Failed: {service.LastError}",
                service.LastException);
        }

        await ((IOrganizationServiceAsync2)service).ExecuteAsync(new WhoAmIRequest(), cancellationToken);
    }
}
