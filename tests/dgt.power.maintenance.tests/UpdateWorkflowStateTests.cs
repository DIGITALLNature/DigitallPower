// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.maintenance.Base;
using dgt.power.maintenance.Logic;
using dgt.power.maintenance.tests.Base;
using dgt.power.tests.Extensions;
using Microsoft.Xrm.Sdk;

namespace dgt.power.maintenance.tests;

public class UpdateWorkflowStateTests : MaintenanceTestsBase<UpdateWorkflowState>
{
    public UpdateWorkflowStateTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper) { }

    [Theory]
    [InlineData("")]
    [InlineData("missing.json")]
    public void InvalidConfigPathsShouldFail(string config) =>
        GetBuilder().Build()
            .Execute(new MaintenanceVerb
            {
                Config = GetResourcePath(config)
            }).Should().BeFalse();

    [Theory]
    [InlineData("empty.json")]
    [InlineData("simple.json")]
    public void ValidConfigShouldSucceed(string config) =>
        GetBuilder().Build()
            .Execute(new MaintenanceVerb
            {
                Config = GetResourcePath(config)
            }).Should().BeTrue();

    [Fact]
    private void DefaultShouldActivateFlows()
    {
        var draftFlow = new Workflow(Guid.NewGuid())
        {
            Name = "Draft -> Activated",
            Category = new OptionSetValue(Workflow.Options.Category.ModernFlow),
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Draft),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Draft),
        };
        var suspendedFlow = new Workflow(Guid.NewGuid())
        {
            Name = "Suspended -> Activated",
            Category = new OptionSetValue(Workflow.Options.Category.ModernFlow),
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Suspended),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.CompanyDLPViolation),
        };
        var activatedFlow = new Workflow(Guid.NewGuid())
        {
            Name = "Activated -> Activated",
            Category = new OptionSetValue(Workflow.Options.Category.ModernFlow),
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Activated),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Activated),
        };

        var context = GetBuilder()
            .WithData(draftFlow)
            .WithData(suspendedFlow)
            .WithData(activatedFlow)
            .Build();

        context.Execute(new MaintenanceVerb
        {
            Config = GetResourcePath("empty.json"),
        }).Should().Succeed();

        var updatedDraftFlow = context.DataContext.WorkflowSet.Single(w => w.Id == draftFlow.Id);
        var updatedSuspendedFlow = context.DataContext.WorkflowSet.Single(w => w.Id == suspendedFlow.Id);
        var updatedActivatedFlow = context.DataContext.WorkflowSet.Single(w => w.Id == activatedFlow.Id);

        updatedDraftFlow.StateCode!.Value.Should().Be(Workflow.Options.StateCode.Activated);
        updatedDraftFlow.StatusCode!.Value.Should().Be(Workflow.Options.StatusCode.Activated);
        updatedSuspendedFlow.StateCode!.Value.Should().Be(Workflow.Options.StateCode.Activated);
        updatedSuspendedFlow.StatusCode!.Value.Should().Be(Workflow.Options.StatusCode.Activated);
        updatedActivatedFlow.StateCode!.Value.Should().Be(Workflow.Options.StateCode.Activated);
        updatedActivatedFlow.StatusCode!.Value.Should().Be(Workflow.Options.StatusCode.Activated);
    }

    [Fact]
    private void ShouldDeactivateFlows()
    {
        var draftFlow = new Workflow(Guid.NewGuid())
        {
            Name = "Draft -> Draft",
            Category = new OptionSetValue(Workflow.Options.Category.ModernFlow),
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Draft),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Draft),
        };
        var suspendedFlow = new Workflow(Guid.NewGuid())
        {
            Name = "Suspended -> Suspended",
            Category = new OptionSetValue(Workflow.Options.Category.ModernFlow),
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Suspended),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.CompanyDLPViolation),
        };
        var activatedFlow = new Workflow(Guid.NewGuid())
        {
            Name = "Activated -> Draft",
            Category = new OptionSetValue(Workflow.Options.Category.ModernFlow),
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Activated),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Activated),
        };

        var context = GetBuilder()
            .WithData(draftFlow)
            .WithData(suspendedFlow)
            .WithData(activatedFlow)
            .Build();

        context.Execute(new MaintenanceVerb
        {
            Config = GetResourcePath("deactivate.json"),
        }).Should().Succeed();

        var updatedDraftFlow = context.DataContext.WorkflowSet.Single(w => w.Id == draftFlow.Id);
        var updatedSuspendedFlow = context.DataContext.WorkflowSet.Single(w => w.Id == suspendedFlow.Id);
        var updatedActivatedFlow = context.DataContext.WorkflowSet.Single(w => w.Id == activatedFlow.Id);

        updatedDraftFlow.StateCode!.Value.Should().Be(Workflow.Options.StateCode.Draft);
        updatedDraftFlow.StatusCode!.Value.Should().Be(Workflow.Options.StatusCode.Draft);
        updatedSuspendedFlow.StateCode!.Value.Should().Be(Workflow.Options.StateCode.Suspended);
        updatedSuspendedFlow.StatusCode!.Value.Should().Be(Workflow.Options.StatusCode.CompanyDLPViolation);
        updatedActivatedFlow.StateCode!.Value.Should().Be(Workflow.Options.StateCode.Draft);
        updatedActivatedFlow.StatusCode!.Value.Should().Be(Workflow.Options.StatusCode.Draft);
    }

    [Fact]
    private void ShouldOverwriteOwner()
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
        var flowOwner = new SystemUser(Guid.NewGuid())
        {
            Attributes = new AttributeCollection
            {
                { SystemUser.LogicalNames.FullName, "Flow Owner" },
                { SystemUser.LogicalNames.DomainName, "flow@owner.com" },
            },
            DomainName = "flow@owner.com",
        };

        var currentToDefaultFlow = new Workflow(Guid.NewGuid())
        {
            Name = "Current -> Default",
            Category = new OptionSetValue(Workflow.Options.Category.ModernFlow),
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Draft),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Draft),
            OwnerId = currentOwner.ToEntityReference(),
        };
        var currentToFlowFlow = new Workflow(Guid.NewGuid())
        {
            Name = "Current -> Flow",
            Category = new OptionSetValue(Workflow.Options.Category.ModernFlow),
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Suspended),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.CompanyDLPViolation),
            OwnerId = currentOwner.ToEntityReference(),
        };
        var currentToCurrentSpecifiedFlow = new Workflow(Guid.NewGuid())
        {
            Name = "Current -> Current (specified)",
            Category = new OptionSetValue(Workflow.Options.Category.ModernFlow),
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Activated),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Activated),
            OwnerId = currentOwner.ToEntityReference(),
        };
        var currentToCurrentFallbackFlow = new Workflow(Guid.NewGuid())
        {
            Name = "Current -> Current (fallback)",
            Category = new OptionSetValue(Workflow.Options.Category.ModernFlow),
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Activated),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Activated),
            OwnerId = currentOwner.ToEntityReference(),
        };
        var currentToDefaultSpecifiedFlow = new Workflow(Guid.NewGuid())
        {
            Name = "Current -> Default (specified)",
            Category = new OptionSetValue(Workflow.Options.Category.ModernFlow),
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Activated),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Activated),
            OwnerId = currentOwner.ToEntityReference(),
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

        context.Execute(new MaintenanceVerb
        {
            Config = GetResourcePath("owner.json"),
        }).Should().Succeed();

        var updatedCurrentToDefaultFlow = context.DataContext.WorkflowSet.Single(w => w.Id == currentToDefaultFlow.Id);
        updatedCurrentToDefaultFlow.OwnerId.Should().Be(defaultOwner.ToEntityReference());

        var updatedCurrentToFlowFlow = context.DataContext.WorkflowSet.Single(w => w.Id == currentToFlowFlow.Id);
        updatedCurrentToFlowFlow.OwnerId.Should().Be(flowOwner.ToEntityReference());

        var updatedCurrentToCurrentSpecifiedFlow = context.DataContext.WorkflowSet.Single(w => w.Id == currentToCurrentSpecifiedFlow.Id);
        updatedCurrentToCurrentSpecifiedFlow.OwnerId.Should().Be(currentOwner.ToEntityReference());

        var updatedCurrentToCurrentFallbackFlow = context.DataContext.WorkflowSet.Single(w => w.Id == currentToCurrentFallbackFlow.Id);
        updatedCurrentToCurrentFallbackFlow.OwnerId.Should().Be(currentOwner.ToEntityReference());

        var updatedCurrentToDefaultSpecifiedFlow = context.DataContext.WorkflowSet.Single(w => w.Id == currentToDefaultSpecifiedFlow.Id);
        updatedCurrentToDefaultSpecifiedFlow.OwnerId.Should().Be(defaultOwner.ToEntityReference());
    }

    [Theory]
    [InlineData("filter-solution.json")]
    [InlineData("filter-solution-pattern.json")]
    private void ShouldFilterBySolution(string config)
    {
        var matchingSolution = new Solution(Guid.NewGuid())
        {
            UniqueName = "match",
        };
        var otherSolution = new Solution(Guid.NewGuid())
        {
            UniqueName = "other",
        };

        var flowInMatchingSolution = new Workflow(Guid.NewGuid())
        {
            Name = "Flow in matching",
            Category = new OptionSetValue(Workflow.Options.Category.ModernFlow),
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Draft),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Draft),
        };
        var flowInOtherSolution = new Workflow(Guid.NewGuid())
        {
            Name = "Flow in other",
            Category = new OptionSetValue(Workflow.Options.Category.ModernFlow),
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Draft),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Draft),
        };

        var matchingComponent = new SolutionComponent(Guid.NewGuid())
        {
            Attributes = new AttributeCollection
            {
                { SolutionComponent.LogicalNames.SolutionId, matchingSolution.Id },
                { SolutionComponent.LogicalNames.ObjectId, flowInMatchingSolution.Id },
            },
        };
        var otherComponent = new SolutionComponent(Guid.NewGuid())
        {
            Attributes = new AttributeCollection
            {
                { SolutionComponent.LogicalNames.SolutionId, otherSolution.Id },
                { SolutionComponent.LogicalNames.ObjectId, flowInOtherSolution.Id },
            },
        };

        var context = GetBuilder()
            .WithData(matchingSolution)
            .WithData(otherSolution)
            .WithData(flowInMatchingSolution)
            .WithData(flowInOtherSolution)
            .WithData(matchingComponent)
            .WithData(otherComponent)
            .Build();

        context.Execute(new MaintenanceVerb
        {
            Config = GetResourcePath(config),
        }).Should().Succeed();

        var matchingFlow = context.DataContext.WorkflowSet.Single(w => w.Id == flowInMatchingSolution.Id);
        matchingFlow.StateCode!.Value.Should().Be(Workflow.Options.StateCode.Activated);
        matchingFlow.StatusCode!.Value.Should().Be(Workflow.Options.StatusCode.Activated);

        var otherFlow = context.DataContext.WorkflowSet.Single(w => w.Id == flowInOtherSolution.Id);
        otherFlow.StateCode!.Value.Should().Be(Workflow.Options.StateCode.Draft);
        otherFlow.StatusCode!.Value.Should().Be(Workflow.Options.StatusCode.Draft);
    }

    [Theory(Skip = "Probably broken with fakexrmeasy")]
    [InlineData("filter-publisher.json")]
    [InlineData("filter-publisher-pattern.json")]
    private void ShouldFilterByPublisher(string config)
    {
        var matchingPublisher = new Publisher(Guid.NewGuid())
        {
            UniqueName = "matchingPublisher",
        };
        var otherPublisher = new Publisher(Guid.NewGuid())
        {
            UniqueName = "otherPublisher",
        };

        var matchingSolution = new Solution(Guid.NewGuid())
        {
            UniqueName = "matchingSolution",
            PublisherId = matchingPublisher.ToEntityReference(),
        };
        var otherSolution = new Solution(Guid.NewGuid())
        {
            UniqueName = "otherSolution",
            PublisherId = otherPublisher.ToEntityReference(),
        };

        var flowInMatchingSolution = new Workflow(Guid.NewGuid())
        {
            Name = "Flow in matching",
            Category = new OptionSetValue(Workflow.Options.Category.ModernFlow),
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Draft),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Draft),
        };
        var flowInOtherSolution = new Workflow(Guid.NewGuid())
        {
            Name = "Flow in other",
            Category = new OptionSetValue(Workflow.Options.Category.ModernFlow),
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Draft),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Draft),
        };

        var matchingComponent = new SolutionComponent(Guid.NewGuid())
        {
            Attributes = new AttributeCollection
            {
                { SolutionComponent.LogicalNames.SolutionId, matchingSolution.Id },
                { SolutionComponent.LogicalNames.ObjectId, flowInMatchingSolution.Id },
            },
        };
        var otherComponent = new SolutionComponent(Guid.NewGuid())
        {
            Attributes = new AttributeCollection
            {
                { SolutionComponent.LogicalNames.SolutionId, otherSolution.Id },
                { SolutionComponent.LogicalNames.ObjectId, flowInOtherSolution.Id },
            },
        };

        var context = GetBuilder()
            .WithData(matchingSolution)
            .WithData(otherSolution)
            .WithData(flowInMatchingSolution)
            .WithData(flowInOtherSolution)
            .WithData(matchingComponent)
            .WithData(otherComponent)
            .Build();

        context.Execute(new MaintenanceVerb
        {
            Config = GetResourcePath(config),
        }).Should().Succeed();

        var matchingFlow = context.DataContext.WorkflowSet.Single(w => w.Id == flowInMatchingSolution.Id);
        matchingFlow.StateCode!.Value.Should().Be(Workflow.Options.StateCode.Activated);
        matchingFlow.StatusCode!.Value.Should().Be(Workflow.Options.StatusCode.Activated);

        var otherFlow = context.DataContext.WorkflowSet.Single(w => w.Id == flowInOtherSolution.Id);
        otherFlow.StateCode!.Value.Should().Be(Workflow.Options.StateCode.Draft);
        otherFlow.StatusCode!.Value.Should().Be(Workflow.Options.StatusCode.Draft);
    }
}
