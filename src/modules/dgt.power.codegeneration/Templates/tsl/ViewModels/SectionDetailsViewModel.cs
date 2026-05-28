// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Model;
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace dgt.power.codegeneration.Templates.tsl.ViewModels;

public record SectionDetailsViewModel
{
    public SortedSet<string> ControlNames { get; set; }
    public string SectionName { get; set; }


    public SectionDetailsViewModel(KeyValuePair<string, SectionDetail> keyValuePair)
    {
        SectionName = keyValuePair.Key;
        ControlNames = keyValuePair.Value.ControlNames;
    }
}
