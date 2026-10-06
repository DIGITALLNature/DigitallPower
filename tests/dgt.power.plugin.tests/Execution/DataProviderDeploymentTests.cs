// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.plugin.Execution;
using dgt.power.plugin.Local;
using dgt.power.plugin.Planning;
using dgt.power.plugin.Planning.Deployment;
using dgt.power.plugin.Remote;
using dgt.power.plugin.Output;
using dgt.power.plugin.tests.Planning;
using dgt.power.plugin.tests.Repositories;
using dgt.power.tests.FakeExecutor;
using Digitall.Dataverse.Testing;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Crm.Sdk.Messages;
using Spectre.Console.Testing;

namespace dgt.power.plugin.tests.Execution;

public class DataProviderDeploymentTests
{
    private static PluginDeploymentPlanner Planner(FakeOrganizationServiceAsync service, ProviderTestRepository providers) =>
        new PluginDeploymentTestFactory(service, providers).Planner;

    private static PluginTypeDeploymentExecutor Executor(FakeOrganizationServiceAsync service, ProviderTestRepository providers) =>
        new PluginDeploymentTestFactory(service, providers).TypeExecutor;

    [Test]
    public async Task Apply_NewProvider_RegistersTypeAndProviderWithoutSdkSteps()
    {
        var service = new FakeOrganizationServiceAsync();
        service.AddDefaultRequests();
        var providers = new ProviderTestRepository();
        var plan = await Planner(service, providers).BuildPluginTypesAsync(null, [DataProviderDeploymentPlannerTests.Handler()]);
        var ids = await Executor(service, providers).ApplyAsync(plan, Guid.NewGuid());
        using (Assert.Multiple())
        {
            await Assert.That(providers.AppliedHandlers[DataProviderOperation.Retrieve]).IsEqualTo(ids["Retrieve"]);
            await Assert.That(providers.TableCreates).IsEqualTo(1);
            await Assert.That(providers.ProviderWrites).IsEqualTo(1);
            await Assert.That(service.RetrieveMultiple(new QueryExpression("sdkmessageprocessingstep") { ColumnSet = new ColumnSet(true) }).Entities).IsEmpty();
        }
    }

    [Test]
    public async Task ApplyTypes_OnlyCreatesTypes_LeavesProvidersAndCleanupToExplicitPhases()
    {
        var service = new FakeOrganizationServiceAsync();
        service.AddRequests(new RetrieveDependenciesForDeleteExecutor());
        service.AddDefaultRequests();
        var assemblyId = Guid.NewGuid();
        var oldTypeId = Guid.NewGuid();
        service.Create(new PluginType(oldTypeId) { TypeName = "Old", PluginAssemblyId = new EntityReference("pluginassembly", assemblyId) });
        var providers = new ProviderTestRepository { DataSource = DataProviderDeploymentPlannerTests.Table() };
        providers.Providers.Add(new RemoteDataProvider(Guid.NewGuid(), "dgt_source", "Provider", null,
            new Dictionary<DataProviderOperation, Guid> { [DataProviderOperation.Retrieve] = oldTypeId }));
        var factory = new PluginDeploymentTestFactory(service, providers);
        var plan = await factory.Planner.BuildPluginTypesAsync(assemblyId, [DataProviderDeploymentPlannerTests.Handler()]);

        var typeIds = await factory.TypeExecutor.ApplyTypesAsync(plan, assemblyId, null, CancellationToken.None);
        await Assert.That(typeIds).Count().IsEqualTo(1);
        await Assert.That(providers.ProviderWrites).IsEqualTo(0);
        await Assert.That(service.Retrieve("plugintype", oldTypeId, new ColumnSet("typename")).Id).IsEqualTo(oldTypeId);

        await factory.TypeExecutor.ApplyDataProvidersAsync(plan.DataProviders, typeIds, null, CancellationToken.None);
        await factory.TypeExecutor.ApplyRegistrationsAsync(plan, typeIds, null, CancellationToken.None);
        await Assert.That(providers.AppliedHandlers[DataProviderOperation.Retrieve]).IsEqualTo(typeIds["Retrieve"]);
        await Assert.That(service.Retrieve("plugintype", oldTypeId, new ColumnSet("typename")).Id).IsEqualTo(oldTypeId);

        await factory.TypeExecutor.DeleteTypesAsync(plan.Deletions, null, CancellationToken.None);
        var remainingTypes = service.RetrieveMultiple(new QueryExpression("plugintype") { ColumnSet = new ColumnSet("typename") }).Entities;
        await Assert.That(remainingTypes.Single().Id).IsEqualTo(typeIds["Retrieve"]);
    }

    [Test]
    public async Task Build_InternalProviderSteps_AreNeitherReconciledNorAddedToSolution()
    {
        var service = new FakeOrganizationServiceAsync();
        service.AddDefaultRequests();
        var assemblyId = Guid.NewGuid();
        var typeId = Guid.NewGuid();
        var messageId = Guid.NewGuid();
        service.Create(new PluginType(typeId) { TypeName = "Retrieve", PluginAssemblyId = new EntityReference("pluginassembly", assemblyId) });
        service.Create(new SdkMessage(messageId) { Name = "Retrieve" });
        service.Create(new SdkMessageProcessingStep(Guid.NewGuid())
        {
            Name = "Platform-owned", Stage = new OptionSetValue(30), Mode = new OptionSetValue(0),
            EventHandler = new EntityReference("plugintype", typeId), SdkMessageId = new EntityReference("sdkmessage", messageId)
        });
        var providers = new ProviderTestRepository { DataSource = DataProviderDeploymentPlannerTests.Table() };
        providers.Providers.Add(new RemoteDataProvider(Guid.NewGuid(), "dgt_source", "Provider", null, new Dictionary<DataProviderOperation, Guid> { [DataProviderOperation.Retrieve] = typeId }));
        var plan = await Planner(service, providers).BuildPluginTypesAsync(assemblyId, [DataProviderDeploymentPlannerTests.Handler()]);
        using (Assert.Multiple())
        {
            await Assert.That(plan.Types.Single().Steps).IsEmpty();
            await Assert.That(plan.Types.Single().StepDeletions).IsEmpty();
            await Assert.That(plan.DataProviders.Single().HasChanges).IsFalse();
        }
    }

    [Test]
    [Arguments(false)]
    [Arguments(true)]
    public async Task Apply_Upgrade_CleansUpOnlyAfterProviderWriteSucceeds(bool failProviderWrite)
    {
        var service = new FakeOrganizationServiceAsync();
        service.AddDefaultRequests();
        service.AddRequests(new RetrieveDependenciesForDeleteExecutor());
        var assemblyId = Guid.NewGuid();
        var typeId = Guid.NewGuid();
        service.Create(new PluginAssembly(assemblyId)
        {
            Name = "Plugins", Version = "1.0.0.0", SourceType = new OptionSetValue(0), IsolationMode = new OptionSetValue(2)
        });
        service.Create(new PluginType(typeId) { TypeName = "Retrieve", PluginAssemblyId = new EntityReference("pluginassembly", assemblyId) });
        var providers = new ProviderTestRepository { DataSource = DataProviderDeploymentPlannerTests.Table() };
        providers.Providers.Add(new RemoteDataProvider(Guid.NewGuid(), "dgt_source", "Provider", null,
            new Dictionary<DataProviderOperation, Guid> { [DataProviderOperation.Retrieve] = typeId, [DataProviderOperation.Update] = typeId }));
        var assembly = new LocalAssembly
        {
            Name = "Plugins", Version = new Version(2, 0, 0, 0), Content = "Y29udGVudA==", ContentHash = "hash",
            PluginTypes = [DataProviderDeploymentPlannerTests.Handler()]
        };
        var plan = await Planner(service, providers).BuildAssemblyAsync(assembly, new PluginPushOptions(null, true));
        var oldTypePresentOnProviderWrite = false;
        providers.BeforeApply = () =>
        {
            oldTypePresentOnProviderWrite = service.Retrieve("plugintype", typeId, new ColumnSet("typename")).Id == typeId;
            if (failProviderWrite)
            {
                throw new InvalidOperationException("Provider write failed");
            }
        };
        var executor = new PluginDeploymentTestFactory(service, providers).PushExecutor;
        if (failProviderWrite)
        {
            await Assert.That(async () => await executor.ExecuteAsync(plan)).ThrowsExactly<InvalidOperationException>();
            await Assert.That(oldTypePresentOnProviderWrite).IsTrue();
            await Assert.That(service.Retrieve("pluginassembly", assemblyId, new ColumnSet("name")).Id).IsEqualTo(assemblyId);
            var retainedTypes = service.RetrieveMultiple(new QueryExpression("plugintype") { ColumnSet = new ColumnSet(true) }).Entities;
            await Assert.That(retainedTypes).Count().IsEqualTo(2);
            await Assert.That(retainedTypes.Single(type => type.Id == typeId).GetAttributeValue<EntityReference>("pluginassemblyid").Id).IsEqualTo(assemblyId);
            await Assert.That(providers.ProviderWrites).IsEqualTo(0);
            return;
        }
        var newAssemblyId = await executor.ExecuteAsync(plan);
        var types = service.RetrieveMultiple(new QueryExpression("plugintype") { ColumnSet = new ColumnSet(true) }).Entities;
        var newTypeId = types.Single().Id;
        using (Assert.Multiple())
        {
            await Assert.That(oldTypePresentOnProviderWrite).IsTrue();
            await Assert.That(newTypeId).IsNotEqualTo(typeId);
            await Assert.That(types.Single().GetAttributeValue<EntityReference>("pluginassemblyid").Id).IsEqualTo(newAssemblyId);
            await Assert.That(providers.AppliedHandlers[DataProviderOperation.Retrieve]).IsEqualTo(newTypeId);
            await Assert.That(providers.AppliedHandlers[DataProviderOperation.Update]).IsEqualTo(newTypeId);
        }
    }

    [Test]
    public async Task Apply_PackageHandlersInDifferentAssemblies_MergesMetadataAndWritesProviderOnce()
    {
        var service = new FakeOrganizationServiceAsync();
        service.AddDefaultRequests();
        var providers = new ProviderTestRepository();
        var first = new LocalAssembly
        {
            Name = "RetrieveAssembly", Version = new Version(1, 0, 0, 0), Content = "Y29udGVudA==", ContentHash = "hash",
            PluginTypes = [DataProviderDeploymentPlannerTests.Handler()]
        };
        var second = first with
        {
            Name = "MultipleAssembly",
            PluginTypes = [new LocalPluginType("Multiple", "Multiple", string.Empty, true, [])
            {
                DataProviders = [new LocalDataProviderRegistration("dgt_Source", DataProviderOperation.RetrieveMultiple, null, null, null, null)]
            }]
        };
        var package = new LocalPluginPackage(new LocalPackage("Package", "1.0.0", string.Empty, "hash"), [first, second]);
        var plan = await Planner(service, providers).BuildPackageAsync(package, new PluginPushOptions(null, true, "dgt"));
        using (Assert.Multiple())
        {
            await Assert.That(plan.DataProviders).Count().IsEqualTo(1);
            await Assert.That(plan.DataProviders.Single().Handlers).Count().IsEqualTo(2);
            await Assert.That(plan.DataProviders.Single().Local!.ProviderName).IsEqualTo("Provider");
            await Assert.That(providers.ProviderWrites).IsEqualTo(0);
        }

        var executor = new PluginDeploymentTestFactory(service, providers).PushExecutor;
        await executor.ExecuteAsync(plan);
        var types = service.RetrieveMultiple(new QueryExpression("plugintype") { ColumnSet = new ColumnSet(true) }).Entities.ToDictionary(
            type => type.GetAttributeValue<string>("typename"), type => type.Id);
        using (Assert.Multiple())
        {
            await Assert.That(providers.AppliedHandlers[DataProviderOperation.Retrieve]).IsEqualTo(types["Retrieve"]);
            await Assert.That(providers.AppliedHandlers[DataProviderOperation.RetrieveMultiple]).IsEqualTo(types["Multiple"]);
            await Assert.That(providers.TableCreates).IsEqualTo(1);
            await Assert.That(providers.ProviderWrites).IsEqualTo(1);
        }
        using var console = new TestConsole();
        new PluginPlanRenderer(console).Render(plan);
        await Assert.That(console.Output).Contains("Data providers");
        await Assert.That(console.Output).Contains("retrieve → Retrieve");
        await Assert.That(console.Output).Contains("retrievemultiple → Multiple");
    }

    [Test]
    [Arguments(false, false)]
    [Arguments(true, false)]
    [Arguments(false, true)]
    [Arguments(true, true)]
    public async Task Apply_PackageMovesHandlerToAnotherAssembly_CleansUpOnlyAfterProviderWriteSucceeds(bool preserveUndeclaredOperation, bool failProviderWrite)
    {
        var service = new FakeOrganizationServiceAsync();
        service.AddRequests(new RetrieveDependenciesForDeleteExecutor());
        service.AddDefaultRequests();
        var packageId = Guid.NewGuid();
        var sourceAssemblyId = Guid.NewGuid();
        var targetAssemblyId = Guid.NewGuid();
        var oldTypeId = Guid.NewGuid();
        var oldTypeName = preserveUndeclaredOperation ? "Retrieve" : "Old";
        var newTypeName = preserveUndeclaredOperation ? "Retrieve" : "Replacement";
        service.Create(new PluginPackage(packageId) { Name = "dgt_Package", Version = "1.0.0" });
        service.Create(new PluginAssembly(sourceAssemblyId)
        {
            Name = "SourceAssembly", Version = "1.0.0.0", PackageId = new EntityReference("pluginpackage", packageId),
            SourceType = new OptionSetValue(PluginAssembly.Options.SourceType.FileStore), IsolationMode = new OptionSetValue(2)
        });
        service.Create(new PluginAssembly(targetAssemblyId)
        {
            Name = "TargetAssembly", Version = "1.0.0.0", PackageId = new EntityReference("pluginpackage", packageId),
            SourceType = new OptionSetValue(PluginAssembly.Options.SourceType.FileStore), IsolationMode = new OptionSetValue(2)
        });
        service.Create(new PluginType(oldTypeId) { TypeName = oldTypeName, PluginAssemblyId = new EntityReference("pluginassembly", sourceAssemblyId) });
        var handlers = new Dictionary<DataProviderOperation, Guid> { [DataProviderOperation.Retrieve] = oldTypeId };
        if (preserveUndeclaredOperation)
        {
            handlers[DataProviderOperation.Update] = oldTypeId;
        }
        var providers = new ProviderTestRepository { DataSource = DataProviderDeploymentPlannerTests.Table() };
        providers.Providers.Add(new RemoteDataProvider(Guid.NewGuid(), "dgt_source", "Provider", null, handlers));
        var source = new LocalAssembly
        {
            Name = "SourceAssembly", Version = new Version(1, 0, 0, 0), Content = "Y29udGVudA==", ContentHash = "hash", PluginTypes = []
        };
        var target = source with { Name = "TargetAssembly", PluginTypes = [DataProviderDeploymentPlannerTests.Handler(newTypeName)] };
        var package = new LocalPluginPackage(new LocalPackage("Package", "1.0.0", string.Empty, "hash"), [source, target]);
        var plan = await Planner(service, providers).BuildPackageAsync(package, new PluginPushOptions(null, false, "dgt"));
        var oldTypePresentOnProviderWrite = false;
        providers.BeforeApply = () =>
        {
            oldTypePresentOnProviderWrite = service.Retrieve("plugintype", oldTypeId, new ColumnSet("typename")).Id == oldTypeId;
            if (failProviderWrite)
            {
                throw new InvalidOperationException("Provider write failed");
            }
        };
        var executor = new PluginDeploymentTestFactory(service, providers).PushExecutor;
        if (failProviderWrite)
        {
            await Assert.That(async () => await executor.ExecuteAsync(plan)).ThrowsExactly<InvalidOperationException>();
            await Assert.That(oldTypePresentOnProviderWrite).IsTrue();
            var retainedTypes = service.RetrieveMultiple(new QueryExpression("plugintype") { ColumnSet = new ColumnSet(true) }).Entities;
            await Assert.That(retainedTypes).Count().IsEqualTo(2);
            await Assert.That(retainedTypes.Single(type => type.Id != oldTypeId).GetAttributeValue<EntityReference>("pluginassemblyid").Id).IsEqualTo(targetAssemblyId);
            await Assert.That(providers.ProviderWrites).IsEqualTo(0);
            return;
        }
        await executor.ExecuteAsync(plan);
        var remainingType = service.RetrieveMultiple(new QueryExpression("plugintype") { ColumnSet = new ColumnSet(true) }).Entities.Single();
        using (Assert.Multiple())
        {
            await Assert.That(oldTypePresentOnProviderWrite).IsTrue();
            await Assert.That(remainingType.GetAttributeValue<string>("typename")).IsEqualTo(newTypeName);
            await Assert.That(remainingType.GetAttributeValue<EntityReference>("pluginassemblyid").Id).IsEqualTo(targetAssemblyId);
            await Assert.That(providers.AppliedHandlers[DataProviderOperation.Retrieve]).IsEqualTo(remainingType.Id);
            await Assert.That(providers.ProviderWrites).IsEqualTo(1);
            await Assert.That(providers.TableCreates).IsEqualTo(0);
        }
        if (preserveUndeclaredOperation)
        {
            await Assert.That(providers.AppliedHandlers[DataProviderOperation.Update]).IsEqualTo(remainingType.Id);
        }
    }

    [Test]
    [Arguments(false)]
    [Arguments(true)]
    public async Task Apply_ExistingProvider_EnsuresProviderAndTableSchemaSolutionMembership(bool alreadyInSolution)
    {
        const int providerComponentType = 78;
        var service = new FakeOrganizationServiceAsync();
        var requests = new List<AddSolutionComponentRequest>();
        service.AddRequests(new ProviderRequestFake(typeof(AddSolutionComponentRequest), request =>
        {
            requests.Add((AddSolutionComponentRequest)request);
            return new AddSolutionComponentResponse();
        }));
        service.AddDefaultRequests();
        var solutionId = Guid.NewGuid();
        var assemblyId = Guid.NewGuid();
        var typeId = Guid.NewGuid();
        var providerId = Guid.NewGuid();
        var table = DataProviderDeploymentPlannerTests.Table();
        service.Create(new Solution(solutionId) { UniqueName = "TestSolution" });
        service.Create(new SolutionComponentDefinition(Guid.NewGuid()) { PrimaryEntityName = "entitydataprovider", SolutionComponentType = providerComponentType });
        service.Create(new PluginType(typeId) { TypeName = "Retrieve", PluginAssemblyId = new EntityReference("pluginassembly", assemblyId) });
        if (alreadyInSolution)
        {
            service.Create(new SolutionComponent(Guid.NewGuid())
            {
                ["objectid"] = providerId, ["componenttype"] = new OptionSetValue(providerComponentType), ["solutionid"] = new EntityReference("solution", solutionId)
            });
            service.Create(new SolutionComponent(Guid.NewGuid())
            {
                ["objectid"] = table.MetadataId, ["componenttype"] = new OptionSetValue(1), ["solutionid"] = new EntityReference("solution", solutionId)
            });
        }

        var providers = new ProviderTestRepository { DataSource = table };
        providers.Providers.Add(new RemoteDataProvider(providerId, "dgt_source", "Provider", null,
            new Dictionary<DataProviderOperation, Guid> { [DataProviderOperation.Retrieve] = typeId }));
        var plan = await Planner(service, providers).BuildPluginTypesAsync(assemblyId, [DataProviderDeploymentPlannerTests.Handler()], "TestSolution");
        await Executor(service, providers).ApplyAsync(plan, assemblyId);
        using (Assert.Multiple())
        {
            await Assert.That(plan.DataProviders.Single().HasChanges).IsEqualTo(!alreadyInSolution);
            await Assert.That(providers.ProviderWrites).IsEqualTo(0);
            await Assert.That(requests).Count().IsEqualTo(alreadyInSolution ? 0 : 2);
        }
        if (!alreadyInSolution)
        {
            await Assert.That(requests.Single(request => request.ComponentType == providerComponentType).ComponentId).IsEqualTo(providerId);
            var tableRequest = requests.Single(request => request.ComponentType == 1);
            await Assert.That(tableRequest.ComponentId).IsEqualTo(table.MetadataId!.Value);
            await Assert.That(tableRequest.DoNotIncludeSubcomponents).IsFalse();
        }
    }
}
