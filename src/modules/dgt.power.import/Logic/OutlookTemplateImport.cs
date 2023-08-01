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
using Microsoft.Xrm.Sdk;
using SavedQuery = dgt.power.dataverse.SavedQuery;

namespace dgt.power.import.Logic;

public sealed class OutlookTemplateImport : BaseImport
{
    public OutlookTemplateImport(ITracer tracer, IOrganizationService connection, IConfigResolver configResolver) : base(
        tracer, connection, configResolver)
    {
    }

    protected override bool Invoke(ImportVerb args)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Tracer.Start(this);
        var fileName = string.IsNullOrWhiteSpace(args.FileName) ? "outlooktemplate.json" : args.FileName;


        if (!ConfigResolver.TryGetConfigFile<dto.SavedQuery>(args.FileDir, fileName, out var queries))
        {
            return Tracer.NotConfigured(this);
        }

        var result = true;

        using var context = new DataContext(Connection);
        var savedQueries = (from sq in context.SavedQuerySet
            where sq.QueryType == SavedQueryQueryType.OutlookTemplate
            orderby sq.Name
            select sq).ToList();

        foreach (var savedQuery in savedQueries)
        {
            if (queries.DisabledOutlookTemplates.Contains(savedQuery.Name!) && savedQuery.IsDefault == true)
            {
                Tracer.Log($"Deactivate OutlookTemplate {savedQuery.Name}", TraceEventType.Information);
                result = Connection.TryUpdate(new SavedQuery(savedQuery.Id)
                {
                    IsDefault = false
                }) && result;
            }

            if (!queries.DisabledOutlookTemplates.Contains(savedQuery.Name!) && savedQuery.IsDefault == false)
            {
                Tracer.Log($"Activate OutlookTemplate {savedQuery.Name}", TraceEventType.Information);
                result = Connection.TryUpdate(new SavedQuery(savedQuery.Id)
                {
                    IsDefault = true
                }) && result;
            }
        }

        //anything to do?
        if (!queries.OutlookTemplates.Any())
        {
            return Tracer.End(this, result);
        }

        foreach (var outlookTemplate in queries.OutlookTemplates)
        {
            result = UpsertOutlookTemplate(savedQueries, outlookTemplate) && result;
        }

        return Tracer.End(this, result);
    }

    private bool UpsertOutlookTemplate(List<SavedQuery> savedQueries, OutlookTemplate outlookTemplate)
    {
        var result = true;
        var savedQuery = savedQueries.Find(e => e.Name == outlookTemplate.Name);
        if (savedQuery == null)
        {
            Tracer.Log($"Create OutlookTemplate {outlookTemplate.Name}", TraceEventType.Information);
            result = Connection.TryCreate(new SavedQuery
            {
                FetchXml = outlookTemplate.FetchXml,
                IsQuickFindQuery = false,
                QueryType = SavedQueryQueryType.OutlookTemplate,
                IsDefault = outlookTemplate.IsDefault,
                ReturnedTypeCode = outlookTemplate.Entity,
                Name = outlookTemplate.Name,
                Description = $"{outlookTemplate.Description} ({DateTime.UtcNow:yyyy-MM-dd HH:mm:ss})"
            }, out _);
        }
        else
        {
            var current = VerifyFetchXml(savedQuery.FetchXml!);
            var update = VerifyFetchXml(outlookTemplate.FetchXml);

            if (!XmlCompare(XElement.Parse(current), XElement.Parse(update)))
            {
                Tracer.Log($"Update OutlookTemplate {savedQuery.Name}", TraceEventType.Information);
                Tracer.Log("Update OutlookTemplate does not affect existing rule for users!",
                    TraceEventType.Warning);
                result = Connection.TryUpdate(new SavedQuery(savedQuery.Id)
                {
                    IsDefault = false
                }) && result;
                Thread.Sleep(5000);
                result = Connection.TryDelete(SavedQuery.EntityLogicalName, savedQuery.Id) && result;
                Thread.Sleep(5000);
                result = Connection.TryCreate(new SavedQuery
                {
                    FetchXml = update,
                    IsQuickFindQuery = false,
                    QueryType = SavedQueryQueryType.OutlookTemplate,
                    IsDefault = outlookTemplate.IsDefault,
                    ReturnedTypeCode = outlookTemplate.Entity,
                    Name = outlookTemplate.Name,
                    Description = $"{outlookTemplate.Description} ({DateTime.UtcNow:yyyy-MM-dd HH:mm:ss})"
                }, out _) && result;
            }
            else
            {
                Tracer.Log($"Skip OutlookTemplate {savedQuery.Name}", TraceEventType.Information);
            }
        }

        return result;
    }

    private string VerifyFetchXml(string current)
    {
        var queryResponse = (FetchXmlToQueryExpressionResponse)Connection.Execute(new FetchXmlToQueryExpressionRequest
        {
            FetchXml = current
        });
        var xmlResponse = (QueryExpressionToFetchXmlResponse)Connection.Execute(new QueryExpressionToFetchXmlRequest
        {
            Query = queryResponse.Query
        });
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

                if (attr.Value.ToUpperInvariant() != secondary.Attribute(attr.Name.LocalName)?.Value.ToUpperInvariant())
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
