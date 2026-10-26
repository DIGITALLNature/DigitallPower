// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using Spectre.Console;

namespace dgt.power.common.Telemetry;

/// <summary>
/// Renders the telemetry notice explaining what data is collected and how to opt out.
/// </summary>
public static class TelemetryNotice
{
    /// <summary>
    /// Displays the telemetry notice.
    /// </summary>
    public static void Show(IAnsiConsole console)
    {
        ArgumentNullException.ThrowIfNull(console);

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
