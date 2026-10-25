// Copyright (c) DIGITALL Nature. All rights reserved

using System.Text.Json;
using System.Text.Json.Serialization;
using dgt.power.common.FileAccess;
using Microsoft.Xrm.Sdk;

namespace dgt.power.process.Logic;

public static class ProcessConfigurationWriter
{
#pragma warning disable S1075 // This is the stable public schema identifier written into generated configuration documents.
    private const string SchemaUri = "https://raw.githubusercontent.com/DIGITALLNature/DigitallPower/main/schemas/process/v1/schema.json";
#pragma warning restore S1075

    public static string Serialize(IEnumerable<Entity> processes, string capture, IReadOnlyDictionary<Guid, string> ownerKeys)
    {
        ArgumentNullException.ThrowIfNull(processes);
        ArgumentNullException.ThrowIfNull(ownerKeys);
        var processArray = processes.ToArray();
        var includeState = capture is "state" or "both";
        var includeOwner = capture is "owner" or "both";
        if (includeOwner && Array.Exists(processArray, process => !ownerKeys.ContainsKey(process.Id)))
        {
            throw new InvalidOperationException("Every captured process must have exactly one resolved owner key.");
        }

        var entries = processArray.OrderBy(process => process.Id).Select(process =>
        {
            var processId = process.Id;
            var name = process.GetAttributeValue<string>("name");
            var active = includeState
                ? process.GetAttributeValue<OptionSetValue>("statecode")?.Value == 1
                : (bool?)null;
            var owner = includeOwner && ownerKeys.TryGetValue(process.Id, out var key) ? key : null;
            return new { processId, name, active, owner };
        }).ToArray();
        var document = new Dictionary<string, object?>
        {
            ["$schema"] = SchemaUri,
            ["schemaVersion"] = 1,
            ["processes"] = entries
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
