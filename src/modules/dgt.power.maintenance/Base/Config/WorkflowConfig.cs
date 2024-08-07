// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text.Json.Serialization;

namespace dgt.power.maintenance.Base.Config;

public class WorkflowConfig
{
    [JsonPropertyName("solutionfilter")] public string[]? SolutionFilter { get; set; }
    [JsonPropertyName("publisherfilter")] public string[]? PublisherFilter { get; set; }
    [JsonPropertyName("flows")] public Dictionary<string, FlowConfig>? Flows { get; set; }
    [JsonPropertyName("actions")] public Dictionary<string, BaseWorkflowConfig>? Actions { get; set; }
    [JsonPropertyName("businessrules")] public Dictionary<string, Dictionary<string, BaseWorkflowConfig>>? BusinessRules { get; set; }
    [JsonPropertyName("owner")] public string? DefaultOwner { get; set; }
    [JsonPropertyName("impersonate")] public string? DefaultImpersonate { get; set; }

    public class FlowConfig : BaseWorkflowConfig
    {
        [JsonPropertyName("impersonate")] public string? Impersonate { get; set; }
    }

    public class BaseWorkflowConfig
    {
        [JsonPropertyName("disabled")] public bool? Disabled { get; set; }
        [JsonPropertyName("owner")] public string? Owner { get; set; }
    }
}
