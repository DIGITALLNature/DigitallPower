// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text.Json.Serialization;

namespace dgt.power.codegeneration.Model;
// ReSharper disable UnusedAutoPropertyAccessor.Global

public sealed class BpfClientDataStepList
{
    [JsonPropertyName("list")]
    public required List<BpfClientDataStep> StepList { get; init; }
}
