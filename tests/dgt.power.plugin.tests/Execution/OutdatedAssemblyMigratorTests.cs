// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.plugin.Repositories;
using dgt.power.plugin.Execution;
using dgt.power.plugin.Local;
using dgt.power.plugin.Planning;
using dgt.power.tests.FakeExecutor;
using Digitall.Dataverse.Testing;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.plugin.tests.Execution;

public class OutdatedAssemblyMigratorTests
{
    private sealed class MigrationPipeline(
        PluginDeploymentPlanner planner,
        OutdatedAssemblyMigrator migrator)
    {
        public async Task MigrateAsync(
            string assemblyName,
            Guid newAssemblyId,
            IReadOnlyList<LocalPluginType> replacementTypes,
            PluginPushOptions options)
        {
            var plan = await planner.BuildOutdatedAssembliesAsync(
                assemblyName,
                replacementTypes,
                newAssemblyId);
            if (options.DryRun)
            {
                return;
            }

            await migrator.ApplyAsync(plan);
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Reliability", "CA2000", Justification = "TestConsole ownership is transferred to the migrator.")]
    private static (FakeOrganizationServiceAsync Service, MigrationPipeline Migrator) CreateMigrator()
    {
        var service = new FakeOrganizationServiceAsync();
        service.AddRequests(new RetrieveDependenciesForDeleteExecutor());
        service.AddDefaultRequests();

        var assemblyRepository = new PluginAssemblyRepository(service);
        var packageRepository = new PluginPackageRepository(service);
        var typeRepository = new PluginTypeRepository(service);
        var stepRepository = new SdkMessageProcessingStepRepository(service);
        var imageRepository = new SdkMessageProcessingStepImageRepository(service);
        var customApiRepository = new CustomApiRepository(service);
        var planner = new PluginDeploymentPlanner(new PluginPlanningRepositories
        {
            Assemblies = assemblyRepository,
            Packages = packageRepository,
            Types = typeRepository,
            Steps = stepRepository,
            Images = imageRepository,
            Messages = new SdkMessageRepository(service),
            CustomApis = customApiRepository,
            Solutions = new SolutionComponentRepository(service)
        });
        var migrator = new MigrationPipeline(
            planner,
            new OutdatedAssemblyMigrator(
                assemblyRepository,
                typeRepository,
                stepRepository,
                customApiRepository));

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
