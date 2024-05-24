// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text.Json.Serialization;

namespace dgt.power.dto;

public sealed class UserRole
{
    [JsonPropertyName("UserName")] public required string? UserName { get; init; }

    [JsonPropertyName("BusinessUnit")] public required string BusinessUnit { get; init; }

    [JsonPropertyName("BusinessUnitSeparator")]
    public char BusinessUnitSeparator { get; init; } = '/';

    [JsonPropertyName("SecurityRoles")] public ICollection<string> SecurityRoles { get; init; } = new List<string>();
}
