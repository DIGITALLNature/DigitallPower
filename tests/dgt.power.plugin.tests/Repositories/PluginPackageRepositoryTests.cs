// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.plugin.Repositories;
using Digitall.Dataverse.Testing;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System.Security.Cryptography;

namespace dgt.power.plugin.tests.Repositories;

public class PluginPackageRepositoryTests
{
    private static FakeOrganizationServiceAsync CreateService(IReadOnlyDictionary<Guid, byte[]>? packageFiles = null)
    {
        var service = new FakeOrganizationServiceAsync();
        if (packageFiles is not null)
        {
            service.AddRequests(new InitializePackageFileDownloadExecutor(packageFiles));
            service.AddRequests(new DownloadPackageFileBlockExecutor(packageFiles));
        }

        service.AddDefaultRequests();
        return service;
    }

    [Test]
    public async Task FindByNameAsync_NoMatch_ReturnsNull()
    {
        var service = CreateService();
        var repository = new PluginPackageRepository(service);

        var result = await repository.FindByNameAsync("MyPackage");

        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task FindByNameAsync_MatchesExactName()
    {
        var id = Guid.NewGuid();
        var packageFile = "package-file"u8.ToArray();
        var service = CreateService(new Dictionary<Guid, byte[]> { [id] = packageFile });
        var repository = new PluginPackageRepository(service);
        var package = new PluginPackage(id)
        {
            Name = "new_MyPackage",
            Version = "1.0.0",
            Attributes = { [PluginPackage.LogicalNames.Package] = Guid.NewGuid() }
        };
        service.Create(package);

        var result = await repository.FindByNameAsync("new_MyPackage");

        await Assert.That(result).IsNotNull();
        await Assert.That(result!.Id).IsEqualTo(id);
        await Assert.That(result.PackageHash).IsEqualTo(Convert.ToHexString(SHA256.HashData(packageFile)));
    }

    [Test]
    public async Task FindByNameAsync_DoesNotMatchUnrelatedSuffix()
    {
        var service = CreateService();
        var repository = new PluginPackageRepository(service);
        service.Create(new PluginPackage(Guid.NewGuid()) { Name = "new_OtherMyPackage", Version = "1.0.0" });

        var result = await repository.FindByNameAsync("new_MyPackage");

        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task CreateAsync_CreatesPackageWithGivenNameAndVersion()
    {
        var service = CreateService();
        var repository = new PluginPackageRepository(service);

        var id = await repository.CreateAsync("new_MyPackage", "1.0.0", "base64content");

        var created = service.Retrieve(PluginPackage.EntityLogicalName, id, new ColumnSet(true)).ToEntity<PluginPackage>();
        await Assert.That(created.Name).IsEqualTo("new_MyPackage");
        await Assert.That(created.Version).IsEqualTo("1.0.0");
        await Assert.That(created.Content).IsEqualTo("base64content");
    }

    [Test]
    public async Task UpdateContentAsync_ReplacesContentOnly_VersionUnchanged()
    {
        var service = CreateService();
        var repository = new PluginPackageRepository(service);
        var id = Guid.NewGuid();
        service.Create(new PluginPackage(id) { Name = "new_MyPackage", Version = "1.0.0", Content = "old" });

        await repository.UpdateContentAsync(id, "new");

        var updated = service.Retrieve(PluginPackage.EntityLogicalName, id, new ColumnSet(true)).ToEntity<PluginPackage>();
        await Assert.That(updated.Content).IsEqualTo("new");
        await Assert.That(updated.Version).IsEqualTo("1.0.0");
    }
}
