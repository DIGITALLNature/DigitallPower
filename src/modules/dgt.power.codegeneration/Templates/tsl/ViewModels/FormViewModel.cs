// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Model;
using Microsoft.Xrm.Sdk.Metadata;

namespace dgt.power.codegeneration.Templates.tsl.ViewModels;

public record FormViewModel
{
    public required List<AttributeMetadata> Attributes { get; init; }
    public IEnumerable<EnumAttributeMetadata> EnumAttributes => Attributes.OfType<EnumAttributeMetadata>();
    public required List<BpfControlDetail> BpfControls { get; init; }
    public required FormDetail FormDetail { get; init; }
    public required string Name { get; init; }
}
