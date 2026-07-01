// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.common.DTO;

public record AnalyzerSummary
{
    public string? Task { get; set; }

    public int Anomalies { get; set; }
}
