// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using dgt.power.plugin.Dataverse;
using dgt.power.plugin.Local;
using dgt.power.plugin.Planning;
using Spectre.Console;

namespace dgt.power.plugin.Execution;

/// <summary>
/// Migrates references away from outdated (superseded) plugin assemblies after an
/// <see cref="AssemblyAction.Upgrade"/>, and optionally purges them. Custom API links are always
/// migrated to the same-named replacement type on the new assembly (a Custom API should always
/// invoke the latest code); steps are only migrated - and the outdated assembly only deleted - when
/// <see cref="PluginPushOptions.PurgeOutdated"/> is set, since re-pointing steps changes their
/// execution target and is therefore opt-in.
/// </summary>
public sealed class OutdatedAssemblyMigrator(
    IPluginAssemblyRepository assemblyRepository,
    IPluginTypeRepository typeRepository,
    ISdkMessageProcessingStepRepository stepRepository,
    ICustomApiRepository customApiRepository,
    IAnsiConsole console)
{
    public async Task MigrateAsync(
        string assemblyName,
        Guid newAssemblyId,
        IReadOnlyList<LocalPluginType> replacementTypes,
        PluginPushOptions options,
        CancellationToken cancellationToken = default)
    {
        var outdatedAssemblies = await assemblyRepository.ListOutdatedAsync(assemblyName, newAssemblyId, cancellationToken);
        if (outdatedAssemblies.Count == 0)
        {
            return;
        }

        // Resolved lazily (and only once) since it requires the new types to already be reconciled,
        // and is only needed when there is at least one migration to actually apply.
        Dictionary<string, Guid>? newTypeIdsByName = null;

        foreach (var outdated in outdatedAssemblies)
        {
            var outdatedTypes = await typeRepository.ListByAssemblyAsync(outdated.Id, cancellationToken);
            var migrations = PluginPushPlanner.PlanOutdatedTypeMigration(outdatedTypes, replacementTypes);

            foreach (var migration in migrations)
            {
                if (!migration.HasReplacement)
                {
                    console.MarkupLine(CultureInfo.InvariantCulture,
                        "[yellow]Type '{0}' removed in the new version - its Custom API/step references cannot be migrated[/]",
                        migration.TypeName);
                    continue;
                }

                console.MarkupLine(CultureInfo.InvariantCulture,
                    "Migrate Custom API references for type [bold]{0}[/] to the new assembly", migration.TypeName);

                if (options.DryRun)
                {
                    continue;
                }

                newTypeIdsByName ??= (await typeRepository.ListByAssemblyAsync(newAssemblyId, cancellationToken))
                    .ToDictionary(t => t.TypeName, t => t.Id);

                if (!newTypeIdsByName.TryGetValue(migration.TypeName, out var newTypeId))
                {
                    continue; // Should not happen - the type was just reconciled onto the new assembly.
                }

                var linkedApis = await customApiRepository.ListLinkedToPluginTypeAsync(migration.OldTypeId, cancellationToken);
                foreach (var apiId in linkedApis)
                {
                    await customApiRepository.LinkPluginTypeAsync(apiId, newTypeId, cancellationToken);
                }

                if (options.PurgeOutdated)
                {
                    var steps = await stepRepository.ListByPluginTypeAsync(migration.OldTypeId, cancellationToken);
                    foreach (var step in steps)
                    {
                        await stepRepository.ReassignPluginTypeAsync(step.Id, newTypeId, cancellationToken);
                    }
                }
            }

            if (!options.PurgeOutdated)
            {
                continue;
            }

            console.MarkupLine(CultureInfo.InvariantCulture, "Purge outdated assembly [bold red]{0}[/]", outdated.Id);
            if (options.DryRun)
            {
                continue;
            }

            foreach (var outdatedType in outdatedTypes)
            {
                var remainingStepIds = await typeRepository.GetDependentStepIdsAsync(outdatedType.Id, cancellationToken);
                foreach (var stepId in remainingStepIds)
                {
                    await stepRepository.DeleteAsync(stepId, cancellationToken);
                }

                await typeRepository.DeleteAsync(outdatedType.Id, cancellationToken);
            }

            await assemblyRepository.DeleteAsync(outdated.Id, cancellationToken);
        }
    }
}
