// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using dgt.power.common;
using dgt.power.common.Telemetry;
using dgt.power.telemetry.Base;
using Spectre.Console;

namespace dgt.power.telemetry.Commands;

public sealed class TelemetryStatusCommand(
    ICommandSpanFactory spans,
    TelemetryStateProvider telemetryState,
    IAnsiConsole console)
    : PowerCommand<TelemetrySettings>(spans, console)
{
    protected override Task<bool> RunAsync(TelemetrySettings settings, CancellationToken cancellationToken)
    {
        TelemetryNotice.Show(Console);

        if (telemetryState.IsEnabled)
        {
            Console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[green]●[/] Telemetry is enabled.");
            Console.MarkupLine("Run [bold]dgtp telemetry disable[/] to disable telemetry.");
        }
        else
        {
            Console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[red]●[/] Telemetry is disabled.");
            if (telemetryState.HasOptOutEnvVar)
            {
                Console.MarkupLine("DGTP_TELEMETRY_OPTOUT / DO_NOT_TRACK is set.");
            }
            else
            {
                Console.MarkupLine("Run [bold]dgtp telemetry enable[/] to enable telemetry.");
            }
        }

        return Task.FromResult(true);
    }
}
