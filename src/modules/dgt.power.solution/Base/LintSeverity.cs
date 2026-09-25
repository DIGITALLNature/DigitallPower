// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

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
