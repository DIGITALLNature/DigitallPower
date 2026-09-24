// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.solution.Base;

public class LintContext
{
    private const int PageSize = 5000;

    private LintContext(IOrganizationServiceAsync2 connection, IReadOnlyList<string> solutionNames)
    {
        Connection = connection;
        SolutionNames = solutionNames;
    }

    public static Task<LintContext> CreateAsync(IOrganizationServiceAsync2 connection, IReadOnlyList<string> solutionNames, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(connection);
        return CreateCoreAsync(connection, solutionNames, cancellationToken);
    }

    private static async Task<LintContext> CreateCoreAsync(IOrganizationServiceAsync2 connection, IReadOnlyList<string> solutionNames, CancellationToken cancellationToken)
    {
        var context = new LintContext(connection, solutionNames);

        var entities = ((RetrieveAllEntitiesResponse)await connection.ExecuteAsync(new RetrieveAllEntitiesRequest
        {
            EntityFilters = EntityFilters.Entity | EntityFilters.Attributes
        }, cancellationToken)).EntityMetadata;

        context.EntityMetadata = entities.ToDictionary(x => x.LogicalName, StringComparer.OrdinalIgnoreCase);
        context.AttributeMetadataById = entities
            .SelectMany(static entity => entity.Attributes ?? Array.Empty<AttributeMetadata>())
            .Where(static attribute => attribute.MetadataId.HasValue)
            .GroupBy(static attribute => attribute.MetadataId!.Value)
            .ToDictionary(group => group.Key, group => group.First(), EqualityComparer<Guid>.Default);

        var (components, solutionNamesById) = await context.BuildSolutionComponentEntriesAsync(cancellationToken);
        context.SolutionUniqueNamesById = solutionNamesById;
        context.EntityMemberships = EntityComponentMembershipResolver.Resolve(components, solutionNamesById, context.EntityMetadata, context.AttributeMetadataById);

        context.WebResourceComponents = await context.BuildWebResourceComponentsAsync(cancellationToken);
        context.WebResourcesById = await context.BuildWebResourcesByIdAsync(context.WebResourceComponents, cancellationToken);

        return context;
    }

    private IOrganizationServiceAsync2 Connection { get; }

    private IReadOnlyList<string> SolutionNames { get; }

    private IReadOnlyDictionary<string, EntityMetadata> EntityMetadata { get; set; } = null!;

    private IReadOnlyDictionary<Guid, AttributeMetadata> AttributeMetadataById { get; set; } = null!;

    /// <summary>Solution id -> unique name, resolved once from the same query used to scope components (avoids relying on the unreliable EntityReference.Name).</summary>
    public IReadOnlyDictionary<Guid, string> SolutionUniqueNamesById { get; private set; } = null!;

    /// <summary>Keyed by <see cref="EntityComponentMembershipResolver.BuildKey"/> (solution unique name + entity logical name).</summary>
    public IReadOnlyDictionary<string, EntityComponentMembership> EntityMemberships { get; private set; } = null!;

    /// <summary>
    /// Raw solutioncomponent rows for componenttype=WebResource. Unlike the ismetadata=true query behind
    /// <see cref="EntityMemberships"/>, web resources are standalone components with ismetadata=false, so
    /// they need their own, unfiltered-by-ismetadata query.
    /// </summary>
    public IReadOnlyList<SolutionComponent> WebResourceComponents { get; private set; } = null!;

    /// <summary>Webresource id -> record (Name/Content/WebResourceType) for every id referenced in <see cref="WebResourceComponents"/>.</summary>
    public IReadOnlyDictionary<Guid, WebResource> WebResourcesById { get; private set; } = null!;

    // Both queries push their filter (solution unique name / solution id) into the server-side
    // condition via ConditionOperator.In - the LINQ-to-QueryExpression provider does not support
    // translating a captured HashSet<T>.Contains(...) call, so raw QueryExpression is required
    // here (see BaseAnalyze.GetSolutionComponents for the same pattern).
    private async Task<(Dictionary<Guid, SolutionComponent> Components, Dictionary<Guid, string> SolutionNamesById)> BuildSolutionComponentEntriesAsync(CancellationToken cancellationToken)
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

        var solutionRows = (await Connection.RetrieveMultipleAsync(solutionQuery, cancellationToken)).Entities
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
                SolutionComponent.LogicalNames.RootSolutionComponentId)
        };
        componentQuery.Criteria.AddCondition(SolutionComponent.LogicalNames.IsMetadata, ConditionOperator.Equal, true);
        componentQuery.Criteria.AddCondition(SolutionComponent.LogicalNames.SolutionId, ConditionOperator.In, solutionIds.Cast<object>().ToArray());

        var components = (await RetrieveAllPagesAsync(componentQuery, cancellationToken))
            .Select(static entity => entity.ToEntity<SolutionComponent>())
            .ToDictionary(static component => component.Id);

        return (components, solutionNamesById);
    }

    // WebResource solutioncomponent rows are standalone components (ismetadata=false), so they are
    // deliberately not part of the ismetadata=true query above - a dedicated, unfiltered-by-ismetadata
    // query is required to find them.
    private async Task<List<SolutionComponent>> BuildWebResourceComponentsAsync(CancellationToken cancellationToken)
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

        return (await RetrieveAllPagesAsync(query, cancellationToken))
            .Select(static entity => entity.ToEntity<SolutionComponent>())
            .ToList();
    }

    private async Task<Dictionary<Guid, WebResource>> BuildWebResourcesByIdAsync(IReadOnlyList<SolutionComponent> webResourceComponents, CancellationToken cancellationToken)
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

        return (await RetrieveAllPagesAsync(query, cancellationToken))
            .Select(static entity => entity.ToEntity<WebResource>())
            .ToDictionary(static webResource => webResource.Id, EqualityComparer<Guid>.Default);
    }

    /// <summary>Pages through every result of a query using <see cref="PageSize"/>, following <c>MoreRecords</c>/<c>PagingCookie</c>.</summary>
    private async Task<List<Entity>> RetrieveAllPagesAsync(QueryExpression query, CancellationToken cancellationToken)
    {
        query.PageInfo = new PagingInfo { Count = PageSize, PageNumber = 1 };

        var results = new List<Entity>();
        bool moreRecords;
        do
        {
            var page = await Connection.RetrieveMultipleAsync(query, cancellationToken);
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

