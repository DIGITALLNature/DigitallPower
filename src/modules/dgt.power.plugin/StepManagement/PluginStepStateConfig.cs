// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using System.Text.Json.Serialization;
using dgt.power.plugin.Models;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

namespace dgt.power.plugin.StepManagement;

/// <summary>
/// Configuration for a single plugin step state entry. A step is identified either by <see cref="StepId"/> or by
/// the full composite key (<see cref="PluginType"/>, <see cref="Message"/>, <see cref="Stage"/>, <see cref="Entity"/>).
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
    /// Required. Whether the plugin step should be enabled (true) or disabled (false); a missing value is rejected rather than treated as disable.
    /// </summary>
    [JsonPropertyName("enabled")]
    public bool? Enabled { get; init; }

    /// <summary>
    /// Optional reason for the state change. When supplied, it is stored in the step's description field if the state actually changes.
    /// </summary>
    [JsonPropertyName("reason")]
    public string? Reason { get; init; }

    /// <summary>
    /// Validates that the entry has an explicit state and exactly one valid identification mode.
    /// </summary>
    /// <returns>An error message, or <c>null</c> when the entry is valid.</returns>
    public string? Validate()
    {
        if (!Enabled.HasValue)
        {
            return "'enabled' is required.";
        }

        var hasCompositeOptions = HasCompositeKeyFields();

        if (StepId.HasValue)
        {
            if (StepId.Value == Guid.Empty)
            {
                return "'stepId' must not be an empty GUID.";
            }

            return hasCompositeOptions ? "'stepId' cannot be combined with composite key fields (pluginType, message, stage, entity, secondaryEntity, executionOrder)." : null;
        }

        if (string.IsNullOrWhiteSpace(PluginType) || string.IsNullOrWhiteSpace(Message) || string.IsNullOrWhiteSpace(Entity) || !Stage.HasValue)
        {
            return "Either 'stepId' or all of 'pluginType', 'message', 'stage' and 'entity' must be provided.";
        }

        return IsValidStage(Stage.Value) ? null : $"Invalid stage value: {Stage.Value}.";
    }

    private static bool IsValidStage(int value)
    {
        var stage = (PluginStepStage)value;
        return stage != PluginStepStage.None && Enum.IsDefined(stage);
    }

    private bool HasCompositeKeyFields() => HasCompositeTextFields() || Stage.HasValue || ExecutionOrder.HasValue;

    private bool HasCompositeTextFields() => PluginType != null || Message != null || Entity != null || SecondaryEntity != null;

    /// <summary>
    /// Short human-readable identification of the entry for messages.
    /// </summary>
    public string Describe()
    {
        if (StepId.HasValue)
        {
            return $"stepId {StepId.Value}";
        }

        var parts = new[] { PluginType, Message, Stage?.ToString(CultureInfo.InvariantCulture), Entity, SecondaryEntity, ExecutionOrder?.ToString(CultureInfo.InvariantCulture) }
            .Where(part => !string.IsNullOrEmpty(part));
        return string.Join(", ", parts);
    }
}
