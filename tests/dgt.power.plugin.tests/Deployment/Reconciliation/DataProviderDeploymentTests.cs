// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Security.Cryptography;
using dgt.power.dataverse;
using dgt.power.plugin.Deployment.Local;
using dgt.power.plugin.Deployment.Output;
using dgt.power.plugin.Deployment.Planning.Plans;
using dgt.power.plugin.Deployment.Reconciliation;
using dgt.power.plugin.Exceptions;
using dgt.power.plugin.Models;
using dgt.power.tests.FakeExecutor;
using Digitall.Dataverse.Testing;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Spectre.Console.Testing;

namespace dgt.power.plugin.tests.Deployment.Reconciliation;

public class DataProviderDeploymentTests
{
    private static LocalAssembly Desired(params DataProviderOperation[] operations) => new()
    {
        Name = "Plugins", Version = new Version(1, 0, 0, 0), Content = "Y29udGVudA==",
        ContentHash = Convert.ToHexString(SHA256.HashData("content"u8)),
        PluginTypes = [new LocalPluginType("Plugin", "Plugin", string.Empty, true, [])
        {
            DataProviders = operations.Select(operation => new LocalDataProviderRegistration("dgt_Source", operation,
                "Provider", null, null, null)).ToList()
        }]
    };

    private static FakeOrganizationServiceAsync Service(bool missing = false, bool incompatible = false)
    {
        var service = new FakeOrganizationServiceAsync();
        service.AddDefaultRequests();
        service.AddRequests(new RetrieveDependenciesForDeleteExecutor());
        ProviderTableFixture.Seed(service, missing, incompatible);
        var desired = Desired();
        service.Create(new PluginAssembly
        {
            Name = desired.Name, Version = desired.Version.ToString(), Content = desired.Content,
            SourceType = new OptionSetValue(0), IsolationMode = new OptionSetValue(2)
        });
        return service;
    }

    private static IReadOnlyList<Entity> All(FakeOrganizationServiceAsync service, string name) =>
        service.RetrieveMultiple(new QueryExpression(name) { ColumnSet = new ColumnSet(true) }).Entities;

    private static Entity Provider(FakeOrganizationServiceAsync service) =>
        All(service, EntityDataProvider.EntityLogicalName).Single(provider => provider.GetAttributeValue<string>("name") == "Provider");

    [Test]
    public async Task PrepareAndRender_NewProvider_PerformsNoWrites()
    {
        var service = Service();
        var changes = await new PluginDeployment(service).PrepareAsync(Desired(DataProviderOperation.Retrieve));
        using var console = new TestConsole();

        new DeploymentRenderer(console).Render(changes);

        await Assert.That(console.Output).Contains("Data providers");
        await Assert.That(console.Output).Contains("Provider (dgt_source) Create");
        await Assert.That(console.Output).Contains("Retrieve Set to Plugins / Plugin");
        await Assert.That(All(service, EntityDataProvider.EntityLogicalName)).Count().IsEqualTo(1);
        await Assert.That(All(service, PluginType.EntityLogicalName)).IsEmpty();
    }

    [Test]
    public async Task Apply_NewProvider_CreatesAllDeclaredHandlersAndIsIdempotent()
    {
        var service = Service();
        var desired = Desired(Enum.GetValues<DataProviderOperation>());
        var deployment = new PluginDeployment(service);

        await deployment.ApplyAsync(await deployment.PrepareAsync(desired));

        var typeId = All(service, PluginType.EntityLogicalName).Single().Id;
        foreach (var operation in Enum.GetValues<DataProviderOperation>())
        {
            await Assert.That(Provider(service).GetAttributeValue<Guid>(operation.HandlerField())).IsEqualTo(typeId);
        }
        await Assert.That((await deployment.PrepareAsync(desired)).HasChanges).IsFalse();
    }

    [Test]
    public async Task Apply_DeclaredProvider_ClearsUndeclaredExternalHandlersAndReassignsDeclaredHandler()
    {
        var service = Service();
        var external = Guid.NewGuid();
        var id = service.Create(new EntityDataProvider
        {
            Name = "Provider", DataSourceLogicalName = "dgt_source", Description = "preserve omitted description",
            Attributes = { ["retrieveplugin"] = external, ["updateplugin"] = external }
        });
        var deployment = new PluginDeployment(service);
        var changes = await deployment.PrepareAsync(Desired(DataProviderOperation.Retrieve));

        await deployment.ApplyAsync(changes);

        await Assert.That(Provider(service).Id).IsEqualTo(id);
        await Assert.That(Provider(service).GetAttributeValue<Guid?>("updateplugin")).IsNull();
        await Assert.That(Provider(service).GetAttributeValue<Guid>("retrieveplugin")).IsEqualTo(All(service, PluginType.EntityLogicalName).Single().Id);
        await Assert.That(Provider(service).GetAttributeValue<string>("description")).IsEqualTo("preserve omitted description");
    }

    [Test]
    public async Task Apply_ProviderOmitted_ClearsOnlyOwnedHandlersAndPreservesRecord()
    {
        var service = Service();
        var deployment = new PluginDeployment(service);
        await deployment.ApplyAsync(await deployment.PrepareAsync(Desired(DataProviderOperation.Retrieve)));
        var provider = Provider(service);
        var external = Guid.NewGuid();
        service.Update(new Entity(EntityDataProvider.EntityLogicalName, provider.Id) { ["updateplugin"] = external });

        await deployment.ApplyAsync(await deployment.PrepareAsync(Desired()));

        await Assert.That(Provider(service).Id).IsEqualTo(provider.Id);
        await Assert.That(Provider(service).GetAttributeValue<Guid?>("retrieveplugin")).IsNull();
        await Assert.That(Provider(service).GetAttributeValue<Guid>("updateplugin")).IsEqualTo(external);
        await Assert.That((await deployment.PrepareAsync(Desired())).HasChanges).IsFalse();
    }

    [Test]
    [Arguments(true, false)]
    [Arguments(false, true)]
    public async Task Prepare_MissingOrIncompatibleTable_FailsBeforeDeploymentWrites(bool missing, bool incompatible)
    {
        var service = Service(missing, incompatible);

        await Assert.That(async () => await new PluginDeployment(service).PrepareAsync(Desired(DataProviderOperation.Retrieve)))
            .ThrowsExactly<InvalidDataSourceException>();
        await Assert.That(All(service, PluginType.EntityLogicalName)).IsEmpty();
        await Assert.That(All(service, EntityDataProvider.EntityLogicalName)).Count().IsEqualTo(1);
    }

    [Test]
    public async Task Prepare_AmbiguousProvider_RejectsBeforeWrites()
    {
        var service = Service();
        service.Create(new EntityDataProvider { Name = "First", DataSourceLogicalName = "dgt_source" });
        service.Create(new EntityDataProvider { Name = "Second", DataSourceLogicalName = "dgt_source" });

        await Assert.That(async () => await new PluginDeployment(service).PrepareAsync(Desired(DataProviderOperation.Retrieve)))
            .ThrowsExactly<InvalidOperationException>();
        await Assert.That(All(service, PluginType.EntityLogicalName)).IsEmpty();
    }

    [Test]
    public async Task Prepare_ProviderWithRequestedSolution_MissingComponentDefinitionFailsBeforeWrites()
    {
        var service = Service();
        service.Create(new Solution { UniqueName = "target" });

        await Assert.That(async () => await new PluginDeployment(service).PrepareAsync(Desired(DataProviderOperation.Retrieve), "target"))
            .ThrowsExactly<InvalidOperationException>();
        await Assert.That(All(service, PluginType.EntityLogicalName)).IsEmpty();
    }

    [Test]
    public async Task Apply_MetadataOnlyChange_UpdatesProviderWithoutTouchingHandlers()
    {
        var service = Service();
        var desired = Desired(DataProviderOperation.Retrieve);
        var deployment = new PluginDeployment(service);
        await deployment.ApplyAsync(await deployment.PrepareAsync(desired));
        var providerId = Provider(service).Id;
        var handlerId = Provider(service).GetAttributeValue<Guid>("retrieveplugin");
        desired = desired with
        {
            PluginTypes = [desired.PluginTypes[0] with
            {
                DataProviders = [desired.PluginTypes[0].DataProviders[0] with { ProviderName = "Renamed", Description = "new description" }]
            }]
        };

        var changes = await deployment.PrepareAsync(desired);
        await Assert.That(changes.Registrations.HasChanges).IsFalse();
        await Assert.That(changes.Providers.Single().Handlers).IsEmpty();
        await Assert.That(changes.HasChanges).IsTrue();
        await deployment.ApplyAsync(changes);

        var actual = service.Retrieve(EntityDataProvider.EntityLogicalName, providerId, new ColumnSet(true));
        await Assert.That(actual.GetAttributeValue<string>("name")).IsEqualTo("Renamed");
        await Assert.That(actual.GetAttributeValue<string>("description")).IsEqualTo("new description");
        await Assert.That(actual.GetAttributeValue<Guid>("retrieveplugin")).IsEqualTo(handlerId);
        await Assert.That((await deployment.PrepareAsync(desired)).HasChanges).IsFalse();
    }

    [Test]
    public async Task Apply_CancelledAfterProviderReassignment_StopsCleanupAndRetryCompletes()
    {
        var service = Service();
        var desired = Desired(DataProviderOperation.Retrieve);
        var deployment = new PluginDeployment(service);
        await deployment.ApplyAsync(await deployment.PrepareAsync(desired));
        desired = desired with { Version = new Version(2, 0, 0, 0) };
        using var cancellation = new CancellationTokenSource();

        await Assert.That(async () => await deployment.ApplyAsync(await deployment.PrepareAsync(desired), progress =>
        {
            if (progress.Resource == "data provider")
            {
                cancellation.Cancel();
            }
        }, cancellation.Token)).Throws<OperationCanceledException>();

        await Assert.That(All(service, PluginAssembly.EntityLogicalName)).Count().IsEqualTo(2);
        var target = All(service, PluginAssembly.EntityLogicalName).Single(assembly => assembly.GetAttributeValue<string>("version") is null);
        service.Update(new PluginAssembly(target.Id) { Version = desired.Version.ToString(), SourceType = new OptionSetValue(0), IsolationMode = new OptionSetValue(2) });
        await deployment.ApplyAsync(await deployment.PrepareAsync(desired));
        await Assert.That(All(service, PluginAssembly.EntityLogicalName)).Count().IsEqualTo(1);
    }

    [Test]
    public async Task Apply_UpgradeInterruptedAfterProviderWrite_RetryPreservesProviderAndCompletesCleanup()
    {
        var service = Service();
        var desired = Desired(DataProviderOperation.Retrieve);
        var deployment = new PluginDeployment(service);
        await deployment.ApplyAsync(await deployment.PrepareAsync(desired));
        var providerId = Provider(service).Id;
        var oldType = All(service, PluginType.EntityLogicalName).Single().Id;
        desired = desired with { Version = new Version(2, 0, 0, 0) };

        await Assert.That(async () => await deployment.ApplyAsync(await deployment.PrepareAsync(desired), progress =>
        {
            if (progress.Resource == "data provider")
            {
                throw new InvalidOperationException("Interrupted after provider write.");
            }
        })).ThrowsExactly<InvalidOperationException>();

        await Assert.That(All(service, PluginAssembly.EntityLogicalName)).Count().IsEqualTo(2);
        await Assert.That(Provider(service).GetAttributeValue<Guid>("retrieveplugin")).IsNotEqualTo(oldType);
        var target = All(service, PluginAssembly.EntityLogicalName).Single(assembly => assembly.GetAttributeValue<string>("version") is null);
        service.Update(new PluginAssembly(target.Id) { Version = desired.Version.ToString(), SourceType = new OptionSetValue(0), IsolationMode = new OptionSetValue(2) });
        var retry = await deployment.PrepareAsync(desired);
        await Assert.That(retry.Providers).IsEmpty();
        await deployment.ApplyAsync(retry);

        await Assert.That(Provider(service).Id).IsEqualTo(providerId);
        await Assert.That(All(service, PluginAssembly.EntityLogicalName)).Count().IsEqualTo(1);
        await Assert.That(All(service, PluginType.EntityLogicalName)).Count().IsEqualTo(1);
    }
}
