// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using dgt.power.codegeneration.Model;

namespace dgt.power.codegeneration.Templates.tsl.ViewModels;
// ReSharper disable UnusedAutoPropertyAccessor.Global

public class CustomApiViewModel
{
    public required string Name { get; set; }
    public required IReadOnlyList<WfParameter> InParameters { get; init; }

    public required IReadOnlyList<WfParameter> OutParameters { get; init; }
}
