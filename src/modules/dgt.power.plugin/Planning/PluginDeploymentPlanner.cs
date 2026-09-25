// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.plugin.Execution;
using dgt.power.plugin.Local;
using dgt.power.plugin.Planning.Comparison;
using dgt.power.plugin.Planning.Deployment;
using dgt.power.plugin.Repositories;

namespace dgt.power.plugin.Planning;

public sealed class PluginDeploymentPlanner(PluginPlanningRepositories repositories)
{
    private readonly Dictionary<(string Solution, int ComponentType), IReadOnlySet<Guid>> _solutionComponentIds = [];

    public Task<AssemblyDeploymentPlan> BuildAssemblyAsync(
        LocalAssembly assembly,
        PluginPushOptions options,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(assembly);
        ArgumentNullException.ThrowIfNull(options);

        return BuildStandaloneAssemblyCoreAsync(assembly, options, cancellationToken);
    }

    private async Task<AssemblyDeploymentPlan> BuildStandaloneAssemblyCoreAsync(
        LocalAssembly assembly,
        PluginPushOptions options,
        CancellationToken cancellationToken)
    {
        var remote = await repositories.Assemblies.FindForDeploymentAsync(
            assembly.Name,
            assembly.Version,
            cancellationToken);
        var assemblyComparison = new AssemblyComparison(assembly, remote);
        return await BuildAssemblyCoreAsync(
            assembly,
            assemblyComparison,
            options,
            packageOwned: false,
            cancellationToken);
    }

    public Task<PackageDeploymentPlan> BuildPackageAsync(
        LocalPluginPackage package,
        PluginPushOptions options,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(package);
        ArgumentNullException.ThrowIfNull(options);
        ArgumentException.ThrowIfNullOrWhiteSpace(options.PublisherPrefix);

        return BuildPackageCoreAsync(package, options, cancellationToken);
    }

    private async Task<PackageDeploymentPlan> BuildPackageCoreAsync(
        LocalPluginPackage package,
        PluginPushOptions options,
        CancellationToken cancellationToken)
    {
        var packageName = $"{options.PublisherPrefix}_{package.Package.Name}";
        var remote = await repositories.Packages.FindByNameAsync(packageName, cancellationToken);
        var packageComparison = new PackageComparison(package.Package, remote);
        var assemblies = new List<AssemblyDeploymentPlan>();

        foreach (var assembly in package.Assemblies)
        {
            var remoteAssembly = await repositories.Assemblies.FindForDeploymentAsync(
                assembly.Name,
                assembly.Version,
                cancellationToken);
            var assemblyComparison = new AssemblyComparison(assembly, remoteAssembly);
            var deployment = await BuildAssemblyCoreAsync(
                assembly,
                assemblyComparison,
                options,
                packageOwned: true,
                cancellationToken);
            assemblies.Add(deployment);
        }

        var linkManagedIdentity = package.Assemblies.Any(
            assembly => !string.IsNullOrWhiteSpace(assembly.ManagedIdentityClientId));
        SolutionLink? solution = null;
        if (!string.IsNullOrWhiteSpace(options.Solution))
        {
            var componentType = await repositories.Solutions.GetComponentTypeAsync(
                PluginPackage.EntityLogicalName,
                cancellationToken);
            solution = componentType is { } packageComponentType
                ? await PlanSolutionLinkAsync(
                    packageComponentType,
                    packageComparison.Remote?.Id,
                    packageName,
                    options.Solution,
                    cancellationToken)
                : throw new InvalidOperationException(
                    $"The target environment does not define a solution component type for '{PluginPackage.EntityLogicalName}', " +
                    $"so package '{packageName}' cannot be added to solution '{options.Solution}'.");
        }

        return new PackageDeploymentPlan(
            package,
            packageName,
            packageComparison,
            assemblies,
            linkManagedIdentity,
            string.IsNullOrWhiteSpace(options.Solution) ? null : new SolutionMembershipPlan(options.Solution),
            solution);
    }

    private async Task<AssemblyDeploymentPlan> BuildAssemblyCoreAsync(
        LocalAssembly assembly,
        AssemblyComparison assemblyComparison,
        PluginPushOptions options,
        bool packageOwned,
        CancellationToken cancellationToken)
    {
        var skipStandaloneDeployment =
            assemblyComparison.IsPackageOwned && !packageOwned;

        PluginTypeDeployment? pluginTypes = null;
        var outdated = new OutdatedAssemblyDeployment([]);
        if (!skipStandaloneDeployment)
        {
            var remoteAssemblyId = assemblyComparison.RequiresUpgrade
                ? null
                : assemblyComparison.Remote?.Id;
            pluginTypes = await BuildPluginTypesAsync(
                remoteAssemblyId,
                assembly.PluginTypes,
                options.Solution,
                cancellationToken);

            if (assemblyComparison.RequiresUpgrade)
            {
                outdated = await BuildOutdatedAssembliesAsync(
                    assembly.Name,
                    assembly.PluginTypes,
                    Guid.Empty,
                    cancellationToken);
                pluginTypes = AddMigrationState(pluginTypes, outdated);
            }
        }

        var linkManagedIdentity =
            !skipStandaloneDeployment &&
            !string.IsNullOrWhiteSpace(assembly.ManagedIdentityClientId);
        var solution = packageOwned
            ? null
            : await PlanSolutionLinkAsync(
                IPluginAssemblyRepository.ComponentType,
                assemblyComparison.Remote?.Id,
                assembly.Name,
                options.Solution,
                cancellationToken);

        return new AssemblyDeploymentPlan(
            assemblyComparison,
            pluginTypes,
            outdated,
            linkManagedIdentity,
            string.IsNullOrWhiteSpace(options.Solution) ? null : new SolutionMembershipPlan(options.Solution),
            solution);
    }

    public Task<PluginTypeDeployment> BuildPluginTypesAsync(
        Guid? assemblyId,
        IReadOnlyList<LocalPluginType> localTypes,
        string? solutionUniqueName = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(localTypes);

        return BuildPluginTypesCoreAsync(assemblyId, localTypes, solutionUniqueName, cancellationToken);
    }

    private async Task<PluginTypeDeployment> BuildPluginTypesCoreAsync(
        Guid? assemblyId,
        IReadOnlyList<LocalPluginType> localTypes,
        string? solutionUniqueName,
        CancellationToken cancellationToken)
    {
        var remoteTypes = assemblyId is { } existingAssemblyId
            ? await repositories.Types.ListByAssemblyAsync(existingAssemblyId, cancellationToken)
            : [];
        var ignoredTypeNames = localTypes
            .Where(type => !type.HasRegistrationAttribute)
            .Select(type => type.TypeName)
            .ToHashSet(StringComparer.Ordinal);
        var registeredTypes = localTypes.Where(type => type.HasRegistrationAttribute).ToList();
        var managedRemoteTypes = remoteTypes.Where(type => !ignoredTypeNames.Contains(type.TypeName)).ToList();
        var comparisonSet = PluginRegistrationComparer.ComparePluginTypes(registeredTypes, managedRemoteTypes);
        var typeItems = new List<PluginTypeDeploymentItem>();

        foreach (var typeComparison in comparisonSet.Comparisons)
        {
            var steps = new List<PluginStepDeployment>();
            var deleteSteps = new List<PluginStepDeletion>();

            if (string.IsNullOrEmpty(typeComparison.Local.CustomApi))
            {
                var remoteSteps = typeComparison.Remote is { } existingType
                    ? await repositories.Steps.ListByPluginTypeAsync(existingType.Id, cancellationToken)
                    : [];
                var stepComparisonSet = PluginRegistrationComparer.ComparePluginSteps(typeComparison.Local.Steps, remoteSteps);
                foreach (var stepComparison in stepComparisonSet.Comparisons)
                {
                    ResolvedSdkMessage? message = null;
                    if (stepComparison.RequiresCreate || stepComparison.RequiresUpdate)
                    {
                        message = await repositories.Messages.ResolveAsync(
                            stepComparison.Local.MessageName,
                            stepComparison.Local.PrimaryEntityName,
                            stepComparison.Local.SecondaryEntityName,
                            cancellationToken);
                        if (message is null)
                        {
                            throw new UnresolvedPluginStepMessageException(
                                stepComparison.Local.Name,
                                stepComparison.Local.MessageName,
                                stepComparison.Local.PrimaryEntityName);
                        }
                    }

                    var remoteImages = stepComparison.Remote is { } existingStep
                        ? await repositories.Images.ListByStepAsync(existingStep.Id, cancellationToken)
                        : [];
                    var images = PluginRegistrationComparer.ComparePluginStepImages(stepComparison.Local.Images, remoteImages);
                    var unchangedImages = stepComparison.Local.Images
                        .Where(image => remoteImages.Any(remote =>
                            remote.Name == image.Name &&
                            remote.ImageType == image.ImageType) &&
                            images.Comparisons.All(comparison => comparison.Local != image))
                        .ToList();
                    var solution = await PlanSolutionLinkAsync(
                        ISdkMessageProcessingStepRepository.ComponentType,
                        stepComparison.Remote?.Id,
                        stepComparison.Local.Name,
                        solutionUniqueName,
                        cancellationToken);
                    steps.Add(new PluginStepDeployment(stepComparison, message, images, unchangedImages, solution));
                }

                foreach (var step in stepComparisonSet.Deletions)
                {
                    deleteSteps.Add(new PluginStepDeletion(step));
                }
            }

            var customApi = await BuildCustomApiAsync(typeComparison, cancellationToken);
            typeItems.Add(new PluginTypeDeploymentItem(typeComparison, steps, deleteSteps, customApi, [], []));
        }

        var deleteTypes = new List<PluginTypeDeletion>();
        foreach (var type in comparisonSet.Deletions)
        {
            var dependentSteps = await repositories.Types.GetDependentStepIdsAsync(type.Id, cancellationToken);
            deleteTypes.Add(new PluginTypeDeletion(type, dependentSteps));
        }

        return new PluginTypeDeployment(typeItems, deleteTypes);
    }

    private async Task<PluginCustomApiDeployment> BuildCustomApiAsync(
        PluginTypeComparison typeComparison,
        CancellationToken cancellationToken)
    {
        Guid? desiredId = null;
        if (!string.IsNullOrEmpty(typeComparison.Local.CustomApi))
        {
            desiredId = await repositories.CustomApis.FindIdByUniqueNameAsync(
                typeComparison.Local.CustomApi,
                cancellationToken);
            if (desiredId is null)
            {
                throw new MissingCustomApiException(typeComparison.Local.CustomApi);
            }
        }

        var linked = typeComparison.Remote is { } existing
            ? await repositories.CustomApis.ListLinkedToPluginTypeAsync(existing.Id, cancellationToken)
            : [];
        var unlink = linked.Where(id => id != desiredId).ToList();

        var link = desiredId is { } customApiId && !linked.Contains(customApiId);
        var unchanged = desiredId is not null && !link;

        return new PluginCustomApiDeployment(
            typeComparison.Local.CustomApi,
            desiredId,
            unlink,
            link,
            unchanged);
    }

    private async Task<SolutionLink?> PlanSolutionLinkAsync(
        int componentType,
        Guid? existingComponentId,
        string componentName,
        string? solutionUniqueName,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(solutionUniqueName))
        {
            return null;
        }

        if (existingComponentId is null)
        {
            return new SolutionLink(componentType, componentName, solutionUniqueName);
        }

        var key = (solutionUniqueName, componentType);
        if (!_solutionComponentIds.TryGetValue(key, out var componentIds))
        {
            componentIds = await repositories.Solutions.ListComponentIdsAsync(
                solutionUniqueName,
                componentType,
                cancellationToken);
            _solutionComponentIds.Add(key, componentIds);
        }

        return componentIds.Contains(existingComponentId.Value)
            ? null
            : new SolutionLink(componentType, componentName, solutionUniqueName);
    }

    public Task<OutdatedAssemblyDeployment> BuildOutdatedAssembliesAsync(
        string assemblyName,
        IReadOnlyList<LocalPluginType> replacementTypes,
        Guid excludeAssemblyId,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(replacementTypes);

        return BuildOutdatedAssembliesCoreAsync(
            assemblyName,
            replacementTypes,
            excludeAssemblyId,
            cancellationToken);
    }

    private async Task<OutdatedAssemblyDeployment> BuildOutdatedAssembliesCoreAsync(
        string assemblyName,
        IReadOnlyList<LocalPluginType> replacementTypes,
        Guid excludeAssemblyId,
        CancellationToken cancellationToken)
    {
        var outdatedAssemblies = await repositories.Assemblies.ListOutdatedAsync(
            assemblyName,
            excludeAssemblyId,
            cancellationToken);
        if (outdatedAssemblies.Count == 0)
        {
            return new OutdatedAssemblyDeployment([]);
        }

        var registeredReplacements = replacementTypes
            .Where(type => type.HasRegistrationAttribute)
            .ToList();
        var undeclaredTypeNames = replacementTypes
            .Where(type => !type.HasRegistrationAttribute)
            .Select(type => type.TypeName)
            .ToHashSet(StringComparer.Ordinal);
        var assemblyPlans = new List<OutdatedAssemblyDeploymentItem>();

        foreach (var outdated in outdatedAssemblies)
        {
            var outdatedTypes = await repositories.Types.ListByAssemblyAsync(outdated.Id, cancellationToken);
            var migrations = PluginRegistrationComparer.CompareOutdatedTypes(
                outdatedTypes,
                registeredReplacements);
            var typeDeployments = new List<OutdatedTypeDeployment>();
            var retainedPluginTypeCount = 0;

            foreach (var migration in migrations)
            {
                var oldSteps = await repositories.Steps.ListByPluginTypeAsync(
                    migration.OldTypeId,
                    cancellationToken);
                var dependentStepIds = await repositories.Types.GetDependentStepIdsAsync(
                    migration.OldTypeId,
                    cancellationToken);
                var linkedApis = await repositories.CustomApis.ListLinkedToPluginTypeAsync(
                    migration.OldTypeId,
                    cancellationToken);
                if (!migration.HasReplacement &&
                    undeclaredTypeNames.Contains(migration.TypeName) &&
                    dependentStepIds.Count > 0)
                {
                    retainedPluginTypeCount++;
                    continue;
                }

                var replacement = registeredReplacements.Find(
                    type => type.TypeName == migration.TypeName);

                IReadOnlyList<MigratedPluginStep> migrateSteps = [];
                IReadOnlyList<Guid> deleteStepIds;
                if (replacement is null)
                {
                    deleteStepIds = dependentStepIds;
                }
                else
                {
                    var stepComparisonSet = PluginRegistrationComparer.ComparePluginSteps(replacement.Steps, oldSteps);
                    migrateSteps = await Task.WhenAll(stepComparisonSet.Deletions.Select(async step =>
                        new MigratedPluginStep(
                            step,
                            await repositories.Images.ListByStepAsync(step.Id, cancellationToken))));
                    deleteStepIds = dependentStepIds
                        .Where(id => !migrateSteps.Select(step => step.Step.Id).Contains(id))
                        .ToList();
                }

                typeDeployments.Add(new OutdatedTypeDeployment(
                    migration,
                    linkedApis,
                    migrateSteps,
                    deleteStepIds));
            }

            assemblyPlans.Add(new OutdatedAssemblyDeploymentItem(
                outdated,
                typeDeployments,
                retainedPluginTypeCount));
        }

        return new OutdatedAssemblyDeployment(assemblyPlans);
    }

    private static PluginTypeDeployment AddMigrationState(
        PluginTypeDeployment deployment,
        OutdatedAssemblyDeployment outdatedAssemblies)
    {
        var migrationsByType = outdatedAssemblies.Assemblies
            .SelectMany(assembly => assembly.Types)
            .Where(type => type.Migration.HasReplacement)
            .GroupBy(type => type.Migration.TypeName, StringComparer.Ordinal)
            .ToDictionary(group => group.Key, group => group.ToList(), StringComparer.Ordinal);

        var types = deployment.Types.Select(type =>
        {
            if (!migrationsByType.TryGetValue(type.Comparison.Local.TypeName, out var migrations))
            {
                return type;
            }

            return type with
            {
                MigratedCustomApiIds = migrations.SelectMany(migration => migration.CustomApiIds).ToList(),
                MigratedSteps = migrations.SelectMany(migration => migration.MigrateSteps).ToList()
            };
        }).ToList();
        return new PluginTypeDeployment(types, deployment.Deletions);
    }
}
