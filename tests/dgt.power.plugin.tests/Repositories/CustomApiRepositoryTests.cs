// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.plugin.Repositories;
using Digitall.Dataverse.Testing;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;

namespace dgt.power.plugin.tests.Repositories;

public class CustomApiRepositoryTests
{
    private static FakeOrganizationServiceAsync CreateService()
    {
        var service = new FakeOrganizationServiceAsync();
        service.AddDefaultRequests();
        return service;
    }

    [Test]
    public async Task FindIdByUniqueNameAsync_NoMatch_ReturnsNull()
    {
        var service = CreateService();
        var repository = new CustomApiRepository(service);

        var result = await repository.FindIdByUniqueNameAsync("new_MyApi");

        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task FindIdByUniqueNameAsync_Match_ReturnsId()
    {
        var service = CreateService();
        var id = Guid.NewGuid();
        service.Create(new CustomAPI(id) { UniqueName = "new_MyApi" });
        var repository = new CustomApiRepository(service);

        var result = await repository.FindIdByUniqueNameAsync("new_MyApi");

        await Assert.That(result).IsEqualTo(id);
    }

    [Test]
    public async Task FindIdByUniqueNameAsync_DuplicateMatches_RejectsAmbiguity()
    {
        var service = CreateService();
        service.Create(new CustomAPI { UniqueName = "new_Api" });
        service.Create(new CustomAPI { UniqueName = "new_Api" });

        await Assert.That(async () => await new CustomApiRepository(service).FindIdByUniqueNameAsync("new_Api")).ThrowsExactly<InvalidOperationException>();
    }

    [Test]
    public async Task FindIdByUniqueNameAsync_MatchesAcrossPages_RejectsAmbiguity()
    {
        var service = new FakeOrganizationServiceAsync();
        var executor = new PagedQueryExecutor();
        service.AddRequests(executor);

        await Assert.That(async () => await new CustomApiRepository(service).FindIdByUniqueNameAsync("new_Api")).ThrowsExactly<InvalidOperationException>();
        await Assert.That(executor.Requests).IsEquivalentTo([(1, (string?)null, 5000), (2, "next-page", 5000)]);
    }

    [Test]
    public async Task ListLinkedToPluginTypeAsync_MultiplePages_ReadsEveryPage()
    {
        var service = new FakeOrganizationServiceAsync();
        var executor = new PagedQueryExecutor();
        service.AddRequests(executor);

        var result = await new CustomApiRepository(service).ListLinkedToPluginTypeAsync(Guid.NewGuid());

        await Assert.That(result).Count().IsEqualTo(2);
        await Assert.That(executor.Requests).IsEquivalentTo([(1, (string?)null, 5000), (2, "next-page", 5000)]);
    }

    [Test]
    public async Task ListLinkedToPluginTypeAsync_ReturnsOnlyLinkedApis()
    {
        var service = CreateService();
        var pluginTypeId = Guid.NewGuid();
        var linkedId = Guid.NewGuid();
        service.Create(new CustomAPI(linkedId) { PluginTypeId = new EntityReference(PluginType.EntityLogicalName, pluginTypeId) });
        service.Create(new CustomAPI(Guid.NewGuid()) { PluginTypeId = new EntityReference(PluginType.EntityLogicalName, Guid.NewGuid()) });
        var repository = new CustomApiRepository(service);

        var result = await repository.ListLinkedToPluginTypeAsync(pluginTypeId);

        await Assert.That(result).IsEquivalentTo([linkedId]);
    }

    [Test]
    public async Task LinkPluginTypeAsync_SetsPluginTypeId()
    {
        var service = CreateService();
        var customApiId = Guid.NewGuid();
        var pluginTypeId = Guid.NewGuid();
        service.Create(new CustomAPI(customApiId));
        var repository = new CustomApiRepository(service);

        await repository.LinkPluginTypeAsync(customApiId, pluginTypeId);

        var updated = service.Retrieve(CustomAPI.EntityLogicalName, customApiId, new ColumnSet(true)).ToEntity<CustomAPI>();
        await Assert.That(updated.PluginTypeId!.Id).IsEqualTo(pluginTypeId);
    }

    [Test]
    public async Task UnlinkPluginTypeAsync_ClearsPluginTypeId()
    {
        var service = CreateService();
        var customApiId = Guid.NewGuid();
        service.Create(new CustomAPI(customApiId) { PluginTypeId = new EntityReference(PluginType.EntityLogicalName, Guid.NewGuid()) });
        var repository = new CustomApiRepository(service);

        await repository.UnlinkPluginTypeAsync(customApiId);

        var updated = service.Retrieve(CustomAPI.EntityLogicalName, customApiId, new ColumnSet(true)).ToEntity<CustomAPI>();
        await Assert.That(updated.PluginTypeId).IsNull();
    }
}
