// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;

namespace dgt.power.codegeneration.Templates.tsl.ViewModels;
// ReSharper disable UnusedAutoPropertyAccessor.Global

public class OptionMetadataViewModel
{
    public OptionMetadataViewModel(OptionMetadata optionMetadata)
    {
        ArgumentNullException.ThrowIfNull(optionMetadata);
        Value = optionMetadata.Value.GetValueOrDefault(-1);
        Label = optionMetadata.Label;
    }

    public Label Label { get; set; }

    public int Value { get; set; }
}
