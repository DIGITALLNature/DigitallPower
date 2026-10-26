// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

namespace dgt.power.plugin.Deployment.Batch;

/// <param name="Targets">Per-target results in execution order.</param>
internal sealed record BatchResult(IReadOnlyList<TargetResult> Targets)
{
    internal bool Success => Targets.All(target => target.Outcome != TargetOutcome.Failed);
}
