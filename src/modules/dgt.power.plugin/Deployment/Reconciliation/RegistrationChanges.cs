// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using dgt.power.plugin.Deployment.Local;
using dgt.power.plugin.Models;

namespace dgt.power.plugin.Deployment.Reconciliation;

internal sealed record RegistrationChanges(
    IReadOnlyList<LocalPluginType> TypesToCreate,
    IReadOnlyList<RemotePluginType> TypesToDelete,
    IReadOnlyList<StepWrite> StepsToWrite,
    IReadOnlyList<RemotePluginStep> StepsToDelete,
    IReadOnlyList<ImageWrite> ImagesToWrite,
    IReadOnlyList<RemotePluginStepImage> ImagesToDelete,
    IReadOnlyDictionary<string, Guid> ExistingTypeIds,
    IReadOnlyDictionary<PluginStepReference, Guid> ExistingStepIds)
{
    // Placement hints for the preview tree: deleted step id -> owning type name, deleted image id -> owning step id.
    internal IReadOnlyDictionary<Guid, string> DeletedStepTypeNames { get; init; } = new Dictionary<Guid, string>();

    internal IReadOnlyDictionary<Guid, Guid> DeletedImageStepIds { get; init; } = new Dictionary<Guid, Guid>();

    internal int DestructiveCount => ImagesToDelete.Count + StepsToDelete.Count + TypesToDelete.Count;

    internal bool HasChanges =>
        TypesToCreate.Count + TypesToDelete.Count + StepsToWrite.Count + StepsToDelete.Count + ImagesToWrite.Count + ImagesToDelete.Count > 0;
}
