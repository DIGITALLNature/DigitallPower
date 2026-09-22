// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text;
using System.Text.RegularExpressions;
using dgt.power.dataverse;
using dgt.power.solution.Base;

namespace dgt.power.solution.Rules;

/// <summary>
/// Flags JScript web resources that still contain a source map reference (e.g. "//# sourceMappingURL=..."),
/// which indicates the resource was added to the solution unminified (a source build artifact, not the
/// production/minified output).
/// </summary>
public sealed partial class JScriptSourceMapRule : ILintRule
{
    public string Id => "webresource.jscript-sourcemap";
    public string Description => "JScript web resources must be minified and must not contain a source map reference.";
    public LintSeverity DefaultSeverity => LintSeverity.Warning;
    public bool IsEnabledByDefault => true;

    public Task<IReadOnlyList<LintFinding>> EvaluateAsync(LintContext context, LintRuleConfigEntry? ruleConfig, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(context);

        var severity = ruleConfig?.Severity ?? DefaultSeverity;
        var findings = new List<LintFinding>();

        foreach (var component in context.WebResourceComponents)
        {
            if (component.ObjectId is not { } webResourceId
                || !context.WebResourcesById.TryGetValue(webResourceId, out var webResource)
                || webResource.WebResourceType?.Value != WebResource.Options.WebResourceType.ScriptJScript
                || !ContainsSourceMapReference(webResource.Content))
            {
                continue;
            }

            var solutionUniqueName = component.SolutionId != null && context.SolutionUniqueNamesById.TryGetValue(component.SolutionId.Id, out var uniqueName)
                ? uniqueName
                : null;

            findings.Add(new LintFinding(
                Id,
                severity,
                $"JScript web resource '{webResource.Name}' contains a source map reference and is likely not minified.",
                solutionUniqueName,
                "WebResource",
                webResource.Name,
                webResource.Id));
        }

        return Task.FromResult<IReadOnlyList<LintFinding>>(findings);
    }

    private static bool ContainsSourceMapReference(string? base64Content)
    {
        if (string.IsNullOrEmpty(base64Content))
        {
            return false;
        }

        byte[] bytes;
        try
        {
            bytes = Convert.FromBase64String(base64Content);
        }
        catch (FormatException)
        {
            return false;
        }

        return SourceMapDirective().IsMatch(Encoding.UTF8.GetString(bytes));
    }

    // Matches the source-map comment directive (// or /* form, # or the deprecated @ pragma)
    // rather than a raw substring search, so the token merely appearing in a string literal or
    // identifier does not produce a false positive.
    [GeneratedRegex(@"(?://|/\*)[@#]\s*sourceMappingURL\s*=\s*\S+", RegexOptions.CultureInvariant)]
    private static partial Regex SourceMapDirective();
}
