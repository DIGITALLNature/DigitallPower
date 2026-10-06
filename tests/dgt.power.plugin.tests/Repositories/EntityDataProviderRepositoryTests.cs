// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.plugin.Local;
using dgt.power.plugin.Repositories;
using dgt.power.tests.Extensions;
using Digitall.Dataverse.Testing;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.plugin.tests.Repositories;

public class EntityDataProviderRepositoryTests
{
    [Test]
    [Arguments(0)]
    [Arguments(2)]
    public async Task ResolveJsonConverter_MissingOrAmbiguous_FailsBeforeWrites(int matches)
    {
        var service = new FakeOrganizationServiceAsync();
        service.AddRequests(new ProviderRequestFake(typeof(RetrieveMultipleRequest), _ =>
        {
            var result = new EntityCollection();
            for (var i = 0; i < matches; i++)
            {
                result.Entities.Add(new EntityDataProvider(Guid.NewGuid()));
            }
            return new RetrieveMultipleResponse { Results = { ["EntityCollection"] = result } };
        }));
        await Assert.That(async () => await new EntityDataProviderRepository(service).CreateDataSourceAsync(Provider(), null))
            .ThrowsExactly<InvalidOperationException>();
    }

    [Test]
    public async Task ResolveJsonConverter_UniqueMatch_UsesEnvironmentIdAndCachesLookup()
    {
        var service = new FakeOrganizationServiceAsync();
        var providerId = Guid.NewGuid();
        var calls = 0;
        QueryExpression? query = null;
        service.AddRequests(new ProviderRequestFake(typeof(RetrieveMultipleRequest), request =>
        {
            calls++;
            query = (QueryExpression)((RetrieveMultipleRequest)request).Query;
            return new RetrieveMultipleResponse
            {
                Results = { ["EntityCollection"] = new EntityCollection(new List<Entity> { new EntityDataProvider(providerId) }) }
            };
        }));
        var repository = new EntityDataProviderRepository(service);
        await repository.ValidateDataSourceAsync("dgt_source", new EntityMetadata
        {
            MetadataId = Guid.NewGuid(), DataProviderId = providerId, OwnershipType = OwnershipTypes.OrganizationOwned
        });
        await repository.ValidateDataSourceAsync("dgt_source", null);
        await Assert.That(calls).IsEqualTo(1);
        await Assert.That(query!.TopCount).IsEqualTo(2);
        var condition = query.Criteria.Conditions.Single();
        await Assert.That(condition.AttributeName).IsEqualTo(EntityDataProvider.LogicalNames.Name);
        await Assert.That(condition.Operator).IsEqualTo(ConditionOperator.Equal);
        await Assert.That(condition.Values.Single()).IsEqualTo("JsonConverter");
    }

    [Test]
    [Arguments(0)]
    [Arguments(2)]
    public async Task ValidateDataSource_MissingOrAmbiguousBackingProvider_Throws(int matches)
    {
        var service = new FakeOrganizationServiceAsync();
        service.AddRequests(new ProviderRequestFake(typeof(RetrieveMultipleRequest), _ =>
            new RetrieveMultipleResponse
            {
                Results = { ["EntityCollection"] = new EntityCollection(Enumerable.Range(0, matches)
                    .Select(_ => (Entity)new EntityDataProvider(Guid.NewGuid())).ToList()) }
            }));
        await Assert.That(async () => await new EntityDataProviderRepository(service).ValidateDataSourceAsync("dgt_source", null))
            .ThrowsExactly<InvalidOperationException>();
    }

    [Test]
    [Arguments("ordinary")]
    [Arguments("other-provider")]
    [Arguments("user-owned")]
    [Arguments("missing-id")]
    public async Task ValidateDataSource_IncompatibleExistingTable_Throws(string scenario)
    {
        var service = new FakeOrganizationServiceAsync();
        var providerId = Guid.NewGuid();
        service.AddRequests(new ProviderRequestFake(typeof(RetrieveMultipleRequest), _ =>
            new RetrieveMultipleResponse
            {
                Results = { ["EntityCollection"] = new EntityCollection(new List<Entity> { new EntityDataProvider(providerId) }) }
            }));
        var metadata = new EntityMetadata
        {
            MetadataId = scenario == "missing-id" ? null : Guid.NewGuid(),
            DataProviderId = scenario switch { "ordinary" => null, "other-provider" => Guid.NewGuid(), _ => providerId },
            OwnershipType = scenario == "user-owned" ? OwnershipTypes.UserOwned : OwnershipTypes.OrganizationOwned
        };
        await Assert.That(async () => await new EntityDataProviderRepository(service).ValidateDataSourceAsync("dgt_source", metadata))
            .ThrowsExactly<InvalidDataSourceException>();
    }

    private static LocalDataProvider Provider(string? description = null) => new("dgt_Source", "Provider", null, null, description, new Dictionary<DataProviderOperation, string>());

    [Test]
    public async Task FindDataSource_QueriesLogicalNameAndReturnsTableMetadataId()
    {
        var service = new FakeOrganizationServiceAsync();
        var metadata = new EntityMetadata { MetadataId = Guid.NewGuid(), LogicalName = "dgt_source" };
        RetrieveMetadataChangesRequest? captured = null;
        service.AddRequests(new ProviderRequestFake(typeof(RetrieveMetadataChangesRequest), request =>
        {
            captured = (RetrieveMetadataChangesRequest)request;
            return new RetrieveMetadataChangesResponse { Results = { ["EntityMetadata"] = new EntityMetadataCollection { metadata } } };
        }));
        var table = await new EntityDataProviderRepository(service).FindDataSourceAsync("dgt_source");
        using (Assert.Multiple())
        {
            await Assert.That(table!.MetadataId).IsEqualTo(metadata.MetadataId);
            await Assert.That(captured!.Query.Criteria.Conditions.Single().PropertyName).IsEqualTo(nameof(EntityMetadata.LogicalName));
            await Assert.That(captured.Query.Criteria.Conditions.Single().Value).IsEqualTo("dgt_source");
            await Assert.That(captured.Query.Properties.PropertyNames).Contains(nameof(EntityMetadata.MetadataId));
            await Assert.That(captured.Query.Properties.PropertyNames).Contains(nameof(EntityMetadata.DataProviderId));
        }
    }

    [Test]
    public async Task List_MultiplePages_ReadsOnlySupportedOperationsAndUsesPagingCookie()
    {
        var service = new FakeOrganizationServiceAsync();
        var pages = new List<(int Number, string? Cookie)>();
        var typeId = Guid.NewGuid();
        service.AddRequests(new ProviderRequestFake(typeof(RetrieveMultipleRequest), request =>
        {
            var query = (QueryExpression)((RetrieveMultipleRequest)request).Query;
            if (query.ColumnSet.AllColumns || query.ColumnSet.Columns.Contains("createmultipleplugin") ||
                !Enum.GetValues<DataProviderOperation>().All(operation => query.ColumnSet.Columns.Contains(operation.HandlerField())))
            {
                throw new InvalidOperationException("Provider queries must select the supported handler fields explicitly.");
            }
            pages.Add((query.PageInfo.PageNumber, query.PageInfo.PagingCookie));
            var result = new EntityCollection
            {
                MoreRecords = query.PageInfo.PageNumber == 1,
                PagingCookie = "next-page"
            };
            result.Entities.Add(new Entity("entitydataprovider", Guid.NewGuid())
            {
                ["name"] = "Provider", ["datasourcelogicalname"] = "dgt_source",
                ["createmultipleplugin"] = Guid.NewGuid(), ["unrelatedplugin"] = Guid.NewGuid()
            });
            foreach (var operation in Enum.GetValues<DataProviderOperation>())
            {
                result.Entities[0][operation.HandlerField()] = typeId;
            }
            return new RetrieveMultipleResponse { Results = { ["EntityCollection"] = result } };
        }));
        var providers = await new EntityDataProviderRepository(service).ListAsync();
        using (Assert.Multiple())
        {
            await Assert.That(providers).Count().IsEqualTo(2);
            await Assert.That(providers[1].Handlers).Count().IsEqualTo(5);
            foreach (var operation in Enum.GetValues<DataProviderOperation>())
            {
                await Assert.That(providers[1].Handlers[operation]).IsEqualTo(typeId);
            }
            await Assert.That(pages[0].Number).IsEqualTo(1);
            await Assert.That(pages[1].Number).IsEqualTo(2);
            await Assert.That(pages[1].Cookie).IsEqualTo("next-page");
        }
    }

    [Test]
    public async Task List_MissingAndNullHandlers_AreNotInvented()
    {
        var service = new FakeOrganizationServiceAsync();
        var typeId = Guid.NewGuid();
        service.AddRequests(new ProviderRequestFake(typeof(RetrieveMultipleRequest), _ =>
        {
            var result = new EntityCollection();
            result.Entities.Add(new Entity("entitydataprovider", Guid.NewGuid())
            {
                ["name"] = "Provider", ["datasourcelogicalname"] = "dgt_source",
                ["retrieveplugin"] = typeId, ["updateplugin"] = null
            });
            return new RetrieveMultipleResponse { Results = { ["EntityCollection"] = result } };
        }));

        var provider = (await new EntityDataProviderRepository(service).ListAsync()).Single();
        await Assert.That(provider.Handlers).Count().IsEqualTo(1);
        await Assert.That(provider.Handlers[DataProviderOperation.Retrieve]).IsEqualTo(typeId);
    }

    [Test]
    public async Task Apply_UnsupportedOperation_ThrowsBeforeProviderWrite()
    {
        var service = new FakeOrganizationServiceAsync();
        var repository = new EntityDataProviderRepository(service);
        await Assert.That(async () => await repository.ApplyAsync(Guid.NewGuid(), Provider(),
            new Dictionary<DataProviderOperation, Guid> { [(DataProviderOperation)99] = Guid.NewGuid() }))
            .ThrowsExactly<AssemblyException>();
    }

    [Test]
    public async Task Apply_ExistingProvider_PreservesDescriptionAndUndeclaredOperations()
    {
        var service = new FakeOrganizationServiceAsync();
        service.AddDefaultRequests();
        var providerId = Guid.NewGuid();
        var createId = Guid.NewGuid();
        var retrieveId = Guid.NewGuid();
        service.Create(new Entity("entitydataprovider", providerId)
        {
            ["name"] = "Old", ["description"] = "Keep", ["datasourcelogicalname"] = "dgt_source", ["createplugin"] = createId
        });
        await new EntityDataProviderRepository(service).ApplyAsync(providerId, Provider(), new Dictionary<DataProviderOperation, Guid> { [DataProviderOperation.Retrieve] = retrieveId });
        var actual = service.Retrieve("entitydataprovider", providerId, new Microsoft.Xrm.Sdk.Query.ColumnSet(true));
        using (Assert.Multiple())
        {
            await Assert.That(actual.GetAttributeValue<string>("name")).IsEqualTo("Provider");
            await Assert.That(actual.GetAttributeValue<string>("description")).IsEqualTo("Keep");
            await Assert.That(actual.GetAttributeValue<Guid>("createplugin")).IsEqualTo(createId);
            await Assert.That(actual.GetAttributeValue<Guid>("retrieveplugin")).IsEqualTo(retrieveId);
        }
    }

    [Test]
    public async Task Apply_NewProvider_UsesGuidFieldsAndLeavesUndeclaredHandlersUnset()
    {
        var service = new FakeOrganizationServiceAsync();
        service.AddDefaultRequests();
        var metadata = new EntityMetadata { LogicalName = "entitydataprovider" };
        metadata.SetAttributeCollection(new AttributeMetadata[]
        {
            new StringAttributeMetadata { LogicalName = "name" },
            new StringAttributeMetadata { LogicalName = "description" },
            new StringAttributeMetadata { LogicalName = "datasourcelogicalname" }
        }.Concat(Enum.GetValues<DataProviderOperation>().Select(operation =>
            new UniqueIdentifierAttributeMetadata { LogicalName = operation.HandlerField() })));
        service.AddMetadata(metadata);
        var retrieveId = Guid.NewGuid();
        var repository = new EntityDataProviderRepository(service);
        var id = await repository.ApplyAsync(null, Provider(), new Dictionary<DataProviderOperation, Guid> { [DataProviderOperation.Retrieve] = retrieveId });
        var actual = service.Retrieve("entitydataprovider", id, new Microsoft.Xrm.Sdk.Query.ColumnSet(true));
        using (Assert.Multiple())
        {
            await Assert.That(actual.GetAttributeValue<Guid>("retrieveplugin")).IsEqualTo(retrieveId);
            foreach (var operation in Enum.GetValues<DataProviderOperation>().Where(operation => operation != DataProviderOperation.Retrieve))
            {
                await Assert.That(actual.Contains(operation.HandlerField())).IsFalse();
            }
            await Assert.That(actual.GetAttributeValue<string>("datasourcelogicalname")).IsEqualTo("dgt_source");
            var handlers = (await repository.ListAsync()).Single().Handlers;
            await Assert.That(handlers).Count().IsEqualTo(1);
            await Assert.That(handlers[DataProviderOperation.Retrieve]).IsEqualTo(retrieveId);
        }
    }

    [Test]
    [Arguments("dgt_Source", "dgt_name")]
    [Arguments("contoso_My_DataSource", "contoso_name")]
    [Arguments("DGT_Source", "dgt_name")]
    public async Task CreateDataSource_UsesJsonConverterMetadataAndPublishesOnlyItsTable(string schemaName, string primaryAttributeName)
    {
        var provider = Provider() with { DataSourceSchemaName = schemaName };
        var service = new FakeOrganizationServiceAsync();
        CreateEntityRequest? create = null;
        PublishXmlRequest? publish = null;
        UpdateAttributeRequest? updateId = null;
        var metadataId = Guid.NewGuid();
        var backingProviderId = Guid.NewGuid();
        service.AddRequests(new ProviderRequestFake(typeof(RetrieveMultipleRequest), request =>
        {
            var query = (QueryExpression)((RetrieveMultipleRequest)request).Query;
            if (query.EntityName == EntityDataProvider.EntityLogicalName)
            {
                return new RetrieveMultipleResponse
                {
                    Results = { ["EntityCollection"] = new EntityCollection(new List<Entity> { new EntityDataProvider(backingProviderId) { Name = "JsonConverter" } }) }
                };
            }

            return new RetrieveMultipleResponse
            {
                Results = { ["EntityCollection"] = new EntityCollection(new List<Entity> { new Entity("organization") { ["languagecode"] = 1033 } }) }
            };
        }));
        service.AddRequests(new ProviderRequestFake(typeof(CreateEntityRequest), request =>
        {
            create = (CreateEntityRequest)request;
            return new CreateEntityResponse { Results = { ["EntityId"] = metadataId } };
        }));
        service.AddRequests(new ProviderRequestFake(typeof(RetrieveAttributeRequest), _ =>
            new RetrieveAttributeResponse { Results = { ["AttributeMetadata"] = new UniqueIdentifierAttributeMetadata { LogicalName = "dgt_sourceid" } } }));
        service.AddRequests(new ProviderRequestFake(typeof(UpdateAttributeRequest), request =>
        {
            updateId = (UpdateAttributeRequest)request;
            return new UpdateAttributeResponse();
        }));
        service.AddRequests(new ProviderRequestFake(typeof(PublishXmlRequest), request =>
        {
            publish = (PublishXmlRequest)request;
            return new PublishXmlResponse();
        }));
        service.AddDefaultRequests();
        service.AddMetadata(new EntityMetadata { LogicalName = "organization" });
        service.Create(new Entity("organization", Guid.NewGuid()) { ["languagecode"] = 1033 });
        var result = await new EntityDataProviderRepository(service).CreateDataSourceAsync(provider, "target");
        using (Assert.Multiple())
        {
            await Assert.That(result).IsEqualTo(metadataId);
            await Assert.That(create!.Entity.DataProviderId).IsEqualTo(backingProviderId);
            await Assert.That(create.Entity.OwnershipType).IsEqualTo(OwnershipTypes.OrganizationOwned);
            await Assert.That(create.Entity.CanCreateCharts.Value).IsFalse();
            await Assert.That(create.Entity.CanChangeTrackingBeEnabled.Value).IsFalse();
            await Assert.That(create.Entity.ExternalName).IsEqualTo(schemaName);
            await Assert.That(create.Entity.ExternalCollectionName).IsEqualTo(schemaName);
            await Assert.That(create.Entity.DisplayName.LocalizedLabels.Single().Label).IsEqualTo(schemaName);
            await Assert.That(create.Entity.DisplayCollectionName.LocalizedLabels.Single().Label).IsEqualTo($"{schemaName} Records");
            await Assert.That(create.PrimaryAttribute.SchemaName).IsEqualTo(primaryAttributeName);
            await Assert.That(create.PrimaryAttribute.ExternalName).IsEqualTo(primaryAttributeName);
            await Assert.That(create["SolutionUniqueName"]).IsEqualTo("target");
            await Assert.That(updateId!.Attribute.ExternalName).IsEqualTo($"{schemaName}Id");
            await Assert.That(publish!.ParameterXml).IsEqualTo($"<importexportxml><entities><entity>{provider.DataSourceLogicalName}</entity></entities></importexportxml>");
        }
    }

    [Test]
    public async Task UpdateDataSource_OmittedPlural_DoesNotSendPluralOrOtherMetadata()
    {
        var service = new FakeOrganizationServiceAsync();
        service.AddDefaultRequests();
        service.AddMetadata(new EntityMetadata { LogicalName = "organization" });
        service.Create(new Entity("organization", Guid.NewGuid()) { ["languagecode"] = 1033 });
        UpdateEntityRequest? update = null;
        service.AddRequests(new ProviderRequestFake(typeof(UpdateEntityRequest), request =>
        {
            update = (UpdateEntityRequest)request;
            return new UpdateEntityResponse();
        }));
        service.AddRequests(new ProviderRequestFake(typeof(PublishXmlRequest), _ => new PublishXmlResponse()));
        var provider = Provider() with { DataSourceDisplayName = "New name" };
        await new EntityDataProviderRepository(service).UpdateDataSourceAsync(new EntityMetadata { MetadataId = Guid.NewGuid(), LogicalName = "dgt_source" }, provider);
        using (Assert.Multiple())
        {
            await Assert.That(update!.MergeLabels).IsTrue();
            await Assert.That(update.Entity.DisplayName.LocalizedLabels.Single().Label).IsEqualTo("New name");
            await Assert.That(update.Entity.DisplayCollectionName).IsNull();
            await Assert.That(update.Entity.DataProviderId).IsNull();
        }
    }
}
