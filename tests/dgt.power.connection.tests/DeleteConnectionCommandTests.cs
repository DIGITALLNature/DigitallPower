// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common.Connections;
using dgt.power.connection.Commands;
using dgt.power.connection.tests.Base;
using Spectre.Console.Cli;

namespace dgt.power.connection.tests;

public class DeleteConnectionCommandTests : ConnectionTestsBase<DeleteConnectionCommand, DeleteConnectionSettings>
{
    [Test]
    public async Task DeletesConnectionAndItsSecret()
    {
        ConnectionStore.Upsert("Dev", new ClientSecretConnection
        {
            Url = "https://contoso.crm.dynamics.com",
            TenantId = "contoso.onmicrosoft.com",
            ClientId = "client-id"
        });
        SecretStore.WriteSecret("Dev", "clientSecret", "secret");
        ICommand<DeleteConnectionSettings> command = new DeleteConnectionCommand(
            ConnectionStore,
            SecretStore,
            UserTokenCache,
            new ConnectionInvocationContext(),
            TestConsole);

        var result = await command.ExecuteAsync(
            CreateContext(),
            new DeleteConnectionSettings { Name = "Dev" },
            CancellationToken.None);

        await Assert.That(result).IsEqualTo(0);
        await Assert.That(ConnectionStore.Find("Dev")).IsNull();
        await Assert.That(SecretStore.ReadSecret("Dev", "clientSecret")).IsNull();
    }

    [Test]
    public async Task DeletesAllConnections()
    {
        ConnectionStore.Upsert("Dev", new InteractiveConnection
        {
            Url = "https://contoso.crm.dynamics.com",
            TenantId = "contoso.onmicrosoft.com"
        });
        ConnectionStore.Upsert("Prod", new InteractiveConnection
        {
            Url = "https://contoso.crm.dynamics.com",
            TenantId = "contoso.onmicrosoft.com"
        }, makeCurrent: false);
        ICommand<DeleteConnectionSettings> command = new DeleteConnectionCommand(
            ConnectionStore,
            SecretStore,
            UserTokenCache,
            new ConnectionInvocationContext(),
            TestConsole);

        var result = await command.ExecuteAsync(
            CreateContext(),
            new DeleteConnectionSettings { All = true, Yes = true },
            CancellationToken.None);

        await Assert.That(result).IsEqualTo(0);
        await Assert.That(ConnectionStore.GetAll()).IsEmpty();
    }

    [Test]
    public async Task DeletingConnectionPreservesTokenUsedByAnotherConnection()
    {
        var authenticationRecord = CreateAuthenticationRecord("shared-home-account");
        ConnectionStore.Upsert("Dev", new InteractiveConnection
        {
            Url = "https://contoso.crm.dynamics.com",
            TenantId = "tenant-id",
            AuthenticationRecord = authenticationRecord
        });
        ConnectionStore.Upsert("Test", new DeviceCodeConnection
        {
            Url = "https://contoso.crm.dynamics.com",
            TenantId = "tenant-id",
            AuthenticationRecord = authenticationRecord
        }, makeCurrent: false);
        ICommand<DeleteConnectionSettings> command = new DeleteConnectionCommand(
            ConnectionStore,
            SecretStore,
            UserTokenCache,
            new ConnectionInvocationContext(),
            TestConsole);

        var result = await command.ExecuteAsync(
            CreateContext(),
            new DeleteConnectionSettings { Name = "Dev" },
            CancellationToken.None);

        await Assert.That(result).IsEqualTo(0);
        await Assert.That(UserTokenCache.RemovedHomeAccountIds).IsEmpty();
        await Assert.That(ConnectionStore.Find("Test")).IsNotNull();
    }

    [Test]
    public async Task DeletingLastConnectionRemovesItsCachedAccount()
    {
        ConnectionStore.Upsert("Dev", new InteractiveConnection
        {
            Url = "https://contoso.crm.dynamics.com",
            TenantId = "tenant-id",
            AuthenticationRecord = CreateAuthenticationRecord("home-account")
        });
        ICommand<DeleteConnectionSettings> command = new DeleteConnectionCommand(
            ConnectionStore,
            SecretStore,
            UserTokenCache,
            new ConnectionInvocationContext(),
            TestConsole);

        var result = await command.ExecuteAsync(
            CreateContext(),
            new DeleteConnectionSettings { Name = "Dev" },
            CancellationToken.None);

        await Assert.That(result).IsEqualTo(0);
        await Assert.That(UserTokenCache.RemovedHomeAccountIds).Contains("home-account");
    }

    private static CommandContext CreateContext() =>
        new(Enumerable.Empty<string>(), new EmptyRemainingArguments(), "delete", null);
}
