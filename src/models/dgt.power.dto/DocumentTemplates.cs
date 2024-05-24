// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text.Json.Serialization;

namespace dgt.power.dto;

public sealed class DocumentTemplates
{
    [JsonPropertyName("ignore_missing")] public bool? IgnoreMissing { get; init; } = true;

    [JsonPropertyName("documenttemplates")]
    [JsonRequired]
    public List<DocumentTemplate> Templates { get; init; } = new();
}
