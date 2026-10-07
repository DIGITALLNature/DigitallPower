// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Deployment.Planning.Plans;

namespace dgt.power.plugin.Deployment.Reconciliation;

internal sealed record ProviderSolutionChanges(int ComponentType, IReadOnlyList<ProviderMembershipAddition> Additions)
{
    internal bool HasChanges => Additions.Count > 0;
}
