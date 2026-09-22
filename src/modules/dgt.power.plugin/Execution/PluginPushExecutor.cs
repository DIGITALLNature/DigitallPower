// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using dgt.power.dataverse;
using dgt.power.plugin.Repositories;
using dgt.power.plugin.Local;
using dgt.power.plugin.Planning;
using dgt.power.plugin.Output;
using Spectre.Console;

namespace dgt.power.plugin.Execution;

/// <summary>
/// Orchestrates reconciling <see cref="LocalAssembly"/>/<see cref="LocalPluginPackage"/> targets
/// against a Dataverse environment: looks up remote state via the repositories, decides the
/// required action via <see cref="PluginPushPlanner"/>, and applies it (unless
/// <see cref="PluginPushOptions.DryRun"/> is set, in which case only the decision is reported).
/// Plugin type/step/image reconciliation is delegated to <see cref="PluginTypeReconciler"/>.
/// </summary>
public sealed class PluginPushExecutor(
    IPluginAssemblyRepository assemblyRepository,
    IPluginPackageRepository packageRepository,
    ISolutionComponentRepository solutionRepository,
    IManagedIdentityRepository managedIdentityRepository,
    PluginTypeReconciler typeReconciler,
    OutdatedAssemblyMigrator outdatedAssemblyMigrator,
    PluginPlanRenderer planRenderer,
    IAnsiConsole console)
{
    /// <summary>
    /// Reconciles a standalone plugin assembly. Returns the assembly's Dataverse id, or
    /// <see cref="Guid.Empty"/> in dry-run mode (nothing is actually created).
    /// </summary>
    public Task<Guid> ProcessAssemblyAsync(LocalAssembly assembly, PluginPushOptions options, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(assembly);
        ArgumentNullException.ThrowIfNull(options);

        return ProcessAssemblyCoreAsync(
            assembly, options, reconcilePackageOwnedAssembly: false, renderPlan: true, cancellationToken: cancellationToken);
    }

    private async Task<Guid> ProcessAssemblyCoreAsync(
        LocalAssembly assembly,
        PluginPushOptions options,
        bool reconcilePackageOwnedAssembly,
        bool renderPlan,
        CancellationToken cancellationToken)
    {
        var remote = await assemblyRepository.FindByNameAsync(assembly.Name, cancellationToken);
        var plan = PluginPushPlanner.PlanAssembly(assembly, remote);

        if (renderPlan)
        {
            var root = new PluginPlanNode(assembly.Name, plan.Action.ToString(), Emoji.Known.PuzzlePiece);
            if (plan.Action != AssemblyAction.OwnedByPackage)
            {
                foreach (var node in await typeReconciler.BuildPlanAsync(
                             plan.Existing?.Id ?? Guid.Empty, assembly.PluginTypes, cancellationToken))
                {
                    root.AddChild(node);
                }
                var migrationPlan = await outdatedAssemblyMigrator.BuildPlanAsync(
                    assembly.Name, plan.Existing?.Id ?? Guid.Empty, assembly.PluginTypes, cancellationToken);
                if (migrationPlan is not null)
                {
                    root.AddChild(migrationPlan);
                }
            }

            AddAssemblyAncillaryPlanNodes(root, assembly);
            planRenderer.Render(root);
            if (options.DryRun)
            {
                return Guid.Empty;
            }
        }

        var assemblyId = await ApplyAssemblyPlanAsync(plan, options, cancellationToken);
        if (plan.Action == AssemblyAction.OwnedByPackage && !reconcilePackageOwnedAssembly)
        {
            return assemblyId;
        }

        if (!options.DryRun && !string.IsNullOrWhiteSpace(assembly.ManagedIdentityClientId))
        {
            var managedIdentityId = await managedIdentityRepository
                .EnsureAsync(assembly.ManagedIdentityClientId, assembly.ManagedIdentityTenantId, cancellationToken);
            await managedIdentityRepository.LinkToAssemblyAsync(assemblyId, managedIdentityId, cancellationToken);
        }

        // assemblyId is only Guid.Empty in dry-run for a not-yet-created assembly - reconciling against it
        // still previews every plugin type/step/image/Custom API link declared in the local assembly, since
        // every downstream repository lookup keyed by Guid.Empty safely returns "nothing exists yet".
        await typeReconciler.ReconcileAsync(assemblyId, assembly.PluginTypes, options, cancellationToken);

        if (plan.Action == AssemblyAction.Upgrade)
        {
            await outdatedAssemblyMigrator.MigrateAsync(assembly.Name, assemblyId, assembly.PluginTypes, options, cancellationToken);
        }

        return assemblyId;
    }

    /// <summary>
    /// Reconciles a plugin package and the assemblies bundled inside it. Returns the package's
    /// Dataverse id, or <see cref="Guid.Empty"/> in dry-run mode.
    /// </summary>
    public Task<Guid> ProcessPackageAsync(LocalPluginPackage package, PluginPushOptions options, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(package);
        ArgumentNullException.ThrowIfNull(options);
        ArgumentException.ThrowIfNullOrWhiteSpace(options.PublisherPrefix);

        return ProcessPackageCoreAsync(package, options, cancellationToken);
    }

    private async Task<Guid> ProcessPackageCoreAsync(LocalPluginPackage package, PluginPushOptions options, CancellationToken cancellationToken)
    {
        var packageName = $"{options.PublisherPrefix}_{package.Package.Name}";
        var remote = await packageRepository.FindByNameAsync(packageName, cancellationToken);
        var plan = PluginPushPlanner.PlanPackage(package.Package, remote);

        var packagePlanNode = new PluginPlanNode(packageName, plan.Action.ToString(), Emoji.Known.Package);
        if (package.Assemblies.Any(assembly => !string.IsNullOrWhiteSpace(assembly.ManagedIdentityClientId)))
        {
            packagePlanNode.AddChild(new PluginPlanNode("Managed identity", "Link"));
        }

        foreach (var localAssembly in package.Assemblies)
        {
            var assemblyRemote = await assemblyRepository.FindByNameAsync(localAssembly.Name, cancellationToken);
            var assemblyPlan = PluginPushPlanner.PlanAssembly(localAssembly, assemblyRemote);
            var assemblyNode = new PluginPlanNode(localAssembly.Name, assemblyPlan.Action.ToString(), Emoji.Known.PuzzlePiece);
            foreach (var node in await typeReconciler.BuildPlanAsync(
                         assemblyPlan.Existing?.Id ?? Guid.Empty, localAssembly.PluginTypes, cancellationToken))
            {
                assemblyNode.AddChild(node);
            }
            var migrationPlan = await outdatedAssemblyMigrator.BuildPlanAsync(
                localAssembly.Name, assemblyPlan.Existing?.Id ?? Guid.Empty, localAssembly.PluginTypes, cancellationToken);
            if (migrationPlan is not null)
            {
                assemblyNode.AddChild(migrationPlan);
            }
            AddAssemblyAncillaryPlanNodes(assemblyNode, localAssembly);

            packagePlanNode.AddChild(assemblyNode);
        }

        planRenderer.Render(packagePlanNode);
        if (options.DryRun)
        {
            return Guid.Empty;
        }

        var packageId = await ApplyPackagePlanAsync(plan, options, cancellationToken);

        // Only the first bundled assembly carrying a ManagedIdentityRegistration attribute links the
        // identity to the package itself (mirrors the previous behaviour: one identity per package).
        var packageIdentityLinked = false;
        foreach (var localAssembly in package.Assemblies)
        {
            await ProcessAssemblyCoreAsync(
                localAssembly, options, reconcilePackageOwnedAssembly: true, renderPlan: false, cancellationToken: cancellationToken);

            if (packageIdentityLinked || options.DryRun || string.IsNullOrWhiteSpace(localAssembly.ManagedIdentityClientId))
            {
                continue;
            }

            var managedIdentityId = await managedIdentityRepository
                .EnsureAsync(localAssembly.ManagedIdentityClientId, localAssembly.ManagedIdentityTenantId, cancellationToken);
            await managedIdentityRepository.LinkToPackageAsync(packageId, managedIdentityId, cancellationToken);
            packageIdentityLinked = true;
        }

        return packageId;
    }

    private static void AddAssemblyAncillaryPlanNodes(
        PluginPlanNode root,
        LocalAssembly assembly)
    {
        if (!string.IsNullOrWhiteSpace(assembly.ManagedIdentityClientId))
        {
            root.AddChild(new PluginPlanNode("Managed identity", "Link"));
        }
    }

    private async Task<Guid> ApplyAssemblyPlanAsync(AssemblyPlan plan, PluginPushOptions options, CancellationToken cancellationToken)
    {
        var (assembly, action, existing) = plan;

        switch (action)
        {
            case AssemblyAction.OwnedByPackage:
                console.MarkupLine(CultureInfo.InvariantCulture,
                    "Assembly [bold green]{0}[/] is already owned by a plugin package - skipping standalone reconciliation", assembly.Name);
                return existing!.Id;

            case AssemblyAction.Update:
                console.MarkupLine(CultureInfo.InvariantCulture, "Update Assembly [bold green]{0}[/]", assembly.Name);
                if (!options.DryRun)
                {
                    await assemblyRepository.UpdateContentAsync(existing!.Id, assembly.Content, cancellationToken);
                }

                return existing!.Id;

            default:
                if (action == AssemblyAction.Upgrade)
                {
                    console.MarkupLine(CultureInfo.InvariantCulture, "Upgrade Assembly [bold green]{0}[/] ({1} -> {2})",
                        assembly.Name, existing!.Version, assembly.Version);
                }
                else
                {
                    console.MarkupLine(CultureInfo.InvariantCulture, "Create Assembly [bold green]{0}[/] ({1})",
                        assembly.Name, assembly.Version);
                }

                if (options.DryRun)
                {
                    return Guid.Empty;
                }

                var assemblyId = await assemblyRepository.CreateAsync(assembly.Name, assembly.Content, cancellationToken);
                await AddToSolutionAsync(IPluginAssemblyRepository.ComponentType, assemblyId, options, cancellationToken);
                return assemblyId;
        }
    }

    private async Task<Guid> ApplyPackagePlanAsync(PackagePlan plan, PluginPushOptions options, CancellationToken cancellationToken)
    {
        var (package, action, existing) = plan;

        if (action == PackageAction.Update)
        {
            console.MarkupLine(CultureInfo.InvariantCulture, "Update Package [bold green]{0}[/]", package.Name);
            if (!options.DryRun)
            {
                await packageRepository.UpdateContentAsync(existing!.Id, package.Content, cancellationToken);
            }

            return existing!.Id;
        }

        var name = $"{options.PublisherPrefix}_{package.Name}";
        console.MarkupLine(CultureInfo.InvariantCulture, "Create Package [bold green]{0}[/] ({1})", name, package.Version);

        if (options.DryRun)
        {
            return Guid.Empty;
        }

        var packageId = await packageRepository.CreateAsync(name, package.Version, package.Content, cancellationToken);
        var componentType = await solutionRepository.GetComponentTypeAsync(PluginPackage.EntityLogicalName, cancellationToken);
        if (componentType is not null)
        {
            await AddToSolutionAsync(componentType.Value, packageId, options, cancellationToken);
        }

        return packageId;
    }

    private async Task AddToSolutionAsync(int componentType, Guid componentId, PluginPushOptions options, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(options.Solution))
        {
            return;
        }

        await solutionRepository.AddToSolutionAsync(componentType, componentId, options.Solution, cancellationToken);
    }
}
