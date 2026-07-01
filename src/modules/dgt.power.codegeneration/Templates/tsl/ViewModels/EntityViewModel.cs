// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using Microsoft.Xrm.Sdk.Metadata;

namespace dgt.power.codegeneration.Templates.tsl.ViewModels;
// ReSharper disable UnusedAutoPropertyAccessor.Global

public record EntityViewModel
{
    public required string SchemaName { get; init; }
    public required string LogicalName { get; init; }
    public required IReadOnlyList<AttributeMetadata> Attributes { get; init; }
    public IEnumerable<EnumAttributeMetadata> EnumAttributes => Attributes.OfType<EnumAttributeMetadata>();
    public int? LanguageCode { get; set; }
}
