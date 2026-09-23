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
/// Checks whether the current MSAL token is still valid without opening a browser.
/// Exit codes:
///   0 — token is valid (or connection uses a connection string — no MSAL token to check)
///   2 — interactive login is required; ask the user to re-authenticate
/// Intended for coding agents to use as a pre-flight check before any Dataverse command.
/// </summary>
// ReSharper disable once ClassNeverInstantiated.Global
public class ConnectionStatusCommand(IXrmConnection xrmConnection, IProfileManager profileManager, IAnsiConsole console)
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

        if (profileManager.CurrentIdentity is AzureDevOpsFederatedIdentity)
        {
            console.MarkupLine("[red]AUTH_REQUIRED: Failed to acquire a token via Azure DevOps workload identity federation.[/]");
            console.MarkupLine("[red]              Verify 'SYSTEM_ACCESSTOKEN' is exposed to this pipeline step and that the service connection is authorized for this job.[/]");
            return (int)ExitCode.AuthRequired;
        }

        console.MarkupLine("[red]AUTH_REQUIRED: Interactive login is required.[/]");
        console.MarkupLine("[red]              Ask the user to authenticate, then retry the command.[/]");
        console.MarkupLine("[grey]Tip: run 'dgtp connection refresh' to re-authenticate.[/]");
        return (int)ExitCode.AuthRequired;
    }
}
