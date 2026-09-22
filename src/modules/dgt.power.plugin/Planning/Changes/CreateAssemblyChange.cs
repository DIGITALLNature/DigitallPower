// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Local;

namespace dgt.power.plugin.Planning.Changes;

public sealed record CreateAssemblyChange(LocalAssembly Local) : AssemblyChange(Local, null);
