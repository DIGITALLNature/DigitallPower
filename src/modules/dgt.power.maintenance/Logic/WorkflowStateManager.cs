// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using dgt.power.common;
using dgt.power.dataverse;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using IAnsiConsole = Spectre.Console.IAnsiConsole;
using static Spectre.Console.AnsiConsoleExtensions;

namespace dgt.power.maintenance.Logic;

public class WorkflowStateManager(IOrganizationServiceAsync2 organizationService,
    string[] solutions, string[] publishers,
    IAnsiConsole console,
    WorkflowStateManager.TaskStatusCallback? taskStatusCallback = null)
{
    public delegate void TaskStatusCallback(string taskName, string message, int progress, double? total = null);

    public async Task<Workflow[]> LoadAllWorkflowsAsync()
    {
        taskStatusCallback?.Invoke("Loading all workflows", "preparing tasks", -1);
        console.MarkupLine("[grey]Loading all workflows: starting tasks[/]");

        var loadTasks = new[]
        {
            LoadDirectWorkflowsAsync(),
            LoadIndirectBusinessRulesAsync()
        };

        taskStatusCallback?.Invoke("Loading all workflows", "loading", 0, loadTasks.Length);
        while (Array.Exists(loadTasks, t => !t.IsCompleted))
        {
            await Task.WhenAny(loadTasks);

            taskStatusCallback?.Invoke("Loading all workflows", "loading", loadTasks.Count(t => t.IsCompleted), loadTasks.Length);
        }

        console.MarkupLine("[grey]Loading all workflows: all tasks completed[/]");

        var uniqueWorkflows = loadTasks
            .SelectMany(w => w.Result)
            .DistinctBy(w => w.WorkflowId)
            .ToArray();

        console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[grey]Loading all workflows: {uniqueWorkflows.Length} unique workflows loaded[/]");
        taskStatusCallback?.Invoke("Loading all workflows", "loaded", loadTasks.Length, loadTasks.Length);

        return uniqueWorkflows;
    }

    private async Task<Workflow[]> LoadDirectWorkflowsAsync()
    {
        console.MarkupLine("[grey]Loading workflows in solution: preparing query[/]");
        taskStatusCallback?.Invoke("Workflows in solution", "preparing query", -1);

        var query = new QueryExpression(Workflow.EntityLogicalName);

        // filter for all workflows of type definition
        var filter = new FilterExpression(LogicalOperator.And);
        query.Criteria.AddFilter(filter);
        filter.AddCondition(Workflow.LogicalNames.Type, ConditionOperator.Equal, Workflow.Options.Type.Definition);

        // add subfilter that will filter on linked entity conditions
        var linkFilter = new FilterExpression(LogicalOperator.Or);
        filter.AddFilter(linkFilter);

        if (solutions.Length > 0 || publishers.Length > 0)
        {
            // add link entity workflow -> solution component -> solution to enable filters on solution
            var componentLink = query.AddLink(SolutionComponent.EntityLogicalName, Workflow.LogicalNames.WorkflowId, SolutionComponent.LogicalNames.ObjectId);
            var solutionLink = componentLink.AddLink(Solution.EntityLogicalName, SolutionComponent.LogicalNames.SolutionId, Solution.LogicalNames.SolutionId);
            solutionLink.EntityAlias = "solution";

            // add filter for solutions (uniquename like 'name')
            // the like operator allows for % wildcards to be used
            foreach (var solution in solutions)
            {
                console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[grey]Loading workflows in solution: adding filter for solution '{solution}'[/]");
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
                    console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[grey]Loading workflows in solution: adding filter for publisher '{publisher}'[/]");
                    linkFilter.AddCondition("publisher", Publisher.LogicalNames.UniqueName, ConditionOperator.Like, publisher);
                }
            }
        }

        console.MarkupLine("[grey]Loading workflows in solution: executing query[/]");
        taskStatusCallback?.Invoke("Workflows in solution", "loading", 0);

        var workflows = await RetrieveMultipleWorkflowsAsync(query);

        console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[grey]Loading workflows in solution: {workflows.Length} workflows loaded[/]");
        taskStatusCallback?.Invoke("Workflows in solution", "loaded", workflows.Length, workflows.Length);

        return workflows;
    }

    private async Task<Workflow[]> LoadIndirectBusinessRulesAsync()
    {
        console.MarkupLine("[grey]Loading workflows indirectly: load full tables[/]");
        taskStatusCallback?.Invoke("Business rules", "starting tasks", -1);

        var tableIds = await LoadIndirectBusinessRuleTablesAsync();

        console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[grey]Loading workflows indirectly: found {tableIds.Length} tables with potential business rules[/]");
        taskStatusCallback?.Invoke("Business rules", "resolving table names", 0, tableIds.Length + 1);

        var index = 0;
        List<string> tableNames = [];
        await Parallel.ForEachAsync(tableIds, async (tableId, _) =>
        {
            console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[grey]Loading workflows indirectly {tableId}: resolving table name[/]");
            var tableName = await GetTableNameFromMetadataAsync(tableId);

            if (tableName != null)
            {
                console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[grey]Loading workflows indirectly {tableId} ({tableName}): resolved table name[/]");
                tableNames.Add(tableName);
            }
            else
            {
                console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[grey]Loading workflows indirectly {tableId}: failed to resolve table name[/]");
            }

            Interlocked.Increment(ref index);
            taskStatusCallback?.Invoke("Business rules", "resolving table names", index, tableIds.Length + 1);
        });

        console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[grey]Loading workflows indirectly: resolved {tableNames.Count} tables with potential business rules[/]");
        taskStatusCallback?.Invoke("Business rules", "loading business rules", tableIds.Length, tableIds.Length + 1);

        var workflows = tableNames.Count > 0 ? await LoadBusinessRulesAsync(tableNames.ToArray()) : [];

        console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[grey]Loading workflows indirectly: {workflows.Length} business rules loaded[/]");
        taskStatusCallback?.Invoke("Business rules", "loaded", tableIds.Length + 1, tableIds.Length + 1);

        return workflows.ToArray();
    }

    private async Task<Workflow[]> LoadBusinessRulesAsync(string[] tableNames)
    {
        var tablesString = string.Join(",", tableNames);
        console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[grey]Loading business rules ({tablesString}): preparing query[/]");

        var query = new QueryExpression(Workflow.EntityLogicalName);

        // filter for all workflows of type business rule and within table
        var filter = new FilterExpression(LogicalOperator.And);
        query.Criteria.AddFilter(filter);
        filter.AddCondition(Workflow.LogicalNames.Type, ConditionOperator.Equal, Workflow.Options.Type.Definition);
        filter.AddCondition(Workflow.LogicalNames.Category, ConditionOperator.Equal, Workflow.Options.Category.BusinessRule);
        filter.AddCondition(Workflow.LogicalNames.PrimaryEntity, ConditionOperator.In, tableNames.Select(object (t) => t).ToArray());

        console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[grey]Loading business rules ({tablesString}): executing query[/]");

        var workflows = await RetrieveMultipleWorkflowsAsync(query);

        console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[grey]Loading business rules ({tablesString}): {workflows.Length} business rules loaded[/]");

        return workflows;
    }

    private async Task<string?> GetTableNameFromMetadataAsync(Guid tableId)
    {
        var metadataRequest = new RetrieveEntityRequest
        {
            RetrieveAsIfPublished = true,
            MetadataId = tableId,
            EntityFilters = EntityFilters.Entity
        };

        try
        {
            var metadataResponse = await ExecuteAsync<RetrieveEntityResponse>(metadataRequest);
            return metadataResponse.EntityMetadata.LogicalName;
        }
        catch (Exception e) when (e is not OutOfMemoryException and not StackOverflowException)
        {
            console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[red]Failed to resolve table name for id '{tableId}' with error '{e.Message}'[/]");
            return null;
        }
    }

    private async Task<Guid[]> LoadIndirectBusinessRuleTablesAsync()
    {
        console.MarkupLine("[grey]Loading workflows indirectly: preparing query[/]");
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
            console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[grey]Loading workflows indirectly: adding filter for solution '{solution}'[/]");
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
                console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[grey]Loading workflows indirectly: adding filter for publisher '{publisher}'[/]");
                filter.AddCondition("publisher", Publisher.LogicalNames.UniqueName, ConditionOperator.Like, publisher);
            }
        }

        console.MarkupLine("[grey]Loading workflows indirectly: executing query[/]");
        taskStatusCallback?.Invoke("Business rules", "loading tables", 0);

        var retrieveMultipleRequest = new RetrieveMultipleRequest
        {
            Query = query
        };
        var response = await ExecuteAsync<RetrieveMultipleResponse>(retrieveMultipleRequest);
        var solutionComponents = response.EntityCollection.Entities.Cast<SolutionComponent>().ToArray();

        console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[grey]Loading workflows indirectly: {solutionComponents.Length} tables loaded[/]");
        taskStatusCallback?.Invoke("Business rules", "loaded tables", solutionComponents.Length);

        return solutionComponents
            .Select(sc => sc.ObjectId!.Value)
            .ToArray();
    }

    private async Task<Workflow[]> RetrieveMultipleWorkflowsAsync(QueryExpression query)
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
            Query = query
        };
        var response = await ExecuteAsync<RetrieveMultipleResponse>(retrieveMultpipleRequest);
        var workflows = response.EntityCollection.Entities.Cast<Workflow>().ToArray();

        return workflows;
    }

    private async Task<TResponse> ExecuteAsync<TResponse>(OrganizationRequest request)
        where TResponse : OrganizationResponse
    {
        console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[grey]Executing request: {request.RequestName}[/]");

        var response = await organizationService.ExecuteAsync(request);
        return (TResponse)response;
    }
}
