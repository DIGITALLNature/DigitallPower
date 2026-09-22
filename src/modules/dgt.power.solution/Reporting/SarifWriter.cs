// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.solution.Base;
using Microsoft.CodeAnalysis.Sarif;

namespace dgt.power.solution.Reporting;

/// <summary>Writes/reads lint findings as a SARIF 2.1.0 log (via Sarif.Sdk) - used for both the --sarif-output export and the --baseline file.</summary>
public static class SarifWriter
{
    private const string FingerprintKey = "dgtpLintKey";
    private const string ToolName = "dgtp-lint";
    private static readonly Uri ToolInformationUri = new("https://digitallnature.github.io/customizing/naming-conventions/");

    public static async Task WriteAsync(string path, IReadOnlyList<LintFinding> findings, IReadOnlySet<string>? suppressedKeys, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(path);
        ArgumentNullException.ThrowIfNull(findings);

        var fullPath = Path.GetFullPath(path);
        var directory = Path.GetDirectoryName(fullPath);
        if (!string.IsNullOrEmpty(directory))
        {
            Directory.CreateDirectory(directory);
        }

        var log = BuildLog(findings, suppressedKeys);
        await Task.Run(() => log.Save(fullPath), cancellationToken);
    }

    /// <summary>Reads the <see cref="LintFinding.BaselineKey"/> fingerprints from a previously written baseline SARIF file.</summary>
    public static IReadOnlySet<string> ReadBaselineKeys(string path)
    {
        ArgumentNullException.ThrowIfNull(path);
        if (!File.Exists(path))
        {
            return new HashSet<string>(StringComparer.Ordinal);
        }

        var log = SarifLog.Load(path);
        var keys = (log.Runs ?? [])
            .SelectMany(static run => run.Results ?? [])
            .Select(static result => result.PartialFingerprints != null && result.PartialFingerprints.TryGetValue(FingerprintKey, out var key) ? key : null)
            .Where(static key => key != null)
            .Select(static key => key!);

        return new HashSet<string>(keys, StringComparer.Ordinal);
    }

    private static SarifLog BuildLog(IReadOnlyList<LintFinding> findings, IReadOnlySet<string>? suppressedKeys)
    {
        var driver = new ToolComponent
        {
            Name = ToolName,
            InformationUri = ToolInformationUri,
            Rules = findings
                .Select(static finding => finding.RuleId)
                .Distinct(StringComparer.Ordinal)
                .OrderBy(static id => id, StringComparer.Ordinal)
                .Select(static id => new ReportingDescriptor { Id = id })
                .ToList()
        };

        var run = new Run { Tool = new Tool { Driver = driver }, Results = [] };

        foreach (var finding in findings)
        {
            var result = new Result
            {
                RuleId = finding.RuleId,
                Level = ToSarifLevel(finding.Severity),
                Message = new Message { Text = finding.Message },
                Locations = [new Location { LogicalLocations = [new LogicalLocation { FullyQualifiedName = BuildFullyQualifiedName(finding) }] }],
                PartialFingerprints = new Dictionary<string, string> { [FingerprintKey] = finding.BaselineKey }
            };

            if (suppressedKeys != null && suppressedKeys.Contains(finding.BaselineKey))
            {
                result.Suppressions = [new Suppression { Kind = SuppressionKind.External, Justification = "Present in the lint baseline" }];
            }

            run.Results.Add(result);
        }

        return new SarifLog { Runs = [run] };
    }

    private static string BuildFullyQualifiedName(LintFinding finding) =>
        string.Join('/', new[] { finding.SolutionUniqueName, finding.ComponentType, finding.ComponentLogicalName ?? finding.ComponentId?.ToString() }
            .Where(static part => !string.IsNullOrWhiteSpace(part)));

    private static FailureLevel ToSarifLevel(LintSeverity severity) => severity switch
    {
        LintSeverity.Error => FailureLevel.Error,
        LintSeverity.Warning => FailureLevel.Warning,
        _ => FailureLevel.Note
    };
}

