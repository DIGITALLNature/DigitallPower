// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

namespace dgt.power.plugin.Models;

/// <summary>
/// A plugin type already registered on the target environment, as read from Dataverse.
/// </summary>
public sealed record RemotePluginType(Guid Id, string TypeName);
