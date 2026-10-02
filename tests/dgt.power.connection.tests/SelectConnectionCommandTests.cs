// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common.Connections;
using dgt.power.connection.Commands;
using dgt.power.connection.tests.Base;
using Spectre.Console.Cli;

namespace dgt.power.connection.tests;

public class SelectConnectionCommandTests : ConnectionTestsBase<SelectConnectionCommand, NamedConnectionSettings>
{
    [Test]
    public async Task SelectsExistingConnectionCaseInsensitively()
    {
        ConnectionStore.Upsert("Dev", new InteractiveConnection
        {
            Url = "https://contoso.crm.dynamics.com",
            TenantId = "contoso.onmicrosoft.com"
        });
        ICommand<NamedConnectionSettings> command = new SelectConnectionCommand(ConnectionStore, TestConsole);

        var result = await command.ExecuteAsync(
            CreateContext(),
            new NamedConnectionSettings { Name = "dev" },
            CancellationToken.None);

        await Assert.That(result).IsEqualTo(0);
        await Assert.That(ConnectionStore.Current).IsEqualTo("Dev");
    }

    [Test]
    public async Task MissingConnectionDoesNotChangeCurrent()
    {
        ConnectionStore.Upsert("Dev", new InteractiveConnection
        {
            Url = "https://contoso.crm.dynamics.com",
            TenantId = "contoso.onmicrosoft.com"
        });
        ICommand<NamedConnectionSettings> command = new SelectConnectionCommand(ConnectionStore, TestConsole);

        var result = await command.ExecuteAsync(
            CreateContext(),
            new NamedConnectionSettings { Name = "missing" },
            CancellationToken.None);

        await Assert.That(result).IsEqualTo(-1);
        await Assert.That(ConnectionStore.Current).IsEqualTo("Dev");
    }

    private static CommandContext CreateContext() =>
        new(Enumerable.Empty<string>(), new EmptyRemainingArguments(), "select", null);
}
