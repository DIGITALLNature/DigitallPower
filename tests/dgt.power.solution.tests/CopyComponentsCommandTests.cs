// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.solution.Base;
using dgt.power.tests;
using dgt.power.tests.Extensions;
using dgt.power.tests.FakeExecutor;
using Digitall.Dataverse.Testing;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;

namespace dgt.power.solution.tests;

public class CopyComponentsCommandTests : CommandTestsBase<CopyComponentsCommand, CopyComponentsSettings>
{
    private const string TargetSolutionName = "target_solution";
    private const string SourceSolutionName = "source_solution";

    private static readonly Guid UnmanagedEntityMetadataId = Guid.NewGuid();
    private static readonly Guid ManagedEntityMetadataId = Guid.NewGuid();
    private static readonly Guid ActiveAttributeMetadataId = Guid.NewGuid();
    private static readonly Guid InactiveAttributeMetadataId = Guid.NewGuid();
    private static readonly Guid ManagedActiveWorkflowId = Guid.NewGuid();
    private static readonly Guid ManagedInactiveWorkflowId = Guid.NewGuid();
    private static readonly Guid UnmanagedWorkflowId = Guid.NewGuid();

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
        await Assert.That(byObjectId[UnmanagedEntityMetadataId].DoNotIncludeSubcomponents).IsFalse();
        // Managed table: added as skeleton only.
        await Assert.That(byObjectId[ManagedEntityMetadataId].DoNotIncludeSubcomponents).IsTrue();
        // Managed attribute with an active layer: included.
        await Assert.That(byObjectId.ContainsKey(ActiveAttributeMetadataId)).IsTrue();
        // Managed attribute without an active layer: excluded (redundant vs. the managed baseline).
        await Assert.That(byObjectId.ContainsKey(InactiveAttributeMetadataId)).IsFalse();
        // Unmanaged standalone (workflow) component: included.
        await Assert.That(byObjectId.ContainsKey(UnmanagedWorkflowId)).IsTrue();
        // Managed standalone component with an active layer: included.
        await Assert.That(byObjectId.ContainsKey(ManagedActiveWorkflowId)).IsTrue();
        // Managed standalone component without an active layer: excluded.
        await Assert.That(byObjectId.ContainsKey(ManagedInactiveWorkflowId)).IsFalse();

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
        await Assert.That(byObjectId[UnmanagedEntityMetadataId].DoNotIncludeSubcomponents).IsFalse();
        await Assert.That(byObjectId[ManagedEntityMetadataId].DoNotIncludeSubcomponents).IsTrue();
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
            .WithData(service => PrepareData(service, targetIsManaged));
    }

    private static EntityMetadata[] BuildEntityMetadata()
    {
        var activeAttribute = new AttributeMetadata { LogicalName = "dgt_active", MetadataId = ActiveAttributeMetadataId };
        activeAttribute.SetSealedPropertyValue(nameof(AttributeMetadata.IsManaged), true);

        var inactiveAttribute = new AttributeMetadata { LogicalName = "dgt_inactive", MetadataId = InactiveAttributeMetadataId };
        inactiveAttribute.SetSealedPropertyValue(nameof(AttributeMetadata.IsManaged), true);

        var unmanagedEntity = new EntityMetadata { LogicalName = "dgt_unmanaged", MetadataId = UnmanagedEntityMetadataId };
        unmanagedEntity.SetSealedPropertyValue(nameof(EntityMetadata.IsManaged), false);

        var managedEntity = new EntityMetadata { LogicalName = "isv_managed", MetadataId = ManagedEntityMetadataId };
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

    private static IEnumerable<Entity> PrepareData(FakeOrganizationServiceAsync service, bool targetIsManaged)
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
            [SolutionComponent.LogicalNames.ObjectId] = UnmanagedEntityMetadataId,
            [SolutionComponent.LogicalNames.SolutionId] = source.ToEntityReference(),
            [SolutionComponent.LogicalNames.RootComponentBehavior] = new OptionSetValue(SolutionComponent.Options.RootComponentBehavior.IncludeSubcomponents)
        };

        var managedEntityComponent = new SolutionComponent(Guid.NewGuid())
        {
            [SolutionComponent.LogicalNames.ComponentType] = new OptionSetValue(SolutionComponent.Options.ComponentType.Entity),
            [SolutionComponent.LogicalNames.ObjectId] = ManagedEntityMetadataId,
            [SolutionComponent.LogicalNames.SolutionId] = source.ToEntityReference(),
            [SolutionComponent.LogicalNames.RootComponentBehavior] = new OptionSetValue(SolutionComponent.Options.RootComponentBehavior.DoNotIncludeSubcomponents)
        };

        var activeAttributeComponent = new SolutionComponent(Guid.NewGuid())
        {
            [SolutionComponent.LogicalNames.ComponentType] = new OptionSetValue(SolutionComponent.Options.ComponentType.Attribute),
            [SolutionComponent.LogicalNames.ObjectId] = ActiveAttributeMetadataId,
            [SolutionComponent.LogicalNames.SolutionId] = source.ToEntityReference(),
            [SolutionComponent.LogicalNames.RootSolutionComponentId] = managedEntityComponent.Id
        };

        var inactiveAttributeComponent = new SolutionComponent(Guid.NewGuid())
        {
            [SolutionComponent.LogicalNames.ComponentType] = new OptionSetValue(SolutionComponent.Options.ComponentType.Attribute),
            [SolutionComponent.LogicalNames.ObjectId] = InactiveAttributeMetadataId,
            [SolutionComponent.LogicalNames.SolutionId] = source.ToEntityReference(),
            [SolutionComponent.LogicalNames.RootSolutionComponentId] = managedEntityComponent.Id
        };

        var managedActiveWorkflowComponent = new SolutionComponent(Guid.NewGuid())
        {
            [SolutionComponent.LogicalNames.ComponentType] = new OptionSetValue(SolutionComponent.Options.ComponentType.Workflow),
            [SolutionComponent.LogicalNames.ObjectId] = ManagedActiveWorkflowId,
            [SolutionComponent.LogicalNames.SolutionId] = source.ToEntityReference()
        };

        var managedInactiveWorkflowComponent = new SolutionComponent(Guid.NewGuid())
        {
            [SolutionComponent.LogicalNames.ComponentType] = new OptionSetValue(SolutionComponent.Options.ComponentType.Workflow),
            [SolutionComponent.LogicalNames.ObjectId] = ManagedInactiveWorkflowId,
            [SolutionComponent.LogicalNames.SolutionId] = source.ToEntityReference()
        };

        var unmanagedWorkflowComponent = new SolutionComponent(Guid.NewGuid())
        {
            [SolutionComponent.LogicalNames.ComponentType] = new OptionSetValue(SolutionComponent.Options.ComponentType.Workflow),
            [SolutionComponent.LogicalNames.ObjectId] = UnmanagedWorkflowId,
            [SolutionComponent.LogicalNames.SolutionId] = source.ToEntityReference()
        };

        var workflowRecords = new[]
        {
            new Entity("workflow", ManagedActiveWorkflowId) { ["ismanaged"] = true },
            new Entity("workflow", ManagedInactiveWorkflowId) { ["ismanaged"] = true },
            new Entity("workflow", UnmanagedWorkflowId) { ["ismanaged"] = false }
        };

        var activeLayers = new[]
        {
            new MsdynComponentlayer(Guid.NewGuid())
            {
                [MsdynComponentlayer.LogicalNames.MsdynSolutioncomponentname] = "Attribute",
                [MsdynComponentlayer.LogicalNames.MsdynComponentid] = $"{ActiveAttributeMetadataId:B}",
                [MsdynComponentlayer.LogicalNames.MsdynSolutionname] = "Active",
                [MsdynComponentlayer.LogicalNames.MsdynOrder] = 1
            },
            new MsdynComponentlayer(Guid.NewGuid())
            {
                [MsdynComponentlayer.LogicalNames.MsdynSolutioncomponentname] = "Workflow",
                [MsdynComponentlayer.LogicalNames.MsdynComponentid] = $"{ManagedActiveWorkflowId:B}",
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
