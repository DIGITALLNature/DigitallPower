// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using dgt.power.common;
using dgt.power.common.Extensions;
using Spectre.Console;

namespace dgt.power;

internal sealed class Tracer : ITracer
{
    public void Log(string message, TraceEventType type) => AnsiConsole.MarkupLine($"[underline red]{type}:[/]  {message}");

    public void Start<T>(T action) where T : IPowerLogic => AnsiConsole.Write(new Rule($"[lime]{action.GetType().Name} - Start[/]"));

    public bool NotConfigured<T>(T action) where T : IPowerLogic
    {
        AnsiConsole.Write(new Rule($"[red]{action.GetType().Name} - Not Configured[/]"));
        return false;
    }

    public bool Skipped<T>(T action) where T : IPowerLogic
    {
        AnsiConsole.Write(new Rule($"[yellow]{action.GetType().Name} - Skipped[/]"));
        return true;
    }

    public bool End<T>(T action, bool result) where T : IPowerLogic
    {
        AnsiConsole.Write(result ? new Rule($"[lime]{action.GetType().Name} - End:OK[/]") : new Rule($"[red]{action.GetType().Name} - End:ERROR[/]"));
        return result;
    }

    public void Exception(Exception e, TraceEventType type)
    {
        var error = e.RootException();
        AnsiConsole.WriteException(error);
    }
}
