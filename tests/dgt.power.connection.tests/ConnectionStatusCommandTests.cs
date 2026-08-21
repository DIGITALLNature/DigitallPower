// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.connection.Base;
using dgt.power.connection.Commands;
using dgt.power.connection.tests.Base;
using dgt.power.common.Logic;
using Spectre.Console.Cli;

namespace dgt.power.connection.tests;

[NotInParallel("Serial_Connection_Tests")]
public class ConnectionStatusCommandTests : ConnectionTestsBase<ConnectionStatusCommand, ConnectionSettings>
{
    [Test]
    public async Task ShouldReturnSuccess_WhenAuthenticationIsValid()
    {
        ICommand<ConnectionSettings> command = new ConnectionStatusCommand(new FakeXrmConnection { CheckAuthResult = true }, ProfileManager, TestConsole);

        var result = await command.ExecuteAsync(CreateContext(), new ConnectionSettings(), CancellationToken.None);

        await Assert.That(result).IsEqualTo(0);
        await Assert.That(TestConsole.Output).Contains("AUTH_OK");
    }

    [Test]
    public async Task ShouldReturnAuthRequired_WhenAuthenticationIsInvalid()
    {
        ICommand<ConnectionSettings> command = new ConnectionStatusCommand(new FakeXrmConnection { CheckAuthResult = false }, ProfileManager, TestConsole);

        var result = await command.ExecuteAsync(CreateContext(), new ConnectionSettings(), CancellationToken.None);

        await Assert.That(result).IsEqualTo(2);
        await Assert.That(TestConsole.Output).Contains("AUTH_REQUIRED");
        await Assert.That(TestConsole.Output).Contains("dgtp connection refresh");
    }

    [Test]
    public async Task ShouldReturnAuthRequired_WithoutRefreshTip_WhenFederatedAuthenticationIsInvalid()
    {
        var manager = ProfileManager;
#pragma warning disable S1075
        manager.LoadIdentities().Upsert("FEDERATED", new AzureDevOpsFederatedIdentity
        {
            ConnectionString = "https://contoso.crm.dynamics.com",
            TenantId = "11111111-1111-1111-1111-111111111111",
            ClientId = "22222222-2222-2222-2222-222222222222",
            ServiceConnectionId = "33333333-3333-3333-3333-333333333333"
        });
#pragma warning restore S1075
        manager.Save();

        ICommand<ConnectionSettings> command = new ConnectionStatusCommand(new FakeXrmConnection { CheckAuthResult = false }, manager, TestConsole);

        var result = await command.ExecuteAsync(CreateContext(), new ConnectionSettings(), CancellationToken.None);

        await Assert.That(result).IsEqualTo(2);
        await Assert.That(TestConsole.Output).Contains("AUTH_REQUIRED");
        await Assert.That(TestConsole.Output).Contains("SYSTEM_ACCESSTOKEN");
        await Assert.That(TestConsole.Output).DoesNotContain("dgtp connection refresh");
    }

    private static CommandContext CreateContext() =>
        new(Enumerable.Empty<string>(), new EmptyRemainingArguments(), "status", null);
}
