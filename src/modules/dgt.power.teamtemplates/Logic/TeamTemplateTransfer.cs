// Copyright (c) DIGITALL Nature. All rights reserved

using System.ServiceModel;
using dgt.power.common.Extensions;
using dgt.power.teamtemplates.Configuration;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using Spectre.Console;

namespace dgt.power.teamtemplates.Logic;

public sealed class TeamTemplateTransfer(IOrganizationServiceAsync2 service, IAnsiConsole console)
{
    public async Task<(IReadOnlyList<EntityMetadata> Metadata, IReadOnlyList<Entity> Templates)> ReadAsync(
        CancellationToken cancellationToken)
    {
        var metadata = (RetrieveAllEntitiesResponse)await service.ExecuteAsync(
            new RetrieveAllEntitiesRequest { EntityFilters = EntityFilters.Entity }, cancellationToken);
        var query = new QueryExpression("teamtemplate")
        {
            ColumnSet = new ColumnSet("teamtemplateid", "teamtemplatename", "description",
                "defaultaccessrightsmask", "objecttypecode", "issystem"),
            Orders = { new OrderExpression("teamtemplateid", OrderType.Ascending) }
        };
        var templates = await service.RetrieveAllAsync(query, cancellationToken: cancellationToken);
        return (metadata.EntityMetadata, templates);
    }

    public static TeamTemplatePlan Plan(TeamTemplateConfiguration configuration,
        IReadOnlyList<EntityMetadata> metadata, IReadOnlyList<Entity> target, IReadOnlyList<string> cleanupTables)
    {
        ArgumentNullException.ThrowIfNull(configuration);
        ArgumentNullException.ThrowIfNull(metadata);
        ArgumentNullException.ThrowIfNull(target);
        ArgumentNullException.ThrowIfNull(cleanupTables);
        configuration.Validate();
        if (configuration.Templates.Count == 0 && cleanupTables.Count > 0)
        {
            throw new TeamTemplateConfigurationException("Empty templates cannot be combined with cleanup.");
        }

        var tables = ResolveTables(metadata, configuration.Templates.Select(entry => entry.Table).Concat(cleanupTables));
        var creates = new List<Entity>();
        var updates = new List<Entity>();
        var unchanged = new List<Guid>();
        var included = configuration.Templates.Select(entry => entry.Id).ToHashSet();
        foreach (var entry in configuration.Templates)
        {
            var code = tables[entry.Table];
            var existing = target.SingleOrDefault(template => template.Id == entry.Id);
            if (existing is not null &&
                (existing.GetAttributeValue<bool>("issystem") || existing.GetAttributeValue<int?>("objecttypecode") != code))
            {
                throw new TeamTemplateConfigurationException($"Template '{entry.Id:D}' is system-defined or belongs to another table.");
            }
            if (target.Any(template => template.Id != entry.Id &&
                template.GetAttributeValue<int?>("objecttypecode") == code &&
                string.Equals(template.GetAttributeValue<string>("teamtemplatename"), entry.Name, StringComparison.OrdinalIgnoreCase)))
            {
                throw new TeamTemplateConfigurationException($"Table/name conflict for '{entry.Table}/{entry.Name}' under another GUID.");
            }

            var desired = new Entity("teamtemplate", entry.Id)
            {
                ["teamtemplatename"] = entry.Name,
                ["description"] = entry.Description,
                ["defaultaccessrightsmask"] = entry.Rights
            };
            if (existing is null)
            {
                desired["objecttypecode"] = code;
                creates.Add(desired);
            }
            else if (string.Equals(existing.GetAttributeValue<string>("teamtemplatename"), entry.Name, StringComparison.Ordinal) &&
                     string.Equals(existing.GetAttributeValue<string>("description"), entry.Description, StringComparison.Ordinal) &&
                     existing.GetAttributeValue<int?>("defaultaccessrightsmask") == entry.Rights)
            {
                unchanged.Add(entry.Id);
            }
            else
            {
                updates.Add(desired);
            }
        }

        var cleanupCodes = cleanupTables.Select(table => tables[table]).ToHashSet();
        var deletes = target.Where(template => !included.Contains(template.Id) &&
                !template.GetAttributeValue<bool>("issystem") &&
                template.GetAttributeValue<int?>("objecttypecode") is { } code && cleanupCodes.Contains(code))
            .Select(template => template.Id).Order().ToArray();
        return new TeamTemplatePlan(creates, updates, unchanged, deletes);
    }

    public static TeamTemplateConfiguration Export(IReadOnlyList<EntityMetadata> metadata,
        IReadOnlyList<Entity> target, IReadOnlyList<string> selectedTables)
    {
        ArgumentNullException.ThrowIfNull(metadata);
        ArgumentNullException.ThrowIfNull(target);
        ArgumentNullException.ThrowIfNull(selectedTables);
        var selected = ResolveTables(metadata, selectedTables);
        var entries = new List<TeamTemplateEntry>();
        foreach (var template in target.Where(template => !template.GetAttributeValue<bool>("issystem")))
        {
            var code = template.GetAttributeValue<int?>("objecttypecode");
            if (selected.Count > 0 && !selected.ContainsValue(code ?? -1))
            {
                continue;
            }
            var table = metadata.SingleOrDefault(table => code.HasValue && table.ObjectTypeCode == code);
            if (table?.LogicalName is not { } name)
            {
                throw new TeamTemplateConfigurationException($"Cannot resolve table for template '{template.Id:D}'.");
            }
            ResolveTables(metadata, [name]);
            var rights = template.GetAttributeValue<int?>("defaultaccessrightsmask")
                ?? throw new TeamTemplateConfigurationException($"Template '{template.Id:D}' has no access-rights mask.");
            entries.Add(new TeamTemplateEntry
            {
                Id = template.Id,
                Table = name,
                Name = template.GetAttributeValue<string>("teamtemplatename"),
                Description = template.GetAttributeValue<string>("description"),
                Rights = rights
            });
        }
        var configuration = new TeamTemplateConfiguration
        {
            SchemaVersion = 1,
            Templates = entries.OrderBy(entry => entry.Table, StringComparer.Ordinal).ThenBy(entry => entry.Id).ToArray()
        };
        configuration.Validate();
        return configuration;
    }

    private static Dictionary<string, int> ResolveTables(IReadOnlyList<EntityMetadata> metadata, IEnumerable<string> names)
    {
        var result = new Dictionary<string, int>(StringComparer.Ordinal);
        foreach (var name in names.Distinct(StringComparer.Ordinal))
        {
            var table = metadata.SingleOrDefault(table => table.LogicalName == name);
            if (!TeamTemplateConfiguration.IsTableName(name) || table is not { ObjectTypeCode: { } code, AutoCreateAccessTeams: true })
            {
                throw new TeamTemplateConfigurationException(
                    $"Table '{name}' must exist, have an object type code, and have access teams enabled.");
            }
            result.Add(name, code);
        }
        return result;
    }

    public Task<bool> ApplyAsync(TeamTemplatePlan plan, bool dryRun, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(plan);
        return ApplyCoreAsync(plan, dryRun, cancellationToken);
    }

    private async Task<bool> ApplyCoreAsync(TeamTemplatePlan plan, bool dryRun, CancellationToken cancellationToken)
    {
        console.MarkupLine($"{(dryRun ? "DRY RUN: " : "")}{plan.Creates.Count} create, {plan.Updates.Count} update, " +
            $"{plan.Unchanged.Count} unchanged, {plan.Deletes.Count} delete.");
        foreach (var id in plan.Unchanged)
        {
            console.MarkupLine($"unchanged {id:D}");
        }
        var requests = plan.Creates.Select(entity => (Action: "create", entity.Id, Request: (OrganizationRequest)new CreateRequest { Target = entity }))
            .Concat(plan.Updates.Select(entity => (Action: "update", entity.Id, Request: (OrganizationRequest)new UpdateRequest { Target = entity })))
            .Concat(plan.Deletes.Select(id => (Action: "delete", Id: id, Request: (OrganizationRequest)new DeleteRequest
            {
                Target = new EntityReference("teamtemplate", id)
            })));
        var completed = 0;
        foreach (var (action, id, request) in requests)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                console.MarkupLine($"[red]Cancelled; {completed} completed mutations retained. No rollback or further writes.[/]");
                cancellationToken.ThrowIfCancellationRequested();
            }
            console.MarkupLine($"{action} {id:D}");
            if (dryRun)
            {
                continue;
            }
            try
            {
                await service.ExecuteAsync(request, cancellationToken);
                completed++;
            }
            catch (OperationCanceledException)
            {
                console.MarkupLine($"[red]Cancelled during {action} {id:D}; {completed} completed mutations retained; " +
                    "the in-flight result may be unknown. No rollback or further writes.[/]");
                throw;
            }
            catch (Exception exception) when (exception is FaultException<OrganizationServiceFault> or HttpRequestException or TimeoutException)
            {
                console.MarkupLine($"[red]Failed {action} {id:D}: {exception.Message.EscapeMarkup()}. " +
                    $"{completed} completed mutations retained; no rollback or further writes.[/]");
                return false;
            }
        }
        if (!dryRun)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                console.MarkupLine($"[red]Cancelled; {completed} completed mutations retained. No rollback or further writes.[/]");
            }
            cancellationToken.ThrowIfCancellationRequested();
            console.MarkupLine($"[green]{completed} mutations completed.[/]");
        }
        return true;
    }
}
