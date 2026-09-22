// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Planning;
using dgt.power.plugin.Repositories;

namespace dgt.power.plugin.Execution;

public sealed class OutdatedAssemblyMigrator(
    IPluginAssemblyRepository assemblyRepository,
    IPluginTypeRepository typeRepository,
    ISdkMessageProcessingStepRepository stepRepository,
    ICustomApiRepository customApiRepository)
{
    public Task ApplyAsync(
        OutdatedAssemblyDeploymentPlan plan,
        IReadOnlyDictionary<string, Guid> replacementTypeIds,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(plan);
        ArgumentNullException.ThrowIfNull(replacementTypeIds);

        return ApplyCoreAsync(plan, replacementTypeIds, cancellationToken);
    }

    private async Task ApplyCoreAsync(
        OutdatedAssemblyDeploymentPlan plan,
        IReadOnlyDictionary<string, Guid> replacementTypeIds,
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
                    }

                    foreach (var stepId in type.MigrateStepIds)
                    {
                        await stepRepository.ReassignPluginTypeAsync(
                            stepId,
                            replacementTypeId,
                            cancellationToken);
                    }
                }

                foreach (var stepId in type.DeleteStepIds)
                {
                    await stepRepository.DeleteAsync(stepId, cancellationToken);
                }

                await typeRepository.DeleteAsync(type.Migration.OldTypeId, cancellationToken);
            }

            await assemblyRepository.DeleteAsync(assembly.Assembly.Id, cancellationToken);
        }
    }
}
