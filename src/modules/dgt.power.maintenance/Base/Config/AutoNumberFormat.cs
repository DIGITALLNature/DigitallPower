// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text.Json.Serialization;

#pragma warning disable CS8618

namespace dgt.power.maintenance.Base.Config;

public class AutoNumberFormat
{
    [JsonPropertyName("entity")] public string Entity { get; set; }

    [JsonPropertyName("field")] public string Field { get; set; }

    [JsonPropertyName("format")] public string Format { get; set; }
}
