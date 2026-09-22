// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.plugin.Planning;

public sealed record AssemblyDeploymentPlan(
    AssemblyPlan Assembly,
    PluginTypeDeploymentPlan? PluginTypes,
    OutdatedAssemblyDeploymentPlan OutdatedAssemblies,
    bool LinkManagedIdentity,
    SolutionLinkPlan? Solution) : PluginDeploymentPlan;
