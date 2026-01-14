// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text.Json.Serialization;

namespace dgt.power.codegeneration.Model
{
    public class BpfClientDataStep
    {
        [JsonPropertyName("__class")]
        public required string Class { get; init; }

        [JsonPropertyName("description")]
        public required string Description { get; init; }

        [JsonPropertyName("id")]
        public required string Id { get; init; }

        [JsonPropertyName("name")]
        public required string Name { get; init; }

        [JsonPropertyName("stepLabels")]
        public required BpfClientDataStepLabelList StepLabels { get; init; }

        [JsonPropertyName("steps")]
        public BpfClientDataStepList? Steps { get; init; }

        [JsonPropertyName("controlId")]
        public string? ControlId { get; init; }

        [JsonPropertyName("classId")]
        public string? ClassId { get; init; }

        [JsonPropertyName("dataFieldName")]
        public string? DataFieldName { get; init; }

        [JsonPropertyName("systemStepType")]
        public string? SystemStepType { get; init; }

        [JsonPropertyName("isSystemControl")]
        public bool? IsSystemControl { get; init; }

        [JsonPropertyName("parameters")]
        public string? Parameters { get; init; }

        [JsonPropertyName("controlDisplayName")]
        public string? ControlDisplayName { get; init; }

        [JsonPropertyName("isUnbound")]
        public bool? IsUnbound { get; init; }

        [JsonPropertyName("controlType")]
        public string? ControlType { get; init; }
    }
}
