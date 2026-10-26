// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Metadata.Query;
using Microsoft.Xrm.Sdk.Query;
using Spectre.Console;

namespace dgt.power.solution.Base;

internal sealed class FieldProtectionPlan(IReadOnlyList<FieldProtectionDecision> fields)
{
    public static async Task<FieldProtectionPlan> PrepareAsync(
        IOrganizationServiceAsync2 service,
        IReadOnlyList<ComponentCopyDecision> decisions,
        CancellationToken cancellationToken)
    {
        var attributes = new Dictionary<Guid, (string EntityName, AttributeMetadata Attribute)>();
        foreach (var table in decisions.Where(static d => d.Include &&
                     d.ComponentType == SolutionComponent.Options.ComponentType.Entity && !d.DoNotIncludeSubcomponents))
        {
            cancellationToken.ThrowIfCancellationRequested();
            var metadata = ((RetrieveEntityResponse)await service.ExecuteAsync(new RetrieveEntityRequest
            {
                MetadataId = table.ObjectId,
                EntityFilters = EntityFilters.Entity | EntityFilters.Attributes,
                RetrieveAsIfPublished = true
            }, cancellationToken)).EntityMetadata;
            if (metadata?.MetadataId != table.ObjectId || string.IsNullOrWhiteSpace(metadata.LogicalName) ||
                metadata.Attributes is null)
            {
                throw new InvalidOperationException($"Incomplete protection metadata for table {table.ObjectId}.");
            }
            foreach (var attribute in metadata.Attributes)
            {
                AddAttribute(attributes, metadata.LogicalName, attribute);
            }
        }

        var standaloneIds = decisions.Where(static d => d.Include &&
                d.ComponentType == SolutionComponent.Options.ComponentType.Attribute)
            .Select(static d => d.ObjectId).Distinct().Where(id => !attributes.ContainsKey(id)).ToArray();
        foreach (var batch in standaloneIds.Chunk(200))
        {
            cancellationToken.ThrowIfCancellationRequested();
            var criteria = new MetadataFilterExpression(LogicalOperator.And);
            criteria.Conditions.Add(new MetadataConditionExpression(
                nameof(AttributeMetadata.MetadataId), MetadataConditionOperator.In, batch));
            var response = (RetrieveMetadataChangesResponse)await service.ExecuteAsync(new RetrieveMetadataChangesRequest
            {
                Query = new EntityQueryExpression
                {
                    Properties = new MetadataPropertiesExpression(nameof(EntityMetadata.LogicalName), nameof(EntityMetadata.Attributes)),
                    AttributeQuery = new AttributeQueryExpression
                    {
                        Properties = new MetadataPropertiesExpression { AllProperties = true },
                        Criteria = criteria
                    }
                }
            }, cancellationToken);
            foreach (var entity in response.EntityMetadata)
            {
                foreach (var attribute in entity.Attributes ?? [])
                {
                    if (attribute.MetadataId is { } id && batch.Contains(id))
                    {
                        AddAttribute(attributes, entity.LogicalName, attribute);
                    }
                }
            }
            var missing = batch.Where(id => !attributes.ContainsKey(id)).ToArray();
            if (missing.Length > 0)
            {
                throw new InvalidOperationException($"Protection metadata not found for selected attribute(s): {string.Join(", ", missing)}.");
            }
        }

        var fields = new List<FieldProtectionDecision>();
        foreach (var (id, (entity, attribute)) in attributes.OrderBy(static p => p.Value.EntityName, StringComparer.Ordinal)
                     .ThenBy(static p => p.Value.Attribute.LogicalName, StringComparer.Ordinal))
        {
            var identity = $"{entity}.{attribute.LogicalName}";
            if (!attribute.IsManaged.HasValue)
            {
                throw new InvalidOperationException($"Unknown managed state for protection candidate {identity}.");
            }
            if (attribute.IsManaged.Value)
            {
                continue;
            }
            if (attribute.SourceType is < 0 or > 4)
            {
                throw new InvalidOperationException($"Unknown calculation SourceType {attribute.SourceType} for {identity}.");
            }

            // Full metadata reads distinguish a known null (non-calculation) from an unavailable response.
            var reason = attribute switch
            {
                MoneyAttributeMetadata { IsBaseCurrency: true } => "Exception: basis-currency companion",
                { LogicalName: "bpf_duration" } => "Exception: historic BPF duration compatibility policy",
                _ => null
            };
            if (reason is null && attribute.SourceType is not (1 or 2 or 3))
            {
                continue;
            }
            if (reason is null && attribute is MoneyAttributeMetadata { IsBaseCurrency: null })
            {
                throw new InvalidOperationException($"Unknown basis-currency state for {identity}.");
            }
            var requiresUpdate = false;
            if (reason is null)
            {
                var property = attribute.IsCustomizable ??
                    throw new InvalidOperationException($"Missing IsCustomizable protection metadata for {identity}.");
                if (!property.Value)
                {
                    reason = "Unchanged: already protected";
                }
                else if (!property.CanBeChanged)
                {
                    throw new InvalidOperationException($"Cannot protect {identity}: IsCustomizable cannot be changed.");
                }
                else
                {
                    requiresUpdate = true;
                    reason = "Set IsCustomizable=false";
                }
            }
            var field = new FieldProtectionDecision(id, entity, attribute.LogicalName, attribute.SourceType,
                attribute.AttributeType, requiresUpdate, reason);
            if (requiresUpdate)
            {
                _ = field.CreateUpdate();
            }
            fields.Add(field);
        }
        return new FieldProtectionPlan(fields);
    }

    private static void AddAttribute(
        Dictionary<Guid, (string EntityName, AttributeMetadata Attribute)> attributes,
        string entityName,
        AttributeMetadata attribute)
    {
        if (string.IsNullOrWhiteSpace(entityName) || string.IsNullOrWhiteSpace(attribute.LogicalName) ||
            attribute.MetadataId is not { } id || id == Guid.Empty)
        {
            throw new InvalidOperationException($"Missing protection metadata identity for {entityName}.{attribute.LogicalName}.");
        }
        if (attributes.TryGetValue(id, out var existing) &&
            (existing.EntityName != entityName || existing.Attribute.LogicalName != attribute.LogicalName))
        {
            throw new InvalidOperationException($"Conflicting protection metadata identity for {id}.");
        }
        attributes.TryAdd(id, (entityName, attribute));
    }

    public void Render(IAnsiConsole console)
    {
        console.MarkupLine("[yellow]Field protection changes shared environment metadata, including other solutions. " +
            "IsCustomizable=false is a column-level managed-deployment policy, not an unmanaged-development or data-access lock. " +
            "Labels/translations may remain editable.[/]");
        var table = new Table().AddColumn("Column").AddColumn("Calculation type").AddColumn("Protection");
        foreach (var field in fields)
        {
            table.AddRow(Markup.Escape(field.Identity), field.CalculationType, Markup.Escape(field.Reason));
        }
        console.Write(table);
    }

    public async Task ExecuteAsync(
        IOrganizationServiceAsync2 service,
        CopyExecutionProgress progress,
        IAnsiConsole console,
        CancellationToken cancellationToken)
    {
        foreach (var field in fields.Where(static f => f.RequiresUpdate))
        {
            cancellationToken.ThrowIfCancellationRequested();
            progress.PendingEffect = $"Protection update failed or unconfirmed: {field.Identity}";
            await service.ExecuteAsync(new UpdateAttributeRequest
            {
                EntityName = field.EntityName,
                Attribute = field.CreateUpdate(),
                MergeLabels = true
            }, cancellationToken);
            progress.PendingEffect = $"Protection update accepted, read-back unconfirmed: {field.Identity}";
            cancellationToken.ThrowIfCancellationRequested();
            var metadata = ((RetrieveAttributeResponse)await service.ExecuteAsync(new RetrieveAttributeRequest
            {
                EntityLogicalName = field.EntityName,
                MetadataId = field.MetadataId,
                RetrieveAsIfPublished = true
            }, cancellationToken)).AttributeMetadata;
            if (metadata?.MetadataId != field.MetadataId || metadata.IsCustomizable?.Value != false)
            {
                throw new InvalidOperationException($"Protection read-back mismatch for {field.Identity}; update may have taken effect.");
            }
            progress.ProtectedFields.Add(field.Identity);
            progress.PendingEffect = null;
            console.MarkupLine($"[green]Confirmed protected: {Markup.Escape(field.Identity)}[/]");
        }
    }
}
