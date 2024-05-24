// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using dgt.power.common;
using dgt.power.common.Extensions;
using dgt.power.dataverse;
using dgt.power.dto;
using dgt.power.import.Base;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using TeamTemplate = dgt.power.dto.TeamTemplate;

namespace dgt.power.import.Logic;

public sealed class TeamTemplateImport : BaseImport
{
    public TeamTemplateImport(ITracer tracer, IOrganizationService connection, IConfigResolver configResolver) : base(tracer, connection, configResolver)
    {
    }

    protected override bool Invoke(ImportVerb args)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Tracer.Start(this);
        var fileName = string.IsNullOrWhiteSpace(args.FileName) ? "teamtemplate.json" : args.FileName;

        if (!ConfigResolver.TryGetConfigFile<TeamTemplates>(args.FileDir, fileName, out var templates))
        {
            return Tracer.NotConfigured(this);
        }

        //anything to do?
        if (!templates.Any())
        {
            return Tracer.NotConfigured(this);
        }

        var result = true;

        var request = new RetrieveAllEntitiesRequest
        {
            EntityFilters = EntityFilters.Entity
        };
        var response = (RetrieveAllEntitiesResponse)Connection.Execute(request);
        var entities = response.EntityMetadata.ToList();

        using var context = new DataContext(Connection);
        var teamTemplates = (from tt in context.TeamTemplateSet
            orderby tt.TeamTemplateName
            select tt).ToList();

        //find templates which need to be removed
        Tracer.Log("delete check", TraceEventType.Information);
        foreach (var teamTemplate in teamTemplates.Where(e => templates.TrueForAll(c => e.Id != c.TeamTemplateId)))
        {
            result = DeleteTeamTemplate(teamTemplate) && result;
        }

        //find templates which need to be updated
        Tracer.Log("update check", TraceEventType.Information);
        foreach (var updateTemplate in templates.Where(c => teamTemplates.Exists(e => e.Id == c.TeamTemplateId)))
        {
            result = UpdateTeamTemplate(teamTemplates, updateTemplate, entities) && result;
        }

        //find templates which need to be created
        Tracer.Log("create check", TraceEventType.Information);
        foreach (var newTemplate in templates.Where(c => teamTemplates.TrueForAll(e => e.Id != c.TeamTemplateId)))
        {
            result = CreateTeamTemplate(entities, newTemplate) && result;
        }

        return Tracer.End(this, result);
    }

    private bool CreateTeamTemplate(List<EntityMetadata> entities, TeamTemplate newTemplate)
    {
        var entity = entities.Find(e => e.LogicalName == newTemplate.Entity);

        if (entity?.ObjectTypeCode == null)
        {
            Tracer.Log($"unknown template entity: {newTemplate.Entity}", TraceEventType.Verbose);
            return false;
        }

        if (entity.AutoCreateAccessTeams == null)
        {
            Tracer.Log($"access teams not enabled template entity: {newTemplate.Entity}", TraceEventType.Verbose);
            return false;
        }

        //create template
        Tracer.Log($"create template: {newTemplate.TeamTemplateName}", TraceEventType.Verbose);
        return Connection.TryCreate(new dataverse.TeamTemplate(newTemplate.TeamTemplateId)
        {
            TeamTemplateName = newTemplate.TeamTemplateName,
            Description = newTemplate.Description,
            DefaultAccessRightsMask = newTemplate.DefaultAccessRightsMask,
            ObjectTypeCode = entity.ObjectTypeCode
        }, out _);
    }

    private bool UpdateTeamTemplate(IEnumerable<dataverse.TeamTemplate> teamTemplates, TeamTemplate updateTemplate, List<EntityMetadata> entities)
    {
        var existingTemplate = teamTemplates.Single(e => e.Id == updateTemplate.TeamTemplateId);

        var entity = entities.Find(e => e.LogicalName == updateTemplate.Entity)?.ObjectTypeCode;

        if (entity == null)
        {
            Tracer.Log($"unknown template entity: {updateTemplate.Entity}", TraceEventType.Verbose);
            return false;
        }

        var unchangedRule = Unchanged(existingTemplate, entity.Value, updateTemplate);

        var result = true;
        if (unchangedRule)
        {
            Tracer.Log($"unchanged template: {updateTemplate.TeamTemplateName}", TraceEventType.Verbose);
        }
        else
        {
            //update template
            Tracer.Log($"update template: {updateTemplate.TeamTemplateName}", TraceEventType.Verbose);
            result = Connection.TryUpdate(new dataverse.TeamTemplate(existingTemplate.Id)
            {
                TeamTemplateName = updateTemplate.TeamTemplateName,
                Description = updateTemplate.Description,
                //IsSystem = updateTemplate.IsSystem,
                DefaultAccessRightsMask = updateTemplate.DefaultAccessRightsMask,
                ObjectTypeCode = entity
            });
        }

        return result;
    }

    private bool DeleteTeamTemplate(dataverse.TeamTemplate teamTemplate)
    {
        //delete template
        Tracer.Log($"delete template: {teamTemplate.TeamTemplateName}", TraceEventType.Verbose);
        return Connection.TryDelete(dataverse.TeamTemplate.EntityLogicalName, teamTemplate.Id);
    }

    private static bool Unchanged(dataverse.TeamTemplate existing, int entity, TeamTemplate config)
    {
        if (config == null)
        {
            return false;
        }

        return
            (
                existing.TeamTemplateName == config.TeamTemplateName ||
                (existing.TeamTemplateName != null &&
                 existing.TeamTemplateName.Equals(config.TeamTemplateName, StringComparison.Ordinal))
            ) &&
            (
                existing.Description == config.Description ||
                (existing.Description != null &&
                 existing.Description.Equals(config.Description, StringComparison.Ordinal))
            ) &&
            (
                existing.DefaultAccessRightsMask == config.DefaultAccessRightsMask ||
                (existing.DefaultAccessRightsMask != null &&
                 existing.DefaultAccessRightsMask.Equals(config.DefaultAccessRightsMask))
            ) &&
            (
                existing.ObjectTypeCode == entity ||
                (existing.ObjectTypeCode != null &&
                 existing.ObjectTypeCode.Equals(entity))
            );
    }
}
