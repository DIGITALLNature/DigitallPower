// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using System.Globalization;
using dgt.power.common;
using dgt.power.common.Telemetry;
using dgt.power.telemetry.Base;
using Spectre.Console;

namespace dgt.power.telemetry.Commands;

public sealed class TelemetryStatusCommand(
    ICommandSpanFactory spans,
    TelemetryStateProvider telemetryState,
    ITelemetryNoticeTracker noticeTracker,
    IAnsiConsole console)
    : PowerCommand<TelemetrySettings>(spans, console)
{
    protected override Task<bool> RunAsync(TelemetrySettings settings, CancellationToken cancellationToken)
    {
        // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
        if (!noticeTracker.IsNoticeShown)
        {
            TelemetryNotice.Show(Console);
        }

        if (telemetryState.IsEnabled)
        {
            Console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[green]●[/] Telemetry is enabled.");
            Console.MarkupLine("Run [bold]dgtp telemetry disable[/] to disable telemetry.");
        }
        else
        {
            Console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[red]●[/] Telemetry is disabled.");
            Console.MarkupLine(TelemetryStateProvider.HasOptOutEnvVar
                ? "DGTP_TELEMETRY_OPTOUT / DO_NOT_TRACK is set."
                : "Run [bold]dgtp telemetry enable[/] to enable telemetry.");
        }

        return Task.FromResult(true);
    }
}
