// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common.Storage;
using dgt.power.telemetry.Base;
using dgt.power.telemetry.Commands;
using Spectre.Console.Cli;

namespace dgt.power.telemetry.tests;

public class TelemetryDisableCommandTests : TelemetryTestBase
{
    [Test]
    public async Task Disable_PersistsDisabledAndShowsConfirmation()
    {
        ClearTelemetryEnvVars();
        StateStore.SetTelemetryEnabled(true);
        var command = new TelemetryDisableCommand(SpanFactory, TelemetryState, TestConsole);

        var result = await command.ExecuteAsync(CreateContext(), new TelemetrySettings(), CancellationToken.None);

        await Assert.That(result).IsEqualTo(0);
        await Assert.That(TestConsole.Output).Contains("Telemetry is now disabled.");
        await Assert.That(StateStore.TelemetryEnabled).IsFalse();
    }

    [Test]
    public async Task Disable_PreservesInstallId()
    {
        ClearTelemetryEnvVars();
        var installId = StateStore.GetOrCreateTelemetryInstallId();
        var command = new TelemetryDisableCommand(SpanFactory, TelemetryState, TestConsole);

        await command.ExecuteAsync(CreateContext(), new TelemetrySettings(), CancellationToken.None);

        var reloaded = new StateStore(Home);
        await Assert.That(reloaded.GetOrCreateTelemetryInstallId(out var created)).IsEqualTo(installId);
        await Assert.That(created).IsFalse();
    }

    private static CommandContext CreateContext() =>
        new(Enumerable.Empty<string>(), new EmptyRemainingArguments(), "disable", null);
}
