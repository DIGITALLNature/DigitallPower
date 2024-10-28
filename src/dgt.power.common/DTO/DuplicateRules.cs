// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text.Json.Serialization;

namespace dgt.power.dto;

public sealed class DuplicateRules : Assignee
{
    [JsonPropertyName("duplicate_rules")]
    [JsonRequired]
    public List<DuplicateRule> Rules { get; set; } = new();
}
