// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text.Json.Serialization;

namespace dgt.power.dto;

public sealed class SavedQuery
{
    [JsonPropertyName("disabled_outlook_templates")]
    public ICollection<string> DisabledOutlookTemplates { get; init; } = new List<string>();

    [JsonPropertyName("outlook_templates")]
    public ICollection<OutlookTemplate> OutlookTemplates { get; init; } = new List<OutlookTemplate>();
}
