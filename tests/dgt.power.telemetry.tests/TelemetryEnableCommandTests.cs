// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using dgt.power.telemetry.Base;
using dgt.power.telemetry.Commands;
using Spectre.Console.Cli;

namespace dgt.power.telemetry.tests;

[NotInParallel("TelemetryEnvironmentVariables")]
public class TelemetryEnableCommandTests : TelemetryTestBase
{
    [Test]
    public async Task Enable_PersistsEnabledAndShowsConfirmation()
    {
        ClearTelemetryEnvVars();
        StateStore.SetTelemetryEnabled(false);
        var command = new TelemetryEnableCommand(SpanFactory, TelemetryState, TestConsole);

        var result = await command.ExecuteAsync(CreateContext(), new TelemetrySettings(), CancellationToken.None);

        await Assert.That(result).IsEqualTo(0);
        await Assert.That(TestConsole.Output).Contains("Telemetry is now enabled.");
        await Assert.That(StateStore.TelemetryEnabled).IsTrue();
    }

    [Test]
    [Arguments("DGTP_TELEMETRY_OPTOUT")]
    [Arguments("DO_NOT_TRACK")]
    public async Task Enable_WithEnvOptOut_ShowsWarningAndEnvVarLine(string envVar)
    {
        ClearTelemetryEnvVars();
        StateStore.SetTelemetryEnabled(false);
        SetEnvVar(envVar, "1");
        var command = new TelemetryEnableCommand(SpanFactory, TelemetryState, TestConsole);

        var result = await command.ExecuteAsync(CreateContext(), new TelemetrySettings(), CancellationToken.None);

        await Assert.That(result).IsEqualTo(0);
        await Assert.That(TestConsole.Output).Contains("Telemetry is now enabled.");
        await Assert.That(TestConsole.Output).Contains("DGTP_TELEMETRY_OPTOUT / DO_NOT_TRACK is set.");
        await Assert.That(StateStore.TelemetryEnabled).IsTrue();
    }

    private static CommandContext CreateContext() =>
        new(Enumerable.Empty<string>(), new EmptyRemainingArguments(), "enable", null);
}
