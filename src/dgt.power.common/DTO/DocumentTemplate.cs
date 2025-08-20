// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

#pragma warning disable CS8618
using System.Text.Json.Serialization;

namespace dgt.power.dto;

public sealed class DocumentTemplate
{
    public enum DocumentTemplateType
    {
        MicrosoftExcel = 1,
        MicrosoftWord = 2
    }

    [JsonPropertyName("documenttemplateid")]
    public Guid? DocumentTemplateId { get; init; }

    [JsonPropertyName("name")] public required string Name { get; init; }

    [JsonPropertyName("document_type")] public required DocumentTemplateType DocumentType { get; init; }

    [JsonPropertyName("document_status")] public required bool? DocumentStatus { get; init; }

    [JsonPropertyName("force_update")] public bool? ForceUpdate { get; init; } = false;

    [JsonPropertyName("file")] public string File { get; init; }

    [JsonPropertyName("language_code")] public int? LanguageCode { get; init; }

    [JsonPropertyName("description")] public string? Description { get; init; }

    [JsonPropertyName("associated_entity")]
    public required string AssociatedEntityTypeCode { get; init; }

    //[JsonPropertyName("client_data")]
    //public string ClientData { get; set; }
}
