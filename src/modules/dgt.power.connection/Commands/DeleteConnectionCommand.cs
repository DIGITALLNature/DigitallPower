// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common.Connections;
using Spectre.Console;
using Spectre.Console.Cli;

namespace dgt.power.connection.Commands;

public class DeleteConnectionCommand(
    IConnectionStore connectionStore,
    ISecretStore secretStore,
    IUserTokenCache userTokenCache,
    ConnectionInvocationContext invocationContext,
    IAnsiConsole console)
    : AsyncCommand<DeleteConnectionSettings>
{
    public override async Task<int> ExecuteAsync(
        CommandContext context,
        DeleteConnectionSettings settings,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(settings);
        var connections = connectionStore.GetAll();
        if (settings.All)
        {
            return await DeleteAllAsync(settings, connections, cancellationToken);
        }

        var name = settings.Name!;
        if (!connections.TryGetValue(name, out var definition))
        {
            console.MarkupLine($"[red]Connection {Markup.Escape(name)} not found.[/]");
            return -1;
        }

        await RemoveUnreferencedAccountAsync(name, definition, connections, cancellationToken);
        DeleteSecretsIfNeeded(name, definition);
        connectionStore.Remove(name);
        var rule = new Rule($"Connection [lime]{Markup.Escape(name)}[/] is removed.");
        rule.LeftJustified();
        console.Write(rule);
        return 0;
    }

    private async Task<int> DeleteAllAsync(
        DeleteConnectionSettings settings,
        IReadOnlyDictionary<string, ConnectionDefinition> connections,
        CancellationToken cancellationToken)
    {
        if (connections.Count == 0)
        {
            console.MarkupLine("[grey]No connections to delete.[/]");
            return 0;
        }

        if (!settings.Yes)
        {
            console.MarkupLine("[yellow]The following connections will be deleted:[/]");
            foreach (var name in connections.Keys)
            {
                console.MarkupLine($"  [red]- {Markup.Escape(name)}[/]");
            }

            if (!console.Confirm($"Delete all {connections.Count} connection(s)?", defaultValue: false))
            {
                console.MarkupLine("[grey]Aborted.[/]");
                return 0;
            }
        }

        var accountRecords = connections.Values
            .Select(CredentialFactory.GetAuthenticationRecord)
            .Where(record => record is not null)
            .DistinctBy(record => record!.HomeAccountId, StringComparer.OrdinalIgnoreCase);
        foreach (var record in accountRecords)
        {
            await userTokenCache.RemoveAccountAsync(
                record!,
                invocationContext.AllowUnencryptedStorage,
                cancellationToken);
        }

        foreach (var (name, definition) in connections)
        {
            DeleteSecretsIfNeeded(name, definition);
        }

        connectionStore.RemoveAll();
        var rule = new Rule("All connections have been [red]deleted[/].");
        rule.LeftJustified();
        console.Write(rule);
        return 0;
    }

    private async Task RemoveUnreferencedAccountAsync(
        string connectionName,
        ConnectionDefinition definition,
        IReadOnlyDictionary<string, ConnectionDefinition> connections,
        CancellationToken cancellationToken)
    {
        var record = CredentialFactory.GetAuthenticationRecord(definition);
        if (record is null)
        {
            return;
        }

        var sharedByAnotherConnection = connections
            .Where(pair => !string.Equals(pair.Key, connectionName, StringComparison.OrdinalIgnoreCase))
            .Select(pair => CredentialFactory.GetAuthenticationRecord(pair.Value))
            .Any(other => string.Equals(
                other?.HomeAccountId,
                record.HomeAccountId,
                StringComparison.OrdinalIgnoreCase));
        if (!sharedByAnotherConnection)
        {
            await userTokenCache.RemoveAccountAsync(
                record,
                invocationContext.AllowUnencryptedStorage,
                cancellationToken);
        }
    }

    private void DeleteSecretsIfNeeded(string name, ConnectionDefinition definition)
    {
        if (definition is ClientSecretConnection or ClientCertificateConnection { CertificatePath: not null })
        {
            secretStore.Delete(name);
        }
    }
}
