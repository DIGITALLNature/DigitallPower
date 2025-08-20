// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text.Json.Serialization;

namespace dgt.power.dto;

public sealed class RoutingRuleConfig : Assignee
{
    [JsonPropertyName("Name")] public required string Name { get; init; }

    [JsonPropertyName("RoutingRuleId")] public required Guid RoutingRuleId { get; init; }

    [JsonPropertyName("Active")] public required bool Active { get; init; }

    [JsonPropertyName("RoutingRuleItems")] public required ICollection<RoutingRuleItem> RoutingRuleItems { get; init; } = new List<RoutingRuleItem>();
}
