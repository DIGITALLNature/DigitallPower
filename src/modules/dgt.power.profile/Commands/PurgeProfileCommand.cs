// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics.CodeAnalysis;
using dgt.power.common;
using dgt.power.profile.Base;
using Spectre.Console;
using Spectre.Console.Cli;

// ReSharper disable ClassNeverInstantiated.Global

namespace dgt.power.profile.Commands;

public class PurgeProfileCommand : Command<ProfileSettings>
{
    private readonly IProfileManager _profileManager;
    private readonly IAnsiConsole _console;

    public PurgeProfileCommand(IProfileManager profileManager, IAnsiConsole console)
    {
        _profileManager = profileManager;
        _console = console;
    }

    protected override int Execute(CommandContext context, ProfileSettings settings, CancellationToken cancellationToken)
    {
        _profileManager.Purge();

        var rule = new Rule("Identities have been [red]purged[/].");
        rule.LeftJustified();
        _console.Write(rule);

        return 0;
    }
}
