// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.plugin.Deployment.Batch;

/// <summary>
/// Detects targets in one batch that would deploy the same Dataverse identity. Such targets would
/// overwrite each other and the result would depend on order, so the whole batch is rejected.
/// Works on local data only, so callers run it before connecting to Dataverse.
/// </summary>
internal static class BatchPreflight
{
    internal static IReadOnlyList<string> Validate(IReadOnlyList<LocalDeploymentTarget> targets, BatchOptions options)
    {
        ArgumentNullException.ThrowIfNull(targets);
        ArgumentNullException.ThrowIfNull(options);
        var errors = new List<string>();
        if (string.IsNullOrEmpty(options.PublisherPrefix) && targets.Any(target => target.Package is not null))
        {
            errors.Add("--publisher-prefix is required when processing a plugin package (.nupkg)");
        }

        errors.AddRange(FindConflicts(targets));
        return errors;
    }

    internal static IReadOnlyList<string> FindConflicts(IReadOnlyList<LocalDeploymentTarget> targets)
    {
        ArgumentNullException.ThrowIfNull(targets);
        var conflicts = new List<string>();
        Collect(conflicts, "Assembly", targets, target => target.Assemblies.Select(assembly => assembly.Name));
        Collect(conflicts, "Package", targets, target => target.Package is null ? [] : [target.Package.Package.Name]);
        Collect(conflicts, "Data provider", targets,
            target => target.Assemblies.SelectMany(assembly => assembly.PluginTypes).SelectMany(type => type.DataProviders).Select(provider => provider.DataSourceSchemaName));
        Collect(conflicts, "Custom API", targets,
            target => target.Assemblies.SelectMany(assembly => assembly.PluginTypes).Select(type => type.CustomApi).Where(name => !string.IsNullOrWhiteSpace(name)));
        return conflicts;
    }

    private static void Collect(List<string> conflicts, string kind, IReadOnlyList<LocalDeploymentTarget> targets, Func<LocalDeploymentTarget, IEnumerable<string>> identities)
    {
        var owners = targets.SelectMany(target => identities(target).Distinct(StringComparer.OrdinalIgnoreCase).Select(identity => new DeclaredIdentity(identity, target.FilePath)))
            .GroupBy(item => item.Identity, StringComparer.OrdinalIgnoreCase)
            .Where(group => group.Select(item => item.FilePath).Distinct(StringComparer.OrdinalIgnoreCase).Count() > 1)
            .OrderBy(group => group.Key, StringComparer.OrdinalIgnoreCase);
        foreach (var group in owners)
        {
            var paths = string.Join(", ", group.Select(item => $"'{item.FilePath}'").Order(StringComparer.OrdinalIgnoreCase));
            conflicts.Add($"{kind} '{group.Key}' is declared by multiple targets: {paths}.");
        }
    }
}
