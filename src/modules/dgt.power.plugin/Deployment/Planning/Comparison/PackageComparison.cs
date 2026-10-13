// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Deployment.Local;
using dgt.power.plugin.Models;

namespace dgt.power.plugin.Deployment.Planning.Comparison;

public sealed record PackageComparison(
    LocalPackage Local,
    RemotePackage? Remote)
{
    public bool RequiresCreate => Remote is null;

    public bool RequiresUpdate =>
        Remote is not null &&
        !string.Equals(Local.PackageHash, Remote.PackageHash, StringComparison.OrdinalIgnoreCase);
}
