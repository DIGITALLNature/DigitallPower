// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Model;

namespace dgt.power.codegeneration.Templates.tsl.ViewModels;

public record SectionDetaisViewModel
{

    public SectionDetaisViewModel(KeyValuePair<string, SectionDetail> keyValuePair)
    {
        Name = keyValuePair.Key;
        SectionDetail = keyValuePair.Value;
    }

    public SectionDetail SectionDetail { get; set; }

    public string Name { get; set; }
}
