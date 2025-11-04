// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using Microsoft.Xrm.Sdk.Metadata;

namespace dgt.power.codegeneration.Templates.tsl.ViewModels;

public class OptionMetadataViewModel
{
    public OptionMetadataViewModel(OptionMetadata optionMetadata)
    {
        Value = optionMetadata.Value.GetValueOrDefault(-1);
        Label = optionMetadata.Label.UserLocalizedLabel.Label;
    }

    public string Label { get; set; }

    public int Value { get; set; }
}
