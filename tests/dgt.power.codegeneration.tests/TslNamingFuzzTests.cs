// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Logic;
using dgt.power.codegeneration.Templates;
using Fluid;

namespace dgt.power.codegeneration.tests;

public class TslNamingFuzzTests
{
    private const string FuzzAlphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 äöüß-_.()/\\@%:;!?+=*^[]{}<>\"'`~🚀Приветمرحبا";
    private const string UniqueTokenAlphabet = "abcdefghijklmnopqrstuvwxyz0123456789_äöüß-";

    [Test]
    public async Task SanitizeAndCamelCase_ShouldRemainDeterministicAcrossFuzzInputs()
    {
        var seedValues = new[] { 17, 42, 1_337, 20_260_609 };
        foreach (var seed in seedValues)
        {
            var random = new Random(seed);
            for (var i = 0; i < 250; i++)
            {
                var input = GenerateRandomText(random, random.Next(1, 80));
                var sanitizedA = Formatter.Sanitize(input);
                var sanitizedB = Formatter.Sanitize(input);
                var camelA = Formatter.CamelCase(input);
                var camelB = Formatter.CamelCase(input);

                await Assert.That(sanitizedA).IsEqualTo(sanitizedB);
                await Assert.That(camelA).IsEqualTo(camelB);
                await Assert.That(sanitizedA.Length > 0).IsTrue();
                await Assert.That(sanitizedA.Contains(' ')).IsFalse();
                await Assert.That(sanitizedA.Contains('/')).IsFalse();
                await Assert.That(sanitizedA.Contains('(')).IsFalse();
                await Assert.That(sanitizedA.Contains(')')).IsFalse();
                await Assert.That(sanitizedA.Contains('-')).IsFalse();
                await Assert.That(sanitizedA.Contains('.')).IsFalse();
                if (char.IsDigit(input[0]))
                {
                    await Assert.That(sanitizedA.StartsWith('_')).IsTrue();
                }
            }
        }
    }

    [Test]
    public async Task Sanitize_ShouldHandleEnterpriseEdgeCases()
    {
        var edgeCases = new[]
        {
            "äöü ß Konto Name",
            "Customer/Account(Main).v2",
            "123StartWithNumber",
            "Emoji🚀AndUnicodeПривет",
            "A very very very very very very very long field name with spaces"
        };

        foreach (var input in edgeCases)
        {
            var sanitized = Formatter.Sanitize(input);
            await Assert.That(sanitized.Length > 0).IsTrue();
            await Assert.That(sanitized.Contains(' ')).IsFalse();
        }
    }

    [Test]
    public async Task UniqueFilter_ShouldRemainCollisionFreeUnderFuzzedNames()
    {
        var options = TslTemplateOptionsFactory.Create(strictModeValue: "false", isCiAgent: false, maxStepsValue: null);
        var random = new Random(20_240_609);

        var tokens = new List<string>();
        for (var i = 0; i < 40; i++)
        {
            tokens.Add(GenerateRandomText(random, random.Next(3, 15), UniqueTokenAlphabet));
            if (i % 5 == 0)
            {
                tokens.Add(tokens[^1]); // enforce collisions
            }
        }

        var templateSource = string.Join("|",
            tokens.Select(token => $"{{{{ \"{EscapeForLiquid(token)}\" | unique: \"scope\" }}}}"));
        var template = ParseTemplate(templateSource);
        var output = template.Render(new TemplateContext(new { }, options));
        var rendered = output.Split('|');

        await Assert.That(rendered.Length).IsEqualTo(tokens.Count);
        await Assert.That(rendered.Distinct(StringComparer.Ordinal).Count()).IsEqualTo(tokens.Count);
    }

    private static string GenerateRandomText(Random random, int length, string alphabet = FuzzAlphabet)
    {
        var chars = new char[length];
        for (var i = 0; i < length; i++)
        {
            chars[i] = alphabet[random.Next(alphabet.Length)];
        }
        return new string(chars);
    }

    private static string EscapeForLiquid(string value) =>
        value.Replace("\\", "\\\\", StringComparison.Ordinal).Replace("\"", "\\\"", StringComparison.Ordinal);

    private static IFluidTemplate ParseTemplate(string templateSource)
    {
        var parser = new FluidParser(new FluidParserOptions { AllowParentheses = true });
        if (!parser.TryParse(templateSource, out var template, out var error))
        {
            throw new InvalidOperationException($"Failed to parse test template: {error}");
        }

        return template ?? throw new InvalidOperationException("Template parser returned no template for test case.");
    }
}
