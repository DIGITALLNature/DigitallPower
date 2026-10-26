// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

namespace dgt.power.plugin.Deployment.Reconciliation;

internal sealed record ResolvedPackageAssembly(
    Guid AssemblyId,
    PackageAssemblyChanges Changes,
    RegistrationChanges Registrations);
