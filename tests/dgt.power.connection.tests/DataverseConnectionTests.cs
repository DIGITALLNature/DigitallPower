// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common.Connections;
using dgt.power.common.Exceptions;
using dgt.power.common.Logic;
using dgt.power.connection.Base;
using dgt.power.connection.Commands;
using dgt.power.connection.tests.Base;

namespace dgt.power.connection.tests;

public class DataverseConnectionTests : ConnectionTestsBase<ConnectionStatusCommand, ConnectionSettings>
{
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
        var connection = new DataverseConnection(
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
        var connection = new DataverseConnection(
            ConnectionStore,
            new CredentialFactory(SecretStore, TestConsole),
            new ConnectionInvocationContext(),
            TestConsole);

        var first = connection.GetServiceAsync();
        await Assert.ThrowsAsync<MissingConnectionException>(async () => await first);

        var second = connection.GetServiceAsync();

        await Assert.That(second).IsNotSameReferenceAs(first);
        await Assert.ThrowsAsync<MissingConnectionException>(async () => await second);
    }

    [Test]
    public async Task GetServiceAsync_ConcurrentFirstCalls_ShareOneAttempt()
    {
        var connection = new DataverseConnection(
            ConnectionStore,
            new CredentialFactory(SecretStore, TestConsole),
            new ConnectionInvocationContext(),
            TestConsole);

        var first = connection.GetServiceAsync();
        var second = connection.GetServiceAsync();

        await Assert.That(second).IsSameReferenceAs(first);
        await Assert.ThrowsAsync<MissingConnectionException>(async () => await first);
    }

    [Test]
    public async Task ResolveName_ReturnsPersistedSpelling()
    {
        ConnectionStore.Upsert("Dev", new InteractiveConnection { Url = ConnectionTestUrls.Dataverse });

        await Assert.That(ConnectionStore.ResolveName("dev")).IsEqualTo("Dev");
        await Assert.That(ConnectionStore.ResolveName("other")).IsEqualTo("other");
    }
}
