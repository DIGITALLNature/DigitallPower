// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.plugin.Repositories;
using dgt.power.tests.FakeExecutor;
using Digitall.Dataverse.Testing;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.plugin.tests.Repositories;

public class PluginTypeRepositoryTests
{
    private static FakeOrganizationServiceAsync CreateService()
    {
        var service = new FakeOrganizationServiceAsync();
        service.AddRequests(new RetrieveDependenciesForDeleteExecutor());
        service.AddDefaultRequests();
        return service;
    }

    [Test]
    public async Task ListByAssemblyAsync_NoTypes_ReturnsEmpty()
    {
        var service = CreateService();
        var repository = new PluginTypeRepository(service);

        var result = await repository.ListByAssemblyAsync(Guid.NewGuid());

        await Assert.That(result).IsEmpty();
    }

    [Test]
    public async Task ListByAssemblyAsync_ReturnsOnlyTypesOfThatAssembly()
    {
        var service = CreateService();
        var assemblyId = Guid.NewGuid();
        service.Create(new PluginType(Guid.NewGuid())
        {
            TypeName = "MyPlugin",
            PluginAssemblyId = new EntityReference(PluginAssembly.EntityLogicalName, assemblyId)
        });
        service.Create(new PluginType(Guid.NewGuid())
        {
            TypeName = "OtherPlugin",
            PluginAssemblyId = new EntityReference(PluginAssembly.EntityLogicalName, Guid.NewGuid())
        });
        var repository = new PluginTypeRepository(service);

        var result = await repository.ListByAssemblyAsync(assemblyId);

        await Assert.That(result).Count().IsEqualTo(1);
        await Assert.That(result[0].TypeName).IsEqualTo("MyPlugin");
    }

    [Test]
    public async Task CreateAsync_CreatesPluginType()
    {
        var service = CreateService();
        var assemblyId = Guid.NewGuid();
        var repository = new PluginTypeRepository(service);

        var id = await repository.CreateAsync(assemblyId, "MyPlugin", "MyPlugin");

        var created = service.Retrieve(PluginType.EntityLogicalName, id, new ColumnSet(true)).ToEntity<PluginType>();
        await Assert.That(created.TypeName).IsEqualTo("MyPlugin");
        await Assert.That(created.PluginAssemblyId!.Id).IsEqualTo(assemblyId);
    }

    [Test]
    public async Task GetDependentStepIdsAsync_NoSteps_ReturnsEmpty()
    {
        var service = CreateService();
        var repository = new PluginTypeRepository(service);

        var result = await repository.GetDependentStepIdsAsync(Guid.NewGuid());

        await Assert.That(result).IsEmpty();
    }

    [Test]
    public async Task GetDependentStepIdsAsync_ReturnsStepsBoundToType()
    {
        var service = CreateService();
        var pluginTypeId = Guid.NewGuid();
        var stepId = Guid.NewGuid();
        service.Create(new SdkMessageProcessingStep(stepId)
        {
            EventHandler = new EntityReference(PluginType.EntityLogicalName, pluginTypeId)
        });
        var repository = new PluginTypeRepository(service);

        var result = await repository.GetDependentStepIdsAsync(pluginTypeId);

        await Assert.That(result).IsEquivalentTo([stepId]);
    }

    [Test]
    public async Task DeleteAsync_RemovesPluginType()
    {
        var service = CreateService();
        var id = Guid.NewGuid();
        service.Create(new PluginType(id) { TypeName = "MyPlugin" });
        var repository = new PluginTypeRepository(service);

        await repository.DeleteAsync(id);

        await Assert.That(() => service.Retrieve(PluginType.EntityLogicalName, id, new ColumnSet(true)))
            .Throws<Exception>();
    }
}
