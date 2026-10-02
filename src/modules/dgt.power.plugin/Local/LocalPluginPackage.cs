// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.plugin.Local;

/// <summary>
/// A parsed .nupkg together with the plugin assemblies bundled inside it.
/// </summary>
public sealed record LocalPluginPackage(LocalPackage Package, IReadOnlyList<LocalAssembly> Assemblies);
