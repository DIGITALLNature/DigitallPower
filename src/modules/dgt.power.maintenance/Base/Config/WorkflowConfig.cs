// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text.Json.Serialization;

namespace dgt.power.maintenance.Base.Config;
// ReSharper disable UnusedAutoPropertyAccessor.Global

public class WorkflowConfig
{
    [JsonPropertyName("solutionfilter")] public IReadOnlyList<string>? SolutionFilter { get; set; }
    [JsonPropertyName("publisherfilter")] public IReadOnlyList<string>? PublisherFilter { get; set; }
    [JsonPropertyName("flows")] public Dictionary<string, WorkflowFlowConfig>? Flows { get; init; }
    [JsonPropertyName("actions")] public Dictionary<string, WorkflowBaseConfig>? Actions { get; init; }
    [JsonPropertyName("businessrules")] public Dictionary<string, Dictionary<string, WorkflowBaseConfig>>? BusinessRules { get; init; }
    [JsonPropertyName("owner")] public string? DefaultOwner { get; set; }
    [JsonPropertyName("impersonate")] public string? DefaultImpersonate { get; set; }
}
