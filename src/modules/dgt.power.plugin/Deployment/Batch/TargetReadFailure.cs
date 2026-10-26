// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

namespace dgt.power.plugin.Deployment.Batch;

/// <summary>An artifact that could not be read; it blocks the whole batch because it may hide a conflict.</summary>
internal sealed record TargetReadFailure(string FilePath, string Message);
