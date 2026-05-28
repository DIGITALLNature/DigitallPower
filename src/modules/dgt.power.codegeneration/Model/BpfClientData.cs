// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text.Json.Serialization;

namespace dgt.power.codegeneration.Model;

public sealed class BpfClientData
{
    [JsonPropertyName("__class")]
    public required string Class { get; init; }

    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("description")]
    public required string Description { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("stepLabels")]
    public required BpfClientDataStepLabelList StepLabels { get; init; }

    [JsonPropertyName("steps")]
    public BpfClientDataStepList? Steps { get; init; }

    [JsonPropertyName("primaryEntityName")]
    public required string PrimaryEntityName { get; init; }

    [JsonPropertyName("nextStepIndex")]
    public required string NextStepIndex { get; init; }

    [JsonPropertyName("isCrmUIWorkflow")]
    public required bool IsCrmUIWorkflow { get; init; }

    [JsonPropertyName("category")]
    public required string Category { get; init; }

    [JsonPropertyName("businessProcessType")]
    public required string BusinessProcessType { get; init; }

    [JsonPropertyName("mode")]
    public required string Mode { get; init; }

    [JsonPropertyName("title")]
    public required string Title { get; init; }

    [JsonPropertyName("workflowEntityId")]
    public required string WorkflowEntityId { get; init; }

    [JsonPropertyName("formId")]
    public string? FormId { get; init; }

    [JsonPropertyName("argumentsArray")]
    public required List<string> ArgumentsArray { get; init; }

    [JsonPropertyName("variables")]
    public required List<string> Variables { get; init; }

    [JsonPropertyName("inputs")]
    public required List<string> Inputs { get; init; }
}