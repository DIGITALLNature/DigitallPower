// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using Spectre.Console.Cli;
using Spectre.Console.Testing;

namespace dgt.power.tests;

public class ConsoleInjectionTests
{
    private sealed class StubCommand : Command<StubCommand.StubSettings>
    {
        public sealed class StubSettings : CommandSettings;

        public override int Execute(CommandContext context, StubSettings settings, CancellationToken cancellationToken) => 0;
    }

    [Test]
    public async Task TestServiceCollection_ShouldRegisterIAnsiConsole()
    {
        var services = new TestServiceCollection();
        var provider = services.BuildServiceProvider();
        var console = provider.GetService<IAnsiConsole>();
        await Assert.That(console).IsNotNull();
    }

    [Test]
    public async Task CommandTestContextBuilder_WithAnsiConsole_ShouldReturnBuilder()
    {
        using var testConsole = new TestConsole();
        var builder = new CommandTestContextBuilder<StubCommand, StubCommand.StubSettings>()
            .WithAnsiConsole(testConsole);
        await Assert.That(builder).IsNotNull();
    }
}
