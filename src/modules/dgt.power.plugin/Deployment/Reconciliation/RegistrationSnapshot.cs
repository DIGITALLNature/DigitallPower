// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using dgt.power.plugin.Models;

namespace dgt.power.plugin.Deployment.Reconciliation;

internal sealed record RegistrationSnapshot(
    IReadOnlyList<RemotePluginType> Types,
    IReadOnlyDictionary<Guid, IReadOnlyList<RemotePluginStep>> StepsByType,
    IReadOnlyDictionary<Guid, IReadOnlyList<RemotePluginStepImage>> ImagesByStep);
