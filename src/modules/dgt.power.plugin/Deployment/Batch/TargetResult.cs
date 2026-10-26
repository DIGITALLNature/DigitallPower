// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

namespace dgt.power.plugin.Deployment.Batch;

internal sealed record TargetResult(string FilePath, TargetOutcome Outcome, string? Message = null);
