// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Planning.Comparison;

namespace dgt.power.plugin.Planning.Deployment;

public sealed record AssemblyDeploymentPlan(
    AssemblyComparison Comparison,
    PluginTypeDeployment? PluginTypes,
    OutdatedAssemblyDeployment OutdatedAssemblies,
    bool LinkManagedIdentity,
    SolutionLink? Solution) : PluginDeploymentPlan;
