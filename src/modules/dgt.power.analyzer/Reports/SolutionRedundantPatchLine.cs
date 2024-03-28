// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.analyzer.Reports;

public record SolutionRedundantPatchLine
{
    public string? Solution { get; init; }
    public int TopLayers { get; init; }
    public int OtherLayers { get; set; }
}
