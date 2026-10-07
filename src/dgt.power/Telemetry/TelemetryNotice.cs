// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using Spectre.Console;

namespace dgt.power.Telemetry;

/// <summary>
/// Renders the telemetry notice when an installation ID is first created.
/// </summary>
internal static class TelemetryNotice
{
    /// <summary>
    /// Displays the telemetry notice.
    /// </summary>
    public static void Show(IAnsiConsole console)
    {
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
}
