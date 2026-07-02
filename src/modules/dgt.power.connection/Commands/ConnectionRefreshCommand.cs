// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common;
using dgt.power.common.Commands;
using dgt.power.common.Logic;
using dgt.power.connection.Base;
using Spectre.Console;
using Spectre.Console.Cli;

namespace dgt.power.connection.Commands;

/// <summary>
/// Forces an interactive MSAL browser login for the active connection and saves the refreshed token.
/// For connection-string connections this is a no-op — a message is printed and exit code 0 is returned.
/// Exit codes:
///   0 — authentication refreshed successfully, or no token refresh required (connection-string connection)
///   1 — refresh failed
/// </summary>
// ReSharper disable once ClassNeverInstantiated.Global
public class ConnectionRefreshCommand(IProfileManager profileManager, IXrmConnection xrmConnection, IAnsiConsole console)
    : AsyncCommand<ConnectionSettings>
{
    protected override async Task<int> ExecuteAsync(CommandContext context, ConnectionSettings settings, CancellationToken cancellationToken)
    {
        try
        {
            if (profileManager.CurrentIdentity is not TokenIdentity)
            {
                console.MarkupLine("[grey]AUTH_SKIP: Connection uses a connection string — no token to refresh.[/]");
                return (int)ExitCode.Success;
            }

            console.MarkupLine("[yellow]AUTH: Opening browser for interactive login...[/]");
            await xrmConnection.RefreshAuthAsync();
            console.MarkupLine("[green]AUTH_OK: Authentication refreshed successfully.[/]");
            return (int)ExitCode.Success;
        }
#pragma warning disable CA1031 // Intentional broad catch: all failures must return exit code 1
        catch (Exception ex)
#pragma warning restore CA1031
        {
            console.MarkupLine($"[red]Error: {Markup.Escape(ex.Message)}[/]");
            return (int)ExitCode.Error;
        }
    }
}
