// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL


namespace dgt.power.codegeneration.Templates.tsl.ViewModels;
// ReSharper disable UnusedAutoPropertyAccessor.Global

public record OptionSetViewModel
{
    public IReadOnlyList<KeyValuePair<string, List<Option>>> OptionSets { get; init; } = [];
}
