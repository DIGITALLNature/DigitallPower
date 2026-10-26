// Copyright (c) DIGITALL Nature. All rights reserved

using dgt.power.common;
using dgt.power.documenttemplates.Configuration;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Spectre.Console;

namespace dgt.power.documenttemplates.Logic;

public sealed class DocumentTemplateImportEngine(IDataverseConnection connection, IAnsiConsole console)
{
    private static async Task<IReadOnlyList<LocalDocumentTemplate>> LoadAsync(string manifestPath,
        CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(manifestPath);
        var manifest = DocumentTemplateManifest.Parse(await File.ReadAllTextAsync(manifestPath, cancellationToken));
        var documents = new List<LocalDocumentTemplate>();
        foreach (var entry in manifest.Templates)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var path = DocumentPackage.ResolveFile(manifestPath, entry.File);
            var bytes = await File.ReadAllBytesAsync(path, cancellationToken);
            cancellationToken.ThrowIfCancellationRequested();
            DocumentPackage.Validate(bytes, entry.DocumentType);
            documents.Add(new LocalDocumentTemplate(entry, bytes));
        }
        return documents;
    }

    public async Task<bool> ImportAsync(string manifestPath, bool dryRun, CancellationToken cancellationToken)
    {
        IReadOnlyList<LocalDocumentTemplate> local;
        try
        {
            local = await LoadAsync(manifestPath, cancellationToken);
        }
        catch (Exception exception) when (exception is not OperationCanceledException)
        {
            console.MarkupLine($"[red]Local preflight failed: {exception.Message.EscapeMarkup()}[/]");
            return false;
        }
        if (local.Count == 0)
        {
            console.WriteLine("0 document templates; no target access required.");
            return true;
        }
        var service = await connection.GetServiceAsync();
        IReadOnlyList<PreparedDocumentTemplate> prepared;
        try
        {
            prepared = await PrepareAsync(local, service, new TemplateMetadata(service), cancellationToken);
        }
        catch (Exception exception) when (exception is not OperationCanceledException)
        {
            console.MarkupLine($"[red]Target preflight failed; no writes: {exception.Message.EscapeMarkup()}[/]");
            return false;
        }
        if (dryRun)
        {
            foreach (var item in prepared)
            {
                cancellationToken.ThrowIfCancellationRequested();
                console.WriteLine($"DRY RUN: {(item.Create ? "create" : "update")} {Label(item.Entry)} ({item.TargetId:D}); activate.");
            }
            console.WriteLine($"{prepared.Count} document templates prepared; no writes. Later server errors remain possible.");
            return true;
        }
        return await ExecuteAsync(prepared, service, cancellationToken);
    }

    private static Task<IReadOnlyList<PreparedDocumentTemplate>> PrepareAsync(
        IReadOnlyList<LocalDocumentTemplate> local, IOrganizationServiceAsync2 service, TemplateMetadata metadata,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(local);
        ArgumentNullException.ThrowIfNull(service);
        ArgumentNullException.ThrowIfNull(metadata);
        return PrepareCoreAsync(local, service, metadata, cancellationToken);
    }

    private static async Task<IReadOnlyList<PreparedDocumentTemplate>> PrepareCoreAsync(
        IReadOnlyList<LocalDocumentTemplate> local, IOrganizationServiceAsync2 service, TemplateMetadata metadata,
        CancellationToken cancellationToken)
    {
        var targets = await DocumentTemplateReader.ReadAllAsync(service, DocumentTemplateReader.Query(false), cancellationToken);
        var prepared = new List<PreparedDocumentTemplate>();
        var keys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        var ids = new HashSet<Guid>();
        foreach (var document in local)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var entity = await metadata.GetAsync(document.Entry.Entity, cancellationToken);
            var entry = document.Entry with { Entity = entity.LogicalName };
            if (!keys.Add(entry.Identity) || !ids.Add(entry.SourceId))
                throw new InvalidDataException($"Duplicate identity or source ID for {Label(entry)}.");
            var matches = targets.Where(target => Matches(target, entry)).ToArray();
            if (matches.Length > 1)
                throw new InvalidDataException($"Ambiguous target identity {Label(entry)}.");
            if (targets.Any(target => target.Id == entry.SourceId && !Matches(target, entry)))
                throw new InvalidDataException($"sourceId {entry.SourceId:D} is occupied by a different target template.");
            var content = entry.DocumentType == "word"
                ? await WordTemplateAdapter.PrepareAsync(document.Content.ToArray(), entry.Entity, metadata, cancellationToken)
                : document.Content.ToArray();
            prepared.Add(new PreparedDocumentTemplate(entry, matches.SingleOrDefault()?.Id ?? entry.SourceId,
                matches.Length == 0, content));
        }
        return prepared;
    }

    public Task<bool> ExecuteAsync(IReadOnlyList<PreparedDocumentTemplate> prepared,
        IOrganizationServiceAsync2 service, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(prepared);
        ArgumentNullException.ThrowIfNull(service);
        return ExecuteCoreAsync(prepared, service, cancellationToken);
    }

    private async Task<bool> ExecuteCoreAsync(IReadOnlyList<PreparedDocumentTemplate> prepared,
        IOrganizationServiceAsync2 service, CancellationToken cancellationToken)
    {
        var succeeded = 0;
        foreach (var item in prepared)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var contentWritten = false;
            try
            {
                var target = new Entity("documenttemplate", item.TargetId)
                {
                    ["name"] = item.Entry.Name,
                    ["content"] = Convert.ToBase64String(item.Content.Span),
                    ["languagecode"] = item.Entry.LanguageCode,
                    ["description"] = item.Entry.Description
                };
                if (item.Create)
                {
                    target["documenttype"] = new OptionSetValue(item.Entry.TypeCode);
                    target["associatedentitytypecode"] = item.Entry.Entity;
                    await service.ExecuteAsync(new CreateRequest { Target = target }, cancellationToken);
                }
                else
                    await service.ExecuteAsync(new UpdateRequest { Target = target }, cancellationToken);
                contentWritten = true;
                cancellationToken.ThrowIfCancellationRequested();
                await service.ExecuteAsync(new UpdateRequest
                {
                    Target = new Entity("documenttemplate", item.TargetId) { ["status"] = false }
                }, cancellationToken);
                succeeded++;
                console.WriteLine($"Imported and activated {Label(item.Entry)} ({item.TargetId:D}).");
            }
            catch (OperationCanceledException)
            {
                console.WriteLine($"Import cancelled at {Label(item.Entry)} ({item.TargetId:D}); completed writes remain. No rollback.");
                throw;
            }
            catch (Exception exception) when (exception is not OperationCanceledException)
            {
                console.MarkupLine($"[red]Failed {Label(item.Entry).EscapeMarkup()} ({item.TargetId:D}): {exception.Message.EscapeMarkup()}. " +
                    (contentWritten
                        ? "Partial changes: content/metadata were written; activation failed. No rollback.[/]"
                        : "The content write failed; server-side partial effects may remain. Activation was not attempted. No rollback.[/]"));
            }
        }
        console.WriteLine($"{succeeded}/{prepared.Count} document templates imported and activated.");
        return succeeded == prepared.Count;
    }

    private static bool Matches(Entity target, DocumentTemplateEntry entry) =>
        string.Equals(target.GetAttributeValue<string>("name"), entry.Name, StringComparison.OrdinalIgnoreCase) &&
        target.GetAttributeValue<OptionSetValue>("documenttype")?.Value == entry.TypeCode &&
        string.Equals(target.GetAttributeValue<string>("associatedentitytypecode"), entry.Entity, StringComparison.OrdinalIgnoreCase);

    private static string Label(DocumentTemplateEntry entry) => $"'{entry.Name}' ({entry.DocumentType}, {entry.Entity})";
}
