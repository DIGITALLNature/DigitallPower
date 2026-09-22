// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Local;
using dgt.power.plugin.Remote;

namespace dgt.power.plugin.Planning.Changes;

public sealed record UpdateAssemblyChange(LocalAssembly Local, RemoteAssembly Remote)
    : AssemblyChange(Local, Remote);
