// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Deployment.Planning.Plans;

namespace dgt.power.plugin.Deployment.Reconciliation;

internal sealed record PackageSolutionChanges(
    string UniqueName,
    int PackageComponentType,
    bool AddPackage,
    IReadOnlyList<PackageStepMembershipAddition> Steps)
{
    internal bool HasChanges => AddPackage || Steps.Count > 0;
}
