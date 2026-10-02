// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common.Connections;
using dgt.power.connection.Commands;
using dgt.power.connection.tests.Base;
using Spectre.Console.Cli;

namespace dgt.power.connection.tests;

public class ConnectionLogoutCommandTests : ConnectionTestsBase<ConnectionLogoutCommand, NamedConnectionSettings>
{
    [Test]
    public async Task LogoutRemovesCachedAccountWithoutDeletingConnection()
    {
        ConnectionStore.Upsert("Dev", new InteractiveConnection
        {
            Url = "https://contoso.crm.dynamics.com",
            TenantId = "tenant-id",
            AuthenticationRecord = CreateAuthenticationRecord("home-account")
        });
        ICommand<NamedConnectionSettings> command = new ConnectionLogoutCommand(
            ConnectionStore,
            UserTokenCache,
            ConnectionInvocationOptions.FromArguments([]),
            TestConsole);

        var result = await command.ExecuteAsync(
            CreateContext(),
            new NamedConnectionSettings { Name = "Dev" },
            CancellationToken.None);

        await Assert.That(result).IsEqualTo(0);
        await Assert.That(UserTokenCache.RemovedHomeAccountIds).Contains("home-account");
        await Assert.That(ConnectionStore.Find("Dev")).IsNotNull();
    }

    [Test]
    public async Task LogoutReportsMissingConnection()
    {
        ICommand<NamedConnectionSettings> command = new ConnectionLogoutCommand(
            ConnectionStore,
            UserTokenCache,
            ConnectionInvocationOptions.FromArguments([]),
            TestConsole);

        var result = await command.ExecuteAsync(
            CreateContext(),
            new NamedConnectionSettings { Name = "missing" },
            CancellationToken.None);

        await Assert.That(result).IsNotEqualTo(0);
        await Assert.That(UserTokenCache.RemovedHomeAccountIds).IsEmpty();
    }

    private static CommandContext CreateContext() =>
        new(Enumerable.Empty<string>(), new EmptyRemainingArguments(), "logout", null);
}
