// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using dgt.power.common.Connections;
using dgt.power.connection.Base;
using dgt.power.connection.Commands;
using dgt.power.connection.tests.Base;
using Spectre.Console.Cli;

namespace dgt.power.connection.tests;

public class ListConnectionCommandTests : ConnectionTestsBase<ListConnectionCommand, ConnectionSettings>
{
    [Test]
    public async Task ListsTypedConnectionsWithTypeDisplayNamesAndMarksCurrent()
    {
        ConnectionStore.Upsert("Interactive", new InteractiveConnection { Url = ConnectionTestUrls.Dataverse });
        ConnectionStore.Upsert("Device", new DeviceCodeConnection { Url = ConnectionTestUrls.Dataverse }, makeCurrent: false);
        ConnectionStore.Upsert("Secret", new ClientSecretConnection
        {
            Url = ConnectionTestUrls.Dataverse,
            TenantId = "tenant",
            ClientId = "client"
        }, makeCurrent: false);
        ConnectionStore.Upsert("Certificate", new ClientCertificateConnection
        {
            Url = ConnectionTestUrls.Dataverse,
            TenantId = "tenant",
            ClientId = "client"
        }, makeCurrent: false);
        ConnectionStore.Upsert("Federated", new AzureDevOpsFederatedConnection
        {
            Url = ConnectionTestUrls.Dataverse,
            TenantId = "tenant",
            ClientId = "client",
            ServiceConnectionId = "service-connection"
        }, makeCurrent: false);
        ICommand<ConnectionSettings> command = new ListConnectionCommand(
            ConnectionStore,
            new ConnectionInvocationContext(),
            TestConsole);

        var result = await command.ExecuteAsync(CreateContext(), new ConnectionSettings(), CancellationToken.None);

        await Assert.That(result).IsEqualTo(0);
        await Assert.That(TestConsole.Output).Contains("Interactive");
        await Assert.That(TestConsole.Output).Contains("Device Code");
        await Assert.That(TestConsole.Output).Contains("Client Secret");
        await Assert.That(TestConsole.Output).Contains("Client Certificate");
        await Assert.That(TestConsole.Output).Contains("Azure DevOps Federated");
        await Assert.That(TestConsole.Output).Contains("*");
    }

    private static CommandContext CreateContext() =>
        new(Enumerable.Empty<string>(), new EmptyRemainingArguments(), "list", null);
}
