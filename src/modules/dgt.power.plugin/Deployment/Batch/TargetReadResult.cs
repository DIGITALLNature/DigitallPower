// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.plugin.Deployment.Batch;

/// <param name="Targets">Readable plugin artifacts.</param>
/// <param name="Skipped">Readable assemblies without plugin types (for example dependencies).</param>
/// <param name="Failures">Unreadable or invalid artifacts.</param>
internal sealed record TargetReadResult(
    IReadOnlyList<LocalDeploymentTarget> Targets,
    IReadOnlyList<string> Skipped,
    IReadOnlyList<TargetReadFailure> Failures);
