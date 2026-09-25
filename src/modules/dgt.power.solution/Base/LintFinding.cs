// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.solution.Base;

public record LintFinding(
    string RuleId,
    LintSeverity Severity,
    string Message,
    string? SolutionUniqueName,
    string? ComponentType,
    string? ComponentLogicalName,
    Guid? ComponentId,
    Dictionary<string, object?>? Properties = null)
{
    /// <summary>Stable identity used to match a finding against a baseline across runs (RuleId + component + solution).</summary>
    public string BaselineKey => string.Join('|', RuleId, SolutionUniqueName, ComponentType, ComponentLogicalName, ComponentId);
}

