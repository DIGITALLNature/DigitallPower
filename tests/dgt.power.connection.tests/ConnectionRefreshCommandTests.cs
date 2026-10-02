// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common.Connections;
using dgt.power.connection.Base;
using dgt.power.connection.Commands;
using dgt.power.connection.tests.Base;
using Spectre.Console.Cli;

namespace dgt.power.connection.tests;

public class ConnectionRefreshCommandTests : ConnectionTestsBase<ConnectionRefreshCommand, ConnectionSettings>
{
    [Test]
    public async Task SkipsRefreshForServicePrincipalConnection()
    {
        ConnectionStore.Upsert("App", new ClientSecretConnection
        {
            Url = "https://contoso.crm.dynamics.com",
            TenantId = "tenant",
            ClientId = "client"
        });
        var fakeConnection = new FakeDataverseConnection();
        ICommand<ConnectionSettings> command = new ConnectionRefreshCommand(
            ConnectionStore,
            new ConnectionInvocationContext(),
            fakeConnection,
            TestConsole);

        var result = await command.ExecuteAsync(CreateContext(), new ConnectionSettings(), CancellationToken.None);

        await Assert.That(result).IsEqualTo(0);
        await Assert.That(fakeConnection.RefreshCalls).IsEqualTo(0);
    }

    [Test]
    public async Task RefreshesUserConnection()
    {
        ConnectionStore.Upsert("User", new InteractiveConnection
        {
            Url = "https://contoso.crm.dynamics.com",
            TenantId = "tenant"
        });
        var fakeConnection = new FakeDataverseConnection();
        ICommand<ConnectionSettings> command = new ConnectionRefreshCommand(
            ConnectionStore,
            new ConnectionInvocationContext(),
            fakeConnection,
            TestConsole);

        var result = await command.ExecuteAsync(CreateContext(), new ConnectionSettings(), CancellationToken.None);

        await Assert.That(result).IsEqualTo(0);
        await Assert.That(fakeConnection.RefreshCalls).IsEqualTo(1);
    }

    private static CommandContext CreateContext() =>
        new(Enumerable.Empty<string>(), new EmptyRemainingArguments(), "refresh", null);
}
