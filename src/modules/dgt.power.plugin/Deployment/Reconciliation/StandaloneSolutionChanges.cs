// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Deployment.Planning.Plans;

namespace dgt.power.plugin.Deployment.Reconciliation;

internal sealed record StandaloneSolutionChanges(string UniqueName, bool AddAssembly, IReadOnlyList<StepMembershipAddition> Steps,
    ProviderSolutionChanges? Providers = null)
{
    internal bool HasChanges => AddAssembly || Steps.Count > 0 || Providers is { HasChanges: true };
}
