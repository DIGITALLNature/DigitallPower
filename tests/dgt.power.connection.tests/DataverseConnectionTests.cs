// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using dgt.power.common.Connections;
using dgt.power.common.Exceptions;
using dgt.power.common.Logic;
using dgt.power.connection.Base;
using dgt.power.connection.Commands;
using dgt.power.connection.tests.Base;
using Spectre.Console;

namespace dgt.power.connection.tests;

public class DataverseConnectionTests : ConnectionTestsBase<ConnectionStatusCommand, ConnectionSettings>
{
    [Test]
    public async Task SelectedConnection_RemainsRetainedAfterPersistedSelectionAndDefinitionChange()
    {
        ConnectionStore.Upsert("Original", new ClientSecretConnection
        {
            Url = ConnectionTestUrls.Dataverse,
            TenantId = "original-tenant",
            ClientId = "original-client"
        });
        using var connection = new DataverseConnection(ConnectionStore,
            new CredentialFactory(SecretStore, TestConsole), new ConnectionInvocationContext(), TestConsole);
        await Assert.That(connection.GetServiceAsync()).Throws<InvalidOperationException>()
            .WithMessageContaining("'Original'");
        ConnectionStore.Upsert("Replacement", new ClientSecretConnection
        {
            Url = "https://replacement.crm.dynamics.com",
            TenantId = "replacement-tenant",
            ClientId = "replacement-client"
        });
        ConnectionStore.Upsert("Original", new ClientSecretConnection
        {
            Url = "https://changed.crm.dynamics.com",
            TenantId = "changed-tenant",
            ClientId = "changed-client"
        }, makeCurrent: false);
        TestConsole.Clear(home: false);

        await Assert.That(connection.GetServiceAsync(Guid.NewGuid())).Throws<InvalidOperationException>()
            .WithMessageContaining("'Original'");

        await Assert.That(ConnectionStore.Current).IsEqualTo("Replacement");
        await Assert.That(TestConsole.Output).Contains(ConnectionTestUrls.Dataverse);
        await Assert.That(TestConsole.Output).DoesNotContain("replacement.crm");
        await Assert.That(TestConsole.Output).DoesNotContain("changed.crm");
    }

    [Test]
    public async Task ExplicitName_TakesPrecedenceOverSavedSelection()
    {
        ConnectionStore.Upsert("Explicit", new ClientSecretConnection
        {
            Url = ConnectionTestUrls.Dataverse, TenantId = "tenant", ClientId = "client"
        });
        ConnectionStore.Upsert("Saved", new InteractiveConnection { Url = "https://saved.crm.dynamics.com" });
        var invocation = new ConnectionInvocationContext();
        invocation.Capture(new ConnectionSettings { Connection = "Explicit" });
        using var connection = new DataverseConnection(ConnectionStore,
            new CredentialFactory(SecretStore, TestConsole), invocation, TestConsole);

        await Assert.That(connection.GetServiceAsync(Guid.NewGuid())).Throws<InvalidOperationException>()
            .WithMessageContaining("'Explicit'");
        await Assert.That(TestConsole.Output).Contains(ConnectionTestUrls.Dataverse);
        await Assert.That(TestConsole.Output).DoesNotContain("saved.crm");
    }

    [Test]
    public async Task ConnectionString_TakesPrecedenceOverExplicitAndSavedNames()
    {
        ConnectionStore.Upsert("Saved", new InteractiveConnection { Url = "https://saved.crm.dynamics.com" });
        var invocation = new ConnectionInvocationContext();
        invocation.Capture(new ConnectionSettings
        {
            Connection = "Missing",
            ConnectionString = "AuthType=ClientSecret;ClientSecret=private-connection-secret"
        });
        using var connection = new DataverseConnection(ConnectionStore,
            new CredentialFactory(SecretStore, TestConsole), invocation, TestConsole);

        await Assert.That(connection.GetServiceAsync(Guid.NewGuid())).Throws<NullReferenceException>();
        await Assert.That(TestConsole.Output).Contains("(Connection String) failed");
        await Assert.That(TestConsole.Output).DoesNotContain("private-connection-secret");
        await Assert.That(TestConsole.Output).DoesNotContain("saved.crm");
    }

    [Test]
    public async Task AuthenticationDouble_RejectsCallerAcquisitionExplicitly()
    {
        var connection = new FakeDataverseConnection();
        await Assert.That(async () => await connection.GetServiceAsync(Guid.Empty)).Throws<ArgumentException>();
        await Assert.That(async () => await connection.GetServiceAsync(Guid.NewGuid())).Throws<NotSupportedException>();
    }

    [Test]
    [Arguments(false)]
    [Arguments(true)]
    public async Task GetServiceAsync_MissingClientSecret_ReportsFailureAndPropagatesOriginalError(bool activeDisplay)
    {
        const string name = "private-connection-name";
        ConnectionStore.Upsert(name, new ClientSecretConnection
        {
            Url = ConnectionTestUrls.Dataverse,
            TenantId = "tenant",
            ClientId = "client"
        });
        // TestConsole disables exclusivity; use a real console over its captured output.
        var console = AnsiConsole.Create(new AnsiConsoleSettings
        {
            Out = TestConsole.Profile.Out,
            Ansi = AnsiSupport.Yes,
            Interactive = InteractionSupport.Yes
        });
        console.Profile.Width = 200;
        var invocationContext = new ConnectionInvocationContext();
        invocationContext.Capture(new ConnectionSettings { Connection = name });
        using var connection = new DataverseConnection(
            ConnectionStore,
            new CredentialFactory(SecretStore, console),
            invocationContext,
            console);

        var attempt = activeDisplay
            // ReSharper disable once AccessToDisposedClosure
            ? console.Status().StartAsync("Working...", _ => connection.GetServiceAsync())
            : connection.GetServiceAsync();

        await Assert.That(attempt).Throws<InvalidOperationException>()
            .WithMessageContaining($"Client secret is missing for connection '{name}'");

        await Assert.That(TestConsole.Output)
            .Contains($"Connection to {ConnectionTestUrls.Dataverse} (Client Secret) failed");
        await Assert.That(TestConsole.Output).DoesNotContain(name);
        await Assert.That(TestConsole.Output).DoesNotContain("Client secret is missing");
        await Assert.That(TestConsole.Output).DoesNotContain("Connected to");
    }

    [Test]
    [NotInParallel("SystemAccessToken")]
    public async Task CheckAuth_ReturnsFalseWhenFederatedCredentialCannotBeCreated()
    {
        ConnectionStore.Upsert("Pipeline", new AzureDevOpsFederatedConnection
        {
            Url = ConnectionTestUrls.Dataverse,
            TenantId = "tenant",
            ClientId = "client",
            ServiceConnectionId = "service-connection"
        });
        using var connection = new DataverseConnection(
            ConnectionStore,
            new CredentialFactory(SecretStore, TestConsole),
            new ConnectionInvocationContext(),
            TestConsole);
        var original = Environment.GetEnvironmentVariable("SYSTEM_ACCESSTOKEN");
        Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", null);
        try
        {
            var result = await connection.CheckAuthAsync();

            await Assert.That(result).IsFalse();
        }
        finally
        {
            Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", original);
        }
    }

    [Test]
    public async Task GetServiceAsync_FailedConnect_IsNotCachedAndRetries()
    {
        var invocationContext = new ConnectionInvocationContext();
        using var connection = new DataverseConnection(
            ConnectionStore,
            new CredentialFactory(SecretStore, TestConsole),
            invocationContext,
            TestConsole);

        await Assert.That(connection.GetServiceAsync()).Throws<MissingConnectionException>()
            .WithMessageContaining("No connection is selected");

        invocationContext.Capture(new ConnectionSettings { Connection = "Missing" });

        await Assert.That(connection.GetServiceAsync()).Throws<MissingConnectionException>()
            .WithMessageContaining("'Missing' was not found");
    }

    [Test]
    public async Task GetServiceAsync_InvalidConnectionString_PropagatesOriginalExceptionWithoutWritingSecret()
    {
        const string secret = "do-not-display-this-secret";
        var invocationContext = new ConnectionInvocationContext();
        invocationContext.Capture(new ConnectionSettings
        {
            ConnectionString = $"AuthType=ClientSecret;ClientSecret={secret}"
        });
        using var connection = new DataverseConnection(
            ConnectionStore,
            new CredentialFactory(SecretStore, TestConsole),
            invocationContext,
            TestConsole);

        await Assert.That(connection.GetServiceAsync()).Throws<NullReferenceException>();

        await Assert.That(TestConsole.Output).DoesNotContain(secret);
        await Assert.That(TestConsole.Output).Contains("Connection to Dataverse environment (Connection String) failed");
    }

    [Test]
    public async Task ResolveName_ReturnsPersistedSpelling()
    {
        ConnectionStore.Upsert("Dev", new InteractiveConnection { Url = ConnectionTestUrls.Dataverse });

        await Assert.That(ConnectionStore.ResolveName("dev")).IsEqualTo("Dev");
        await Assert.That(ConnectionStore.ResolveName("other")).IsEqualTo("other");
    }
}
