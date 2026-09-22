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

        // Per the source map spec, the sourceMappingURL comment must be the last line of the
        // generated code. Only inspecting that last line (rather than searching the whole text)
        // is what actually distinguishes a real directive from the same token merely appearing
        // inside a string literal or identifier earlier in the file.
        var text = Encoding.UTF8.GetString(bytes).AsSpan().TrimEnd();
        var lastNewline = text.LastIndexOfAny('\n', '\r');
        var lastLine = lastNewline >= 0 ? text[(lastNewline + 1)..] : text;

        return SourceMapDirective().IsMatch(lastLine);
    }

    // Anchored to the start of the (already isolated) last line, so a directive-looking string
    // embedded earlier in the file - e.g. `var x = '//# sourceMappingURL=foo';` - never matches.
    [GeneratedRegex(@"^\s*(?://|/\*)[@#]\s*sourceMappingURL\s*=\s*\S+", RegexOptions.CultureInvariant)]
    private static partial Regex SourceMapDirective();
}
