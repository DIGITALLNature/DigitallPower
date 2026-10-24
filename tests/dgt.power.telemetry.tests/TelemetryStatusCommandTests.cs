// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.telemetry.Base;
using dgt.power.telemetry.Commands;
using Spectre.Console.Cli;

namespace dgt.power.telemetry.tests;

[NotInParallel("TelemetryEnvironmentVariables")]
public class TelemetryStatusCommandTests : TelemetryTestBase
{
    [Test]
    public async Task Status_Enabled_ShowsGreenCircleAndDisableHint()
    {
        ClearTelemetryEnvVars();
        StateStore.SetTelemetryEnabled(true);
        var command = new TelemetryStatusCommand(SpanFactory, StateStore, TestConsole);

        var result = await command.ExecuteAsync(CreateContext(), new TelemetrySettings(), CancellationToken.None);

        await Assert.That(result).IsEqualTo(0);
        await Assert.That(TestConsole.Output).Contains("Telemetry is enabled.");
        await Assert.That(TestConsole.Output).Contains("To disable: dgtp telemetry disable");
        await Assert.That(TestConsole.Output).Contains("Telemetry Notice");
    }

    [Test]
    public async Task Status_Disabled_ShowsRedCircleAndEnableHint()
    {
        ClearTelemetryEnvVars();
        StateStore.SetTelemetryEnabled(false);
        var command = new TelemetryStatusCommand(SpanFactory, StateStore, TestConsole);

        var result = await command.ExecuteAsync(CreateContext(), new TelemetrySettings(), CancellationToken.None);

        await Assert.That(result).IsEqualTo(0);
        await Assert.That(TestConsole.Output).Contains("Telemetry is disabled.");
        await Assert.That(TestConsole.Output).Contains("To enable: dgtp telemetry enable");
        await Assert.That(TestConsole.Output).Contains("Telemetry Notice");
    }

    [Test]
    [Arguments("DGTP_TELEMETRY_OPTOUT")]
    [Arguments("DO_NOT_TRACK")]
    public async Task Status_EnvOptOut_ShowsRedCircleAndEnvVarLine(string envVar)
    {
        ClearTelemetryEnvVars();
        StateStore.SetTelemetryEnabled(true);
        SetEnvVar(envVar, "1");
        var command = new TelemetryStatusCommand(SpanFactory, StateStore, TestConsole);

        var result = await command.ExecuteAsync(CreateContext(), new TelemetrySettings(), CancellationToken.None);

        await Assert.That(result).IsEqualTo(0);
        await Assert.That(TestConsole.Output).Contains("Telemetry is disabled.");
        await Assert.That(TestConsole.Output).Contains("DGTP_TELEMETRY_OPTOUT / DO_NOT_TRACK is set.");
        await Assert.That(TestConsole.Output).Contains("Telemetry Notice");
    }

    [Test]
    public async Task Status_NoticeDisplayedBeforeStatusLine()
    {
        ClearTelemetryEnvVars();
        StateStore.SetTelemetryEnabled(true);
        var command = new TelemetryStatusCommand(SpanFactory, StateStore, TestConsole);

        await command.ExecuteAsync(CreateContext(), new TelemetrySettings(), CancellationToken.None);

        var noticeIndex = TestConsole.Output.IndexOf("Telemetry Notice", StringComparison.Ordinal);
        var statusIndex = TestConsole.Output.IndexOf("Telemetry is enabled.", StringComparison.Ordinal);
        await Assert.That(noticeIndex).IsGreaterThanOrEqualTo(0);
        await Assert.That(statusIndex).IsGreaterThan(noticeIndex);
    }

    private static CommandContext CreateContext() =>
        new(Enumerable.Empty<string>(), new EmptyRemainingArguments(), "status", null);
}
