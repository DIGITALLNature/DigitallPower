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
        IReadOnlyDictionary<string, Guid> replacementTypeIds,
        Action<PluginDeploymentProgress>? reportProgress = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(plan);
        ArgumentNullException.ThrowIfNull(replacementTypeIds);

        return ApplyCoreAsync(plan, replacementTypeIds, reportProgress, cancellationToken);
    }

    private async Task ApplyCoreAsync(
        OutdatedAssemblyDeployment plan,
        IReadOnlyDictionary<string, Guid> replacementTypeIds,
        Action<PluginDeploymentProgress>? reportProgress,
        CancellationToken cancellationToken)
    {
        foreach (var assembly in plan.Assemblies)
        {
            foreach (var type in assembly.Types)
            {
                if (type.Migration.HasReplacement &&
                    replacementTypeIds.TryGetValue(type.Migration.TypeName, out var replacementTypeId))
                {
                    foreach (var customApiId in type.CustomApiIds)
                    {
                        await customApiRepository.LinkPluginTypeAsync(
                            customApiId,
                            replacementTypeId,
                            cancellationToken);
                        Report(reportProgress, PluginDeploymentOperation.Migrated, "Custom API", customApiId.ToString());
                    }

                    foreach (var stepId in type.MigrateStepIds)
                    {
                        await stepRepository.ReassignPluginTypeAsync(
                            stepId,
                            replacementTypeId,
                            cancellationToken);
                        Report(reportProgress, PluginDeploymentOperation.Migrated, "step", stepId.ToString());
                    }
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
            Report(reportProgress, PluginDeploymentOperation.Deleted, "assembly", assembly.Assembly.Id.ToString());
        }
    }

    private static void Report(
        Action<PluginDeploymentProgress>? reportProgress,
        PluginDeploymentOperation operation,
        string resource,
        string name) =>
        reportProgress?.Invoke(new PluginDeploymentProgress(operation, resource, name));
}
