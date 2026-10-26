// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using dgt.power.codegeneration.Model;
using Microsoft.Xrm.Sdk.Metadata;

namespace dgt.power.codegeneration.Templates.tsl.ViewModels;
// ReSharper disable UnusedAutoPropertyAccessor.Global

public record FormViewModel
{
    public required IReadOnlyList<AttributeMetadata> Attributes { get; init; }
    public IEnumerable<EnumAttributeMetadata> EnumAttributes => Attributes.OfType<EnumAttributeMetadata>();
    public required IReadOnlyList<BpfControlDetail> BpfControls { get; init; }
    public required FormDetail FormDetail { get; init; }
    public required string Name { get; init; }
    public int LanguageCode { get; init; }
}
