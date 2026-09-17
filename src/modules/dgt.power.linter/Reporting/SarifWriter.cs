// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text.Json;
using System.Text.Json.Serialization;
using dgt.power.linter.Base;

namespace dgt.power.linter.Reporting;

/// <summary>Writes/reads lint findings as a SARIF 2.1.0 log - used for both the --sarif-output export and the --baseline file.</summary>
public static class SarifWriter
{
    private const string FingerprintKey = "dgtpLintKey";

    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };

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
        await File.WriteAllTextAsync(fullPath, JsonSerializer.Serialize(log, SerializerOptions), cancellationToken);
    }

    /// <summary>Reads the <see cref="LintFinding.BaselineKey"/> fingerprints from a previously written baseline SARIF file.</summary>
    public static IReadOnlySet<string> ReadBaselineKeys(string path)
    {
        ArgumentNullException.ThrowIfNull(path);
        if (!File.Exists(path))
        {
            return new HashSet<string>(StringComparer.Ordinal);
        }

        var log = JsonSerializer.Deserialize<SarifLog>(File.ReadAllText(path), SerializerOptions);
        var keys = (log?.Runs ?? [])
            .SelectMany(static run => run.Results)
            .Select(static result => result.PartialFingerprints != null && result.PartialFingerprints.TryGetValue(FingerprintKey, out var key) ? key : null)
            .Where(static key => key != null)
            .Select(static key => key!);

        return new HashSet<string>(keys, StringComparer.Ordinal);
    }

    private static SarifLog BuildLog(IReadOnlyList<LintFinding> findings, IReadOnlySet<string>? suppressedKeys)
    {
        var run = new SarifRun();
        run.Tool.Driver.Rules.AddRange(findings
            .Select(static finding => finding.RuleId)
            .Distinct(StringComparer.Ordinal)
            .OrderBy(static id => id, StringComparer.Ordinal)
            .Select(static id => new SarifRule { Id = id }));

        foreach (var finding in findings)
        {
            var result = new SarifResult
            {
                RuleId = finding.RuleId,
                Level = ToSarifLevel(finding.Severity),
                Message = new SarifMessage { Text = finding.Message },
                Locations = [new SarifLocation { LogicalLocations = [new SarifLogicalLocation { FullyQualifiedName = BuildFullyQualifiedName(finding) }] }],
                PartialFingerprints = new Dictionary<string, string> { [FingerprintKey] = finding.BaselineKey }
            };

            if (suppressedKeys != null && suppressedKeys.Contains(finding.BaselineKey))
            {
                result.Suppressions = [new SarifSuppression { Justification = "Present in the lint baseline" }];
            }

            run.Results.Add(result);
        }

        var log = new SarifLog();
        log.Runs.Add(run);
        return log;
    }

    private static string BuildFullyQualifiedName(LintFinding finding) =>
        string.Join('/', new[] { finding.SolutionUniqueName, finding.ComponentType, finding.ComponentLogicalName ?? finding.ComponentId?.ToString() }
            .Where(static part => !string.IsNullOrWhiteSpace(part)));

    private static string ToSarifLevel(LintSeverity severity) => severity switch
    {
        LintSeverity.Error => "error",
        LintSeverity.Warning => "warning",
        _ => "note"
    };

    private sealed class SarifLog
    {
        [JsonPropertyName("$schema")]
        public string Schema { get; init; } = "https://raw.githubusercontent.com/oasis-tcs/sarif-spec/master/Schemata/sarif-schema-2.1.0.json";

        public string Version { get; init; } = "2.1.0";

        public List<SarifRun> Runs { get; init; } = [];
    }

    private sealed class SarifRun
    {
        public SarifTool Tool { get; init; } = new();

        public List<SarifResult> Results { get; init; } = [];
    }

    private sealed class SarifTool
    {
        public SarifDriver Driver { get; init; } = new();
    }

    private sealed class SarifDriver
    {
        public string Name { get; init; } = "dgtp-lint";

        public string InformationUri { get; init; } = "https://digitallnature.github.io/customizing/naming-conventions/";

        public List<SarifRule> Rules { get; init; } = [];
    }

    private sealed class SarifRule
    {
        public string Id { get; init; } = string.Empty;
    }

    private sealed class SarifResult
    {
        public string RuleId { get; init; } = string.Empty;

        public string Level { get; init; } = "warning";

        public SarifMessage Message { get; init; } = new();

        public List<SarifLocation>? Locations { get; init; }

        public Dictionary<string, string>? PartialFingerprints { get; init; }

        public List<SarifSuppression>? Suppressions { get; set; }
    }

    private sealed class SarifMessage
    {
        public string Text { get; init; } = string.Empty;
    }

    private sealed class SarifLocation
    {
        public List<SarifLogicalLocation>? LogicalLocations { get; init; }
    }

    private sealed class SarifLogicalLocation
    {
        public string? FullyQualifiedName { get; init; }
    }

    private sealed class SarifSuppression
    {
        public string Kind { get; init; } = "external";

        public string? Justification { get; init; }
    }
}
