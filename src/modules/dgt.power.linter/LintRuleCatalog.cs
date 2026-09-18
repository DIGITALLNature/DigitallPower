// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.linter.Base;
using dgt.power.linter.Rules;

namespace dgt.power.linter;

public static class LintRuleCatalog
{
    public static IReadOnlyList<ILintRule> All { get; } =
    [
        new UnmanagedFieldNamingRule(),
        new TableRootComponentBehaviorRule()
    ];
}
