// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using dgt.power.common;
using dgt.power.common.Extensions;
using dgt.power.dto;
using dgt.power.import.Base;
using DocumentFormat.OpenXml.Packaging;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using static dgt.power.dataverse.DocumentTemplate.Options;
using DocumentTemplate = dgt.power.dto.DocumentTemplate;
using IAnsiConsole = Spectre.Console.IAnsiConsole;
using static Spectre.Console.AnsiConsoleExtensions;

namespace dgt.power.import.Logic;

public sealed class DocumentTemplateImport(
    ICommandSpanFactory spans,
    IDataverseConnection connection,
    IConfigResolver configResolver,
    IAnsiConsole console)
    : PowerCommand<ImportVerb>(spans, console)
{
    protected override async Task<bool> RunAsync(ImportVerb args, CancellationToken cancellationToken)
    {
        var fileName = string.IsNullOrWhiteSpace(args.FileName) ? "documenttemplate.json" : args.FileName;


        if (!configResolver.TryGetConfigFile<DocumentTemplates>(args.FileDir, fileName, out var templates))
        {
            Console.MarkupLine("[red]Document template configuration not found.[/]");
            return false;
        }

        //anything to do?
        if (templates.Templates.Count == 0)
        {
            Console.MarkupLine("[red]Document template configuration is empty.[/]");
            return false;
        }

        var result = true;

        var service = await connection.GetServiceAsync();
        var query = new QueryExpression(dataverse.DocumentTemplate.EntityLogicalName)
        {
            NoLock = true,
            ColumnSet = new ColumnSet(
                dataverse.DocumentTemplate.LogicalNames.DocumentTemplateId,
                dataverse.DocumentTemplate.LogicalNames.Name,
                dataverse.DocumentTemplate.LogicalNames.DocumentType,
                dataverse.DocumentTemplate.LogicalNames.Status,
                dataverse.DocumentTemplate.LogicalNames.Description,
                dataverse.DocumentTemplate.LogicalNames.LanguageCode),
            Orders = { new OrderExpression(dataverse.DocumentTemplate.LogicalNames.Name, OrderType.Ascending) }
        };
        var documentTemplates = (await service.RetrieveAllAsync(query, cancellationToken: cancellationToken))
            .Select(entity => entity.ToEntity<dataverse.DocumentTemplate>()).ToList();

        //find document template which need to be disabled (missing)
        Console.WriteLine("disable check");
        foreach (var deleteTemplate in documentTemplates.Where(e =>
                     templates.Templates.All(c => (e.Name, e.DocumentType?.Value) != (c.Name, (int)c.DocumentType))))
        {
            result = await DisableTemplateAsync(service, templates, deleteTemplate, cancellationToken) && result;
        }

        //find queues need to be updated
        Console.WriteLine("update check");
        foreach (var updateTemplate in documentTemplates.Where(e =>
                     templates.Templates.Any(c => (e.Name, e.DocumentType?.Value) == (c.Name, (int)c.DocumentType))))
        {
            result = await UpdateTemplateAsync(service, args, templates, updateTemplate, cancellationToken) && result;
        }

        //find bulk deletes which need to be created
        Console.WriteLine("create check");
        foreach (var createTemplate in templates.Templates.Where(e =>
                     documentTemplates.TrueForAll(c => (c.Name, c.DocumentType?.Value) != (e.Name, (int)e.DocumentType))))
        {
            result = await CreateTemplateAsync(service, args, createTemplate, cancellationToken) && result;
        }

        return result;
    }

    private async Task<bool> CreateTemplateAsync(IOrganizationServiceAsync2 service, ImportVerb args, DocumentTemplate createTemplate,
        CancellationToken cancellationToken)
    {
        Console.MarkupLine("[grey]--->[/]");
        var name = createTemplate.Name.Trim().Replace(".xlsx", "", StringComparison.Ordinal).Replace(".docx", "", StringComparison.Ordinal);
        Console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[grey]create document template: {name}[/]");
        //create
        var template = new dataverse.DocumentTemplate
        {
            DocumentTemplateId = createTemplate.DocumentTemplateId,
            Name = name,
            DocumentType = createTemplate.DocumentType == DocumentTemplate.DocumentTemplateType.MicrosoftExcel
                ? new OptionSetValue(DocumentType.MicrosoftExcel)
                : new OptionSetValue(DocumentType.MicrosoftWord),
            Description = createTemplate.Description,
            LanguageCode = createTemplate.LanguageCode ?? 1033,
            Content = await GetContentAsync(service, args, createTemplate.File, createTemplate.DocumentType,
                createTemplate.AssociatedEntityTypeCode, cancellationToken)
        };
        var (result, id) = await TryCreateTemplateAsync(service, template, cancellationToken);
        return await SetTemplateStatusAsync(service, createTemplate, id, cancellationToken) && result;
    }

    private static async Task<(bool Succeeded, Guid Id)> TryCreateTemplateAsync(IOrganizationServiceAsync2 service,
        dataverse.DocumentTemplate template, CancellationToken cancellationToken)
    {
        var (succeeded, response) = await service.TryExecuteAsync<CreateRequest, CreateResponse>(
            new CreateRequest { Target = template }, cancellationToken);
        return (succeeded, response?.id ?? Guid.Empty);
    }

    private static Task<bool> SetTemplateStatusAsync(IOrganizationServiceAsync2 service, DocumentTemplate template, Guid id,
        CancellationToken cancellationToken)
    {
        if ((template.DocumentStatus ?? false) == Status.Activated) //enable
        {
            return service.TryUpdateAsync(new dataverse.DocumentTemplate(id) { Status = Status.Activated }, cancellationToken);
        }

        if ((template.DocumentStatus ?? false) == Status.Draft) //disable
        {
            return service.TryUpdateAsync(new dataverse.DocumentTemplate(id) { Status = Status.Draft }, cancellationToken);
        }

        return Task.FromResult(true);
    }

    private async Task<bool> DisableTemplateAsync(IOrganizationServiceAsync2 service, DocumentTemplates templates,
        dataverse.DocumentTemplate deleteTemplate, CancellationToken cancellationToken)
    {
        Console.MarkupLine("[grey]--->[/]");
        var result = true;
        if (!templates.IgnoreMissing ?? false)
        {
            //disable document template
            Console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[grey]disable document template: {deleteTemplate.Name}[/]");
            result = await service.TryUpdateAsync(new dataverse.DocumentTemplate(deleteTemplate.Id) { Status = Status.Draft },
                cancellationToken);
        }
        else
        {
            Console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[grey]skip not listed document template: {deleteTemplate.Name}[/]");
        }

        return result;
    }

    private async Task<bool> UpdateTemplateAsync(IOrganizationServiceAsync2 service, ImportVerb args, DocumentTemplates templates,
        dataverse.DocumentTemplate updateTemplate, CancellationToken cancellationToken)
    {
        Console.MarkupLine("[grey]--->[/]");
        var existingTemplate = templates.Templates.Single(e =>
            (updateTemplate.Name, updateTemplate.DocumentType?.Value) == (e.Name, (int)e.DocumentType));
        var currentStatus = updateTemplate.Status.GetValueOrDefault(Status.Draft);
        if (currentStatus != existingTemplate.DocumentStatus && currentStatus == Status.Activated) //disable
        {
            Console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[grey]disable document template: {updateTemplate.Name}[/]");
            return await service.TryUpdateAsync(new dataverse.DocumentTemplate(updateTemplate.Id) { Status = Status.Draft },
                cancellationToken);
        }

        var result = true;
        if (existingTemplate.ForceUpdate ?? false)
        {
            Console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[grey]update document template: {updateTemplate.Name}[/]");
            //delete old
            result = await service.TryDeleteAsync(dataverse.DocumentTemplate.EntityLogicalName, updateTemplate.Id, cancellationToken);

            //create
            var template = new dataverse.DocumentTemplate
            {
                DocumentTemplateId = existingTemplate.DocumentTemplateId ?? updateTemplate.Id,
                Name = existingTemplate.Name.Trim().Replace(".xlsx", "", StringComparison.Ordinal).Replace(".docx", "", StringComparison.Ordinal),
                DocumentType = existingTemplate.DocumentType == DocumentTemplate.DocumentTemplateType.MicrosoftExcel
                    ? new OptionSetValue(DocumentType.MicrosoftExcel)
                    : new OptionSetValue(DocumentType.MicrosoftWord),
                Description = existingTemplate.Description ?? updateTemplate.Description,
                LanguageCode = existingTemplate.LanguageCode ?? updateTemplate.LanguageCode,
                Content = await GetContentAsync(service, args, existingTemplate.File, existingTemplate.DocumentType,
                    existingTemplate.AssociatedEntityTypeCode, cancellationToken)
            };
            var (created, id) = await TryCreateTemplateAsync(service, template, cancellationToken);
            result = created && result;
            updateTemplate.Id = id;
        }
        else
        {
            if (updateTemplate.Description != existingTemplate.Description)
            {
                Console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[grey]update document template: {updateTemplate.Name}[/]");
                var template = new dataverse.DocumentTemplate(updateTemplate.Id)
                {
                    Description = existingTemplate.Description
                };
                result = await service.TryUpdateAsync(template, cancellationToken);
            }
        }

        return await SetTemplateStatusAsync(service, existingTemplate, updateTemplate.Id, cancellationToken) && result;
    }

    private static async Task<int> GetEntityTypeCodeAsync(IOrganizationServiceAsync2 service, string entity,
        CancellationToken cancellationToken)
    {
        var (succeeded, response) = await service.TryExecuteAsync<RetrieveEntityRequest, RetrieveEntityResponse>(
            new RetrieveEntityRequest { LogicalName = entity, EntityFilters = EntityFilters.Entity }, cancellationToken);
        if (!succeeded || response == null)
        {
            throw new InvalidOperationException($"Could not retrieve metadata for {nameof(entity)} '{entity}'.");
        }

        // ReSharper disable once PossibleInvalidOperationException
        return response.EntityMetadata.ObjectTypeCode!.Value;
    }

    private async Task<string> GetContentAsync(IOrganizationServiceAsync2 service, ImportVerb args, string templateFile,
        DocumentTemplate.DocumentTemplateType type, string entity, CancellationToken cancellationToken)
    {
        var fileName = Path.GetFileName(templateFile);
        byte[] obj;
        try
        {
            var file = Path.Combine(Directory.GetCurrentDirectory(), args.FileDir,
                Path.GetDirectoryName(templateFile) ?? "", fileName);
            if (!ReadDocument(file, out obj))
            {
                file = Path.Combine(Directory.GetCurrentDirectory(), args.FileDir, fileName);
                if (!ReadDocument(file, out obj))
                {
                    file = Path.Combine(Directory.GetCurrentDirectory(), args.FileDir, fileName);
                    if (!ReadDocument(file, out obj))
                    {
                        file = Path.Combine(Directory.GetCurrentDirectory(), file);
                        if (!ReadDocument(file, out obj))
                        {
                            throw new FileNotFoundException("Can't find file...", file);
                        }
                    }
                }
            }

            if (DocumentTemplate.DocumentTemplateType.MicrosoftWord == type)
            {
                using var memoryStream = new MemoryStream();
                await memoryStream.WriteAsync(obj, cancellationToken);
                memoryStream.Position = 0;

                using (var doc = WordprocessingDocument.Open(memoryStream, true,
                           new OpenSettings { AutoSave = true }))
                {
                    var mainDocumentPart = doc.MainDocumentPart;
                    if (mainDocumentPart?.Document == null)
                    {
                        throw new InvalidDataException("Word template does not contain a main document part.");
                    }

                    var etc = await GetEntityTypeCodeAsync(service, entity, cancellationToken);
                    var matcher = $"({entity}/" + @"\d{1,5})";
                    var replacement = $"{entity}/{etc}";
                    var match = Regex.Match(mainDocumentPart.Document.InnerXml, matcher,
                        RegexOptions.CultureInvariant | RegexOptions.Multiline);

                    while (match.Success)
                    {
                        foreach (Group group in match.Groups)
                        {
                            foreach (Capture capture in group.Captures)
                            {
                                if (capture.Value != replacement)
                                {
                                    Console.WriteLine($"Replace InnerXml etc: {capture} -> {replacement}");
                                    mainDocumentPart.Document.InnerXml =
                                        mainDocumentPart.Document.InnerXml.Replace(capture.Value, replacement, StringComparison.Ordinal);
                                }
                            }
                        }

                        match = match.NextMatch();
                    }

                    // replace type code in CustomXml
                    mainDocumentPart.CustomXmlParts.ToList()
                        .ForEach(part => ReplacePatternInPart(part, matcher, replacement));

                    // replace type code in Footer
                    mainDocumentPart.FooterParts.ToList()
                        .ForEach(part => ReplacePatternInPart(part, matcher, replacement));

                    // replace type code in Header
                    mainDocumentPart.HeaderParts.ToList()
                        .ForEach(part => ReplacePatternInPart(part, matcher, replacement));
                }

                obj = memoryStream.ToArray();
            }
        }
#pragma warning disable CA1031 // Intentional: log any error before rethrowing to preserve original exception
        catch (Exception e)
        {
            Console.MarkupLine($"[red]{Spectre.Console.Markup.Escape(e.Message)}[/]");
            throw;
        }
#pragma warning restore CA1031

        return Convert.ToBase64String(obj);
    }

    private void ReplacePatternInPart(OpenXmlPart part, string matcher, string replacement)
    {
        using var reader = new StreamReader(part.GetStream());
        var xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + XDocument.Load(reader);
        var match = Regex.Match(xml, matcher, RegexOptions.CultureInvariant | RegexOptions.Multiline);
        while (match.Success)
        {
            foreach (Group group in match.Groups)
            {
                foreach (Capture capture in group.Captures)
                {
                    if (capture.Value != replacement)
                    {
                        Console.WriteLine($"Replace '${part.Uri}' etc: {capture} -> {replacement}");
                        xml = xml.Replace(capture.Value, replacement, StringComparison.Ordinal);
                        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml));
                        part.FeedData(stream);
                    }
                }
            }

            match = match.NextMatch();
        }
    }

    private bool ReadDocument(string file, out byte[] obj)
    {
        if (File.Exists(file))
        {
            Console.WriteLine($"Read file {file}");
            obj = File.ReadAllBytes(file);
            return true;
        }

        Console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[yellow]File {file} not found[/]");
        obj = Array.Empty<byte>();
        return false;
    }
}
