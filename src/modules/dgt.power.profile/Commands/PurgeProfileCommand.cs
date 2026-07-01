// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common;
using dgt.power.profile.Base;
using Spectre.Console;
using Spectre.Console.Cli;

// ReSharper disable ClassNeverInstantiated.Global

namespace dgt.power.profile.Commands;

public class PurgeProfileCommand(IProfileManager profileManager, IAnsiConsole console) : Command<ProfileSettings>
{
    protected override int Execute(CommandContext context, ProfileSettings settings, CancellationToken cancellationToken)
    {
        profileManager.Purge();

        var rule = new Rule("Identities have been [red]purged[/].");
        rule.LeftJustified();
        console.Write(rule);

        return 0;
    }
}
