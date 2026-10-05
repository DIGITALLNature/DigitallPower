// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.solution.Base;
using Digitall.Dataverse.Testing;
using dgt.power.tests.Extensions;
using dgt.power.tests.FakeExecutor;
using System.ServiceModel;
using Microsoft.Crm.Sdk.Messages;

namespace dgt.power.solution.tests;

[NotInParallel("AnsiConsole")]
public class CopyComponentsCommandTests : CommandTestsBase<CopyComponentsCommand, CopyComponentsSettings>
{
    private const string TargetSolutionName = "target_solution";
    private const string SourceSolutionName = "source_solution";
    private const int AppElementComponentType = 10400;

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

    [Test]
    [Arguments(AppHandling.Skip)]
    [Arguments(AppHandling.Strip)]
    [Arguments(AppHandling.Allow)]
    public async Task ShouldHandleModelDrivenAppAccordingToAppsMode(AppHandling appHandling)
    {
        TestConsole.Profile.Width = 500;
        var appId = Guid.NewGuid();
        var expandedSubcomponentId = Guid.NewGuid();
        var preExistingTargetComponentId = Guid.NewGuid();
        var targetSolution = new Solution(Guid.NewGuid()) { UniqueName = TargetSolutionName, [Solution.LogicalNames.IsManaged] = false };

        FakeOrganizationServiceAsync? service = null;
        var addedComponents = new List<AddSolutionComponentRequest>();
        var removedComponents = new List<RemoveSolutionComponentRequest>();
        var context = new CommandTestContextBuilder<CopyComponentsCommand, CopyComponentsSettings>()
            .WithAnsiConsole(TestConsole)
            .WithFakeMessageExecutor(new RetrieveEntityExecutor())
            .WithMetaData(BuildAppModuleComponentMetadata())
            .WithData(fake =>
            {
                service = fake;
                return PrepareAppData(appId, preExistingTargetComponentId, targetSolution);
            })
            .WithExecutionMock<AddSolutionComponentRequest>(request =>
            {
                var add = (AddSolutionComponentRequest)request;
                addedComponents.Add(add);

                // Adding an app-bound component (here: AppElement) makes Dataverse pull in the app's subcomponents - already
                // before the app itself is added explicitly.
                if (add.ComponentType != AppElementComponentType)
                {
                    return new AddSolutionComponentResponse();
                }

                service!.Create(new SolutionComponent(Guid.NewGuid())
                {
                    [SolutionComponent.LogicalNames.ComponentType] = new OptionSetValue(SolutionComponent.Options.ComponentType.Entity),
                    [SolutionComponent.LogicalNames.ObjectId] = expandedSubcomponentId,
                    [SolutionComponent.LogicalNames.SolutionId] = targetSolution.ToEntityReference()
                });
                return new AddSolutionComponentResponse();
            })
            .WithExecutionMock<RemoveSolutionComponentRequest>(request =>
            {
                removedComponents.Add((RemoveSolutionComponentRequest)request);
                return new RemoveSolutionComponentResponse();
            })
            .Build();

        await context
            .Execute(new CopyComponentsSettings { Target = TargetSolutionName, Source = SourceSolutionName, Apps = appHandling })
            .Succeed();

        if (appHandling == AppHandling.Skip)
        {
            await Assert.That(addedComponents).IsEmpty();
            await Assert.That(removedComponents).IsEmpty();
            return;
        }

        // App, app setting, app module component and app element are all copied; the app comes last.
        await Assert.That(addedComponents.Count).IsEqualTo(4);
        await Assert.That(addedComponents[^1].ComponentId).IsEqualTo(appId);
        await Assert.That(addedComponents[^1].ComponentType).IsEqualTo(CopyComponentsContext.AppModuleComponentType);
        // The platform rejects DoNotIncludeSubcomponents=true on anything but Entity roots.
        await Assert.That(addedComponents[^1].DoNotIncludeSubcomponents).IsFalse();
        await Assert.That(addedComponents[^1].AddRequiredComponents).IsFalse();

        if (appHandling == AppHandling.Allow)
        {
            await Assert.That(removedComponents).IsEmpty();
            return;
        }

        // Only the row the platform added on its own is removed - not the app itself, not rows that were already in the target.
        await Assert.That(removedComponents.Count).IsEqualTo(1);
        await Assert.That(removedComponents[0].ComponentId).IsEqualTo(expandedSubcomponentId);
        await Assert.That(removedComponents[0].ComponentType).IsEqualTo(SolutionComponent.Options.ComponentType.Entity);
        await Assert.That(removedComponents[0].SolutionUniqueName).IsEqualTo(TargetSolutionName);

        // The run explains what it found, so a strip that finds nothing can be troubleshot from the output alone.
        await Assert.That(TestConsole.Output).Contains("(1 new): 1 removed, 0 already gone, 0 kept because they are part of the plan");
    }

    [Test]
    public async Task ShouldIgnoreAlreadyRemovedSubcomponentsAndNameTypesWhenStrippingApps()
    {
        TestConsole.Profile.Width = 500;
        const int savedQueryComponentType = 26;
        var appId = Guid.NewGuid();
        var tableId = Guid.NewGuid();
        var viewId = Guid.NewGuid();
        var targetSolution = new Solution(Guid.NewGuid()) { UniqueName = TargetSolutionName, [Solution.LogicalNames.IsManaged] = false };

        FakeOrganizationServiceAsync? service = null;
        var removedComponents = new List<RemoveSolutionComponentRequest>();
        var viewRowId = Guid.NewGuid();
        var context = new CommandTestContextBuilder<CopyComponentsCommand, CopyComponentsSettings>()
            .WithAnsiConsole(TestConsole)
            .WithFakeMessageExecutor(new RetrieveEntityExecutor())
            .WithMetaData(BuildAppModuleComponentMetadata())
            .WithData(fake =>
            {
                service = fake;
                return
                [
                    .. PrepareAppData(appId, Guid.NewGuid(), targetSolution),
                    new SolutionComponentDefinition(Guid.NewGuid()) { ["solutioncomponenttype"] = SolutionComponent.Options.ComponentType.Entity, ["name"] = "Entity" },
                    new SolutionComponentDefinition(Guid.NewGuid()) { ["solutioncomponenttype"] = savedQueryComponentType, ["name"] = "SavedQuery" }
                ];
            })
            .WithExecutionMock<AddSolutionComponentRequest>(request =>
            {
                if (((AddSolutionComponentRequest)request).ComponentType != AppElementComponentType)
                {
                    return new AddSolutionComponentResponse();
                }

                // The platform adds a table together with one of its views.
                service!.Create(new SolutionComponent(Guid.NewGuid())
                {
                    [SolutionComponent.LogicalNames.ComponentType] = new OptionSetValue(SolutionComponent.Options.ComponentType.Entity),
                    [SolutionComponent.LogicalNames.ObjectId] = tableId,
                    [SolutionComponent.LogicalNames.SolutionId] = targetSolution.ToEntityReference()
                });
                service.Create(new SolutionComponent(viewRowId)
                {
                    [SolutionComponent.LogicalNames.ComponentType] = new OptionSetValue(savedQueryComponentType),
                    [SolutionComponent.LogicalNames.ObjectId] = viewId,
                    [SolutionComponent.LogicalNames.SolutionId] = targetSolution.ToEntityReference()
                });
                return new AddSolutionComponentResponse();
            })
            .WithExecutionMock<RemoveSolutionComponentRequest>(request =>
            {
                var remove = (RemoveSolutionComponentRequest)request;
                removedComponents.Add(remove);
                if (remove.ComponentType != savedQueryComponentType)
                {
                    return new RemoveSolutionComponentResponse();
                }

                // The view vanished with its table before it could be removed on its own.
                service!.Delete(SolutionComponent.EntityLogicalName, viewRowId);
                throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault(), "Cannot find solution component");
            })
            .Build();

        await context
            .Execute(new CopyComponentsSettings { Target = TargetSolutionName, Source = SourceSolutionName, Apps = AppHandling.Strip })
            .Succeed();

        // Tables are removed last so their views are not stripped twice; a fault for something already gone is ignored.
        await Assert.That(removedComponents.Count).IsEqualTo(2);
        await Assert.That(removedComponents[^1].ComponentType).IsEqualTo(SolutionComponent.Options.ComponentType.Entity);
        await Assert.That(TestConsole.Output).Contains("1 removed, 1 already gone");
        await Assert.That(TestConsole.Output).Contains($"Entity: {tableId}");
    }

    private static EntityMetadata BuildAppModuleComponentMetadata()
    {
        var appModuleComponentEntity = new EntityMetadata { LogicalName = "appmodulecomponent" };
        appModuleComponentEntity.SetSealedPropertyValue(nameof(EntityMetadata.PrimaryIdAttribute), "appmodulecomponentid");
        appModuleComponentEntity.SetAttributeCollection([new AttributeMetadata { LogicalName = "ismanaged" }]);
        return appModuleComponentEntity;
    }

    private static IEnumerable<Entity> PrepareAppData(Guid appId, Guid preExistingTargetComponentId, Solution target)
    {
        var source = new Solution(Guid.NewGuid()) { UniqueName = SourceSolutionName, [Solution.LogicalNames.IsManaged] = false };

        var definition = new SolutionComponentDefinition(Guid.NewGuid())
        {
            ["solutioncomponenttype"] = CopyComponentsContext.AppModuleComponentType,
            ["name"] = "AppModule"
        };

        var appComponent = new SolutionComponent(Guid.NewGuid())
        {
            [SolutionComponent.LogicalNames.ComponentType] = new OptionSetValue(CopyComponentsContext.AppModuleComponentType),
            [SolutionComponent.LogicalNames.ObjectId] = appId,
            [SolutionComponent.LogicalNames.SolutionId] = source.ToEntityReference()
        };

        var preExistingTargetComponent = new SolutionComponent(Guid.NewGuid())
        {
            [SolutionComponent.LogicalNames.ComponentType] = new OptionSetValue(SolutionComponent.Options.ComponentType.Entity),
            [SolutionComponent.LogicalNames.ObjectId] = preExistingTargetComponentId,
            [SolutionComponent.LogicalNames.SolutionId] = target.ToEntityReference()
        };

        // Componenttypes above 10000 are environment-specific, so app-bound components are recognized by definition name / backing table.
        var appBoundDefinitions = new[]
        {
            new SolutionComponentDefinition(Guid.NewGuid()) { ["solutioncomponenttype"] = 10246, ["name"] = "AppSetting" },
            new SolutionComponentDefinition(Guid.NewGuid()) { ["solutioncomponenttype"] = 10311, ["name"] = "SomethingElse", ["primaryentityname"] = "appmodulecomponent" },
            new SolutionComponentDefinition(Guid.NewGuid()) { ["solutioncomponenttype"] = AppElementComponentType, ["name"] = "AppElement" }
        };

        var appBoundComponents = appBoundDefinitions.Select(appBoundDefinition => new SolutionComponent(Guid.NewGuid())
        {
            [SolutionComponent.LogicalNames.ComponentType] = new OptionSetValue(appBoundDefinition.SolutionComponentType!.Value),
            [SolutionComponent.LogicalNames.ObjectId] = Guid.NewGuid(),
            [SolutionComponent.LogicalNames.SolutionId] = source.ToEntityReference()
        });

        return [target, source, definition, appComponent, preExistingTargetComponent, .. appBoundDefinitions, .. appBoundComponents];
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
