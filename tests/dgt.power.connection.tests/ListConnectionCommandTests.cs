// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common.Connections;
using dgt.power.connection.Base;
using dgt.power.connection.Commands;
using dgt.power.connection.tests.Base;
using Spectre.Console.Cli;

namespace dgt.power.connection.tests;

public class ListConnectionCommandTests : ConnectionTestsBase<ListConnectionCommand, ConnectionSettings>
{
    [Test]
    public async Task ListsTypedConnectionsAndMarksCurrent()
    {
        var definition = new InteractiveConnection
        {
            Url = "https://contoso.crm.dynamics.com",
            TenantId = "contoso.onmicrosoft.com"
        };
        ConnectionStore.Upsert("Dev", definition);
        ConnectionStore.Upsert("Test", definition, makeCurrent: false);
        ICommand<ConnectionSettings> command = new ListConnectionCommand(
            ConnectionStore,
            new ConnectionInvocationContext(),
            TestConsole);

        var result = await command.ExecuteAsync(CreateContext(), new ConnectionSettings(), CancellationToken.None);

        await Assert.That(result).IsEqualTo(0);
        await Assert.That(TestConsole.Output).Contains("Dev");
        await Assert.That(TestConsole.Output).Contains("Test");
        await Assert.That(TestConsole.Output).Contains("interactive");
    }

    private static CommandContext CreateContext() =>
        new(Enumerable.Empty<string>(), new EmptyRemainingArguments(), "list", null);
}
