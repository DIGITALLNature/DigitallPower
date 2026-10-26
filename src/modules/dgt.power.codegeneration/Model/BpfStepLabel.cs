// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

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