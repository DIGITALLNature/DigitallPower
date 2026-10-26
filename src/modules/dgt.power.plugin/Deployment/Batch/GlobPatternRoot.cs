// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

namespace dgt.power.plugin.Deployment.Batch;

/// <summary>A glob pattern split into its non-wildcard root directory and the pattern relative to it.</summary>
internal sealed record GlobPatternRoot(string Root, string Pattern);
