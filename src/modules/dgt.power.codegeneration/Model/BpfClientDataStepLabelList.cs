// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text.Json.Serialization;

#pragma warning disable CA1002 // JSON-deserialized list properties require List<T> for population

namespace dgt.power.codegeneration.Model;

public sealed class BpfClientDataStepLabelList
{
    [JsonPropertyName("list")]
    public required List<BpfStepLabel> ListLabels { get; init; }
}