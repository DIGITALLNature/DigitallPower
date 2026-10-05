// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Remote;

namespace dgt.power.plugin.Repositories;

/// <summary>
/// Thin CRUD access to <c>sdkmessageprocessingstep</c> records. Contains no Create/Update decision
/// logic - see <see cref="dgt.power.plugin.Planning.PluginRegistrationComparer"/> for that.
/// </summary>
public interface ISdkMessageProcessingStepRepository
{
    /// <summary>Well-known solution component type code for <c>sdkmessageprocessingstep</c>.</summary>
    public const int ComponentType = 92;

    /// <summary>Lists the steps currently registered under the given plugin type.</summary>
    Task<IReadOnlyList<RemotePluginStep>> ListByPluginTypeAsync(Guid pluginTypeId, CancellationToken cancellationToken = default);

    /// <summary>Registers a new step and returns its id.</summary>
    Task<Guid> CreateAsync(PluginStepData data, CancellationToken cancellationToken = default);

    /// <summary>Updates the mutable fields of an existing step.</summary>
    Task UpdateAsync(Guid id, PluginStepData data, CancellationToken cancellationToken = default);

    /// <summary>Deletes a step.</summary>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Re-points an existing step to a different plugin type, keeping everything else (filters,
    /// images, history) unchanged. Used to migrate steps off an outdated assembly onto its
    /// replacement before the outdated assembly is purged.
    /// </summary>
    Task ReassignPluginTypeAsync(Guid stepId, Guid newPluginTypeId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Finds steps matching the provided composite key criteria. Used for resolving a step
    /// without knowing its id.
    /// </summary>
    // Retain the contract for planned constructor injection; commands currently construct repositories directly.
    // ReSharper disable once UnusedMemberInSuper.Global
    Task<IReadOnlyList<RemotePluginStep>> FindByCompositeKeyAsync(
        string? pluginTypeName,
        string? messageName,
        int? stageValue,
        string? primaryEntityName,
        string? secondaryEntityName,
        int? executionOrder,
        CancellationToken cancellationToken = default);

    /// <summary>Updates the unsecure configuration on a step.</summary>
    // Retain the contract for planned constructor injection; commands currently construct repositories directly.
    // ReSharper disable once UnusedMemberInSuper.Global
    Task UpdateConfigurationAsync(Guid stepId, string? configuration, CancellationToken cancellationToken = default);
}
