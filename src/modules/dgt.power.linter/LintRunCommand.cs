// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using System.Text.Json;
using dgt.power.common;
using dgt.power.linter.Base;
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

        var context = new LintContext(Connection, solutionNames, ConfigResolver);
        var requestedRuleIds = ParseRuleIds(args.Rules);
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

            var ruleFindings = await rule.EvaluateAsync(context, ruleConfig, cancellationToken);
            findings.AddRange(ruleFindings);
        }

        if (!string.IsNullOrWhiteSpace(args.Report))
        {
            var reportPath = Path.GetFullPath(args.Report);
            Directory.CreateDirectory(Path.GetDirectoryName(reportPath) ?? ".");
            await File.WriteAllTextAsync(reportPath, JsonSerializer.Serialize(findings, new JsonSerializerOptions
            {
                WriteIndented = true
            }), cancellationToken);
        }

        if (findings.Count == 0)
        {
            Console.MarkupLine("[green]No lint findings found.[/]");
            return Tracer.End(this, true);
        }

        var threshold = ParseSeverity(args.FailOn);
        var failingFindings = findings.Where(finding => finding.Severity >= threshold).ToList();

        foreach (var finding in findings.OrderBy(static finding => finding.Severity).ThenBy(static finding => finding.RuleId, StringComparer.OrdinalIgnoreCase))
        {
            var color = finding.Severity switch
            {
                LintSeverity.Error => "red",
                LintSeverity.Warning => "yellow",
                _ => "blue"
            };

            Console.MarkupLine(CultureInfo.InvariantCulture, "[{0}]({1}) {2}[/] {3}", color, finding.Severity, finding.RuleId, finding.Message);
            if (!string.IsNullOrWhiteSpace(finding.ComponentLogicalName))
            {
                Console.MarkupLine(CultureInfo.InvariantCulture, "  component: {0}", finding.ComponentLogicalName);
            }
        }

        return Tracer.End(this, failingFindings.Count == 0);
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

    private static LintSeverity ParseSeverity(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return LintSeverity.Error;
        }

        return Enum.TryParse<LintSeverity>(value, true, out var severity)
            ? severity
            : LintSeverity.Error;
    }
}
