// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using dgt.power.codegeneration.Model;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace dgt.power.codegeneration.Templates.tsl.ViewModels;

public record SectionDetailsViewModel
{
    public SortedSet<string> ControlNames { get; init; }
    public string SectionName { get; init; }


    public SectionDetailsViewModel(KeyValuePair<string, SectionDetail> keyValuePair)
    {
        SectionName = keyValuePair.Key;
        ControlNames = keyValuePair.Value.ControlNames;
    }
}
