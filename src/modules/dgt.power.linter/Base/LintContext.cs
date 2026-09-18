// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common;
using dgt.power.dataverse;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.linter.Base;

public class LintContext
{
    private const int PageSize = 5000;

    public LintContext(IOrganizationService connection, IReadOnlyList<string> solutionNames, IConfigResolver configResolver)
    {
        ArgumentNullException.ThrowIfNull(connection);
        ArgumentNullException.ThrowIfNull(configResolver);

        Connection = connection;
        SolutionNames = solutionNames;
        ConfigResolver = configResolver;

        var entities = ((RetrieveAllEntitiesResponse)connection.Execute(new RetrieveAllEntitiesRequest
        {
            EntityFilters = EntityFilters.Entity | EntityFilters.Attributes
        })).EntityMetadata;

        EntityMetadata = entities.ToDictionary(x => x.LogicalName, StringComparer.OrdinalIgnoreCase);
        AttributeMetadataById = entities
            .SelectMany(static entity => entity.Attributes ?? Array.Empty<AttributeMetadata>())
            .Where(static attribute => attribute.MetadataId.HasValue)
            .GroupBy(static attribute => attribute.MetadataId!.Value)
            .ToDictionary(group => group.Key, group => group.First(), EqualityComparer<Guid>.Default);

        var (components, solutionNamesById) = BuildSolutionComponentEntries();
        SolutionComponentEntries = components;
        SolutionUniqueNamesById = solutionNamesById;
        EntityMemberships = EntityComponentMembershipResolver.Resolve(components, solutionNamesById, EntityMetadata, AttributeMetadataById);
    }

    public IOrganizationService Connection { get; }

    public IConfigResolver ConfigResolver { get; }

    public IReadOnlyList<string> SolutionNames { get; }

    public IReadOnlyDictionary<string, EntityMetadata> EntityMetadata { get; }

    public IReadOnlyDictionary<Guid, AttributeMetadata> AttributeMetadataById { get; }

    public IReadOnlyDictionary<Guid, SolutionComponent> SolutionComponentEntries { get; }

    /// <summary>Solution id -> unique name, resolved once from the same query used to scope components (avoids relying on the unreliable EntityReference.Name).</summary>
    public IReadOnlyDictionary<Guid, string> SolutionUniqueNamesById { get; }

    /// <summary>Keyed by <see cref="EntityComponentMembershipResolver.BuildKey"/> (solution unique name + entity logical name).</summary>
    public IReadOnlyDictionary<string, EntityComponentMembership> EntityMemberships { get; }

    // Both queries push their filter (solution unique name / solution id) into the server-side
    // condition via ConditionOperator.In - the LINQ-to-QueryExpression provider does not support
    // translating a captured HashSet<T>.Contains(...) call, so raw QueryExpression is required
    // here (see BaseAnalyze.GetSolutionComponents for the same pattern).
    private (Dictionary<Guid, SolutionComponent> Components, Dictionary<Guid, string> SolutionNamesById) BuildSolutionComponentEntries()
    {
        var solutionNames = new HashSet<string>(SolutionNames, StringComparer.OrdinalIgnoreCase);
        if (solutionNames.Count == 0)
        {
            return ([], []);
        }

        var solutionQuery = new QueryExpression(Solution.EntityLogicalName)
        {
            NoLock = true,
            ColumnSet = new ColumnSet(Solution.LogicalNames.UniqueName)
        };
        solutionQuery.Criteria.AddCondition(Solution.LogicalNames.UniqueName, ConditionOperator.In, solutionNames.Cast<object>().ToArray());

        var solutionRows = Connection.RetrieveMultiple(solutionQuery).Entities
            .Select(entity => entity.ToEntity<Solution>())
            .ToList();
        var solutionNamesById = solutionRows.ToDictionary(solution => solution.Id, solution => solution.UniqueName ?? string.Empty);

        var solutionIds = solutionRows.Select(solution => solution.Id).ToArray();
        if (solutionIds.Length == 0)
        {
            return ([], solutionNamesById);
        }

        var componentQuery = new QueryExpression(SolutionComponent.EntityLogicalName)
        {
            NoLock = true,
            ColumnSet = new ColumnSet(
                SolutionComponent.LogicalNames.ComponentType,
                SolutionComponent.LogicalNames.ObjectId,
                SolutionComponent.LogicalNames.IsMetadata,
                SolutionComponent.LogicalNames.SolutionId,
                SolutionComponent.LogicalNames.RootComponentBehavior,
                SolutionComponent.LogicalNames.RootSolutionComponentId),
            PageInfo = new PagingInfo { Count = PageSize, PageNumber = 1 }
        };
        componentQuery.Criteria.AddCondition(SolutionComponent.LogicalNames.IsMetadata, ConditionOperator.Equal, true);
        componentQuery.Criteria.AddCondition(SolutionComponent.LogicalNames.SolutionId, ConditionOperator.In, solutionIds.Cast<object>().ToArray());

        var components = new Dictionary<Guid, SolutionComponent>();
        bool moreRecords;
        do
        {
            var page = Connection.RetrieveMultiple(componentQuery);
            foreach (var entity in page.Entities)
            {
                var component = entity.ToEntity<SolutionComponent>();
                components[component.Id] = component;
            }

            moreRecords = page.MoreRecords;
            if (moreRecords)
            {
                componentQuery.PageInfo.PageNumber++;
                componentQuery.PageInfo.PagingCookie = page.PagingCookie;
            }
        } while (moreRecords);

        return (components, solutionNamesById);
    }
}

