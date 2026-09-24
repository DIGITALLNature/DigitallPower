// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using Microsoft.Xrm.Sdk.Metadata;

namespace dgt.power.solution.Base;

/// <summary>Resolves per-(solution, entity) effective component membership from the raw solutioncomponent rows.</summary>
public static class EntityComponentMembershipResolver
{
    /// <summary>Distinct entity MetadataIds referenced by Entity-type solutioncomponent rows, so callers can fetch metadata scoped to just these entities.</summary>
    public static IReadOnlyCollection<Guid> GetReferencedEntityMetadataIds(IReadOnlyDictionary<Guid, SolutionComponent> solutionComponents)
    {
        ArgumentNullException.ThrowIfNull(solutionComponents);

        return solutionComponents.Values
            .Where(IsEntityComponent)
            .Select(static component => component.ObjectId)
            .Where(static id => id.HasValue)
            .Select(static id => id!.Value)
            .Distinct()
            .ToList();
    }

    public static IReadOnlyDictionary<string, EntityComponentMembership> Resolve(
        IReadOnlyDictionary<Guid, SolutionComponent> solutionComponents,
        IReadOnlyDictionary<Guid, string> solutionUniqueNamesById,
        IReadOnlyDictionary<string, EntityMetadata> entityMetadataByLogicalName,
        IReadOnlyDictionary<Guid, AttributeMetadata> attributeMetadataById)
    {
        ArgumentNullException.ThrowIfNull(solutionComponents);
        ArgumentNullException.ThrowIfNull(solutionUniqueNamesById);
        ArgumentNullException.ThrowIfNull(entityMetadataByLogicalName);
        ArgumentNullException.ThrowIfNull(attributeMetadataById);

        var entityLogicalNameByMetadataId = entityMetadataByLogicalName.Values
            .Where(static entity => entity.MetadataId.HasValue)
            .ToDictionary(entity => entity.MetadataId!.Value, entity => entity.LogicalName);

        var memberships = new Dictionary<string, EntityComponentMembership>(StringComparer.Ordinal);

        foreach (var entityComponent in solutionComponents.Values.Where(IsEntityComponent))
        {
            if (!TryResolveEntity(entityComponent, entityLogicalNameByMetadataId, entityMetadataByLogicalName, out var entityLogicalName, out var entityMetadata) ||
                !TryResolveSolutionName(entityComponent, solutionUniqueNamesById, out var solutionUniqueName))
            {
                continue;
            }

            // A missing behavior is treated conservatively as "explicit list only" rather than expanding to everything.
            var behavior = entityComponent.RootComponentBehavior?.Value ?? SolutionComponent.Options.RootComponentBehavior.DoNotIncludeSubcomponents;

            var explicitSubcomponentsByType = solutionComponents.Values
                .Where(candidate => candidate.RootSolutionComponentId == entityComponent.Id)
                .GroupBy(static candidate => candidate.ComponentType?.Value ?? -1)
                .ToDictionary(group => group.Key, group => (IReadOnlyList<SolutionComponent>)[.. group]);

            var key = BuildKey(solutionUniqueName, entityLogicalName);
            memberships[key] = new EntityComponentMembership
            {
                SolutionUniqueName = solutionUniqueName,
                EntityLogicalName = entityLogicalName,
                EntityComponent = entityComponent,
                RootComponentBehavior = behavior,
                IsTableManaged = entityMetadata.IsManaged == true,
                ExplicitSubcomponentsByType = explicitSubcomponentsByType,
                EffectiveAttributes = ResolveEffectiveAttributes(behavior, entityMetadata, explicitSubcomponentsByType, attributeMetadataById)
            };
        }

        return memberships;
    }

    private static bool TryResolveEntity(
        SolutionComponent entityComponent,
        Dictionary<Guid, string> entityLogicalNameByMetadataId,
        IReadOnlyDictionary<string, EntityMetadata> entityMetadataByLogicalName,
        out string entityLogicalName,
        out EntityMetadata entityMetadata)
    {
        entityLogicalName = string.Empty;
        entityMetadata = null!;

        return entityComponent.ObjectId is { } entityMetadataId &&
            entityLogicalNameByMetadataId.TryGetValue(entityMetadataId, out entityLogicalName!) &&
            entityMetadataByLogicalName.TryGetValue(entityLogicalName, out entityMetadata!);
    }

    private static bool TryResolveSolutionName(
        SolutionComponent entityComponent,
        IReadOnlyDictionary<Guid, string> solutionUniqueNamesById,
        out string solutionUniqueName)
    {
        solutionUniqueName = string.Empty;
        return entityComponent.SolutionId?.Id is { } solutionId &&
            solutionUniqueNamesById.TryGetValue(solutionId, out solutionUniqueName!);
    }

    public static string BuildKey(string solutionUniqueName, string entityLogicalName) =>
        string.Join('|', solutionUniqueName, entityLogicalName);

    private static bool IsEntityComponent(SolutionComponent component) =>
        component.ComponentType?.Value == SolutionComponent.Options.ComponentType.Entity;

    private static IReadOnlyList<AttributeMetadata> ResolveEffectiveAttributes(
        int behavior,
        EntityMetadata entityMetadata,
        Dictionary<int, IReadOnlyList<SolutionComponent>> explicitSubcomponentsByType,
        IReadOnlyDictionary<Guid, AttributeMetadata> attributeMetadataById)
    {
        if (behavior == SolutionComponent.Options.RootComponentBehavior.IncludeSubcomponents)
        {
            return entityMetadata.Attributes ?? [];
        }

        if (behavior == SolutionComponent.Options.RootComponentBehavior.IncludeAsShellOnly ||
            !explicitSubcomponentsByType.TryGetValue(SolutionComponent.Options.ComponentType.Attribute, out var explicitAttributeComponents))
        {
            return [];
        }

        return explicitAttributeComponents
            .Where(static component => component.ObjectId.HasValue)
            .Select(component => attributeMetadataById.GetValueOrDefault(component.ObjectId!.Value))
            .Where(static attribute => attribute != null)
            .Select(static attribute => attribute!)
            .ToList();
    }
}
