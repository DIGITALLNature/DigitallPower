// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common.Connections;
using dgt.power.connection.Commands;
using dgt.power.connection.tests.Base;
using Spectre.Console.Cli;

namespace dgt.power.connection.tests;

public class CreateConnectionCommandTests
    : ConnectionTestsBase<CreateConnectionCommand, CreateConnectionSettings>
{
    [Test]
    public async Task SavesConnectionAfterSuccessfulVerification()
    {
        var verifier = new FakeConnectionVerifier();
        var result = await RunAsync(verifier, CreateFederatedSettings("pipeline"));

        await Assert.That(result).IsEqualTo(0);
        await Assert.That(ConnectionStore.Find("pipeline")).IsTypeOf<AzureDevOpsFederatedConnection>();
        await Assert.That(ConnectionStore.Current).IsEqualTo("pipeline");
        await Assert.That(verifier.WasCalled).IsTrue();
    }

    [Test]
    public async Task FailedVerificationPreservesExistingConnectionAndSelection()
    {
        var existing = new InteractiveConnection
        {
            Url = "https://existing.crm.dynamics.com",
            TenantId = "tenant"
        };
        ConnectionStore.Upsert("prod", existing);
        ConnectionStore.Upsert("other", new DeviceCodeConnection
        {
            Url = "https://other.crm.dynamics.com",
            TenantId = "tenant"
        }, makeCurrent: false);
        var verifier = new FakeConnectionVerifier(new InvalidOperationException("verification failed"));

        var exception = await RunExpectingVerificationFailureAsync(
            verifier,
            CreateFederatedSettings("prod"));

        await Assert.That(exception.Message).IsEqualTo("verification failed");
        var saved = ConnectionStore.Find("prod");
        await Assert.That(saved).IsTypeOf<InteractiveConnection>();
        await Assert.That(((InteractiveConnection)saved!).Url).IsEqualTo(existing.Url);
        await Assert.That(ConnectionStore.Current).IsEqualTo("prod");
        await Assert.That(verifier.WasCalled).IsTrue();
    }

    [Test]
    public async Task FailedVerificationRestoresPreviouslyStoredSecret()
    {
        ConnectionStore.Upsert("prod", new ClientSecretConnection
        {
            Url = "https://existing.crm.dynamics.com",
            TenantId = "tenant",
            ClientId = "client"
        });
        SecretStore.WriteSecret("prod", "clientSecret", "old-secret");
        var verifier = new FakeConnectionVerifier(new InvalidOperationException("verification failed"));
        var settings = new CreateConnectionSettings
        {
            Name = "prod",
            Url = "https://replacement.crm.dynamics.com",
            TenantId = "tenant",
            ClientId = "client",
            ClientSecret = "new-secret"
        };

        await RunExpectingVerificationFailureAsync(verifier, settings);

        await Assert.That(SecretStore.ReadSecret("prod", "clientSecret")).IsEqualTo("old-secret");
        await Assert.That(((ClientSecretConnection)ConnectionStore.Find("prod")!).Url)
            .IsEqualTo("https://existing.crm.dynamics.com");
    }

    [Test]
    public async Task NoVerifySkipsDataverseVerification()
    {
        var verifier = new FakeConnectionVerifier();
        var result = await RunAsync(verifier, CreateFederatedSettings("pipeline", noVerify: true));

        await Assert.That(result).IsEqualTo(0);
        await Assert.That(verifier.WasCalled).IsFalse();
        await Assert.That(ConnectionStore.Find("pipeline")).IsTypeOf<AzureDevOpsFederatedConnection>();
    }

    [Test]
    public async Task SavesClientSecretWithoutPromptingOrLeakingValue()
    {
        var result = await RunAsync(new FakeConnectionVerifier(), new CreateConnectionSettings
        {
            Name = "prod", Url = "https://contoso.crm.dynamics.com",
            TenantId = "tenant", ClientId = "client", ClientSecret = "test-secret",
            NonInteractive = true
        });

        await Assert.That(result).IsEqualTo(0);
        await Assert.That(SecretStore.ReadSecret("prod", "clientSecret")).IsEqualTo("test-secret");
        await Assert.That(File.ReadAllText(Home.ConnectionsPath)).DoesNotContain("test-secret");
        await Assert.That(TestConsole.Output).DoesNotContain("test-secret");
    }

    [Test]
    [Arguments(null)]
    [Arguments("")]
    [Arguments("pfx-password")]
    public async Task SavesCertificatePasswordWithoutPrompting(string? password)
    {
        var result = await RunAsync(new FakeConnectionVerifier(), new CreateConnectionSettings
        {
            Name = "prod", Url = "https://contoso.crm.dynamics.com",
            TenantId = "tenant", ClientId = "client", CertificatePath = "certificate.pfx",
            CertificatePassword = password, NonInteractive = true
        });

        await Assert.That(result).IsEqualTo(0);
        await Assert.That(SecretStore.ReadSecret("prod", "certificatePassword")).IsEqualTo(password ?? "");
        await Assert.That(ConnectionStore.Find("prod")).IsTypeOf<ClientCertificateConnection>();
        if (!string.IsNullOrEmpty(password))
        {
            await Assert.That(File.ReadAllText(Home.ConnectionsPath)).DoesNotContain(password);
            await Assert.That(TestConsole.Output).DoesNotContain(password);
        }
    }

    private async Task<int> RunAsync(
        FakeConnectionVerifier verifier,
        CreateConnectionSettings settings)
    {
        ICommand<CreateConnectionSettings> command = new CreateConnectionCommand(
            ConnectionStore,
            SecretStore,
            new CredentialFactory(SecretStore, TestConsole),
            verifier,
            new ConnectionInvocationContext(),
            TestConsole);

        return await command.ExecuteAsync(
            new CommandContext(Enumerable.Empty<string>(), new EmptyRemainingArguments(), "create", null),
            settings,
            CancellationToken.None);
    }

    private async Task<InvalidOperationException> RunExpectingVerificationFailureAsync(
        FakeConnectionVerifier verifier,
        CreateConnectionSettings settings)
    {
        try
        {
            await RunAsync(verifier, settings);
        }
        catch (InvalidOperationException exception)
        {
            return exception;
        }

        throw new InvalidOperationException("Expected Dataverse verification to fail.");
    }

    private static CreateConnectionSettings CreateFederatedSettings(string name, bool noVerify = false) => new()
    {
        Name = name,
        Url = "https://contoso.crm.dynamics.com",
        TenantId = "tenant",
        ClientId = "client",
        ServiceConnectionId = "service-connection",
        AzureDevOpsFederated = true,
        NoVerify = noVerify
    };

    private sealed class FakeConnectionVerifier(Exception? failure = null) : IConnectionVerifier
    {
        public bool WasCalled { get; private set; }

        public Task VerifyAsync(
            string connectionName,
            ConnectionDefinition connection,
            bool allowUnencryptedStorage,
            CancellationToken cancellationToken)
        {
            WasCalled = true;
            return failure is null ? Task.CompletedTask : Task.FromException(failure);
        }
    }
}
