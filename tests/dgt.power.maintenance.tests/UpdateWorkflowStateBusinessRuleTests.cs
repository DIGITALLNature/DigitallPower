// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.maintenance.Logic;
using dgt.power.tests;
using dgt.power.tests.Extensions;
using FakeXrmEasy.Abstractions;
using FakeXrmEasy.Abstractions.FakeMessageExecutors;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;

namespace dgt.power.maintenance.tests;

[NotInParallel("AnsiConsole")]
public class UpdateWorkflowStateBusinessRuleTests : CommandTestsBase<UpdateWorkflowState, UpdateWorkflowState.Settings>
{
    [Test]
    public async Task DefaultShouldActivateBusinessRules()
    {
        var draftBusinessRule = new Workflow(Guid.NewGuid())
        {
            Name = "Draft -> Activated",
            Category = new OptionSetValue(Workflow.Options.Category.BusinessRule),
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Draft),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Draft),
        };
        var suspendedBusinessRule = new Workflow(Guid.NewGuid())
        {
            Name = "Suspended -> Activated",
            Category = new OptionSetValue(Workflow.Options.Category.BusinessRule),
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Suspended),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.CompanyDLPViolation),
        };
        var activatedBusinessRule = new Workflow(Guid.NewGuid())
        {
            Name = "Activated -> Activated",
            Category = new OptionSetValue(Workflow.Options.Category.BusinessRule),
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Activated),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Activated),
        };

        var context = GetBuilder()
            .WithData(draftBusinessRule)
            .WithData(suspendedBusinessRule)
            .WithData(activatedBusinessRule)
            .Build();

        await context.Execute(new UpdateWorkflowState.Settings
        {
            Config = GetResourcePath("empty.json"),
        }).Succeed();

        var updatedDraftBusinessRule = context.DataContext.WorkflowSet.Single(w => w.Id == draftBusinessRule.Id);
        var updatedSuspendedBusinessRule = context.DataContext.WorkflowSet.Single(w => w.Id == suspendedBusinessRule.Id);
        var updatedActivatedBusinessRule = context.DataContext.WorkflowSet.Single(w => w.Id == activatedBusinessRule.Id);

        await Assert.That(updatedDraftBusinessRule.StateCode!.Value).IsEqualTo(Workflow.Options.StateCode.Activated);
        await Assert.That(updatedDraftBusinessRule.StatusCode!.Value).IsEqualTo(Workflow.Options.StatusCode.Activated);
        await Assert.That(updatedSuspendedBusinessRule.StateCode!.Value).IsEqualTo(Workflow.Options.StateCode.Activated);
        await Assert.That(updatedSuspendedBusinessRule.StatusCode!.Value).IsEqualTo(Workflow.Options.StatusCode.Activated);
        await Assert.That(updatedActivatedBusinessRule.StateCode!.Value).IsEqualTo(Workflow.Options.StateCode.Activated);
        await Assert.That(updatedActivatedBusinessRule.StatusCode!.Value).IsEqualTo(Workflow.Options.StatusCode.Activated);
    }

    [Test]
    public async Task DefaultShouldActivateIndirectBusinessRules()
    {
        var draftBusinessRule = new Workflow(Guid.NewGuid())
        {
            Name = "Draft -> Activated",
            Category = new OptionSetValue(Workflow.Options.Category.BusinessRule),
            PrimaryEntity = "test-table",
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Draft),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Draft),
        };
        var suspendedBusinessRule = new Workflow(Guid.NewGuid())
        {
            Name = "Suspended -> Activated",
            Category = new OptionSetValue(Workflow.Options.Category.BusinessRule),
            PrimaryEntity = "test-table",
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Suspended),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.CompanyDLPViolation),
        };
        var activatedBusinessRule = new Workflow(Guid.NewGuid())
        {
            Name = "Activated -> Activated",
            Category = new OptionSetValue(Workflow.Options.Category.BusinessRule),
            PrimaryEntity = "test-table",
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Activated),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Activated),
        };

        var ignoredBusinessRule = new Workflow(Guid.NewGuid())
        {
            Name = "Draft -> Draft",
            Category = new OptionSetValue(Workflow.Options.Category.BusinessRule),
            PrimaryEntity = "ignored-table",
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Draft),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Draft),
        };

        var solution = new Solution(Guid.NewGuid())
        {
            UniqueName = "match",
        };

        var tableComponent = new SolutionComponent(Guid.NewGuid())
        {
            [SolutionComponent.LogicalNames.ComponentType] = new OptionSetValue(SolutionComponent.Options.ComponentType.Entity),
            [SolutionComponent.LogicalNames.SolutionId] = solution.ToEntityReference(),
            [SolutionComponent.LogicalNames.ObjectId] = Guid.NewGuid(),
            [SolutionComponent.LogicalNames.RootComponentBehavior] = new OptionSetValue(SolutionComponent.Options.RootComponentBehavior.IncludeSubcomponents),
        };

        var context = GetBuilder()
            .WithData(solution)
            .WithData(tableComponent)
            .WithData(draftBusinessRule)
            .WithData(suspendedBusinessRule)
            .WithData(activatedBusinessRule)
            .WithData(ignoredBusinessRule)
            .WithFakeMessageExecutor<RetrieveEntityRequest>(new FakeRetrieveEntityRequest())
            .Build();

        await context.Execute(new UpdateWorkflowState.Settings
        {
            Config = GetResourcePath("filter-solution.json"),
        }).Succeed();

        var updatedDraftBusinessRule = context.DataContext.WorkflowSet.Single(w => w.Id == draftBusinessRule.Id);
        var updatedSuspendedBusinessRule = context.DataContext.WorkflowSet.Single(w => w.Id == suspendedBusinessRule.Id);
        var updatedActivatedBusinessRule = context.DataContext.WorkflowSet.Single(w => w.Id == activatedBusinessRule.Id);
        var updatedIgnoredBusinessRule = context.DataContext.WorkflowSet.Single(w => w.Id == ignoredBusinessRule.Id);

        await Assert.That(updatedDraftBusinessRule.StateCode!.Value).IsEqualTo(Workflow.Options.StateCode.Activated);
        await Assert.That(updatedDraftBusinessRule.StatusCode!.Value).IsEqualTo(Workflow.Options.StatusCode.Activated);
        await Assert.That(updatedSuspendedBusinessRule.StateCode!.Value).IsEqualTo(Workflow.Options.StateCode.Activated);
        await Assert.That(updatedSuspendedBusinessRule.StatusCode!.Value).IsEqualTo(Workflow.Options.StatusCode.Activated);
        await Assert.That(updatedActivatedBusinessRule.StateCode!.Value).IsEqualTo(Workflow.Options.StateCode.Activated);
        await Assert.That(updatedActivatedBusinessRule.StatusCode!.Value).IsEqualTo(Workflow.Options.StatusCode.Activated);
        await Assert.That(updatedIgnoredBusinessRule.StateCode!.Value).IsEqualTo(Workflow.Options.StateCode.Draft);
        await Assert.That(updatedIgnoredBusinessRule.StatusCode!.Value).IsEqualTo(Workflow.Options.StatusCode.Draft);
    }

    [Test]
    public async Task ShouldDeactivateFlows()
    {
        var draftBusinessRule = new Workflow(Guid.NewGuid())
        {
            Name = "Draft -> Draft",
            Category = new OptionSetValue(Workflow.Options.Category.BusinessRule),
            PrimaryEntity = "test-table",
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Draft),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Draft),
        };
        var suspendedBusinessRule = new Workflow(Guid.NewGuid())
        {
            Name = "Suspended -> Suspended",
            Category = new OptionSetValue(Workflow.Options.Category.BusinessRule),
            PrimaryEntity = "test-table",
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Suspended),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.CompanyDLPViolation),
        };
        var activatedBusinessRule = new Workflow(Guid.NewGuid())
        {
            Name = "Activated -> Draft",
            Category = new OptionSetValue(Workflow.Options.Category.BusinessRule),
            PrimaryEntity = "test-table",
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Activated),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Activated),
        };

        var context = GetBuilder()
            .WithData(draftBusinessRule)
            .WithData(suspendedBusinessRule)
            .WithData(activatedBusinessRule)
            .Build();

        await context.Execute(new UpdateWorkflowState.Settings
        {
            Config = GetResourcePath("businessrules-deactivate.json"),
        }).Succeed();

        var updatedDraftBusinessRule = context.DataContext.WorkflowSet.Single(w => w.Id == draftBusinessRule.Id);
        var updatedSuspendedBusinessRule = context.DataContext.WorkflowSet.Single(w => w.Id == suspendedBusinessRule.Id);
        var updatedActivatedBusinessRule = context.DataContext.WorkflowSet.Single(w => w.Id == activatedBusinessRule.Id);

        await Assert.That(updatedDraftBusinessRule.StateCode!.Value).IsEqualTo(Workflow.Options.StateCode.Draft);
        await Assert.That(updatedDraftBusinessRule.StatusCode!.Value).IsEqualTo(Workflow.Options.StatusCode.Draft);
        await Assert.That(updatedSuspendedBusinessRule.StateCode!.Value).IsEqualTo(Workflow.Options.StateCode.Suspended);
        await Assert.That(updatedSuspendedBusinessRule.StatusCode!.Value).IsEqualTo(Workflow.Options.StatusCode.CompanyDLPViolation);
        await Assert.That(updatedActivatedBusinessRule.StateCode!.Value).IsEqualTo(Workflow.Options.StateCode.Draft);
        await Assert.That(updatedActivatedBusinessRule.StatusCode!.Value).IsEqualTo(Workflow.Options.StatusCode.Draft);
    }

    [Test]
    public async Task ShouldOverwriteBusinessRuleOwner()
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
        var BusinessRuleOwner = new SystemUser(Guid.NewGuid())
        {
            Attributes = new AttributeCollection
            {
                { SystemUser.LogicalNames.FullName, "Flow Owner" },
                { SystemUser.LogicalNames.DomainName, "flow@owner.com" },
            },
            DomainName = "flow@owner.com",
        };

        var currentToDefaultBusinessRule = new Workflow(Guid.NewGuid())
        {
            Name = "Current -> Default",
            Category = new OptionSetValue(Workflow.Options.Category.BusinessRule),
            PrimaryEntity = "test-table",
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Draft),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Draft),
            OwnerId = currentOwner.ToEntityReference(),
        };
        var currentToBusinessRuleBusinessRule = new Workflow(Guid.NewGuid())
        {
            Name = "Current -> Flow",
            Category = new OptionSetValue(Workflow.Options.Category.BusinessRule),
            PrimaryEntity = "test-table",
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Suspended),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.CompanyDLPViolation),
            OwnerId = currentOwner.ToEntityReference(),
        };
        var currentToCurrentSpecifiedBusinessRule = new Workflow(Guid.NewGuid())
        {
            Name = "Current -> Current (specified)",
            Category = new OptionSetValue(Workflow.Options.Category.BusinessRule),
            PrimaryEntity = "test-table",
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Activated),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Activated),
            OwnerId = currentOwner.ToEntityReference(),
        };
        var currentToCurrentFallbackBusinessRule = new Workflow(Guid.NewGuid())
        {
            Name = "Current -> Current (fallback)",
            Category = new OptionSetValue(Workflow.Options.Category.BusinessRule),
            PrimaryEntity = "test-table",
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Activated),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Activated),
            OwnerId = currentOwner.ToEntityReference(),
        };
        var currentToDefaultSpecifiedBusinessRule = new Workflow(Guid.NewGuid())
        {
            Name = "Current -> Default (specified)",
            Category = new OptionSetValue(Workflow.Options.Category.BusinessRule),
            PrimaryEntity = "test-table",
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Activated),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Activated),
            OwnerId = currentOwner.ToEntityReference(),
        };

        var context = GetBuilder()
            .WithData(currentOwner)
            .WithData(defaultOwner)
            .WithData(BusinessRuleOwner)
            .WithData(currentToDefaultBusinessRule)
            .WithData(currentToBusinessRuleBusinessRule)
            .WithData(currentToCurrentSpecifiedBusinessRule)
            .WithData(currentToCurrentFallbackBusinessRule)
            .WithData(currentToDefaultSpecifiedBusinessRule)
            .Build();

        await context.Execute(new UpdateWorkflowState.Settings
        {
            Config = GetResourcePath("businessrules-owner.json"),
        }).Succeed();

        var updatedCurrentToDefaultBusinessRule = context.DataContext.WorkflowSet.Single(w => w.Id == currentToDefaultBusinessRule.Id);
        await Assert.That(updatedCurrentToDefaultBusinessRule.OwnerId).IsEqualTo(defaultOwner.ToEntityReference());

        var updatedCurrentToBusinessRuleBusinessRule = context.DataContext.WorkflowSet.Single(w => w.Id == currentToBusinessRuleBusinessRule.Id);
        await Assert.That(updatedCurrentToBusinessRuleBusinessRule.OwnerId).IsEqualTo(BusinessRuleOwner.ToEntityReference());

        var updatedCurrentToCurrentSpecifiedBusinessRule = context.DataContext.WorkflowSet.Single(w => w.Id == currentToCurrentSpecifiedBusinessRule.Id);
        await Assert.That(updatedCurrentToCurrentSpecifiedBusinessRule.OwnerId).IsEqualTo(currentOwner.ToEntityReference());

        var updatedCurrentToCurrentFallbackBusinessRule = context.DataContext.WorkflowSet.Single(w => w.Id == currentToCurrentFallbackBusinessRule.Id);
        await Assert.That(updatedCurrentToCurrentFallbackBusinessRule.OwnerId).IsEqualTo(currentOwner.ToEntityReference());

        var updatedCurrentToDefaultSpecifiedBusinessRule = context.DataContext.WorkflowSet.Single(w => w.Id == currentToDefaultSpecifiedBusinessRule.Id);
        await Assert.That(updatedCurrentToDefaultSpecifiedBusinessRule.OwnerId).IsEqualTo(defaultOwner.ToEntityReference());
    }

    [Test]
    [Arguments("filter-solution.json")]
    [Arguments("filter-solution-pattern.json")]
    public async Task ShouldFilterBusinessRulesBySolution(string config)
    {
        var matchingSolution = new Solution(Guid.NewGuid())
        {
            UniqueName = "match",
        };
        var otherSolution = new Solution(Guid.NewGuid())
        {
            UniqueName = "other",
        };

        var BusinessRuleInMatchingSolution = new Workflow(Guid.NewGuid())
        {
            Name = "Flow in matching",
            Category = new OptionSetValue(Workflow.Options.Category.BusinessRule),
            PrimaryEntity = "test-table",
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Draft),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Draft),
        };
        var BusinessRuleInOtherSolution = new Workflow(Guid.NewGuid())
        {
            Name = "Flow in other",
            Category = new OptionSetValue(Workflow.Options.Category.BusinessRule),
            PrimaryEntity = "test-table",
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Draft),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Draft),
        };

        var matchingComponent = new SolutionComponent(Guid.NewGuid())
        {
            Attributes = new AttributeCollection
            {
                { SolutionComponent.LogicalNames.SolutionId, matchingSolution.Id },
                { SolutionComponent.LogicalNames.ObjectId, BusinessRuleInMatchingSolution.Id },
            },
        };
        var otherComponent = new SolutionComponent(Guid.NewGuid())
        {
            Attributes = new AttributeCollection
            {
                { SolutionComponent.LogicalNames.SolutionId, otherSolution.Id },
                { SolutionComponent.LogicalNames.ObjectId, BusinessRuleInOtherSolution.Id },
            },
        };

        var context = GetBuilder()
            .WithData(matchingSolution)
            .WithData(otherSolution)
            .WithData(BusinessRuleInMatchingSolution)
            .WithData(BusinessRuleInOtherSolution)
            .WithData(matchingComponent)
            .WithData(otherComponent)
            .Build();

        await context.Execute(new UpdateWorkflowState.Settings
        {
            Config = GetResourcePath(config),
        }).Succeed();

        var matchingBusinessRule = context.DataContext.WorkflowSet.Single(w => w.Id == BusinessRuleInMatchingSolution.Id);
        await Assert.That(matchingBusinessRule.StateCode!.Value).IsEqualTo(Workflow.Options.StateCode.Activated);
        await Assert.That(matchingBusinessRule.StatusCode!.Value).IsEqualTo(Workflow.Options.StatusCode.Activated);

        var otherBusinessRule = context.DataContext.WorkflowSet.Single(w => w.Id == BusinessRuleInOtherSolution.Id);
        await Assert.That(otherBusinessRule.StateCode!.Value).IsEqualTo(Workflow.Options.StateCode.Draft);
        await Assert.That(otherBusinessRule.StatusCode!.Value).IsEqualTo(Workflow.Options.StatusCode.Draft);
    }

    class FakeRetrieveEntityRequest : IFakeMessageExecutor
    {
        public bool CanExecute(OrganizationRequest request) => request is IFakeMessageExecutor;
        public OrganizationResponse Execute(OrganizationRequest request, IXrmFakedContext ctx) => new RetrieveEntityResponse
        {
            Results = new ParameterCollection
            {
                { "EntityMetadata", new EntityMetadata { LogicalName = "test-table" } },
            },
        };
        public Type GetResponsibleRequestType() => typeof(RetrieveEntityRequest);
    }
}
