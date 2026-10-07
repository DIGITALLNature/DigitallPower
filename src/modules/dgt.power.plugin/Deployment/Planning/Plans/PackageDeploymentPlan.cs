// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Deployment.Local;
using dgt.power.plugin.Deployment.Planning.Comparison;

namespace dgt.power.plugin.Deployment.Planning.Plans;

public sealed record PackageDeploymentPlan(
    LocalPluginPackage Package,
    string DataverseName,
    PackageComparison Comparison,
    IReadOnlyList<AssemblyDeploymentPlan> Assemblies,
    SolutionMembershipPlan? SolutionMembership,
    SolutionLink? Solution) : PluginDeploymentPlan
{
    public IReadOnlyList<DataProviderDeployment> DataProviders { get; init; } = [];
}
