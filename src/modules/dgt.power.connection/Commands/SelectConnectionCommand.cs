// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common;
using Spectre.Console;
using Spectre.Console.Cli;

// ReSharper disable ClassNeverInstantiated.Global

namespace dgt.power.connection.Commands;

public class SelectConnectionCommand(IProfileManager profileManager, IAnsiConsole console) : Command<NamedConnectionSettings>
{
    protected override int Execute(CommandContext context, NamedConnectionSettings settings, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(settings);

        var identities = profileManager.LoadIdentities();
        if (!identities.Contains(settings.Name))
        {
            console.MarkupLine($"[Red]Connection {Markup.Escape(settings.Name)} not found![/]");
            return -1;
        }

        identities.SetCurrent(settings.Name);
        profileManager.Save();

        var rule = new Rule($"Connection [lime]{Markup.Escape(settings.Name)}[/] set.");
        rule.LeftJustified();
        console.Write(rule);

        return 0;
    }
}
