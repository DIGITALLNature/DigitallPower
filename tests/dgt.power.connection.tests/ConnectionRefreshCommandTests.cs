// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common.Logic;
using dgt.power.connection.Base;
using dgt.power.connection.Commands;
using dgt.power.connection.tests.Base;
using Spectre.Console.Cli;

namespace dgt.power.connection.tests;

[NotInParallel("Serial_Connection_Tests")]
public class ConnectionRefreshCommandTests : ConnectionTestsBase<ConnectionRefreshCommand, ConnectionSettings>
{
    [Test]
    public async Task ShouldSkipRefresh_WhenConnectionUsesConnectionString()
    {
        var profileManager = ProfileManager;
        ICommand<ConnectionSettings> command = new ConnectionRefreshCommand(profileManager, new FakeXrmConnection(), TestConsole);

        var result = await command.ExecuteAsync(CreateContext(), new ConnectionSettings(), CancellationToken.None);

        await Assert.That(result).IsEqualTo(0);
        await Assert.That(TestConsole.Output).Contains("no token to refresh");
    }

    [Test]
    public async Task ShouldRefreshAuth_WhenCurrentIdentityUsesMsal()
    {
        var profileManager = ProfileManager;
        profileManager.LoadIdentities().Upsert("MSAL", new TokenIdentity
        {
            ConnectionString = "https://contoso.crm.dynamics.com",
            Token = string.Empty
        });
        profileManager.Save();

        var fakeConnection = new FakeXrmConnection();
        ICommand<ConnectionSettings> command = new ConnectionRefreshCommand(profileManager, fakeConnection, TestConsole);

        var result = await command.ExecuteAsync(CreateContext(), new ConnectionSettings(), CancellationToken.None);

        await Assert.That(result).IsEqualTo(0);
        await Assert.That(fakeConnection.RefreshCalls).IsEqualTo(1);
        await Assert.That(TestConsole.Output).Contains("AUTH_OK");
    }

    [Test]
    public async Task ShouldReturnError_WhenRefreshThrows()
    {
        var profileManager = ProfileManager;
        profileManager.LoadIdentities().Upsert("MSAL", new TokenIdentity
        {
            ConnectionString = "https://contoso.crm.dynamics.com",
            Token = string.Empty
        });
        profileManager.Save();

        var fakeConnection = new FakeXrmConnection { RefreshThrows = true };
        ICommand<ConnectionSettings> command = new ConnectionRefreshCommand(profileManager, fakeConnection, TestConsole);

        var result = await command.ExecuteAsync(CreateContext(), new ConnectionSettings(), CancellationToken.None);

        await Assert.That(result).IsEqualTo(1);
        await Assert.That(fakeConnection.RefreshCalls).IsEqualTo(1);
        await Assert.That(TestConsole.Output).Contains("Error:");
    }

    private static CommandContext CreateContext() =>
        new(Enumerable.Empty<string>(), new EmptyRemainingArguments(), "refresh", null);
}
