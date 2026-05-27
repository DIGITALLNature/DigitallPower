// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics.CodeAnalysis;
using dgt.power.common;
using dgt.power.profile.Base;
using Spectre.Console;
using Spectre.Console.Cli;

// ReSharper disable ClassNeverInstantiated.Global


namespace dgt.power.profile.Commands;

public class ListProfileCommand : Command<ProfileSettings>
{
    private readonly IProfileManager _profileManager;
    private readonly IAnsiConsole _console;

    public ListProfileCommand(IProfileManager profileManager, IAnsiConsole console)
    {
        _profileManager = profileManager;
        _console = console;
    }

    protected override int Execute(CommandContext context, ProfileSettings settings, CancellationToken cancellationToken)
    {
        var identities = _profileManager.LoadIdentities();

        var grid = new Grid();
        // Add columns
        grid.AddColumn().AddColumn().AddColumn().AddColumn().AddColumn();

        // Add header row
        grid.AddRow("Current", "Name", "Type", "Protocol", "Insecure");

        foreach (var identity in identities.Infos)
        {
            grid.AddRow(identity.Name == _profileManager.Current ? "*" : string.Empty, identity.Name , identity.Type , _profileManager.CurrentIdentity?.SecurityProtocol ?? string.Empty,
                _profileManager.CurrentIdentity?.Insecure == true ? "yes" : "no");
        }

        _console.Write(grid);
        return 0;
    }
}
