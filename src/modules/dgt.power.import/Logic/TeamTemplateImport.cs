// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using dgt.power.common;
using dgt.power.common.Extensions;
using dgt.power.dataverse;
using dgt.power.import.Base;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Spectre.Console;
using TeamTemplate = dgt.power.dto.TeamTemplate;

namespace dgt.power.import.Logic;

public sealed class TeamTemplateImport(
    ICommandSpanFactory spans,
    IDataverseConnection connection,
    IConfigResolver configResolver,
    IAnsiConsole console)
    : PowerCommand<ImportVerb>(spans, console)
{
    protected override async Task<bool> RunAsync([NotNull] ImportVerb args, CancellationToken cancellationToken)
    {
        var fileName = string.IsNullOrWhiteSpace(args.FileName) ? "teamtemplate.json" : args.FileName;

        if (!configResolver.TryGetConfigFile<List<TeamTemplate>>(args.FileDir, fileName, out var templates))
        {
            Console.MarkupLine("[red]Team template configuration not found.[/]");
            return false;
        }

        //anything to do?
        if (templates.Count == 0)
        {
            Console.MarkupLine("[red]Team template configuration is empty.[/]");
            return false;
        }

        var result = true;

        var service = (IOrganizationService)await connection.GetServiceAsync();
        var request = new RetrieveAllEntitiesRequest
        {
            EntityFilters = EntityFilters.Entity
        };
        var response = (RetrieveAllEntitiesResponse)service.Execute(request);
        var entities = response.EntityMetadata.ToList();

        using var context = new DataContext(service);
        var teamTemplates = (from tt in context.TeamTemplateSet
            orderby tt.TeamTemplateName
            select tt).ToList();

        //find templates which need to be removed
        Console.WriteLine("delete check");
        foreach (var teamTemplate in teamTemplates.Where(e => templates.TrueForAll(c => e.Id != c.TeamTemplateId)))
        {
            result = DeleteTeamTemplate(service, teamTemplate) && result;
        }

        //find templates which need to be updated
        Console.WriteLine("update check");
        foreach (var updateTemplate in templates.Where(c => teamTemplates.Exists(e => e.Id == c.TeamTemplateId)))
        {
            result = UpdateTeamTemplate(service, teamTemplates, updateTemplate, entities) && result;
        }

        //find templates which need to be created
        Console.WriteLine("create check");
        foreach (var newTemplate in templates.Where(c => teamTemplates.TrueForAll(e => e.Id != c.TeamTemplateId)))
        {
            result = CreateTeamTemplate(service, entities, newTemplate) && result;
        }

        return result;
    }

    private bool CreateTeamTemplate(IOrganizationService service, List<EntityMetadata> entities, TeamTemplate newTemplate)
    {
        var entity = entities.Find(e => e.LogicalName == newTemplate.Entity);

        if (entity?.ObjectTypeCode == null)
        {
            Console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[grey]unknown template entity: {newTemplate.Entity}[/]");
            return false;
        }

        if (entity.AutoCreateAccessTeams == null)
        {
            Console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[grey]access teams not enabled template entity: {newTemplate.Entity}[/]");
            return false;
        }

        //create template
        Console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[grey]create template: {newTemplate.TeamTemplateName}[/]");
        return service.TryCreate(new dataverse.TeamTemplate(newTemplate.TeamTemplateId)
        {
            TeamTemplateName = newTemplate.TeamTemplateName,
            Description = newTemplate.Description,
            DefaultAccessRightsMask = newTemplate.DefaultAccessRightsMask,
            ObjectTypeCode = entity.ObjectTypeCode
        }, out _);
    }

    private bool UpdateTeamTemplate(IOrganizationService service, IEnumerable<dataverse.TeamTemplate> teamTemplates, TeamTemplate updateTemplate,
        List<EntityMetadata> entities)
    {
        var existingTemplate = teamTemplates.Single(e => e.Id == updateTemplate.TeamTemplateId);

        var entity = entities.Find(e => e.LogicalName == updateTemplate.Entity)?.ObjectTypeCode;

        if (entity == null)
        {
            Console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[grey]unknown template entity: {updateTemplate.Entity}[/]");
            return false;
        }

        var unchangedRule = Unchanged(existingTemplate, entity.Value, updateTemplate);

        var result = true;
        if (unchangedRule)
        {
            Console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[grey]unchanged template: {updateTemplate.TeamTemplateName}[/]");
        }
        else
        {
            //update template
            Console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[grey]update template: {updateTemplate.TeamTemplateName}[/]");
            result = service.TryUpdate(new dataverse.TeamTemplate(existingTemplate.Id)
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

    private bool DeleteTeamTemplate(IOrganizationService service, dataverse.TeamTemplate teamTemplate)
    {
        //delete template
        Console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[grey]delete template: {teamTemplate.TeamTemplateName}[/]");
        return service.TryDelete(dataverse.TeamTemplate.EntityLogicalName, teamTemplate.Id);
    }

    private static bool Unchanged(dataverse.TeamTemplate existing, int entity, TeamTemplate config)
    {
        if (!HasSameText(existing.TeamTemplateName, config.TeamTemplateName))
        {
            return false;
        }

        if (!HasSameText(existing.Description, config.Description))
        {
            return false;
        }

        if (!HasSameNullableValue(existing.DefaultAccessRightsMask, config.DefaultAccessRightsMask))
        {
            return false;
        }

        return HasSameValue(existing.ObjectTypeCode, entity);
    }

    private static bool HasSameText(string? current, string? expected) =>
        string.Equals(current, expected, StringComparison.Ordinal);

    private static bool HasSameNullableValue<T>(T? current, T? expected)
        where T : struct =>
        Nullable.Equals(current, expected);

    private static bool HasSameValue<T>(T? current, T expected)
        where T : struct =>
        current.HasValue && EqualityComparer<T>.Default.Equals(current.Value, expected);
}
