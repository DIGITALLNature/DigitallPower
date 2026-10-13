// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

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
