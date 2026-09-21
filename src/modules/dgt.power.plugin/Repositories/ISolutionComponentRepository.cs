// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.plugin.Repositories;

/// <summary>
/// Thin access to solution component registration, used to add newly created plugin
/// assemblies/packages to a solution.
/// </summary>
public interface ISolutionComponentRepository
{
    /// <summary>
    /// Looks up the solution component type code for the given entity, for entities (such as
    /// <c>pluginpackage</c>) whose component type is not a fixed well-known constant.
    /// </summary>
    Task<int?> GetComponentTypeAsync(string entityLogicalName, CancellationToken cancellationToken = default);

    /// <summary>Adds a component to a solution.</summary>
    Task AddToSolutionAsync(int componentType, Guid componentId, string solutionUniqueName, CancellationToken cancellationToken = default);

    /// <summary>
    /// Looks up the customization prefix of the solution's publisher, for naming newly created
    /// components (e.g. plugin packages). Returns <paramref name="defaultValue"/> when the
    /// solution is not found or not set.
    /// </summary>
}
