// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text.Json;
using System.Text.Json.Serialization;

namespace dgt.power.common.Storage;

internal sealed class UpdateState
{
    public DateTimeOffset? LastCheckOn { get; set; }

    // Used by System.Text.Json to preserve unrecognized state fields.
    // ReSharper disable once UnusedMember.Global
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? AdditionalProperties { get; set; }
}
