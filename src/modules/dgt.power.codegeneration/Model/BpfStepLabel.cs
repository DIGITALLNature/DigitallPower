// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text.Json.Serialization;

namespace dgt.power.codegeneration.Model;

public sealed class BpfStepLabel
{
    [JsonPropertyName("labelId")]
    public required string LabelId { get; init; }

    [JsonPropertyName("languageCode")]
    public required int LanguageCode { get; init; }

    [JsonPropertyName("description")]
    public required string Description { get; init; }
}