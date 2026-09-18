// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using System.Text.Json;
using dgt.power.common;
using dgt.power.linter.Base;
using dgt.power.linter.Reporting;
using Microsoft.Xrm.Sdk;
using Spectre.Console;

namespace dgt.power.linter;

public sealed class LintRunCommand(
    ITracer tracer,
    IOrganizationService connection,
    IConfigResolver configResolver,
    IAnsiConsole console)
    : PowerLogic<LintVerb>(tracer, connection, configResolver, console)
{
    protected override async Task<bool> InvokeAsync(LintVerb args, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(args);
        Tracer.Start(this);

        if (args.UpdateBaseline && string.IsNullOrWhiteSpace(args.Baseline))
        {
            Console.MarkupLine("[red]--update-baseline requires --baseline <path>.[/]");
            return Tracer.End(this, false);
        }

        if (!ConfigResolver.TryGetConfigFile<LintConfig>(args.Config, out var config))
        {
            Console.MarkupLine(CultureInfo.InvariantCulture, "[red]Unable to read lint config from {0}[/]", args.Config);
            return Tracer.End(this, false);
        }

        var solutionNames = ParseSolutionNames(args.Solutions);
        if (solutionNames.Count == 0)
        {
            Console.MarkupLine("[yellow]No solution names supplied. Use --solutions <sol1,sol2>.[/]");
            return Tracer.End(this, false);
        }

        var findings = await EvaluateRulesAsync(Console, Connection, ConfigResolver, config, solutionNames, ParseRuleIds(args.Rules), cancellationToken);

        if (args.UpdateBaseline)
        {
            await SarifWriter.WriteAsync(args.Baseline, findings, suppressedKeys: null, cancellationToken);
            Console.MarkupLine(CultureInfo.InvariantCulture, "[green]Baseline updated: {0} finding(s) written to {1}[/]", findings.Count, args.Baseline);
            await WriteJsonReportAsync(args.Report, findings, baselinedKeys: new HashSet<string>(StringComparer.Ordinal), cancellationToken);
            return Tracer.End(this, true);
        }

        IReadOnlySet<string> baselinedKeys = string.IsNullOrWhiteSpace(args.Baseline)
            ? new HashSet<string>(StringComparer.Ordinal)
            : SarifWriter.ReadBaselineKeys(args.Baseline);

        await WriteJsonReportAsync(args.Report, findings, baselinedKeys, cancellationToken);

        if (!string.IsNullOrWhiteSpace(args.SarifOutput))
        {
            await SarifWriter.WriteAsync(args.SarifOutput, findings, baselinedKeys, cancellationToken);
        }

        PrintConsoleReport(findings, baselinedKeys);

        var threshold = ParseSeverity(args.FailOn);
        if (threshold is null)
        {
            return Tracer.End(this, true);
        }

        var failingFindings = findings
            .Where(finding => !baselinedKeys.Contains(finding.BaselineKey) && finding.Severity >= threshold.Value)
            .ToList();

        return Tracer.End(this, failingFindings.Count == 0);
    }

    private static async Task<List<LintFinding>> EvaluateRulesAsync(
        IAnsiConsole console,
        IOrganizationService connection,
        IConfigResolver configResolver,
        LintConfig config,
        IReadOnlyList<string> solutionNames,
        IReadOnlyList<string> requestedRuleIds,
        CancellationToken cancellationToken)
    {
        LintContext? context = null;
        await console.Status()
            .Spinner(Spinner.Known.Pong)
            .SpinnerStyle(Style.Parse("green bold"))
            .StartAsync("Preparing lint context (fetching metadata and solution components)...", _ =>
            {
                context = new LintContext(connection, solutionNames, configResolver);
                return Task.CompletedTask;
            });

        var findings = new List<LintFinding>();

        foreach (var rule in LintRuleCatalog.All)
        {
            if (requestedRuleIds.Count > 0 && !requestedRuleIds.Contains(rule.Id, StringComparer.OrdinalIgnoreCase))
            {
                continue;
            }

            var ruleConfig = GetRuleConfig(config, rule.Id);
            if (ruleConfig is { Enabled: false })
            {
                continue;
            }

            findings.AddRange(await rule.EvaluateAsync(context!, ruleConfig, cancellationToken));
        }

        return findings;
    }

    private static async Task WriteJsonReportAsync(string reportPath, IReadOnlyList<LintFinding> findings, IReadOnlySet<string> baselinedKeys, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(reportPath))
        {
            return;
        }

        var fullPath = Path.GetFullPath(reportPath);
        Directory.CreateDirectory(Path.GetDirectoryName(fullPath) ?? ".");

        var report = findings.Select(finding => new
        {
            finding.RuleId,
            finding.Severity,
            finding.Message,
            finding.SolutionUniqueName,
            finding.ComponentType,
            finding.ComponentLogicalName,
            finding.ComponentId,
            finding.Properties,
            Baselined = baselinedKeys.Contains(finding.BaselineKey)
        });

        await File.WriteAllTextAsync(fullPath, JsonSerializer.Serialize(report, new JsonSerializerOptions { WriteIndented = true }), cancellationToken);
    }

    private void PrintConsoleReport(IReadOnlyList<LintFinding> findings, IReadOnlySet<string> baselinedKeys)
    {
        if (findings.Count == 0)
        {
            Console.MarkupLine("[green]No lint findings found.[/]");
            return;
        }

        foreach (var finding in findings.OrderBy(static finding => finding.Severity).ThenBy(static finding => finding.RuleId, StringComparer.OrdinalIgnoreCase))
        {
            var color = finding.Severity switch
            {
                LintSeverity.Error => "red",
                LintSeverity.Warning => "yellow",
                _ => "blue"
            };

            var baselinedSuffix = baselinedKeys.Contains(finding.BaselineKey) ? " [grey](baselined)[/]" : string.Empty;
            Console.MarkupLine(CultureInfo.InvariantCulture, "[{0}]({1}) {2}[/] {3}{4}", color, finding.Severity, finding.RuleId, finding.Message, baselinedSuffix);
            if (!string.IsNullOrWhiteSpace(finding.ComponentLogicalName))
            {
                Console.MarkupLine(CultureInfo.InvariantCulture, "  component: {0}", finding.ComponentLogicalName);
            }
        }
    }

    private static LintRuleConfigEntry? GetRuleConfig(LintConfig config, string ruleId)
    {
        ArgumentNullException.ThrowIfNull(config);
        return config.Rules.TryGetValue(ruleId, out var ruleConfig) ? ruleConfig : null;
    }

    private static List<string> ParseRuleIds(string ruleList)
    {
        if (string.IsNullOrWhiteSpace(ruleList))
        {
            return [];
        }

        return ruleList
            .Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();
    }

    private static List<string> ParseSolutionNames(string solutionList)
    {
        if (string.IsNullOrWhiteSpace(solutionList))
        {
            return [];
        }

        return solutionList
            .Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();
    }

    /// <summary>Returns null for "None" (gate disabled), falls back to Error for empty/unrecognized values.</summary>
    private static LintSeverity? ParseSeverity(string value)
    {
        if (string.Equals(value, "None", StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }

        return Enum.TryParse<LintSeverity>(value, true, out var severity) ? severity : LintSeverity.Error;
    }
}

