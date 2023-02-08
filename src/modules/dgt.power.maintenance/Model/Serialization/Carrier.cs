// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text.Json.Serialization;

namespace dgt.power.maintenance.Model.Serialization;

public class Carrier
{
    [JsonPropertyName("solutionid")] public Guid SolutionId { get; set; }
    [JsonPropertyName("carrierid")] public Guid CarrierId { get; set; }
    [JsonPropertyName("uniquename")] public string? UniqueName { get; set; }
    [JsonPropertyName("friendlyname")] public string? FriendlyName { get; set; }
    [JsonPropertyName("version")] public string? Version { get; set; }
    [JsonPropertyName("order")] public int Order { get; set; }
}
