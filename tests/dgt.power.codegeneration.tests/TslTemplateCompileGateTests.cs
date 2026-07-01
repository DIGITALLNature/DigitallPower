// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Templates;

namespace dgt.power.codegeneration.tests;

public class TslTemplateCompileGateTests
{
    private const string TslResourcePrefix = "dgt.power.codegeneration.Templates.tsl";

    [Test]
    public Task ShouldParseAllEmbeddedTslTemplates()
    {
        var templateNames = typeof(LiquidTemplateEngine).Assembly
            .GetManifestResourceNames()
            .Where(resourceName => resourceName.StartsWith($"{TslResourcePrefix}.", StringComparison.Ordinal)
                                   && resourceName.EndsWith(".liquid", StringComparison.Ordinal))
            .Select(resourceName => resourceName[(TslResourcePrefix.Length + 1)..])
            .OrderBy(templateName => templateName, StringComparer.Ordinal)
            .ToList();

        if (templateNames.Count == 0)
        {
            throw new InvalidOperationException("No embedded TSL templates were discovered for the compile gate.");
        }

        var failures = new List<string>();
        foreach (var templateName in templateNames)
        {
            try
            {
                _ = LiquidTemplateEngine.LoadTemplate(TslResourcePrefix, templateName);
            }
            catch (InvalidOperationException ex)
            {
                failures.Add($"- {templateName}: {ex.Message}");
            }
        }

        if (failures.Count > 0)
        {
            throw new InvalidOperationException(
                $"TSL template compile gate failed:{Environment.NewLine}{string.Join(Environment.NewLine, failures)}");
        }

        return Task.CompletedTask;
    }
}
