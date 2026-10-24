// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using Spectre.Console;

namespace dgt.power.common.Telemetry;

/// <summary>
/// Renders the telemetry notice explaining what data is collected and how to opt out.
/// The notice is shown at most once per process invocation.
/// </summary>
public static class TelemetryNotice
{
    private static bool _shown;

    /// <summary>
    /// Displays the telemetry notice. No-op if already shown in this invocation.
    /// </summary>
    public static void Show(IAnsiConsole console)
    {
        ArgumentNullException.ThrowIfNull(console);

        if (_shown)
        {
            return;
        }

        _shown = true;

        var panel = new Panel(
            "[grey]DIGITALL Power CLI collects anonymous usage data to improve the tool.\n" +
            "No personal data is collected. Only command name, duration, success/failure,\n" +
            "OS, tool version, and whether running on CI are recorded.\n\n" +
            "To opt out, set the environment variable:[/] [yellow]DGTP_TELEMETRY_OPTOUT=1[/]\n" +
            "[grey]Or use the[/] [yellow]--no-telemetry[/] [grey]flag per invocation.\n\n" +
            "More info: https://github.com/DIGITALLNature/DigitallPower[/]")
        {
            Header = new PanelHeader("[blue]Telemetry Notice[/]"),
            Border = BoxBorder.Rounded,
            Padding = new Padding(1, 0)
        };

        console.Write(panel);
        console.WriteLine();
    }

    /// <summary>
    /// Resets the shown flag. Intended for test isolation only.
    /// </summary>
    public static void Reset() => _shown = false;
}
