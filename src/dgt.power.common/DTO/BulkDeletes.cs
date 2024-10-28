// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text.Json.Serialization;

namespace dgt.power.dto;

public sealed class BulkDeletes
{
    [JsonPropertyName("bulk_deletes")]
    [JsonRequired]
    public List<BulkDelete> Deletes { get; init; } = new();
}
