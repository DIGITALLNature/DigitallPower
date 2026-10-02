// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Remote;

namespace dgt.power.plugin.Repositories;

/// <summary>
/// Thin CRUD access to <c>plugintype</c> records. Contains no create/unchanged decision logic -
/// see <see cref="dgt.power.plugin.Planning.PluginRegistrationComparer"/> for that.
/// </summary>
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
