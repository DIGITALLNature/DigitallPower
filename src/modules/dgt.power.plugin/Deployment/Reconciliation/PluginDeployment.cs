// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

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
    private readonly SolutionComponentRepository _solutions = new(service);

    internal Task<StandaloneDeploymentChanges> PrepareAsync(LocalAssembly desired, string? solution = null, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(desired);
        return PrepareCoreAsync(desired, solution, cancellationToken);
    }

    private async Task<StandaloneDeploymentChanges> PrepareCoreAsync(LocalAssembly desired, string? solution, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ValidateCustomApiDeclarations([desired]);
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
        await ValidateRemovedTypesAsync(changes, [snapshot, oldSnapshot], cancellationToken);

        var messages = new Dictionary<PluginStepReference, ResolvedSdkMessage>();
        foreach (var write in changes.StepsToWrite)
        {
            var step = write.Desired;
            var message = await _messages.ResolveAsync(step.MessageName, NormalizeEntity(step.PrimaryEntityName), NormalizeEntity(step.SecondaryEntityName), cancellationToken);
            messages.Add(new PluginStepReference(write.TypeName, step), message ?? throw new UnresolvedPluginStepMessageException(step.Name, step.MessageName, step.PrimaryEntityName));
        }

        var customApis = await PrepareCustomApisAsync([desired], [.. snapshot.Types, .. oldSnapshot.Types],
            changes.ExistingTypeIds.ToDictionary(pair => new PluginTypeReference(desired.Name, pair.Key), pair => pair.Value), cancellationToken);
        var membership = await PrepareStandaloneSolutionAsync(desired, existing?.Id, changes, solution, cancellationToken);
        var providers = await PrepareProvidersAsync([desired], [.. snapshot.Types, .. oldSnapshot.Types],
            changes.ExistingTypeIds.ToDictionary(pair => new PluginTypeReference(desired.Name, pair.Key), pair => pair.Value), solution, cancellationToken);
        if (membership is not null)
        {
            membership = membership with { Providers = providers.Solution };
        }
        return new StandaloneDeploymentChanges(desired, existing?.Id,
            existing is not null && !string.Equals(desired.ContentHash, existing.ContentHash, StringComparison.OrdinalIgnoreCase),
            changes, messages, assemblies, superseded, customApis, membership, providers.Changes);
    }

    private async Task ValidateRemovedTypesAsync(RegistrationChanges changes, IReadOnlyList<RegistrationSnapshot> snapshots, CancellationToken cancellationToken)
    {
        foreach (var type in changes.TypesToDelete)
        {
            var owner = snapshots.First(snapshot => snapshot.StepsByType.ContainsKey(type.Id));
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
    }

    private async Task<RegistrationSnapshot> LoadSnapshotAsync(Guid? assemblyId, CancellationToken cancellationToken)
    {
        var types = assemblyId is null ? [] : await _types.ListByAssemblyAsync(assemblyId.Value, cancellationToken);
        var stepsByType = new Dictionary<Guid, IReadOnlyList<RemotePluginStep>>();
        var imagesByStep = new Dictionary<Guid, IReadOnlyList<RemotePluginStepImage>>();
        foreach (var type in types)
        {
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

        var resolved = await ApplyRegistrationsAsync(changes.Registrations, changes.Messages, assemblyId, progress, cancellationToken);
        await ApplyCustomApisAsync(changes.CustomApis,
            resolved.TypeIds.ToDictionary(pair => new PluginTypeReference(changes.Desired.Name, pair.Key), pair => pair.Value), progress, cancellationToken);
        var providerIds = await ApplyProvidersAsync(changes.Providers,
            resolved.TypeIds.ToDictionary(pair => new PluginTypeReference(changes.Desired.Name, pair.Key), pair => pair.Value), progress, cancellationToken);
        if (changes.Solution is { } solution)
        {
            await ApplyProviderMembershipAsync(solution.Providers, solution.UniqueName, providerIds, progress, cancellationToken);
            if (solution.AddAssembly)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await _solutions.AddToSolutionAsync(IPluginAssemblyRepository.ComponentType, assemblyId, solution.UniqueName, cancellationToken);
                Report(PluginDeploymentOperation.AddedToSolution, "assembly", changes.Desired.Identity);
            }

            foreach (var step in solution.Steps)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await _solutions.AddToSolutionAsync(ISdkMessageProcessingStepRepository.ComponentType,
                    resolved.StepIds[new PluginStepReference(step.TypeName, step.Step)], solution.UniqueName, cancellationToken);
                Report(PluginDeploymentOperation.AddedToSolution, "step", step.Step.Name);
            }
        }
        await CleanupRegistrationsAsync(changes.Registrations, progress, cancellationToken);

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

    private async Task<ResolvedRegistrations> ApplyRegistrationsAsync(RegistrationChanges registrations,
        IReadOnlyDictionary<PluginStepReference, ResolvedSdkMessage> messages,
        Guid assemblyId, Action<PluginDeploymentProgress>? progress, CancellationToken cancellationToken)
    {
        var typeIds = registrations.ExistingTypeIds.ToDictionary(pair => pair.Key, pair => pair.Value, StringComparer.Ordinal);
        var stepIds = registrations.ExistingStepIds.ToDictionary(pair => pair.Key, pair => pair.Value);
        foreach (var type in registrations.TypesToCreate)
        {
            cancellationToken.ThrowIfCancellationRequested();
            typeIds.Add(type.TypeName, await _types.CreateAsync(assemblyId, type.TypeName, type.Name, cancellationToken));
            Report(PluginDeploymentOperation.Created, "plugin type", type.TypeName);
        }

        foreach (var write in registrations.StepsToWrite)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var step = write.Desired;
            var message = messages[new PluginStepReference(write.TypeName, step)];
            var data = new PluginStepData(step.Name, typeIds[write.TypeName], message.MessageId, message.MessageFilterId, step.Stage, step.Mode, step.ExecutionOrder, step.FilterAttributes);
            if (write.ExistingId is { } stepId)
            {
                await _steps.UpdateAsync(stepId, data, cancellationToken);
                Report(PluginDeploymentOperation.Updated, "step", step.Name);
            }
            else
            {
                stepIds.Add(new PluginStepReference(write.TypeName, step), await _steps.CreateAsync(data, cancellationToken));
                Report(PluginDeploymentOperation.Created, "step", step.Name);
            }
        }

        foreach (var write in registrations.ImagesToWrite)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var image = write.Desired;
            var stepId = stepIds[new PluginStepReference(write.TypeName, write.Step)];
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

        return new ResolvedRegistrations(typeIds, stepIds);

        void Report(PluginDeploymentOperation operation, string resource, string name) =>
            progress?.Invoke(new PluginDeploymentProgress(operation, resource, name));
    }

    private async Task CleanupRegistrationsAsync(RegistrationChanges registrations,
        Action<PluginDeploymentProgress>? progress, CancellationToken cancellationToken)
    {
        foreach (var image in registrations.ImagesToDelete)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _images.DeleteAsync(image.Id, cancellationToken);
            Report(PluginDeploymentOperation.Deleted, "step image", image.Name);
        }

        foreach (var step in registrations.StepsToDelete)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _steps.DeleteAsync(step.Id, cancellationToken);
            Report(PluginDeploymentOperation.Deleted, "step", step.Name);
        }

        await EnsureNoProviderReferencesAsync(registrations.TypesToDelete.Select(type => type.Id).ToHashSet(), cancellationToken);
        foreach (var type in registrations.TypesToDelete)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if ((await _types.GetDependentStepIdsAsync(type.Id, cancellationToken)).Count > 0 ||
                (await _apis.ListLinkedToPluginTypeAsync(type.Id, cancellationToken)).Count > 0)
            {
                throw new InvalidOperationException($"Plugin type '{type.TypeName}' still has dependent SDK steps or Custom API associations; cleanup stopped.");
            }

            await _types.DeleteAsync(type.Id, cancellationToken);
            Report(PluginDeploymentOperation.Deleted, "plugin type", type.TypeName);
        }

        void Report(PluginDeploymentOperation operation, string resource, string name) =>
            progress?.Invoke(new PluginDeploymentProgress(operation, resource, name));
    }

    private static string NormalizeEntity(string entity) =>
        string.IsNullOrEmpty(entity) || string.Equals(entity, "none", StringComparison.OrdinalIgnoreCase) ? "none" : entity.ToLowerInvariant();

    private async Task<StandaloneSolutionChanges?> PrepareStandaloneSolutionAsync(LocalAssembly desired, Guid? assemblyId,
        RegistrationChanges registrations, string? solution, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(solution))
        {
            return null;
        }

        var assemblies = await _solutions.ListComponentIdsAsync(solution, IPluginAssemblyRepository.ComponentType, cancellationToken);
        var steps = await _solutions.ListComponentIdsAsync(solution, ISdkMessageProcessingStepRepository.ComponentType, cancellationToken);
        var additions = new List<StepMembershipAddition>();
        foreach (var type in desired.PluginTypes)
        {
            foreach (var step in type.Steps)
            {
                if (!registrations.ExistingStepIds.TryGetValue(new PluginStepReference(type.TypeName, step), out var id) || !steps.Contains(id))
                {
                    additions.Add(new StepMembershipAddition(type.TypeName, step));
                }
            }
        }

        return new StandaloneSolutionChanges(solution, assemblyId is null || !assemblies.Contains(assemblyId.Value), additions);
    }

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
            throw new InvalidOperationException($"Data provider '{dependent.Name}' still references a plugin type scheduled for removal; cleanup stopped.");
        }
    }

    internal Task<PackageDeploymentChanges> PreparePackageAsync(LocalPluginPackage desired, string publisherPrefix,
        string? solution = null, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(desired);
        ArgumentException.ThrowIfNullOrWhiteSpace(publisherPrefix);
        return PreparePackageCoreAsync(desired, publisherPrefix, solution, cancellationToken);
    }

    private async Task<PackageDeploymentChanges> PreparePackageCoreAsync(LocalPluginPackage desired, string publisherPrefix,
        string? solution, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        if (desired.Assemblies.Select(assembly => assembly.Name).Distinct(StringComparer.OrdinalIgnoreCase).Count() != desired.Assemblies.Count)
        {
            throw new AssemblyException("Plugin assembly names must be unique within the package.");
        }

        ValidateCustomApiDeclarations(desired.Assemblies);
        foreach (var assembly in desired.Assemblies)
        {
            RegistrationDiff.Compare(assembly.PluginTypes, new RegistrationSnapshot([], new Dictionary<Guid, IReadOnlyList<RemotePluginStep>>(),
                new Dictionary<Guid, IReadOnlyList<RemotePluginStepImage>>()));
        }

        var name = $"{publisherPrefix}_{desired.Package.Name}";
        var existing = await _packages.FindByNameAsync(name, cancellationToken);
        if (existing is not null && !string.Equals(existing.Version, desired.Package.Version, StringComparison.Ordinal))
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
        var ownedTypes = new List<RemotePluginType>();
        var existingTypeIds = new Dictionary<PluginTypeReference, Guid>();
        foreach (var assembly in desired.Assemblies)
        {
            var remote = remoteAssemblies.SingleOrDefault(candidate => string.Equals(candidate.Name, assembly.Name, StringComparison.OrdinalIgnoreCase));
            var snapshot = await LoadSnapshotAsync(remote?.Id, cancellationToken);
            ownedTypes.AddRange(snapshot.Types);
            var registrations = RegistrationDiff.Compare(assembly.PluginTypes, snapshot);
            var messages = new Dictionary<PluginStepReference, ResolvedSdkMessage>();
            foreach (var write in registrations.StepsToWrite)
            {
                var step = write.Desired;
                var message = await _messages.ResolveAsync(step.MessageName, NormalizeEntity(step.PrimaryEntityName), NormalizeEntity(step.SecondaryEntityName), cancellationToken);
                messages.Add(new PluginStepReference(write.TypeName, step), message ?? throw new UnresolvedPluginStepMessageException(step.Name, step.MessageName, step.PrimaryEntityName));
            }

            assemblies.Add(new PackageAssemblyChanges(assembly, remote, registrations, messages));
            foreach (var (typeName, id) in registrations.ExistingTypeIds)
            {
                existingTypeIds.Add(new PluginTypeReference(assembly.Name, typeName), id);
            }
        }

        var customApis = await PrepareCustomApisAsync(desired.Assemblies, ownedTypes, existingTypeIds, cancellationToken);
        var membership = await PreparePackageSolutionAsync(existing?.Id, assemblies, solution, cancellationToken);
        var providers = await PrepareProvidersAsync(desired.Assemblies, ownedTypes, existingTypeIds, solution, cancellationToken);
        if (membership is not null)
        {
            membership = membership with { Providers = providers.Solution };
        }
        return new PackageDeploymentChanges(desired, name, existing,
            existing is not null && !string.Equals(existing.PackageHash, desired.Package.PackageHash, StringComparison.OrdinalIgnoreCase),
            remoteAssemblies, assemblies, customApis, membership, providers.Changes);
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

        var released = await ReleaseRemovedTypesAsync(changes, progress, cancellationToken);
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

        cancellationToken.ThrowIfCancellationRequested();
        // Upload owns assembly/type creation. Resolve all server records before any registration writes.
        var uploadedAssemblies = await _assemblies.ListByPackageAsync(packageId, cancellationToken);
        var desiredAssemblyNames = changes.Assemblies.Select(assembly => assembly.Desired.Name).ToHashSet(StringComparer.OrdinalIgnoreCase);
        if (uploadedAssemblies.Count != desiredAssemblyNames.Count ||
            uploadedAssemblies.Any(assembly => !desiredAssemblyNames.Contains(assembly.Name)) ||
            uploadedAssemblies.Select(assembly => assembly.Name).Distinct(StringComparer.OrdinalIgnoreCase).Count() != uploadedAssemblies.Count)
        {
            throw new InvalidOperationException("Package upload produced an unexpected set of owned assemblies; no registration writes were applied.");
        }

        var resolved = new List<ResolvedPackageAssembly>();
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
            if (uploaded.Version != assembly.Desired.Version)
            {
                throw new InvalidOperationException(
                    $"Package assembly '{assembly.Desired.Name}' has version '{uploaded.Version}', not expected version '{assembly.Desired.Version}'. " +
                    "No registration writes were applied; prepare a fresh deployment.");
            }

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

            // Dataverse deletes removed types itself during upload, once their steps and API links are gone.
            resolved.Add(new ResolvedPackageAssembly(uploaded.Id, assembly, assembly.Registrations with
            {
                TypesToCreate = [],
                TypesToDelete = [],
                StepsToDelete = [.. assembly.Registrations.StepsToDelete.Where(step => !released.StepIds.Contains(step.Id))],
                ExistingTypeIds = ids
            }));
        }

        var stepIds = new Dictionary<PackagePluginStepReference, Guid>();
        foreach (var assembly in resolved)
        {
            var registrations = await ApplyRegistrationsAsync(assembly.Registrations, assembly.Changes.Messages, assembly.AssemblyId, progress, cancellationToken);
            foreach (var (step, id) in registrations.StepIds)
            {
                stepIds.Add(new PackagePluginStepReference(new PluginTypeReference(assembly.Changes.Desired.Name, step.TypeName), step.Step), id);
            }
        }

        var resolvedTypeIds = resolved.SelectMany(assembly => assembly.Registrations.ExistingTypeIds.Select(pair =>
            new KeyValuePair<PluginTypeReference, Guid>(new PluginTypeReference(assembly.Changes.Desired.Name, pair.Key), pair.Value))).ToDictionary();
        await ApplyCustomApisAsync([.. changes.CustomApis.Where(api => !released.UnlinkedApiIds.Contains(api.Id))], resolvedTypeIds, progress, cancellationToken);
        var providerIds = await ApplyProvidersAsync(changes.Providers, resolvedTypeIds, progress, cancellationToken);
        if (changes.Solution is { } solution)
        {
            await ApplyProviderMembershipAsync(solution.Providers, solution.UniqueName, providerIds, progress, cancellationToken);
            if (solution.AddPackage)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await _solutions.AddToSolutionAsync(solution.PackageComponentType, packageId, solution.UniqueName, cancellationToken);
                progress?.Invoke(new PluginDeploymentProgress(PluginDeploymentOperation.AddedToSolution, "package", changes.DataverseName));
            }

            foreach (var step in solution.Steps)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await _solutions.AddToSolutionAsync(ISdkMessageProcessingStepRepository.ComponentType,
                    stepIds[new PackagePluginStepReference(step.Handler, step.Step)], solution.UniqueName, cancellationToken);
                progress?.Invoke(new PluginDeploymentProgress(PluginDeploymentOperation.AddedToSolution, "step", step.Step.Name));
            }
        }
        foreach (var assembly in resolved)
        {
            await CleanupRegistrationsAsync(assembly.Registrations, progress, cancellationToken);
        }

        return packageId;
    }

    // Dataverse rejects a package upload that removes a plugin type with registered steps, so declared steps and
    // API links of removed types are cleared before the upload. Validation of anything else is left to Dataverse.
    private async Task<ReleasedTypeDependencies> ReleaseRemovedTypesAsync(PackageDeploymentChanges changes,
        Action<PluginDeploymentProgress>? progress, CancellationToken cancellationToken)
    {
        var removedTypeIds = changes.Assemblies.SelectMany(assembly => assembly.Registrations.TypesToDelete).Select(type => type.Id).ToHashSet();
        if (removedTypeIds.Count == 0)
        {
            return ReleasedTypeDependencies.None;
        }

        var unlinkedApiIds = new HashSet<Guid>();
        foreach (var api in changes.CustomApis.Where(api => api.PreviousHandlerId is { } previous && removedTypeIds.Contains(previous)))
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _apis.UnlinkPluginTypeAsync(api.Id, cancellationToken);
            if (api.Handler is null)
            {
                unlinkedApiIds.Add(api.Id);
                progress?.Invoke(new PluginDeploymentProgress(PluginDeploymentOperation.Updated, "Custom API", api.Name));
            }
        }

        var stepIds = new HashSet<Guid>();
        foreach (var assembly in changes.Assemblies)
        {
            var planned = assembly.Registrations.StepsToDelete.ToDictionary(step => step.Id);
            foreach (var type in assembly.Registrations.TypesToDelete)
            {
                foreach (var step in await _steps.ListByPluginTypeAsync(type.Id, cancellationToken))
                {
                    // Platform-managed steps (e.g. Custom API) go away with their link; anything else is left to Dataverse to reject.
                    if (!planned.TryGetValue(step.Id, out var plannedStep))
                    {
                        continue;
                    }

                    cancellationToken.ThrowIfCancellationRequested();
                    await _steps.DeleteAsync(step.Id, cancellationToken);
                    stepIds.Add(step.Id);
                    progress?.Invoke(new PluginDeploymentProgress(PluginDeploymentOperation.Deleted, "step", plannedStep.Name));
                }
            }
        }

        return new ReleasedTypeDependencies(stepIds, unlinkedApiIds);
    }

    private async Task<PackageSolutionChanges?> PreparePackageSolutionAsync(Guid? packageId,
        IReadOnlyList<PackageAssemblyChanges> assemblies, string? solution, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(solution))
        {
            return null;
        }

        var componentType = await _solutions.GetComponentTypeAsync(dgt.power.dataverse.PluginPackage.EntityLogicalName, cancellationToken) ??
            throw new InvalidOperationException($"The target environment does not define a solution component type for pluginpackage; membership in '{solution}' cannot be prepared.");
        var packages = await _solutions.ListComponentIdsAsync(solution, componentType, cancellationToken);
        var steps = await _solutions.ListComponentIdsAsync(solution, ISdkMessageProcessingStepRepository.ComponentType, cancellationToken);
        var additions = new List<PackageStepMembershipAddition>();
        foreach (var assembly in assemblies)
        {
            foreach (var type in assembly.Desired.PluginTypes)
            {
                foreach (var step in type.Steps)
                {
                    if (!assembly.Registrations.ExistingStepIds.TryGetValue(new PluginStepReference(type.TypeName, step), out var id) || !steps.Contains(id))
                    {
                        additions.Add(new PackageStepMembershipAddition(new PluginTypeReference(assembly.Desired.Name, type.TypeName), step));
                    }
                }
            }
        }

        return new PackageSolutionChanges(solution, componentType, packageId is null || !packages.Contains(packageId.Value), additions);
    }

    private static void ValidateCustomApiDeclarations(IReadOnlyList<LocalAssembly> desired)
    {
        var duplicate = desired.SelectMany(assembly => assembly.PluginTypes)
            .Where(type => type.CustomApi.Length > 0)
            .GroupBy(type => type.CustomApi, StringComparer.OrdinalIgnoreCase)
            .FirstOrDefault(group => group.Count() > 1);
        if (duplicate is not null)
        {
            throw new InvalidOperationException($"Custom API '{duplicate.Key}' is declared by multiple plugin types in the deployment target.");
        }
    }

    private async Task<PreparedProviders> PrepareProvidersAsync(IReadOnlyList<LocalAssembly> desired,
        IReadOnlyList<RemotePluginType> ownedTypes, Dictionary<PluginTypeReference, Guid> existingTypeIds,
        string? solutionName, CancellationToken cancellationToken)
    {
        var declarations = DataProviderRegistrationGrouper.Group(desired.SelectMany(assembly => assembly.PluginTypes).ToList());
        int? componentType = null;
        IReadOnlySet<Guid> providerMembers = new HashSet<Guid>();
        var additions = new List<ProviderMembershipAddition>();
        if (declarations.Count > 0 && !string.IsNullOrWhiteSpace(solutionName))
        {
            componentType = await _solutions.GetComponentTypeAsync(dgt.power.dataverse.EntityDataProvider.EntityLogicalName, cancellationToken) ??
                throw new InvalidOperationException($"The target environment does not define a solution component type for entitydataprovider; membership in '{solutionName}' cannot be prepared.");
            providerMembers = await _solutions.ListComponentIdsAsync(solutionName, componentType.Value, cancellationToken);
        }

        var remote = await _providers.ListAsync(cancellationToken);
        var changes = new List<DataProviderChange>();
        var declaredSources = declarations.Select(provider => provider.DataSourceLogicalName).ToHashSet(StringComparer.OrdinalIgnoreCase);
        foreach (var provider in declarations)
        {
            var candidates = remote.Where(candidate => string.Equals(candidate.DataSourceLogicalName, provider.DataSourceLogicalName, StringComparison.OrdinalIgnoreCase)).ToList();
            if (candidates.Count > 1)
            {
                throw new InvalidOperationException($"Data source '{provider.DataSourceLogicalName}' has multiple providers; reconciliation is ambiguous.");
            }

            var existing = candidates.SingleOrDefault();
            var table = await _providers.FindDataSourceAsync(provider.DataSourceLogicalName, cancellationToken) ??
                throw new InvalidDataSourceException($"Data-source table '{provider.DataSourceLogicalName}' must be provisioned before plugin push.");
            await _providers.ValidateDataSourceAsync(provider.DataSourceLogicalName, table, cancellationToken);
            if (componentType is not null && (existing is null || !providerMembers.Contains(existing.Id)))
            {
                additions.Add(new ProviderMembershipAddition(provider.DataSourceLogicalName, existing?.Id));
            }
            var handlers = new Dictionary<DataProviderOperation, PluginTypeReference?>();
            foreach (var operation in provider.Handlers.Keys)
            {
                var matches = desired.SelectMany(assembly => assembly.PluginTypes
                        .Where(type => type.DataProviders.Any(registration =>
                            string.Equals(registration.DataSourceSchemaName, provider.DataSourceSchemaName, StringComparison.OrdinalIgnoreCase) &&
                            registration.Event == operation))
                        .Select(type => new PluginTypeReference(assembly.Name, type.TypeName)))
                    .Distinct().ToList();
                if (matches.Count != 1)
                {
                    throw new AssemblyException($"Provider '{provider.ProviderName}' operation '{operation}' has competing assembly-scoped handlers.");
                }

                var handler = matches[0];
                if (!existingTypeIds.TryGetValue(handler, out var id) ||
                    existing is null || !existing.Handlers.TryGetValue(operation, out var currentId) || currentId != id)
                {
                    handlers.Add(operation, handler);
                }
            }

            if (existing is not null)
            {
                foreach (var operation in existing.Handlers.Keys.Where(operation => !provider.Handlers.ContainsKey(operation)))
                {
                    handlers.Add(operation, null);
                }
            }

            var metadataChanged = existing is null || existing.Name != provider.ProviderName ||
                provider.Description is not null && provider.Description != existing.Description;
            if (metadataChanged || handlers.Count > 0)
            {
                changes.Add(new DataProviderChange(provider, existing, handlers));
            }
        }

        var ownedIds = ownedTypes.Select(type => type.Id).ToHashSet();
        foreach (var provider in remote.Where(provider => !declaredSources.Contains(provider.DataSourceLogicalName)))
        {
            var handlers = provider.Handlers.Where(pair => ownedIds.Contains(pair.Value))
                .ToDictionary(pair => pair.Key, _ => (PluginTypeReference?)null);
            if (handlers.Count > 0)
            {
                changes.Add(new DataProviderChange(null, provider, handlers));
            }
        }

        return new PreparedProviders(changes, componentType is { } type ? new ProviderSolutionChanges(type, additions) : null);
    }

    private async Task<Dictionary<string, Guid>> ApplyProvidersAsync(IReadOnlyList<DataProviderChange> changes,
        Dictionary<PluginTypeReference, Guid> typeIds, Action<PluginDeploymentProgress>? progress, CancellationToken cancellationToken)
    {
        var ids = new Dictionary<string, Guid>(StringComparer.OrdinalIgnoreCase);
        foreach (var change in changes)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var handlers = change.Handlers.ToDictionary(pair => pair.Key,
                pair => pair.Value is { } handler ? (Guid?)typeIds[handler] : null);
            var id = await _providers.ApplyHandlersAsync(change.Existing?.Id, change.Desired, handlers, cancellationToken);
            if (change.Desired is { } desired)
            {
                ids.Add(desired.DataSourceLogicalName, id);
            }
            progress?.Invoke(new PluginDeploymentProgress(change.Existing is null ? PluginDeploymentOperation.Created : PluginDeploymentOperation.Updated,
                "data provider", change.Name));
        }
        return ids;
    }

    private async Task ApplyProviderMembershipAsync(ProviderSolutionChanges? changes, string solutionName,
        Dictionary<string, Guid> providerIds, Action<PluginDeploymentProgress>? progress, CancellationToken cancellationToken)
    {
        if (changes is null)
        {
            return;
        }
        foreach (var addition in changes.Additions)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var id = addition.ExistingProviderId ?? providerIds[addition.DataSourceLogicalName];
            await _solutions.AddToSolutionAsync(changes.ComponentType, id, solutionName, cancellationToken);
            progress?.Invoke(new PluginDeploymentProgress(PluginDeploymentOperation.AddedToSolution, "data provider", addition.DataSourceLogicalName));
        }
    }

    private async Task<IReadOnlyList<CustomApiChange>> PrepareCustomApisAsync(IReadOnlyList<LocalAssembly> desired,
        IReadOnlyList<RemotePluginType> ownedTypes, Dictionary<PluginTypeReference, Guid> existingTypeIds,
        CancellationToken cancellationToken)
    {
        var changes = new List<CustomApiChange>();
        var declaredIds = new HashSet<Guid>();
        foreach (var assembly in desired)
        {
            foreach (var type in assembly.PluginTypes.Where(type => type.CustomApi.Length > 0))
            {
                var api = await _apis.FindByUniqueNameAsync(type.CustomApi, cancellationToken) ??
                    throw new MissingCustomApiException(type.CustomApi);
                declaredIds.Add(api.Id);
                var handler = new PluginTypeReference(assembly.Name, type.TypeName);
                if (!existingTypeIds.TryGetValue(handler, out var id) || api.PluginTypeId != id)
                {
                    changes.Add(new CustomApiChange(api.Id, type.CustomApi, handler, api.PluginTypeId));
                }
            }
        }

        var omittedIds = new HashSet<Guid>();
        foreach (var type in ownedTypes)
        {
            foreach (var api in await _apis.ListAssociationsAsync(type.Id, cancellationToken))
            {
                if (!declaredIds.Contains(api.Id) && omittedIds.Add(api.Id))
                {
                    changes.Add(new CustomApiChange(api.Id, api.Name, null, type.Id));
                }
            }
        }

        return changes;
    }

    private async Task ApplyCustomApisAsync(IReadOnlyList<CustomApiChange> changes,
        Dictionary<PluginTypeReference, Guid> typeIds,
        Action<PluginDeploymentProgress>? progress, CancellationToken cancellationToken)
    {
        foreach (var change in changes)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (change.Handler is { } handler)
            {
                await _apis.LinkPluginTypeAsync(change.Id, typeIds[handler], cancellationToken);
            }
            else
            {
                await _apis.UnlinkPluginTypeAsync(change.Id, cancellationToken);
            }

            progress?.Invoke(new PluginDeploymentProgress(PluginDeploymentOperation.Updated, "Custom API", change.Name));
        }
    }
}
