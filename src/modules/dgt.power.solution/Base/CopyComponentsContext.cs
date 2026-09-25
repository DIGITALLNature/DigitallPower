// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.solution.Base;

/// <summary>
/// Fetches every solutioncomponent row across the given source solutions (deduped by
/// componenttype+objectid), resolves managed state and active-layer state where needed, and turns
/// that into the final <see cref="ComponentCopyDecision"/> list for `solution copy-components`.
/// </summary>
public sealed class CopyComponentsContext
{
    private const int PageSize = 5000;
    private const int MaxConcurrentEntityMetadataRequests = 8;

    private static readonly IReadOnlyDictionary<(int, Guid), bool> EmptyLookup = new Dictionary<(int, Guid), bool>();

    private CopyComponentsContext(IReadOnlyList<SolutionComponent> components)
    {
        Components = components;
    }

    private IReadOnlyList<SolutionComponent> Components { get; }

    public static Task<CopyComponentsContext> CreateAsync(IOrganizationServiceAsync2 connection, IReadOnlyCollection<Guid> sourceSolutionIds, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(connection);
        ArgumentNullException.ThrowIfNull(sourceSolutionIds);
        return CreateCoreAsync(connection, sourceSolutionIds, cancellationToken);
    }

    private static async Task<CopyComponentsContext> CreateCoreAsync(IOrganizationServiceAsync2 connection, IReadOnlyCollection<Guid> sourceSolutionIds, CancellationToken cancellationToken)
    {
        var components = await FetchComponentsAsync(connection, sourceSolutionIds, cancellationToken);
        return new CopyComponentsContext(components);
    }

    /// <summary>Resolves managed state, active layers, and the per-component behavior/inclusion decision.</summary>
    public Task<IReadOnlyList<ComponentCopyDecision>> BuildDecisionsAsync(IOrganizationServiceAsync2 connection, bool bestPractices, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(connection);
        return BuildDecisionsCoreAsync(connection, bestPractices, cancellationToken);
    }

    private async Task<IReadOnlyList<ComponentCopyDecision>> BuildDecisionsCoreAsync(IOrganizationServiceAsync2 connection, bool bestPractices, CancellationToken cancellationToken)
    {
        // solutioncomponentdefinition is a small, cheap lookup needed for the plan's type names in both
        // modes. Everything else below (entity/attribute metadata, backing-table, and layer lookups) only
        // exists to support best-practice filtering, so raw mode skips it entirely - it neither needs nor
        // should pay for those queries (and shouldn't fail because one of them errors out).
        var definitionsByType = await RetrieveComponentDefinitionsAsync(connection, cancellationToken);

        if (!bestPractices)
        {
            return BuildAllDecisions(bestPractices: false, EmptyLookup, EmptyLookup, definitionsByType);
        }

        var entityObjectIds = Components
            .Where(static component => component.ComponentType?.Value == SolutionComponent.Options.ComponentType.Entity && component.ObjectId.HasValue)
            .Select(static component => component.ObjectId!.Value)
            .Distinct()
            .ToArray();

        var entities = await RetrieveEntityMetadataAsync(connection, entityObjectIds, cancellationToken);
        var entityMetadataByMetadataId = entities
            .Where(static entity => entity.MetadataId.HasValue)
            .ToDictionary(static entity => entity.MetadataId!.Value, static entity => entity);
        var attributeMetadataById = entities
            .SelectMany(static entity => entity.Attributes ?? Array.Empty<AttributeMetadata>())
            .Where(static attribute => attribute.MetadataId.HasValue)
            .GroupBy(static attribute => attribute.MetadataId!.Value)
            .ToDictionary(group => group.Key, group => group.First());

        var managedStateResolver = new ComponentManagedStateResolver(connection);
        var managedByComponent = await managedStateResolver.ResolveAsync(Components, entityMetadataByMetadataId, attributeMetadataById, definitionsByType, cancellationToken);

        // Active layer only matters for managed, non-Entity components (Entity itself is always kept as an anchor) - skip the lookup for everything else.
        var activeLayerCandidates = Components
            .Where(component => component.ComponentType?.Value is { } type &&
                type != SolutionComponent.Options.ComponentType.Entity &&
                component.ObjectId.HasValue &&
                managedByComponent.GetValueOrDefault((type, component.ObjectId!.Value)))
            .ToList();

        var activeLayerResolver = new ComponentActiveLayerResolver(connection);
        var hasActiveLayerByComponent = await activeLayerResolver.ResolveAsync(activeLayerCandidates, definitionsByType, cancellationToken);

        return BuildAllDecisions(bestPractices: true, managedByComponent, hasActiveLayerByComponent, definitionsByType);
    }

    private IReadOnlyList<ComponentCopyDecision> BuildAllDecisions(
        bool bestPractices,
        IReadOnlyDictionary<(int, Guid), bool> managedByComponent,
        IReadOnlyDictionary<(int, Guid), bool> hasActiveLayerByComponent,
        IReadOnlyDictionary<int, SolutionComponentDefinitionInfo> definitionsByType) =>
        Components
            .Where(static component => component.ComponentType?.Value != null && component.ObjectId.HasValue)
            .Select(component => BuildDecision(component, bestPractices, managedByComponent, hasActiveLayerByComponent, definitionsByType))
            .ToList();

    private static ComponentCopyDecision BuildDecision(
        SolutionComponent component,
        bool bestPractices,
        IReadOnlyDictionary<(int, Guid), bool> managedByComponent,
        IReadOnlyDictionary<(int, Guid), bool> hasActiveLayerByComponent,
        IReadOnlyDictionary<int, SolutionComponentDefinitionInfo> definitionsByType)
    {
        var componentType = component.ComponentType!.Value;
        var objectId = component.ObjectId!.Value;
        var key = (componentType, objectId);
        var typeName = definitionsByType.GetValueOrDefault(componentType).Name ?? $"componenttype {componentType}";

        if (componentType == SolutionComponent.Options.ComponentType.Entity)
        {
            var isManaged = managedByComponent.GetValueOrDefault(key);
            // AddSolutionComponentRequest.DoNotIncludeSubcomponents is a plain bool, so it can only ever
            // distinguish complete (IncludeSubcomponents) from non-complete - a source's own
            // IncludeAsShellOnly cannot be preserved as such; it collapses into non-complete here too.
            var doNotIncludeSubcomponents = bestPractices
                ? isManaged
                : component.RootComponentBehavior?.Value != SolutionComponent.Options.RootComponentBehavior.IncludeSubcomponents;

            string reason;
            if (!bestPractices)
            {
                reason = "Raw mode - preserves complete vs. non-complete table behavior (shell-only sources are treated as non-complete)";
            }
            else if (isManaged)
            {
                reason = "Managed table - added as skeleton only (delta)";
            }
            else
            {
                reason = "Unmanaged table - added completely";
            }

            return new ComponentCopyDecision(componentType, objectId, typeName, Include: true, doNotIncludeSubcomponents, reason);
        }

        if (!bestPractices)
        {
            return new ComponentCopyDecision(componentType, objectId, typeName, Include: true, DoNotIncludeSubcomponents: false, "Raw mode - copied as-is");
        }

        var managed = managedByComponent.GetValueOrDefault(key);
        if (!managed)
        {
            return new ComponentCopyDecision(componentType, objectId, typeName, Include: true, DoNotIncludeSubcomponents: false, "Unmanaged component - added completely");
        }

        var hasActiveLayer = hasActiveLayerByComponent.GetValueOrDefault(key);
        return hasActiveLayer
            ? new ComponentCopyDecision(componentType, objectId, typeName, Include: true, DoNotIncludeSubcomponents: false, "Managed component with an active customization layer")
            : new ComponentCopyDecision(componentType, objectId, typeName, Include: false, DoNotIncludeSubcomponents: false, "Managed component without an active layer - redundant, skipped");
    }

    private static async Task<List<EntityMetadata>> RetrieveEntityMetadataAsync(IOrganizationServiceAsync2 connection, Guid[] entityMetadataIds, CancellationToken cancellationToken)
    {
        if (entityMetadataIds.Length == 0)
        {
            return [];
        }

        var entities = new System.Collections.Concurrent.ConcurrentBag<EntityMetadata>();

        await Parallel.ForEachAsync(
            entityMetadataIds,
            new ParallelOptions { MaxDegreeOfParallelism = MaxConcurrentEntityMetadataRequests, CancellationToken = cancellationToken },
            async (metadataId, ct) =>
            {
                var response = (RetrieveEntityResponse)await connection.ExecuteAsync(new RetrieveEntityRequest
                {
                    MetadataId = metadataId,
                    EntityFilters = EntityFilters.Entity | EntityFilters.Attributes
                }, ct);

                entities.Add(response.EntityMetadata);
            });

        return [.. entities];
    }

    // Fetched unfiltered (not scoped to the componenttypes actually present) - solutioncomponentdefinition
    // is a small, effectively-static system table, so a single unconditional query is cheaper and simpler
    // than building an IN(...) filter from the distinct componenttypes seen in this run.
    private static async Task<Dictionary<int, SolutionComponentDefinitionInfo>> RetrieveComponentDefinitionsAsync(IOrganizationServiceAsync2 connection, CancellationToken cancellationToken)
    {
        var query = new QueryExpression(SolutionComponentDefinition.EntityLogicalName)
        {
            NoLock = true,
            ColumnSet = new ColumnSet(
                SolutionComponentDefinition.LogicalNames.SolutionComponentType,
                SolutionComponentDefinition.LogicalNames.Name,
                SolutionComponentDefinition.LogicalNames.PrimaryEntityName)
        };

        var rows = (await connection.RetrieveMultipleAsync(query, cancellationToken)).Entities
            .Select(static entity => entity.ToEntity<SolutionComponentDefinition>())
            .Where(static definition => definition.SolutionComponentType.HasValue)
            .ToList();

        return rows.ToDictionary(
            static definition => definition.SolutionComponentType!.Value,
            static definition => new SolutionComponentDefinitionInfo(definition.Name, definition.PrimaryEntityName));
    }

    private static async Task<List<SolutionComponent>> FetchComponentsAsync(IOrganizationServiceAsync2 connection, IReadOnlyCollection<Guid> sourceSolutionIds, CancellationToken cancellationToken)
    {
        if (sourceSolutionIds.Count == 0)
        {
            return [];
        }

        var query = new QueryExpression(SolutionComponent.EntityLogicalName)
        {
            NoLock = true,
            ColumnSet = new ColumnSet(
                SolutionComponent.LogicalNames.ComponentType,
                SolutionComponent.LogicalNames.ObjectId,
                SolutionComponent.LogicalNames.SolutionId,
                SolutionComponent.LogicalNames.RootComponentBehavior,
                SolutionComponent.LogicalNames.RootSolutionComponentId)
        };
        query.Criteria.AddCondition(SolutionComponent.LogicalNames.SolutionId, ConditionOperator.In, sourceSolutionIds.Cast<object>().ToArray());
        query.PageInfo = new PagingInfo { Count = PageSize, PageNumber = 1 };

        var rows = new List<Entity>();
        bool moreRecords;
        do
        {
            var page = await connection.RetrieveMultipleAsync(query, cancellationToken);
            rows.AddRange(page.Entities);
            moreRecords = page.MoreRecords;
            if (moreRecords)
            {
                query.PageInfo.PageNumber++;
                query.PageInfo.PagingCookie = page.PagingCookie;
            }
        } while (moreRecords);

        // Dedupe by (componenttype, objectid): the same component can legitimately appear in
        // multiple source solutions. When behaviors disagree, the most complete one wins
        // (IncludeSubcomponents=0 < DoNotIncludeSubcomponents=1 < IncludeAsShellOnly=2).
        return rows
            .Select(static entity => entity.ToEntity<SolutionComponent>())
            .Where(static component => component.ComponentType?.Value != null && component.ObjectId.HasValue)
            .GroupBy(static component => (component.ComponentType!.Value, component.ObjectId!.Value))
            .Select(static group => group.OrderBy(static component => component.RootComponentBehavior?.Value ?? int.MaxValue).First())
            .ToList();
    }
}
