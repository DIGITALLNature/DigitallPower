// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace dgt.power.analyzer.Reports;

public record ActiveLayerLine
{
    public string? Component { get; init; }
    public int? Order { get; init; }
    public string? Name { get; init; }
    public string? Solution { get; init; }
}
