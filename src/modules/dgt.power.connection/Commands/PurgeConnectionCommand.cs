// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common;
using dgt.power.connection.Base;
using Spectre.Console;
using Spectre.Console.Cli;

// ReSharper disable ClassNeverInstantiated.Global

namespace dgt.power.connection.Commands;

public class PurgeConnectionCommand(IProfileManager profileManager, IAnsiConsole console) : Command<ConnectionSettings>
{
    protected override int Execute(CommandContext context, ConnectionSettings settings, CancellationToken cancellationToken)
    {
        profileManager.Purge();

        var rule = new Rule("Connections have been [red]purged[/].");
        rule.LeftJustified();
        console.Write(rule);

        return 0;
    }
}
