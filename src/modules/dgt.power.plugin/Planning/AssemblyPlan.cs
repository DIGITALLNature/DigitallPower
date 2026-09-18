// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Local;

namespace dgt.power.plugin.Planning;

/// <summary>
/// The decided <see cref="AssemblyAction"/> for a <see cref="LocalAssembly"/>, together with the
/// matching remote assembly (if any) the action was derived from.
/// </summary>
public sealed record AssemblyPlan(LocalAssembly Local, AssemblyAction Action, RemoteAssembly? Existing);
