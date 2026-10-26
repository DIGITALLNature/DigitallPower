// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using dgt.power.plugin.Models;

namespace dgt.power.plugin.Deployment.Reconciliation;

/// <summary>
/// An assembly that exists in the package on the server but is no longer part of the desired package.
/// Its plugin types and declared steps are planned for removal; Dataverse removes the assembly itself during upload.
/// </summary>
internal sealed record RemovedPackageAssembly(RemoteAssembly Assembly, RegistrationChanges Registrations);
