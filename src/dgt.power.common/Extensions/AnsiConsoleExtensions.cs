// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using Spectre.Console;

namespace dgt.power.common.Extensions;

public static class AnsiConsoleExtensions
{
    public static void Log(this IAnsiConsole console, string message, TraceEventType type) =>
        console.MarkupLine($"[underline red]{type}:[/]  {message}");

    public static void Start<T>(this IAnsiConsole console, T action) where T : IPowerLogic =>
        console.Write(new Rule($"[lime]{action.GetType().Name} - Start[/]"));

    public static void NotConfigured<T>(this IAnsiConsole console, T action) where T : IPowerLogic =>
        console.Write(new Rule($"[red]{action.GetType().Name} - Not Configured[/]"));

    public static void Skipped<T>(this IAnsiConsole console, T action) where T : IPowerLogic =>
        console.Write(new Rule($"[yellow]{action.GetType().Name} - Skipped[/]"));

    public static void End<T>(this IAnsiConsole console, T action, bool result) where T : IPowerLogic =>
        console.Write(result ? new Rule($"[lime]{action.GetType().Name} - End:OK[/]") : new Rule($"[red]{action.GetType().Name} - End:ERROR[/]"));

    public static void Exception(this IAnsiConsole console, Exception e, TraceEventType type) =>
        console.WriteException(e.RootException());
}
