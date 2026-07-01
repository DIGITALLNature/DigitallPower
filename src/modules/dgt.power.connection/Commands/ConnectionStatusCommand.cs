// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common;
using dgt.power.common.Commands;
using dgt.power.connection.Base;
using Spectre.Console;
using Spectre.Console.Cli;

namespace dgt.power.connection.Commands;

/// <summary>
/// Checks whether the current MSAL token is still valid without opening a browser.
/// Exit codes:
///   0 — token is valid (or connection uses classic auth, no MSAL)
///   2 — interactive login is required; ask the user to re-authenticate
/// Intended for coding agents to use as a pre-flight check before any Dataverse command.
/// </summary>
// ReSharper disable once ClassNeverInstantiated.Global
public class ConnectionStatusCommand(IXrmConnection xrmConnection, IAnsiConsole console)
    : AsyncCommand<ConnectionSettings>
{
    protected override async Task<int> ExecuteAsync(CommandContext context, ConnectionSettings settings, CancellationToken cancellationToken)
    {
        var isValid = await xrmConnection.CheckAuthAsync();

        if (isValid)
        {
            console.MarkupLine("[green]AUTH_OK: Token is valid — no interactive login required.[/]");
            return (int)ExitCode.Success;
        }

        console.MarkupLine("[red]AUTH_REQUIRED: Interactive login is required.[/]");
        console.MarkupLine("[red]              Ask the user to authenticate, then retry the command.[/]");
        console.MarkupLine("[grey]Tip: run 'dgtp connection refresh' to re-authenticate.[/]");
        return (int)ExitCode.AuthRequired;
    }
}
