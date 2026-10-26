// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

namespace dgt.power.plugin.Deployment.Batch;

/// <param name="Targets">Readable plugin artifacts.</param>
/// <param name="Skipped">Readable assemblies without plugin types (for example dependencies).</param>
/// <param name="Failures">Unreadable or invalid artifacts.</param>
internal sealed record TargetReadResult(
    IReadOnlyList<LocalDeploymentTarget> Targets,
    IReadOnlyList<string> Skipped,
    IReadOnlyList<TargetReadFailure> Failures);
