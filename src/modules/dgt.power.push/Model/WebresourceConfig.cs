// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text.Json.Serialization;

namespace dgt.power.push.Model;

public class WebresourceConfig
{
    [JsonPropertyName("maps")]
    public Dictionary<string, string>? Maps { get; set; }
}
