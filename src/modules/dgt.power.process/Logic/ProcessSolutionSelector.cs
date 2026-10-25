// Copyright (c) DIGITALL Nature. All rights reserved

using dgt.power.common.Extensions;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using dgt.power.dataverse;

namespace dgt.power.process.Logic;

public sealed class ProcessSolutionSelector(IOrganizationServiceAsync2 service)
{
    private const int EntityComponent = 1;
    private const int WorkflowComponent = 29;
    private const int IncludeAllSubcomponents = SolutionComponent.Options.RootComponentBehavior.IncludeSubcomponents;

    public Task<IReadOnlyList<Entity>> SelectAsync(
        IReadOnlyCollection<string> solutionNames,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(solutionNames);
        if (solutionNames.Count == 0)
        {
            throw new ArgumentException("At least one solution unique name is required.", nameof(solutionNames));
        }

        return SelectCoreAsync(solutionNames.ToArray(), cancellationToken);
    }

    private async Task<IReadOnlyList<Entity>> SelectCoreAsync(
        string[] solutionNames,
        CancellationToken cancellationToken)
    {
        var uniqueNames = solutionNames.Distinct(StringComparer.OrdinalIgnoreCase).ToArray();
        var solutionsQuery = new QueryExpression("solution")
        {
            ColumnSet = new ColumnSet("solutionid", "uniquename")
        };
        solutionsQuery.Criteria.AddCondition("uniquename", ConditionOperator.In, uniqueNames.Cast<object>().ToArray());
        var solutions = await service.RetrieveAllAsync(solutionsQuery, cancellationToken: cancellationToken);
        var solutionIds = new HashSet<Guid>();
        foreach (var uniqueName in uniqueNames)
        {
            var matches = solutions.Where(solution =>
                string.Equals(solution.GetAttributeValue<string>("uniquename"), uniqueName, StringComparison.OrdinalIgnoreCase))
                .ToArray();
            if (matches.Length != 1)
            {
                throw new InvalidOperationException(matches.Length == 0
                    ? $"Solution '{uniqueName}' was not found."
                    : $"Solution unique name '{uniqueName}' is ambiguous.");
            }

            solutionIds.Add(matches[0].Id);
        }

        var componentsQuery = new QueryExpression("solutioncomponent")
        {
            ColumnSet = new ColumnSet("objectid", "componenttype", "rootcomponentbehavior", "solutionid")
        };
        componentsQuery.Criteria.AddCondition("solutionid", ConditionOperator.In, solutionIds.Cast<object>().ToArray());
        var components = await service.RetrieveAllAsync(componentsQuery, cancellationToken: cancellationToken);
        var workflowIds = components
            .Where(component => component.GetAttributeValue<OptionSetValue>("componenttype")?.Value == WorkflowComponent)
            .Select(component => component.GetAttributeValue<Guid>("objectid"))
            .Where(id => id != Guid.Empty)
            .ToHashSet();
        var tableIds = components
            .Where(component =>
                component.GetAttributeValue<OptionSetValue>("componenttype")?.Value == EntityComponent &&
                component.GetAttributeValue<OptionSetValue>("rootcomponentbehavior")?.Value == IncludeAllSubcomponents)
            .Select(component => component.GetAttributeValue<Guid>("objectid"))
            .Where(id => id != Guid.Empty)
            .Distinct()
            .ToArray();

        if (tableIds.Length > 0)
        {
            var metadataResponse = (RetrieveAllEntitiesResponse)await service.ExecuteAsync(
                new RetrieveAllEntitiesRequest
                {
                    EntityFilters = EntityFilters.Entity,
                    RetrieveAsIfPublished = false
                },
                cancellationToken);
            var logicalNames = metadataResponse.EntityMetadata
                .Where(metadata => metadata.MetadataId.HasValue && tableIds.Contains(metadata.MetadataId.Value))
                .Select(metadata => metadata.LogicalName)
                .Where(name => !string.IsNullOrWhiteSpace(name))
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            if (logicalNames.Count > 0)
            {
                var businessRulesQuery = new QueryExpression("workflow")
                {
                    ColumnSet = new ColumnSet("workflowid", "category", "type", "primaryentity")
                };
                businessRulesQuery.Criteria.AddCondition("type", ConditionOperator.Equal, 1);
                businessRulesQuery.Criteria.AddCondition("category", ConditionOperator.Equal, 2);
                var rules = await service.RetrieveAllAsync(businessRulesQuery, cancellationToken: cancellationToken);
                foreach (var rule in rules.Where(rule => logicalNames.Contains(rule.GetAttributeValue<string>("primaryentity") ?? string.Empty)))
                {
                    workflowIds.Add(rule.Id);
                }
            }
        }

        if (workflowIds.Count == 0)
        {
            return [];
        }

        var workflowQuery = new QueryExpression("workflow")
        {
            ColumnSet = new ColumnSet("workflowid", "name", "category", "type", "statecode", "statuscode", "ownerid")
        };
        workflowQuery.Criteria.AddCondition("workflowid", ConditionOperator.In, workflowIds.Cast<object>().ToArray());
        workflowQuery.Criteria.AddCondition("type", ConditionOperator.Equal, 1);
        workflowQuery.Criteria.AddCondition("category", ConditionOperator.In, 0, 2, 3, 4, 5);
        var selected = await service.RetrieveAllAsync(workflowQuery, cancellationToken: cancellationToken);
        return selected.DistinctBy(process => process.Id).OrderBy(process => process.Id).ToArray();
    }
}
