// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Deployment.Local;
using dgt.power.plugin.Deployment.Planning.Comparison;
using dgt.power.plugin.Models;

namespace dgt.power.plugin.Deployment.Planning.Plans;

public sealed record PluginStepDeployment(
    PluginStepComparison Comparison,
    ResolvedSdkMessage? Message,
    PluginStepImageComparisonSet Images,
    IReadOnlyList<LocalPluginStepImage> UnchangedImages,
    SolutionLink? Solution,
    MigratedPluginStep? MigrationSource = null);
