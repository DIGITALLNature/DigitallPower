// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Models;

namespace dgt.power.plugin.Repositories;

/// <summary>
/// Thin CRUD access to <c>pluginpackage</c> records. Contains no Create/Update decision logic -
/// see <see cref="dgt.power.plugin.Deployment.Reconciliation.PluginDeployment"/> for that.
/// </summary>
// Public repository contract; the deployment core uses the sealed implementations directly.
// ReSharper disable UnusedMemberInSuper.Global
public interface IPluginPackageRepository
{
    /// <summary>
    /// Finds the plugin package with the exact Dataverse name, if any.
    /// </summary>
    Task<RemotePackage?> FindByNameAsync(string name, CancellationToken cancellationToken = default);

    /// <summary>Registers a new plugin package and returns its id.</summary>
    Task<Guid> CreateAsync(string name, string version, string content, CancellationToken cancellationToken = default);

    /// <summary>Replaces the content of an existing plugin package. Version is intentionally not updatable.</summary>
    Task UpdateContentAsync(Guid id, string content, CancellationToken cancellationToken = default);
}
