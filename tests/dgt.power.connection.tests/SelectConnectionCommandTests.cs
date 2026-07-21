// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common.Logic;
using dgt.power.connection.Commands;
using dgt.power.connection.tests.Base;
using Spectre.Console.Cli;

namespace dgt.power.connection.tests;

[NotInParallel("Serial_Connection_Tests")]
public class SelectConnectionCommandTests : ConnectionTestsBase<SelectConnectionCommand, NamedConnectionSettings>
{
    [Test]
    public async Task ShouldSelectExistingConnection()
    {
        var profileManager = ProfileManager;
        profileManager.LoadIdentities().Upsert("FIRST", new Identity { ConnectionString = "connection-1" });
        profileManager.Save();
        profileManager.LoadIdentities().Upsert("SECOND", new Identity { ConnectionString = "connection-2" });
        profileManager.Save();

        ICommand<NamedConnectionSettings> command = new SelectConnectionCommand(profileManager, TestConsole);
        var result = await command.ExecuteAsync(CreateContext(), new NamedConnectionSettings { Name = "FIRST" }, CancellationToken.None);

        await Assert.That(result).IsEqualTo(0);
        await Assert.That(GetIdentities().Current).IsEqualTo("FIRST");
        await Assert.That(TestConsole.Output).Contains("set.");
    }

    [Test]
    public async Task ShouldReturnError_WhenConnectionDoesNotExist()
    {
        var profileManager = ProfileManager;
        profileManager.LoadIdentities().Upsert("EXISTING", new Identity { ConnectionString = "connection" });
        profileManager.Save();

        ICommand<NamedConnectionSettings> command = new SelectConnectionCommand(profileManager, TestConsole);
        var result = await command.ExecuteAsync(CreateContext(), new NamedConnectionSettings { Name = "MISSING" }, CancellationToken.None);

        await Assert.That(result).IsEqualTo(-1);
        await Assert.That(GetIdentities().Current).IsEqualTo("EXISTING");
        await Assert.That(TestConsole.Output).Contains("not found");
    }

    private static CommandContext CreateContext() =>
        new(Enumerable.Empty<string>(), new EmptyRemainingArguments(), "select", null);
}
