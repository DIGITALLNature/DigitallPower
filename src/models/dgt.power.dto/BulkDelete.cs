// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

#pragma warning disable CS8618
using System.Text.Json.Serialization;

namespace dgt.power.dto;

public sealed class BulkDelete
{
    [JsonPropertyName("name")] public required string Name { get; init; }

    [JsonPropertyName("recurrence_pattern")]
    public required string RecurrencePattern { get; init; }

    [JsonPropertyName("recurrence_start_time")]
    public required string RecurrenceStartTime { get; init; }

    [JsonPropertyName("disable")] public bool Disable { get; set; } = false;

    [JsonPropertyName("FetchXml")] public string FetchXml { get; init; }
}
