// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common;
using dgt.power.profile.Base;
using Spectre.Console;
using Spectre.Console.Cli;

// ReSharper disable ClassNeverInstantiated.Global


namespace dgt.power.profile.Commands;

public class ListProfileCommand(IProfileManager profileManager, IAnsiConsole console) : Command<ProfileSettings>
{
    protected override int Execute(CommandContext context, ProfileSettings settings, CancellationToken cancellationToken)
    {
        var identities = profileManager.LoadIdentities();

        var grid = new Grid();
        // Add columns
        grid.AddColumn().AddColumn().AddColumn();

        // Add header row
        grid.AddRow("Current", "Name", "Type");

        foreach (var identity in identities.Infos)
        {
            grid.AddRow(identity.Name == profileManager.Current ? "*" : string.Empty, identity.Name, identity.Type);
        }

        console.Write(grid);
        return 0;
    }
}
