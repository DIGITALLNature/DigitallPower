// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.plugin.Local;
using dgt.power.plugin.Remote;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Metadata.Query;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.plugin.Repositories;

public sealed class EntityDataProviderRepository(IOrganizationServiceAsync2 service) : IEntityDataProviderRepository
{
    private Guid? _jsonConverterProviderId;

    public async Task<IReadOnlyList<RemoteDataProvider>> ListAsync(CancellationToken cancellationToken = default)
    {
        var operations = Enum.GetValues<DataProviderOperation>();
        var columns = new[] { EntityDataProvider.LogicalNames.Name, EntityDataProvider.LogicalNames.Description, EntityDataProvider.LogicalNames.DataSourceLogicalName }
            .Concat(operations.Select(operation => operation.HandlerField())).ToArray();
        var query = new QueryExpression(EntityDataProvider.EntityLogicalName) { ColumnSet = new ColumnSet(columns), PageInfo = new PagingInfo { PageNumber = 1, Count = 5000 } };
        var providers = new List<RemoteDataProvider>();
        EntityCollection result;
        do
        {
            result = await service.RetrieveMultipleAsync(query, cancellationToken);
            providers.AddRange(result.Entities.Select(entity => entity.ToEntity<EntityDataProvider>()).Select(entity => new RemoteDataProvider(entity.Id,
                entity.DataSourceLogicalName ?? string.Empty, entity.Name ?? entity.Id.ToString(),
                entity.Description,
                operations.Where(operation => entity.Attributes.TryGetValue(operation.HandlerField(), out var value) && value is not null)
                    .ToDictionary(operation => operation, operation => entity.GetAttributeValue<Guid>(operation.HandlerField())))));
            query.PageInfo.PageNumber++;
            query.PageInfo.PagingCookie = result.PagingCookie;
        } while (result.MoreRecords);

        return providers;
    }

    public async Task<EntityMetadata?> FindDataSourceAsync(string logicalName, CancellationToken cancellationToken = default)
    {
        var query = new EntityQueryExpression
        {
            Criteria = new MetadataFilterExpression
            {
                Conditions = { new MetadataConditionExpression(nameof(EntityMetadata.LogicalName), MetadataConditionOperator.Equals, logicalName) }
            },
            Properties = new MetadataPropertiesExpression
            {
                PropertyNames = { nameof(EntityMetadata.MetadataId), nameof(EntityMetadata.LogicalName), nameof(EntityMetadata.SchemaName), nameof(EntityMetadata.DataProviderId),
                    nameof(EntityMetadata.OwnershipType), nameof(EntityMetadata.DisplayName), nameof(EntityMetadata.DisplayCollectionName) }
            }
        };
        var response = (RetrieveMetadataChangesResponse)await service.ExecuteAsync(new RetrieveMetadataChangesRequest { Query = query }, cancellationToken);
        return response.EntityMetadata.SingleOrDefault();
    }

    /// <inheritdoc />
    public async Task ValidateDataSourceAsync(string logicalName, EntityMetadata? metadata, CancellationToken cancellationToken = default)
    {
        var backingProviderId = await ResolveJsonConverterProviderIdAsync(cancellationToken);
        if (metadata is not null && (metadata.DataProviderId != backingProviderId ||
                                    metadata.OwnershipType != OwnershipTypes.OrganizationOwned || metadata.MetadataId is null))
        {
            throw new InvalidDataSourceException($"Table '{logicalName}' is not a valid organization-owned JsonConverter data-source table. " +
                                                "An ordinary table cannot be converted into one.");
        }
    }

    private async Task<Guid> ResolveJsonConverterProviderIdAsync(CancellationToken cancellationToken)
    {
        if (_jsonConverterProviderId is { } cachedId)
        {
            return cachedId;
        }

        var query = new QueryExpression(EntityDataProvider.EntityLogicalName)
        {
            ColumnSet = new ColumnSet(EntityDataProvider.LogicalNames.EntityDataProviderId),
            TopCount = 2,
            Criteria = new FilterExpression
            {
                Conditions = { new ConditionExpression(EntityDataProvider.LogicalNames.Name, ConditionOperator.Equal, "JsonConverter") }
            }
        };
        var result = await service.RetrieveMultipleAsync(query, cancellationToken);
        if (result.Entities.Count != 1)
        {
            throw new InvalidOperationException(result.Entities.Count == 0
                ? "The required JsonConverter data provider was not found in the target environment."
                : "Multiple data providers named JsonConverter were found in the target environment; the configuration-table backing provider is ambiguous.");
        }

        var providerId = result.Entities[0].Id;
        _jsonConverterProviderId = providerId;
        return providerId;
    }

    /// <inheritdoc />
    public Task<Guid> CreateDataSourceAsync(LocalDataProvider provider, string? solution, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(provider);
        return CreateDataSourceCoreAsync(provider, solution, cancellationToken);
    }

    private async Task<Guid> CreateDataSourceCoreAsync(LocalDataProvider provider, string? solution, CancellationToken cancellationToken)
    {
        var backingProviderId = await ResolveJsonConverterProviderIdAsync(cancellationToken);
        var languageCode = await GetBaseLanguageCodeAsync(cancellationToken);
        var singular = provider.DataSourceDisplayName ?? provider.DataSourceSchemaName;
        var plural = provider.DataSourcePluralName ?? $"{singular} Records";
        var primaryAttributeName = $"{provider.DataSourceLogicalName.Split('_')[0]}_name";
        var request = new CreateEntityRequest
        {
            Entity = new EntityMetadata
            {
                SchemaName = provider.DataSourceSchemaName,
                DisplayName = new Label(singular, languageCode),
                DisplayCollectionName = new Label(plural, languageCode),
                DataProviderId = backingProviderId,
                ExternalName = provider.DataSourceSchemaName,
                ExternalCollectionName = provider.DataSourceSchemaName,
                OwnershipType = OwnershipTypes.OrganizationOwned,
                IsActivity = false,
                IsAvailableOffline = false,
                ChangeTrackingEnabled = false,
                CanChangeTrackingBeEnabled = new BooleanManagedProperty(false),
                CanCreateCharts = new BooleanManagedProperty(false),
                IsVisibleInMobileClient = new BooleanManagedProperty(false),
                IsAuditEnabled = new BooleanManagedProperty(false),
                IsDuplicateDetectionEnabled = new BooleanManagedProperty(false),
                IsConnectionsEnabled = new BooleanManagedProperty(false),
                IsMailMergeEnabled = new BooleanManagedProperty(false),
                IsBusinessProcessEnabled = false
            },
            PrimaryAttribute = new StringAttributeMetadata
            {
                SchemaName = primaryAttributeName,
                DisplayName = new Label("Name", languageCode),
                ExternalName = primaryAttributeName,
                MaxLength = 200,
                RequiredLevel = new AttributeRequiredLevelManagedProperty(AttributeRequiredLevel.None)
            },
            HasActivities = false,
            HasNotes = false
        };
        if (!string.IsNullOrWhiteSpace(solution))
        {
            request["SolutionUniqueName"] = solution;
        }

        var response = (CreateEntityResponse)await service.ExecuteAsync(request, cancellationToken);
        var attributeResponse = (RetrieveAttributeResponse)await service.ExecuteAsync(new RetrieveAttributeRequest
        {
            EntityLogicalName = provider.DataSourceLogicalName,
            LogicalName = $"{provider.DataSourceLogicalName}id",
            RetrieveAsIfPublished = true
        }, cancellationToken);
        attributeResponse.AttributeMetadata.ExternalName = $"{provider.DataSourceSchemaName}Id";
        await service.ExecuteAsync(new UpdateAttributeRequest
        {
            EntityName = provider.DataSourceLogicalName, Attribute = attributeResponse.AttributeMetadata, MergeLabels = true
        }, cancellationToken);
        await PublishAsync(provider.DataSourceLogicalName, cancellationToken);
        return response.EntityId;
    }

    public Task UpdateDataSourceAsync(EntityMetadata metadata, LocalDataProvider provider, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(metadata);
        ArgumentNullException.ThrowIfNull(provider);
        return UpdateDataSourceCoreAsync(metadata, provider, cancellationToken);
    }

    private async Task UpdateDataSourceCoreAsync(EntityMetadata metadata, LocalDataProvider provider, CancellationToken cancellationToken)
    {
        var languageCode = await GetBaseLanguageCodeAsync(cancellationToken);
        var update = new EntityMetadata { MetadataId = metadata.MetadataId, LogicalName = metadata.LogicalName };
        if (provider.DataSourceDisplayName is not null)
        {
            update.DisplayName = new Label(provider.DataSourceDisplayName, languageCode);
        }

        if (provider.DataSourcePluralName is not null)
        {
            update.DisplayCollectionName = new Label(provider.DataSourcePluralName, languageCode);
        }

        await service.ExecuteAsync(new UpdateEntityRequest { Entity = update, MergeLabels = true }, cancellationToken);
        await PublishAsync(provider.DataSourceLogicalName, cancellationToken);
    }

    public async Task<int> GetBaseLanguageCodeAsync(CancellationToken cancellationToken = default)
    {
        var organizations = await service.RetrieveMultipleAsync(new QueryExpression("organization") { ColumnSet = new ColumnSet("languagecode") }, cancellationToken);
        var languageCode = organizations.Entities.Single().GetAttributeValue<int>("languagecode");
        return languageCode > 0 ? languageCode : throw new InvalidOperationException("The target organization does not define a valid base language code.");
    }

    private Task<OrganizationResponse> PublishAsync(string logicalName, CancellationToken cancellationToken) =>
        service.ExecuteAsync(new PublishXmlRequest { ParameterXml = $"<importexportxml><entities><entity>{logicalName}</entity></entities></importexportxml>" }, cancellationToken);

    public Task<Guid> ApplyAsync(Guid? id, LocalDataProvider? provider, IReadOnlyDictionary<DataProviderOperation, Guid> handlers, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(handlers);
        if (id is null)
        {
            ArgumentNullException.ThrowIfNull(provider);
        }
        return ApplyCoreAsync(id, provider, handlers, cancellationToken);
    }

    private async Task<Guid> ApplyCoreAsync(Guid? id, LocalDataProvider? provider, IReadOnlyDictionary<DataProviderOperation, Guid> handlers, CancellationToken cancellationToken)
    {
        var entity = new EntityDataProvider();
        if (provider is not null)
        {
            entity.Name = provider.ProviderName;
            entity.DataSourceLogicalName = provider.DataSourceLogicalName;
            if (provider.Description is not null)
            {
                entity.Description = provider.Description;
            }
        }

        foreach (var (field, typeId) in handlers)
        {
            entity[field.HandlerField()] = typeId;
        }

        if (id is { } existingId)
        {
            entity.Id = existingId;
            await service.UpdateAsync(entity, cancellationToken);
            return existingId;
        }

        return await service.CreateAsync(entity, cancellationToken);
    }
}
