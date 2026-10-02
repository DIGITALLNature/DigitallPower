// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Remote;

namespace dgt.power.plugin.Planning.Comparison;

public sealed record PluginStepImageComparisonSet(
    IReadOnlyList<PluginStepImageComparison> Comparisons,
    IReadOnlyList<RemotePluginStepImage> Deletions);
