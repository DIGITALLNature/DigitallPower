// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using Spectre.Console.Cli;
using Spectre.Console.Cli.Testing;

namespace dgt.power.cli.tests;

public class CommandContextNameTests
{
    [Test]
    public async Task Name_TopLevelCommand_IsCommandName()
    {
        var recorded = Run("probe");

        await Assert.That(recorded).IsEqualTo("probe");
    }

    [Test]
    public async Task Name_NestedCommand_IsLeafNameOnly()
    {
        var recorded = Run("outer", "inner", "probe");

        await Assert.That(recorded).IsEqualTo("probe");
    }

    private static string Run(params string[] args)
    {
        var tester = new CommandAppTester();
        tester.Configure(config =>
        {
            config.AddCommand<ProbeCommand>("probe");
            config.AddBranch("outer", outer => outer.AddBranch("inner", inner => inner.AddCommand<ProbeCommand>("probe")));
        });
        tester.Run(args);
        return ProbeCommand.LastName ?? string.Empty;
    }

    private sealed class ProbeCommand : Command<EmptyProbeSettings>
    {
        internal static string? LastName { get; private set; }

        public override int Execute(CommandContext context, EmptyProbeSettings settings, CancellationToken cancellationToken)
        {
            LastName = context.Name;
            return 0;
        }
    }

    private sealed class EmptyProbeSettings : CommandSettings;
}
