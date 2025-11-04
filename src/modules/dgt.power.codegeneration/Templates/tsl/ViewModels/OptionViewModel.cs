// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.codegeneration.Templates.tsl.ViewModels;

public record OptionViewModel
{
    public OptionViewModel(KeyValuePair<string, List<Option>> keyValuePair)
    {
        Name = keyValuePair.Key;
        Options = keyValuePair.Value;
    }

    public List<Option> Options { get; set; }

    public string Name { get; set; }
}
