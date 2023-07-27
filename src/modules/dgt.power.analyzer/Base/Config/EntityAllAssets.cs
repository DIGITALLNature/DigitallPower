// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text.Json.Serialization;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable CollectionNeverUpdated.Global

namespace dgt.power.analyzer.Base.Config;

public class EntityAllAssets
{
    [JsonPropertyName("solution")] public required string Solution { get; set; }

    [JsonPropertyName("strict")] public required bool Strict { get; set; }

    //regex
    [JsonPropertyName("whitelist")]
    [JsonRequired]
    public List<string> WhiteList { get; init; } = new();

    //regex
    [JsonPropertyName("blacklist")]
    [JsonRequired]
    public List<string> BlackList { get; init; } = new();
}
