// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using dgt.power.common;
using dgt.power.common.Telemetry;
using dgt.power.telemetry.Base;
using Spectre.Console;

namespace dgt.power.telemetry.Commands;

public sealed class TelemetryDisableCommand(
    ICommandSpanFactory spans,
    TelemetryStateProvider telemetryState,
    IAnsiConsole console)
    : PowerCommand<TelemetrySettings>(spans, console)
{
    protected override Task<bool> RunAsync(TelemetrySettings settings, CancellationToken cancellationToken)
    {
        telemetryState.SetEnabled(false);

        Console.MarkupLineInterpolated(CultureInfo.InvariantCulture,
            $"[red]●[/] Telemetry is now disabled.");

        return Task.FromResult(true);
    }
}
