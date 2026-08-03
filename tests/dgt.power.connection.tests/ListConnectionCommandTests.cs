// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common.Logic;
using dgt.power.connection.Base;
using dgt.power.connection.Commands;
using dgt.power.connection.tests.Base;
using Spectre.Console.Cli;

namespace dgt.power.connection.tests;

[NotInParallel("Serial_Connection_Tests")]
public class ListConnectionCommandTests : ConnectionTestsBase<ListConnectionCommand, ConnectionSettings>
{
    [Test]
    public async Task ShouldListConnectionsAndMarkCurrent()
    {
        var profileManager = ProfileManager;
        profileManager.LoadIdentities().Upsert("FIRST", new Identity { ConnectionString = "connection-1" });
        profileManager.Save();
        profileManager.LoadIdentities().Upsert("SECOND", new Identity { ConnectionString = "connection-2" });
        profileManager.Save();

        ICommand<ConnectionSettings> command = new ListConnectionCommand(profileManager, TestConsole);
        var result = await command.ExecuteAsync(CreateContext(), new ConnectionSettings(), CancellationToken.None);

        await Assert.That(result).IsEqualTo(0);
        await Assert.That(TestConsole.Output).Contains("FIRST");
        await Assert.That(TestConsole.Output).Contains("SECOND");
    }

    private static CommandContext CreateContext() =>
        new(Enumerable.Empty<string>(), new EmptyRemainingArguments(), "list", null);
}
