// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

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
