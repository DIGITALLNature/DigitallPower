// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text.RegularExpressions;
using dgt.power.solution.Base;
using Microsoft.Xrm.Sdk.Metadata;

namespace dgt.power.solution.Rules;

/// <summary>
/// Validates unmanaged custom field logical names against the DIGITALL Nature naming convention
/// (https://digitallnature.github.io/customizing/naming-conventions/): prfx_fieldname[_type-suffix].
/// </summary>
public sealed partial class UnmanagedFieldNamingRule : ILintRule
{
    // https://learn.microsoft.com/dotnet/api/microsoft.xrm.sdk.metadata.attributemetadata.sourcetype
    private const int Calculated = 1;
    private const int Rollup = 2;
    private const int Formula = 3;

    public string Id => "naming.unmanaged-field-logicalname";
    public string Description => "Ensures unmanaged custom fields use an allowed publisher prefix and the expected field-type suffix.";
    public LintSeverity DefaultSeverity => LintSeverity.Error;
    public bool IsEnabledByDefault => true;

    public Task<IReadOnlyList<LintFinding>> EvaluateAsync(LintContext context, LintRuleConfigEntry? ruleConfig, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(context);

        var prefixes = NormalizePrefixes(ruleConfig?.ReadOptions<UnmanagedFieldNamingOptions>().PublisherPrefixes);
        var severity = ruleConfig?.Severity ?? DefaultSeverity;
        var findings = new List<LintFinding>();

        // Iterates the resolved membership (not the raw solutioncomponent rows) so that entities
        // added with RootComponentBehavior.IncludeSubcomponents - which never get per-attribute
        // solutioncomponent rows - still have every one of their fields checked.
        foreach (var membership in context.EntityMemberships.Values)
        {
            foreach (var attribute in membership.EffectiveAttributes)
            {
                if (attribute.IsCustomAttribute != true || attribute.IsManaged == true)
                {
                    continue;
                }

                var attributeName = attribute.LogicalName;
                if (string.IsNullOrWhiteSpace(attributeName) || !attributeName.Contains('_', StringComparison.Ordinal))
                {
                    // Dataverse itself sometimes provisions default columns without any publisher
                    // prefix (e.g. "name", "createdon", or the auto-created statecode/statuscode on a
                    // new custom table). We never author those, so a missing prefix is treated as
                    // out of our control rather than a violation.
                    continue;
                }

                var expectedSuffixes = ResolveExpectedSuffixes(attribute);
                if (expectedSuffixes is null)
                {
                    continue;
                }

                var violation = Validate(attributeName, prefixes, expectedSuffixes);
                if (violation is null)
                {
                    continue;
                }

                findings.Add(new LintFinding(
                    Id,
                    severity,
                    $"Custom field '{attributeName}' {violation}.",
                    membership.SolutionUniqueName,
                    "Attribute",
                    attributeName,
                    attribute.MetadataId,
                    new Dictionary<string, object?>
                    {
                        ["entityLogicalName"] = membership.EntityLogicalName,
                        ["publisherPrefixes"] = prefixes
                    }));
            }
        }

        return Task.FromResult<IReadOnlyList<LintFinding>>(findings);
    }

    private static string? Validate(string attributeName, IReadOnlyList<string> prefixes, IReadOnlyList<string> expectedSuffixes)
    {
        var matchedPrefix = prefixes.FirstOrDefault(prefix => attributeName.StartsWith(prefix, StringComparison.OrdinalIgnoreCase));
        if (matchedPrefix is null)
        {
            return $"does not start with an allowed publisher prefix ({string.Join(", ", prefixes)})";
        }

        var remainder = attributeName[matchedPrefix.Length..];
        if (remainder.Length == 0 || !SnakeCaseNameRegex().IsMatch(remainder))
        {
            return "must be lowercase snake_case after the publisher prefix";
        }

        // An empty entry means "no suffix required" (plain Text fields) - any remainder is accepted.
        if (expectedSuffixes.Contains(string.Empty, StringComparer.Ordinal))
        {
            return null;
        }

        return expectedSuffixes.Any(suffix => remainder.EndsWith(suffix, StringComparison.OrdinalIgnoreCase))
            ? null
            : $"must end with one of: {string.Join(", ", expectedSuffixes)}";
    }

    /// <summary>Returns the acceptable suffix(es) for the attribute's type, or null if this rule does not evaluate that type.</summary>
    private static IReadOnlyList<string>? ResolveExpectedSuffixes(AttributeMetadata attribute)
    {
        var baseSuffix = attribute.AttributeType switch
        {
            AttributeTypeCode.Lookup => "_id",
            AttributeTypeCode.Customer => "_vid",
            AttributeTypeCode.Picklist => "_set",
            AttributeTypeCode.Money => "_cur",
            AttributeTypeCode.DateTime => "_dt",
            AttributeTypeCode.Integer => ResolveIntegerSuffix(attribute),
            AttributeTypeCode.Decimal => "_dec",
            AttributeTypeCode.Double => "_flt",
            AttributeTypeCode.Boolean => "_bit",
            AttributeTypeCode.Memo => "_txt",
            AttributeTypeCode.String => ResolveStringSuffix(attribute),
            AttributeTypeCode.Virtual => ResolveVirtualSuffix(attribute),
            _ => null
        };

        if (baseSuffix is null)
        {
            return null;
        }

        // Plain Text fields accept either no suffix or "_txt" - signal that via an empty entry.
        if (attribute.AttributeType == AttributeTypeCode.String && baseSuffix == "_txt")
        {
            return [string.Empty, "_txt"];
        }

        var modifier = attribute.SourceType switch
        {
            Calculated => "_cf",
            Rollup => "_rf",
            Formula => "_fx",
            _ => null
        };

        return modifier is null ? [baseSuffix] : [baseSuffix + modifier];
    }

    private static string ResolveIntegerSuffix(AttributeMetadata attribute) => attribute switch
    {
        IntegerAttributeMetadata { Format: IntegerFormat.Duration } => "_dur",
        IntegerAttributeMetadata { Format: IntegerFormat.Language } => "_lcid",
        IntegerAttributeMetadata { Format: IntegerFormat.TimeZone } => "_tzid",
        _ => "_int"
    };

    private static string? ResolveStringSuffix(AttributeMetadata attribute)
    {
        var formatName = (attribute as StringAttributeMetadata)?.FormatName?.Value;
        return formatName switch
        {
            "Email" => "_email",
            "Phone" => "_number",
            "Url" => "_url",
            _ => "_txt" // plain Text - special-cased by the caller to also accept no suffix
        };
    }

    private static string? ResolveVirtualSuffix(AttributeMetadata attribute) => attribute switch
    {
        MultiSelectPicklistAttributeMetadata => "_mset",
        ImageAttributeMetadata => "_img",
        FileAttributeMetadata => "_file",
        _ => null
    };

    private static List<string> NormalizePrefixes(IReadOnlyList<string>? configuredPrefixes)
    {
        var normalized = (configuredPrefixes ?? [])
            .Where(prefix => !string.IsNullOrWhiteSpace(prefix))
            .Select(prefix => prefix.EndsWith('_') ? prefix : prefix + "_")
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        return normalized.Count > 0 ? normalized : ["dgt_"];
    }

    [GeneratedRegex("^[a-z][a-z0-9_]*$")]
    private static partial Regex SnakeCaseNameRegex();

    private sealed class UnmanagedFieldNamingOptions
    {
        public IReadOnlyList<string> PublisherPrefixes { get; init; } = ["dgt_"];
    }
}
