// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using dgt.power.common;
using Spectre.Console;
using Spectre.Console.Cli;

// ReSharper disable ClassNeverInstantiated.Global

namespace dgt.power.profile.Commands;

public class SelectProfileCommand(IProfileManager profileManager, IAnsiConsole console) : Command<NamedProfileSettings>
{
    protected override int Execute(CommandContext context, NamedProfileSettings settings, CancellationToken cancellationToken)
    {
        Debug.Assert(settings != null, nameof(settings) + " != null");

        var identities = profileManager.LoadIdentities();
        if (!identities.Contains(settings.Name.ToUpperInvariant()))
        {
            console.MarkupLine($"[Red]Identity {settings.Name} not found![/]");
            return -1;
        }

        identities.SetCurrent(settings.Name.ToUpperInvariant());
        profileManager.Save();

        var rule = new Rule($"Identity [lime]{settings.Name}[/] set.");
        rule.LeftJustified();
        console.Write(rule);

        return 0;
    }
}
