// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Deployment.Local;
using dgt.power.plugin.Exceptions;
using dgt.power.plugin.Models;

namespace dgt.power.plugin.Deployment.Reconciliation;

internal static class RegistrationDiff
{
    internal static RegistrationChanges Compare(IReadOnlyList<LocalPluginType> desired, RegistrationSnapshot snapshot, RegistrationSnapshot? superseded = null)
    {
        ArgumentNullException.ThrowIfNull(desired);
        ArgumentNullException.ThrowIfNull(snapshot);
        AssemblyReflectionReader.EnsureAllPluginTypesDeclared(desired);
        if (desired.Select(type => type.TypeName).Distinct(StringComparer.Ordinal).Count() != desired.Count ||
            snapshot.Types.Select(type => type.TypeName).Distinct(StringComparer.Ordinal).Count() != snapshot.Types.Count)
        {
            throw new AssemblyException("Plugin type names must be unique within the assembly being reconciled.");
        }
        if (superseded is not null && superseded.Types.Select(type => type.TypeName).Distinct(StringComparer.Ordinal).Count() != superseded.Types.Count)
        {
            throw new AssemblyException("Plugin type names must be unique within the replacement source assembly.");
        }

        var createTypes = new List<LocalPluginType>();
        var stepWrites = new List<StepWrite>();
        var deleteSteps = new List<RemotePluginStep>();
        var imageWrites = new List<ImageWrite>();
        var deleteImages = new List<RemotePluginStepImage>();
        var typeIds = new Dictionary<string, Guid>(StringComparer.Ordinal);
        var stepIds = new Dictionary<(string TypeName, LocalPluginStep Step), Guid>();
        var remoteTypes = snapshot.Types.ToDictionary(type => type.TypeName, StringComparer.Ordinal);
        var oldTypes = (superseded?.Types ?? []).ToDictionary(type => type.TypeName, StringComparer.Ordinal);

        foreach (var type in desired)
        {
            IReadOnlyList<RemotePluginStep> remoteSteps = [];
            if (remoteTypes.TryGetValue(type.TypeName, out var existingType))
            {
                typeIds.Add(type.TypeName, existingType.Id);
                remoteSteps = snapshot.StepsByType[existingType.Id].Where(step => LocalPluginStepValidator.IsDeclarativeStage(step.Stage)).ToList();
            }
            else
            {
                createTypes.Add(type);
            }

            var destinationStepIds = remoteSteps.Select(step => step.Id).ToHashSet();
            if (oldTypes.TryGetValue(type.TypeName, out var oldType))
            {
                remoteSteps = remoteSteps.Concat(superseded!.StepsByType[oldType.Id].Where(step => LocalPluginStepValidator.IsDeclarativeStage(step.Stage))).ToList();
            }

            var matches = PluginStepMatcher.Match(type, remoteSteps);
            foreach (var step in type.Steps)
            {
                var existing = matches.GetValueOrDefault(step);
                if (existing is not null)
                {
                    stepIds.Add((type.TypeName, step), existing.Id);
                }

                var requiresReassignment = existing is not null && !destinationStepIds.Contains(existing.Id);
                var contentChanged = existing is not null && (existing.Name != step.Name ||
                    (existing.ExecutionOrder ?? 1) != (step.ExecutionOrder ?? 1) || !AttributesEqual(existing.FilterAttributes, step.FilterAttributes));
                if (existing is null || requiresReassignment || contentChanged)
                {
                    stepWrites.Add(new StepWrite(type.TypeName, step, existing?.Id));
                }

                IReadOnlyList<RemotePluginStepImage> images = [];
                if (existing is not null)
                {
                    images = destinationStepIds.Contains(existing.Id) ? snapshot.ImagesByStep[existing.Id] : superseded!.ImagesByStep[existing.Id];
                }
                CompareImages(type.TypeName, step, images, imageWrites, deleteImages);
            }

            var matchedIds = matches.Values.Select(step => step.Id).ToHashSet();
            deleteSteps.AddRange(remoteSteps.Where(step => !matchedIds.Contains(step.Id)));
        }

        var desiredNames = desired.Select(type => type.TypeName).ToHashSet(StringComparer.Ordinal);
        var deleteTypes = snapshot.Types.Where(type => !desiredNames.Contains(type.TypeName)).ToList();
        deleteTypes.AddRange(superseded?.Types ?? []);
        foreach (var removedType in deleteTypes.Where(type => !desiredNames.Contains(type.TypeName)))
        {
            var owner = snapshot.StepsByType.ContainsKey(removedType.Id) ? snapshot : superseded!;
            deleteSteps.AddRange(owner.StepsByType[removedType.Id].Where(step => LocalPluginStepValidator.IsDeclarativeStage(step.Stage)));
        }
        return new RegistrationChanges(createTypes, deleteTypes, stepWrites, deleteSteps, imageWrites, deleteImages, typeIds, stepIds);
    }

    private static void CompareImages(string typeName, LocalPluginStep step, IReadOnlyList<RemotePluginStepImage> remote,
        List<ImageWrite> writes, List<RemotePluginStepImage> deletions)
    {
        var declaredKeys = new HashSet<(string Name, int Type)>();
        var remoteByKey = remote.GroupBy(image => (image.Name, image.ImageType)).ToDictionary(group => group.Key, group => group.ToList());
        var retainedIds = new HashSet<Guid>();
        foreach (var image in step.Images)
        {
            var key = (image.Name, image.ImageType);
            if (!declaredKeys.Add(key))
            {
                throw new InvalidPluginStepException($"Step '{step.Name}' declares multiple images named '{image.Name}' with image type '{image.ImageType}'.");
            }

            remoteByKey.TryGetValue(key, out var candidates);
            if (candidates is { Count: > 1 })
            {
                throw new InvalidPluginStepException($"Step '{step.Name}' matches multiple existing images named '{image.Name}' with image type '{image.ImageType}'.");
            }

            var existing = candidates?.SingleOrDefault();
            if (existing is not null)
            {
                retainedIds.Add(existing.Id);
            }

            if (existing is null || !AttributesEqual(existing.Attributes, image.Attributes))
            {
                writes.Add(new ImageWrite(typeName, step, image, existing?.Id));
            }
        }

        deletions.AddRange(remote.Where(image => !retainedIds.Contains(image.Id)));
    }

    private static bool AttributesEqual(IReadOnlyList<string>? left, IReadOnlyList<string>? right) =>
        (left ?? []).Order(StringComparer.Ordinal).SequenceEqual((right ?? []).Order(StringComparer.Ordinal));
}
