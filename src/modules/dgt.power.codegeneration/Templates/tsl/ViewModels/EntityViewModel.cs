// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Model;
using Microsoft.Xrm.Sdk.Metadata;

namespace dgt.power.codegeneration.Templates.tsl.ViewModels;

public record EntityViewModel
{
    public required string SchemaName { get; init; }
    public required List<AttributeMetadata> Attributes { get; init; }
    public IEnumerable<PicklistAttributeMetadata> PicklistAttributes => Attributes.OfType<PicklistAttributeMetadata>();
}
