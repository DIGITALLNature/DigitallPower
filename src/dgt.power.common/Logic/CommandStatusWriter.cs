// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using Spectre.Console;

namespace dgt.power.common.Logic;

/// <summary>
/// Prints a plain start line and a left-aligned end rule with result and duration.
/// </summary>
public sealed class CommandStatusWriter(IAnsiConsole console) : ICommandStatusWriter
{
    public void WriteStart(string commandName)
    {
        var symbol = console.Profile.Capabilities.Unicode ? "▶" : ">";
        console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[grey]{symbol}[/] {commandName}");
    }

    public void WriteEnd(string commandName, bool success, TimeSpan duration)
    {
        var unicode = console.Profile.Capabilities.Unicode;
        var (symbol, color, word) = success
            ? (unicode ? "✔" : "OK", "lime", string.Empty)
            : (unicode ? "✖" : "FAIL", "red", " failed");
        var title = Markup.Escape($"{symbol} {commandName}{word} · {FormatDuration(duration)}");
        if (!unicode)
        {
            title = title.Replace('·', '-');
        }

        console.Write(new Rule($"[{color}]{title}[/]")
        {
            Justification = Justify.Left,
            Border = unicode ? BoxBorder.Square : BoxBorder.Ascii
        });
    }

    private static string FormatDuration(TimeSpan duration) =>
        duration.TotalSeconds < 1
            ? string.Create(CultureInfo.InvariantCulture, $"{duration.TotalMilliseconds:0}ms")
            : string.Create(CultureInfo.InvariantCulture, $"{duration.TotalSeconds:0.0}s");
}
