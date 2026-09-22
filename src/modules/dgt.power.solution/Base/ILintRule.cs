// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.solution.Base;

public interface ILintRule
{
    string Id { get; }

    string Description { get; }

    LintSeverity DefaultSeverity { get; }

    bool IsEnabledByDefault { get; }

    Task<IReadOnlyList<LintFinding>> EvaluateAsync(LintContext context, LintRuleConfigEntry? ruleConfig, CancellationToken cancellationToken);
}
