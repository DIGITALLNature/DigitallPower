// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Model;

namespace dgt.power.codegeneration.Templates.tsl.ViewModels;
// ReSharper disable UnusedAutoPropertyAccessor.Global

public class CustomApiViewModel
{
    public required string Name { get; set; }
    public required IReadOnlyList<WfParameter> InParameters { get; init; }

    public required IReadOnlyList<WfParameter> OutParameters { get; init; }
}
