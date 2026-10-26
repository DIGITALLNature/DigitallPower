// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

namespace dgt.power.plugin.Deployment.Reconciliation;

internal sealed record PackageSolutionChanges(
    string UniqueName,
    int PackageComponentType,
    bool AddPackage,
    IReadOnlyList<PackageStepMembershipAddition> Steps,
    ProviderSolutionChanges? Providers = null)
{
    internal bool HasChanges => AddPackage || Steps.Count > 0 || Providers is { HasChanges: true };
}
