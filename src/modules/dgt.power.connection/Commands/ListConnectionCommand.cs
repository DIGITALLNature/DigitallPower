// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using dgt.power.common.Connections;
using dgt.power.connection.Base;
using Spectre.Console;
using Spectre.Console.Cli;

namespace dgt.power.connection.Commands;

public class ListConnectionCommand(
    IConnectionStore connectionStore,
    ConnectionInvocationContext invocationContext,
    IAnsiConsole console)
    : Command<ConnectionSettings>
{
    public override int Execute(CommandContext context, ConnectionSettings settings, CancellationToken cancellationToken)
    {
        var current = invocationContext.ConnectionName ?? connectionStore.Current;
        var grid = new Grid();
        grid.AddColumn().AddColumn().AddColumn();
        grid.AddRow("Current", "Name", "Type");

        foreach (var (name, definition) in connectionStore.GetAll())
        {
            grid.AddRow(
                string.Equals(name, current, StringComparison.OrdinalIgnoreCase) ? "*" : string.Empty,
                name,
                definition.TypeDisplayName);
        }

        console.Write(grid);
        return 0;
    }
}
