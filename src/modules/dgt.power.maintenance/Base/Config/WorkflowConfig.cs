// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text.Json.Serialization;

namespace dgt.power.maintenance.Base.Config;

public class WorkflowConfig
{
    [JsonPropertyName("solutionfilter")] public string[]? SolutionFilter { get; set; }
    [JsonPropertyName("publisherfilter")] public string[]? PublisherFilter { get; set; }
    [JsonPropertyName("flows")] public Dictionary<string, FlowConfig>? Flows { get; set; }

    public class FlowConfig
    {
        [JsonPropertyName("disabled")] public bool? Disabled { get; set; }
        [JsonPropertyName("owner")] public string? Owner { get; set; }
        [JsonPropertyName("impersonate")] public string? Impersonate { get; set; }
    }
}
