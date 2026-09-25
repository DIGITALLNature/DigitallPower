// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.plugin.Repositories;

/// <summary>
/// Links plugin types to <c>customapi</c> records by unique name (message name). A Custom API
/// handler type has no <c>sdkmessageprocessingstep</c> - Dataverse invokes it directly via this
/// link instead.
/// </summary>
public interface ICustomApiRepository
{
    /// <summary>Finds the id of a <c>customapi</c> record by its unique name, if any.</summary>
    Task<Guid?> FindIdByUniqueNameAsync(string uniqueName, CancellationToken cancellationToken = default);

    /// <summary>Lists the ids of Custom APIs currently linked to the given plugin type.</summary>
    Task<IReadOnlyList<Guid>> ListLinkedToPluginTypeAsync(Guid pluginTypeId, CancellationToken cancellationToken = default);

    /// <summary>Links a Custom API to the given plugin type.</summary>
    Task LinkPluginTypeAsync(Guid customApiId, Guid pluginTypeId, CancellationToken cancellationToken = default);

    /// <summary>Removes the plugin type link from a Custom API.</summary>
    Task UnlinkPluginTypeAsync(Guid customApiId, CancellationToken cancellationToken = default);
}
