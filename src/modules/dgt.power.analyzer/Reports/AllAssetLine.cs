// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

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
