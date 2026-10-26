// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using System.Text.Json.Serialization;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace dgt.power.plugin.StepManagement;

/// <summary>
/// Root configuration for plugin step state management.
/// </summary>
public class PluginStepStateConfigRoot
{
    /// <summary>
    /// List of plugin step state configurations.
    /// </summary>
    [JsonPropertyName("steps")]
    public IReadOnlyList<PluginStepStateConfig> Steps { get; init; } = [];
}
