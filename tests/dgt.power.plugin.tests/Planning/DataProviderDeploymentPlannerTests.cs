// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Local;
using dgt.power.plugin.Planning;
using dgt.power.plugin.Planning.Deployment;
using dgt.power.plugin.Remote;
using dgt.power.plugin.Repositories;
using Digitall.Dataverse.Testing;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;

namespace dgt.power.plugin.tests.Planning;

public class DataProviderDeploymentPlannerTests
{
    internal static LocalPluginType Handler(string typeName = "Retrieve", string? singular = null) =>
        new(typeName, typeName, string.Empty, true, [])
        {
            DataProviders = [new LocalDataProviderRegistration("dgt_Source", DataProviderOperation.Retrieve, "Provider", singular, null, null)]
        };

    internal static EntityMetadata Table(Guid? providerId = null) => new()
    {
        MetadataId = Guid.NewGuid(), LogicalName = "dgt_source", SchemaName = "dgt_Source",
        DataProviderId = providerId ?? ProviderTestRepository.BackingProviderId,
        OwnershipType = OwnershipTypes.OrganizationOwned
    };

    private static Task<IReadOnlyList<DataProviderDeployment>> Plan(ProviderTestRepository repository, IReadOnlyList<LocalPluginType>? types = null,
        IReadOnlyList<RemotePluginType>? remoteTypes = null, IReadOnlyList<RemotePluginType>? replacedTypes = null) =>
        new DataProviderDeploymentPlanner(repository, new SolutionComponentRepository(new FakeOrganizationServiceAsync()), (_, _, _, _, _) => Task.FromResult<SolutionLink?>(null))
            .BuildAsync(types ?? [Handler()], remoteTypes ?? [], replacedTypes ?? [], null, CancellationToken.None);

    [Test]
    [Arguments(null, false)]
    [Arguments("", false)]
    [Arguments("Keep", true)]
    public async Task Build_ExplicitEmptyDescription_IsIdempotentAfterDataverseClearsValue(string? existing, bool expectedUpdate)
    {
        var typeId = Guid.NewGuid();
        var repository = new ProviderTestRepository { DataSource = Table() };
        repository.Providers.Add(new RemoteDataProvider(Guid.NewGuid(), "dgt_source", "Provider", existing,
            new Dictionary<DataProviderOperation, Guid> { [DataProviderOperation.Retrieve] = typeId }));
        var handler = Handler() with
        {
            DataProviders = [new LocalDataProviderRegistration("dgt_Source", DataProviderOperation.Retrieve, "Provider", null, null, string.Empty)]
        };
        var plan = (await Plan(repository, [handler], [new RemotePluginType(typeId, "Retrieve")])).Single();
        await Assert.That(plan.UpdateProvider).IsEqualTo(expectedUpdate);
    }

    [Test]
    [Arguments("Source", "Quelle", false)]
    [Arguments("Old", "Source", true)]
    public async Task Build_ExplicitLabel_ComparedInOrganizationBaseLanguage(string baseLabel, string callerLabel, bool expectedUpdate)
    {
        var typeId = Guid.NewGuid();
        var table = Table();
        table.DisplayName = new Label(baseLabel, 1033) { UserLocalizedLabel = new LocalizedLabel(callerLabel, 1031) };
        var repository = new ProviderTestRepository { DataSource = table };
        repository.Providers.Add(new RemoteDataProvider(Guid.NewGuid(), "dgt_source", "Provider", null,
            new Dictionary<DataProviderOperation, Guid> { [DataProviderOperation.Retrieve] = typeId }));
        var plan = (await Plan(repository, [Handler(singular: "Source")], [new RemotePluginType(typeId, "Retrieve")])).Single();
        await Assert.That(plan.UpdateDataSource).IsEqualTo(expectedUpdate);
    }

    [Test]
    public async Task Build_NewProvider_PlansTableAndProviderWithoutWrites()
    {
        var repository = new ProviderTestRepository();
        var plan = (await Plan(repository)).Single();
        using (Assert.Multiple())
        {
            await Assert.That(plan.UpdateProvider).IsTrue();
            await Assert.That(plan.UpdateDataSource).IsTrue();
            await Assert.That(repository.PlatformValidations).IsEqualTo(1);
            await Assert.That(repository.ProviderWrites).IsEqualTo(0);
            await Assert.That(repository.TableCreates).IsEqualTo(0);
        }
    }

    [Test]
    public async Task Build_ExistingProvider_OmittedMetadataPreservedAndUnchanged()
    {
        var typeId = Guid.NewGuid();
        var repository = new ProviderTestRepository { DataSource = Table() };
        repository.Providers.Add(new RemoteDataProvider(Guid.NewGuid(), "dgt_source", "Provider", "Keep description",
            new Dictionary<DataProviderOperation, Guid> { [DataProviderOperation.Retrieve] = typeId, [DataProviderOperation.Update] = Guid.NewGuid() }));
        var plan = (await Plan(repository, remoteTypes: [new RemotePluginType(typeId, "Retrieve")])).Single();
        using (Assert.Multiple())
        {
            await Assert.That(plan.UpdateProvider).IsFalse();
            await Assert.That(plan.UpdateDataSource).IsFalse();
            await Assert.That(plan.Handlers).Count().IsEqualTo(1);
            await Assert.That(plan.Local!.Description).IsNull();
        }
    }

    [Test]
    public async Task Build_AmbiguousProviders_FailsBeforeWrites()
    {
        var repository = new ProviderTestRepository { DataSource = Table() };
        repository.Providers.Add(new RemoteDataProvider(Guid.NewGuid(), "dgt_source", "First", null, new Dictionary<DataProviderOperation, Guid>()));
        repository.Providers.Add(new RemoteDataProvider(Guid.NewGuid(), "dgt_source", "Second", null, new Dictionary<DataProviderOperation, Guid>()));
        await Assert.That(async () => await Plan(repository)).ThrowsExactly<InvalidOperationException>();
    }

    [Test]
    public async Task Build_DataSourceValidationFailure_PropagatesBeforeWrites()
    {
        var table = Table(Guid.Empty);
        var repository = new ProviderTestRepository
        {
            DataSource = table,
            BeforeValidate = (logicalName, metadata) =>
            {
                if (logicalName != "dgt_source" || !ReferenceEquals(metadata, table))
                {
                    throw new ArgumentException("Unexpected validation arguments.");
                }
                throw new InvalidDataSourceException("Incompatible data-source table.");
            }
        };
        await Assert.That(async () => await Plan(repository)).ThrowsExactly<InvalidDataSourceException>();
        await Assert.That(repository.PlatformValidations).IsEqualTo(1);
        await Assert.That(repository.ProviderWrites).IsEqualTo(0);
        await Assert.That(repository.TableUpdates).IsEqualTo(0);
    }

    [Test]
    public async Task Build_DeletingReferencedType_FailsInsteadOfClearingUndeclaredHandler()
    {
        var typeId = Guid.NewGuid();
        var repository = new ProviderTestRepository();
        repository.Providers.Add(new RemoteDataProvider(Guid.NewGuid(), "dgt_other", "Other", null, new Dictionary<DataProviderOperation, Guid> { [DataProviderOperation.Retrieve] = typeId }));
        await Assert.That(async () => await Plan(repository, [], [new RemotePluginType(typeId, "Old")])).ThrowsExactly<InvalidOperationException>();
    }

    [Test]
    public async Task Build_ReplacedType_MovesUndeclaredOperationsToReplacement()
    {
        var typeId = Guid.NewGuid();
        var repository = new ProviderTestRepository();
        repository.Providers.Add(new RemoteDataProvider(Guid.NewGuid(), "dgt_source", "Provider", null,
            new Dictionary<DataProviderOperation, Guid> { [DataProviderOperation.Retrieve] = typeId, [DataProviderOperation.Update] = typeId }));
        var result = await Plan(repository, [new LocalPluginType("Retrieve", "Retrieve", string.Empty, true, [])], replacedTypes: [new RemotePluginType(typeId, "Retrieve")]);
        await Assert.That(result.Single(deployment => deployment.Local is null).Handlers[DataProviderOperation.Update]).IsEqualTo("Retrieve");
    }
}
