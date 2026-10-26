// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using dgt.power.plugin.Deployment.Local;
using dgt.power.plugin.Models;

namespace dgt.power.plugin.Deployment.Reconciliation;

internal sealed record PackageAssemblyChanges(
    LocalAssembly Desired,
    RemoteAssembly? Existing,
    RegistrationChanges Registrations,
    IReadOnlyDictionary<PluginStepReference, ResolvedSdkMessage> Messages);
