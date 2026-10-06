// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

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
    public Task<Guid> ExecuteAsync(PluginDeploymentPlan plan, Action<PluginDeploymentProgress>? reportProgress = null, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(plan);
        return plan switch
        {
            AssemblyDeploymentPlan assembly => ExecuteAssemblyAsync(assembly, reportProgress, cancellationToken),
            PackageDeploymentPlan package => ExecutePackageAsync(package, reportProgress, cancellationToken),
            _ => throw new ArgumentOutOfRangeException(nameof(plan), plan.GetType(), "Unknown plugin deployment plan type.")
        };
    }

    private async Task<Guid> ExecuteAssemblyAsync(AssemblyDeploymentPlan plan, Action<PluginDeploymentProgress>? reportProgress, CancellationToken cancellationToken)
    {
        var assemblyId = await ApplyAssemblyAsync(plan, resolvedPackageAssemblyId: null, reportProgress, cancellationToken);
        if (plan.PluginTypes is null)
        {
            return assemblyId;
        }

        var typeIds = await typeExecutor.ApplyTypesAsync(plan.PluginTypes, assemblyId, reportProgress, cancellationToken);
        await typeExecutor.ApplyDataProvidersAsync(plan.PluginTypes.DataProviders, typeIds, reportProgress, cancellationToken);
        await typeExecutor.ApplyRegistrationsAsync(plan.PluginTypes, typeIds, reportProgress, cancellationToken);
        await typeExecutor.DeleteTypesAsync(plan.PluginTypes.Deletions, reportProgress, cancellationToken);
        await ApplyAssemblyManagedIdentityAsync(plan, assemblyId, reportProgress, cancellationToken);
        await outdatedAssemblyMigrator.ApplyAsync(plan.OutdatedAssemblies, reportProgress, cancellationToken);
        return assemblyId;
    }

    private async Task ApplyAssemblyManagedIdentityAsync(AssemblyDeploymentPlan plan, Guid assemblyId,
        Action<PluginDeploymentProgress>? reportProgress, CancellationToken cancellationToken)
    {
        if (plan.LinkManagedIdentity)
        {
            var local = plan.Comparison.Local;
            var managedIdentityId = await managedIdentityRepository.EnsureAsync(local.ManagedIdentityClientId!, local.ManagedIdentityTenantId, cancellationToken);
            await managedIdentityRepository.LinkToAssemblyAsync(assemblyId, managedIdentityId, cancellationToken);
            Report(reportProgress, PluginDeploymentOperation.Linked, "managed identity", local.ManagedIdentityClientId!);
        }
    }

    private async Task<Guid> ApplyAssemblyAsync(AssemblyDeploymentPlan deployment, Guid? resolvedPackageAssemblyId, Action<PluginDeploymentProgress>? reportProgress,
        CancellationToken cancellationToken)
    {
        var comparison = deployment.Comparison;
        Guid assemblyId;
        if (resolvedPackageAssemblyId is { } packageAssemblyId)
        {
            assemblyId = packageAssemblyId;
        }
        else
        {
            if (comparison.RequiresCreate || comparison.RequiresUpgrade)
            {
                assemblyId = await assemblyRepository.CreateAsync(comparison.Local.Name, comparison.Local.Content, cancellationToken);
                Report(reportProgress, PluginDeploymentOperation.Created, "assembly", comparison.Local.Identity);
            }
            else
            {
                assemblyId = comparison.Remote!.Id;
                if (comparison.RequiresUpdate)
                {
                    await assemblyRepository.UpdateContentAsync(assemblyId, comparison.Local.Content, cancellationToken);
                    Report(reportProgress, PluginDeploymentOperation.Updated, "assembly", comparison.Local.Identity);
                }
            }
        }

        if (deployment.Solution is { } solution)
        {
            await solutionRepository.AddToSolutionAsync(solution.ComponentType, assemblyId, solution.SolutionUniqueName, cancellationToken);
            Report(reportProgress, PluginDeploymentOperation.Added, "assembly", $"{comparison.Local.Identity} to solution {solution.SolutionUniqueName}");
        }

        return assemblyId;
    }

    private async Task<Guid> ExecutePackageAsync(PackageDeploymentPlan deployment, Action<PluginDeploymentProgress>? reportProgress, CancellationToken cancellationToken)
    {
        var packageId = await ApplyPackageAsync(deployment, reportProgress, cancellationToken);
        var packageTypeIds = new Dictionary<string, Guid>(StringComparer.Ordinal);

        foreach (var assembly in deployment.Assemblies)
        {
            var remoteAssembly = await assemblyRepository.FindForPackageDeploymentAsync(assembly.Comparison.Local.Name, assembly.Comparison.Local.Version, packageId, cancellationToken);
            if (remoteAssembly?.PackageId is { } ownerPackageId && ownerPackageId != packageId)
            {
                throw new InvalidOperationException($"Assembly '{assembly.Comparison.Local.Name}' belongs to another plugin package.");
            }

            var packageAssemblyId = remoteAssembly?.PackageId == packageId ? remoteAssembly.Id : (Guid?)null;
            var assemblyId = await ApplyAssemblyAsync(assembly, packageAssemblyId, reportProgress, cancellationToken);
            if (assembly.PluginTypes is not { } types)
            {
                continue;
            }

            var typeIds = await typeExecutor.ApplyTypesAsync(types, assemblyId, reportProgress, cancellationToken);
            await typeExecutor.ApplyRegistrationsAsync(types, typeIds, reportProgress, cancellationToken);
            await ApplyAssemblyManagedIdentityAsync(assembly, assemblyId, reportProgress, cancellationToken);
            foreach (var (typeName, typeId) in typeIds)
            {
                packageTypeIds[typeName] = typeId;
            }
        }

        await typeExecutor.ApplyDataProvidersAsync(deployment.DataProviders, packageTypeIds, reportProgress, cancellationToken);
        foreach (var assembly in deployment.Assemblies)
        {
            if (assembly.PluginTypes is { } types)
            {
                await typeExecutor.DeleteTypesAsync(types.Deletions, reportProgress, cancellationToken);
            }

            await outdatedAssemblyMigrator.ApplyAsync(assembly.OutdatedAssemblies, reportProgress, cancellationToken);
        }

        if (deployment.LinkManagedIdentity)
        {
            var source = deployment.Package.Assemblies.First(assembly => !string.IsNullOrWhiteSpace(assembly.ManagedIdentityClientId));
            var managedIdentityId = await managedIdentityRepository.EnsureAsync(source.ManagedIdentityClientId!, source.ManagedIdentityTenantId, cancellationToken);
            await managedIdentityRepository.LinkToPackageAsync(packageId, managedIdentityId, cancellationToken);
            Report(reportProgress, PluginDeploymentOperation.Linked, "managed identity", source.ManagedIdentityClientId!);
        }

        return packageId;
    }

    private async Task<Guid> ApplyPackageAsync(PackageDeploymentPlan deployment, Action<PluginDeploymentProgress>? reportProgress, CancellationToken cancellationToken)
    {
        var comparison = deployment.Comparison;
        Guid packageId;
        if (comparison.RequiresCreate)
        {
            packageId = await packageRepository.CreateAsync(deployment.DataverseName, comparison.Local.Version, comparison.Local.Content, cancellationToken);
            Report(reportProgress, PluginDeploymentOperation.Created, "package", deployment.DataverseName);
        }
        else if (comparison.RequiresUpdate)
        {
            packageId = comparison.Remote!.Id;
            await packageRepository.UpdateContentAsync(packageId, comparison.Local.Content, cancellationToken);
            Report(reportProgress, PluginDeploymentOperation.Updated, "package", deployment.DataverseName);
        }
        else
        {
            packageId = comparison.Remote!.Id;
        }

        if (deployment.Solution is { } solution)
        {
            await solutionRepository.AddToSolutionAsync(solution.ComponentType, packageId, solution.SolutionUniqueName, cancellationToken);
            Report(reportProgress, PluginDeploymentOperation.Added, "package", $"{solution.ComponentName} to solution {solution.SolutionUniqueName}");
        }

        return packageId;
    }

    private static void Report(Action<PluginDeploymentProgress>? reportProgress, PluginDeploymentOperation operation, string resource, string name) =>
        reportProgress?.Invoke(new PluginDeploymentProgress(operation, resource, name));
}
