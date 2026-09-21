// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.plugin.Local;

/// <summary>
/// A plugin package parsed from a local .nupkg file, independent of any Dataverse state.
/// </summary>
/// <param name="Name">Package id from the nuspec.</param>
/// <param name="Version">
/// Raw SemVer version string from the nuspec. Dataverse plugin packages cannot have their version
/// changed after creation, so - unlike plugin assemblies - package version is not used to decide
/// between Update/Upgrade; it is only used for display and for the initial Create.
/// </param>
/// <param name="Content">Base64-encoded package content.</param>
public sealed record LocalPackage(string Name, string Version, string Content);
