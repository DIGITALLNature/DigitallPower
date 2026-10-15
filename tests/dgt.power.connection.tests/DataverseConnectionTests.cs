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
        await Assert.That(await Fails(first)).IsTrue();

        var second = connection.GetServiceAsync();

        await Assert.That(ReferenceEquals(second, first)).IsFalse();
        await Assert.That(await Fails(second)).IsTrue();
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

        await Assert.That(ReferenceEquals(second, first)).IsTrue();
        await Assert.That(await Fails(first)).IsTrue();
    }

    private static async Task<bool> Fails(Task task)
    {
        await Task.WhenAny(task);
        return task.IsFaulted && task.Exception.InnerException is MissingConnectionException;
    }

    [Test]
    public async Task ResolveName_ReturnsPersistedSpelling()
    {
        ConnectionStore.Upsert("Dev", new InteractiveConnection { Url = ConnectionTestUrls.Dataverse });

        await Assert.That(ConnectionStore.ResolveName("dev")).IsEqualTo("Dev");
        await Assert.That(ConnectionStore.ResolveName("other")).IsEqualTo("other");
    }
}
