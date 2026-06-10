// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using System.Xml.Linq;
using dgt.power.common;
using dgt.power.common.Extensions;
using dgt.power.dataverse;
using dgt.power.dto;
using dgt.power.import.Base;
using Microsoft.Crm.Sdk;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Extensions.Configuration;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using Spectre.Console;
using SavedQuery = dgt.power.dataverse.SavedQuery;

namespace dgt.power.import.Logic;

public sealed class OutlookTemplateImport(
    ITracer tracer,
    IOrganizationService connection,
    IConfigResolver configResolver,
    IConfiguration configuration,
    IAnsiConsole console)
    : BaseImport(tracer, connection, configResolver, console)
{
    private readonly int _sleepTime = configuration.GetValue<int>("pollrate");

    protected override Task<bool> InvokeAsync(ImportVerb args, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(args);
        return InvokeCoreAsync(args, cancellationToken);
    }

    private async Task<bool> InvokeCoreAsync(ImportVerb args, CancellationToken cancellationToken)
    {
        Tracer.Start(this);
        var fileName = string.IsNullOrWhiteSpace(args.FileName) ? "outlooktemplate.json" : args.FileName;

        if (!ConfigResolver.TryGetConfigFile<dto.SavedQuery>(args.FileDir, fileName, out var queries))
        {
            return Tracer.NotConfigured(this);
        }

        var result = true;
        var orgAsync = (IOrganizationServiceAsync2)Connection;

        var savedQueryExpression = new QueryExpression(SavedQuery.EntityLogicalName)
        {
            ColumnSet = new ColumnSet(
                SavedQuery.LogicalNames.Name,
                SavedQuery.LogicalNames.IsDefault,
                SavedQuery.LogicalNames.FetchXml)
        };
        savedQueryExpression.Criteria.AddCondition(
            SavedQuery.LogicalNames.QueryType,
            ConditionOperator.Equal,
            SavedQueryQueryType.OutlookTemplate);
        savedQueryExpression.AddOrder(SavedQuery.LogicalNames.Name, OrderType.Ascending);
        var retrieveResponse = (RetrieveMultipleResponse)await orgAsync.ExecuteAsync(
            new RetrieveMultipleRequest { Query = savedQueryExpression },
            cancellationToken);
        var savedQueries = retrieveResponse.EntityCollection.Entities.Cast<SavedQuery>().ToList();

        foreach (var savedQuery in savedQueries)
        {
            if (queries.DisabledOutlookTemplates.Contains(savedQuery.Name!) && savedQuery.IsDefault == true)
            {
                Tracer.Log($"Deactivate OutlookTemplate {savedQuery.Name}", TraceEventType.Information);
                result = await orgAsync.TryUpdateAsync(new SavedQuery(savedQuery.Id)
                {
                    IsDefault = false
                }, cancellationToken) && result;
            }

            if (!queries.DisabledOutlookTemplates.Contains(savedQuery.Name!) && savedQuery.IsDefault == false)
            {
                Tracer.Log($"Activate OutlookTemplate {savedQuery.Name}", TraceEventType.Information);
                result = await orgAsync.TryUpdateAsync(new SavedQuery(savedQuery.Id)
                {
                    IsDefault = true
                }, cancellationToken) && result;
            }
        }

        if (queries.OutlookTemplates.Count == 0)
        {
            return Tracer.End(this, result);
        }

        foreach (var outlookTemplate in queries.OutlookTemplates)
        {
            result = await UpsertOutlookTemplateAsync(orgAsync, savedQueries, outlookTemplate, cancellationToken) && result;
        }

        return Tracer.End(this, result);
    }

    private async Task<bool> UpsertOutlookTemplateAsync(
        IOrganizationServiceAsync2 orgAsync,
        List<SavedQuery> savedQueries,
        OutlookTemplate outlookTemplate,
        CancellationToken cancellationToken)
    {
        var result = true;
        var savedQuery = savedQueries.Find(e => e.Name == outlookTemplate.Name);
        if (savedQuery == null)
        {
            Tracer.Log($"Create OutlookTemplate {outlookTemplate.Name}", TraceEventType.Information);
            result = await orgAsync.TryCreateAsync(new SavedQuery
            {
                FetchXml = outlookTemplate.FetchXml,
                IsQuickFindQuery = false,
                QueryType = SavedQueryQueryType.OutlookTemplate,
                IsDefault = outlookTemplate.IsDefault,
                ReturnedTypeCode = outlookTemplate.Entity,
                Name = outlookTemplate.Name,
                Description = $"{outlookTemplate.Description} ({DateTime.UtcNow:yyyy-MM-dd HH:mm:ss})"
            }, cancellationToken);
        }
        else
        {
            var current = await VerifyFetchXmlAsync(orgAsync, savedQuery.FetchXml!, cancellationToken);
            var update = await VerifyFetchXmlAsync(orgAsync, outlookTemplate.FetchXml, cancellationToken);

            if (!XmlCompare(XElement.Parse(current), XElement.Parse(update)))
            {
                Tracer.Log($"Update OutlookTemplate {savedQuery.Name}", TraceEventType.Information);
                Tracer.Log("Update OutlookTemplate does not affect existing rule for users!",
                    TraceEventType.Warning);
                result = await orgAsync.TryUpdateAsync(new SavedQuery(savedQuery.Id)
                {
                    IsDefault = false
                }, cancellationToken) && result;
                await Task.Delay(_sleepTime, cancellationToken);
                result = await orgAsync.TryDeleteAsync(SavedQuery.EntityLogicalName, savedQuery.Id, cancellationToken) && result;
                await Task.Delay(_sleepTime, cancellationToken);
                result = await orgAsync.TryCreateAsync(new SavedQuery
                {
                    FetchXml = update,
                    IsQuickFindQuery = false,
                    QueryType = SavedQueryQueryType.OutlookTemplate,
                    IsDefault = outlookTemplate.IsDefault,
                    ReturnedTypeCode = outlookTemplate.Entity,
                    Name = outlookTemplate.Name,
                    Description = $"{outlookTemplate.Description} ({DateTime.UtcNow:yyyy-MM-dd HH:mm:ss})"
                }, cancellationToken) && result;
            }
            else
            {
                Tracer.Log($"Skip OutlookTemplate {savedQuery.Name}", TraceEventType.Information);
            }
        }

        return result;
    }

    private static async Task<string> VerifyFetchXmlAsync(IOrganizationServiceAsync2 orgAsync, string fetchXml, CancellationToken cancellationToken)
    {
        var queryResponse = (FetchXmlToQueryExpressionResponse)await orgAsync.ExecuteAsync(
            new FetchXmlToQueryExpressionRequest { FetchXml = fetchXml },
            cancellationToken);
        var xmlResponse = (QueryExpressionToFetchXmlResponse)await orgAsync.ExecuteAsync(
            new QueryExpressionToFetchXmlRequest { Query = queryResponse.Query },
            cancellationToken);
        return xmlResponse.FetchXml;
    }

    private static bool XmlCompare(XElement primary, XElement secondary)
    {
        if (primary.HasAttributes)
        {
            if (primary.Attributes().Count() != secondary.Attributes().Count())
            {
                return false;
            }

            foreach (var attr in primary.Attributes())
            {
                if (secondary.Attribute(attr.Name.LocalName) == null)
                {
                    return false;
                }

                if (!attr.Value.Equals(secondary.Attribute(attr.Name.LocalName)?.Value.ToUpperInvariant(), StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
            }
        }
        else if (secondary.HasAttributes)
        {
            return false;
        }

        if (primary.HasElements)
        {
            if (primary.Elements().Count() != secondary.Elements().Count())
            {
                return false;
            }

            for (var i = 0; i <= primary.Elements().Count() - 1; i++)
            {
                var primaryElement = primary.Elements().Skip(i).Take(1).Single();
                var secondaryElement = secondary.Elements().Skip(i).Take(1).Single();
                if (!XmlCompare(primaryElement, secondaryElement))
                {
                    return false;
                }
            }
        }
        else if (secondary.HasElements)
        {
            return false;
        }

        return true;
    }
}
