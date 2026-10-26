// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

namespace dgt.power.plugin.Models;

/// <summary>
/// A plugin step image already registered on the target environment, as read from Dataverse.
/// </summary>
public sealed record RemotePluginStepImage(Guid Id, string Name, int ImageType, IReadOnlyList<string>? Attributes);
