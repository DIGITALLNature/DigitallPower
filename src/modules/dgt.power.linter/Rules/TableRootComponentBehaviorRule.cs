// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.linter.Base;

namespace dgt.power.linter.Rules;

/// <summary>
/// Enforces the table-level counterpart to a managed/unmanaged split: an unmanaged (first-party)
/// table must always be added completely (RootComponentBehavior.IncludeSubcomponents), while a
/// managed (e.g. ISV-owned) table must never be - only its actual delta may be listed explicitly
/// (DoNotIncludeSubcomponents) or referenced as a shell (IncludeAsShellOnly).
/// </summary>
public sealed class TableRootComponentBehaviorRule : ILintRule
{
    public string Id => "completeness.table-root-component-behavior";
    public string Description => "Unmanaged tables must be added completely; managed tables must never be added completely.";
    public LintSeverity DefaultSeverity => LintSeverity.Error;
    public bool IsEnabledByDefault => true;

    public Task<IReadOnlyList<LintFinding>> EvaluateAsync(LintContext context, LintRuleConfigEntry? ruleConfig, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(context);

        var severity = ruleConfig?.Severity ?? DefaultSeverity;
        var findings = new List<LintFinding>();

        foreach (var membership in context.EntityMemberships.Values)
        {
            var isComplete = membership.RootComponentBehavior == SolutionComponent.Options.RootComponentBehavior.IncludeSubcomponents;
            if (membership.IsTableManaged != isComplete)
            {
                continue;
            }

            var message = membership.IsTableManaged
                ? $"Managed table '{membership.EntityLogicalName}' must not be included completely (IncludeSubcomponents) - only its actual delta may be listed."
                : $"Unmanaged table '{membership.EntityLogicalName}' must be included completely (IncludeSubcomponents), not partially.";

            findings.Add(new LintFinding(
                Id,
                severity,
                message,
                membership.SolutionUniqueName,
                "Entity",
                membership.EntityLogicalName,
                membership.EntityComponent.ObjectId,
                new Dictionary<string, object?>
                {
                    ["rootComponentBehavior"] = membership.RootComponentBehavior,
                    ["isTableManaged"] = membership.IsTableManaged
                }));
        }

        return Task.FromResult<IReadOnlyList<LintFinding>>(findings);
    }
}
