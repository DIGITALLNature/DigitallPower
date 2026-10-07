// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Deployment.Local;
using dgt.power.plugin.Exceptions;
using dgt.power.plugin.Models;

namespace dgt.power.plugin.Deployment.Reconciliation;

internal static class PluginStepMatcher
{
    // Candidates must be scoped to this plugin type, including any superseded assembly.
    internal static IReadOnlyDictionary<LocalPluginStep, RemotePluginStep> Match(
        LocalPluginType desired, IReadOnlyList<RemotePluginStep> candidates)
    {
        ArgumentNullException.ThrowIfNull(desired);
        ArgumentNullException.ThrowIfNull(candidates);
        LocalPluginStepValidator.Validate(desired);

        var declarations = new Dictionary<MatchKey, LocalPluginStep>();
        foreach (var step in desired.Steps)
        {
            if (!declarations.TryAdd(Key(step.MessageName, step.Stage, step.Mode, step.PrimaryEntityName, step.SecondaryEntityName), step))
            {
                throw new InvalidPluginStepException(
                    $"Plugin type '{desired.TypeName}' declares multiple steps with the same message, stage, mode, and entities. " +
                    $"Step '{step.Name}' cannot be distinguished by execution order, name, filtering attributes, or images.");
            }
        }

        var remoteByKey = candidates.GroupBy(step => Key(step.MessageName, step.Stage, step.Mode, step.PrimaryEntityName, step.SecondaryEntityName))
            .ToDictionary(group => group.Key, group => group.ToList());
        var matches = new Dictionary<LocalPluginStep, RemotePluginStep>();
        foreach (var (key, step) in declarations)
        {
            if (!remoteByKey.TryGetValue(key, out var existing))
            {
                continue;
            }

            if (existing.Count > 1)
            {
                throw new InvalidPluginStepException(
                    $"Step '{step.Name}' on plugin type '{desired.TypeName}' matches multiple existing steps: " +
                    $"{string.Join(", ", existing.Select(candidate => $"{candidate.Name} ({candidate.Id})"))}. " +
                    "Resolve the ambiguity before deployment; existing configuration cannot be assigned safely.");
            }

            matches.Add(step, existing[0]);
        }

        return matches;
    }

    private static MatchKey Key(string message, int stage, int mode, string primaryEntity, string secondaryEntity) =>
        new(message.ToUpperInvariant(), stage, mode, NormalizeEntity(primaryEntity), NormalizeEntity(secondaryEntity));

    private static string NormalizeEntity(string entity) =>
        string.IsNullOrEmpty(entity) ? "NONE" : entity.ToUpperInvariant();

    private readonly record struct MatchKey(string Message, int Stage, int Mode, string PrimaryEntity, string SecondaryEntity);
}
