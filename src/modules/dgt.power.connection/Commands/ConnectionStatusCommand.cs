// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common;
using dgt.power.common.Connections;
using dgt.power.common.Commands;
using dgt.power.connection.Base;
using Spectre.Console;
using Spectre.Console.Cli;

namespace dgt.power.connection.Commands;

public class ConnectionStatusCommand(
    IDataverseConnection dataverseConnection,
    IConnectionStore connectionStore,
    ConnectionInvocationContext invocationContext,
    IAnsiConsole console)
    : AsyncCommand<ConnectionSettings>
{
    public override async Task<int> ExecuteAsync(CommandContext context, ConnectionSettings settings, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(invocationContext.ConnectionString))
        {
            console.MarkupLine("[grey]AUTH_SKIP: An ad-hoc connection string is active; no stored token to check.[/]");
            return (int)ExitCode.Success;
        }

        var name = invocationContext.ConnectionName ?? connectionStore.Current;
        var isFederated = name is not null && connectionStore.Find(name) is AzureDevOpsFederatedConnection;
        var isValid = await dataverseConnection.CheckAuthAsync();

        if (isValid)
        {
            console.MarkupLine("[green]AUTH_OK: Authentication is valid — no interactive login required.[/]");
            return (int)ExitCode.Success;
        }

        if (isFederated)
        {
            console.MarkupLine("[red]AUTH_REQUIRED: Failed to acquire a token via Azure DevOps workload identity federation.[/]");
            console.MarkupLine("[red]              Verify the Azure Pipelines access token and service connection authorization.[/]");
            return (int)ExitCode.AuthRequired;
        }

        console.MarkupLine("[red]AUTH_REQUIRED: Interactive login is required.[/]");
        console.MarkupLine("[red]              Ask the user to authenticate, then retry the command.[/]");
        console.MarkupLine("[grey]Tip: run 'dgtp connection refresh' to re-authenticate.[/]");
        return (int)ExitCode.AuthRequired;
    }
}
