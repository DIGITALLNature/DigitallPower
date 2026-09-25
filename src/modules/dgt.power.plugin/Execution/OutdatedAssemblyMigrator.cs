// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Planning.Deployment;
using dgt.power.plugin.Repositories;

namespace dgt.power.plugin.Execution;

public sealed class OutdatedAssemblyMigrator(
    IPluginAssemblyRepository assemblyRepository,
    IPluginTypeRepository typeRepository,
    ISdkMessageProcessingStepRepository stepRepository,
    ICustomApiRepository customApiRepository)
{
    public Task ApplyAsync(
        OutdatedAssemblyDeployment plan,
        Action<PluginDeploymentProgress>? reportProgress = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(plan);

        return ApplyCoreAsync(plan, reportProgress, cancellationToken);
    }

    private async Task ApplyCoreAsync(
        OutdatedAssemblyDeployment plan,
        Action<PluginDeploymentProgress>? reportProgress,
        CancellationToken cancellationToken)
    {
        foreach (var assembly in plan.Assemblies)
        {
            foreach (var type in assembly.Types)
            {
                foreach (var customApiId in type.UnlinkCustomApiIds)
                {
                    await customApiRepository.UnlinkPluginTypeAsync(customApiId, cancellationToken);
                    Report(reportProgress, PluginDeploymentOperation.Unlinked, "Custom API", customApiId.ToString());
                }

                foreach (var stepId in type.DeleteStepIds)
                {
                    await stepRepository.DeleteAsync(stepId, cancellationToken);
                    Report(reportProgress, PluginDeploymentOperation.Deleted, "step", stepId.ToString());
                }

                await typeRepository.DeleteAsync(type.Migration.OldTypeId, cancellationToken);
                Report(reportProgress, PluginDeploymentOperation.Deleted, "plugin type", type.Migration.TypeName);
            }

            await assemblyRepository.DeleteAsync(assembly.Assembly.Id, cancellationToken);
            Report(
                reportProgress,
                PluginDeploymentOperation.Deleted,
                "assembly",
                assembly.Assembly.Identity);
        }
    }

    private static void Report(
        Action<PluginDeploymentProgress>? reportProgress,
        PluginDeploymentOperation operation,
        string resource,
        string name) =>
        reportProgress?.Invoke(new PluginDeploymentProgress(operation, resource, name));
}
