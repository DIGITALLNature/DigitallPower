// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using dgt.power.common;
using dgt.power.common.Extensions;
using dgt.power.dataverse;
using dgt.power.dto;
using dgt.power.import.Base;
using DocumentFormat.OpenXml.Packaging;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
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
    protected override async Task<bool> RunAsync([NotNull] ImportVerb args, CancellationToken cancellationToken)
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

        var service = (IOrganizationService)await connection.GetServiceAsync();
        using var context = new DataContext(service) { MergeOption = MergeOption.NoTracking };

        var documentTemplates = (from dt in context.DocumentTemplateSet
            //where dt.Status == Model.DocumentTemplate.Options.Status.On
            orderby dt.Name
            select dt).ToList();

        //find document template which need to be disabled (missing)
        Console.WriteLine("disable check");
        foreach (var deleteTemplate in documentTemplates.Where(e =>
                     templates.Templates.All(c => (e.Name, e.DocumentType?.Value) != (c.Name, (int)c.DocumentType))))
        {
            result = DisableTemplate(service, templates, deleteTemplate) && result;
        }

        //find queues need to be updated
        Console.WriteLine("update check");
        foreach (var updateTemplate in documentTemplates.Where(e =>
                     templates.Templates.Any(c => (e.Name, e.DocumentType?.Value) == (c.Name, (int)c.DocumentType))))
        {
            result = UpdateTemplate(service, args, templates, updateTemplate) && result;
        }

        //find bulk deletes which need to be created
        Console.WriteLine("create check");
        foreach (var createTemplate in templates.Templates.Where(e =>
                     documentTemplates.TrueForAll(c => (c.Name, c.DocumentType?.Value) != (e.Name, (int)e.DocumentType))))
        {
            result = CreateTemplate(service, args, createTemplate)&& result;
        }

        return result;
    }

    private bool CreateTemplate(IOrganizationService service, ImportVerb args, DocumentTemplate createTemplate)
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
            Content = GetContent(service, args, createTemplate.File, createTemplate.DocumentType,
                createTemplate.AssociatedEntityTypeCode)
        };
        var result = service.TryCreate(template, out var id);
        return SetTemplateStatus(service, createTemplate, id) && result;
    }

    private static bool SetTemplateStatus(IOrganizationService service, DocumentTemplate template, Guid id)
    {
        var result = true;
        if ((template.DocumentStatus ?? false) == Status.Activated) //enable
        {
            result = service.TrySetStateDocumentTemplate(
                new EntityReference(dataverse.DocumentTemplate.EntityLogicalName, id),
                Status.Activated);
        }
        else if ((template.DocumentStatus ?? false) == Status.Draft) //disable
        {
            result = service.TrySetStateDocumentTemplate(
                new EntityReference(dataverse.DocumentTemplate.EntityLogicalName, id),
                Status.Draft);
        }

        return result;
    }

    private bool DisableTemplate(IOrganizationService service, DocumentTemplates templates, dataverse.DocumentTemplate deleteTemplate)
    {
        Console.MarkupLine("[grey]--->[/]");
        var result = true;
        if (!templates.IgnoreMissing ?? false)
        {
            //disable document template
            Console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[grey]disable document template: {deleteTemplate.Name}[/]");
            result = service.TrySetStateDocumentTemplate(deleteTemplate.ToEntityReference(), Status.Draft);
        }
        else
        {
            Console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[grey]skip not listed document template: {deleteTemplate.Name}[/]");
        }

        return result;
    }

    private bool UpdateTemplate(IOrganizationService service, ImportVerb args, DocumentTemplates templates, dataverse.DocumentTemplate updateTemplate)
    {
        Console.MarkupLine("[grey]--->[/]");
        var existingTemplate = templates.Templates.Single(e =>
            (updateTemplate.Name, updateTemplate.DocumentType?.Value) == (e.Name, (int)e.DocumentType));
        var currentStatus = updateTemplate.Status.GetValueOrDefault(Status.Draft);
        if (currentStatus != existingTemplate.DocumentStatus && currentStatus == Status.Activated) //disable
        {
            Console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[grey]disable document template: {updateTemplate.Name}[/]");
            return service.TrySetStateDocumentTemplate(updateTemplate.ToEntityReference(), Status.Draft);
        }

        var result = true;
        if (existingTemplate.ForceUpdate ?? false)
        {
            Console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[grey]update document template: {updateTemplate.Name}[/]");
            //delete old
            result = service.TryDelete(dataverse.DocumentTemplate.EntityLogicalName, updateTemplate.Id);

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
                Content = GetContent(service, args, existingTemplate.File, existingTemplate.DocumentType,
                    existingTemplate.AssociatedEntityTypeCode)
            };
            result = service.TryCreate(template, out var id) && result;
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
                result = service.TryUpdate(template);
            }
        }

        return SetTemplateStatus(service, existingTemplate, updateTemplate.Id) && result;
    }

    private static int GetEntityTypeCode(IOrganizationService service, string entity)
    {
        service.TryExecute<RetrieveEntityRequest, RetrieveEntityResponse>(
            new RetrieveEntityRequest { LogicalName = entity, EntityFilters = EntityFilters.Entity }, out var response);
        // ReSharper disable once PossibleInvalidOperationException
        return response.EntityMetadata.ObjectTypeCode!.Value;
    }

    private string GetContent(IOrganizationService service, ImportVerb args, string templateFile, DocumentTemplate.DocumentTemplateType type,
        string entity)
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
                memoryStream.Write(obj, 0, obj.Length);
                memoryStream.Position = 0;

                using (var doc = WordprocessingDocument.Open(memoryStream, true,
                           new OpenSettings { AutoSave = true }))
                {
                    var mainDocumentPart = doc.MainDocumentPart;
                    if (mainDocumentPart?.Document == null)
                    {
                        throw new InvalidDataException("Word template does not contain a main document part.");
                    }

                    var etc = GetEntityTypeCode(service, entity);
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
