// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Local;
using dgt.power.plugin.Output;
using dgt.power.plugin.Planning;
using dgt.power.plugin.Planning.Deployment;

namespace dgt.power.plugin.Execution;

public sealed class PluginDeploymentPipeline(
    PluginDeploymentPlanner planner,
    PluginPlanRenderer renderer,
    PluginPushExecutor executor)
{
    public async Task<Guid> ProcessAssemblyAsync(
        LocalAssembly assembly,
        PluginPushOptions options,
        CancellationToken cancellationToken = default)
    {
        var plan = await planner.BuildAssemblyAsync(assembly, options, cancellationToken);
        renderer.Render(plan);
        return options.DryRun
            ? Guid.Empty
            : await executor.ExecuteAsync(plan, cancellationToken);
    }

    public async Task<Guid> ProcessPackageAsync(
        LocalPluginPackage package,
        PluginPushOptions options,
        CancellationToken cancellationToken = default)
    {
        var plan = await planner.BuildPackageAsync(package, options, cancellationToken);
        renderer.Render(plan);
        return options.DryRun
            ? Guid.Empty
            : await executor.ExecuteAsync(plan, cancellationToken);
    }
}
