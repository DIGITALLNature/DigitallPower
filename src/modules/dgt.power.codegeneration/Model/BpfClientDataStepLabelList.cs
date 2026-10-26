// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using System.Text.Json.Serialization;

#pragma warning disable CA1002 // JSON-deserialized list properties require List<T> for population

namespace dgt.power.codegeneration.Model;

public sealed class BpfClientDataStepLabelList
{
    [JsonPropertyName("list")]
    public required List<BpfStepLabel> ListLabels { get; init; }
}