// Copyright (c) DIGITALL Nature. All rights reserved

using System.Text.Json;
using System.Text.Json.Serialization;
using dgt.power.process.Configuration;
using Microsoft.Xrm.Sdk;

namespace dgt.power.process.Logic;

public static class ProcessConfigurationWriter
{
    private const string SchemaUri = "https://raw.githubusercontent.com/DIGITALLNature/DigitallPower/main/schemas/process/v1/schema.json";

    public static string Serialize(IEnumerable<Entity> processes, string capture, IReadOnlyDictionary<Guid, string> ownerKeys)
    {
        ArgumentNullException.ThrowIfNull(processes);
        ArgumentNullException.ThrowIfNull(ownerKeys);
        var includeState = capture is "state" or "both";
        var includeOwner = capture is "owner" or "both";
        if (includeOwner && processes.Any(process => !ownerKeys.ContainsKey(process.Id)))
        {
            throw new InvalidOperationException("Every captured process must have exactly one resolved owner key.");
        }

        var entries = processes.OrderBy(process => process.Id).Select(process => new
        {
            processId = process.Id,
            name = process.GetAttributeValue<string>("name"),
            active = includeState
                ? process.GetAttributeValue<OptionSetValue>("statecode")?.Value == 1
                : (bool?)null,
            owner = includeOwner && ownerKeys.TryGetValue(process.Id, out var key) ? key : null
        }).ToArray();
        var document = new Dictionary<string, object?>
        {
            ["$schema"] = SchemaUri,
            ["schemaVersion"] = 1,
            ["processes"] = entries.Select(entry => new
            {
                processId = entry.processId,
                name = entry.name,
                active = entry.active,
                owner = entry.owner
            })
        };

        return JsonSerializer.Serialize(document, new JsonSerializerOptions
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        });
    }

    public static async Task WriteAtomicallyAsync(string outputPath, string contents, bool overwrite, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(outputPath);
        ArgumentNullException.ThrowIfNull(contents);
        var path = Path.GetFullPath(outputPath, Environment.CurrentDirectory);
        if (!overwrite && File.Exists(path))
        {
            throw new IOException($"Output file '{path}' already exists. Use --overwrite to replace it.");
        }

        var directory = Path.GetDirectoryName(path) ?? Environment.CurrentDirectory;
        Directory.CreateDirectory(directory);
        var temporaryPath = Path.Combine(directory, $".{Path.GetFileName(path)}.{Guid.NewGuid():N}.tmp");
        try
        {
            await using (var stream = new FileStream(temporaryPath, FileMode.CreateNew, FileAccess.Write, FileShare.None))
            await using (var writer = new StreamWriter(stream))
            {
                await writer.WriteAsync(contents.AsMemory(), cancellationToken);
                await writer.FlushAsync(cancellationToken);
            }

            File.Move(temporaryPath, path, overwrite);
        }
        finally
        {
            if (File.Exists(temporaryPath))
            {
                File.Delete(temporaryPath);
            }
        }
    }
}
