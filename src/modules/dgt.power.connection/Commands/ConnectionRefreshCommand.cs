// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common;
using dgt.power.common.Commands;
using dgt.power.connection.Base;
using Spectre.Console;
using Spectre.Console.Cli;

namespace dgt.power.connection.Commands;

/// <summary>
/// Forces an interactive MSAL browser login for the active connection and saves the refreshed token.
/// For classic (non-MSAL) connections this is a no-op.
/// Exit codes:
///   0 — authentication refreshed successfully (or not required for classic connections)
///   1 — refresh failed
/// </summary>
// ReSharper disable once ClassNeverInstantiated.Global
public class ConnectionRefreshCommand(IXrmConnection xrmConnection, IAnsiConsole console)
    : AsyncCommand<ConnectionSettings>
{
    protected override async Task<int> ExecuteAsync(CommandContext context, ConnectionSettings settings, CancellationToken cancellationToken)
    {
        console.MarkupLine("[yellow]AUTH: Opening browser for interactive login...[/]");
        await xrmConnection.RefreshAuthAsync();
        console.MarkupLine("[green]AUTH_OK: Authentication refreshed successfully.[/]");
        return (int)ExitCode.Success;
    }
}
