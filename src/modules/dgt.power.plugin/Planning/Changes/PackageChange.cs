// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Local;
using dgt.power.plugin.Remote;

namespace dgt.power.plugin.Planning.Changes;

/// <summary>The decided <see cref="PackageAction"/> for a <see cref="LocalPackage"/>.</summary>
public sealed record PackageChange(LocalPackage Local, PackageAction Action, RemotePackage? Existing);
