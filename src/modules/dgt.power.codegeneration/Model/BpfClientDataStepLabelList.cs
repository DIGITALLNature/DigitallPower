// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text.Json.Serialization;

namespace dgt.power.codegeneration.Model
{
    public sealed class BpfClientDataStepLabelList
    {
        [JsonPropertyName("list")]
        public required List<BpfStepLabel> ListLabels { get; init; }
    }
}
