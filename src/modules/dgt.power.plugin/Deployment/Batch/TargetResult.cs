// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.plugin.Deployment.Batch;

internal sealed record TargetResult(string FilePath, TargetOutcome Outcome, string? Message = null);
