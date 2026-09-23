// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.plugin.Repositories;

/// <summary>
/// Thin access to solution component registration for plugin packages, assemblies, and steps.
/// </summary>
public interface ISolutionComponentRepository
{
    /// <summary>
    /// Looks up the solution component type code for the given entity, for entities (such as
    /// <c>pluginpackage</c>) whose component type is not a fixed well-known constant.
    /// </summary>
    Task<int?> GetComponentTypeAsync(string entityLogicalName, CancellationToken cancellationToken = default);

    /// <summary>Lists component IDs of the specified type already present in a solution.</summary>
    Task<IReadOnlySet<Guid>> ListComponentIdsAsync(
        string solutionUniqueName,
        int componentType,
        CancellationToken cancellationToken = default);

    /// <summary>Adds a component to a solution.</summary>
    Task AddToSolutionAsync(int componentType, Guid componentId, string solutionUniqueName, CancellationToken cancellationToken = default);

}
