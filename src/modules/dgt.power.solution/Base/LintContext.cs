// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common;
using dgt.power.dataverse;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.solution.Base;

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

        // TODO(async): migrate to IOrganizationServiceAsync2 - see todo.md (dgt.power.solution row).
        // All Connection.Execute/RetrieveMultiple calls in this constructor are synchronous; since
        // constructors cannot be async, this requires a static async factory instead.
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

        WebResourceComponents = BuildWebResourceComponents();
        WebResourcesById = BuildWebResourcesById(WebResourceComponents);
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

    /// <summary>
    /// Raw solutioncomponent rows for componenttype=WebResource. Unlike <see cref="SolutionComponentEntries"/>
    /// (which is scoped to ismetadata=true rows for entity/attribute membership), web resources are standalone
    /// components with ismetadata=false, so they need their own, unfiltered-by-ismetadata query.
    /// </summary>
    public IReadOnlyList<SolutionComponent> WebResourceComponents { get; }

    /// <summary>Webresource id -> record (Name/Content/WebResourceType) for every id referenced in <see cref="WebResourceComponents"/>.</summary>
    public IReadOnlyDictionary<Guid, WebResource> WebResourcesById { get; }

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

    // WebResource solutioncomponent rows are standalone components (ismetadata=false), so they are
    // deliberately not part of the ismetadata=true query above - a dedicated, unfiltered-by-ismetadata
    // query is required to find them.
    private List<SolutionComponent> BuildWebResourceComponents()
    {
        var solutionIds = SolutionUniqueNamesById.Keys.ToArray();
        if (solutionIds.Length == 0)
        {
            return [];
        }

        var query = new QueryExpression(SolutionComponent.EntityLogicalName)
        {
            NoLock = true,
            ColumnSet = new ColumnSet(SolutionComponent.LogicalNames.ObjectId, SolutionComponent.LogicalNames.SolutionId)
        };
        query.Criteria.AddCondition(SolutionComponent.LogicalNames.ComponentType, ConditionOperator.Equal, SolutionComponent.Options.ComponentType.WebResource);
        query.Criteria.AddCondition(SolutionComponent.LogicalNames.SolutionId, ConditionOperator.In, solutionIds.Cast<object>().ToArray());

        return RetrieveAllPages(query)
            .Select(static entity => entity.ToEntity<SolutionComponent>())
            .ToList();
    }

    private Dictionary<Guid, WebResource> BuildWebResourcesById(IReadOnlyList<SolutionComponent> webResourceComponents)
    {
        var webResourceIds = webResourceComponents
            .Select(static component => component.ObjectId)
            .Where(static id => id.HasValue)
            .Select(static id => id!.Value)
            .Distinct()
            .ToArray();

        if (webResourceIds.Length == 0)
        {
            return [];
        }

        var query = new QueryExpression(WebResource.EntityLogicalName)
        {
            NoLock = true,
            ColumnSet = new ColumnSet(WebResource.LogicalNames.Name, WebResource.LogicalNames.Content, WebResource.LogicalNames.WebResourceType)
        };
        query.Criteria.AddCondition(WebResource.LogicalNames.WebResourceId, ConditionOperator.In, webResourceIds.Cast<object>().ToArray());

        return RetrieveAllPages(query)
            .Select(static entity => entity.ToEntity<WebResource>())
            .ToDictionary(static webResource => webResource.Id, EqualityComparer<Guid>.Default);
    }

    /// <summary>Pages through every result of a query using <see cref="PageSize"/>, following <c>MoreRecords</c>/<c>PagingCookie</c>.</summary>
    private List<Entity> RetrieveAllPages(QueryExpression query)
    {
        query.PageInfo = new PagingInfo { Count = PageSize, PageNumber = 1 };

        var results = new List<Entity>();
        bool moreRecords;
        do
        {
            var page = Connection.RetrieveMultiple(query);
            results.AddRange(page.Entities);

            moreRecords = page.MoreRecords;
            if (moreRecords)
            {
                query.PageInfo.PageNumber++;
                query.PageInfo.PagingCookie = page.PagingCookie;
            }
        } while (moreRecords);

        return results;
    }
}

