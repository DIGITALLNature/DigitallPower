// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

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
