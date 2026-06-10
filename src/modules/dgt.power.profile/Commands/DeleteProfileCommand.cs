// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using dgt.power.common;
using Spectre.Console;
using Spectre.Console.Cli;

namespace dgt.power.profile.Commands;

public class DeleteProfileCommand(IProfileManager profileManager, IAnsiConsole console) : Command<NamedProfileSettings>
{
    protected override int Execute(CommandContext context, NamedProfileSettings settings, CancellationToken cancellationToken)
    {
        Debug.Assert(settings != null, nameof(settings) + " != null");

        var identities = profileManager.LoadIdentities();
        identities.Remove(settings.Name);
        profileManager.Save();

        var rule = new Rule($"Identity [lime]{settings.Name}[/] is removed.");
        rule.LeftJustified();
        console.Write(rule);

        return 0;
    }
}
