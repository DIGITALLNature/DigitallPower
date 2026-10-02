// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text;
using dgt.power.common.Connections;
using dgt.power.common.Storage;

namespace dgt.power.connection.tests.Storage;

public class ConnectionStoreTests
{
    [Test]
    public async Task Upsert_RoundTripsTypedConnectionAndPreservesNameCase()
    {
        var directory = CreateTemporaryDirectory();
        try
        {
            var store = new ConnectionStore(new DgtpHome(directory));
            store.Upsert("Dev", new InteractiveConnection
            {
                Url = "https://contoso.crm.dynamics.com",
                TenantId = "contoso.onmicrosoft.com"
            });

            var reloaded = new ConnectionStore(new DgtpHome(directory));
            await Assert.That(reloaded.Current).IsEqualTo("Dev");
            await Assert.That(reloaded.Find("dev")).IsTypeOf<InteractiveConnection>();
            await Assert.That(reloaded.GetAll().Keys.Single()).IsEqualTo("Dev");
        }
        finally
        {
            Directory.Delete(directory, recursive: true);
        }
    }

    [Test]
    public async Task Upsert_ConnectionWithoutSelection_DoesNotReplaceCurrentConnection()
    {
        var directory = CreateTemporaryDirectory();
        try
        {
            var store = new ConnectionStore(new DgtpHome(directory));
            var connection = new InteractiveConnection
            {
                Url = "https://contoso.crm.dynamics.com",
                TenantId = "contoso.onmicrosoft.com"
            };
            store.Upsert("first", connection);
            store.Upsert("second", connection, makeCurrent: false);

            await Assert.That(store.Current).IsEqualTo("first");
        }
        finally
        {
            Directory.Delete(directory, recursive: true);
        }
    }

    [Test]
    public async Task Remove_ClearsCurrentAndSelectsAnotherConnection()
    {
        var directory = CreateTemporaryDirectory();
        try
        {
            var store = new ConnectionStore(new DgtpHome(directory));
            var connection = new InteractiveConnection
            {
                Url = "https://contoso.crm.dynamics.com",
                TenantId = "contoso.onmicrosoft.com"
            };
            store.Upsert("first", connection);
            store.Upsert("second", connection, makeCurrent: false);

            await Assert.That(store.Remove("first")).IsTrue();
            await Assert.That(store.Current).IsEqualTo("second");
        }
        finally
        {
            Directory.Delete(directory, recursive: true);
        }
    }

    [Test]
    public async Task StateStore_PersistsInstallIdAndNoticeStateAcrossInstances()
    {
        var directory = CreateTemporaryDirectory();
        try
        {
            var home = new DgtpHome(directory);
            var first = new StateStore(home);
            var installId = first.GetOrCreateTelemetryInstallId();
            first.MarkTelemetryNoticeShown();

            var second = new StateStore(new DgtpHome(directory));
            await Assert.That(second.GetOrCreateTelemetryInstallId()).IsEqualTo(installId);
            await Assert.That(second.TelemetryNoticeShown).IsTrue();
        }
        finally
        {
            Directory.Delete(directory, recursive: true);
        }
    }

    [Test]
    public async Task SecretStore_ProtectsAndDeletesConnectionSecretsOnWindows()
    {
        if (!OperatingSystem.IsWindows())
        {
            return;
        }

        var directory = CreateTemporaryDirectory();
        const string secret = "a-test-client-secret";
        try
        {
            var store = new SecretStore(directory);
            store.WriteSecret("prod", "clientSecret", secret);

            await Assert.That(store.ReadSecret("prod", "clientSecret")).IsEqualTo(secret);
            await Assert.That(Encoding.UTF8.GetString(File.ReadAllBytes(System.IO.Path.Combine(directory, "secrets.bin")))
                .Contains(secret, StringComparison.Ordinal)).IsFalse();

            store.Delete("prod");
            await Assert.That(store.ReadSecret("prod", "clientSecret")).IsNull();
        }
        finally
        {
            Directory.Delete(directory, recursive: true);
        }
    }

    private static string CreateTemporaryDirectory()
    {
        var directory = System.IO.Path.Combine(System.IO.Path.GetTempPath(), $"dgtp-{Guid.NewGuid():N}");
        Directory.CreateDirectory(directory);
        return directory;
    }
}
