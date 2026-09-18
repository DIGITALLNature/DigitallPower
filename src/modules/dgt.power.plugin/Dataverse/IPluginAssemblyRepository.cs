// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Planning;

namespace dgt.power.plugin.Dataverse;

/// <summary>
/// Thin CRUD access to <c>pluginassembly</c> records. Contains no Create/Update/Upgrade decision
/// logic - see <see cref="PluginPushPlanner"/> for that.
/// </summary>
public interface IPluginAssemblyRepository
{
    /// <summary>Well-known solution component type code for <c>pluginassembly</c>.</summary>
    public const int ComponentType = 91;

    /// <summary>
    /// Finds the most recently registered sandboxed plugin assembly with the given name, if any.
    /// </summary>
    Task<RemoteAssembly?> FindByNameAsync(string name, CancellationToken cancellationToken = default);

    /// <summary>
    /// Lists every other sandboxed plugin assembly registered under the given name - i.e. the
    /// versions superseded by an Upgrade - excluding <paramref name="excludeId"/> (the just
    /// created/updated assembly).
    /// </summary>
    Task<IReadOnlyList<RemoteAssembly>> ListOutdatedAsync(string name, Guid excludeId, CancellationToken cancellationToken = default);

    /// <summary>Registers a new plugin assembly and returns its id.</summary>
    Task<Guid> CreateAsync(string name, string content, CancellationToken cancellationToken = default);

    /// <summary>Replaces the content (and reported version) of an existing plugin assembly.</summary>
    Task UpdateContentAsync(Guid id, string content, CancellationToken cancellationToken = default);

    /// <summary>Deletes a plugin assembly.</summary>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
