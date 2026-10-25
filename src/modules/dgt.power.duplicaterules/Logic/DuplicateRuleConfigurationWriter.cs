// Copyright (c) DIGITALL Nature. All rights reserved

using System.Text.Json;
using System.Text.Json.Serialization;
using dgt.power.common.FileAccess;
using Microsoft.Xrm.Sdk;

namespace dgt.power.duplicaterules.Logic;

public static class DuplicateRuleConfigurationWriter
{
#pragma warning disable S1075 // This is the stable public schema identifier written into generated configuration documents.
    private const string SchemaUri = "https://raw.githubusercontent.com/DIGITALLNature/DigitallPower/main/schemas/duplicaterules/v1/schema.json";
#pragma warning restore S1075

    public static string Serialize(IEnumerable<Entity> rules, string capture, IReadOnlyDictionary<Guid, string> ownerKeys)
    {
        ArgumentNullException.ThrowIfNull(rules);
        ArgumentNullException.ThrowIfNull(ownerKeys);
        var ruleArray = rules.ToArray();
        var includeState = capture is "state" or "both";
        var includeOwner = capture is "owner" or "both";
        if (includeOwner && Array.Exists(ruleArray, rule => !ownerKeys.ContainsKey(rule.Id)))
        {
            throw new InvalidOperationException("Every captured rule must have exactly one resolved owner key.");
        }

        var entries = ruleArray.OrderBy(rule => rule.Id).Select(rule =>
        {
            var state = DuplicateRuleStates.Classify(rule);
            if (includeState && state is not (DuplicateRulePublicationState.Published or DuplicateRulePublicationState.Unpublished))
            {
                throw new InvalidOperationException($"Rule '{rule.Id:D}' is not in a stable publication state.");
            }

            var ruleId = rule.Id;
            var name = rule.GetAttributeValue<string>("name");
            var published = includeState ? state == DuplicateRulePublicationState.Published : (bool?)null;
            var owner = includeOwner && ownerKeys.TryGetValue(rule.Id, out var key) ? key : null;
            return new { ruleId, name, published, owner };
        }).ToArray();
        var document = new Dictionary<string, object?>
        {
            ["$schema"] = SchemaUri,
            ["schemaVersion"] = 1,
            ["rules"] = entries
        };

        return JsonSerializer.Serialize(document, new JsonSerializerOptions
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        });
    }

    public static Task WriteAtomicallyAsync(string outputPath, string contents, bool overwrite, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(outputPath);
        ArgumentNullException.ThrowIfNull(contents);
        return AtomicFileWriter.WriteAsync(outputPath, contents, overwrite, cancellationToken);
    }
}
