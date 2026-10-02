// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common;
using dgt.power.common.Connections;
using dgt.power.common.Commands;
using dgt.power.connection.Base;
using Spectre.Console;
using Spectre.Console.Cli;

namespace dgt.power.connection.Commands;

public class ConnectionRefreshCommand(
    IConnectionStore connectionStore,
    ConnectionInvocationOptions invocationOptions,
    IXrmConnection xrmConnection,
    IAnsiConsole console)
    : AsyncCommand<ConnectionSettings>
{
    public override async Task<int> ExecuteAsync(CommandContext context, ConnectionSettings settings, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(invocationOptions.ConnectionString))
        {
            console.MarkupLine("[grey]AUTH_SKIP: An ad-hoc connection string is active; nothing is persisted or refreshed.[/]");
            return (int)ExitCode.Success;
        }

        var name = invocationOptions.ConnectionName ?? connectionStore.Current;
        var definition = name is null ? null : connectionStore.Find(name);
        if (definition is not (InteractiveConnection or DeviceCodeConnection))
        {
            console.MarkupLine("[grey]AUTH_SKIP: This connection type does not support interactive refresh.[/]");
            return (int)ExitCode.Success;
        }

        try
        {
            console.MarkupLine("[yellow]AUTH: Starting interactive authentication...[/]");
            await xrmConnection.RefreshAuthAsync();
            console.MarkupLine("[green]AUTH_OK: Authentication refreshed successfully.[/]");
            return (int)ExitCode.Success;
        }
#pragma warning disable CA1031 // The command reports failures with a non-success exit code.
        catch (Exception exception)
#pragma warning restore CA1031
        {
            console.MarkupLine($"[red]Error: {Markup.Escape(exception.Message)}[/]");
            return (int)ExitCode.Error;
        }
    }
}
