// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.plugin.Repositories;
using dgt.power.plugin.Execution;
using dgt.power.plugin.Local;
using dgt.power.tests.FakeExecutor;
using Digitall.Dataverse.Testing;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Spectre.Console.Testing;

namespace dgt.power.plugin.tests.Execution;

public class PluginTypeReconcilerTests
{
    private static (FakeOrganizationServiceAsync Service, PluginTypeReconciler Reconciler, TestConsole Console) CreateReconcilerWithConsole()
    {
        var service = new FakeOrganizationServiceAsync();
        service.AddRequests(new RetrieveDependenciesForDeleteExecutor());
        service.AddDefaultRequests();

        var console = new TestConsole();
        var reconciler = new PluginTypeReconciler(
            new PluginTypeRepository(service),
            new SdkMessageProcessingStepRepository(service),
            new SdkMessageProcessingStepImageRepository(service),
            new SdkMessageRepository(service),
            new CustomApiRepository(service),
            console);

        return (service, reconciler, console);
    }

    private static (FakeOrganizationServiceAsync Service, PluginTypeReconciler Reconciler) CreateReconciler()
    {
        var service = new FakeOrganizationServiceAsync();
        service.AddRequests(new RetrieveDependenciesForDeleteExecutor());
        service.AddDefaultRequests();

        var reconciler = new PluginTypeReconciler(
            new PluginTypeRepository(service),
            new SdkMessageProcessingStepRepository(service),
            new SdkMessageProcessingStepImageRepository(service),
            new SdkMessageRepository(service),
            new CustomApiRepository(service),
            new TestConsole());

        return (service, reconciler);
    }

    private static Guid SeedMessage(FakeOrganizationServiceAsync service, string name, string? primaryEntityName = null)
    {
        var messageId = Guid.NewGuid();
        service.Create(new SdkMessage(messageId) { Name = name });

        if (primaryEntityName is not null)
        {
            var filter = new SdkMessageFilter(Guid.NewGuid()) { SdkMessageId = new EntityReference(SdkMessage.EntityLogicalName, messageId) };
            filter.Attributes[SdkMessageFilter.LogicalNames.PrimaryObjectTypeCode] = primaryEntityName;
            service.Create(filter);
        }

        return messageId;
    }

    private static LocalPluginStep Step(
        string name = "step", string messageName = "Create", string primaryEntityName = "account",
        int? executionOrder = 1, IReadOnlyList<LocalPluginStepImage>? images = null) =>
        new(name, SdkMessageProcessingStep.Options.Mode.Synchronous, messageName,
            SdkMessageProcessingStep.Options.Stage.PostOperation, primaryEntityName, "none", null, executionOrder, null,
            images ?? []);

    [Test]
    public async Task ReconcileAsync_NewTypeWithStep_CreatesTypeAndStep()
    {
        var (service, reconciler) = CreateReconciler();
        SeedMessage(service, "Create", "account");
        var assemblyId = Guid.NewGuid();
        var localType = new LocalPluginType("MyPlugin", "MyPlugin", string.Empty, true, [Step()]);

        await reconciler.ReconcileAsync(assemblyId, [localType], new PluginPushOptions(null, DryRun: false));

        var types = service.RetrieveMultiple(new QueryExpression(PluginType.EntityLogicalName) { ColumnSet = new ColumnSet(true) }).Entities;
        await Assert.That(types.Count).IsEqualTo(1);
        var typeEntity = types[0].ToEntity<PluginType>();
        await Assert.That(typeEntity.TypeName).IsEqualTo("MyPlugin");

        var steps = service.RetrieveMultiple(new QueryExpression(SdkMessageProcessingStep.EntityLogicalName) { ColumnSet = new ColumnSet(true) }).Entities;
        await Assert.That(steps.Count).IsEqualTo(1);
        await Assert.That(steps[0].ToEntity<SdkMessageProcessingStep>().Name).IsEqualTo("step");
    }

    [Test]
    public async Task ReconcileAsync_UnresolvedMessage_Throws()
    {
        var (_, reconciler) = CreateReconciler();
        var localType = new LocalPluginType("MyPlugin", "MyPlugin", string.Empty, true, [Step(messageName: "DoesNotExist")]);

        await Assert.That(() => reconciler.ReconcileAsync(Guid.NewGuid(), [localType], new PluginPushOptions(null, DryRun: false)))
            .Throws<UnresolvedPluginStepMessageException>();
    }

    [Test]
    public async Task ReconcileAsync_DryRun_CreatesNothing()
    {
        var (service, reconciler) = CreateReconciler();
        SeedMessage(service, "Create", "account");
        var localType = new LocalPluginType("MyPlugin", "MyPlugin", string.Empty, true, [Step()]);

        await reconciler.ReconcileAsync(Guid.NewGuid(), [localType], new PluginPushOptions(null, DryRun: true));

        var types = service.RetrieveMultiple(new QueryExpression(PluginType.EntityLogicalName) { ColumnSet = new ColumnSet(true) }).Entities;
        await Assert.That(types.Count).IsEqualTo(0);
    }

    [Test]
    public async Task ReconcileAsync_DryRun_PreviewsStepsAndImagesForBrandNewType()
    {
        var (service, reconciler, console) = CreateReconcilerWithConsole();
        SeedMessage(service, "Update", "account");
        var image = new LocalPluginStepImage(SdkMessageProcessingStepImage.Options.ImageType.PreImage, "PreImage", "PreImage", "Target", null);
        var localType = new LocalPluginType("MyPlugin", "MyPlugin", string.Empty, true,
            [Step(messageName: "Update", images: [image])]);

        // Even for a brand-new (never-before-registered) assembly/type, dry-run must still preview
        // every step/image declared on it - not just the type itself - since it can't rely on a real
        // Dataverse id existing yet to reconcile against.
        await reconciler.ReconcileAsync(Guid.NewGuid(), [localType], new PluginPushOptions(null, DryRun: true));

        using (Assert.Multiple())
        {
            await Assert.That(console.Output).Contains("Create PluginType");
            await Assert.That(console.Output).Contains("Create Step");
            await Assert.That(console.Output).Contains("Create Image");

            var steps = service.RetrieveMultiple(new QueryExpression(SdkMessageProcessingStep.EntityLogicalName) { ColumnSet = new ColumnSet(true) }).Entities;
            await Assert.That(steps.Count).IsEqualTo(0);
            var images = service.RetrieveMultiple(new QueryExpression(SdkMessageProcessingStepImage.EntityLogicalName) { ColumnSet = new ColumnSet(true) }).Entities;
            await Assert.That(images.Count).IsEqualTo(0);
        }
    }

    [Test]
    public async Task ReconcileAsync_DryRun_ReportsUnchangedItems()
    {
        var (service, reconciler, console) = CreateReconcilerWithConsole();
        var messageId = Guid.NewGuid();
        service.Create(new SdkMessage(messageId) { Name = "Create" });
        var filterId = Guid.NewGuid();
        var filter = new SdkMessageFilter(filterId)
        {
            SdkMessageId = new EntityReference(SdkMessage.EntityLogicalName, messageId)
        };
        filter.Attributes[SdkMessageFilter.LogicalNames.PrimaryObjectTypeCode] = "account";
        service.Create(filter);
        var assemblyId = Guid.NewGuid();
        var typeId = Guid.NewGuid();
        service.Create(new PluginType(typeId)
        {
            TypeName = "MyPlugin",
            PluginAssemblyId = new EntityReference(PluginAssembly.EntityLogicalName, assemblyId)
        });
        service.Create(new SdkMessageProcessingStep(Guid.NewGuid())
        {
            Name = "step",
            EventHandler = new EntityReference(PluginType.EntityLogicalName, typeId),
            SdkMessageId = new EntityReference(SdkMessage.EntityLogicalName, messageId),
            Mode = new OptionSetValue(SdkMessageProcessingStep.Options.Mode.Synchronous),
            Stage = new OptionSetValue(SdkMessageProcessingStep.Options.Stage.PostOperation),
            Rank = 1,
            SdkMessageFilterId = new EntityReference(SdkMessageFilter.EntityLogicalName, filterId)
        });

        await reconciler.ReconcileAsync(
            assemblyId,
            [new LocalPluginType("MyPlugin", "MyPlugin", string.Empty, true, [Step()])],
            new PluginPushOptions(null, DryRun: true));

        using (Assert.Multiple())
        {
            await Assert.That(console.Output).Contains("Checked 1 plugin type(s): 0 new, 1 existing, 0 removed");
            await Assert.That(console.Output).Contains("Steps: 0 created, 0 updated, 1 unchanged, 0 deleted");
        }
    }

    [Test]
    public async Task ReconcileAsync_ExistingStepWithChangedOrder_Updates()
    {
        var (service, reconciler) = CreateReconciler();
        var messageId = SeedMessage(service, "Create", "account");
        var assemblyId = Guid.NewGuid();
        var typeId = Guid.NewGuid();
        service.Create(new PluginType(typeId)
        {
            TypeName = "MyPlugin",
            PluginAssemblyId = new EntityReference(PluginAssembly.EntityLogicalName, assemblyId)
        });
        service.Create(new SdkMessageProcessingStep(Guid.NewGuid())
        {
            Name = "step",
            EventHandler = new EntityReference(PluginType.EntityLogicalName, typeId),
            SdkMessageId = new EntityReference(SdkMessage.EntityLogicalName, messageId),
            Mode = new OptionSetValue(SdkMessageProcessingStep.Options.Mode.Synchronous),
            Stage = new OptionSetValue(SdkMessageProcessingStep.Options.Stage.PostOperation),
            Rank = 1
        });
        var localType = new LocalPluginType("MyPlugin", "MyPlugin", string.Empty, true, [Step(executionOrder: 42)]);

        await reconciler.ReconcileAsync(assemblyId, [localType], new PluginPushOptions(null, DryRun: false));

        var steps = service.RetrieveMultiple(new QueryExpression(SdkMessageProcessingStep.EntityLogicalName) { ColumnSet = new ColumnSet(true) }).Entities;
        await Assert.That(steps.Count).IsEqualTo(1);
        await Assert.That(steps[0].ToEntity<SdkMessageProcessingStep>().Rank).IsEqualTo(42);
    }

    [Test]
    public async Task ReconcileAsync_OrphanedRemoteType_DeletesTypeAndDependentSteps()
    {
        var (service, reconciler) = CreateReconciler();
        var assemblyId = Guid.NewGuid();
        var orphanTypeId = Guid.NewGuid();
        service.Create(new PluginType(orphanTypeId)
        {
            TypeName = "Orphaned",
            PluginAssemblyId = new EntityReference(PluginAssembly.EntityLogicalName, assemblyId)
        });
        var orphanStepId = Guid.NewGuid();
        service.Create(new SdkMessageProcessingStep(orphanStepId)
        {
            Name = "orphan-step",
            EventHandler = new EntityReference(PluginType.EntityLogicalName, orphanTypeId)
        });

        await reconciler.ReconcileAsync(assemblyId, [], new PluginPushOptions(null, DryRun: false));

        var remainingTypes = service.RetrieveMultiple(new QueryExpression(PluginType.EntityLogicalName) { ColumnSet = new ColumnSet(true) }).Entities;
        await Assert.That(remainingTypes.Count).IsEqualTo(0);
        var remainingSteps = service.RetrieveMultiple(new QueryExpression(SdkMessageProcessingStep.EntityLogicalName) { ColumnSet = new ColumnSet(true) }).Entities;
        await Assert.That(remainingSteps.Count).IsEqualTo(0);
    }

    [Test]
    public async Task ReconcileAsync_CustomApiHandler_LinksMatchingCustomApiAndSkipsSteps()
    {
        var (service, reconciler) = CreateReconciler();
        var assemblyId = Guid.NewGuid();
        var customApiId = Guid.NewGuid();
        service.Create(new CustomAPI(customApiId) { UniqueName = "new_MyApi" });
        var localType = new LocalPluginType("MyPlugin", "MyPlugin", "new_MyApi", true, []);

        await reconciler.ReconcileAsync(assemblyId, [localType], new PluginPushOptions(null, DryRun: false));

        var customApi = service.Retrieve(CustomAPI.EntityLogicalName, customApiId, new ColumnSet(true)).ToEntity<CustomAPI>();
        var types = service.RetrieveMultiple(new QueryExpression(PluginType.EntityLogicalName) { ColumnSet = new ColumnSet(true) }).Entities;
        await Assert.That(types.Count).IsEqualTo(1);
        await Assert.That(customApi.PluginTypeId!.Id).IsEqualTo(types[0].Id);

        var steps = service.RetrieveMultiple(new QueryExpression(SdkMessageProcessingStep.EntityLogicalName) { ColumnSet = new ColumnSet(true) }).Entities;
        await Assert.That(steps.Count).IsEqualTo(0);
    }

    [Test]
    public async Task ReconcileAsync_StepWithPreImage_CreatesImage()
    {
        var (service, reconciler) = CreateReconciler();
        SeedMessage(service, "Update", "account");
        var assemblyId = Guid.NewGuid();
        var image = new LocalPluginStepImage(SdkMessageProcessingStepImage.Options.ImageType.PreImage, "PreImage", "PreImage", "Target", null);
        var localType = new LocalPluginType("MyPlugin", "MyPlugin", string.Empty, true,
            [Step(messageName: "Update", images: [image])]);

        await reconciler.ReconcileAsync(assemblyId, [localType], new PluginPushOptions(null, DryRun: false));

        var images = service.RetrieveMultiple(new QueryExpression(SdkMessageProcessingStepImage.EntityLogicalName) { ColumnSet = new ColumnSet(true) }).Entities;
        await Assert.That(images.Count).IsEqualTo(1);
        await Assert.That(images[0].ToEntity<SdkMessageProcessingStepImage>().Name).IsEqualTo("PreImage");
    }
}
