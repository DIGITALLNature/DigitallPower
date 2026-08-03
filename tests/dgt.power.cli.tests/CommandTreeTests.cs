// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using Spectre.Console.Cli;
using Spectre.Console.Cli.Testing;

namespace dgt.power.cli.tests;

/// <summary>
/// Tier 1: structural wiring tests for the real dgtp command tree (<see cref="CommandTree"/>).
/// These tests exercise Spectre.Console.Cli's own model building (command/branch/alias
/// registration and example validation) against the exact same registration code path used
/// by <c>Program.cs</c> and the dotnet-suggest handler - without bootstrapping the rest of the
/// application (DI, telemetry, NuGet, Dataverse connection).
/// <para>
/// Building the <c>ICommandModel</c> (triggered by any <c>Run</c>/<c>--help</c> invocation)
/// walks the *entire* configured tree, so a single invocation is enough to catch:
/// duplicate command names/aliases, broken branch registration, and invalid
/// <c>WithExample(...)</c> calls.
/// </para>
/// </summary>
public class CommandTreeTests
{
    [Test]
    public async Task Register_WithValidateExamples_BuildsModelWithoutThrowing()
    {
        var tester = new CommandAppTester();
        tester.Configure(config =>
        {
            CommandTree.Register(config);
            config.ValidateExamples();
        });

        var result = tester.Run("--help");

        await Assert.That(result.ExitCode).IsEqualTo(0);
    }

    // Belt-and-braces: verify every top-level branch/command is independently reachable.
    // (Model building already covers the whole tree above; this additionally guards against
    // routing issues that only surface when actually navigating into a specific path.)
    [Test]
    [Arguments("profile")]
    [Arguments("export")]
    [Arguments("maintenance")]
    [Arguments("analyze")]
    [Arguments("import")]
    [Arguments("codegeneration")]
    [Arguments("cg")] // alias for codegeneration
    [Arguments("push")]
    [Arguments("complete")]
    public async Task TopLevelPath_HelpInvocation_Succeeds(string path)
    {
        var tester = new CommandAppTester();
        tester.Configure(CommandTree.Register);

        var result = tester.Run(path, "--help");

        await Assert.That(result.ExitCode).IsEqualTo(0);
    }
}
