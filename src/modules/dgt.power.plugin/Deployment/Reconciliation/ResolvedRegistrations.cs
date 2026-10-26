// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

namespace dgt.power.plugin.Deployment.Reconciliation;

internal sealed record ResolvedRegistrations(
    IReadOnlyDictionary<string, Guid> TypeIds,
    IReadOnlyDictionary<PluginStepReference, Guid> StepIds);
