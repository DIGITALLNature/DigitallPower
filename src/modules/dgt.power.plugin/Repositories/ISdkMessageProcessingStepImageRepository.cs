// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Remote;

namespace dgt.power.plugin.Repositories;

/// <summary>
/// Thin CRUD access to <c>sdkmessageprocessingstepimage</c> records. Contains no Create/Update
/// decision logic - see <see cref="dgt.power.plugin.Planning.PluginPushPlanner"/> for that.
/// </summary>
public interface ISdkMessageProcessingStepImageRepository
{
    /// <summary>Lists the images currently registered under the given step.</summary>
    Task<IReadOnlyList<RemotePluginStepImage>> ListByStepAsync(Guid stepId, CancellationToken cancellationToken = default);

    /// <summary>Registers a new image and returns its id.</summary>
    Task<Guid> CreateAsync(PluginStepImageData data, CancellationToken cancellationToken = default);

    /// <summary>Replaces the attributes of an existing image.</summary>
    Task UpdateAsync(Guid id, IReadOnlyList<string>? attributes, CancellationToken cancellationToken = default);

    /// <summary>Deletes an image.</summary>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
