// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.tests.Extensions;
using dgt.power.tests.FakeExecutor;
using Microsoft.Crm.Sdk.Messages;

namespace dgt.power.solution.tests;

public class CopyComponentsCommandTests : CommandTestsBase<CopyComponentsCommand, CopyComponentsSettings>
{
    private const string TargetSolutionName = "target_solution";
    private const string SourceSolutionName = "source_solution";

    private static readonly Guid s_unmanagedEntityMetadataId = Guid.NewGuid();
    private static readonly Guid s_managedEntityMetadataId = Guid.NewGuid();
    private static readonly Guid s_activeAttributeMetadataId = Guid.NewGuid();
    private static readonly Guid s_inactiveAttributeMetadataId = Guid.NewGuid();
    private static readonly Guid s_managedActiveWorkflowId = Guid.NewGuid();
    private static readonly Guid s_managedInactiveWorkflowId = Guid.NewGuid();
    private static readonly Guid s_unmanagedWorkflowId = Guid.NewGuid();

    [Test]
    public async Task ShouldFailWhenTargetSolutionNotFound() => await CreateBuilder()
        .Build()
        .Execute(new CopyComponentsSettings { Target = "missing", Source = SourceSolutionName })
        .Fail();

    [Test]
    public async Task ShouldFailWhenTargetSolutionIsManaged()
    {
        var context = CreateBuilder(targetIsManaged: true).Build();

        await context
            .Execute(new CopyComponentsSettings { Target = TargetSolutionName, Source = SourceSolutionName })
            .Fail();
    }

    [Test]
    public async Task ShouldFailWhenSourceSolutionNotFound() => await CreateBuilder()
        .Build()
        .Execute(new CopyComponentsSettings { Target = TargetSolutionName, Source = "missing_source" })
        .Fail();

    [Test]
    public async Task ShouldApplyBestPracticesByDefault()
    {
        var addedComponents = new List<AddSolutionComponentRequest>();
        var context = CreateBuilder()
            .WithExecutionMock<AddSolutionComponentRequest>(request =>
            {
                addedComponents.Add((AddSolutionComponentRequest)request);
                return new AddSolutionComponentResponse();
            })
            .Build();

        await context
            .Execute(new CopyComponentsSettings { Target = TargetSolutionName, Source = SourceSolutionName })
            .Succeed();

        var byObjectId = addedComponents.ToDictionary(request => request.ComponentId);

        // Unmanaged table: added completely.
        await Assert.That(byObjectId[s_unmanagedEntityMetadataId].DoNotIncludeSubcomponents).IsFalse();
        // Managed table: added as skeleton only.
        await Assert.That(byObjectId[s_managedEntityMetadataId].DoNotIncludeSubcomponents).IsTrue();
        // Managed attribute with an active layer: included.
        await Assert.That(byObjectId.ContainsKey(s_activeAttributeMetadataId)).IsTrue();
        // Managed attribute without an active layer: excluded (redundant vs. the managed baseline).
        await Assert.That(byObjectId.ContainsKey(s_inactiveAttributeMetadataId)).IsFalse();
        // Unmanaged standalone (workflow) component: included.
        await Assert.That(byObjectId.ContainsKey(s_unmanagedWorkflowId)).IsTrue();
        // Managed standalone component with an active layer: included.
        await Assert.That(byObjectId.ContainsKey(s_managedActiveWorkflowId)).IsTrue();
        // Managed standalone component without an active layer: excluded.
        await Assert.That(byObjectId.ContainsKey(s_managedInactiveWorkflowId)).IsFalse();

        await Assert.That(addedComponents.TrueForAll(request => !request.AddRequiredComponents)).IsTrue();
        await Assert.That(addedComponents.TrueForAll(request => request.SolutionUniqueName == TargetSolutionName)).IsTrue();
        await Assert.That(addedComponents.Count).IsEqualTo(5);
    }

    [Test]
    public async Task ShouldMirrorSourceBehaviorWhenRawModeEnabled()
    {
        var addedComponents = new List<AddSolutionComponentRequest>();
        var context = CreateBuilder()
            .WithExecutionMock<AddSolutionComponentRequest>(request =>
            {
                addedComponents.Add((AddSolutionComponentRequest)request);
                return new AddSolutionComponentResponse();
            })
            .Build();

        await context
            .Execute(new CopyComponentsSettings { Target = TargetSolutionName, Source = SourceSolutionName, Raw = true })
            .Succeed();

        // Raw mode never filters by managed/active-layer state - every source component is copied.
        await Assert.That(addedComponents.Count).IsEqualTo(7);

        var byObjectId = addedComponents.ToDictionary(request => request.ComponentId);
        await Assert.That(byObjectId[s_unmanagedEntityMetadataId].DoNotIncludeSubcomponents).IsFalse();
        await Assert.That(byObjectId[s_managedEntityMetadataId].DoNotIncludeSubcomponents).IsTrue();
    }

    [Test]
    public async Task ShouldDedupeComponentAcrossSourcesAndKeepMostCompleteRootComponentBehavior()
    {
        const string sourceSolutionNameB = "source_solution_b";
        var sharedObjectId = Guid.NewGuid();

        var entityMetadata = new EntityMetadata { LogicalName = "dgt_shared", MetadataId = sharedObjectId };
        entityMetadata.SetSealedPropertyValue(nameof(EntityMetadata.IsManaged), false);

        var addedComponents = new List<AddSolutionComponentRequest>();
        var context = new CommandTestContextBuilder<CopyComponentsCommand, CopyComponentsSettings>()
            .WithFakeMessageExecutor(new RetrieveEntityExecutor())
            .WithMetaData(entityMetadata)
            .WithData(_ => PrepareMultiSourceData(sharedObjectId, sourceSolutionNameB))
            .WithExecutionMock<AddSolutionComponentRequest>(request =>
            {
                addedComponents.Add((AddSolutionComponentRequest)request);
                return new AddSolutionComponentResponse();
            })
            .Build();

        await context
            .Execute(new CopyComponentsSettings { Target = TargetSolutionName, Source = $"{SourceSolutionName},{sourceSolutionNameB}", Raw = true })
            .Succeed();

        // Same component (componenttype+objectid) present in both sources with conflicting
        // RootComponentBehavior must be added exactly once, using the most complete behavior seen.
        await Assert.That(addedComponents.Count).IsEqualTo(1);
        await Assert.That(addedComponents[0].DoNotIncludeSubcomponents).IsFalse();
    }

    private static IEnumerable<Entity> PrepareMultiSourceData(Guid sharedObjectId, string sourceSolutionNameB)
    {
        var target = new Solution(Guid.NewGuid()) { UniqueName = TargetSolutionName, [Solution.LogicalNames.IsManaged] = false };
        var sourceA = new Solution(Guid.NewGuid()) { UniqueName = SourceSolutionName, [Solution.LogicalNames.IsManaged] = false };
        var sourceB = new Solution(Guid.NewGuid()) { UniqueName = sourceSolutionNameB, [Solution.LogicalNames.IsManaged] = false };

        var definition = new SolutionComponentDefinition(Guid.NewGuid())
        {
            ["solutioncomponenttype"] = SolutionComponent.Options.ComponentType.Entity,
            ["name"] = "Entity"
        };

        // Less complete behavior, present in source A.
        var componentInSourceA = new SolutionComponent(Guid.NewGuid())
        {
            [SolutionComponent.LogicalNames.ComponentType] = new OptionSetValue(SolutionComponent.Options.ComponentType.Entity),
            [SolutionComponent.LogicalNames.ObjectId] = sharedObjectId,
            [SolutionComponent.LogicalNames.SolutionId] = sourceA.ToEntityReference(),
            [SolutionComponent.LogicalNames.RootComponentBehavior] = new OptionSetValue(SolutionComponent.Options.RootComponentBehavior.DoNotIncludeSubcomponents)
        };

        // Most complete behavior, present in source B - this is the one that must win the dedupe.
        var componentInSourceB = new SolutionComponent(Guid.NewGuid())
        {
            [SolutionComponent.LogicalNames.ComponentType] = new OptionSetValue(SolutionComponent.Options.ComponentType.Entity),
            [SolutionComponent.LogicalNames.ObjectId] = sharedObjectId,
            [SolutionComponent.LogicalNames.SolutionId] = sourceB.ToEntityReference(),
            [SolutionComponent.LogicalNames.RootComponentBehavior] = new OptionSetValue(SolutionComponent.Options.RootComponentBehavior.IncludeSubcomponents)
        };

        return [target, sourceA, sourceB, definition, componentInSourceA, componentInSourceB];
    }

    [Test]
    public async Task ShouldNotExecuteAnyRequestOnDryRun()
    {
        var addedComponents = new List<AddSolutionComponentRequest>();
        var context = CreateBuilder()
            .WithExecutionMock<AddSolutionComponentRequest>(request =>
            {
                addedComponents.Add((AddSolutionComponentRequest)request);
                return new AddSolutionComponentResponse();
            })
            .Build();

        await context
            .Execute(new CopyComponentsSettings { Target = TargetSolutionName, Source = SourceSolutionName, DryRun = true })
            .Succeed();

        await Assert.That(addedComponents).IsEmpty();
    }

    private static CommandTestContextBuilder<CopyComponentsCommand, CopyComponentsSettings> CreateBuilder(bool targetIsManaged = false)
    {
        return new CommandTestContextBuilder<CopyComponentsCommand, CopyComponentsSettings>()
            .WithFakeMessageExecutor(new RetrieveEntityExecutor())
            .WithMetaData(BuildEntityMetadata())
            .WithData(_ => PrepareData(targetIsManaged));
    }

    private static EntityMetadata[] BuildEntityMetadata()
    {
        var activeAttribute = new AttributeMetadata { LogicalName = "dgt_active", MetadataId = s_activeAttributeMetadataId };
        activeAttribute.SetSealedPropertyValue(nameof(AttributeMetadata.IsManaged), true);

        var inactiveAttribute = new AttributeMetadata { LogicalName = "dgt_inactive", MetadataId = s_inactiveAttributeMetadataId };
        inactiveAttribute.SetSealedPropertyValue(nameof(AttributeMetadata.IsManaged), true);

        var unmanagedEntity = new EntityMetadata { LogicalName = "dgt_unmanaged", MetadataId = s_unmanagedEntityMetadataId };
        unmanagedEntity.SetSealedPropertyValue(nameof(EntityMetadata.IsManaged), false);

        var managedEntity = new EntityMetadata { LogicalName = "isv_managed", MetadataId = s_managedEntityMetadataId };
        managedEntity.SetSealedPropertyValue(nameof(EntityMetadata.IsManaged), true);
        managedEntity.SetAttributeCollection([activeAttribute, inactiveAttribute]);

        // Backing table for the standalone (record-based) Workflow componenttype scenario - only
        // needs an "ismanaged" attribute and a primary id so ComponentManagedStateResolver can query it.
        var isManagedColumn = new AttributeMetadata { LogicalName = "ismanaged" };
        var workflowEntity = new EntityMetadata { LogicalName = "workflow" };
        workflowEntity.SetSealedPropertyValue(nameof(EntityMetadata.PrimaryIdAttribute), "workflowid");
        workflowEntity.SetAttributeCollection([isManagedColumn]);

        return [unmanagedEntity, managedEntity, workflowEntity];
    }

    private static IEnumerable<Entity> PrepareData(bool targetIsManaged)
    {
        var target = new Solution(Guid.NewGuid()) { UniqueName = TargetSolutionName, [Solution.LogicalNames.IsManaged] = targetIsManaged };
        var source = new Solution(Guid.NewGuid()) { UniqueName = SourceSolutionName, [Solution.LogicalNames.IsManaged] = false };

        var definitions = new[]
        {
            new SolutionComponentDefinition(Guid.NewGuid())
            {
                ["solutioncomponenttype"] = SolutionComponent.Options.ComponentType.Entity,
                ["name"] = "Entity"
            },
            new SolutionComponentDefinition(Guid.NewGuid())
            {
                ["solutioncomponenttype"] = SolutionComponent.Options.ComponentType.Attribute,
                ["name"] = "Attribute"
            },
            new SolutionComponentDefinition(Guid.NewGuid())
            {
                ["solutioncomponenttype"] = SolutionComponent.Options.ComponentType.Workflow,
                ["name"] = "Workflow",
                ["primaryentityname"] = "workflow"
            }
        };

        var unmanagedEntityComponent = new SolutionComponent(Guid.NewGuid())
        {
            [SolutionComponent.LogicalNames.ComponentType] = new OptionSetValue(SolutionComponent.Options.ComponentType.Entity),
            [SolutionComponent.LogicalNames.ObjectId] = s_unmanagedEntityMetadataId,
            [SolutionComponent.LogicalNames.SolutionId] = source.ToEntityReference(),
            [SolutionComponent.LogicalNames.RootComponentBehavior] = new OptionSetValue(SolutionComponent.Options.RootComponentBehavior.IncludeSubcomponents)
        };

        var managedEntityComponent = new SolutionComponent(Guid.NewGuid())
        {
            [SolutionComponent.LogicalNames.ComponentType] = new OptionSetValue(SolutionComponent.Options.ComponentType.Entity),
            [SolutionComponent.LogicalNames.ObjectId] = s_managedEntityMetadataId,
            [SolutionComponent.LogicalNames.SolutionId] = source.ToEntityReference(),
            [SolutionComponent.LogicalNames.RootComponentBehavior] = new OptionSetValue(SolutionComponent.Options.RootComponentBehavior.DoNotIncludeSubcomponents)
        };

        var activeAttributeComponent = new SolutionComponent(Guid.NewGuid())
        {
            [SolutionComponent.LogicalNames.ComponentType] = new OptionSetValue(SolutionComponent.Options.ComponentType.Attribute),
            [SolutionComponent.LogicalNames.ObjectId] = s_activeAttributeMetadataId,
            [SolutionComponent.LogicalNames.SolutionId] = source.ToEntityReference(),
            [SolutionComponent.LogicalNames.RootSolutionComponentId] = managedEntityComponent.Id
        };

        var inactiveAttributeComponent = new SolutionComponent(Guid.NewGuid())
        {
            [SolutionComponent.LogicalNames.ComponentType] = new OptionSetValue(SolutionComponent.Options.ComponentType.Attribute),
            [SolutionComponent.LogicalNames.ObjectId] = s_inactiveAttributeMetadataId,
            [SolutionComponent.LogicalNames.SolutionId] = source.ToEntityReference(),
            [SolutionComponent.LogicalNames.RootSolutionComponentId] = managedEntityComponent.Id
        };

        var managedActiveWorkflowComponent = new SolutionComponent(Guid.NewGuid())
        {
            [SolutionComponent.LogicalNames.ComponentType] = new OptionSetValue(SolutionComponent.Options.ComponentType.Workflow),
            [SolutionComponent.LogicalNames.ObjectId] = s_managedActiveWorkflowId,
            [SolutionComponent.LogicalNames.SolutionId] = source.ToEntityReference()
        };

        var managedInactiveWorkflowComponent = new SolutionComponent(Guid.NewGuid())
        {
            [SolutionComponent.LogicalNames.ComponentType] = new OptionSetValue(SolutionComponent.Options.ComponentType.Workflow),
            [SolutionComponent.LogicalNames.ObjectId] = s_managedInactiveWorkflowId,
            [SolutionComponent.LogicalNames.SolutionId] = source.ToEntityReference()
        };

        var unmanagedWorkflowComponent = new SolutionComponent(Guid.NewGuid())
        {
            [SolutionComponent.LogicalNames.ComponentType] = new OptionSetValue(SolutionComponent.Options.ComponentType.Workflow),
            [SolutionComponent.LogicalNames.ObjectId] = s_unmanagedWorkflowId,
            [SolutionComponent.LogicalNames.SolutionId] = source.ToEntityReference()
        };

        var workflowRecords = new[]
        {
            new Entity("workflow", s_managedActiveWorkflowId) { ["ismanaged"] = true },
            new Entity("workflow", s_managedInactiveWorkflowId) { ["ismanaged"] = true },
            new Entity("workflow", s_unmanagedWorkflowId) { ["ismanaged"] = false }
        };

        var activeLayers = new[]
        {
            new MsdynComponentlayer(Guid.NewGuid())
            {
                [MsdynComponentlayer.LogicalNames.MsdynSolutioncomponentname] = "Attribute",
                [MsdynComponentlayer.LogicalNames.MsdynComponentid] = $"{s_activeAttributeMetadataId:B}",
                [MsdynComponentlayer.LogicalNames.MsdynSolutionname] = "Active",
                [MsdynComponentlayer.LogicalNames.MsdynOrder] = 1
            },
            new MsdynComponentlayer(Guid.NewGuid())
            {
                [MsdynComponentlayer.LogicalNames.MsdynSolutioncomponentname] = "Workflow",
                [MsdynComponentlayer.LogicalNames.MsdynComponentid] = $"{s_managedActiveWorkflowId:B}",
                [MsdynComponentlayer.LogicalNames.MsdynSolutionname] = "Active",
                [MsdynComponentlayer.LogicalNames.MsdynOrder] = 1
            }
        };

        return
        [
            target, source,
            .. definitions,
            unmanagedEntityComponent, managedEntityComponent,
            activeAttributeComponent, inactiveAttributeComponent,
            managedActiveWorkflowComponent, managedInactiveWorkflowComponent, unmanagedWorkflowComponent,
            .. workflowRecords,
            .. activeLayers
        ];
    }
}
