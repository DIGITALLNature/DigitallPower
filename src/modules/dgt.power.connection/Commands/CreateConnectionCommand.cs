// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common.Connections;
using dgt.power.common.Logic;
using Spectre.Console;
using Spectre.Console.Cli;

namespace dgt.power.connection.Commands;

public class CreateConnectionCommand(
    IConnectionStore connectionStore,
    ISecretStore secretStore,
    CredentialFactory credentialFactory,
    IConnectionVerifier connectionVerifier,
    ConnectionInvocationContext invocationContext,
    IAnsiConsole console)
    : AsyncCommand<CreateConnectionSettings>
{
    public override Task<int> ExecuteAsync(CommandContext context, CreateConnectionSettings settings, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(settings);
        return ExecuteCoreAsync(settings, cancellationToken);
    }

    private async Task<int> ExecuteCoreAsync(CreateConnectionSettings settings, CancellationToken cancellationToken)
    {
        var previousDefinition = connectionStore.Find(settings.Name);
        var previousCurrent = connectionStore.Current;
        var definition = await CreateDefinitionAsync(settings, cancellationToken);
        var secretToStore = GetSecret(settings);
        var secretKey = GetSecretKey(settings);
        string? previousSecret = null;

        if (secretToStore is not null && secretKey is not null)
        {
            previousSecret = secretStore.ReadSecret(settings.Name, secretKey);
            secretStore.WriteSecret(settings.Name, secretKey, secretToStore);
        }

        var persisted = false;
        try
        {
            if (!settings.NoVerify)
            {
                await connectionVerifier.VerifyAsync(
                    settings.Name,
                    definition,
                    invocationContext.AllowUnencryptedStorage,
                    cancellationToken);
            }

            connectionStore.Upsert(settings.Name, definition);
            persisted = true;
            RemoveObsoleteSecret(settings.Name, previousDefinition, definition);
        }
        catch
        {
            if (secretKey is not null && secretToStore is not null)
            {
                if (previousSecret is null)
                {
                    secretStore.DeleteSecret(settings.Name, secretKey);
                }
                else
                {
                    secretStore.WriteSecret(settings.Name, secretKey, previousSecret);
                }
            }

            if (persisted)
            {
                if (previousDefinition is null)
                {
                    connectionStore.Remove(settings.Name);
                }
                else
                {
                    connectionStore.Upsert(
                        settings.Name,
                        previousDefinition,
                        makeCurrent: string.Equals(previousCurrent, settings.Name, StringComparison.OrdinalIgnoreCase));
                }
            }

            throw;
        }

        var rule = new Rule($"Connection [lime]{Markup.Escape(settings.Name)}[/] saved.");
        rule.LeftJustified();
        console.Write(rule);
        return 0;
    }

    private async Task<ConnectionDefinition> CreateDefinitionAsync(
        CreateConnectionSettings settings,
        CancellationToken cancellationToken)
    {
        if (settings.AzureDevOpsFederated)
        {
            var resolved = !string.IsNullOrWhiteSpace(settings.ServiceConnectionName)
                ? await AzureDevOpsServiceConnectionResolver.ResolveAsync(settings.ServiceConnectionName, cancellationToken)
                : null;
            return new AzureDevOpsFederatedConnection
            {
                Url = resolved?.Url ?? settings.Url!,
                TenantId = resolved?.TenantId ?? settings.TenantId!,
                ClientId = resolved?.ClientId ?? settings.ClientId!,
                ServiceConnectionId = resolved?.ServiceConnectionId ?? settings.ServiceConnectionId!,
                ServiceConnectionName = settings.ServiceConnectionName
            };
        }

        if (settings.ClientSecret is not null)
        {
            return new ClientSecretConnection
            {
                Url = settings.Url!,
                TenantId = settings.TenantId!,
                ClientId = settings.ClientId!
            };
        }

        if (settings.CertificateThumbprint is not null || settings.CertificatePath is not null)
        {
            return new ClientCertificateConnection
            {
                Url = settings.Url!,
                TenantId = settings.TenantId!,
                ClientId = settings.ClientId!,
                Thumbprint = settings.CertificateThumbprint,
                CertificatePath = settings.CertificatePath
            };
        }

        if (settings.DeviceCode)
        {
            var connection = new DeviceCodeConnection
            {
                Url = settings.Url!,
                TenantId = settings.TenantId
            };
            var record = await credentialFactory.AuthenticateAsync(
                connection,
                invocationContext.AllowUnencryptedStorage,
                cancellationToken);
            return connection with { AuthenticationRecord = record };
        }

        var interactiveConnection = new InteractiveConnection
        {
            Url = settings.Url!,
            TenantId = settings.TenantId
        };
        var authenticationRecord = await credentialFactory.AuthenticateAsync(
            interactiveConnection,
            invocationContext.AllowUnencryptedStorage,
            cancellationToken);
        return interactiveConnection with { AuthenticationRecord = authenticationRecord };
    }

    private static string? GetSecret(CreateConnectionSettings settings)
    {
        if (settings.ClientSecret is not null)
        {
            return settings.ClientSecret;
        }

        if (settings.CertificatePath is not null)
        {
            return settings.CertificatePassword ?? string.Empty;
        }

        return null;
    }

    private static string? GetSecretKey(CreateConnectionSettings settings) =>
        settings.ClientSecret is not null ? "clientSecret"
            : settings.CertificatePath is not null ? "certificatePassword"
            : null;

    private void RemoveObsoleteSecret(
        string connectionName,
        ConnectionDefinition? previous,
        ConnectionDefinition current)
    {
        if (previous is ClientSecretConnection && current is not ClientSecretConnection)
        {
            secretStore.DeleteSecret(connectionName, "clientSecret");
        }

        if (previous is ClientCertificateConnection { CertificatePath: not null }
            && current is not ClientCertificateConnection { CertificatePath: not null })
        {
            secretStore.DeleteSecret(connectionName, "certificatePassword");
        }
    }
}
