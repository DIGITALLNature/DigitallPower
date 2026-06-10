// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using Microsoft.Xrm.Sdk.Metadata;

namespace dgt.power.codegeneration.Model;

public class EntityWithMetadataFormData
{
    public required EntityMetadata EntityMetadata { get; set; }
    public required Dictionary<string, FormDetail> ParsedFormDetail { get; init; }
}