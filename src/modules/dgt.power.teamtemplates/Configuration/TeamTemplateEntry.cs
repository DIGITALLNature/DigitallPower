// Copyright (c) DIGITALL Nature. All rights reserved

using System.Text.Json.Serialization;

namespace dgt.power.teamtemplates.Configuration;

public sealed record TeamTemplateEntry
{
    [JsonPropertyName("teamtemplateid")]
    public required Guid Id { get; init; }

    [JsonPropertyName("entity")]
    public required string Table { get; init; }

    [JsonPropertyName("teamtemplatename")]
    public required string Name { get; init; }

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("defaultaccessrightsmask")]
    public required int Rights { get; init; }
}
