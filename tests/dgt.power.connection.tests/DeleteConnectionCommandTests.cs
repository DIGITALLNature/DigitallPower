// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.connection.Base;
using dgt.power.common.Logic;
using dgt.power.connection.Commands;
using dgt.power.connection.tests.Base;
using Spectre.Console.Cli;

namespace dgt.power.connection.tests;

[NotInParallel("Serial_Connection_Tests")]
public class DeleteConnectionCommandTests : ConnectionTestsBase<DeleteConnectionCommand, DeleteConnectionSettings>
{
    [Test]
    public async Task ShouldDeleteSelectedConnection()
    {
        var profileManager = ProfileManager;
        profileManager.LoadIdentities().Upsert("KEEP", new Identity { ConnectionString = "connection-1" });
        profileManager.Save();
        profileManager.LoadIdentities().Upsert("REMOVE", new Identity { ConnectionString = "connection-2" });
        profileManager.Save();
        profileManager.LoadIdentities().SetCurrent("KEEP");
        profileManager.Save();

        ICommand<DeleteConnectionSettings> command = new DeleteConnectionCommand(profileManager, TestConsole);
        var result = await command.ExecuteAsync(CreateContext(), new DeleteConnectionSettings { Name = "REMOVE" }, CancellationToken.None);

        await Assert.That(result).IsEqualTo(0);
        await Assert.That(GetIdentities().Contains("REMOVE")).IsFalse();
        await Assert.That(GetIdentities().Current).IsEqualTo("KEEP");
        await Assert.That(TestConsole.Output).Contains("removed");
    }

    [Test]
    public async Task ShouldDeleteAllConnections_WhenRequested()
    {
        var profileManager = ProfileManager;
        profileManager.LoadIdentities().Upsert("FIRST", new Identity { ConnectionString = "connection-1" });
        profileManager.Save();
        profileManager.LoadIdentities().Upsert("SECOND", new Identity { ConnectionString = "connection-2" });
        profileManager.Save();

        ICommand<DeleteConnectionSettings> command = new DeleteConnectionCommand(profileManager, TestConsole);
        var result = await command.ExecuteAsync(CreateContext(), new DeleteConnectionSettings { All = true, Yes = true }, CancellationToken.None);

        await Assert.That(result).IsEqualTo(0);
        await Assert.That(GetIdentities().Infos).IsEmpty();
        await Assert.That(TestConsole.Output).Contains("deleted");
    }

    private static CommandContext CreateContext() =>
        new(Enumerable.Empty<string>(), new EmptyRemainingArguments(), "delete", null);
}
