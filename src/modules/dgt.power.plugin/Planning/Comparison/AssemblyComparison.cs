// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Local;
using dgt.power.plugin.Remote;

namespace dgt.power.plugin.Planning.Comparison;

public sealed record AssemblyComparison(
    LocalAssembly Local,
    RemoteAssembly? Remote)
{
    public bool IsPackageOwned => Remote?.PackageId is not null;

    public bool RequiresCreate => Remote is null;

    public bool RequiresUpgrade =>
        Remote is not null &&
        !IsPackageOwned &&
        (Local.Version.Major != Remote.Version.Major ||
         Local.Version.Minor != Remote.Version.Minor);

    public bool RequiresUpdate =>
        Remote is not null &&
        !IsPackageOwned &&
        !RequiresUpgrade &&
        !string.Equals(Local.ContentHash, Remote.ContentHash, StringComparison.OrdinalIgnoreCase);
}
