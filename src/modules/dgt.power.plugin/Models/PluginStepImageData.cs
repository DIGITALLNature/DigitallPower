// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

namespace dgt.power.plugin.Models;

/// <summary>
/// All Dataverse-facing fields needed to create a <c>sdkmessageprocessingstepimage</c>.
/// </summary>
public sealed record PluginStepImageData(
    Guid StepId,
    string Name,
    string EntityAlias,
    int ImageType,
    string MessagePropertyName,
    IReadOnlyList<string>? Attributes);
