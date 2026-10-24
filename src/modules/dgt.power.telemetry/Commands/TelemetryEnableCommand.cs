// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using dgt.power.common;
using dgt.power.common.Telemetry;
using dgt.power.telemetry.Base;
using Spectre.Console;

namespace dgt.power.telemetry.Commands;

public sealed class TelemetryEnableCommand(
    ICommandSpanFactory spans,
    TelemetryStateProvider telemetryState,
    IAnsiConsole console)
    : PowerCommand<TelemetrySettings>(spans, console)
{
    protected override Task<bool> RunAsync(TelemetrySettings settings, CancellationToken cancellationToken)
    {
        telemetryState.SetEnabled(true);

        if (TelemetryStateProvider.HasOptOutEnvVar)
        {
            Console.MarkupLineInterpolated(CultureInfo.InvariantCulture,
                $"{Emoji.Known.Warning} Telemetry is now enabled.");
            Console.MarkupLine("DGTP_TELEMETRY_OPTOUT / DO_NOT_TRACK is set.");
        }
        else
        {
            Console.MarkupLineInterpolated(CultureInfo.InvariantCulture,
                $"[green]●[/] Telemetry is now enabled.");
        }

        return Task.FromResult(true);
    }
}
