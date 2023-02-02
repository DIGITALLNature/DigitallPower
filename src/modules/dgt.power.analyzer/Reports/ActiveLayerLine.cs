// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace dgt.power.analyzer.Reports;

public record ActiveLayerLine
{
    public string? Component { get; init; }
    public int? Order { get; init; }
    public string? Name { get; init; }
    public string? Solution { get; init; }
}
