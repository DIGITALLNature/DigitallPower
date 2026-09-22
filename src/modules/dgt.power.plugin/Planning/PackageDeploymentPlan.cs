// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Local;

namespace dgt.power.plugin.Planning;

public sealed record PackageDeploymentPlan(
    LocalPluginPackage Package,
    string DataverseName,
    PackagePlan PackageAction,
    IReadOnlyList<AssemblyDeploymentPlan> Assemblies,
    bool LinkManagedIdentity,
    SolutionLinkPlan? Solution) : PluginDeploymentPlan;
