// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

namespace dgt.power.plugin.Deployment.Batch;

/// <summary>A Dataverse identity declared by the target at <paramref name="FilePath"/>.</summary>
internal sealed record DeclaredIdentity(string Identity, string FilePath);
