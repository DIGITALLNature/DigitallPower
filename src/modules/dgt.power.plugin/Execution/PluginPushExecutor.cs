// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Planning;
using dgt.power.plugin.Planning.Changes;
using dgt.power.plugin.Planning.Deployment;
using dgt.power.plugin.Repositories;

namespace dgt.power.plugin.Execution;

public sealed class PluginPushExecutor(
    IPluginAssemblyRepository assemblyRepository,
    IPluginPackageRepository packageRepository,
    ISolutionComponentRepository solutionRepository,
    IManagedIdentityRepository managedIdentityRepository,
    PluginTypeDeploymentExecutor typeExecutor,
    OutdatedAssemblyMigrator outdatedAssemblyMigrator)
{
    public Task<Guid> ExecuteAsync(
        PluginDeploymentPlan plan,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(plan);
        return plan switch
        {
            AssemblyDeploymentPlan assembly => ExecuteAssemblyAsync(
                assembly,
                resolvedPackageAssemblyId: null,
                cancellationToken),
            PackageDeploymentPlan package => ExecutePackageAsync(package, cancellationToken),
            _ => throw new ArgumentOutOfRangeException(nameof(plan), plan.GetType(), "Unknown plugin deployment plan type.")
        };
    }

    private async Task<Guid> ExecuteAssemblyAsync(
        AssemblyDeploymentPlan plan,
        Guid? resolvedPackageAssemblyId,
        CancellationToken cancellationToken)
    {
        var assemblyId = await ApplyAssemblyAsync(
            plan,
            resolvedPackageAssemblyId,
            cancellationToken);
        if (plan.PluginTypes is null)
        {
            return assemblyId;
        }

        var typeIds = await typeExecutor.ApplyAsync(
            plan.PluginTypes,
            assemblyId,
            cancellationToken);

        if (plan.LinkManagedIdentity)
        {
            var local = plan.Assembly.Local;
            var managedIdentityId = await managedIdentityRepository.EnsureAsync(
                local.ManagedIdentityClientId!,
                local.ManagedIdentityTenantId,
                cancellationToken);
            await managedIdentityRepository.LinkToAssemblyAsync(
                assemblyId,
                managedIdentityId,
                cancellationToken);
        }

        await outdatedAssemblyMigrator.ApplyAsync(
            plan.OutdatedAssemblies,
            typeIds,
            cancellationToken);
        return assemblyId;
    }

    private async Task<Guid> ApplyAssemblyAsync(
        AssemblyDeploymentPlan deployment,
        Guid? resolvedPackageAssemblyId,
        CancellationToken cancellationToken)
    {
        var plan = deployment.Assembly;
        Guid assemblyId;
        if (resolvedPackageAssemblyId is { } packageAssemblyId)
        {
            assemblyId = packageAssemblyId;
        }
        else
        {
            switch (plan)
            {
                case PackageOwnedAssemblyChange packageOwned:
                    assemblyId = packageOwned.Remote.Id;
                    break;

                case UpdateAssemblyChange update:
                    assemblyId = update.Remote.Id;
                    await assemblyRepository.UpdateContentAsync(
                        assemblyId,
                        update.Local.Content,
                        cancellationToken);
                    break;

                case CreateAssemblyChange:
                case UpgradeAssemblyChange:
                    assemblyId = await assemblyRepository.CreateAsync(
                        plan.Local.Name,
                        plan.Local.Content,
                        cancellationToken);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(
                        nameof(deployment),
                        plan.GetType(),
                        "Unknown assembly deployment action.");
            }
        }

        if (deployment.Solution is { } solution)
        {
            await solutionRepository.AddToSolutionAsync(
                solution.ComponentType,
                assemblyId,
                solution.SolutionUniqueName,
                cancellationToken);
        }

        return assemblyId;
    }

    private async Task<Guid> ExecutePackageAsync(
        PackageDeploymentPlan deployment,
        CancellationToken cancellationToken)
    {
        var packageId = await ApplyPackageAsync(deployment, cancellationToken);

        foreach (var assembly in deployment.Assemblies)
        {
            var remoteAssembly = await assemblyRepository.FindByNameAsync(
                assembly.Assembly.Local.Name,
                cancellationToken);
            if (remoteAssembly?.PackageId is { } ownerPackageId && ownerPackageId != packageId)
            {
                throw new InvalidOperationException(
                    $"Assembly '{assembly.Assembly.Local.Name}' belongs to another plugin package.");
            }

            var packageAssemblyId = remoteAssembly?.PackageId == packageId
                ? remoteAssembly.Id
                : (Guid?)null;
            await ExecuteAssemblyAsync(
                assembly,
                packageAssemblyId,
                cancellationToken);
        }

        if (deployment.LinkManagedIdentity)
        {
            var source = deployment.Package.Assemblies.First(
                assembly => !string.IsNullOrWhiteSpace(assembly.ManagedIdentityClientId));
            var managedIdentityId = await managedIdentityRepository.EnsureAsync(
                source.ManagedIdentityClientId!,
                source.ManagedIdentityTenantId,
                cancellationToken);
            await managedIdentityRepository.LinkToPackageAsync(
                packageId,
                managedIdentityId,
                cancellationToken);
        }

        return packageId;
    }

    private async Task<Guid> ApplyPackageAsync(
        PackageDeploymentPlan deployment,
        CancellationToken cancellationToken)
    {
        var plan = deployment.Change;
        Guid packageId;
        if (plan.Action == PackageAction.Update)
        {
            packageId = plan.Existing!.Id;
            await packageRepository.UpdateContentAsync(
                packageId,
                plan.Local.Content,
                cancellationToken);
        }
        else
        {
            packageId = await packageRepository.CreateAsync(
                deployment.DataverseName,
                plan.Local.Version,
                plan.Local.Content,
                cancellationToken);
        }

        if (deployment.Solution is { } solution)
        {
            await solutionRepository.AddToSolutionAsync(
                solution.ComponentType,
                packageId,
                solution.SolutionUniqueName,
                cancellationToken);
        }

        return packageId;
    }
}
