// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.plugin.Deployment.Batch;

/// <param name="Targets">Per-target results in execution order.</param>
internal sealed record BatchResult(IReadOnlyList<TargetResult> Targets)
{
    internal bool Success => Targets.All(target => target.Outcome != TargetOutcome.Failed);
}
