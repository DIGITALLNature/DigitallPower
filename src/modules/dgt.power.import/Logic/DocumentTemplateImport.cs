﻿using System.Diagnostics;
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

namespace dgt.power.import.Logic;

public sealed class DocumentTemplateImport : BaseImport
{
    public DocumentTemplateImport(ITracer tracer, IOrganizationService connection, IConfigResolver configResolver) : base(
        tracer, connection, configResolver)
    {
    }

    protected override bool Invoke(ImportVerb args)
    {
        Tracer.Start(this);
        var fileName = string.IsNullOrWhiteSpace(args.FileName) ? "documenttemplate.json" : args.FileName;


        if (!ConfigResolver.GetConfigFile<DocumentTemplates>(args.FileDir, fileName, out var templates))
        {
            return Tracer.NotConfigured(this);
        }

        //anything to do?
        if (!templates.Templates.Any())
        {
            return Tracer.NotConfigured(this);
        }

        var result = true;

        using var context = new DataContext(Connection) { MergeOption = MergeOption.NoTracking };

        var documentTemplates = (from dt in context.DocumentTemplateSet
            //where dt.Status == Model.DocumentTemplate.Options.Status.On
            orderby dt.Name
            select dt).ToList();

        //find document template which need to be disabled (missing)
        Tracer.Log("disable check", TraceEventType.Information);
        foreach (var deleteTemplate in documentTemplates.Where(e =>
                     templates.Templates.All(c => (e.Name, e.DocumentType?.Value) != (c.Name, (int)c.DocumentType))))
        {
            result = DisableTemplate(templates, deleteTemplate, result);
        }

        //find queues need to be updated
        Tracer.Log("update check", TraceEventType.Information);
        foreach (var updateTemplate in documentTemplates.Where(e =>
                     templates.Templates.Any(c => (e.Name, e.DocumentType?.Value) == (c.Name, (int)c.DocumentType))))
        {
            result = UpdateTemplate(args, templates, updateTemplate, result);
        }

        //find bulk deletes which need to be created
        Tracer.Log("create check", TraceEventType.Information);
        foreach (var createTemplate in templates.Templates.Where(e =>
                     documentTemplates.All(c => (c.Name, c.DocumentType?.Value) != (e.Name, (int)e.DocumentType))))
        {
            result = CreateTemplate(args, createTemplate, result);
        }

        return Tracer.End(this, result);
    }

    private bool CreateTemplate(ImportVerb args, DocumentTemplate createTemplate, bool result)
    {
        Tracer.Log("--->", TraceEventType.Verbose);
        var name = createTemplate.Name.Trim().Replace(".xlsx", "").Replace(".docx", "");
        Tracer.Log($"create document template: {name}", TraceEventType.Verbose);
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
            Content = GetContent(args, createTemplate.File, createTemplate.DocumentType,
                createTemplate.AssociatedEntityTypeCode)
        };
        result = Connection.TryCreate(template, out var id) & result;
        result = SetTemplateStatus(createTemplate, result, id);

        return result;
    }

    private bool SetTemplateStatus(DocumentTemplate template, bool result, Guid id)
    {
        if ((template.DocumentStatus ?? false) == Status.Activated) //enable
        {
            result = Connection.TrySetStateDocumentTemplate(
                new EntityReference(dataverse.DocumentTemplate.EntityLogicalName, id),
                Status.Activated) & result;
        }
        else if ((template.DocumentStatus ?? false) == Status.Draft) //disable
        {
            result = Connection.TrySetStateDocumentTemplate(
                new EntityReference(dataverse.DocumentTemplate.EntityLogicalName, id),
                Status.Draft) & result;
        }

        return result;
    }

    private bool DisableTemplate(DocumentTemplates templates, dataverse.DocumentTemplate deleteTemplate, bool result)
    {
        Tracer.Log("--->", TraceEventType.Verbose);
        if (!templates.IgnoreMissing ?? false)
        {
            //disable document template
            Tracer.Log($"disable document template: {deleteTemplate.Name}", TraceEventType.Verbose);
            result = Connection.TrySetStateDocumentTemplate(deleteTemplate.ToEntityReference(), Status.Draft) &
                     result;
        }
        else
        {
            Tracer.Log($"skip not listed document template: {deleteTemplate.Name}", TraceEventType.Verbose);
        }

        return result;
    }

    private bool UpdateTemplate(ImportVerb args, DocumentTemplates templates, dataverse.DocumentTemplate updateTemplate,
        bool result)
    {
        Tracer.Log("--->", TraceEventType.Verbose);
        var existingTemplate = templates.Templates.Single(e =>
            (updateTemplate.Name, updateTemplate.DocumentType?.Value) == (e.Name, (int)e.DocumentType));
        var currentStatus = updateTemplate.Status.GetValueOrDefault(Status.Draft);
        if (currentStatus != existingTemplate.DocumentStatus && currentStatus == Status.Activated) //disable
        {
            Tracer.Log($"disable document template: {updateTemplate.Name}", TraceEventType.Verbose);
            result = Connection.TrySetStateDocumentTemplate(updateTemplate.ToEntityReference(), Status.Draft) &
                     result;
            return result;
        }

        if (existingTemplate.ForceUpdate ?? false)
        {
            Tracer.Log($"update document template: {updateTemplate.Name}", TraceEventType.Verbose);
            //delete old
            result =
                Connection.TryDelete(dataverse.DocumentTemplate.EntityLogicalName, updateTemplate.Id) &
                result;

            //create
            var template = new dataverse.DocumentTemplate
            {
                DocumentTemplateId = existingTemplate.DocumentTemplateId ?? updateTemplate.Id,
                Name = existingTemplate.Name.Trim().Replace(".xlsx", "").Replace(".docx", ""),
                DocumentType = existingTemplate.DocumentType == DocumentTemplate.DocumentTemplateType.MicrosoftExcel
                    ? new OptionSetValue(DocumentType.MicrosoftExcel)
                    : new OptionSetValue(DocumentType.MicrosoftWord),
                Description = existingTemplate.Description ?? updateTemplate.Description,
                LanguageCode = existingTemplate.LanguageCode ?? updateTemplate.LanguageCode,
                Content = GetContent(args, existingTemplate.File, existingTemplate.DocumentType,
                    existingTemplate.AssociatedEntityTypeCode)
            };
            result = Connection.TryCreate(template, out var id) & result;
            updateTemplate.Id = id;
        }
        else
        {
            if (updateTemplate.Description != existingTemplate.Description)
            {
                Tracer.Log($"update document template: {updateTemplate.Name}", TraceEventType.Verbose);
                var template = new dataverse.DocumentTemplate(updateTemplate.Id)
                {
                    Description = existingTemplate.Description
                };
                result = Connection.TryUpdate(template) & result;
            }
        }

        result = SetTemplateStatus(existingTemplate, result, updateTemplate.Id);

        return result;
    }

    private int GetEntityTypeCode(string entity)
    {
        Connection.TryExecute<RetrieveEntityRequest, RetrieveEntityResponse>(
            new RetrieveEntityRequest { LogicalName = entity, EntityFilters = EntityFilters.Entity }, out var response);
        // ReSharper disable once PossibleInvalidOperationException
        return response.EntityMetadata.ObjectTypeCode!.Value;
    }

    private string GetContent(ImportVerb args, string templateFile, DocumentTemplate.DocumentTemplateType type,
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
                    var etc = GetEntityTypeCode(entity);
                    var matcher = $"({entity}/" + @"\d{1,5})";
                    var replacement = $"{entity}/{etc}";
                    var match = Regex.Match(doc.MainDocumentPart!.Document.InnerXml, matcher,
                        RegexOptions.CultureInvariant | RegexOptions.Multiline);

                    while (match.Success)
                    {
                        foreach (Group group in match.Groups)
                        {
                            foreach (Capture capture in group.Captures)
                            {
                                if (capture.Value != replacement)
                                {
                                    Tracer.Log($"Replace InnerXml etc: {capture} -> {replacement}",
                                        TraceEventType.Information);
                                    doc.MainDocumentPart.Document.InnerXml =
                                        doc.MainDocumentPart.Document.InnerXml.Replace(capture.Value, replacement);
                                }
                            }
                        }

                        match = match.NextMatch();
                    }

                    // replace type code in CustomXml
                    doc.MainDocumentPart.CustomXmlParts.ToList()
                        .ForEach(part => ReplacePatternInPart(part, matcher, replacement));

                    // replace type code in Footer
                    doc.MainDocumentPart.FooterParts.ToList()
                        .ForEach(part => ReplacePatternInPart(part, matcher, replacement));

                    // replace type code in Header
                    doc.MainDocumentPart.HeaderParts.ToList()
                        .ForEach(part => ReplacePatternInPart(part, matcher, replacement));
                }

                obj = memoryStream.ToArray();
            }
        }
        catch (Exception e)
        {
            Tracer.Log(e.Message, TraceEventType.Error);
            throw;
        }

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
                        Tracer.Log($"Replace '${part.Uri}' etc: {capture} -> {replacement}",
                            TraceEventType.Information);
                        xml = xml.Replace(capture.Value, replacement);
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
            Tracer.Log($"Read file {file}", TraceEventType.Information);
            obj = File.ReadAllBytes(file);
            return true;
        }

        Tracer.Log($"File {file} not found", TraceEventType.Warning);
        obj = Array.Empty<byte>();
        return false;
    }
}
