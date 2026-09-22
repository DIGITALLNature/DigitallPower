// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Local;
using dgt.power.plugin.Remote;

namespace dgt.power.plugin.Planning;

/// <summary>
/// Pure decision logic for reconciling local plugin assemblies/packages against a Dataverse
/// environment. Takes already-fetched remote lookups as plain input - it never talks to Dataverse
/// itself - so it can be fully unit-tested without any service dependency.
/// </summary>
public static class PluginPushPlanner
{
    /// <summary>
    /// Decides the <see cref="AssemblyAction"/> for a local assembly given the (already looked up)
    /// remote assembly with the same name, if any.
    /// </summary>
    /// <param name="local">The parsed local assembly.</param>
    /// <param name="remote">
    /// The existing remote assembly registered under the same name, or <see langword="null"/> when
    /// none exists yet.
    /// </param>
    public static AssemblyPlan PlanAssembly(LocalAssembly local, RemoteAssembly? remote)
    {
        ArgumentNullException.ThrowIfNull(local);

        if (remote is null)
        {
            return new AssemblyPlan(local, AssemblyAction.Create, null);
        }

        if (remote.PackageId is not null)
        {
            return new AssemblyPlan(local, AssemblyAction.OwnedByPackage, remote);
        }

        var action = IsSameMajorMinor(local.Version, remote.Version) ? AssemblyAction.Update : AssemblyAction.Upgrade;
        return new AssemblyPlan(local, action, remote);
    }

    /// <summary>
    /// Decides the <see cref="PackageAction"/> for a local package given the (already looked up)
    /// remote package with the same name, if any. Package version is never compared - Dataverse
    /// plugin packages cannot have their version changed after creation, so an existing package is
    /// always updated in place regardless of the local version.
    /// </summary>
    public static PackagePlan PlanPackage(LocalPackage local, RemotePackage? remote)
    {
        ArgumentNullException.ThrowIfNull(local);

        return remote is null
            ? new PackagePlan(local, PackageAction.Create, null)
            : new PackagePlan(local, PackageAction.Update, remote);
    }

    /// <summary>
    /// Matches local plugin types (keyed by <see cref="LocalPluginType.TypeName"/>) against the
    /// types already registered on the assembly. Every local type gets either a <see cref="PluginTypeAction.Create"/>
    /// plan (no remote match) or a <see cref="PluginTypeAction.Unchanged"/> plan (remote match -
    /// its Custom API link still needs checking, even though the type record itself never changes).
    /// Remote types with no local match are returned separately for purging.
    /// </summary>
    public static PluginTypeReconciliationPlan PlanPluginTypes(
        IReadOnlyList<LocalPluginType> local, IReadOnlyList<RemotePluginType> remote)
    {
        ArgumentNullException.ThrowIfNull(local);
        ArgumentNullException.ThrowIfNull(remote);

        var plans = new List<PluginTypePlan>();
        var matchedRemoteIds = new HashSet<Guid>();

        foreach (var localType in local)
        {
            var match = remote.FirstOrDefault(r => r.TypeName == localType.TypeName);
            if (match is null)
            {
                plans.Add(new PluginTypePlan(localType, PluginTypeAction.Create, null));
                continue;
            }

            matchedRemoteIds.Add(match.Id);
            plans.Add(new PluginTypePlan(localType, PluginTypeAction.Unchanged, match));
        }

        var purge = remote.Where(r => !matchedRemoteIds.Contains(r.Id)).ToList();
        return new PluginTypeReconciliationPlan(plans, purge);
    }

    /// <summary>
    /// Matches local plugin steps against the steps already registered under the owning plugin
    /// type. Matching mirrors the legacy equality (message name, mode, stage and primary entity
    /// name, treating an unset/"none" primary entity as equal). A matched step is only planned for
    /// <see cref="PluginStepAction.Update"/> when its content actually differs; otherwise it is left
    /// untouched. Remote steps with no local match are returned separately for purging.
    /// </summary>
    public static PluginStepReconciliationPlan PlanPluginSteps(
        IReadOnlyList<LocalPluginStep> local, IReadOnlyList<RemotePluginStep> remote)
    {
        ArgumentNullException.ThrowIfNull(local);
        ArgumentNullException.ThrowIfNull(remote);

        var plans = new List<PluginStepPlan>();
        var matchedRemoteIds = new HashSet<Guid>();

        foreach (var localStep in local)
        {
            var match = remote.FirstOrDefault(r => StepKeysMatch(localStep, r));
            if (match is null)
            {
                plans.Add(new PluginStepPlan(localStep, PluginStepAction.Create, null));
                continue;
            }

            matchedRemoteIds.Add(match.Id);
            var action = StepContentDiffers(localStep, match) ? PluginStepAction.Update : PluginStepAction.Unchanged;
            plans.Add(new PluginStepPlan(localStep, action, match));
        }

        var purge = remote.Where(r => !matchedRemoteIds.Contains(r.Id)).ToList();
        return new PluginStepReconciliationPlan(plans, purge);
    }

    /// <summary>
    /// Matches local plugin step images against the images already registered under the owning
    /// step, keyed by name and image type. A matched image is only planned for
    /// <see cref="PluginStepImageAction.Update"/> when its attributes actually differ. Remote
    /// images with no local match are returned separately for purging.
    /// </summary>
    public static PluginStepImageReconciliationPlan PlanPluginStepImages(
        IReadOnlyList<LocalPluginStepImage> local, IReadOnlyList<RemotePluginStepImage> remote)
    {
        ArgumentNullException.ThrowIfNull(local);
        ArgumentNullException.ThrowIfNull(remote);

        var plans = new List<PluginStepImagePlan>();
        var matchedRemoteIds = new HashSet<Guid>();

        foreach (var localImage in local)
        {
            var match = remote.FirstOrDefault(r => r.Name == localImage.Name && r.ImageType == localImage.ImageType);
            if (match is null)
            {
                plans.Add(new PluginStepImagePlan(localImage, PluginStepImageAction.Create, null));
                continue;
            }

            matchedRemoteIds.Add(match.Id);
            if (!AttributesEqual(localImage.Attributes, match.Attributes))
            {
                plans.Add(new PluginStepImagePlan(localImage, PluginStepImageAction.Update, match));
            }
        }

        var purge = remote.Where(r => !matchedRemoteIds.Contains(r.Id)).ToList();
        return new PluginStepImageReconciliationPlan(plans, purge);
    }

    /// <summary>
    /// Matches the plugin types of an outdated (superseded) assembly against the local types
    /// declared for its replacement, by <see cref="LocalPluginType.TypeName"/>/<see cref="RemotePluginType.TypeName"/>
    /// equality - purely a name lookup, no Dataverse access involved.
    /// </summary>
    public static IReadOnlyList<OutdatedTypeMigration> PlanOutdatedTypeMigration(
        IReadOnlyList<RemotePluginType> outdatedTypes, IReadOnlyList<LocalPluginType> replacementTypes)
    {
        ArgumentNullException.ThrowIfNull(outdatedTypes);
        ArgumentNullException.ThrowIfNull(replacementTypes);

        var replacementTypeNames = replacementTypes.Select(t => t.TypeName).ToHashSet();
        return outdatedTypes
            .Select(old => new OutdatedTypeMigration(old.Id, old.TypeName, replacementTypeNames.Contains(old.TypeName)))
            .ToList();
    }

    private static bool IsSameMajorMinor(Version local, Version remote) =>
        local.Major == remote.Major && local.Minor == remote.Minor;

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
