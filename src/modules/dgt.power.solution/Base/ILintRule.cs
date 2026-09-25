// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.solution.Base;

public interface ILintRule
{
    string Id { get; }

    LintSeverity DefaultSeverity { get; }

    bool IsEnabledByDefault { get; }

    // cancellationToken is unused by every current rule (they only iterate the already-fetched
    // LintContext, which itself is built asynchronously) but is kept for interface consistency
    // in case a future rule needs its own Dataverse I/O.
    // ReSharper disable once UnusedParameter.Global
    Task<IReadOnlyList<LintFinding>> EvaluateAsync(LintContext context, LintRuleConfigEntry? ruleConfig, CancellationToken cancellationToken);
}
