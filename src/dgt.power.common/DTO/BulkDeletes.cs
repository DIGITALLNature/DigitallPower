// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text.Json.Serialization;

namespace dgt.power.dto;

public sealed class BulkDeletes
{
    [JsonPropertyName("bulk_deletes")]
    [JsonRequired]
#pragma warning disable CA1002 // List is mutated by export logic
    public List<BulkDelete> Deletes { get; init; } = new();
#pragma warning restore CA1002
}
