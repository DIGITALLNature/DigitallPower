// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.plugin.Repositories;
using Digitall.Dataverse.Testing;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.plugin.tests.Repositories;

public class ManagedIdentityRepositoryTests
{
    private const string ClientId = "12345678-1234-1234-1234-123456789abc";
    private const string TenantId = "aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee";

    private static FakeOrganizationServiceAsync CreateService()
    {
        var service = new FakeOrganizationServiceAsync();
        service.AddDefaultRequests();
        return service;
    }

    [Test]
    public async Task EnsureAsync_NoExistingIdentity_CreatesNewOne()
    {
        var service = CreateService();
        var repository = new ManagedIdentityRepository(service);

        var id = await repository.EnsureAsync(ClientId, TenantId);

        var created = service.Retrieve(ManagedIdentity.EntityLogicalName, id, new ColumnSet(true)).ToEntity<ManagedIdentity>();
        await Assert.That(created.ApplicationId).IsEqualTo(Guid.Parse(ClientId));
        await Assert.That(created.TenantId).IsEqualTo(Guid.Parse(TenantId));
    }

    [Test]
    public async Task EnsureAsync_ExistingIdentity_ReturnsExistingId()
    {
        var service = CreateService();
        var existingId = Guid.NewGuid();
        service.Create(new ManagedIdentity(existingId) { ApplicationId = Guid.Parse(ClientId) });
        var repository = new ManagedIdentityRepository(service);

        var id = await repository.EnsureAsync(ClientId, null);

        await Assert.That(id).IsEqualTo(existingId);
    }

    [Test]
    public async Task LinkToAssemblyAsync_SetsManagedIdentityId()
    {
        var service = CreateService();
        var assemblyId = Guid.NewGuid();
        var managedIdentityId = Guid.NewGuid();
        service.Create(new PluginAssembly(assemblyId) { Name = "MyPlugins" });
        var repository = new ManagedIdentityRepository(service);

        await repository.LinkToAssemblyAsync(assemblyId, managedIdentityId);

        var updated = service.Retrieve(PluginAssembly.EntityLogicalName, assemblyId, new ColumnSet(true)).ToEntity<PluginAssembly>();
        await Assert.That(updated.ManagedIdentityId?.Id).IsEqualTo(managedIdentityId);
    }

    [Test]
    public async Task LinkToPackageAsync_SetsManagedIdentityId()
    {
        var service = CreateService();
        var packageId = Guid.NewGuid();
        var managedIdentityId = Guid.NewGuid();
        service.Create(new PluginPackage(packageId) { Name = "MyPackage" });
        var repository = new ManagedIdentityRepository(service);

        await repository.LinkToPackageAsync(packageId, managedIdentityId);

        var updated = service.Retrieve(PluginPackage.EntityLogicalName, packageId, new ColumnSet(true)).ToEntity<PluginPackage>();
        await Assert.That(updated.Managedidentityid?.Id).IsEqualTo(managedIdentityId);
    }
}
