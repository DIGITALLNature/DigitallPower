// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using Microsoft.Xrm.Sdk.Metadata;

namespace dgt.power.codegeneration.Model;

public class EntityWithMetadataFormData
{
    public required EntityMetadata EntityMetadata { get; set; }
    public required Dictionary<string, FormDetail> ParsedFormDetail { get; init; }
}