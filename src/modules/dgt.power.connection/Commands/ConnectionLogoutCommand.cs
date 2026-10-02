// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common.Commands;
using dgt.power.common.Connections;
using Spectre.Console;
using Spectre.Console.Cli;

namespace dgt.power.connection.Commands;

public sealed class ConnectionLogoutCommand(
    IConnectionStore connectionStore,
    IUserTokenCache userTokenCache,
    ConnectionInvocationOptions invocationOptions,
    IAnsiConsole console)
    : AsyncCommand<NamedConnectionSettings>
{
    protected override async Task<int> ExecuteAsync(
        CommandContext context,
        NamedConnectionSettings settings,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(settings);
        var connection = connectionStore.Find(settings.Name);
        if (connection is null)
        {
            console.MarkupLine($"[red]Connection {Markup.Escape(settings.Name)} not found.[/]");
            return (int)ExitCode.Error;
        }

        var authenticationRecord = CredentialFactory.GetAuthenticationRecord(connection);
        if (authenticationRecord is null)
        {
            console.MarkupLine("[grey]This connection has no stored user account to log out.[/]");
            return (int)ExitCode.Success;
        }

        var removed = await userTokenCache.RemoveAccountAsync(
            authenticationRecord,
            invocationOptions.AllowUnencryptedStorage,
            cancellationToken);
        console.MarkupLine(removed
            ? $"[green]Signed out the user account for connection {Markup.Escape(settings.Name)}.[/]"
            : "[grey]No cached user account was found for this connection.[/]");
        return (int)ExitCode.Success;
    }
}
