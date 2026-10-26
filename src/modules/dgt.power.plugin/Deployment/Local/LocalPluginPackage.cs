// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

namespace dgt.power.plugin.Deployment.Local;

/// <summary>
/// A parsed .nupkg together with the plugin assemblies bundled inside it.
/// </summary>
public sealed record LocalPluginPackage(LocalPackage Package, IReadOnlyList<LocalAssembly> Assemblies);
