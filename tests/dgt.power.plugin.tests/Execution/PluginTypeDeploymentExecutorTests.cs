// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.plugin.Repositories;
using dgt.power.plugin.Execution;
using dgt.power.plugin.Local;
using dgt.power.plugin.Output;
using dgt.power.plugin.Planning;
using dgt.power.plugin.Remote;
using dgt.power.tests.FakeExecutor;
using Digitall.Dataverse.Testing;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Spectre.Console.Testing;
using System.Security.Cryptography;

namespace dgt.power.plugin.tests.Execution;

public class PluginTypeDeploymentExecutorTests
{
    private sealed class PluginTypeTestPipeline(
        PluginDeploymentPlanner planner,
        PluginTypeDeploymentExecutor executor,
        PluginPlanRenderer renderer)
    {
        public async Task ApplyAsync(
            Guid assemblyId,
            IReadOnlyList<LocalPluginType> localTypes,
            PluginPushOptions options)
        {
            var localAssembly = new LocalAssembly
            {
                Name = "TestAssembly",
                Version = new Version(1, 0),
                Content = "YmFzZTY0",
                ContentHash = Convert.ToHexString(SHA256.HashData("base64"u8.ToArray())),
                Kind = LocalAssemblyKind.Plugin,
                PluginTypes = localTypes
            };
            var typePlan = await planner.BuildPluginTypesAsync(assemblyId, localTypes, options.Solution);
            var deployment = new AssemblyDeploymentPlan(
                new AssemblyComparison(
                    localAssembly,
                    new RemoteAssembly(assemblyId, localAssembly.Version, null)),
                typePlan,
                new OutdatedAssemblyDeployment([]),
                LinkManagedIdentity: false,
                SolutionMembership: string.IsNullOrWhiteSpace(options.Solution)
                    ? null
                    : new SolutionMembershipPlan(options.Solution),
                Solution: null);
            renderer.Render(deployment);
            if (!options.DryRun)
            {
                await executor.ApplyAsync(typePlan, assemblyId);
            }
        }
    }

    private static (FakeOrganizationServiceAsync Service, PluginTypeTestPipeline Executor, TestConsole Console) CreateExecutorWithConsole()
    {
        var service = new FakeOrganizationServiceAsync();
        service.AddRequests(new AddSolutionComponentExecutor());
        service.AddRequests(new RetrieveDependenciesForDeleteExecutor());
        service.AddDefaultRequests();

        var console = new TestConsole();
        var typeRepository = new PluginTypeRepository(service);
        var stepRepository = new SdkMessageProcessingStepRepository(service);
        var imageRepository = new SdkMessageProcessingStepImageRepository(service);
        var customApiRepository = new CustomApiRepository(service);
        var solutionRepository = new SolutionComponentRepository(service);
        var planner = new PluginDeploymentPlanner(new PluginPlanningRepositories
        {
            Assemblies = new PluginAssemblyRepository(service),
            Packages = new PluginPackageRepository(service),
            Types = typeRepository,
            Steps = stepRepository,
            Images = imageRepository,
            Messages = new SdkMessageRepository(service),
            CustomApis = customApiRepository,
            Solutions = solutionRepository
        });
        var executor = new PluginTypeTestPipeline(
            planner,
            new PluginTypeDeploymentExecutor(
                typeRepository,
                stepRepository,
                imageRepository,
                customApiRepository,
                solutionRepository),
            new PluginPlanRenderer(console));

        return (service, executor, console);
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Reliability", "CA2000", Justification = "TestConsole ownership is transferred to the executor.")]
    private static (FakeOrganizationServiceAsync Service, PluginTypeTestPipeline Executor) CreateExecutor()
    {
        var service = new FakeOrganizationServiceAsync();
        service.AddRequests(new AddSolutionComponentExecutor());
        service.AddRequests(new RetrieveDependenciesForDeleteExecutor());
        service.AddDefaultRequests();

        var console = new TestConsole();
        var typeRepository = new PluginTypeRepository(service);
        var stepRepository = new SdkMessageProcessingStepRepository(service);
        var imageRepository = new SdkMessageProcessingStepImageRepository(service);
        var customApiRepository = new CustomApiRepository(service);
        var solutionRepository = new SolutionComponentRepository(service);
        var planner = new PluginDeploymentPlanner(new PluginPlanningRepositories
        {
            Assemblies = new PluginAssemblyRepository(service),
            Packages = new PluginPackageRepository(service),
            Types = typeRepository,
            Steps = stepRepository,
            Images = imageRepository,
            Messages = new SdkMessageRepository(service),
            CustomApis = customApiRepository,
            Solutions = solutionRepository
        });
        var executor = new PluginTypeTestPipeline(
            planner,
            new PluginTypeDeploymentExecutor(
                typeRepository,
                stepRepository,
                imageRepository,
                customApiRepository,
                solutionRepository),
            new PluginPlanRenderer(console));

        return (service, executor);
    }

    private static Guid SeedMessage(FakeOrganizationServiceAsync service, string name, string? primaryEntityName = null)
    {
        var messageId = Guid.NewGuid();
        service.Create(new SdkMessage(messageId) { Name = name });

        if (primaryEntityName is not null)
        {
            service.Create(new SdkMessageFilter(Guid.NewGuid())
            {
                SdkMessageId = new EntityReference(SdkMessage.EntityLogicalName, messageId),
                Attributes = { [SdkMessageFilter.LogicalNames.PrimaryObjectTypeCode] = primaryEntityName }
            });
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
    public async Task ApplyAsync_NewTypeWithStep_CreatesTypeAndStep()
    {
        var (service, executor) = CreateExecutor();
        SeedMessage(service, "Create", "account");
        var assemblyId = Guid.NewGuid();
        var localType = new LocalPluginType("MyPlugin", "MyPlugin", string.Empty, true, [Step()]);

        await executor.ApplyAsync(assemblyId, [localType], new PluginPushOptions(null, DryRun: false));

        var types = service.RetrieveMultiple(new QueryExpression(PluginType.EntityLogicalName) { ColumnSet = new ColumnSet(true) }).Entities;
        await Assert.That(types.Count).IsEqualTo(1);
        var typeEntity = types[0].ToEntity<PluginType>();
        await Assert.That(typeEntity.TypeName).IsEqualTo("MyPlugin");

        var steps = service.RetrieveMultiple(new QueryExpression(SdkMessageProcessingStep.EntityLogicalName) { ColumnSet = new ColumnSet(true) }).Entities;
        await Assert.That(steps.Count).IsEqualTo(1);
        await Assert.That(steps[0].ToEntity<SdkMessageProcessingStep>().Name).IsEqualTo("step");
    }

    [Test]
    public async Task ApplyAsync_NewStepWithSolution_ListsAndAddsSolutionMembership()
    {
        var (service, executor, console) = CreateExecutorWithConsole();
        service.Create(new Solution(Guid.NewGuid()) { UniqueName = "TestSolution" });
        SeedMessage(service, "Create", "account");
        var localType = new LocalPluginType("MyPlugin", "MyPlugin", string.Empty, true, [Step()]);

        await executor.ApplyAsync(
            Guid.NewGuid(),
            [localType],
            new PluginPushOptions("TestSolution", DryRun: false));

        using (Assert.Multiple())
        {
            await Assert.That(console.Output).Contains("Solution membership: TestSolution");
            await Assert.That(console.Output).Contains("Step step");
        }
    }

    [Test]
    public async Task ApplyAsync_UnresolvedMessage_Throws()
    {
        var (_, executor) = CreateExecutor();
        var localType = new LocalPluginType("MyPlugin", "MyPlugin", string.Empty, true, [Step(messageName: "DoesNotExist")]);

        await Assert.That(() => executor.ApplyAsync(Guid.NewGuid(), [localType], new PluginPushOptions(null, DryRun: false)))
            .Throws<UnresolvedPluginStepMessageException>();
    }

    [Test]
    public async Task ApplyAsync_MissingCustomApi_Throws()
    {
        var (_, executor) = CreateExecutor();
        var localType = new LocalPluginType("MyPlugin", "MyPlugin", "missing_api", true, []);

        await Assert.That(() => executor.ApplyAsync(
                Guid.NewGuid(), [localType], new PluginPushOptions(null, DryRun: false)))
            .Throws<MissingCustomApiException>();
    }

    [Test]
    public async Task ApplyAsync_DryRun_CreatesNothing()
    {
        var (service, executor) = CreateExecutor();
        SeedMessage(service, "Create", "account");
        var localType = new LocalPluginType("MyPlugin", "MyPlugin", string.Empty, true, [Step()]);

        await executor.ApplyAsync(Guid.NewGuid(), [localType], new PluginPushOptions(null, DryRun: true));

        var types = service.RetrieveMultiple(new QueryExpression(PluginType.EntityLogicalName) { ColumnSet = new ColumnSet(true) }).Entities;
        await Assert.That(types.Count).IsEqualTo(0);
    }

    [Test]
    public async Task ApplyAsync_DryRun_PreviewsStepsAndImagesForBrandNewType()
    {
        var (service, executor, console) = CreateExecutorWithConsole();
        SeedMessage(service, "Update", "account");
        var image = new LocalPluginStepImage(SdkMessageProcessingStepImage.Options.ImageType.PreImage, "PreImage", "PreImage", "Target", null);
        var localType = new LocalPluginType("MyPlugin", "MyPlugin", string.Empty, true,
            [Step(messageName: "Update", images: [image])]);

        // Even for a brand-new (never-before-registered) assembly/type, dry-run must still preview
        // every step/image declared on it - not just the type itself - since it can't rely on a real
        // Dataverse id existing yet to compare against.
        await executor.ApplyAsync(Guid.NewGuid(), [localType], new PluginPushOptions(null, DryRun: true));

        using (Assert.Multiple())
        {
            await Assert.That(console.Output).Contains("MyPlugin Create");
            await Assert.That(console.Output).Contains("step Create");
            await Assert.That(console.Output).Contains("PreImage Create");

            var steps = service.RetrieveMultiple(new QueryExpression(SdkMessageProcessingStep.EntityLogicalName) { ColumnSet = new ColumnSet(true) }).Entities;
            await Assert.That(steps.Count).IsEqualTo(0);
            var images = service.RetrieveMultiple(new QueryExpression(SdkMessageProcessingStepImage.EntityLogicalName) { ColumnSet = new ColumnSet(true) }).Entities;
            await Assert.That(images.Count).IsEqualTo(0);
        }
    }

    [Test]
    public async Task ApplyAsync_DryRun_DoesNotReportAggregateCounts()
    {
        var (service, executor, console) = CreateExecutorWithConsole();
        var messageId = Guid.NewGuid();
        service.Create(new SdkMessage(messageId) { Name = "Create" });
        var filterId = Guid.NewGuid();
        var filter = new SdkMessageFilter(filterId)
        {
            SdkMessageId = new EntityReference(SdkMessage.EntityLogicalName, messageId),
            Attributes = { [SdkMessageFilter.LogicalNames.PrimaryObjectTypeCode] = "account" }
        };
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

        await executor.ApplyAsync(
            assemblyId,
            [new LocalPluginType("MyPlugin", "MyPlugin", string.Empty, true, [Step()])],
            new PluginPushOptions(null, DryRun: true));

        await Assert.That(console.Output).DoesNotContain("Checked 1 plugin type(s)");
        await Assert.That(console.Output).DoesNotContain("Steps:");
    }

    [Test]
    public async Task ApplyAsync_UnregisteredType_IsIgnoredAndPreservesExistingRegistration()
    {
        var (service, executor, console) = CreateExecutorWithConsole();
        var assemblyId = Guid.NewGuid();
        var typeId = Guid.NewGuid();
        service.Create(new PluginType(typeId)
        {
            TypeName = "MyPlugin",
            PluginAssemblyId = new EntityReference(PluginAssembly.EntityLogicalName, assemblyId)
        });

        await executor.ApplyAsync(
            assemblyId,
            [new LocalPluginType("MyPlugin", "MyPlugin", "new_api", false, [])],
            new PluginPushOptions(null, DryRun: false));

        var existing = service.Retrieve(PluginType.EntityLogicalName, typeId, new ColumnSet(true)).ToEntity<PluginType>();
        using (Assert.Multiple())
        {
            await Assert.That(existing.Id).IsEqualTo(typeId);
            await Assert.That(console.Output).DoesNotContain("Create PluginType");
            await Assert.That(console.Output).DoesNotContain("Link Custom API");
        }
    }

    [Test]
    public async Task ApplyAsync_ExistingStepWithChangedOrder_Updates()
    {
        var (service, executor) = CreateExecutor();
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

        await executor.ApplyAsync(assemblyId, [localType], new PluginPushOptions(null, DryRun: false));

        var steps = service.RetrieveMultiple(new QueryExpression(SdkMessageProcessingStep.EntityLogicalName) { ColumnSet = new ColumnSet(true) }).Entities;
        await Assert.That(steps.Count).IsEqualTo(1);
        await Assert.That(steps[0].ToEntity<SdkMessageProcessingStep>().Rank).IsEqualTo(42);
    }

    [Test]
    public async Task ApplyAsync_OrphanedRemoteType_DeletesTypeAndDependentSteps()
    {
        var (service, executor) = CreateExecutor();
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

        await executor.ApplyAsync(assemblyId, [], new PluginPushOptions(null, DryRun: false));

        var remainingTypes = service.RetrieveMultiple(new QueryExpression(PluginType.EntityLogicalName) { ColumnSet = new ColumnSet(true) }).Entities;
        await Assert.That(remainingTypes.Count).IsEqualTo(0);
        var remainingSteps = service.RetrieveMultiple(new QueryExpression(SdkMessageProcessingStep.EntityLogicalName) { ColumnSet = new ColumnSet(true) }).Entities;
        await Assert.That(remainingSteps.Count).IsEqualTo(0);
    }

    [Test]
    public async Task ApplyAsync_CustomApiHandler_LinksMatchingCustomApiAndSkipsSteps()
    {
        var (service, executor) = CreateExecutor();
        var assemblyId = Guid.NewGuid();
        var customApiId = Guid.NewGuid();
        service.Create(new CustomAPI(customApiId) { UniqueName = "new_MyApi" });
        var localType = new LocalPluginType("MyPlugin", "MyPlugin", "new_MyApi", true, []);

        await executor.ApplyAsync(assemblyId, [localType], new PluginPushOptions(null, DryRun: false));

        var customApi = service.Retrieve(CustomAPI.EntityLogicalName, customApiId, new ColumnSet(true)).ToEntity<CustomAPI>();
        var types = service.RetrieveMultiple(new QueryExpression(PluginType.EntityLogicalName) { ColumnSet = new ColumnSet(true) }).Entities;
        await Assert.That(types.Count).IsEqualTo(1);
        await Assert.That(customApi.PluginTypeId!.Id).IsEqualTo(types[0].Id);

        var steps = service.RetrieveMultiple(new QueryExpression(SdkMessageProcessingStep.EntityLogicalName) { ColumnSet = new ColumnSet(true) }).Entities;
        await Assert.That(steps.Count).IsEqualTo(0);
    }

    [Test]
    public async Task ApplyAsync_CustomApiHandler_PreservesLegacyImplementationStep()
    {
        var (service, executor) = CreateExecutor();
        var assemblyId = Guid.NewGuid();
        var typeId = Guid.NewGuid();
        var customApiId = Guid.NewGuid();
        service.Create(new PluginType(typeId)
        {
            TypeName = "MyPlugin",
            PluginAssemblyId = new EntityReference(PluginAssembly.EntityLogicalName, assemblyId)
        });
        service.Create(new CustomAPI(customApiId)
        {
            UniqueName = "new_MyApi",
            PluginTypeId = new EntityReference(PluginType.EntityLogicalName, typeId)
        });
        service.Create(new SdkMessageProcessingStep(Guid.NewGuid())
        {
            Name = "CustomApi 'new_MyApi' implementation",
            EventHandler = new EntityReference(PluginType.EntityLogicalName, typeId)
        });

        await executor.ApplyAsync(
            assemblyId,
            [new LocalPluginType("MyPlugin", "MyPlugin", "new_MyApi", true, [])],
            new PluginPushOptions(null, DryRun: false));

        var steps = service.RetrieveMultiple(
            new QueryExpression(SdkMessageProcessingStep.EntityLogicalName) { ColumnSet = new ColumnSet(true) }).Entities;
        await Assert.That(steps).Count().IsEqualTo(1);
    }

    [Test]
    public async Task ApplyAsync_StepWithPreImage_CreatesImage()
    {
        var (service, executor) = CreateExecutor();
        SeedMessage(service, "Update", "account");
        var assemblyId = Guid.NewGuid();
        var image = new LocalPluginStepImage(SdkMessageProcessingStepImage.Options.ImageType.PreImage, "PreImage", "PreImage", "Target", null);
        var localType = new LocalPluginType("MyPlugin", "MyPlugin", string.Empty, true,
            [Step(messageName: "Update", images: [image])]);

        await executor.ApplyAsync(assemblyId, [localType], new PluginPushOptions(null, DryRun: false));

        var images = service.RetrieveMultiple(new QueryExpression(SdkMessageProcessingStepImage.EntityLogicalName) { ColumnSet = new ColumnSet(true) }).Entities;
        await Assert.That(images.Count).IsEqualTo(1);
        await Assert.That(images[0].ToEntity<SdkMessageProcessingStepImage>().Name).IsEqualTo("PreImage");
    }
}
