// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common;
using Spectre.Console;
using Spectre.Console.Cli;

namespace dgt.power.connection.Commands;

// ReSharper disable once ClassNeverInstantiated.Global
public class DeleteConnectionCommand(IProfileManager profileManager, IAnsiConsole console) : Command<DeleteConnectionSettings>
{
    protected override int Execute(CommandContext context, DeleteConnectionSettings settings, CancellationToken cancellationToken)
    {
        if (settings.All && settings.Name != null)
        {
            console.MarkupLine("[red]Error: specify either a connection name or --all, not both.[/]");
            return -1;
        }

        if (!settings.All && settings.Name == null)
        {
            console.MarkupLine("[red]Error: provide a connection name or use --all to delete all connections.[/]");
            return -1;
        }

        if (settings.All)
        {
            profileManager.Purge();
            var allRule = new Rule("All connections have been [red]deleted[/].");
            allRule.LeftJustified();
            console.Write(allRule);
            return 0;
        }

        var identities = profileManager.LoadIdentities();
        identities.Remove(settings.Name!);
        profileManager.Save();

        var rule = new Rule($"Connection [lime]{settings.Name}[/] is removed.");
        rule.LeftJustified();
        console.Write(rule);
        return 0;
    }
}

