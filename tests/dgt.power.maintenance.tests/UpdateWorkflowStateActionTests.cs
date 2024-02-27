// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.maintenance.Logic;
using dgt.power.tests;
using dgt.power.tests.Extensions;
using Microsoft.Xrm.Sdk;

namespace dgt.power.maintenance.tests;

public class UpdateWorkflowStateActionTests : CommandTestsBase<UpdateWorkflowState, UpdateWorkflowState.Settings>
{
    public UpdateWorkflowStateActionTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper) { }

    [Fact]
    private void DefaultShouldActivateWorkflows()
    {
        var draftAction = new Workflow(Guid.NewGuid())
        {
            UniqueName = "Draft -> Activated",
            Category = new OptionSetValue(Workflow.Options.Category.Action),
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Draft),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Draft),
        };
        var suspendedAction = new Workflow(Guid.NewGuid())
        {
            UniqueName = "Suspended -> Activated",
            Category = new OptionSetValue(Workflow.Options.Category.Action),
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Suspended),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.CompanyDLPViolation),
        };
        var activatedAction = new Workflow(Guid.NewGuid())
        {
            UniqueName = "Activated -> Activated",
            Category = new OptionSetValue(Workflow.Options.Category.Action),
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Activated),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Activated),
        };

        var context = GetBuilder()
            .WithData(draftAction)
            .WithData(suspendedAction)
            .WithData(activatedAction)
            .Build();

        context.Execute(new UpdateWorkflowState.Settings
        {
            Config = GetResourcePath("empty.json"),
        }).Should().Succeed();

        var updatedDraftAction = context.DataContext.WorkflowSet.Single(w => w.Id == draftAction.Id);
        var updatedSuspendedAction = context.DataContext.WorkflowSet.Single(w => w.Id == suspendedAction.Id);
        var updatedActivatedAction = context.DataContext.WorkflowSet.Single(w => w.Id == activatedAction.Id);

        updatedDraftAction.StateCode!.Value.Should().Be(Workflow.Options.StateCode.Activated);
        updatedDraftAction.StatusCode!.Value.Should().Be(Workflow.Options.StatusCode.Activated);
        updatedSuspendedAction.StateCode!.Value.Should().Be(Workflow.Options.StateCode.Activated);
        updatedSuspendedAction.StatusCode!.Value.Should().Be(Workflow.Options.StatusCode.Activated);
        updatedActivatedAction.StateCode!.Value.Should().Be(Workflow.Options.StateCode.Activated);
        updatedActivatedAction.StatusCode!.Value.Should().Be(Workflow.Options.StatusCode.Activated);
    }

    [Fact]
    private void ShouldDeactivateFlows()
    {
        var draftAction = new Workflow(Guid.NewGuid())
        {
            UniqueName = "Draft -> Draft",
            Category = new OptionSetValue(Workflow.Options.Category.Action),
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Draft),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Draft),
        };
        var suspendedAction = new Workflow(Guid.NewGuid())
        {
            UniqueName = "Suspended -> Suspended",
            Category = new OptionSetValue(Workflow.Options.Category.Action),
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Suspended),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.CompanyDLPViolation),
        };
        var activatedAction = new Workflow(Guid.NewGuid())
        {
            UniqueName = "Activated -> Draft",
            Category = new OptionSetValue(Workflow.Options.Category.Action),
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Activated),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Activated),
        };

        var context = GetBuilder()
            .WithData(draftAction)
            .WithData(suspendedAction)
            .WithData(activatedAction)
            .Build();

        context.Execute(new UpdateWorkflowState.Settings
        {
            Config = GetResourcePath("actions-deactivate.json"),
        }).Should().Succeed();

        var updatedDraftAction = context.DataContext.WorkflowSet.Single(w => w.Id == draftAction.Id);
        var updatedSuspendedAction = context.DataContext.WorkflowSet.Single(w => w.Id == suspendedAction.Id);
        var updatedActivatedAction = context.DataContext.WorkflowSet.Single(w => w.Id == activatedAction.Id);

        updatedDraftAction.StateCode!.Value.Should().Be(Workflow.Options.StateCode.Draft);
        updatedDraftAction.StatusCode!.Value.Should().Be(Workflow.Options.StatusCode.Draft);
        updatedSuspendedAction.StateCode!.Value.Should().Be(Workflow.Options.StateCode.Suspended);
        updatedSuspendedAction.StatusCode!.Value.Should().Be(Workflow.Options.StatusCode.CompanyDLPViolation);
        updatedActivatedAction.StateCode!.Value.Should().Be(Workflow.Options.StateCode.Draft);
        updatedActivatedAction.StatusCode!.Value.Should().Be(Workflow.Options.StatusCode.Draft);
    }

    [Fact]
    private void ShouldOverwriteFlowOwner()
    {
        var currentOwner = new SystemUser(Guid.NewGuid())
        {
            Attributes = new AttributeCollection
            {
                { SystemUser.LogicalNames.FullName, "Current Owner" },
            },
            DomainName = "current@owner.com",
        };
        var defaultOwner = new SystemUser(Guid.NewGuid())
        {
            Attributes = new AttributeCollection
            {
                { SystemUser.LogicalNames.FullName, "Default Owner" },
                { SystemUser.LogicalNames.DomainName, "default@owner.com" },
            },
            DomainName = "default@owner.com",
        };
        var actionOwner = new SystemUser(Guid.NewGuid())
        {
            Attributes = new AttributeCollection
            {
                { SystemUser.LogicalNames.FullName, "Flow Owner" },
                { SystemUser.LogicalNames.DomainName, "flow@owner.com" },
            },
            DomainName = "flow@owner.com",
        };

        var currentToDefaultAction = new Workflow(Guid.NewGuid())
        {
            UniqueName = "Current -> Default",
            Category = new OptionSetValue(Workflow.Options.Category.Action),
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Draft),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Draft),
            OwnerId = currentOwner.ToEntityReference(),
        };
        var currentToActionAction = new Workflow(Guid.NewGuid())
        {
            UniqueName = "Current -> Flow",
            Category = new OptionSetValue(Workflow.Options.Category.Action),
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Suspended),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.CompanyDLPViolation),
            OwnerId = currentOwner.ToEntityReference(),
        };
        var currentToCurrentSpecifiedAction = new Workflow(Guid.NewGuid())
        {
            UniqueName = "Current -> Current (specified)",
            Category = new OptionSetValue(Workflow.Options.Category.Action),
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Activated),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Activated),
            OwnerId = currentOwner.ToEntityReference(),
        };
        var currentToCurrentFallbackAction = new Workflow(Guid.NewGuid())
        {
            UniqueName = "Current -> Current (fallback)",
            Category = new OptionSetValue(Workflow.Options.Category.Action),
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Activated),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Activated),
            OwnerId = currentOwner.ToEntityReference(),
        };
        var currentToDefaultSpecifiedAction = new Workflow(Guid.NewGuid())
        {
            UniqueName = "Current -> Default (specified)",
            Category = new OptionSetValue(Workflow.Options.Category.Action),
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Activated),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Activated),
            OwnerId = currentOwner.ToEntityReference(),
        };

        var context = GetBuilder()
            .WithData(currentOwner)
            .WithData(defaultOwner)
            .WithData(actionOwner)
            .WithData(currentToDefaultAction)
            .WithData(currentToActionAction)
            .WithData(currentToCurrentSpecifiedAction)
            .WithData(currentToCurrentFallbackAction)
            .WithData(currentToDefaultSpecifiedAction)
            .Build();

        context.Execute(new UpdateWorkflowState.Settings
        {
            Config = GetResourcePath("actions-owner.json"),
        }).Should().Succeed();

        var updatedCurrentToDefaultAction = context.DataContext.WorkflowSet.Single(w => w.Id == currentToDefaultAction.Id);
        updatedCurrentToDefaultAction.OwnerId.Should().Be(defaultOwner.ToEntityReference());

        var updatedCurrentToActionAction = context.DataContext.WorkflowSet.Single(w => w.Id == currentToActionAction.Id);
        updatedCurrentToActionAction.OwnerId.Should().Be(actionOwner.ToEntityReference());

        var updatedCurrentToCurrentSpecifiedAction = context.DataContext.WorkflowSet.Single(w => w.Id == currentToCurrentSpecifiedAction.Id);
        updatedCurrentToCurrentSpecifiedAction.OwnerId.Should().Be(currentOwner.ToEntityReference());

        var updatedCurrentToCurrentFallbackAction = context.DataContext.WorkflowSet.Single(w => w.Id == currentToCurrentFallbackAction.Id);
        updatedCurrentToCurrentFallbackAction.OwnerId.Should().Be(currentOwner.ToEntityReference());

        var updatedCurrentToDefaultSpecifiedAction = context.DataContext.WorkflowSet.Single(w => w.Id == currentToDefaultSpecifiedAction.Id);
        updatedCurrentToDefaultSpecifiedAction.OwnerId.Should().Be(defaultOwner.ToEntityReference());
    }

    [Theory]
    [InlineData("filter-solution.json")]
    [InlineData("filter-solution-pattern.json")]
    private void ShouldFilterFlowsBySolution(string config)
    {
        var matchingSolution = new Solution(Guid.NewGuid())
        {
            UniqueName = "match",
        };
        var otherSolution = new Solution(Guid.NewGuid())
        {
            UniqueName = "other",
        };

        var actionInMatchingSolution = new Workflow(Guid.NewGuid())
        {
            UniqueName = "Flow in matching",
            Category = new OptionSetValue(Workflow.Options.Category.Action),
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Draft),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Draft),
        };
        var actionInOtherSolution = new Workflow(Guid.NewGuid())
        {
            UniqueName = "Flow in other",
            Category = new OptionSetValue(Workflow.Options.Category.Action),
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Draft),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Draft),
        };

        var matchingComponent = new SolutionComponent(Guid.NewGuid())
        {
            Attributes = new AttributeCollection
            {
                { SolutionComponent.LogicalNames.SolutionId, matchingSolution.Id },
                { SolutionComponent.LogicalNames.ObjectId, actionInMatchingSolution.Id },
            },
        };
        var otherComponent = new SolutionComponent(Guid.NewGuid())
        {
            Attributes = new AttributeCollection
            {
                { SolutionComponent.LogicalNames.SolutionId, otherSolution.Id },
                { SolutionComponent.LogicalNames.ObjectId, actionInOtherSolution.Id },
            },
        };

        var context = GetBuilder()
            .WithData(matchingSolution)
            .WithData(otherSolution)
            .WithData(actionInMatchingSolution)
            .WithData(actionInOtherSolution)
            .WithData(matchingComponent)
            .WithData(otherComponent)
            .Build();

        context.Execute(new UpdateWorkflowState.Settings
        {
            Config = GetResourcePath(config),
        }).Should().Succeed();

        var matchingAction = context.DataContext.WorkflowSet.Single(w => w.Id == actionInMatchingSolution.Id);
        matchingAction.StateCode!.Value.Should().Be(Workflow.Options.StateCode.Activated);
        matchingAction.StatusCode!.Value.Should().Be(Workflow.Options.StatusCode.Activated);

        var otherAction = context.DataContext.WorkflowSet.Single(w => w.Id == actionInOtherSolution.Id);
        otherAction.StateCode!.Value.Should().Be(Workflow.Options.StateCode.Draft);
        otherAction.StatusCode!.Value.Should().Be(Workflow.Options.StatusCode.Draft);
    }
}
