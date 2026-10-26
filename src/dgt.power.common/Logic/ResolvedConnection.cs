// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using dgt.power.common.Connections;

namespace dgt.power.common.Logic;

/// <summary>
/// A saved connection definition together with its persisted name.
/// </summary>
internal sealed record ResolvedConnection(string Name, ConnectionDefinition Definition);
