// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.IO.IsolatedStorage;
using Spectre.Console;

namespace dgt.power.Telemetry;

/// <summary>
/// Shows a one-time telemetry notice on first invocation.
/// </summary>
internal static class TelemetryNotice
{
    private const string NoticeShownFileName = "telemetry-notice.shown";

    /// <summary>
    /// Displays the telemetry notice if it hasn't been shown before.
    /// </summary>
    public static void ShowIfFirstRun(IsolatedStorageFile store, IAnsiConsole console)
    {
        if (store.FileExists(NoticeShownFileName)) return;

        var panel = new Panel(
            "[grey]DIGITALL Power CLI collects anonymous usage data to improve the tool.\n" +
            "No personal data is collected. Only command name, duration, success/failure,\n" +
            "OS, tool version, and whether running on CI are recorded.\n\n" +
            "To opt out, set the environment variable:[/] [yellow]DGT_TELEMETRY_OPTOUT=1[/]\n" +
            "[grey]Or use the[/] [yellow]--no-telemetry[/] [grey]flag per invocation.\n\n" +
            "More info: https://github.com/DIGITALLNature/DigitallPower[/]")
        {
            Header = new PanelHeader("[blue]Telemetry Notice[/]"),
            Border = BoxBorder.Rounded,
            Padding = new Padding(1, 0)
        };

        console.Write(panel);
        console.WriteLine();

        // Mark as shown
        using var stream = store.OpenFile(NoticeShownFileName, FileMode.Create, FileAccess.Write);
        using var writer = new StreamWriter(stream);
        writer.Write(DateTime.UtcNow.ToString("O"));
    }
}
