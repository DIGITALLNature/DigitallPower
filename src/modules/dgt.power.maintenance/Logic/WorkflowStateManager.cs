// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using dgt.power.common;
using dgt.power.dataverse;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.maintenance.Logic;

public class WorkflowStateManager(IOrganizationServiceAsync2 organizationService,
    string[] solutions, string[] publishers,
    ITracer? tracer = default,
    WorkflowStateManager.TaskStatusCallback? taskStatusCallback = default)
{
    public delegate void TaskStatusCallback(string taskName, string message, int progress, double? total = default);

    public async Task<Workflow[]> LoadAllWorkflows()
    {
        Debug.Assert(solutions?.Length > 0 || publishers?.Length > 0);

        taskStatusCallback?.Invoke("Loading all workflows", "preparing tasks", -1);
        tracer?.Log("Loading all workflows: starting tasks", TraceEventType.Verbose);

        var loadTasks = new[]
        {
            LoadDirectWorkflows(),
            LoadIndirectBusinessRules(),
        };

        taskStatusCallback?.Invoke("Loading all workflows", "loading", 0, loadTasks.Length);
        while (loadTasks.Any(t => !t.IsCompleted))
        {
            await Task.WhenAny(loadTasks);

            taskStatusCallback?.Invoke("Loading all workflows", "loading", loadTasks.Count(t => t.IsCompleted), loadTasks.Length);
        }

        tracer?.Log("Loading all workflows: all tasks completed", TraceEventType.Verbose);

        var uniqueWorkflows = loadTasks
            .SelectMany(w => w.Result)
            .DistinctBy(w => w.WorkflowId)
            .ToArray();

        tracer?.Log($"Loading all workflows: {uniqueWorkflows.Length} unique workflows loaded", TraceEventType.Verbose);
        taskStatusCallback?.Invoke("Loading all workflows", "loaded", loadTasks.Length, loadTasks.Length);

        return uniqueWorkflows;
    }

    private async Task<Workflow[]> LoadDirectWorkflows()
    {
        tracer?.Log("Loading workflows in solution: preparing query", TraceEventType.Verbose);
        taskStatusCallback?.Invoke("Workflows in solution", "preparing query", -1);

        var query = new QueryExpression(Workflow.EntityLogicalName);

        // filter for all workflows of type definition
        var filter = new FilterExpression(LogicalOperator.And);
        query.Criteria.AddFilter(filter);
        filter.AddCondition(Workflow.LogicalNames.Type, ConditionOperator.Equal, Workflow.Options.Type.Definition);

        // add subfilter that will filter on linked entity conditions
        var linkFilter = new FilterExpression(LogicalOperator.Or);
        filter.AddFilter(linkFilter);

        // add link entity workflow -> solution component -> solution to enable filters on solution
        var componentLink = query.AddLink(SolutionComponent.EntityLogicalName, Workflow.LogicalNames.WorkflowId, SolutionComponent.LogicalNames.ObjectId);
        var solutionLink = componentLink.AddLink(Solution.EntityLogicalName, SolutionComponent.LogicalNames.SolutionId, Solution.LogicalNames.SolutionId);
        solutionLink.EntityAlias = "solution";

        // add filter for solutions (uniquename like 'name')
        // the like operator allows for % wildcards to be used
        foreach (var solution in solutions)
        {
            tracer?.Log($"Loading workflows in solution: adding filter for solution '{solution}'", TraceEventType.Verbose);
            linkFilter.AddCondition("solution", Solution.LogicalNames.UniqueName, ConditionOperator.Like, solution);
        }

        // add filter for publishers (publisherid like 'name') if any are specified
        if (publishers.Length > 0)
        {
            // add link entity from solution -> publisher
            var publisherLink = solutionLink.AddLink(Publisher.EntityLogicalName, Solution.LogicalNames.PublisherId, Publisher.LogicalNames.PublisherId, JoinOperator.LeftOuter);
            publisherLink.EntityAlias = "publisher";

            foreach (var publisher in publishers)
            {
                tracer?.Log($"Loading workflows in solution: adding filter for publisher '{publisher}'", TraceEventType.Verbose);
                linkFilter.AddCondition("publisher", Publisher.LogicalNames.UniqueName, ConditionOperator.Like, publisher);
            }
        }

        tracer?.Log("Loading workflows in solution: executing query", TraceEventType.Verbose);
        taskStatusCallback?.Invoke("Workflows in solution", "loading", 0);

        var workflows = await RetrieveMultipleWorkflows(query);

        tracer?.Log($"Loading workflows in solution: {workflows.Length} workflows loaded", TraceEventType.Verbose);
        taskStatusCallback?.Invoke("Workflows in solution", "loaded", workflows.Length, workflows.Length);

        return workflows;
    }

    private async Task<Workflow[]> LoadIndirectBusinessRules()
    {
        tracer?.Log("Loading workflows indirectly: load full tables", TraceEventType.Verbose);
        taskStatusCallback?.Invoke("Business rules", "starting tasks", -1);

        var tableIds = await LoadIndirectBusinessRuleTables();

        tracer?.Log($"Loading workflows indirectly: found {tableIds.Length} tables with potential business rules", TraceEventType.Verbose);
        taskStatusCallback?.Invoke("Business rules", "resolving table names", 0, tableIds.Length + 1);

        var index = 0;
        List<string> tableNames = [];
        await Parallel.ForEachAsync(tableIds, async (tableId, _) => {
            tracer?.Log($"Loading workflows indirectly {tableId}: resolving table name", TraceEventType.Verbose);
            var tableName = await GetTableNameFromMetadata(tableId);

            if (tableName != default)
            {
                tracer?.Log($"Loading workflows indirectly {tableId} ({tableName}): resolved table name", TraceEventType.Verbose);
                tableNames.Add(tableName);
            }
            else
            {
                tracer?.Log($"Loading workflows indirectly {tableId}: failed to resolve table name", TraceEventType.Verbose);
            }

            Interlocked.Increment(ref index);
            taskStatusCallback?.Invoke("Business rules", "resolving table names", index, tableIds.Length + 1);
        });

        tracer?.Log($"Loading workflows indirectly: resolved {tableNames.Count} tables with potential business rules", TraceEventType.Verbose);
        taskStatusCallback?.Invoke("Business rules", "loading business rules", tableIds.Length, tableIds.Length + 1);

        var workflows = await LoadBusinessRules(tableNames.ToArray());

        tracer?.Log($"Loading workflows indirectly: {workflows.Length} business rules loaded", TraceEventType.Verbose);
        taskStatusCallback?.Invoke("Business rules", "loaded", tableIds.Length + 1, tableIds.Length + 1);

        return workflows.ToArray();
    }

    private async Task<Workflow[]> LoadBusinessRules(string[] tableNames)
    {
        var tablesString = string.Join(",", tableNames);
        tracer?.Log($"Loading business rules ({tablesString}): preparing query", TraceEventType.Verbose);

        var query = new QueryExpression(Workflow.EntityLogicalName);

        // filter for all workflows of type business rule and within table
        var filter = new FilterExpression(LogicalOperator.And);
        query.Criteria.AddFilter(filter);
        filter.AddCondition(Workflow.LogicalNames.Type, ConditionOperator.Equal, Workflow.Options.Type.Definition);
        filter.AddCondition(Workflow.LogicalNames.Category, ConditionOperator.Equal, Workflow.Options.Category.BusinessRule);
        filter.AddCondition(Workflow.LogicalNames.PrimaryEntity, ConditionOperator.In, tableNames.Select(t => (object)t).ToArray());

        tracer?.Log($"Loading business rules ({tablesString}): executing query", TraceEventType.Verbose);

        var workflows = await RetrieveMultipleWorkflows(query);

        tracer?.Log($"Loading business rules ({tablesString}): {workflows.Length} business rules loaded", TraceEventType.Verbose);

        return workflows;
    }

    private async Task<string?> GetTableNameFromMetadata(Guid tableId)
    {
        var metadataRequest = new RetrieveEntityRequest
        {
            RetrieveAsIfPublished = true,
            MetadataId = tableId,
            EntityFilters = EntityFilters.Entity,
        };

        try
        {
            var metadataResponse = await ExecuteAsync<RetrieveEntityResponse>(metadataRequest);
            return metadataResponse.EntityMetadata.LogicalName;
        }
        catch (Exception e)
        {
            tracer?.Log($"Failed to resolve table name for id '{tableId}' with error '{e.Message}'", TraceEventType.Error);
            return default;
        }
    }

    private async Task<Guid[]> LoadIndirectBusinessRuleTables()
    {
        tracer?.Log("Loading workflows indirectly: preparing query", TraceEventType.Verbose);
        taskStatusCallback?.Invoke("Business rules", "preparing table query", -1);

        var query = new QueryExpression(SolutionComponent.EntityLogicalName);
        query.ColumnSet.AddColumns(SolutionComponent.LogicalNames.ObjectId);

        // filter for tables fully included in solution
        // > business rules could be part of a table that has rootcomponentbehaviour=includesubcompoments
        // > in that case the business rule does not have a separate solution component
        var filter = new FilterExpression(LogicalOperator.And);
        query.Criteria.AddFilter(filter);
        filter.AddCondition(SolutionComponent.LogicalNames.ComponentType, ConditionOperator.Equal, SolutionComponent.Options.ComponentType.Entity);
        filter.AddCondition(SolutionComponent.LogicalNames.RootComponentBehavior, ConditionOperator.Equal, SolutionComponent.Options.RootComponentBehavior.IncludeSubcomponents);

        // add link entity solution component -> solution
        var solutionLink = query.AddLink(Solution.EntityLogicalName, SolutionComponent.LogicalNames.SolutionId, Solution.LogicalNames.SolutionId);
        solutionLink.EntityAlias = "solution";

        // add filter for solutions (uniquename like 'name')
        // the like operator allows for % wildcards to be used
        var solutionFilter = new FilterExpression(LogicalOperator.Or);
        filter.AddFilter(solutionFilter);
        foreach (var solution in solutions)
        {
            tracer?.Log($"Loading workflows indirectly: adding filter for solution '{solution}'", TraceEventType.Verbose);
            solutionFilter.AddCondition("solution", Solution.LogicalNames.UniqueName, ConditionOperator.Like, solution);
        }

        // add filter for publishers (publisherid like 'name') if any are specified
        if (publishers.Length > 0)
        {
            // add link entity from solution -> publisher
            var publisherLink = solutionLink.AddLink(Publisher.EntityLogicalName, Solution.LogicalNames.PublisherId, Publisher.LogicalNames.PublisherId, JoinOperator.LeftOuter);
            publisherLink.EntityAlias = "publisher";

            foreach (var publisher in publishers)
            {
                tracer?.Log($"Loading workflows indirectly: adding filter for publisher '{publisher}'", TraceEventType.Verbose);
                filter.AddCondition("publisher", Publisher.LogicalNames.UniqueName, ConditionOperator.Like, publisher);
            }
        }

        tracer?.Log("Loading workflows indirectly: executing query", TraceEventType.Verbose);
        taskStatusCallback?.Invoke("Business rules", "loading tables", 0);

        var RetrieveMultipleRequest = new RetrieveMultipleRequest
        {
            Query = query,
        };
        var response = await ExecuteAsync<RetrieveMultipleResponse>(RetrieveMultipleRequest);
        var solutionComponents = response.EntityCollection.Entities.Cast<SolutionComponent>().ToArray();

        tracer?.Log($"Loading workflows indirectly: {solutionComponents.Length} tables loaded", TraceEventType.Verbose);
        taskStatusCallback?.Invoke("Business rules", "loaded tables", solutionComponents.Length);

        return solutionComponents
            .Select(sc => sc.ObjectId!.Value)
            .ToArray();
    }

    private async Task<Workflow[]> RetrieveMultipleWorkflows(QueryExpression query)
    {
        query.ColumnSet.AddColumns
        (
            Workflow.LogicalNames.Category,
            Workflow.LogicalNames.StatusCode,
            Workflow.LogicalNames.Name,
            Workflow.LogicalNames.UniqueName,
            Workflow.LogicalNames.StateCode,
            Workflow.LogicalNames.PrimaryEntity,
            Workflow.LogicalNames.OwnerId,
            Workflow.LogicalNames.WorkflowId
        );

        // add owner to columns
        var ownerLink = query.AddLink(SystemUser.EntityLogicalName, Workflow.LogicalNames.OwnerId, SystemUser.LogicalNames.SystemUserId, JoinOperator.LeftOuter);
        ownerLink.EntityAlias = "owner";
        ownerLink.Columns.AddColumns(SystemUser.LogicalNames.FullName, SystemUser.LogicalNames.DomainName);

        var retrieveMultpipleRequest = new RetrieveMultipleRequest
        {
            Query = query,
        };
        var response = await ExecuteAsync<RetrieveMultipleResponse>(retrieveMultpipleRequest);
        var workflows = response.EntityCollection.Entities.Cast<Workflow>().ToArray();

        return workflows;
    }

    private async Task<TResponse> ExecuteAsync<TResponse>(OrganizationRequest request)
        where TResponse : OrganizationResponse
    {
        tracer?.Log($"Executing request: {request.RequestName}", TraceEventType.Verbose);

        var response = await organizationService.ExecuteAsync(request);
        return (TResponse)response;
    }
}
