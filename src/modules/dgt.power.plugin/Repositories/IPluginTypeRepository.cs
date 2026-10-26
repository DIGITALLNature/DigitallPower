// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using dgt.power.plugin.Models;

namespace dgt.power.plugin.Repositories;

/// <summary>
/// Thin CRUD access to <c>plugintype</c> records. Contains no create/unchanged decision logic -
/// see <see cref="dgt.power.plugin.Deployment.Reconciliation.PluginDeployment"/> for that.
/// </summary>
// Public repository contract; the deployment core uses the sealed implementations directly.
// ReSharper disable UnusedMemberInSuper.Global
public interface IPluginTypeRepository
{
    /// <summary>Well-known solution component type code for <c>plugintype</c>.</summary>
    public const int ComponentType = 90;

    /// <summary>Lists the plugin types currently registered under the given assembly.</summary>
    Task<IReadOnlyList<RemotePluginType>> ListByAssemblyAsync(Guid assemblyId, CancellationToken cancellationToken = default);

    /// <summary>Registers a new plugin type and returns its id.</summary>
    Task<Guid> CreateAsync(Guid assemblyId, string typeName, string name, CancellationToken cancellationToken = default);

    /// <summary>
    /// Finds the ids of <c>sdkmessageprocessingstep</c> records that depend on this plugin type,
    /// so they can be deleted first (Dataverse refuses to delete a type with dependent steps).
    /// </summary>
    Task<IReadOnlyList<Guid>> GetDependentStepIdsAsync(Guid pluginTypeId, CancellationToken cancellationToken = default);

    /// <summary>Deletes a plugin type. Dependent steps must be deleted first.</summary>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
