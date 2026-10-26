// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

namespace dgt.power.plugin.Models;

/// <summary>
/// Name and enabled state of a plugin step already registered on the target environment.
/// </summary>
public sealed record RemotePluginStepState(Guid Id, string Name, bool IsEnabled);
