// Copyright (c) DIGITALL Nature. All rights reserved

using dgt.power.dataverse;
using dgt.power.process.Logic;
using Digitall.Dataverse.Testing;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;

namespace dgt.power.process.tests.Logic;

public class ProcessSolutionSelectorTests
{
    [Test]
    public async Task SelectAsync_CombinesExactSolutionsAndIndirectCompleteTableRules()
    {
        var service = CreateService();
        var firstSolution = Guid.NewGuid();
        var secondSolution = Guid.NewGuid();
        var tableId = Guid.NewGuid();
        var deltaTableId = Guid.NewGuid();
        service.Create(new Entity("solution", firstSolution) { ["uniquename"] = "First" });
        service.Create(new Entity("solution", secondSolution) { ["uniquename"] = "Second" });
        service.State.EntityMetadata["account"] = new EntityMetadata { MetadataId = tableId, LogicalName = "account" };
        service.State.EntityMetadata["contact"] = new EntityMetadata { MetadataId = deltaTableId, LogicalName = "contact" };

        var classic = AddProcess(service, 0, "classic");
        var unsupportedDialog = AddProcess(service, 1, "dialog");
        var includedRule = AddProcess(service, 2, "account", secondSolution);
        var deltaOnlyRule = AddProcess(service, 2, "contact");
        var unsupportedDesktop = AddProcess(service, 6, "desktop");
        service.Create(Component(firstSolution, classic, SolutionComponent.Options.ComponentType.Workflow));
        service.Create(Component(firstSolution, unsupportedDialog, SolutionComponent.Options.ComponentType.Workflow));
        service.Create(Component(firstSolution, unsupportedDesktop, SolutionComponent.Options.ComponentType.Workflow));
        service.Create(Component(secondSolution, includedRule, SolutionComponent.Options.ComponentType.Workflow));
        service.Create(Component(firstSolution, Guid.NewGuid(), SolutionComponent.Options.ComponentType.Entity, tableId,
            SolutionComponent.Options.RootComponentBehavior.IncludeSubcomponents));
        service.Create(Component(secondSolution, Guid.NewGuid(), SolutionComponent.Options.ComponentType.Entity, deltaTableId,
            SolutionComponent.Options.RootComponentBehavior.DoNotIncludeSubcomponents));

        var selected = await new ProcessSolutionSelector(service).SelectAsync(["First", "Second"]);
        var ids = selected.Select(process => process.Id).ToHashSet();

        await Assert.That(ids).Contains(classic);
        await Assert.That(ids).Contains(includedRule);
        await Assert.That(ids).DoesNotContain(unsupportedDialog);
        await Assert.That(ids).DoesNotContain(unsupportedDesktop);
        await Assert.That(ids).DoesNotContain(deltaOnlyRule);
        await Assert.That(selected.Count(process => process.Id == includedRule)).IsEqualTo(1);
    }

    [Test]
    public async Task SelectAsync_UnknownSolutionThrows()
    {
        var service = CreateService();

        await Assert.That(async () => await new ProcessSolutionSelector(service).SelectAsync(["Missing"]))
            .Throws<InvalidOperationException>();
    }

    [Test]
    public async Task SelectAsync_MatchesNamesCaseInsensitivelyAndTreatsWildcardsAsLiteral()
    {
        var service = CreateService();
        service.Create(new Entity("solution", Guid.NewGuid()) { ["uniquename"] = "ContosoCore" });
        var selector = new ProcessSolutionSelector(service);

        var selected = await selector.SelectAsync(["contosocore"]);

        await Assert.That(selected).IsEmpty();
        await Assert.That(async () => await selector.SelectAsync(["Contoso*"]))
            .Throws<InvalidOperationException>();
    }

    private static FakeOrganizationServiceAsync CreateService()
    {
        var service = new FakeOrganizationServiceAsync();
        service.AddDefaultRequests();
        return service;
    }

    private static Guid AddProcess(
        FakeOrganizationServiceAsync service,
        int category,
        string primaryEntity,
        Guid? solutionId = null)
    {
        var id = Guid.NewGuid();
        service.Create(new Workflow(id)
        {
            Category = new OptionSetValue(category),
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            PrimaryEntity = primaryEntity
        });
        if (solutionId.HasValue)
        {
            service.Create(Component(solutionId.Value, id, SolutionComponent.Options.ComponentType.Workflow));
        }

        return id;
    }

    private static Entity Component(
        Guid solutionId,
        Guid componentId,
        int componentType,
        Guid? objectId = null,
        int? behavior = null) =>
        new(SolutionComponent.EntityLogicalName, Guid.NewGuid())
        {
            [SolutionComponent.LogicalNames.ObjectId] = objectId ?? componentId,
            [SolutionComponent.LogicalNames.ComponentType] = new OptionSetValue(componentType),
            [SolutionComponent.LogicalNames.SolutionId] = new EntityReference("solution", solutionId),
            [SolutionComponent.LogicalNames.RootComponentBehavior] = behavior.HasValue ? new OptionSetValue(behavior.Value) : null
        };
}
