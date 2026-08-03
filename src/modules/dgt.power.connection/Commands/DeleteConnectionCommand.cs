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
        ArgumentNullException.ThrowIfNull(settings);

        if (settings.All)
        {
            var allConnections = profileManager.LoadIdentities();
            var names = allConnections.Infos.Select(i => i.Name).ToList();

            if (!settings.Yes)
            {
                if (names.Count == 0)
                {
                    console.MarkupLine("[grey]No connections to delete.[/]");
                    return 0;
                }

                console.MarkupLine("[yellow]The following connections will be deleted:[/]");
                foreach (var name in names)
                {
                    console.MarkupLine($"  [red]- {Markup.Escape(name)}[/]");
                }

                if (!console.Confirm($"Delete all {names.Count} connection(s)?", defaultValue: false))
                {
                    console.MarkupLine("[grey]Aborted.[/]");
                    return 0;
                }
            }

            profileManager.Purge();
            var allRule = new Rule("All connections have been [red]deleted[/].");
            allRule.LeftJustified();
            console.Write(allRule);
            return 0;
        }

        var identities = profileManager.LoadIdentities();
        identities.Remove(settings.Name!);
        profileManager.Save();

        var rule = new Rule($"Connection [lime]{Markup.Escape(settings.Name!)}[/] is removed.");
        rule.LeftJustified();
        console.Write(rule);
        return 0;
    }
}

