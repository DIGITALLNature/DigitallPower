// Copyright (c) DIGITALL Nature. All rights reserved

using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;

namespace dgt.power.documenttemplates.Logic;

public sealed class TemplateMetadata(Func<string, CancellationToken, Task<EntityMetadata>> retrieve)
{
    private readonly Dictionary<string, EntityMetadata> _entities = new(StringComparer.OrdinalIgnoreCase);

    public TemplateMetadata(IOrganizationServiceAsync2 service)
        : this(async (name, token) =>
        {
            var response = (RetrieveEntityResponse)await service.ExecuteAsync(new RetrieveEntityRequest
            {
                LogicalName = name,
                EntityFilters = EntityFilters.Entity | EntityFilters.Attributes | EntityFilters.Relationships
            }, token);
            return response.EntityMetadata;
        })
    {
        ArgumentNullException.ThrowIfNull(service);
    }

    public async Task<EntityMetadata> GetAsync(string name, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        if (_entities.TryGetValue(name, out var metadata))
            return metadata;
        metadata = await retrieve(name.ToLowerInvariant(), cancellationToken);
        if (metadata is null || string.IsNullOrWhiteSpace(metadata.LogicalName) ||
            !string.Equals(metadata.LogicalName, name, StringComparison.OrdinalIgnoreCase))
            throw new InvalidDataException($"Target table '{name}' is missing or ambiguous.");
        _entities.Add(name, metadata);
        return metadata;
    }

    public async Task<string> ResolveRelationshipAsync(string table, string step, CancellationToken cancellationToken)
    {
        var metadata = await GetAsync(table, cancellationToken);
        var targets = new List<(string Key, string Table)>();
        foreach (var relationship in metadata.OneToManyRelationships ?? [])
            if (Same(relationship.ReferencedEntity, table) &&
                (Same(relationship.SchemaName, step) || Same(relationship.ReferencedEntityNavigationPropertyName, step)))
                targets.Add(($"{relationship.SchemaName}:1N", relationship.ReferencingEntity));
        foreach (var relationship in metadata.ManyToOneRelationships ?? [])
            if (Same(relationship.ReferencingEntity, table) &&
                (Same(relationship.SchemaName, step) || Same(relationship.ReferencingEntityNavigationPropertyName, step)))
                targets.Add(($"{relationship.SchemaName}:N1", relationship.ReferencedEntity));
        foreach (var relationship in metadata.ManyToManyRelationships ?? [])
        {
            if (Same(relationship.Entity1LogicalName, table) &&
                (Same(relationship.SchemaName, step) || Same(relationship.Entity1NavigationPropertyName, step)))
                targets.Add(($"{relationship.SchemaName}:NN1", relationship.Entity2LogicalName));
            if (Same(relationship.Entity2LogicalName, table) &&
                (Same(relationship.SchemaName, step) || Same(relationship.Entity2NavigationPropertyName, step)))
                targets.Add(($"{relationship.SchemaName}:NN2", relationship.Entity1LogicalName));
        }
        if (step is "account_leads" or "lead_accounts")
        {
            var verified = (metadata.ManyToManyRelationships ?? []).Where(relationship =>
                IsAccountLeadRelationship(relationship) && (Same(table, "account") || Same(table, "lead"))).ToArray();
            if (verified.Length != 1)
                throw new InvalidDataException($"Unverified N:N alias '{step}' on '{table}'.");
            targets.Add(($"verified:{step}", step == "account_leads" ? "account" : "lead"));
        }
        var distinct = targets.Distinct().ToArray();
        if (distinct.Length != 1 || string.IsNullOrWhiteSpace(distinct[0].Table))
            throw new InvalidDataException($"Missing or ambiguous relationship '{table}/{step}'.");
        _ = await GetAsync(distinct[0].Table, cancellationToken);
        return distinct[0].Table;
    }

    public async Task<bool> HasAttributeAsync(string table, string name, CancellationToken cancellationToken)
    {
        var metadata = await GetAsync(table, cancellationToken);
        var matches = (metadata.Attributes ?? []).Where(attribute => Same(attribute.LogicalName, name)).ToArray();
        if (matches.Length > 1)
            throw new InvalidDataException($"Ambiguous field '{table}/{name}'.");
        if (matches.Length == 0)
            return false;
        if (matches[0].AttributeOf is { Length: > 0 } parent &&
            !Array.Exists(metadata.Attributes ?? [], attribute => Same(attribute.LogicalName, parent)))
            throw new InvalidDataException($"Display field '{table}/{name}' has missing AttributeOf '{parent}'.");
        return true;
    }

    private static bool Same(string? left, string? right) =>
        string.Equals(left, right, StringComparison.OrdinalIgnoreCase);

    private static bool IsAccountLeadRelationship(ManyToManyRelationshipMetadata relationship)
    {
        var endpoints = Same(relationship.Entity1LogicalName, "account")
            ? Same(relationship.Entity2LogicalName, "lead")
            : Same(relationship.Entity1LogicalName, "lead") && Same(relationship.Entity2LogicalName, "account");
        return Same(relationship.SchemaName, "accountleads_association") &&
               Same(relationship.IntersectEntityName, "accountleads") && endpoints;
    }
}
