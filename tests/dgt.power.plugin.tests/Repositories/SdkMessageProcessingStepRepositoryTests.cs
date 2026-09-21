// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.plugin.Repositories;
using Digitall.Dataverse.Testing;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.plugin.tests.Repositories;

public class SdkMessageProcessingStepRepositoryTests
{
    private static FakeOrganizationServiceAsync CreateService()
    {
        var service = new FakeOrganizationServiceAsync();
        service.AddDefaultRequests();
        return service;
    }

    [Test]
    public async Task ListByPluginTypeAsync_NoSteps_ReturnsEmpty()
    {
        var service = CreateService();
        var repository = new SdkMessageProcessingStepRepository(service);

        var result = await repository.ListByPluginTypeAsync(Guid.NewGuid());

        await Assert.That(result).IsEmpty();
    }

    [Test]
    public async Task ListByPluginTypeAsync_ReturnsStepWithResolvedMessageAndFilter()
    {
        var service = CreateService();
        var pluginTypeId = Guid.NewGuid();
        var messageId = Guid.NewGuid();
        var filterId = Guid.NewGuid();
        service.Create(new SdkMessage(messageId) { Name = "Create" });
        service.Create(new SdkMessageFilter(filterId)
        {
            Attributes = { [SdkMessageFilter.LogicalNames.PrimaryObjectTypeCode] = "account" }
        });
        service.Create(new SdkMessageProcessingStep(Guid.NewGuid())
        {
            Name = "step",
            EventHandler = new EntityReference(PluginType.EntityLogicalName, pluginTypeId),
            SdkMessageId = new EntityReference(SdkMessage.EntityLogicalName, messageId),
            SdkMessageFilterId = new EntityReference(SdkMessageFilter.EntityLogicalName, filterId),
            Mode = new OptionSetValue(SdkMessageProcessingStep.Options.Mode.Synchronous),
            Stage = new OptionSetValue(SdkMessageProcessingStep.Options.Stage.PostOperation),
            Rank = 5,
            FilteringAttributesField = "name,telephone1"
        });
        var repository = new SdkMessageProcessingStepRepository(service);

        var result = await repository.ListByPluginTypeAsync(pluginTypeId);

        await Assert.That(result).Count().IsEqualTo(1);
        var step = result[0];
        await Assert.That(step.Name).IsEqualTo("step");
        await Assert.That(step.MessageName).IsEqualTo("Create");
        await Assert.That(step.PrimaryEntityName).IsEqualTo("account");
        await Assert.That(step.Mode).IsEqualTo(SdkMessageProcessingStep.Options.Mode.Synchronous);
        await Assert.That(step.Stage).IsEqualTo(SdkMessageProcessingStep.Options.Stage.PostOperation);
        await Assert.That(step.ExecutionOrder).IsEqualTo(5);
        await Assert.That(step.FilterAttributes).IsEquivalentTo(["name", "telephone1"]);
    }

    [Test]
    public async Task ListByPluginTypeAsync_GlobalMessage_ReturnsNoneEntityName()
    {
        var service = CreateService();
        var pluginTypeId = Guid.NewGuid();
        var messageId = Guid.NewGuid();
        service.Create(new SdkMessage(messageId) { Name = "WhoAmI" });
        service.Create(new SdkMessageProcessingStep(Guid.NewGuid())
        {
            Name = "step",
            EventHandler = new EntityReference(PluginType.EntityLogicalName, pluginTypeId),
            SdkMessageId = new EntityReference(SdkMessage.EntityLogicalName, messageId),
            Mode = new OptionSetValue(SdkMessageProcessingStep.Options.Mode.Synchronous),
            Stage = new OptionSetValue(SdkMessageProcessingStep.Options.Stage.PostOperation)
        });
        var repository = new SdkMessageProcessingStepRepository(service);

        var result = await repository.ListByPluginTypeAsync(pluginTypeId);

        await Assert.That(result[0].PrimaryEntityName).IsEqualTo("none");
    }

    [Test]
    public async Task CreateAsync_CreatesStepWithAllFields()
    {
        var service = CreateService();
        var repository = new SdkMessageProcessingStepRepository(service);
        var pluginTypeId = Guid.NewGuid();
        var messageId = Guid.NewGuid();
        var filterId = Guid.NewGuid();
        var data = new PluginStepData(
            "step", pluginTypeId, messageId, filterId,
            SdkMessageProcessingStep.Options.Stage.PostOperation, SdkMessageProcessingStep.Options.Mode.Asynchronous,
            10, ["name"], "config");

        var id = await repository.CreateAsync(data);

        var created = service.Retrieve(SdkMessageProcessingStep.EntityLogicalName, id, new ColumnSet(true)).ToEntity<SdkMessageProcessingStep>();
        await Assert.That(created.Name).IsEqualTo("step");
        await Assert.That(created.EventHandler!.Id).IsEqualTo(pluginTypeId);
        await Assert.That(created.SdkMessageId!.Id).IsEqualTo(messageId);
        await Assert.That(created.SdkMessageFilterId!.Id).IsEqualTo(filterId);
        await Assert.That(created.Rank).IsEqualTo(10);
        await Assert.That(created.AsyncAutoDelete).IsTrue();
        await Assert.That(created.FilteringAttributesField).IsEqualTo("name");
        await Assert.That(created.Configuration).IsEqualTo("config");
    }

    [Test]
    public async Task CreateAsync_NoMessageFilter_LeavesFilterIdEmpty()
    {
        var service = CreateService();
        var repository = new SdkMessageProcessingStepRepository(service);
        var data = new PluginStepData(
            "step", Guid.NewGuid(), Guid.NewGuid(), null,
            SdkMessageProcessingStep.Options.Stage.PostOperation, SdkMessageProcessingStep.Options.Mode.Synchronous,
            null, null, null);

        var id = await repository.CreateAsync(data);

        var created = service.Retrieve(SdkMessageProcessingStep.EntityLogicalName, id, new ColumnSet(true)).ToEntity<SdkMessageProcessingStep>();
        await Assert.That(created.SdkMessageFilterId).IsNull();
        await Assert.That(created.Rank).IsEqualTo(1);
        await Assert.That(created.AsyncAutoDelete).IsFalse();
    }

    [Test]
    public async Task UpdateAsync_UpdatesMutableFields()
    {
        var service = CreateService();
        var repository = new SdkMessageProcessingStepRepository(service);
        var id = Guid.NewGuid();
        service.Create(new SdkMessageProcessingStep(id) { Name = "old" });
        var data = new PluginStepData(
            "new", Guid.NewGuid(), Guid.NewGuid(), null,
            SdkMessageProcessingStep.Options.Stage.PostOperation, SdkMessageProcessingStep.Options.Mode.Synchronous,
            3, ["a", "b"], "cfg");

        await repository.UpdateAsync(id, data);

        var updated = service.Retrieve(SdkMessageProcessingStep.EntityLogicalName, id, new ColumnSet(true)).ToEntity<SdkMessageProcessingStep>();
        await Assert.That(updated.Name).IsEqualTo("new");
        await Assert.That(updated.FilteringAttributesField).IsEqualTo("a,b");
        await Assert.That(updated.Configuration).IsEqualTo("cfg");
    }

    [Test]
    public async Task DeleteAsync_RemovesStep()
    {
        var service = CreateService();
        var id = Guid.NewGuid();
        service.Create(new SdkMessageProcessingStep(id) { Name = "step" });
        var repository = new SdkMessageProcessingStepRepository(service);

        await repository.DeleteAsync(id);

        await Assert.That(() => service.Retrieve(SdkMessageProcessingStep.EntityLogicalName, id, new ColumnSet(true)))
            .Throws<Exception>();
    }

    [Test]
    public async Task ReassignPluginTypeAsync_UpdatesEventHandlerOnly()
    {
        var service = CreateService();
        var id = Guid.NewGuid();
        var oldTypeId = Guid.NewGuid();
        var newTypeId = Guid.NewGuid();
        service.Create(new SdkMessageProcessingStep(id)
        {
            Name = "step",
            EventHandler = new EntityReference(PluginType.EntityLogicalName, oldTypeId)
        });
        var repository = new SdkMessageProcessingStepRepository(service);

        await repository.ReassignPluginTypeAsync(id, newTypeId);

        var updated = service.Retrieve(SdkMessageProcessingStep.EntityLogicalName, id, new ColumnSet(true)).ToEntity<SdkMessageProcessingStep>();
        await Assert.That(updated.EventHandler!.Id).IsEqualTo(newTypeId);
        await Assert.That(updated.Name).IsEqualTo("step");
    }
}
