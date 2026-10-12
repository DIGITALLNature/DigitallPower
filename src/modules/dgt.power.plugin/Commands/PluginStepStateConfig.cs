// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text.Json.Serialization;

namespace dgt.power.plugin.Commands;

/// <summary>
/// Configuration for a single plugin step state entry.
/// </summary>
public class PluginStepStateConfig
{
    /// <summary>
    /// SdkMessageProcessingStep id. Mutually exclusive with composite key fields.
    /// </summary>
    [JsonPropertyName("stepId")]
    public Guid? StepId { get; init; }

    /// <summary>
    /// Fully qualified plugin type name (e.g., MyNamespace.MyPluginClass).
    /// </summary>
    [JsonPropertyName("pluginType")]
    public string? PluginType { get; init; }

    /// <summary>
    /// SDK message name (e.g., Create, Update, Delete).
    /// </summary>
    [JsonPropertyName("message")]
    public string? Message { get; init; }

    /// <summary>
    /// Execution stage: PreValidation (10), PreOperation (20), MainOperation (30), or PostOperation (40).
    /// </summary>
    [JsonPropertyName("stage")]
    public int? Stage { get; init; }

    /// <summary>
    /// Primary entity logical name (e.g., account, contact).
    /// </summary>
    [JsonPropertyName("entity")]
    public string? Entity { get; init; }

    /// <summary>
    /// Secondary entity logical name (for messages like Associate).
    /// </summary>
    [JsonPropertyName("secondaryEntity")]
    public string? SecondaryEntity { get; init; }

    /// <summary>
    /// Execution order / rank.
    /// </summary>
    [JsonPropertyName("executionOrder")]
    public int? ExecutionOrder { get; init; }

    /// <summary>
    /// Whether the plugin step should be enabled (true) or disabled (false).
    /// </summary>
    [JsonPropertyName("enabled")]
    public bool Enabled { get; init; }

    /// <summary>
    /// Optional reason for the state change. If --persist-reason is specified, this will be stored in the step's description field.
    /// </summary>
    [JsonPropertyName("reason")]
    public string? Reason { get; init; }
}

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
