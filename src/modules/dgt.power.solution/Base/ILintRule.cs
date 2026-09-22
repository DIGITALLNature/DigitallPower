// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.solution.Base;

public interface ILintRule
{
    string Id { get; }

    LintSeverity DefaultSeverity { get; }

    bool IsEnabledByDefault { get; }

    // cancellationToken is unused by every current rule (they only iterate the already-fetched
    // LintContext) but is kept for interface consistency ahead of the planned IOrganizationServiceAsync2
    // migration (see todo.md, dgt.power.solution row).
    // ReSharper disable once UnusedParameter.Global
    Task<IReadOnlyList<LintFinding>> EvaluateAsync(LintContext context, LintRuleConfigEntry? ruleConfig, CancellationToken cancellationToken);
}
