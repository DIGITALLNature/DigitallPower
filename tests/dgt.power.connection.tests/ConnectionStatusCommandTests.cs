// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common.Connections;
using dgt.power.connection.Base;
using dgt.power.connection.Commands;
using dgt.power.connection.tests.Base;
using Spectre.Console.Cli;

namespace dgt.power.connection.tests;

public class ConnectionStatusCommandTests : ConnectionTestsBase<ConnectionStatusCommand, ConnectionSettings>
{
    [Test]
    public async Task ReturnsSuccessWhenAuthenticationIsValid()
    {
        ICommand<ConnectionSettings> command = new ConnectionStatusCommand(
            new FakeXrmConnection { CheckAuthResult = true },
            ConnectionStore,
            ConnectionInvocationOptions.FromArguments([]),
            TestConsole);

        var result = await command.ExecuteAsync(CreateContext(), new ConnectionSettings(), CancellationToken.None);

        await Assert.That(result).IsEqualTo(0);
        await Assert.That(TestConsole.Output).Contains("AUTH_OK");
    }

    [Test]
    public async Task ReturnsAuthRequiredWhenAuthenticationIsInvalid()
    {
        ICommand<ConnectionSettings> command = new ConnectionStatusCommand(
            new FakeXrmConnection { CheckAuthResult = false },
            ConnectionStore,
            ConnectionInvocationOptions.FromArguments([]),
            TestConsole);

        var result = await command.ExecuteAsync(CreateContext(), new ConnectionSettings(), CancellationToken.None);

        await Assert.That(result).IsEqualTo(2);
        await Assert.That(TestConsole.Output).Contains("AUTH_REQUIRED");
        await Assert.That(TestConsole.Output).Contains("dgtp connection refresh");
    }

    [Test]
    public async Task ReturnsFederatedHintForFederatedConnection()
    {
        ConnectionStore.Upsert("Pipeline", new AzureDevOpsFederatedConnection
        {
            Url = "https://contoso.crm.dynamics.com",
            TenantId = "tenant",
            ClientId = "client",
            ServiceConnectionId = "service-connection"
        });
        ICommand<ConnectionSettings> command = new ConnectionStatusCommand(
            new FakeXrmConnection { CheckAuthResult = false },
            ConnectionStore,
            ConnectionInvocationOptions.FromArguments([]),
            TestConsole);

        var result = await command.ExecuteAsync(CreateContext(), new ConnectionSettings(), CancellationToken.None);

        await Assert.That(result).IsEqualTo(2);
        var normalizedOutput = string.Join(
            " ",
            TestConsole.Output.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries));
        await Assert.That(normalizedOutput).Contains("workload identity federation");
    }

    private static CommandContext CreateContext() =>
        new(Enumerable.Empty<string>(), new EmptyRemainingArguments(), "status", null);
}
