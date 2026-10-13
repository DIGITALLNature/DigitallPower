// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

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
