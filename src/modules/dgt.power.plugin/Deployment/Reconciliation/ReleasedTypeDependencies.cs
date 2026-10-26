// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

namespace dgt.power.plugin.Deployment.Reconciliation;

/// <summary>Records removed during the pre-upload phase so later package cleanup does not repeat them.</summary>
internal sealed record ReleasedTypeDependencies(IReadOnlySet<Guid> StepIds, IReadOnlySet<Guid> UnlinkedApiIds)
{
    internal static ReleasedTypeDependencies None { get; } = new(new HashSet<Guid>(), new HashSet<Guid>());
}
