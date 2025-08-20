// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text.Json.Serialization;

namespace dgt.power.dto;

public abstract class Assignee
{
    [JsonPropertyName("owner")] public string? Owner { get; init; }
}
