// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

namespace dgt.power.solution.Base;

public enum LintSeverity
{
    // Never referenced by name in C# - reachable only via user config ("severity": "Info") per
    // schemas/solution/lint/schema.json; removing it would silently break that documented option.
    // ReSharper disable once UnusedMember.Global
    Info,
    Warning,
    Error
}
