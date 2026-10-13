// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common.Connections;

namespace dgt.power.common.Logic;

/// <summary>
/// A saved connection definition together with its persisted name.
/// </summary>
internal sealed record ResolvedConnection(string Name, ConnectionDefinition Definition);
