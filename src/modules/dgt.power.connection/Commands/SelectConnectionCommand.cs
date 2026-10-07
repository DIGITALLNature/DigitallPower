// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common.Connections;
using Spectre.Console;
using Spectre.Console.Cli;

namespace dgt.power.connection.Commands;

public class SelectConnectionCommand(IConnectionStore connectionStore, IAnsiConsole console)
    : Command<NamedConnectionSettings>
{
    public override int Execute(
        CommandContext context,
        NamedConnectionSettings settings,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(settings);
        if (connectionStore.Find(settings.Name) is null)
        {
            console.MarkupLine($"[red]Connection {Markup.Escape(settings.Name)} not found.[/]");
            return -1;
        }

        connectionStore.SetCurrent(settings.Name);
        var rule = new Rule($"Connection [lime]{Markup.Escape(settings.Name)}[/] set.");
        rule.LeftJustified();
        console.Write(rule);
        return 0;
    }
}
