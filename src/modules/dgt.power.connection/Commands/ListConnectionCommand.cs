// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common;
using dgt.power.connection.Base;
using Spectre.Console;
using Spectre.Console.Cli;

// ReSharper disable ClassNeverInstantiated.Global

namespace dgt.power.connection.Commands;

public class ListConnectionCommand(IProfileManager profileManager, IAnsiConsole console) : Command<ConnectionSettings>
{
    protected override int Execute(CommandContext context, ConnectionSettings settings, CancellationToken cancellationToken)
    {
        var identities = profileManager.LoadIdentities();

        var grid = new Grid();
        grid.AddColumn().AddColumn().AddColumn();
        grid.AddRow("Current", "Name", "Type");

        foreach (var identity in identities.Infos)
        {
            grid.AddRow(identity.Name == profileManager.Current ? "*" : string.Empty, identity.Name, identity.Type);
        }

        console.Write(grid);
        return 0;
    }
}
