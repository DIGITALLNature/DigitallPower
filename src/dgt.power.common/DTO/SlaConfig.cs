// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text.Json.Serialization;

namespace dgt.power.dto;

public sealed class SlaConfig : Assignee
{
    [JsonPropertyName("Name")] public required string Name { get; init; }

    [JsonPropertyName("SlaId")] public required Guid? SlaId { get; init; }

    [JsonPropertyName("BusinessHours")] public required Guid? BusinessHours { get; init; }

    [JsonPropertyName("Active")] public required bool? Active { get; init; }

}
