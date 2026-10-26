// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using dgt.power.plugin.Models;

namespace dgt.power.plugin.Repositories;

/// <summary>
/// Thin CRUD access to <c>sdkmessageprocessingstep</c> records. Contains no Create/Update decision
/// logic - see <see cref="dgt.power.plugin.Deployment.Reconciliation.PluginDeployment"/> for that.
/// </summary>
// Public repository contract; the deployment core uses the sealed implementations directly.
// ReSharper disable UnusedMemberInSuper.Global
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

    /// <summary>Enables a step by setting statecode=0 and statuscode=1.</summary>
    // ReSharper disable once UnusedMemberInSuper.Global
    Task EnableAsync(Guid stepId, CancellationToken cancellationToken = default);

    /// <summary>Disables a step by setting statecode=1 and statuscode=2.</summary>
    // ReSharper disable once UnusedMemberInSuper.Global
    Task DisableAsync(Guid stepId, CancellationToken cancellationToken = default);

    /// <summary>Retrieves the name and enabled state of a step, returning null if the step does not exist.</summary>
    // ReSharper disable once UnusedMemberInSuper.Global
    Task<RemotePluginStepState?> GetStateAsync(Guid stepId, CancellationToken cancellationToken = default);

    /// <summary>Retrieves the name and enabled state of the given steps; unknown ids are omitted.</summary>
    // ReSharper disable once UnusedMemberInSuper.Global
    Task<IReadOnlyList<RemotePluginStepState>> ListStatesAsync(IReadOnlyCollection<Guid> stepIds, CancellationToken cancellationToken = default);

    /// <summary>Updates the description field of a step.</summary>
    // ReSharper disable once UnusedMemberInSuper.Global
    Task UpdateDescriptionAsync(Guid stepId, string description, CancellationToken cancellationToken = default);
}
