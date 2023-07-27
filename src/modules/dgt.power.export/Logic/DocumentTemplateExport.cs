// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using dgt.power.common;
using dgt.power.common.Extensions;
using dgt.power.common.FileAccess;
using dgt.power.dto;
using dgt.power.export.Base;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using DocumentTemplate = dgt.power.dataverse.DocumentTemplate;

namespace dgt.power.export.Logic;

public sealed class DocumentTemplateExport : BaseExport
{
    public DocumentTemplateExport(ITracer tracer, IOrganizationService connection, IConfigResolver configResolver, IFileService fileService)
        : base(tracer, connection, configResolver, fileService)
    {
    }

    protected override bool Invoke(ExportVerb args)
    {
        Tracer.Start(this);

        var fileDir = args.FileDir;
        var fileName = string.IsNullOrWhiteSpace(args.FileName) ? "documenttemplate.json" : args.FileName;


        var force = true;
        var missing = true;
        var filter = args.InlineData;
        if (!string.IsNullOrWhiteSpace(args.InlineData) && !args.InlineData.StartsWith("<", StringComparison.InvariantCulture))
        {
            var idx = args.InlineData.IndexOf("<", StringComparison.InvariantCulture);
            filter = args.InlineData.Substring(idx);
            foreach (var fragment in args.InlineData.Substring(0, idx).Split(new[] {'|', ',', ';'}, StringSplitOptions.RemoveEmptyEntries))
            {
                var pair = fragment.Split(new[] {'='}, StringSplitOptions.RemoveEmptyEntries);
                if (pair.Length == 2)
                {
                    switch (pair[0].ToLowerInvariant())
                    {
                        case "force":
                            force = bool.Parse(pair[1]);
                            break;
                        case "missing":
                            missing = bool.Parse(pair[1]);
                            break;
                    }
                }
            }
        }

        if (!GetQueryExpression(Tracer, Connection, filter, out var query))
        {
            return Tracer.End(this, false);
        }

        var documentTemplates = Connection.RetrieveMultiple(query).Entities.Select(s => s.ToEntity<DocumentTemplate>()).ToList();

        var export = new List<dto.DocumentTemplate>();

        foreach (var documentTemplate in documentTemplates)
        {
            // ReSharper disable once PossibleInvalidOperationException
            var id = documentTemplate.DocumentTemplateId!.Value;
            var templateName =
                $"{documentTemplate.Name}{(documentTemplate.DocumentType!.Value == DocumentTemplate.Options.DocumentType.MicrosoftExcel ? ".xlsx" : ".docx")}";
            var status = !documentTemplate.Status.GetValueOrDefault(false); //inverted logic, don't ask why
            var template = new dto.DocumentTemplate
            {
                DocumentTemplateId = id,
                Name = documentTemplate.Name!,
                File = templateName, //.xlsx or .docx
                DocumentType = (dto.DocumentTemplate.DocumentTemplateType)documentTemplate.DocumentType.Value,
                DocumentStatus = status,
                LanguageCode = documentTemplate.LanguageCode,
                Description = documentTemplate.Description,
                AssociatedEntityTypeCode = documentTemplate.AssociatedEntityTypeCode!,
                ForceUpdate = force
            };
            export.Add(template);
            Tracer.Log($"Export {HandleExportFile(fileDir, templateName, Convert.FromBase64String(documentTemplate.Content!))} ",
                TraceEventType.Information);
        }

        var json = GetJson(new DocumentTemplates
        {
            IgnoreMissing = missing,
            Templates = export
        });

        Tracer.Log($"Export {HandleExportFile(fileDir, fileName, json)} ", TraceEventType.Information);
        return Tracer.End(this, true);
    }

    private static bool GetQueryExpression(ITracer tracer, IOrganizationService service, string filter, out QueryExpression? query)
    {
        var fetchXml = string.Empty;
        query = default;
        try
        {
            fetchXml = "<fetch no-lock=\"true\" >" +
                       "<entity name=\"documenttemplate\" >" +
                       "<all-attributes/> " +
                       "<filter type=\"and\" >" +
                       "<condition attribute=\"status\" operator=\"eq\" value=\"0\" />" +
                       filter +
                       "</filter>" +
                       "<order attribute=\"name\" />" +
                       "</entity>" +
                       "</fetch>";
            var response = (FetchXmlToQueryExpressionResponse)service.Execute(new FetchXmlToQueryExpressionRequest
            {
                FetchXml = fetchXml
            });
            query = response.Query;
            return true;
        }
        catch (Exception e)
        {
            tracer.Log($"Invalid fetch-xml: {e.RootMessage()}; check fetch: {fetchXml}", TraceEventType.Error);
            return false;
        }
    }
}
