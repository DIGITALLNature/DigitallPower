// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using Spectre.Console;

namespace dgt.power.common;

/// <summary>Shared banner that tells the user a dry run will not write to Dataverse.</summary>
public static class DryRunBanner
{
    public static void Write(IAnsiConsole console)
    {
        ArgumentNullException.ThrowIfNull(console);

        console.Write(new Panel("[yellow]No changes will be written to Dataverse.[/]")
        {
            Header = new PanelHeader("DRY RUN"), Border = BoxBorder.Rounded, BorderStyle = new Style(Color.Yellow), Padding = new Padding(1, 0, 1, 0)
        });
    }
}
