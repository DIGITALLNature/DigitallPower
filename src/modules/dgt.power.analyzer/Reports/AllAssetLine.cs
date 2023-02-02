// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace dgt.power.analyzer.Reports;

public record AllAssetLine
{
    public enum AllAssetsLevel
    {
        Good = 2,
        Approved = 1,
        Suspicious = 0,
        Bad = -1
    }

    public string? Solution { get; init; }
    public string? Entity { get; init; }
    public AllAssetsLevel Level { get; init; }
}
