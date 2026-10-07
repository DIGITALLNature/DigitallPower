// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using Azure.Core;
using Azure.Identity;
using dgt.power.common.Logic;
using Spectre.Console;

namespace dgt.power.common.Connections;

public sealed class CredentialFactory(ISecretStore secretStore, IAnsiConsole console)
{
    private const string DataverseClientId = "51f81489-12ee-4a9e-aaae-a2591f45987d";

    public TokenCredential Create(
        string connectionName,
        ConnectionDefinition connection,
        bool nonInteractive,
        bool allowUnencryptedStorage)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionName);
        ArgumentNullException.ThrowIfNull(connection);

        return connection switch
        {
            InteractiveConnection interactive => CreateInteractive(interactive, nonInteractive, allowUnencryptedStorage),
            DeviceCodeConnection deviceCode => CreateDeviceCode(deviceCode, nonInteractive, allowUnencryptedStorage),
            ClientSecretConnection clientSecret => CreateClientSecret(connectionName, clientSecret),
            ClientCertificateConnection certificate => CreateClientCertificate(connectionName, certificate, allowUnencryptedStorage),
            AzureDevOpsFederatedConnection federated => AzurePipelinesCredentialFactory.Create(
                federated.TenantId,
                federated.ClientId,
                federated.ServiceConnectionId),
            _ => throw new NotSupportedException($"Connection type '{connection.GetType().Name}' is not supported.")
        };
    }

    public Task<JsonElement> AuthenticateAsync(
        ConnectionDefinition connection,
        bool allowUnencryptedStorage,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(connection);
        return AuthenticateCoreAsync(connection, allowUnencryptedStorage, cancellationToken);
    }

    private async Task<JsonElement> AuthenticateCoreAsync(ConnectionDefinition connection, bool allowUnencryptedStorage, CancellationToken cancellationToken)
    {
        var context = new TokenRequestContext([GetScope(connection.Url)]);

        var record = connection switch
        {
            InteractiveConnection interactive => await CreateInteractive(interactive, nonInteractive: false, allowUnencryptedStorage).AuthenticateAsync(context, cancellationToken),
            DeviceCodeConnection deviceCode => await CreateDeviceCode(deviceCode, nonInteractive: false, allowUnencryptedStorage).AuthenticateAsync(context, cancellationToken),
            _ => throw new ArgumentException("Only user connections can be interactively authenticated.", nameof(connection))
        };

        using var stream = new MemoryStream();
        await record.SerializeAsync(stream, cancellationToken);
        using var jsonDocument = JsonDocument.Parse(stream.ToArray());
        return jsonDocument.RootElement.Clone();
    }

    private static InteractiveBrowserCredential CreateInteractive(
        InteractiveConnection connection,
        bool nonInteractive,
        bool allowUnencryptedStorage) =>
        new(new InteractiveBrowserCredentialOptions
        {
            ClientId = DataverseClientId,
            TenantId = connection.TenantId,
#pragma warning disable S1075
            RedirectUri = new Uri("http://localhost"),
#pragma warning restore S1075
            AuthenticationRecord = DeserializeAuthenticationRecord(connection.AuthenticationRecord),
            DisableAutomaticAuthentication = nonInteractive,
            TokenCachePersistenceOptions = CreateTokenCacheOptions(allowUnencryptedStorage)
        });

    private DeviceCodeCredential CreateDeviceCode(
        DeviceCodeConnection connection,
        bool nonInteractive,
        bool allowUnencryptedStorage) =>
        new(new DeviceCodeCredentialOptions
        {
            ClientId = DataverseClientId,
            TenantId = connection.TenantId,
            AuthenticationRecord = DeserializeAuthenticationRecord(connection.AuthenticationRecord),
            DisableAutomaticAuthentication = nonInteractive,
            TokenCachePersistenceOptions = CreateTokenCacheOptions(allowUnencryptedStorage),
            DeviceCodeCallback = (info, _) =>
            {
                console.MarkupLine(Markup.Escape(info.Message));
                return Task.CompletedTask;
            }
        });

    private ClientSecretCredential CreateClientSecret(string connectionName, ClientSecretConnection connection)
    {
        var secret = secretStore.ReadSecret(connectionName, "clientSecret")
            ?? throw new InvalidOperationException(
                $"Client secret is missing for connection '{connectionName}'. Recreate it with 'dgtp connection create'.");
        return new ClientSecretCredential(connection.TenantId, connection.ClientId, secret);
    }

    private ClientCertificateCredential CreateClientCertificate(
        string connectionName,
        ClientCertificateConnection connection,
        bool allowUnencryptedStorage)
    {
        var certificate = LoadCertificate(connectionName, connection);
        return new ClientCertificateCredential(
            connection.TenantId,
            connection.ClientId,
            certificate,
            new ClientCertificateCredentialOptions
            {
                TokenCachePersistenceOptions = CreateTokenCacheOptions(allowUnencryptedStorage)
            });
    }

    private X509Certificate2 LoadCertificate(string connectionName, ClientCertificateConnection connection)
    {
        if (!string.IsNullOrWhiteSpace(connection.CertificatePath))
        {
            var password = secretStore.ReadSecret(connectionName, "certificatePassword");
            return X509CertificateLoader.LoadPkcs12FromFile(
                connection.CertificatePath,
                password,
                X509KeyStorageFlags.EphemeralKeySet);
        }

        if (string.IsNullOrWhiteSpace(connection.Thumbprint))
        {
#pragma warning disable S2302 // "connection" is part of the error message, not a parameter name.
            throw new InvalidOperationException($"Certificate details are missing for connection '{connectionName}'.");
#pragma warning restore S2302
        }

        using var certificateStore = new X509Store(StoreName.My, StoreLocation.CurrentUser);
        certificateStore.Open(OpenFlags.ReadOnly);
        var certificate = certificateStore.Certificates
            .Find(X509FindType.FindByThumbprint, connection.Thumbprint, validOnly: false)
            .FirstOrDefault();
        return certificate ?? throw new InvalidOperationException(
            $"Certificate '{connection.Thumbprint}' was not found in the CurrentUser certificate store.");
    }

    private static TokenCachePersistenceOptions CreateTokenCacheOptions(bool allowUnencryptedStorage) =>
        new()
        {
            Name = "dgtp",
            UnsafeAllowUnencryptedStorage = allowUnencryptedStorage
        };

    private static AuthenticationRecord? DeserializeAuthenticationRecord(JsonElement? record)
    {
        if (record is not { ValueKind: JsonValueKind.Object } jsonRecord)
        {
            return null;
        }

        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonRecord.GetRawText()));
        return AuthenticationRecord.Deserialize(stream);
    }

    public static AuthenticationRecord? GetAuthenticationRecord(ConnectionDefinition connection) =>
        connection switch
        {
            InteractiveConnection interactive => DeserializeAuthenticationRecord(interactive.AuthenticationRecord),
            DeviceCodeConnection deviceCode => DeserializeAuthenticationRecord(deviceCode.AuthenticationRecord),
            _ => null
        };

    private static string GetScope(string url)
    {
        var uri = new Uri(url);
        return $"{uri.GetLeftPart(UriPartial.Authority)}/.default";
    }
}
