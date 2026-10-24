// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using dgt.power.common;
using dgt.power.common.Storage;
using dgt.power.common.Telemetry;
using dgt.power.telemetry.Base;
using Spectre.Console;

namespace dgt.power.telemetry.Commands;

public sealed class TelemetryStatusCommand(
    ICommandSpanFactory spans,
    StateStore stateStore,
    IAnsiConsole console)
    : PowerCommand<TelemetrySettings>(spans, console)
{
    protected override Task<bool> RunAsync(TelemetrySettings settings, CancellationToken cancellationToken)
    {
        var enabled = stateStore.TelemetryEnabled;
        var optedOut = TelemetryEnvironment.IsOptedOut;
        var effectiveEnabled = enabled && !optedOut;

        TelemetryNotice.Show(Console);

        if (effectiveEnabled)
        {
            Console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[green]●[/] Telemetry is enabled.");
            Console.MarkupLine("To disable: dgtp telemetry disable");
        }
        else
        {
            Console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[red]●[/] Telemetry is disabled.");
            if (optedOut)
            {
                Console.MarkupLine("DGTP_TELEMETRY_OPTOUT / DO_NOT_TRACK is set.");
            }
            else
            {
                Console.MarkupLine("To enable: dgtp telemetry enable");
            }
        }

        return Task.FromResult(true);
    }
}
