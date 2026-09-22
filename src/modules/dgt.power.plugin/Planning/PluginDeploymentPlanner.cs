// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.plugin.Execution;
using dgt.power.plugin.Local;
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
        var assemblyPlan = PluginPushPlanner.PlanAssembly(assembly, remote);
        return await BuildAssemblyCoreAsync(
            assembly,
            assemblyPlan,
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
        var packageAction = PluginPushPlanner.PlanPackage(package.Package, remote);
        var assemblies = new List<AssemblyDeploymentPlan>();

        foreach (var assembly in package.Assemblies)
        {
            var remoteAssembly = await repositories.Assemblies.FindByNameAsync(assembly.Name, cancellationToken);
            var assemblyPlan = PluginPushPlanner.PlanAssembly(assembly, remoteAssembly);
            var deployment = await BuildAssemblyCoreAsync(
                assembly,
                assemblyPlan,
                options,
                packageOwned: true,
                cancellationToken);
            assemblies.Add(deployment);
        }

        var linkManagedIdentity = package.Assemblies.Any(
            assembly => !string.IsNullOrWhiteSpace(assembly.ManagedIdentityClientId));
        SolutionLinkPlan? solution = null;
        if (packageAction.Action == PackageAction.Create && !string.IsNullOrWhiteSpace(options.Solution))
        {
            var componentType = await repositories.Solutions.GetComponentTypeAsync(
                PluginPackage.EntityLogicalName,
                cancellationToken);
            if (componentType is not null)
            {
                solution = new SolutionLinkPlan(componentType.Value, options.Solution);
            }
        }

        return new PackageDeploymentPlan(
            package,
            packageName,
            packageAction,
            assemblies,
            linkManagedIdentity,
            solution);
    }

    private async Task<AssemblyDeploymentPlan> BuildAssemblyCoreAsync(
        LocalAssembly assembly,
        AssemblyPlan assemblyPlan,
        PluginPushOptions options,
        bool packageOwned,
        CancellationToken cancellationToken)
    {
        var skipStandaloneReconciliation =
            assemblyPlan.Action == AssemblyAction.OwnedByPackage && !packageOwned;

        PluginTypeDeploymentPlan? pluginTypes = null;
        var outdated = new OutdatedAssemblyDeploymentPlan([]);
        if (!skipStandaloneReconciliation)
        {
            var remoteAssemblyId = assemblyPlan.Action == AssemblyAction.Upgrade
                ? null
                : assemblyPlan.Existing?.Id;
            pluginTypes = await BuildPluginTypesAsync(
                remoteAssemblyId,
                assembly.PluginTypes,
                cancellationToken);

            if (assemblyPlan.Action == AssemblyAction.Upgrade)
            {
                outdated = await BuildOutdatedAssembliesAsync(
                    assembly.Name,
                    assembly.PluginTypes,
                    Guid.Empty,
                    cancellationToken);
            }
        }

        var linkManagedIdentity =
            !skipStandaloneReconciliation &&
            !string.IsNullOrWhiteSpace(assembly.ManagedIdentityClientId);
        SolutionLinkPlan? solution = null;
        if (!packageOwned &&
            assemblyPlan.Action is AssemblyAction.Create or AssemblyAction.Upgrade &&
            !string.IsNullOrWhiteSpace(options.Solution))
        {
            solution = new SolutionLinkPlan(
                IPluginAssemblyRepository.ComponentType,
                options.Solution);
        }

        return new AssemblyDeploymentPlan(
            assemblyPlan,
            pluginTypes,
            outdated,
            linkManagedIdentity,
            solution);
    }

    public Task<PluginTypeDeploymentPlan> BuildPluginTypesAsync(
        Guid? assemblyId,
        IReadOnlyList<LocalPluginType> localTypes,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(localTypes);

        return BuildPluginTypesCoreAsync(assemblyId, localTypes, cancellationToken);
    }

    private async Task<PluginTypeDeploymentPlan> BuildPluginTypesCoreAsync(
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
        var reconciliation = PluginPushPlanner.PlanPluginTypes(registeredTypes, managedRemoteTypes);
        var typeItems = new List<PluginTypeDeploymentItem>();

        foreach (var typePlan in reconciliation.Plans)
        {
            var steps = new List<PluginStepDeploymentPlan>();
            var deleteSteps = new List<PluginStepDeletionPlan>();

            if (string.IsNullOrEmpty(typePlan.Local.CustomApi))
            {
                var remoteSteps = typePlan.Existing is { } existingType
                    ? await repositories.Steps.ListByPluginTypeAsync(existingType.Id, cancellationToken)
                    : [];
                var stepReconciliation = PluginPushPlanner.PlanPluginSteps(typePlan.Local.Steps, remoteSteps);
                foreach (var stepPlan in stepReconciliation.Plans)
                {
                    ResolvedSdkMessage? message = null;
                    if (stepPlan.Action is PluginStepAction.Create or PluginStepAction.Update)
                    {
                        message = await repositories.Messages.ResolveAsync(
                            stepPlan.Local.MessageName,
                            stepPlan.Local.PrimaryEntityName,
                            stepPlan.Local.SecondaryEntityName,
                            cancellationToken);
                        if (message is null)
                        {
                            throw new UnresolvedPluginStepMessageException(
                                stepPlan.Local.Name,
                                stepPlan.Local.MessageName,
                                stepPlan.Local.PrimaryEntityName);
                        }
                    }

                    var remoteImages = stepPlan.Existing is { } existingStep
                        ? await repositories.Images.ListByStepAsync(existingStep.Id, cancellationToken)
                        : [];
                    var images = PluginPushPlanner.PlanPluginStepImages(stepPlan.Local.Images, remoteImages);
                    var unchangedImages = stepPlan.Local.Images
                        .Where(image => remoteImages.Any(remote =>
                            remote.Name == image.Name &&
                            remote.ImageType == image.ImageType) &&
                            images.Plans.All(plan => plan.Local != image))
                        .ToList();
                    steps.Add(new PluginStepDeploymentPlan(stepPlan, message, images, unchangedImages));
                }

                foreach (var step in stepReconciliation.Purge)
                {
                    deleteSteps.Add(new PluginStepDeletionPlan(step));
                }
            }

            var customApi = await BuildCustomApiAsync(typePlan, cancellationToken);
            typeItems.Add(new PluginTypeDeploymentItem(typePlan, steps, deleteSteps, customApi));
        }

        var deleteTypes = new List<PluginTypeDeletionPlan>();
        foreach (var type in reconciliation.Purge)
        {
            var dependentSteps = await repositories.Types.GetDependentStepIdsAsync(type.Id, cancellationToken);
            deleteTypes.Add(new PluginTypeDeletionPlan(type, dependentSteps));
        }

        return new PluginTypeDeploymentPlan(typeItems, deleteTypes);
    }

    private async Task<PluginCustomApiPlan> BuildCustomApiAsync(
        PluginTypePlan typePlan,
        CancellationToken cancellationToken)
    {
        Guid? desiredId = null;
        if (!string.IsNullOrEmpty(typePlan.Local.CustomApi))
        {
            desiredId = await repositories.CustomApis.FindIdByUniqueNameAsync(
                typePlan.Local.CustomApi,
                cancellationToken);
            if (desiredId is null)
            {
                throw new MissingCustomApiException(typePlan.Local.CustomApi);
            }
        }

        var linked = typePlan.Existing is { } existing
            ? await repositories.CustomApis.ListLinkedToPluginTypeAsync(existing.Id, cancellationToken)
            : [];
        var unlink = linked.Where(id => id != desiredId).ToList();

        var link = desiredId is { } customApiId && !linked.Contains(customApiId);
        var unchanged = desiredId is not null && !link;

        return new PluginCustomApiPlan(
            typePlan.Local.CustomApi,
            desiredId,
            unlink,
            link,
            unchanged);
    }

    public Task<OutdatedAssemblyDeploymentPlan> BuildOutdatedAssembliesAsync(
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

    private async Task<OutdatedAssemblyDeploymentPlan> BuildOutdatedAssembliesCoreAsync(
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
            return new OutdatedAssemblyDeploymentPlan([]);
        }

        var registeredReplacements = replacementTypes
            .Where(type => type.HasRegistrationAttribute)
            .ToList();
        var assemblyPlans = new List<OutdatedAssemblyDeploymentItem>();

        foreach (var outdated in outdatedAssemblies)
        {
            var outdatedTypes = await repositories.Types.ListByAssemblyAsync(outdated.Id, cancellationToken);
            var migrations = PluginPushPlanner.PlanOutdatedTypeMigration(
                outdatedTypes,
                registeredReplacements);
            var typePlans = new List<OutdatedTypeDeploymentPlan>();

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
                    var stepPlan = PluginPushPlanner.PlanPluginSteps(replacement.Steps, oldSteps);
                    migrateStepIds = stepPlan.Purge.Select(step => step.Id).ToList();
                    deleteStepIds = dependentStepIds
                        .Where(id => !migrateStepIds.Contains(id))
                        .ToList();
                }

                typePlans.Add(new OutdatedTypeDeploymentPlan(
                    migration,
                    linkedApis,
                    migrateStepIds,
                    deleteStepIds));
            }

            assemblyPlans.Add(new OutdatedAssemblyDeploymentItem(outdated, typePlans));
        }

        return new OutdatedAssemblyDeploymentPlan(assemblyPlans);
    }
}
