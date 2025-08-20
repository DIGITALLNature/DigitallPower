// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

#pragma warning disable CS8618
using System.Text.Json.Serialization;

namespace dgt.power.dto;

public sealed class DuplicateRuleCondition
{
    [JsonPropertyName("duplicateruleconditionid")]
    public required Guid DuplicateRuleConditionId { get; set; }

    [JsonPropertyName("baseattributename")]
    public required string BaseAttributeName { get; set; }

    [JsonPropertyName("matchingattributename")]
    public required string MatchingAttributeName { get; set; }

    [JsonPropertyName("ignoreblankvalues")]
    public required bool IgnoreBlankValues { get; set; }

    [JsonPropertyName("operatorcode")] public required int OperatorCode { get; set; }

    [JsonPropertyName("operatorparam")] public int? OperatorParam { get; set; }
}
