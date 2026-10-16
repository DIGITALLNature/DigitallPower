// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.plugin.Deployment.Batch;

/// <summary>A glob pattern split into its non-wildcard root directory and the pattern relative to it.</summary>
internal sealed record GlobPatternRoot(string Root, string Pattern);
