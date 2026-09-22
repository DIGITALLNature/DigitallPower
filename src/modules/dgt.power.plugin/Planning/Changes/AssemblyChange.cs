// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Local;
using dgt.power.plugin.Remote;

namespace dgt.power.plugin.Planning.Changes;

/// <summary>Describes how a local assembly relates to the current Dataverse state.</summary>
public abstract record AssemblyChange(LocalAssembly Local, RemoteAssembly? Existing);
