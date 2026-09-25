// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Local;
using dgt.power.plugin.Planning.Comparison;
using dgt.power.plugin.Repositories;

namespace dgt.power.plugin.Planning.Deployment;

public sealed record PluginStepDeployment(
    PluginStepComparison Comparison,
    ResolvedSdkMessage? Message,
    PluginStepImageComparisonSet Images,
    IReadOnlyList<LocalPluginStepImage> UnchangedImages,
    SolutionLink? Solution,
    MigratedPluginStep? MigrationSource = null);
