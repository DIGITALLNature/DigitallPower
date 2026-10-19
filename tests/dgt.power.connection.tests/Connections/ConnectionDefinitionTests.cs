// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text.Json;
using dgt.power.common.Connections;

namespace dgt.power.connection.tests.Connections;

public class ConnectionDefinitionTests
{
    [Test]
    public async Task TypeDisplayNames_AreExplicitAndJsonDiscriminatorsRemainStable()
    {
        ConnectionDefinition[] definitions =
        [
            new InteractiveConnection { Url = ConnectionTestUrls.Dataverse },
            new DeviceCodeConnection { Url = ConnectionTestUrls.Dataverse },
            new ClientSecretConnection { Url = ConnectionTestUrls.Dataverse, TenantId = "tenant", ClientId = "client" },
            new ClientCertificateConnection { Url = ConnectionTestUrls.Dataverse, TenantId = "tenant", ClientId = "client" },
            new AzureDevOpsFederatedConnection
            {
                Url = ConnectionTestUrls.Dataverse,
                TenantId = "tenant",
                ClientId = "client",
                ServiceConnectionId = "service-connection"
            }
        ];
        string[] typeDisplayNames =
        [
            "Interactive",
            "Device Code",
            "Client Secret",
            "Client Certificate",
            "Azure DevOps Federated"
        ];
        string[] discriminators =
        [
            "interactive",
            "deviceCode",
            "clientSecret",
            "clientCertificate",
            "azureDevOpsFederated"
        ];

        for (var index = 0; index < definitions.Length; index++)
        {
            using var document = JsonDocument.Parse(JsonSerializer.Serialize(definitions[index]));
            await Assert.That(definitions[index].TypeDisplayName).IsEqualTo(typeDisplayNames[index]);
            await Assert.That(document.RootElement.GetProperty("type").GetString()).IsEqualTo(discriminators[index]);
            await Assert.That(document.RootElement.TryGetProperty("TypeDisplayName", out _)).IsFalse();
        }
    }
}
