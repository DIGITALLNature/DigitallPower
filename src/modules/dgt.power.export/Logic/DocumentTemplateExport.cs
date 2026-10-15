// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using dgt.power.common;
using dgt.power.common.Extensions;
using dgt.power.common.FileAccess;
using dgt.power.dto;
using dgt.power.export.Base;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Spectre.Console;
using DocumentTemplate = dgt.power.dataverse.DocumentTemplate;

namespace dgt.power.export.Logic;

public sealed class DocumentTemplateExport(
    ICommandSpanFactory spans,
    IDataverseConnection connection,
    IFileService fileService,
    IAnsiConsole console)
    : PowerCommand<ExportVerb>(spans, console)
{
    private static readonly char[] s_separators = ['|', ',', ';'];

    protected override async Task<bool> RunAsync(ExportVerb args, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(args);

        var fileDir = args.FileDir;
        var fileName = string.IsNullOrWhiteSpace(args.FileName) ? "documenttemplate.json" : args.FileName;


        var force = true;
        var missing = true;
        var filter = args.InlineData;
        if (!string.IsNullOrWhiteSpace(args.InlineData) && !args.InlineData.StartsWith('<'))
        {
            var idx = args.InlineData.IndexOf('<', StringComparison.Ordinal);
            var commandData = idx >= 0 ? args.InlineData.Substring(0, idx) : args.InlineData;
            filter = idx >= 0 ? args.InlineData.Substring(idx) : string.Empty;

            foreach (var fragment in commandData.Split(s_separators, StringSplitOptions.RemoveEmptyEntries))
            {
                var pair = fragment.Split('=', StringSplitOptions.RemoveEmptyEntries);
                if (pair.Length == 2)
                {
                    switch (pair[0].ToUpperInvariant())
                    {
                        case "FORCE":
                            force = bool.Parse(pair[1]);
                            break;
                        case "MISSING":
                            missing = bool.Parse(pair[1]);
                            break;
                        default:
                            throw new InvalidOperationException($"Command {pair[0].ToUpperInvariant()} in InlineData is unknown");
                    }
                }
            }
        }

        var service = (IOrganizationService)await connection.GetServiceAsync();
        if (!GetQueryExpression(console, service, filter, out var query))
        {
            return false;
        }

        var documentTemplates = service.RetrieveMultiple(query).Entities.Select(s => s.ToEntity<DocumentTemplate>()).ToList();

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
            console.WriteLine($"Export {fileService.ExportFile(fileDir, templateName, Convert.FromBase64String(documentTemplate.Content!))} ");
        }

        var json = ExportJson.GetJson(new DocumentTemplates
        {
            IgnoreMissing = missing,
            Templates = export
        });

        console.WriteLine($"Export {fileService.ExportFile(fileDir, fileName, json)} ");
        return true;
    }

    private static bool GetQueryExpression(IAnsiConsole console, IOrganizationService service, string filter, out QueryExpression? query)
    {
        var fetchXml = string.Empty;
        query = null;
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
        catch (Exception e) when (e is not OutOfMemoryException and not StackOverflowException)
        {
            console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[red]Invalid fetch-xml: {e.RootMessage()}; check fetch: {fetchXml}[/]");
            return false;
        }
    }
}
