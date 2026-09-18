// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.plugin.Dataverse;
using Digitall.Dataverse.Testing;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.plugin.tests.Dataverse;

public class SdkMessageProcessingStepImageRepositoryTests
{
    private static FakeOrganizationServiceAsync CreateService()
    {
        var service = new FakeOrganizationServiceAsync();
        service.AddDefaultRequests();
        return service;
    }

    [Test]
    public async Task ListByStepAsync_NoImages_ReturnsEmpty()
    {
        var service = CreateService();
        var repository = new SdkMessageProcessingStepImageRepository(service);

        var result = await repository.ListByStepAsync(Guid.NewGuid());

        await Assert.That(result).IsEmpty();
    }

    [Test]
    public async Task ListByStepAsync_ReturnsImagesOfThatStep()
    {
        var service = CreateService();
        var stepId = Guid.NewGuid();
        service.Create(new SdkMessageProcessingStepImage(Guid.NewGuid())
        {
            Name = "PreImage",
            ImageType = new OptionSetValue(SdkMessageProcessingStepImage.Options.ImageType.PreImage),
            AttributesField = "name,telephone1",
            SdkMessageProcessingStepId = new EntityReference(SdkMessageProcessingStep.EntityLogicalName, stepId)
        });
        service.Create(new SdkMessageProcessingStepImage(Guid.NewGuid())
        {
            Name = "OtherImage",
            SdkMessageProcessingStepId = new EntityReference(SdkMessageProcessingStep.EntityLogicalName, Guid.NewGuid())
        });
        var repository = new SdkMessageProcessingStepImageRepository(service);

        var result = await repository.ListByStepAsync(stepId);

        await Assert.That(result).Count().IsEqualTo(1);
        await Assert.That(result[0].Name).IsEqualTo("PreImage");
        await Assert.That(result[0].ImageType).IsEqualTo(SdkMessageProcessingStepImage.Options.ImageType.PreImage);
        await Assert.That(result[0].Attributes).IsEquivalentTo(["name", "telephone1"]);
    }

    [Test]
    public async Task ListByStepAsync_NoAttributes_ReturnsNull()
    {
        var service = CreateService();
        var stepId = Guid.NewGuid();
        service.Create(new SdkMessageProcessingStepImage(Guid.NewGuid())
        {
            Name = "PreImage",
            ImageType = new OptionSetValue(SdkMessageProcessingStepImage.Options.ImageType.PreImage),
            SdkMessageProcessingStepId = new EntityReference(SdkMessageProcessingStep.EntityLogicalName, stepId)
        });
        var repository = new SdkMessageProcessingStepImageRepository(service);

        var result = await repository.ListByStepAsync(stepId);

        await Assert.That(result[0].Attributes).IsNull();
    }

    [Test]
    public async Task CreateAsync_CreatesImageWithAllFields()
    {
        var service = CreateService();
        var repository = new SdkMessageProcessingStepImageRepository(service);
        var stepId = Guid.NewGuid();
        var data = new PluginStepImageData(
            stepId, "PreImage", "PreImage", SdkMessageProcessingStepImage.Options.ImageType.PreImage, "Target", ["name"]);

        var id = await repository.CreateAsync(data);

        var created = service.Retrieve(SdkMessageProcessingStepImage.EntityLogicalName, id, new ColumnSet(true))
            .ToEntity<SdkMessageProcessingStepImage>();
        await Assert.That(created.Name).IsEqualTo("PreImage");
        await Assert.That(created.EntityAlias).IsEqualTo("PreImage");
        await Assert.That(created.MessagePropertyName).IsEqualTo("Target");
        await Assert.That(created.AttributesField).IsEqualTo("name");
        await Assert.That(created.SdkMessageProcessingStepId!.Id).IsEqualTo(stepId);
    }

    [Test]
    public async Task UpdateAsync_ReplacesAttributes()
    {
        var service = CreateService();
        var id = Guid.NewGuid();
        service.Create(new SdkMessageProcessingStepImage(id) { Name = "PreImage", AttributesField = "old" });
        var repository = new SdkMessageProcessingStepImageRepository(service);

        await repository.UpdateAsync(id, ["new1", "new2"]);

        var updated = service.Retrieve(SdkMessageProcessingStepImage.EntityLogicalName, id, new ColumnSet(true))
            .ToEntity<SdkMessageProcessingStepImage>();
        await Assert.That(updated.AttributesField).IsEqualTo("new1,new2");
        await Assert.That(updated.Name).IsEqualTo("PreImage");
    }

    [Test]
    public async Task UpdateAsync_NullAttributes_ClearsField()
    {
        var service = CreateService();
        var id = Guid.NewGuid();
        service.Create(new SdkMessageProcessingStepImage(id) { AttributesField = "old" });
        var repository = new SdkMessageProcessingStepImageRepository(service);

        await repository.UpdateAsync(id, null);

        var updated = service.Retrieve(SdkMessageProcessingStepImage.EntityLogicalName, id, new ColumnSet(true))
            .ToEntity<SdkMessageProcessingStepImage>();
        await Assert.That(updated.AttributesField).IsNull();
    }

    [Test]
    public async Task DeleteAsync_RemovesImage()
    {
        var service = CreateService();
        var id = Guid.NewGuid();
        service.Create(new SdkMessageProcessingStepImage(id) { Name = "PreImage" });
        var repository = new SdkMessageProcessingStepImageRepository(service);

        await repository.DeleteAsync(id);

        await Assert.That(() => service.Retrieve(SdkMessageProcessingStepImage.EntityLogicalName, id, new ColumnSet(true)))
            .Throws<Exception>();
    }
}
