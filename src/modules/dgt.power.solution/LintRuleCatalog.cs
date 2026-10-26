// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using dgt.power.solution.Base;
using dgt.power.solution.Rules;

namespace dgt.power.solution;

public static class LintRuleCatalog
{
    public static IReadOnlyList<ILintRule> All { get; } =
    [
        new UnmanagedFieldNamingRule(),
        new TableRootComponentBehaviorRule(),
        new JScriptSourceMapRule()
    ];
}
