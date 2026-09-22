// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.plugin.Execution;
using dgt.power.plugin.Local;
using dgt.power.plugin.Planning.Changes;
using dgt.power.plugin.Planning.Deployment;
using dgt.power.plugin.Remote;
using dgt.power.plugin.Repositories;

namespace dgt.power.plugin.Planning;

public sealed class PluginDeploymentPlanner(PluginPlanningRepositories repositories)
{
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
        var remote = await repositories.Assemblies.FindByNameAsync(assembly.Name, cancellationToken);
        var assemblyChange = PluginStateComparer.CompareAssembly(assembly, remote);
        return await BuildAssemblyCoreAsync(
            assembly,
            assemblyChange,
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
        var packageChange = PluginStateComparer.ComparePackage(package.Package, remote);
        var assemblies = new List<AssemblyDeploymentPlan>();

        foreach (var assembly in package.Assemblies)
        {
            var remoteAssembly = await repositories.Assemblies.FindByNameAsync(assembly.Name, cancellationToken);
            var assemblyChange = PluginStateComparer.CompareAssembly(assembly, remoteAssembly);
            var deployment = await BuildAssemblyCoreAsync(
                assembly,
                assemblyChange,
                options,
                packageOwned: true,
                cancellationToken);
            assemblies.Add(deployment);
        }

        var linkManagedIdentity = package.Assemblies.Any(
            assembly => !string.IsNullOrWhiteSpace(assembly.ManagedIdentityClientId));
        SolutionLink? solution = null;
        if (packageChange.Action == PackageAction.Create && !string.IsNullOrWhiteSpace(options.Solution))
        {
            var componentType = await repositories.Solutions.GetComponentTypeAsync(
                PluginPackage.EntityLogicalName,
                cancellationToken);
            if (componentType is not null)
            {
                solution = new SolutionLink(componentType.Value, options.Solution);
            }
        }

        return new PackageDeploymentPlan(
            package,
            packageName,
            packageChange,
            assemblies,
            linkManagedIdentity,
            solution);
    }

    private async Task<AssemblyDeploymentPlan> BuildAssemblyCoreAsync(
        LocalAssembly assembly,
        AssemblyChange assemblyChange,
        PluginPushOptions options,
        bool packageOwned,
        CancellationToken cancellationToken)
    {
        var skipStandaloneDeployment =
            assemblyChange is PackageOwnedAssemblyChange && !packageOwned;

        PluginTypeDeployment? pluginTypes = null;
        var outdated = new OutdatedAssemblyDeployment([]);
        if (!skipStandaloneDeployment)
        {
            var remoteAssemblyId = assemblyChange is UpgradeAssemblyChange
                ? null
                : assemblyChange.Existing?.Id;
            pluginTypes = await BuildPluginTypesAsync(
                remoteAssemblyId,
                assembly.PluginTypes,
                cancellationToken);

            if (assemblyChange is UpgradeAssemblyChange)
            {
                outdated = await BuildOutdatedAssembliesAsync(
                    assembly.Name,
                    assembly.PluginTypes,
                    Guid.Empty,
                    cancellationToken);
            }
        }

        var linkManagedIdentity =
            !skipStandaloneDeployment &&
            !string.IsNullOrWhiteSpace(assembly.ManagedIdentityClientId);
        SolutionLink? solution = null;
        if (!packageOwned &&
            assemblyChange is CreateAssemblyChange or UpgradeAssemblyChange &&
            !string.IsNullOrWhiteSpace(options.Solution))
        {
            solution = new SolutionLink(
                IPluginAssemblyRepository.ComponentType,
                options.Solution);
        }

        return new AssemblyDeploymentPlan(
            assemblyChange,
            pluginTypes,
            outdated,
            linkManagedIdentity,
            solution);
    }

    public Task<PluginTypeDeployment> BuildPluginTypesAsync(
        Guid? assemblyId,
        IReadOnlyList<LocalPluginType> localTypes,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(localTypes);

        return BuildPluginTypesCoreAsync(assemblyId, localTypes, cancellationToken);
    }

    private async Task<PluginTypeDeployment> BuildPluginTypesCoreAsync(
        Guid? assemblyId,
        IReadOnlyList<LocalPluginType> localTypes,
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
        var changeSet = PluginStateComparer.ComparePluginTypes(registeredTypes, managedRemoteTypes);
        var typeItems = new List<PluginTypeDeploymentItem>();

        foreach (var typeChange in changeSet.Changes)
        {
            var steps = new List<PluginStepDeployment>();
            var deleteSteps = new List<PluginStepDeletion>();

            if (string.IsNullOrEmpty(typeChange.Local.CustomApi))
            {
                var remoteSteps = typeChange.Existing is { } existingType
                    ? await repositories.Steps.ListByPluginTypeAsync(existingType.Id, cancellationToken)
                    : [];
                var stepChanges = PluginStateComparer.ComparePluginSteps(typeChange.Local.Steps, remoteSteps);
                foreach (var stepChange in stepChanges.Changes)
                {
                    ResolvedSdkMessage? message = null;
                    if (stepChange.Action is PluginStepAction.Create or PluginStepAction.Update)
                    {
                        message = await repositories.Messages.ResolveAsync(
                            stepChange.Local.MessageName,
                            stepChange.Local.PrimaryEntityName,
                            stepChange.Local.SecondaryEntityName,
                            cancellationToken);
                        if (message is null)
                        {
                            throw new UnresolvedPluginStepMessageException(
                                stepChange.Local.Name,
                                stepChange.Local.MessageName,
                                stepChange.Local.PrimaryEntityName);
                        }
                    }

                    var remoteImages = stepChange.Existing is { } existingStep
                        ? await repositories.Images.ListByStepAsync(existingStep.Id, cancellationToken)
                        : [];
                    var images = PluginStateComparer.ComparePluginStepImages(stepChange.Local.Images, remoteImages);
                    var unchangedImages = stepChange.Local.Images
                        .Where(image => remoteImages.Any(remote =>
                            remote.Name == image.Name &&
                            remote.ImageType == image.ImageType) &&
                            images.Changes.All(change => change.Local != image))
                        .ToList();
                    steps.Add(new PluginStepDeployment(stepChange, message, images, unchangedImages));
                }

                foreach (var step in stepChanges.Deletions)
                {
                    deleteSteps.Add(new PluginStepDeletion(step));
                }
            }

            var customApi = await BuildCustomApiAsync(typeChange, cancellationToken);
            typeItems.Add(new PluginTypeDeploymentItem(typeChange, steps, deleteSteps, customApi));
        }

        var deleteTypes = new List<PluginTypeDeletion>();
        foreach (var type in changeSet.Deletions)
        {
            var dependentSteps = await repositories.Types.GetDependentStepIdsAsync(type.Id, cancellationToken);
            deleteTypes.Add(new PluginTypeDeletion(type, dependentSteps));
        }

        return new PluginTypeDeployment(typeItems, deleteTypes);
    }

    private async Task<PluginCustomApiDeployment> BuildCustomApiAsync(
        PluginTypeChange typeChange,
        CancellationToken cancellationToken)
    {
        Guid? desiredId = null;
        if (!string.IsNullOrEmpty(typeChange.Local.CustomApi))
        {
            desiredId = await repositories.CustomApis.FindIdByUniqueNameAsync(
                typeChange.Local.CustomApi,
                cancellationToken);
            if (desiredId is null)
            {
                throw new MissingCustomApiException(typeChange.Local.CustomApi);
            }
        }

        var linked = typeChange.Existing is { } existing
            ? await repositories.CustomApis.ListLinkedToPluginTypeAsync(existing.Id, cancellationToken)
            : [];
        var unlink = linked.Where(id => id != desiredId).ToList();

        var link = desiredId is { } customApiId && !linked.Contains(customApiId);
        var unchanged = desiredId is not null && !link;

        return new PluginCustomApiDeployment(
            typeChange.Local.CustomApi,
            desiredId,
            unlink,
            link,
            unchanged);
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
        var assemblyPlans = new List<OutdatedAssemblyDeploymentItem>();

        foreach (var outdated in outdatedAssemblies)
        {
            var outdatedTypes = await repositories.Types.ListByAssemblyAsync(outdated.Id, cancellationToken);
            var migrations = PluginStateComparer.CompareOutdatedTypes(
                outdatedTypes,
                registeredReplacements);
            var typeDeployments = new List<OutdatedTypeDeployment>();

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
                var replacement = registeredReplacements.Find(
                    type => type.TypeName == migration.TypeName);

                IReadOnlyList<Guid> migrateStepIds = [];
                IReadOnlyList<Guid> deleteStepIds;
                if (replacement is null)
                {
                    deleteStepIds = dependentStepIds;
                }
                else
                {
                    var stepChanges = PluginStateComparer.ComparePluginSteps(replacement.Steps, oldSteps);
                    migrateStepIds = stepChanges.Deletions.Select(step => step.Id).ToList();
                    deleteStepIds = dependentStepIds
                        .Where(id => !migrateStepIds.Contains(id))
                        .ToList();
                }

                typeDeployments.Add(new OutdatedTypeDeployment(
                    migration,
                    linkedApis,
                    migrateStepIds,
                    deleteStepIds));
            }

            assemblyPlans.Add(new OutdatedAssemblyDeploymentItem(outdated, typeDeployments));
        }

        return new OutdatedAssemblyDeployment(assemblyPlans);
    }
}
