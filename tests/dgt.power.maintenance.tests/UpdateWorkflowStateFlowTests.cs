// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.maintenance.Logic;
using dgt.power.tests;
using dgt.power.tests.Extensions;
using Microsoft.Xrm.Sdk;

namespace dgt.power.maintenance.tests;

[NotInParallel("AnsiConsole")]
public class UpdateWorkflowStateFlowTests : CommandTestsBase<UpdateWorkflowState, UpdateWorkflowState.Settings>
{
    [Test]
    [Arguments(Workflow.Options.Category.Workflow_)]
    [Arguments(Workflow.Options.Category.ModernFlow)]
    public async Task DefaultShouldActivateFlows(int category)
    {
        var draftFlow = new Workflow(Guid.NewGuid())
        {
            Name = "Draft -> Activated",
            Category = new OptionSetValue(category),
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Draft),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Draft)
        };
        var suspendedFlow = new Workflow(Guid.NewGuid())
        {
            Name = "Suspended -> Activated",
            Category = new OptionSetValue(category),
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Suspended),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.CompanyDLPViolation)
        };
        var activatedFlow = new Workflow(Guid.NewGuid())
        {
            Name = "Activated -> Activated",
            Category = new OptionSetValue(category),
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Activated),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Activated)
        };

        var context = GetBuilder()
            .WithData(draftFlow)
            .WithData(suspendedFlow)
            .WithData(activatedFlow)
            .Build();

        await context.Execute(new UpdateWorkflowState.Settings
        {
            Config = GetResourcePath("empty.json")
        }).Succeed();

        var updatedDraftFlow = context.DataContext.WorkflowSet.Single(w => w.Id == draftFlow.Id);
        var updatedSuspendedFlow = context.DataContext.WorkflowSet.Single(w => w.Id == suspendedFlow.Id);
        var updatedActivatedFlow = context.DataContext.WorkflowSet.Single(w => w.Id == activatedFlow.Id);

        await Assert.That(updatedDraftFlow.StateCode!.Value).IsEqualTo(Workflow.Options.StateCode.Activated);
        await Assert.That(updatedDraftFlow.StatusCode!.Value).IsEqualTo(Workflow.Options.StatusCode.Activated);
        await Assert.That(updatedSuspendedFlow.StateCode!.Value).IsEqualTo(Workflow.Options.StateCode.Activated);
        await Assert.That(updatedSuspendedFlow.StatusCode!.Value).IsEqualTo(Workflow.Options.StatusCode.Activated);
        await Assert.That(updatedActivatedFlow.StateCode!.Value).IsEqualTo(Workflow.Options.StateCode.Activated);
        await Assert.That(updatedActivatedFlow.StatusCode!.Value).IsEqualTo(Workflow.Options.StatusCode.Activated);
    }

    [Test]
    [Arguments(Workflow.Options.Category.Workflow_)]
    [Arguments(Workflow.Options.Category.ModernFlow)]
    public async Task ShouldDeactivateFlows(int category)
    {
        var draftFlow = new Workflow(Guid.NewGuid())
        {
            Name = "Draft -> Draft",
            Category = new OptionSetValue(category),
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Draft),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Draft)
        };
        var suspendedFlow = new Workflow(Guid.NewGuid())
        {
            Name = "Suspended -> Suspended",
            Category = new OptionSetValue(category),
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Suspended),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.CompanyDLPViolation)
        };
        var activatedFlow = new Workflow(Guid.NewGuid())
        {
            Name = "Activated -> Draft",
            Category = new OptionSetValue(category),
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Activated),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Activated)
        };

        var context = GetBuilder()
            .WithData(draftFlow)
            .WithData(suspendedFlow)
            .WithData(activatedFlow)
            .Build();

        await context.Execute(new UpdateWorkflowState.Settings
        {
            Config = GetResourcePath("flows-deactivate.json")
        }).Succeed();

        var updatedDraftFlow = context.DataContext.WorkflowSet.Single(w => w.Id == draftFlow.Id);
        var updatedSuspendedFlow = context.DataContext.WorkflowSet.Single(w => w.Id == suspendedFlow.Id);
        var updatedActivatedFlow = context.DataContext.WorkflowSet.Single(w => w.Id == activatedFlow.Id);

        await Assert.That(updatedDraftFlow.StateCode!.Value).IsEqualTo(Workflow.Options.StateCode.Draft);
        await Assert.That(updatedDraftFlow.StatusCode!.Value).IsEqualTo(Workflow.Options.StatusCode.Draft);
        await Assert.That(updatedSuspendedFlow.StateCode!.Value).IsEqualTo(Workflow.Options.StateCode.Suspended);
        await Assert.That(updatedSuspendedFlow.StatusCode!.Value).IsEqualTo(Workflow.Options.StatusCode.CompanyDLPViolation);
        await Assert.That(updatedActivatedFlow.StateCode!.Value).IsEqualTo(Workflow.Options.StateCode.Draft);
        await Assert.That(updatedActivatedFlow.StatusCode!.Value).IsEqualTo(Workflow.Options.StatusCode.Draft);
    }

    [Test]
    [Arguments(Workflow.Options.Category.Workflow_)]
    [Arguments(Workflow.Options.Category.ModernFlow)]
    public async Task ShouldOverwriteFlowOwner(int category)
    {
        var currentOwner = new SystemUser(Guid.NewGuid())
        {
            Attributes = new AttributeCollection
            {
                { SystemUser.LogicalNames.FullName, "Current Owner" }
            },
            DomainName = "current@owner.com"
        };
        var defaultOwner = new SystemUser(Guid.NewGuid())
        {
            Attributes = new AttributeCollection
            {
                { SystemUser.LogicalNames.FullName, "Default Owner" },
                { SystemUser.LogicalNames.DomainName, "default@owner.com" }
            },
            DomainName = "default@owner.com"
        };
        var flowOwner = new SystemUser(Guid.NewGuid())
        {
            Attributes = new AttributeCollection
            {
                { SystemUser.LogicalNames.FullName, "Flow Owner" },
                { SystemUser.LogicalNames.DomainName, "flow@owner.com" }
            },
            DomainName = "flow@owner.com"
        };

        var currentToDefaultFlow = new Workflow(Guid.NewGuid())
        {
            Name = "Current -> Default",
            Category = new OptionSetValue(category),
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Draft),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Draft),
            OwnerId = currentOwner.ToEntityReference()
        };
        var currentToFlowFlow = new Workflow(Guid.NewGuid())
        {
            Name = "Current -> Flow",
            Category = new OptionSetValue(category),
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Suspended),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.CompanyDLPViolation),
            OwnerId = currentOwner.ToEntityReference()
        };
        var currentToCurrentSpecifiedFlow = new Workflow(Guid.NewGuid())
        {
            Name = "Current -> Current (specified)",
            Category = new OptionSetValue(category),
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Activated),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Activated),
            OwnerId = currentOwner.ToEntityReference()
        };
        var currentToCurrentFallbackFlow = new Workflow(Guid.NewGuid())
        {
            Name = "Current -> Current (fallback)",
            Category = new OptionSetValue(category),
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Activated),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Activated),
            OwnerId = currentOwner.ToEntityReference()
        };
        var currentToDefaultSpecifiedFlow = new Workflow(Guid.NewGuid())
        {
            Name = "Current -> Default (specified)",
            Category = new OptionSetValue(category),
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Activated),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Activated),
            OwnerId = currentOwner.ToEntityReference()
        };

        var context = GetBuilder()
            .WithData(currentOwner)
            .WithData(defaultOwner)
            .WithData(flowOwner)
            .WithData(currentToDefaultFlow)
            .WithData(currentToFlowFlow)
            .WithData(currentToCurrentSpecifiedFlow)
            .WithData(currentToCurrentFallbackFlow)
            .WithData(currentToDefaultSpecifiedFlow)
            .Build();

        await context.Execute(new UpdateWorkflowState.Settings
        {
            Config = GetResourcePath("flows-owner.json")
        }).Succeed();

        var updatedCurrentToDefaultFlow = context.DataContext.WorkflowSet.Single(w => w.Id == currentToDefaultFlow.Id);
        await Assert.That(updatedCurrentToDefaultFlow.OwnerId).IsEqualTo(defaultOwner.ToEntityReference());

        var updatedCurrentToFlowFlow = context.DataContext.WorkflowSet.Single(w => w.Id == currentToFlowFlow.Id);
        await Assert.That(updatedCurrentToFlowFlow.OwnerId).IsEqualTo(flowOwner.ToEntityReference());

        var updatedCurrentToCurrentSpecifiedFlow = context.DataContext.WorkflowSet.Single(w => w.Id == currentToCurrentSpecifiedFlow.Id);
        await Assert.That(updatedCurrentToCurrentSpecifiedFlow.OwnerId).IsEqualTo(currentOwner.ToEntityReference());

        var updatedCurrentToCurrentFallbackFlow = context.DataContext.WorkflowSet.Single(w => w.Id == currentToCurrentFallbackFlow.Id);
        await Assert.That(updatedCurrentToCurrentFallbackFlow.OwnerId).IsEqualTo(currentOwner.ToEntityReference());

        var updatedCurrentToDefaultSpecifiedFlow = context.DataContext.WorkflowSet.Single(w => w.Id == currentToDefaultSpecifiedFlow.Id);
        await Assert.That(updatedCurrentToDefaultSpecifiedFlow.OwnerId).IsEqualTo(defaultOwner.ToEntityReference());
    }

    [Test]
    [Arguments("filter-solution.json", Workflow.Options.Category.Workflow_)]
    [Arguments("filter-solution.json", Workflow.Options.Category.ModernFlow)]
    [Arguments("filter-solution-pattern.json", Workflow.Options.Category.Workflow_)]
    [Arguments("filter-solution-pattern.json", Workflow.Options.Category.ModernFlow)]
    public async Task ShouldFilterFlowsBySolution(string config, int category)
    {
        var matchingSolution = new Solution(Guid.NewGuid())
        {
            UniqueName = "match"
        };
        var otherSolution = new Solution(Guid.NewGuid())
        {
            UniqueName = "other"
        };

        var flowInMatchingSolution = new Workflow(Guid.NewGuid())
        {
            Name = "Flow in matching",
            Category = new OptionSetValue(category),
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Draft),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Draft)
        };
        var flowInOtherSolution = new Workflow(Guid.NewGuid())
        {
            Name = "Flow in other",
            Category = new OptionSetValue(category),
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Draft),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Draft)
        };

        var matchingComponent = new SolutionComponent(Guid.NewGuid())
        {
            Attributes = new AttributeCollection
            {
                { SolutionComponent.LogicalNames.SolutionId, matchingSolution.Id },
                { SolutionComponent.LogicalNames.ObjectId, flowInMatchingSolution.Id }
            }
        };
        var otherComponent = new SolutionComponent(Guid.NewGuid())
        {
            Attributes = new AttributeCollection
            {
                { SolutionComponent.LogicalNames.SolutionId, otherSolution.Id },
                { SolutionComponent.LogicalNames.ObjectId, flowInOtherSolution.Id }
            }
        };

        var context = GetBuilder()
            .WithData(matchingSolution)
            .WithData(otherSolution)
            .WithData(flowInMatchingSolution)
            .WithData(flowInOtherSolution)
            .WithData(matchingComponent)
            .WithData(otherComponent)
            .Build();

        await context.Execute(new UpdateWorkflowState.Settings
        {
            Config = GetResourcePath(config)
        }).Succeed();

        var matchingFlow = context.DataContext.WorkflowSet.Single(w => w.Id == flowInMatchingSolution.Id);
        await Assert.That(matchingFlow.StateCode!.Value).IsEqualTo(Workflow.Options.StateCode.Activated);
        await Assert.That(matchingFlow.StatusCode!.Value).IsEqualTo(Workflow.Options.StatusCode.Activated);

        var otherFlow = context.DataContext.WorkflowSet.Single(w => w.Id == flowInOtherSolution.Id);
        await Assert.That(otherFlow.StateCode!.Value).IsEqualTo(Workflow.Options.StateCode.Draft);
        await Assert.That(otherFlow.StatusCode!.Value).IsEqualTo(Workflow.Options.StatusCode.Draft);
    }

    [Test]
    [Arguments("filter-publisher.json", Workflow.Options.Category.Workflow_)]
    [Arguments("filter-publisher.json", Workflow.Options.Category.ModernFlow)]
    [Arguments("filter-publisher-pattern.json", Workflow.Options.Category.Workflow_)]
    [Arguments("filter-publisher-pattern.json", Workflow.Options.Category.ModernFlow)]
    public async Task ShouldFilterFlowsByPublisher(string config, int category)
    {
        var matchingPublisher = new Publisher(Guid.NewGuid())
        {
            UniqueName = "matchingPublisher"
        };
        var otherPublisher = new Publisher(Guid.NewGuid())
        {
            UniqueName = "otherPublisher"
        };

        var matchingSolution = new Solution(Guid.NewGuid())
        {
            UniqueName = "matchingSolution",
            PublisherId = matchingPublisher.ToEntityReference()
        };
        var otherSolution = new Solution(Guid.NewGuid())
        {
            UniqueName = "otherSolution",
            PublisherId = otherPublisher.ToEntityReference()
        };

        var flowInMatchingSolution = new Workflow(Guid.NewGuid())
        {
            Name = "Flow in matching",
            Category = new OptionSetValue(category),
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Draft),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Draft)
        };
        var flowInOtherSolution = new Workflow(Guid.NewGuid())
        {
            Name = "Flow in other",
            Category = new OptionSetValue(category),
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Draft),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Draft)
        };

        var matchingComponent = new SolutionComponent(Guid.NewGuid())
        {
            Attributes = new AttributeCollection
            {
                { SolutionComponent.LogicalNames.SolutionId, matchingSolution.ToEntityReference() },
                { SolutionComponent.LogicalNames.ObjectId, flowInMatchingSolution.Id }
            }
        };
        var otherComponent = new SolutionComponent(Guid.NewGuid())
        {
            Attributes = new AttributeCollection
            {
                { SolutionComponent.LogicalNames.SolutionId, otherSolution.ToEntityReference() },
                { SolutionComponent.LogicalNames.ObjectId, flowInOtherSolution.Id }
            }
        };

        var context = GetBuilder()
            .WithData(matchingSolution)
            .WithData(otherSolution)
            .WithData(flowInMatchingSolution)
            .WithData(flowInOtherSolution)
            .WithData(matchingComponent)
            .WithData(otherComponent)
            .WithData(matchingPublisher)
            .WithData(otherPublisher)
            .Build();

        await context.Execute(new UpdateWorkflowState.Settings
        {
            Config = GetResourcePath(config)
        }).Succeed();

        var matchingFlow = context.DataContext.WorkflowSet.Single(w => w.Id == flowInMatchingSolution.Id);
        await Assert.That(matchingFlow.StateCode!.Value).IsEqualTo(Workflow.Options.StateCode.Activated);
        await Assert.That(matchingFlow.StatusCode!.Value).IsEqualTo(Workflow.Options.StatusCode.Activated);

        var otherFlow = context.DataContext.WorkflowSet.Single(w => w.Id == flowInOtherSolution.Id);
        await Assert.That(otherFlow.StateCode!.Value).IsEqualTo(Workflow.Options.StateCode.Draft);
        await Assert.That(otherFlow.StatusCode!.Value).IsEqualTo(Workflow.Options.StatusCode.Draft);
    }
}
