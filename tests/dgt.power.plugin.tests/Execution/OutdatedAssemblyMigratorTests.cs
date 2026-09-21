// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.plugin.Dataverse;
using dgt.power.plugin.Execution;
using dgt.power.plugin.Local;
using dgt.power.tests.FakeExecutor;
using Digitall.Dataverse.Testing;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Spectre.Console.Testing;

namespace dgt.power.plugin.tests.Execution;

public class OutdatedAssemblyMigratorTests
{
    private static (FakeOrganizationServiceAsync Service, OutdatedAssemblyMigrator Migrator) CreateMigrator()
    {
        var service = new FakeOrganizationServiceAsync();
        service.AddRequests(new RetrieveDependenciesForDeleteExecutor());
        service.AddDefaultRequests();

        var migrator = new OutdatedAssemblyMigrator(
            new PluginAssemblyRepository(service),
            new PluginTypeRepository(service),
            new SdkMessageProcessingStepRepository(service),
            new CustomApiRepository(service),
            new TestConsole());

        return (service, migrator);
    }

    private static Guid CreateAssembly(FakeOrganizationServiceAsync service, string name, string version)
    {
        var id = Guid.NewGuid();
        service.Create(new PluginAssembly(id)
        {
            Name = name,
            Version = version,
            SourceType = new OptionSetValue(PluginAssembly.Options.SourceType.Database),
            IsolationMode = new OptionSetValue(PluginAssembly.Options.IsolationMode.Sandbox)
        });
        return id;
    }

    private static Guid CreateType(FakeOrganizationServiceAsync service, Guid assemblyId, string typeName)
    {
        var id = Guid.NewGuid();
        service.Create(new PluginType(id)
        {
            TypeName = typeName,
            PluginAssemblyId = new EntityReference(PluginAssembly.EntityLogicalName, assemblyId)
        });
        return id;
    }

    private static Guid SeedMessage(FakeOrganizationServiceAsync service, string name)
    {
        var messageId = Guid.NewGuid();
        service.Create(new SdkMessage(messageId) { Name = name });
        return messageId;
    }

    [Test]
    public async Task MigrateAsync_NoOutdatedAssemblies_DoesNothing()
    {
        var (service, migrator) = CreateMigrator();
        var newAssemblyId = CreateAssembly(service, "MyPlugins", "2.0.0.0");

        await Assert.That(async () =>
                await migrator.MigrateAsync("MyPlugins", newAssemblyId, [], new PluginPushOptions(null, DryRun: false)))
            .ThrowsNothing();
    }

    [Test]
    public async Task MigrateAsync_MatchingType_MigratesCustomApiLinkAndStepsAndDeletesOldAssembly()
    {
        var (service, migrator) = CreateMigrator();
        var oldAssemblyId = CreateAssembly(service, "MyPlugins", "1.0.0.0");
        var oldTypeId = CreateType(service, oldAssemblyId, "MyPlugin");
        var newAssemblyId = CreateAssembly(service, "MyPlugins", "2.0.0.0");
        var newTypeId = CreateType(service, newAssemblyId, "MyPlugin");
        var customApiId = Guid.NewGuid();
        service.Create(new CustomAPI(customApiId) { PluginTypeId = new EntityReference(PluginType.EntityLogicalName, oldTypeId) });
        var messageId = SeedMessage(service, "Create");
        var stepId = Guid.NewGuid();
        service.Create(new SdkMessageProcessingStep(stepId)
        {
            Name = "step",
            EventHandler = new EntityReference(PluginType.EntityLogicalName, oldTypeId),
            SdkMessageId = new EntityReference(SdkMessage.EntityLogicalName, messageId),
            Mode = new OptionSetValue(SdkMessageProcessingStep.Options.Mode.Synchronous),
            Stage = new OptionSetValue(SdkMessageProcessingStep.Options.Stage.PostOperation)
        });

        var replacementTypes = new[] { new LocalPluginType("MyPlugin", "MyPlugin", string.Empty, true, []) };
        await migrator.MigrateAsync("MyPlugins", newAssemblyId, replacementTypes, new PluginPushOptions(null, DryRun: false));

        var api = service.Retrieve(CustomAPI.EntityLogicalName, customApiId, new ColumnSet(true)).ToEntity<CustomAPI>();
        await Assert.That(api.PluginTypeId!.Id).IsEqualTo(newTypeId);

        var step = service.Retrieve(SdkMessageProcessingStep.EntityLogicalName, stepId, new ColumnSet(true)).ToEntity<SdkMessageProcessingStep>();
        await Assert.That(step.EventHandler!.Id).IsEqualTo(newTypeId);

        // Outdated assembly/type are always purged once references have been migrated.
        await Assert.That(() => service.Retrieve(PluginAssembly.EntityLogicalName, oldAssemblyId, new ColumnSet(true)))
            .Throws<Exception>();
        await Assert.That(() => service.Retrieve(PluginType.EntityLogicalName, oldTypeId, new ColumnSet(true)))
            .Throws<Exception>();
    }

    [Test]
    public async Task MigrateAsync_DryRun_DoesNotMigrateOrDeleteAnything()
    {
        var (service, migrator) = CreateMigrator();
        var oldAssemblyId = CreateAssembly(service, "MyPlugins", "1.0.0.0");
        var oldTypeId = CreateType(service, oldAssemblyId, "MyPlugin");
        var newAssemblyId = CreateAssembly(service, "MyPlugins", "2.0.0.0");
        CreateType(service, newAssemblyId, "MyPlugin");
        var customApiId = Guid.NewGuid();
        service.Create(new CustomAPI(customApiId) { PluginTypeId = new EntityReference(PluginType.EntityLogicalName, oldTypeId) });

        var replacementTypes = new[] { new LocalPluginType("MyPlugin", "MyPlugin", string.Empty, true, []) };
        await migrator.MigrateAsync("MyPlugins", newAssemblyId, replacementTypes, new PluginPushOptions(null, DryRun: true));

        var api = service.Retrieve(CustomAPI.EntityLogicalName, customApiId, new ColumnSet(true)).ToEntity<CustomAPI>();
        await Assert.That(api.PluginTypeId!.Id).IsEqualTo(oldTypeId);

        var oldAssembly = service.Retrieve(PluginAssembly.EntityLogicalName, oldAssemblyId, new ColumnSet(true));
        await Assert.That(oldAssembly).IsNotNull();
    }

    [Test]
    public async Task MigrateAsync_TypeWithoutReplacement_DeletesOrphanedStepsAndType()
    {
        var (service, migrator) = CreateMigrator();
        var oldAssemblyId = CreateAssembly(service, "MyPlugins", "1.0.0.0");
        var oldTypeId = CreateType(service, oldAssemblyId, "RemovedPlugin");
        var newAssemblyId = CreateAssembly(service, "MyPlugins", "2.0.0.0");
        var orphanedStepId = Guid.NewGuid();
        service.Create(new SdkMessageProcessingStep(orphanedStepId)
        {
            Name = "orphaned-step",
            EventHandler = new EntityReference(PluginType.EntityLogicalName, oldTypeId)
        });

        await migrator.MigrateAsync("MyPlugins", newAssemblyId, [], new PluginPushOptions(null, DryRun: false));

        await Assert.That(() => service.Retrieve(SdkMessageProcessingStep.EntityLogicalName, orphanedStepId, new ColumnSet(true)))
            .Throws<Exception>();
        await Assert.That(() => service.Retrieve(PluginAssembly.EntityLogicalName, oldAssemblyId, new ColumnSet(true)))
            .Throws<Exception>();
    }
}
