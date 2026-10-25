// Copyright (c) DIGITALL Nature. All rights reserved

using dgt.power.dataverse;
using dgt.power.process.Logic;
using Digitall.Dataverse.Testing;
using Digitall.Dataverse.Testing.OrganizationRequests;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.process.tests.Logic;

public class ProcessSolutionSelectorPagingTests
{
    [Test]
    public async Task SelectAsync_PagesSolutionsComponentsAndWorkflowQueriesWithOneMetadataResolution()
    {
        var tableId = Guid.NewGuid();
        var directId = Guid.NewGuid();
        var indirectRuleId = Guid.NewGuid();
        var executor = new PagedSelectionExecutor(tableId, directId, indirectRuleId);
        var metadata = new MetadataExecutor(tableId);
        var service = new FakeOrganizationServiceAsync();
        service.AddRequests(executor, metadata);

        var selected = await new ProcessSolutionSelector(service).SelectAsync(["First", "Second"]);

        await Assert.That(selected.Select(process => process.Id)).IsEquivalentTo([directId, indirectRuleId]);
        await Assert.That(executor.Pages.Where(page => page.EntityName == "solution").Select(page => page.Page))
            .IsEquivalentTo([1, 2]);
        await Assert.That(executor.Pages.Where(page => page.EntityName == "solutioncomponent").Select(page => page.Page))
            .IsEquivalentTo([1, 2]);
        await Assert.That(executor.Pages.Where(page => page.EntityName == "workflow").Select(page => page.Page))
            .IsEquivalentTo([1, 2, 1, 2]);
        await Assert.That(metadata.RequestCount).IsEqualTo(1);
    }

    private sealed class PagedSelectionExecutor(Guid tableId, Guid directId, Guid indirectRuleId) : IOrganizationRequestFake
    {
        public Type ForType => typeof(RetrieveMultipleRequest);
        public List<(string EntityName, int Page, string? Cookie)> Pages { get; } = [];

        public OrganizationResponse Execute(OrganizationRequest organizationRequest, FakeOrganizationService fakeOrganizationService)
        {
            var query = (QueryExpression)((RetrieveMultipleRequest)organizationRequest).Query;
            var page = query.PageInfo.PageNumber;
            Pages.Add((query.EntityName, page, query.PageInfo.PagingCookie));
            var entities = query.EntityName switch
            {
                "solution" => SolutionPage(page),
                "solutioncomponent" => ComponentPage(page),
                "workflow" when query.Criteria.Conditions.Any(condition =>
                    condition.AttributeName == "category" && condition.Operator == ConditionOperator.Equal) =>
                    BusinessRulePage(page),
                "workflow" => SelectedWorkflowPage(page),
                _ => []
            };
            return new RetrieveMultipleResponse
            {
                Results =
                {
                    ["EntityCollection"] = new EntityCollection(entities)
                    {
                        MoreRecords = page == 1,
                        PagingCookie = page == 1 ? "next-page" : null
                    }
                }
            };
        }

        private static Entity[] SolutionPage(int page) =>
        [
            new("solution", Guid.NewGuid())
            {
                ["uniquename"] = page == 1 ? "First" : "Second"
            }
        ];

        private Entity[] ComponentPage(int page) =>
        [
            page == 1
                ? Component(Guid.NewGuid(), directId, SolutionComponent.Options.ComponentType.Workflow)
                : Component(Guid.NewGuid(), tableId, SolutionComponent.Options.ComponentType.Entity,
                    SolutionComponent.Options.RootComponentBehavior.IncludeSubcomponents)
        ];

        private Entity[] BusinessRulePage(int page) =>
        [
            new("workflow", page == 1 ? indirectRuleId : Guid.NewGuid())
            {
                ["type"] = new OptionSetValue(Workflow.Options.Type.Definition),
                ["category"] = new OptionSetValue(Workflow.Options.Category.BusinessRule),
                ["primaryentity"] = page == 1 ? "account" : "contact"
            }
        ];

        private Entity[] SelectedWorkflowPage(int page) =>
        [
            new("workflow", page == 1 ? directId : indirectRuleId)
            {
                ["type"] = new OptionSetValue(Workflow.Options.Type.Definition),
                ["category"] = new OptionSetValue(page == 1
                    ? Workflow.Options.Category.Workflow
                    : Workflow.Options.Category.BusinessRule)
            }
        ];

        private static Entity Component(Guid solutionId, Guid objectId, int componentType, int? behavior = null) =>
            new(SolutionComponent.EntityLogicalName, Guid.NewGuid())
            {
                [SolutionComponent.LogicalNames.ObjectId] = objectId,
                [SolutionComponent.LogicalNames.ComponentType] = new OptionSetValue(componentType),
                [SolutionComponent.LogicalNames.SolutionId] = new EntityReference("solution", solutionId),
                [SolutionComponent.LogicalNames.RootComponentBehavior] = behavior.HasValue ? new OptionSetValue(behavior.Value) : null
            };
    }

    private sealed class MetadataExecutor(Guid tableId) : IOrganizationRequestFake
    {
        public Type ForType => typeof(RetrieveAllEntitiesRequest);
        public int RequestCount { get; private set; }

        public OrganizationResponse Execute(OrganizationRequest organizationRequest, FakeOrganizationService fakeOrganizationService)
        {
            RequestCount++;
            return new RetrieveAllEntitiesResponse
            {
                Results = { ["EntityMetadata"] = new EntityMetadata[] { new() { MetadataId = tableId, LogicalName = "account" } } }
            };
        }
    }
}
