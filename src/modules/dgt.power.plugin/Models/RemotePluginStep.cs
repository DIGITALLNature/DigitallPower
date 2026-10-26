// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

namespace dgt.power.plugin.Models;

/// <summary>
/// A plugin step already registered on the target environment, as read from Dataverse.
/// </summary>
public sealed record RemotePluginStep(
    Guid Id,
    string Name,
    int Mode,
    string MessageName,
    int Stage,
    string PrimaryEntityName,
    string SecondaryEntityName,
    IReadOnlyList<string>? FilterAttributes,
    int? ExecutionOrder);
