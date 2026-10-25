// Copyright (c) DIGITALL Nature. All rights reserved

using dgt.power.common.Extensions;
using dgt.power.dataverse;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.duplicaterules.Logic;

public sealed class DuplicateRuleSelector(IOrganizationServiceAsync2 service)
{
    public Task<IReadOnlyList<Entity>> SelectAsync(
        string? baseTable,
        IReadOnlyCollection<Guid> ruleIds,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(ruleIds);
        return SelectCoreAsync(baseTable, ruleIds.Distinct().ToArray(), cancellationToken);
    }

    private async Task<IReadOnlyList<Entity>> SelectCoreAsync(
        string? baseTable,
        Guid[] ruleIds,
        CancellationToken cancellationToken)
    {
        var query = new QueryExpression(DuplicateRule.EntityLogicalName)
        {
            ColumnSet = new ColumnSet(DuplicateRuleStates.Columns)
        };
        if (ruleIds.Length > 0)
        {
            query.Criteria.AddCondition("duplicateruleid", ConditionOperator.In, ruleIds.Cast<object>().ToArray());
        }
        else if (baseTable is not null)
        {
            query.Criteria.AddCondition("baseentityname", ConditionOperator.Equal, baseTable);
        }

        var rules = (await service.RetrieveAllAsync(query, cancellationToken: cancellationToken))
            .DistinctBy(rule => rule.Id)
            .ToArray();

        if (ruleIds.Length > 0)
        {
            var found = rules.Select(rule => rule.Id).ToHashSet();
            var missing = ruleIds.Where(id => !found.Contains(id)).OrderBy(id => id).ToArray();
            if (missing.Length > 0)
            {
                throw new InvalidOperationException(
                    $"Duplicate rules were not found for ID(s): {string.Join(", ", missing.Select(id => id.ToString("D")))}.");
            }

            if (baseTable is not null)
            {
                var conflicts = rules
                    .Where(rule => !string.Equals(rule.GetAttributeValue<string>("baseentityname"), baseTable, StringComparison.OrdinalIgnoreCase))
                    .OrderBy(rule => rule.Id)
                    .Select(rule => $"{rule.Id:D} ({rule.GetAttributeValue<string>("baseentityname")})")
                    .ToArray();
                if (conflicts.Length > 0)
                {
                    throw new InvalidOperationException(
                        $"Selected duplicate rule(s) do not belong to base table '{baseTable}': {string.Join(", ", conflicts)}.");
                }
            }
        }

        var unstable = rules
            .Where(rule => DuplicateRuleStates.Classify(rule) is DuplicateRulePublicationState.Publishing or DuplicateRulePublicationState.Unknown)
            .OrderBy(rule => rule.Id)
            .Select(rule => $"{rule.Id:D} is {DuplicateRuleStates.Describe(rule)}")
            .ToArray();
        if (unstable.Length > 0)
        {
            throw new InvalidOperationException(
                $"Selected duplicate rule(s) are not in a stable Published or Unpublished state: {string.Join("; ", unstable)}.");
        }

        return rules.OrderBy(rule => rule.Id).ToArray();
    }
}
