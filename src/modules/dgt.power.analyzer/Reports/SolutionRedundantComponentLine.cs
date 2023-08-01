// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace dgt.power.analyzer.Reports;

public record SolutionRedundantComponentLine
{
    public string? OriginSolution { get; init; }
    public string? AlsoInSolution { get; init; }
    public Guid ObjectId { get; init; }
    public string? ObjectType { get; init; }
}
