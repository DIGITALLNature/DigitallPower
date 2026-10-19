// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using dgt.power.common;
using dgt.power.common.Extensions;
using dgt.power.dataverse;
using dgt.power.import.Base;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
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
    protected override async Task<bool> RunAsync(ImportVerb args, CancellationToken cancellationToken)
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

        var service = await connection.GetServiceAsync();
        var request = new RetrieveAllEntitiesRequest
        {
            EntityFilters = EntityFilters.Entity
        };
        var response = (RetrieveAllEntitiesResponse)await service.ExecuteAsync(request, cancellationToken);
        var entities = response.EntityMetadata.ToList();

        var query = new QueryExpression(dataverse.TeamTemplate.EntityLogicalName)
        {
            NoLock = true,
            ColumnSet = new ColumnSet(
                dataverse.TeamTemplate.LogicalNames.TeamTemplateId,
                dataverse.TeamTemplate.LogicalNames.TeamTemplateName,
                dataverse.TeamTemplate.LogicalNames.Description,
                dataverse.TeamTemplate.LogicalNames.DefaultAccessRightsMask,
                dataverse.TeamTemplate.LogicalNames.ObjectTypeCode),
            Orders = { new OrderExpression(dataverse.TeamTemplate.LogicalNames.TeamTemplateName, OrderType.Ascending) }
        };
        var teamTemplates = (await service.RetrieveAllAsync(query, cancellationToken: cancellationToken))
            .Select(entity => entity.ToEntity<dataverse.TeamTemplate>()).ToList();

        //find templates which need to be removed
        Console.WriteLine("delete check");
        foreach (var teamTemplate in teamTemplates.Where(e => templates.TrueForAll(c => e.Id != c.TeamTemplateId)))
        {
            result = await DeleteTeamTemplateAsync(service, teamTemplate, cancellationToken) && result;
        }

        //find templates which need to be updated
        Console.WriteLine("update check");
        foreach (var updateTemplate in templates.Where(c => teamTemplates.Exists(e => e.Id == c.TeamTemplateId)))
        {
            result = await UpdateTeamTemplateAsync(service, teamTemplates, updateTemplate, entities, cancellationToken) && result;
        }

        //find templates which need to be created
        Console.WriteLine("create check");
        foreach (var newTemplate in templates.Where(c => teamTemplates.TrueForAll(e => e.Id != c.TeamTemplateId)))
        {
            result = await CreateTeamTemplateAsync(service, entities, newTemplate, cancellationToken) && result;
        }

        return result;
    }

    private async Task<bool> CreateTeamTemplateAsync(IOrganizationServiceAsync2 service, List<EntityMetadata> entities, TeamTemplate newTemplate,
        CancellationToken cancellationToken)
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
        return await service.TryCreateAsync(new dataverse.TeamTemplate(newTemplate.TeamTemplateId)
        {
            TeamTemplateName = newTemplate.TeamTemplateName,
            Description = newTemplate.Description,
            DefaultAccessRightsMask = newTemplate.DefaultAccessRightsMask,
            ObjectTypeCode = entity.ObjectTypeCode
        }, cancellationToken);
    }

    private async Task<bool> UpdateTeamTemplateAsync(IOrganizationServiceAsync2 service, IEnumerable<dataverse.TeamTemplate> teamTemplates,
        TeamTemplate updateTemplate, List<EntityMetadata> entities, CancellationToken cancellationToken)
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
            result = await service.TryUpdateAsync(new dataverse.TeamTemplate(existingTemplate.Id)
            {
                TeamTemplateName = updateTemplate.TeamTemplateName,
                Description = updateTemplate.Description,
                //IsSystem = updateTemplate.IsSystem,
                DefaultAccessRightsMask = updateTemplate.DefaultAccessRightsMask,
                ObjectTypeCode = entity
            }, cancellationToken);
        }

        return result;
    }

    private async Task<bool> DeleteTeamTemplateAsync(IOrganizationServiceAsync2 service, dataverse.TeamTemplate teamTemplate,
        CancellationToken cancellationToken)
    {
        //delete template
        Console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[grey]delete template: {teamTemplate.TeamTemplateName}[/]");
        return await service.TryDeleteAsync(dataverse.TeamTemplate.EntityLogicalName, teamTemplate.Id, cancellationToken);
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
