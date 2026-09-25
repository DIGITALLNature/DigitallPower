// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.solution.Base;

/// <summary>
/// Resolves whether the record/metadata behind a solutioncomponent row is managed, keyed by
/// (componenttype, objectid). Entity/Attribute are metadata-driven (no extra query beyond what
/// <see cref="CopyComponentsContext"/> already fetched); every other componenttype is resolved by
/// querying the backing table's own <c>ismanaged</c> column. Componenttypes with no backing table
/// (pure metadata, e.g. Relationship/OptionSet) or whose backing table has no <c>ismanaged</c>
/// column fail open (treated as unmanaged - always included) since best-practice filtering can only
/// ever be applied where a managed state is actually observable.
/// </summary>
public sealed class ComponentManagedStateResolver(IOrganizationServiceAsync2 connection)
{
    // Bounds concurrent RetrieveEntityRequest calls while resolving distinct backing tables.
    private const int MaxConcurrentEntityMetadataRequests = 8;

    public Task<IReadOnlyDictionary<(int ComponentType, Guid ObjectId), bool>> ResolveAsync(
        IReadOnlyCollection<SolutionComponent> components,
        IReadOnlyDictionary<Guid, EntityMetadata> entityMetadataByMetadataId,
        IReadOnlyDictionary<Guid, AttributeMetadata> attributeMetadataById,
        IReadOnlyDictionary<int, SolutionComponentDefinitionInfo> definitionsByType,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(components);
        ArgumentNullException.ThrowIfNull(entityMetadataByMetadataId);
        ArgumentNullException.ThrowIfNull(attributeMetadataById);
        ArgumentNullException.ThrowIfNull(definitionsByType);

        return ResolveCoreAsync(components, entityMetadataByMetadataId, attributeMetadataById, definitionsByType, cancellationToken);
    }

    private async Task<IReadOnlyDictionary<(int ComponentType, Guid ObjectId), bool>> ResolveCoreAsync(
        IReadOnlyCollection<SolutionComponent> components,
        IReadOnlyDictionary<Guid, EntityMetadata> entityMetadataByMetadataId,
        IReadOnlyDictionary<Guid, AttributeMetadata> attributeMetadataById,
        IReadOnlyDictionary<int, SolutionComponentDefinitionInfo> definitionsByType,
        CancellationToken cancellationToken)
    {
        var result = new Dictionary<(int, Guid), bool>();

        foreach (var component in components)
        {
            if (component.ObjectId is not { } objectId || component.ComponentType?.Value is not { } type)
            {
                continue;
            }

            if (type == SolutionComponent.Options.ComponentType.Entity && entityMetadataByMetadataId.TryGetValue(objectId, out var entityMetadata))
            {
                result[(type, objectId)] = entityMetadata.IsManaged == true;
            }
            else if (type == SolutionComponent.Options.ComponentType.Attribute && attributeMetadataById.TryGetValue(objectId, out var attributeMetadata))
            {
                result[(type, objectId)] = attributeMetadata.IsManaged == true;
            }
        }

        var recordBasedGroups = components
            .Where(component => component.ComponentType?.Value is { } type &&
                type != SolutionComponent.Options.ComponentType.Entity &&
                type != SolutionComponent.Options.ComponentType.Attribute &&
                component.ObjectId.HasValue)
            .GroupBy(component => component.ComponentType!.Value)
            .ToList();

        await Parallel.ForEachAsync(
            recordBasedGroups,
            new ParallelOptions { MaxDegreeOfParallelism = MaxConcurrentEntityMetadataRequests, CancellationToken = cancellationToken },
            async (group, ct) =>
            {
                var managedById = await ResolveGroupAsync(group, definitionsByType, ct);
                lock (result)
                {
                    foreach (var pair in managedById)
                    {
                        result[(group.Key, pair.Key)] = pair.Value;
                    }
                }
            });

        return result;
    }

    private async Task<Dictionary<Guid, bool>> ResolveGroupAsync(
        IGrouping<int, SolutionComponent> group,
        IReadOnlyDictionary<int, SolutionComponentDefinitionInfo> definitionsByType,
        CancellationToken cancellationToken)
    {
        var objectIds = group.Select(static component => component.ObjectId!.Value).Distinct().ToArray();

        if (!definitionsByType.TryGetValue(group.Key, out var definition) || string.IsNullOrEmpty(definition.PrimaryEntityName))
        {
            // No backing table for this componenttype (e.g. Relationship/OptionSet) - fail open.
            return objectIds.ToDictionary(static id => id, static _ => false);
        }

        var response = (RetrieveEntityResponse)await connection.ExecuteAsync(new RetrieveEntityRequest
        {
            LogicalName = definition.PrimaryEntityName,
            EntityFilters = EntityFilters.Entity | EntityFilters.Attributes
        }, cancellationToken);

        var backingEntity = response.EntityMetadata;
        var hasIsManagedColumn = backingEntity.Attributes != null && Array.Exists(backingEntity.Attributes, static attribute => attribute.LogicalName == "ismanaged");
        if (!hasIsManagedColumn || string.IsNullOrEmpty(backingEntity.PrimaryIdAttribute))
        {
            return objectIds.ToDictionary(static id => id, static _ => false);
        }

        var query = new QueryExpression(definition.PrimaryEntityName)
        {
            NoLock = true,
            ColumnSet = new ColumnSet(backingEntity.PrimaryIdAttribute, "ismanaged")
        };
        query.Criteria.AddCondition(backingEntity.PrimaryIdAttribute, ConditionOperator.In, objectIds.Cast<object>().ToArray());

        var rows = (await connection.RetrieveMultipleAsync(query, cancellationToken)).Entities;
        var managedById = rows.ToDictionary(static entity => entity.Id, static entity => entity.GetAttributeValue<bool>("ismanaged"));

        // A record referenced by a solutioncomponent row but missing from the query result (e.g. already deleted) fails open too.
        return objectIds.ToDictionary(id => id, id => managedById.GetValueOrDefault(id));
    }
}
