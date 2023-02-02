// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace dgt.power.analyzer.Reports;

public record SolutionRedundantComponentLine
{
    public string? OriginSolution { get; init; }
    public string? AlsoInSolution { get; init; }
    public Guid ObjectId { get; init; }
    public string? ObjectType { get; init; }
}
