// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

namespace dgt.power.plugin.StepManagement;

/// <summary>
/// Identifies a plugin step either by <see cref="StepId"/> or by composite key criteria.
/// </summary>
internal sealed record PluginStepSelector(
    Guid? StepId = null,
    string? PluginType = null,
    string? Message = null,
    int? Stage = null,
    string? Entity = null,
    string? SecondaryEntity = null,
    int? ExecutionOrder = null);
