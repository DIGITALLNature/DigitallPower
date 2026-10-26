// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

namespace dgt.power.solution.Base;

public interface ILintRule
{
    string Id { get; }

    // ReSharper disable once UnusedMemberInSuper.Global -- public rule metadata exposed through LintRuleCatalog.All
    LintSeverity DefaultSeverity { get; }

    bool IsEnabledByDefault { get; }

    // cancellationToken is unused by every current rule (they only iterate the already-fetched
    // LintContext, which itself is built asynchronously) but is kept for interface consistency
    // in case a future rule needs its own Dataverse I/O.
    // ReSharper disable once UnusedParameter.Global
    Task<IReadOnlyList<LintFinding>> EvaluateAsync(LintContext context, LintRuleConfigEntry? ruleConfig, CancellationToken cancellationToken);
}
