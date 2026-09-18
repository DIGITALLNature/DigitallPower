// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Planning;

namespace dgt.power.plugin.Dataverse;

/// <summary>
/// Thin CRUD access to <c>pluginpackage</c> records. Contains no Create/Update decision logic -
/// see <see cref="PluginPushPlanner"/> for that.
/// </summary>
public interface IPluginPackageRepository
{
    /// <summary>
    /// Finds the plugin package whose (solution-prefixed) name ends with the given unprefixed
    /// package name, if any. When more than one match exists (e.g. deployed under different
    /// solution prefixes), the most recently created one is returned.
    /// </summary>
    Task<RemotePackage?> FindByNameAsync(string name, CancellationToken cancellationToken = default);

    /// <summary>Registers a new plugin package and returns its id.</summary>
    Task<Guid> CreateAsync(string name, string version, string content, CancellationToken cancellationToken = default);

    /// <summary>Replaces the content of an existing plugin package. Version is intentionally not updatable.</summary>
    Task UpdateContentAsync(Guid id, string content, CancellationToken cancellationToken = default);
}
