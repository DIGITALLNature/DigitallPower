// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

namespace dgt.power.codegeneration.Templates.tsl.ViewModels;
// ReSharper disable UnusedAutoPropertyAccessor.Global

public record OptionViewModel
{
    public OptionViewModel(KeyValuePair<string, List<Option>> keyValuePair)
    {
        Name = keyValuePair.Key;
        Options = keyValuePair.Value;
    }

    public IReadOnlyList<Option> Options { get; init; }

    public string Name { get; init; }
}
