// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using System.Globalization;
using dgt.power.common;
using dgt.power.common.Extensions;
using dgt.power.Telemetry;
using Spectre.Console;

namespace dgt.power;

internal sealed class CommandDiagnostics(TelemetryState state, IAnsiConsole console) : IDiagnostics
{
    public void Log(string message, TraceEventType type) =>
        console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[underline red]{type}:[/]  {message}");

    public void Exception(Exception exception, TraceEventType type)
    {
        ArgumentNullException.ThrowIfNull(exception);

        console.WriteException(exception.RootException());
        var activity = Activity.Current;
        if (state.IsActive && activity is not null)
        {
            ActivityExceptions.Record(activity, exception, escaped: false);
        }
    }
}
