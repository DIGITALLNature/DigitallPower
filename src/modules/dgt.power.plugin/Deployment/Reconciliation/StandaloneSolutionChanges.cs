// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

namespace dgt.power.plugin.Deployment.Reconciliation;

internal sealed record StandaloneSolutionChanges(string UniqueName, bool AddAssembly, IReadOnlyList<StepMembershipAddition> Steps,
    ProviderSolutionChanges? Providers = null)
{
    internal bool HasChanges => AddAssembly || Steps.Count > 0 || Providers is { HasChanges: true };
}
