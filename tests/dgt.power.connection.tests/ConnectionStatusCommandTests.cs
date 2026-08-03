// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.connection.Base;
using dgt.power.connection.Commands;
using dgt.power.connection.tests.Base;
using Spectre.Console.Cli;

namespace dgt.power.connection.tests;

[NotInParallel("Serial_Connection_Tests")]
public class ConnectionStatusCommandTests : ConnectionTestsBase<ConnectionStatusCommand, ConnectionSettings>
{
    [Test]
    public async Task ShouldReturnSuccess_WhenAuthenticationIsValid()
    {
        ICommand<ConnectionSettings> command = new ConnectionStatusCommand(new FakeXrmConnection { CheckAuthResult = true }, TestConsole);

        var result = await command.ExecuteAsync(CreateContext(), new ConnectionSettings(), CancellationToken.None);

        await Assert.That(result).IsEqualTo(0);
        await Assert.That(TestConsole.Output).Contains("AUTH_OK");
    }

    [Test]
    public async Task ShouldReturnAuthRequired_WhenAuthenticationIsInvalid()
    {
        ICommand<ConnectionSettings> command = new ConnectionStatusCommand(new FakeXrmConnection { CheckAuthResult = false }, TestConsole);

        var result = await command.ExecuteAsync(CreateContext(), new ConnectionSettings(), CancellationToken.None);

        await Assert.That(result).IsEqualTo(2);
        await Assert.That(TestConsole.Output).Contains("AUTH_REQUIRED");
        await Assert.That(TestConsole.Output).Contains("dgtp connection refresh");
    }

    private static CommandContext CreateContext() =>
        new(Enumerable.Empty<string>(), new EmptyRemainingArguments(), "status", null);
}
