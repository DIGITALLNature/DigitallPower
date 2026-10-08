// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.plugin.Deployment.Batch;

/// <param name="Targets">Per-target results in execution order; empty when preflight rejected the batch.</param>
/// <param name="PreflightErrors">Reasons the batch was rejected before any Dataverse access.</param>
internal sealed record BatchResult(IReadOnlyList<TargetResult> Targets, IReadOnlyList<string> PreflightErrors)
{
    internal bool Success => PreflightErrors.Count == 0 && Targets.All(target => target.Outcome != TargetOutcome.Failed);
}
