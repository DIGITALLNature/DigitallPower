// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Local;
using dgt.power.plugin.Planning.Comparison;
using dgt.power.plugin.Remote;

namespace dgt.power.plugin.Planning;

/// <summary>
/// Pure decision logic for comparing local plugin assemblies/packages against a Dataverse
/// environment. Takes already-fetched remote lookups as plain input - it never talks to Dataverse
/// itself - so it can be fully unit-tested without any service dependency.
/// </summary>
public static class PluginRegistrationComparer
{
    /// <summary>
    /// Matches local plugin types (keyed by <see cref="LocalPluginType.TypeName"/>) against the
    /// types already registered on the assembly. Every local type is paired with its remote match,
    /// if any; a missing match requires creation. Its Custom API link still needs checking even
    /// when the type record itself requires no write.
    /// Remote types with no local match are returned separately for purging.
    /// </summary>
    public static PluginTypeComparisonSet ComparePluginTypes(
        IReadOnlyList<LocalPluginType> local, IReadOnlyList<RemotePluginType> remote)
    {
        ArgumentNullException.ThrowIfNull(local);
        ArgumentNullException.ThrowIfNull(remote);

        var comparisons = new List<PluginTypeComparison>();
        var matchedRemoteIds = new HashSet<Guid>();

        foreach (var localType in local)
        {
            var match = remote.FirstOrDefault(r => r.TypeName == localType.TypeName);
            if (match is null)
            {
                comparisons.Add(new PluginTypeComparison(localType, null));
                continue;
            }

            matchedRemoteIds.Add(match.Id);
            comparisons.Add(new PluginTypeComparison(localType, match));
        }

        var deletions = remote.Where(r => !matchedRemoteIds.Contains(r.Id)).ToList();
        return new PluginTypeComparisonSet(comparisons, deletions);
    }

    /// <summary>
    /// Matches local plugin steps against the steps already registered under the owning plugin
    /// type. Matching mirrors the legacy equality (message name, mode, stage and primary entity
    /// name, treating an unset/"none" primary entity as equal). The comparison flags content
    /// differences requiring an update. Remote steps with no local match are returned separately
    /// for deletion.
    /// </summary>
    public static PluginStepComparisonSet ComparePluginSteps(
        IReadOnlyList<LocalPluginStep> local, IReadOnlyList<RemotePluginStep> remote)
    {
        ArgumentNullException.ThrowIfNull(local);
        ArgumentNullException.ThrowIfNull(remote);

        var comparisons = new List<PluginStepComparison>();
        var matchedRemoteIds = new HashSet<Guid>();

        foreach (var localStep in local)
        {
            var match = remote.FirstOrDefault(r => StepKeysMatch(localStep, r));
            if (match is null)
            {
                comparisons.Add(new PluginStepComparison(localStep, null, RequiresUpdate: false));
                continue;
            }

            matchedRemoteIds.Add(match.Id);
            comparisons.Add(new PluginStepComparison(
                localStep,
                match,
                RequiresUpdate: StepContentDiffers(localStep, match)));
        }

        var deletions = remote.Where(r => !matchedRemoteIds.Contains(r.Id)).ToList();
        return new PluginStepComparisonSet(comparisons, deletions);
    }

    /// <summary>
    /// Matches local plugin step images against the images already registered under the owning
    /// step, keyed by name and image type. The comparison flags matched images whose attributes
    /// differ. Remote images with no local match are returned separately as deletions.
    /// </summary>
    public static PluginStepImageComparisonSet ComparePluginStepImages(
        IReadOnlyList<LocalPluginStepImage> local, IReadOnlyList<RemotePluginStepImage> remote)
    {
        ArgumentNullException.ThrowIfNull(local);
        ArgumentNullException.ThrowIfNull(remote);

        var comparisons = new List<PluginStepImageComparison>();
        var matchedRemoteIds = new HashSet<Guid>();

        foreach (var localImage in local)
        {
            var match = remote.FirstOrDefault(r => r.Name == localImage.Name && r.ImageType == localImage.ImageType);
            if (match is null)
            {
                comparisons.Add(new PluginStepImageComparison(localImage, null, RequiresUpdate: false));
                continue;
            }

            matchedRemoteIds.Add(match.Id);
            if (!AttributesEqual(localImage.Attributes, match.Attributes))
            {
                comparisons.Add(new PluginStepImageComparison(localImage, match, RequiresUpdate: true));
            }
        }

        var deletions = remote.Where(r => !matchedRemoteIds.Contains(r.Id)).ToList();
        return new PluginStepImageComparisonSet(comparisons, deletions);
    }

    /// <summary>
    /// Matches the plugin types of an outdated (superseded) assembly against the local types
    /// declared for its replacement, by <see cref="LocalPluginType.TypeName"/>/<see cref="RemotePluginType.TypeName"/>
    /// equality - purely a name lookup, no Dataverse access involved.
    /// </summary>
    public static IReadOnlyList<OutdatedTypeMigration> CompareOutdatedTypes(
        IReadOnlyList<RemotePluginType> outdatedTypes, IReadOnlyList<LocalPluginType> replacementTypes)
    {
        ArgumentNullException.ThrowIfNull(outdatedTypes);
        ArgumentNullException.ThrowIfNull(replacementTypes);

        var replacementTypeNames = replacementTypes.Select(t => t.TypeName).ToHashSet();
        return outdatedTypes
            .Select(old => new OutdatedTypeMigration(old.Id, old.TypeName, replacementTypeNames.Contains(old.TypeName)))
            .ToList();
    }

    private static bool StepKeysMatch(LocalPluginStep local, RemotePluginStep remote)
    {
        if (!string.Equals(local.MessageName, remote.MessageName, StringComparison.OrdinalIgnoreCase) ||
            local.Mode != remote.Mode ||
            local.Stage != remote.Stage)
        {
            return false;
        }

        return string.Equals(
                   NormalizeEntityName(local.PrimaryEntityName), NormalizeEntityName(remote.PrimaryEntityName),
                   StringComparison.OrdinalIgnoreCase)
            && string.Equals(
                   NormalizeEntityName(local.SecondaryEntityName), NormalizeEntityName(remote.SecondaryEntityName),
                   StringComparison.OrdinalIgnoreCase);
    }

    private static bool StepContentDiffers(LocalPluginStep local, RemotePluginStep remote) =>
        remote.Name != local.Name
        || remote.ExecutionOrder != local.ExecutionOrder
        || remote.Configuration != local.Configuration
        || !AttributesEqual(local.FilterAttributes, remote.FilterAttributes);

    private static string NormalizeEntityName(string entityName) =>
        string.IsNullOrEmpty(entityName) ? "none" : entityName;

    private static bool AttributesEqual(IReadOnlyList<string>? left, IReadOnlyList<string>? right)
    {
        left = left is { Count: > 0 } ? left : null;
        right = right is { Count: > 0 } ? right : null;

        if (left is null || right is null)
        {
            return left is null && right is null;
        }

        return left.OrderBy(a => a, StringComparer.Ordinal).SequenceEqual(right.OrderBy(a => a, StringComparer.Ordinal));
    }

}
