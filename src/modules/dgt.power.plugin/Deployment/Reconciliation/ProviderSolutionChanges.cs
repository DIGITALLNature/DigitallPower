// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

namespace dgt.power.plugin.Deployment.Reconciliation;

internal sealed record ProviderSolutionChanges(int ComponentType, IReadOnlyList<ProviderMembershipAddition> Additions)
{
    internal bool HasChanges => Additions.Count > 0;
}
