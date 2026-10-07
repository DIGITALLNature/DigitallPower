// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Deployment.Execution;
using dgt.power.plugin.Deployment.Local;
using dgt.power.plugin.Exceptions;
using dgt.power.plugin.Models;
using dgt.power.plugin.Repositories;
using Microsoft.PowerPlatform.Dataverse.Client;

namespace dgt.power.plugin.Deployment.Reconciliation;

internal sealed class PluginDeployment(IOrganizationServiceAsync2 service)
{
    private readonly PluginAssemblyRepository _assemblies = new(service);
    private readonly PluginTypeRepository _types = new(service);
    private readonly SdkMessageProcessingStepRepository _steps = new(service);
    private readonly SdkMessageProcessingStepImageRepository _images = new(service);
    private readonly SdkMessageRepository _messages = new(service);
    private readonly CustomApiRepository _apis = new(service);
    private readonly EntityDataProviderRepository _providers = new(service);
    private readonly PluginPackageRepository _packages = new(service);

    internal Task<StandaloneDeploymentChanges> PrepareAsync(LocalAssembly desired, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(desired);
        return PrepareCoreAsync(desired, cancellationToken);
    }

    private async Task<StandaloneDeploymentChanges> PrepareCoreAsync(LocalAssembly desired, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        if (desired.ManagedIdentityClientId is not null || desired.PluginTypes.Any(type => type.CustomApi.Length > 0 || type.DataProviders.Count > 0))
        {
            throw new NotSupportedException("The replacement standalone core does not yet support identity, Custom API, or provider declarations.");
        }

        // Validate the whole local target before remote reads or resolving individual operations.
        RegistrationDiff.Compare(desired.PluginTypes, new RegistrationSnapshot([], new Dictionary<Guid, IReadOnlyList<RemotePluginStep>>(),
            new Dictionary<Guid, IReadOnlyList<RemotePluginStepImage>>()));

        var assemblies = await _assemblies.ListStandaloneByNameAsync(desired.Name, cancellationToken);
        if (assemblies.Count > 2)
        {
            throw new InvalidOperationException($"Assembly '{desired.Name}' has more than two standalone records; recovery is ambiguous.");
        }

        var targetTrain = assemblies.Where(assembly => assembly.Version.Major == desired.Version.Major && assembly.Version.Minor == desired.Version.Minor).ToList();
        if (targetTrain.Count > 1 || assemblies.Count == 2 && targetTrain.Count != 1)
        {
            throw new InvalidOperationException($"Assembly '{desired.Name}' has ambiguous standalone version trains.");
        }

        var existing = targetTrain.SingleOrDefault();
        var superseded = assemblies.SingleOrDefault(assembly => assembly.Id != existing?.Id);
        if (superseded?.ManagedIdentityId is not null)
        {
            throw new NotSupportedException("The superseded assembly has a managed identity association; identity transfer must be implemented before replacement.");
        }

        if (assemblies.Count == 2 && !string.Equals(existing!.ContentHash, desired.ContentHash, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException($"Recovery of '{desired.Name}' requires the target assembly content to match the requested artifact. Resolve the content mismatch before retrying.");
        }

        if (assemblies.Count == 0 && await _assemblies.FindForDeploymentAsync(desired.Name, desired.Version, cancellationToken) is { PackageId: not null })
        {
            throw new InvalidOperationException($"Assembly '{desired.Name}' belongs to a package and cannot be deployed as a standalone assembly.");
        }

        var snapshot = await LoadSnapshotAsync(existing?.Id, cancellationToken);
        var oldSnapshot = await LoadSnapshotAsync(superseded?.Id, cancellationToken);
        var changes = RegistrationDiff.Compare(desired.PluginTypes, snapshot, oldSnapshot);
        foreach (var type in changes.TypesToDelete)
        {
            var owner = snapshot.StepsByType.ContainsKey(type.Id) ? snapshot : oldSnapshot;
            if (owner.StepsByType[type.Id].Any(step => !LocalPluginStepValidator.IsDeclarativeStage(step.Stage)))
            {
                throw new NotSupportedException($"Plugin type '{type.TypeName}' has platform-only steps; its owning association phase must be implemented before cleanup.");
            }

            var knownStepIds = owner.StepsByType[type.Id].Select(step => step.Id).ToHashSet();
            var dependentStepIds = await _types.GetDependentStepIdsAsync(type.Id, cancellationToken);
            if (dependentStepIds.Any(id => !knownStepIds.Contains(id)))
            {
                throw new InvalidOperationException($"Plugin type '{type.TypeName}' has step dependencies missing from the registration snapshot; cleanup cannot be prepared safely.");
            }
        }

        await EnsureNoProviderReferencesAsync(changes.TypesToDelete.Select(type => type.Id).ToHashSet(), cancellationToken);

        var messages = new Dictionary<(string TypeName, LocalPluginStep Step), ResolvedSdkMessage>();
        foreach (var write in changes.StepsToWrite)
        {
            var step = write.Desired;
            var message = await _messages.ResolveAsync(step.MessageName, NormalizeEntity(step.PrimaryEntityName), NormalizeEntity(step.SecondaryEntityName), cancellationToken);
            messages.Add((write.TypeName, step), message ?? throw new UnresolvedPluginStepMessageException(step.Name, step.MessageName, step.PrimaryEntityName));
        }

        return new StandaloneDeploymentChanges(desired, existing?.Id,
            existing is not null && !string.Equals(desired.ContentHash, existing.ContentHash, StringComparison.OrdinalIgnoreCase), changes, messages, assemblies, superseded);
    }

    private async Task<RegistrationSnapshot> LoadSnapshotAsync(Guid? assemblyId, CancellationToken cancellationToken)
    {
        var types = assemblyId is null ? [] : await _types.ListByAssemblyAsync(assemblyId.Value, cancellationToken);
        var stepsByType = new Dictionary<Guid, IReadOnlyList<RemotePluginStep>>();
        var imagesByStep = new Dictionary<Guid, IReadOnlyList<RemotePluginStepImage>>();
        foreach (var type in types)
        {
            if ((await _apis.ListLinkedToPluginTypeAsync(type.Id, cancellationToken)).Count > 0)
            {
                throw new NotSupportedException("Existing Custom API associations require the association phase, which is not implemented yet.");
            }

            var steps = await _steps.ListByPluginTypeAsync(type.Id, cancellationToken);
            stepsByType.Add(type.Id, steps);
            foreach (var step in steps.Where(step => LocalPluginStepValidator.IsDeclarativeStage(step.Stage)))
            {
                imagesByStep.Add(step.Id, await _images.ListByStepAsync(step.Id, cancellationToken));
            }
        }

        return new RegistrationSnapshot(types, stepsByType, imagesByStep);
    }

    internal Task<Guid> ApplyAsync(StandaloneDeploymentChanges changes, Action<PluginDeploymentProgress>? progress = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(changes);
        return ApplyCoreAsync(changes, progress, cancellationToken);
    }

    private async Task<Guid> ApplyCoreAsync(StandaloneDeploymentChanges changes, Action<PluginDeploymentProgress>? progress, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var currentAssemblies = await _assemblies.ListStandaloneByNameAsync(changes.Desired.Name, cancellationToken);
        if (!currentAssemblies.OrderBy(assembly => assembly.Id).SequenceEqual(changes.ExpectedAssemblies.OrderBy(assembly => assembly.Id)))
        {
            throw new InvalidOperationException("Assembly state changed after preparation; prepare a fresh deployment before applying.");
        }
        var assemblyId = changes.ExistingAssemblyId ?? await _assemblies.CreateAsync(changes.Desired.Name, changes.Desired.Content, cancellationToken);
        if (changes.ExistingAssemblyId is null)
        {
            Report(PluginDeploymentOperation.Created, "assembly", changes.Desired.Identity);
        }
        else if (changes.UpdateContent)
        {
            await _assemblies.UpdateContentAsync(assemblyId, changes.Desired.Content, cancellationToken);
            Report(PluginDeploymentOperation.Updated, "assembly", changes.Desired.Identity);
        }

        await ApplyRegistrationsAsync(changes.Registrations, changes.Messages, assemblyId, progress, cancellationToken);

        if (changes.AssemblyToDelete is { } oldAssembly)
        {
            if ((await _types.ListByAssemblyAsync(oldAssembly.Id, cancellationToken)).Count > 0)
            {
                throw new InvalidOperationException($"Superseded assembly '{oldAssembly.Identity}' still has plugin types; cleanup stopped.");
            }

            await _assemblies.DeleteAsync(oldAssembly.Id, cancellationToken);
            Report(PluginDeploymentOperation.Deleted, "assembly", oldAssembly.Identity);
        }

        return assemblyId;

        void Report(PluginDeploymentOperation operation, string resource, string name) =>
            progress?.Invoke(new PluginDeploymentProgress(operation, resource, name));
    }

    private async Task ApplyRegistrationsAsync(RegistrationChanges registrations,
        IReadOnlyDictionary<(string TypeName, LocalPluginStep Step), ResolvedSdkMessage> messages,
        Guid assemblyId, Action<PluginDeploymentProgress>? progress, CancellationToken cancellationToken)
    {
        var typeIds = registrations.ExistingTypeIds.ToDictionary(pair => pair.Key, pair => pair.Value, StringComparer.Ordinal);
        var stepIds = registrations.ExistingStepIds.ToDictionary(pair => pair.Key, pair => pair.Value);
        foreach (var type in registrations.TypesToCreate)
        {
            typeIds.Add(type.TypeName, await _types.CreateAsync(assemblyId, type.TypeName, type.Name, cancellationToken));
            Report(PluginDeploymentOperation.Created, "plugin type", type.TypeName);
        }

        foreach (var write in registrations.StepsToWrite)
        {
            var step = write.Desired;
            var message = messages[(write.TypeName, step)];
            var data = new PluginStepData(step.Name, typeIds[write.TypeName], message.MessageId, message.MessageFilterId, step.Stage, step.Mode, step.ExecutionOrder, step.FilterAttributes);
            if (write.ExistingId is { } stepId)
            {
                await _steps.UpdateAsync(stepId, data, cancellationToken);
                Report(PluginDeploymentOperation.Updated, "step", step.Name);
            }
            else
            {
                stepIds.Add((write.TypeName, step), await _steps.CreateAsync(data, cancellationToken));
                Report(PluginDeploymentOperation.Created, "step", step.Name);
            }
        }

        foreach (var write in registrations.ImagesToWrite)
        {
            var image = write.Desired;
            var stepId = stepIds[(write.TypeName, write.Step)];
            if (write.ExistingId is { } imageId)
            {
                await _images.UpdateAsync(imageId, stepId, image.Attributes, cancellationToken);
                Report(PluginDeploymentOperation.Updated, "step image", image.Name);
            }
            else
            {
                await _images.CreateAsync(new PluginStepImageData(stepId, image.Name, image.EntityAlias, image.ImageType, image.MessagePropertyName, image.Attributes), cancellationToken);
                Report(PluginDeploymentOperation.Created, "step image", image.Name);
            }
        }

        foreach (var image in registrations.ImagesToDelete)
        {
            await _images.DeleteAsync(image.Id, cancellationToken);
            Report(PluginDeploymentOperation.Deleted, "step image", image.Name);
        }

        foreach (var step in registrations.StepsToDelete)
        {
            await _steps.DeleteAsync(step.Id, cancellationToken);
            Report(PluginDeploymentOperation.Deleted, "step", step.Name);
        }

        await EnsureNoProviderReferencesAsync(registrations.TypesToDelete.Select(type => type.Id).ToHashSet(), cancellationToken);
        foreach (var type in registrations.TypesToDelete)
        {
            if ((await _types.GetDependentStepIdsAsync(type.Id, cancellationToken)).Count > 0 ||
                (await _apis.ListLinkedToPluginTypeAsync(type.Id, cancellationToken)).Count > 0)
            {
                throw new InvalidOperationException($"Plugin type '{type.TypeName}' still has dependent SDK steps; cleanup stopped.");
            }

            await _types.DeleteAsync(type.Id, cancellationToken);
            Report(PluginDeploymentOperation.Deleted, "plugin type", type.TypeName);
        }

        void Report(PluginDeploymentOperation operation, string resource, string name) =>
            progress?.Invoke(new PluginDeploymentProgress(operation, resource, name));
    }

    private static string NormalizeEntity(string entity) =>
        string.IsNullOrEmpty(entity) || string.Equals(entity, "none", StringComparison.OrdinalIgnoreCase) ? "none" : entity.ToLowerInvariant();

    private async Task EnsureNoProviderReferencesAsync(HashSet<Guid> removedTypeIds, CancellationToken cancellationToken)
    {
        if (removedTypeIds.Count == 0)
        {
            return;
        }

        var providers = await _providers.ListAsync(cancellationToken);
        var dependent = providers.FirstOrDefault(provider => provider.Handlers.Values.Any(removedTypeIds.Contains));
        if (dependent is not null)
        {
            throw new NotSupportedException($"Data provider '{dependent.Name}' references a plugin type scheduled for removal; provider reassignment must be implemented before cleanup.");
        }
    }

    internal Task<PackageDeploymentChanges> PreparePackageAsync(LocalPluginPackage desired, string publisherPrefix, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(desired);
        ArgumentException.ThrowIfNullOrWhiteSpace(publisherPrefix);
        return PreparePackageCoreAsync(desired, publisherPrefix, cancellationToken);
    }

    private async Task<PackageDeploymentChanges> PreparePackageCoreAsync(LocalPluginPackage desired, string publisherPrefix, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        if (desired.Assemblies.Select(assembly => assembly.Name).Distinct(StringComparer.OrdinalIgnoreCase).Count() != desired.Assemblies.Count)
        {
            throw new AssemblyException("Plugin assembly names must be unique within the package.");
        }

        foreach (var assembly in desired.Assemblies)
        {
            if (assembly.ManagedIdentityClientId is not null || assembly.PluginTypes.Any(type => type.CustomApi.Length > 0 || type.DataProviders.Count > 0))
            {
                throw new NotSupportedException("The replacement package core does not yet support identity, Custom API, or provider declarations.");
            }

            RegistrationDiff.Compare(assembly.PluginTypes, new RegistrationSnapshot([], new Dictionary<Guid, IReadOnlyList<RemotePluginStep>>(),
                new Dictionary<Guid, IReadOnlyList<RemotePluginStepImage>>()));
        }

        var name = $"{publisherPrefix}_{desired.Package.Name}";
        var existing = await _packages.FindByNameAsync(name, cancellationToken);
        if (existing?.ManagedIdentityId is not null)
        {
            throw new NotSupportedException("The replacement package core does not yet support packages with managed identity associations.");
        }

        if (existing?.Version is not null && !string.Equals(existing.Version, desired.Package.Version, StringComparison.Ordinal))
        {
            throw new InvalidOperationException($"Package '{name}' already exists at immutable version '{existing.Version}', not '{desired.Package.Version}'.");
        }

        var remoteAssemblies = existing is null ? [] : await _assemblies.ListByPackageAsync(existing.Id, cancellationToken);
        if (remoteAssemblies.GroupBy(assembly => assembly.Name, StringComparer.OrdinalIgnoreCase).Any(group => group.Count() > 1))
        {
            throw new InvalidOperationException($"Package '{name}' has multiple same-name assemblies; deployment is ambiguous.");
        }

        var declaredNames = desired.Assemblies.Select(assembly => assembly.Name).ToHashSet(StringComparer.OrdinalIgnoreCase);
        if (remoteAssemblies.Any(assembly => !declaredNames.Contains(assembly.Name)))
        {
            throw new NotSupportedException("Package assembly removal requires verification of server-side upload/dependency behavior before it can be applied.");
        }

        var assemblies = new List<PackageAssemblyChanges>();
        foreach (var assembly in desired.Assemblies)
        {
            var remote = remoteAssemblies.SingleOrDefault(candidate => string.Equals(candidate.Name, assembly.Name, StringComparison.OrdinalIgnoreCase));
            var snapshot = await LoadSnapshotAsync(remote?.Id, cancellationToken);
            var registrations = RegistrationDiff.Compare(assembly.PluginTypes, snapshot);
            if (registrations.TypesToDelete.Count > 0)
            {
                throw new NotSupportedException("Package type removal requires verification of server-side upload/dependency behavior before it can be applied.");
            }
            if (registrations.StepsToDelete.Count > 0 || registrations.ImagesToDelete.Count > 0)
            {
                throw new NotSupportedException("Package registration removal requires verification of server-side upload/dependency behavior before it can be applied.");
            }

            var messages = new Dictionary<(string TypeName, LocalPluginStep Step), ResolvedSdkMessage>();
            foreach (var write in registrations.StepsToWrite)
            {
                var step = write.Desired;
                var message = await _messages.ResolveAsync(step.MessageName, NormalizeEntity(step.PrimaryEntityName), NormalizeEntity(step.SecondaryEntityName), cancellationToken);
                messages.Add((write.TypeName, step), message ?? throw new UnresolvedPluginStepMessageException(step.Name, step.MessageName, step.PrimaryEntityName));
            }

            assemblies.Add(new PackageAssemblyChanges(assembly, remote, snapshot, registrations, messages));
        }

        return new PackageDeploymentChanges(desired, name, existing,
            existing is not null && !string.Equals(existing.PackageHash, desired.Package.PackageHash, StringComparison.OrdinalIgnoreCase), remoteAssemblies, assemblies);
    }

    internal Task<Guid> ApplyPackageAsync(PackageDeploymentChanges changes, Action<PluginDeploymentProgress>? progress = null, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(changes);
        return ApplyPackageCoreAsync(changes, progress, cancellationToken);
    }

    private async Task<Guid> ApplyPackageCoreAsync(PackageDeploymentChanges changes, Action<PluginDeploymentProgress>? progress, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var current = await _packages.FindByNameAsync(changes.DataverseName, cancellationToken);
        if (current != changes.Existing)
        {
            throw new InvalidOperationException("Package state changed after preparation; prepare a fresh deployment before applying.");
        }

        var beforeUpload = current is null ? [] : await _assemblies.ListByPackageAsync(current.Id, cancellationToken);
        if (!beforeUpload.OrderBy(assembly => assembly.Id).SequenceEqual(changes.ExpectedAssemblies.OrderBy(assembly => assembly.Id)))
        {
            throw new InvalidOperationException("Package assembly state changed after preparation; prepare a fresh deployment before applying.");
        }

        var packageId = current?.Id ?? await _packages.CreateAsync(changes.DataverseName, changes.Desired.Package.Version, changes.Desired.Package.Content, cancellationToken);
        if (current is null)
        {
            progress?.Invoke(new PluginDeploymentProgress(PluginDeploymentOperation.Created, "package", changes.DataverseName));
        }
        else if (changes.UpdateContent)
        {
            await _packages.UpdateContentAsync(packageId, changes.Desired.Package.Content, cancellationToken);
            progress?.Invoke(new PluginDeploymentProgress(PluginDeploymentOperation.Updated, "package", changes.DataverseName));
        }

        // Upload owns assembly/type creation. Resolve all server records before any registration writes.
        var uploadedAssemblies = await _assemblies.ListByPackageAsync(packageId, cancellationToken);
        var desiredAssemblyNames = changes.Assemblies.Select(assembly => assembly.Desired.Name).ToHashSet(StringComparer.OrdinalIgnoreCase);
        if (uploadedAssemblies.Count != desiredAssemblyNames.Count ||
            uploadedAssemblies.Any(assembly => !desiredAssemblyNames.Contains(assembly.Name)) ||
            uploadedAssemblies.Select(assembly => assembly.Name).Distinct(StringComparer.OrdinalIgnoreCase).Count() != uploadedAssemblies.Count)
        {
            throw new InvalidOperationException("Package upload produced an unexpected set of owned assemblies; no registration writes were applied.");
        }

        var resolved = new List<(Guid AssemblyId, PackageAssemblyChanges Changes, RegistrationChanges Registrations)>();
        foreach (var assembly in changes.Assemblies)
        {
            var candidates = uploadedAssemblies.Where(candidate => string.Equals(candidate.Name, assembly.Desired.Name, StringComparison.OrdinalIgnoreCase)).ToList();
            if (candidates.Count != 1)
            {
                throw new InvalidOperationException(
                    $"Package upload did not resolve exactly one owned assembly '{assembly.Desired.Name}'. " +
                    "Prepare a fresh deployment; no standalone fallback will be created.");
            }

            var uploaded = candidates[0];
            if (assembly.Existing is not null && uploaded.Id != assembly.Existing.Id)
            {
                throw new InvalidOperationException($"Package upload replaced assembly '{assembly.Desired.Name}' unexpectedly; prepare a fresh deployment before registration writes.");
            }

            var serverTypes = await _types.ListByAssemblyAsync(uploaded.Id, cancellationToken);
            if (serverTypes.Count != assembly.Desired.PluginTypes.Count ||
                serverTypes.Select(type => type.TypeName).Distinct(StringComparer.Ordinal).Count() != serverTypes.Count)
            {
                throw new InvalidOperationException($"Package assembly '{assembly.Desired.Name}' has unexpected plugin types after upload.");
            }

            var ids = new Dictionary<string, Guid>(StringComparer.Ordinal);
            foreach (var type in assembly.Desired.PluginTypes)
            {
                var serverType = serverTypes.SingleOrDefault(candidate => candidate.TypeName == type.TypeName) ??
                    throw new InvalidOperationException($"Package upload did not register plugin type '{type.TypeName}' in assembly '{assembly.Desired.Name}'.");
                if (assembly.Registrations.ExistingTypeIds.TryGetValue(type.TypeName, out var expectedId) && serverType.Id != expectedId)
                {
                    throw new InvalidOperationException($"Package upload replaced plugin type '{type.TypeName}' unexpectedly; prepare a fresh deployment.");
                }

                ids.Add(type.TypeName, serverType.Id);
            }

            resolved.Add((uploaded.Id, assembly, assembly.Registrations with { TypesToCreate = [], ExistingTypeIds = ids }));
        }

        foreach (var assembly in resolved)
        {
            await ApplyRegistrationsAsync(assembly.Registrations, assembly.Changes.Messages, assembly.AssemblyId, progress, cancellationToken);
        }

        return packageId;
    }
}
