// Copyright (c) DIGITALL Nature. All rights reserved

using dgt.power.common;
using dgt.power.documenttemplates.Configuration;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Spectre.Console;

namespace dgt.power.documenttemplates.Logic;

public sealed class DocumentTemplateExportEngine(IDataverseConnection connection, IAnsiConsole console,
    Func<string, byte[], bool, CancellationToken, Task>? writeFile = null)
{
    public async Task<bool> ExportAsync(string directory, string? entity, string? type, bool overwrite,
        CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(directory);
        var written = new List<string>();
        try
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (type is not (null or "word" or "excel"))
                throw new InvalidDataException("--type must be word or excel.");
            var output = Path.GetFullPath(directory);
            var service = await connection.GetServiceAsync();
            var query = DocumentTemplateReader.Query(true);
            query.Criteria.AddCondition("status", ConditionOperator.Equal, false);
            query.Criteria.AddCondition("documenttype", ConditionOperator.In, 1, 2);
            if (entity is not null)
                query.Criteria.AddCondition("associatedentitytypecode", ConditionOperator.Equal, entity);
            if (type is not null)
                query.Criteria.AddCondition("documenttype", ConditionOperator.Equal, type == "word" ? 2 : 1);
            var targets = await DocumentTemplateReader.ReadAllAsync(service, query, cancellationToken);
            var files = new Dictionary<string, byte[]>(StringComparer.OrdinalIgnoreCase);
            var entries = new List<DocumentTemplateEntry>();
            foreach (var target in targets)
            {
                cancellationToken.ThrowIfCancellationRequested();
                var typeCode = target.GetAttributeValue<OptionSetValue>("documenttype")?.Value;
                var logicalName = target.GetAttributeValue<string>("associatedentitytypecode");
                var active = target.Attributes.TryGetValue("status", out var status) && status is false;
                var supported = typeCode is 1 or 2;
                var matchesType = type is null || (type == "word" ? 2 : 1) == typeCode;
                var matchesEntity = entity is null || string.Equals(entity, logicalName, StringComparison.OrdinalIgnoreCase);
                if (!active || !supported || !matchesType || !matchesEntity)
                    continue;
                var documentType = typeCode == 2 ? "word" : "excel";
                var filename = $"{target.Id:D}.{(typeCode == 2 ? "docx" : "xlsx")}".ToLowerInvariant();
                if (target.Id == Guid.Empty || !files.TryAdd(filename, Convert.FromBase64String(
                        target.GetAttributeValue<string>("content") ?? throw new InvalidDataException("Missing document content."))))
                    throw new InvalidDataException($"Invalid ID or generated destination collision '{filename}'.");
                DocumentPackage.Validate(files[filename], documentType);
                if (!target.Attributes.TryGetValue("languagecode", out var language) || language is not int languageCode)
                    throw new InvalidDataException($"Missing explicit language for '{filename}'.");
                entries.Add(new DocumentTemplateEntry(target.Id, target.GetAttributeValue<string>("name") ?? "",
                    documentType, logicalName ?? "", filename, languageCode, target.GetAttributeValue<string>("description")));
            }
            var manifest = new DocumentTemplateManifest(entries);
            var manifestJson = manifest.ToJson();
            _ = DocumentTemplateManifest.Parse(manifestJson);
            files.Add("documenttemplates.json", System.Text.Encoding.UTF8.GetBytes(manifestJson));
            foreach (var name in files.Keys)
            {
                var destination = Path.Combine(output, name);
                if (System.IO.Directory.Exists(destination) || new FileInfo(destination).LinkTarget is not null ||
                    (!overwrite && File.Exists(destination)))
                    throw new IOException($"Destination collision '{destination}'; use --overwrite only for regular files.");
            }
            cancellationToken.ThrowIfCancellationRequested();
            System.IO.Directory.CreateDirectory(output);
            foreach (var (name, content) in files)
            {
                cancellationToken.ThrowIfCancellationRequested();
                var destination = Path.Combine(output, name);
                written.Add(destination);
                await (writeFile ?? WriteFileAsync)(destination, content, overwrite, cancellationToken);
            }
            console.WriteLine($"Exported {entries.Count} document templates.");
            return true;
        }
        catch (OperationCanceledException)
        {
            if (written.Count > 0)
                console.WriteLine($"Export cancelled; partial output remains (not rolled back): {string.Join(", ", written)}");
            throw;
        }
        catch (Exception exception) when (exception is not OperationCanceledException)
        {
            console.MarkupLine($"[red]Export failed: {exception.Message.EscapeMarkup()}[/]");
            console.WriteLine(written.Count == 0 ? "No output files written." :
                $"Partial output remains (not rolled back; last file may be incomplete): {string.Join(", ", written)}");
            return false;
        }
    }

    private static async Task WriteFileAsync(string destination, byte[] content, bool overwrite,
        CancellationToken cancellationToken)
    {
        await using var stream = new FileStream(destination, overwrite ? FileMode.Create : FileMode.CreateNew,
            FileAccess.Write, FileShare.None, 4096, true);
        await stream.WriteAsync(content, cancellationToken);
    }
}
