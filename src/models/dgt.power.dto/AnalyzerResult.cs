namespace dgt.power.dto;

public record AnalyzerSummary
{
    public string? Task { get; set; }

    public int Anomalies { get; set; }
}
