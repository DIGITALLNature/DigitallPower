// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Security.Cryptography;
using dgt.power.dataverse;
using dgt.power.plugin.Deployment.Local;
using dgt.power.plugin.Deployment.Output;
using dgt.power.plugin.Deployment.Reconciliation;
using dgt.power.plugin.Exceptions;
using dgt.power.tests.FakeExecutor;
using Digitall.Dataverse.Testing;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Spectre.Console.Testing;

namespace dgt.power.plugin.tests.Deployment.Reconciliation;

public class StandalonePluginDeploymentTests
{
    private static FakeOrganizationServiceAsync Service()
    {
        var service = new FakeOrganizationServiceAsync();
        service.AddDefaultRequests();
        service.AddRequests(new RetrieveDependenciesForDeleteExecutor());
        service.Create(new SdkMessage(Guid.NewGuid()) { Name = "Update" });
        return service;
    }

    private static LocalPluginStep Step() => new("step", 0, "Update", 20, "none", "none", null, 100,
        [new LocalPluginStepImage(0, "PreImage", "PreImage", "Target", ["name"])]);

    private static LocalAssembly Assembly(params LocalPluginStep[] steps) => new()
    {
        Name = "Plugins", Version = new Version(1, 0, 0, 0), Content = "Y29udGVudA==",
        ContentHash = Convert.ToHexString(SHA256.HashData("content"u8)),
        PluginTypes = [new LocalPluginType("Plugin", "Plugin", string.Empty, true, steps)]
    };

    private static void SeedAssembly(FakeOrganizationServiceAsync service, LocalAssembly desired)
    {
        var id = Guid.NewGuid();
        service.Create(new PluginAssembly(id)
        {
            Name = desired.Name, Version = desired.Version.ToString(), Content = desired.Content,
            SourceType = new OptionSetValue(0), IsolationMode = new OptionSetValue(2)
        });
    }

    private static IReadOnlyList<Entity> All(FakeOrganizationServiceAsync service, string name) =>
        service.RetrieveMultiple(new QueryExpression(name) { ColumnSet = new ColumnSet(true) }).Entities;

    [Test]
    public async Task PrepareAndRender_NewAssembly_PerformNoWrites()
    {
        var service = Service();
        var deployment = new PluginDeployment(service);
        using var console = new TestConsole();

        var changes = await deployment.PrepareAsync(Assembly(Step()));
        new DeploymentRenderer(console).Render(changes);

        await Assert.That(changes.HasChanges).IsTrue();
        await Assert.That(All(service, "pluginassembly")).Count().IsEqualTo(0);
        await Assert.That(All(service, "plugintype")).Count().IsEqualTo(0);
        await Assert.That(All(service, "sdkmessageprocessingstep")).Count().IsEqualTo(0);
        await Assert.That(console.Output).Contains("Plugins v1.0.0.0 + Create");
        await Assert.That(console.Output).Contains("PreImage + Create");
    }

    [Test]
    public async Task Apply_NewAssembly_CreatesRegistrationsAndReportsActualWrites()
    {
        var service = Service();
        var deployment = new PluginDeployment(service);
        var changes = await deployment.PrepareAsync(Assembly(Step()));
        var progress = new List<PluginDeploymentProgress>();

        var id = await deployment.ApplyAsync(changes, progress.Add);

        await Assert.That(All(service, "pluginassembly").Single().Id).IsEqualTo(id);
        await Assert.That(All(service, "plugintype")).Count().IsEqualTo(1);
        await Assert.That(All(service, "sdkmessageprocessingstep")).Count().IsEqualTo(1);
        await Assert.That(All(service, "sdkmessageprocessingstepimage")).Count().IsEqualTo(1);
        await Assert.That(progress).Count().IsEqualTo(4);
        await Assert.That(progress.All(item => item.Operation == PluginDeploymentOperation.Created)).IsTrue();
    }

    [Test]
    public async Task ApplyThenPrepare_ExistingAssembly_IsIdempotent()
    {
        var service = Service();
        var desired = Assembly(Step());
        SeedAssembly(service, desired);
        var deployment = new PluginDeployment(service);
        await deployment.ApplyAsync(await deployment.PrepareAsync(desired));

        var repeated = await deployment.PrepareAsync(desired);
        var progress = new List<PluginDeploymentProgress>();
        await deployment.ApplyAsync(repeated, progress.Add);

        await Assert.That(repeated.HasChanges).IsFalse();
        await Assert.That(progress).Count().IsEqualTo(0);
        await Assert.That(All(service, "sdkmessageprocessingstep")).Count().IsEqualTo(1);
    }

    [Test]
    public async Task Apply_Update_PreservesExistingIdentityWithoutProvisioning()
    {
        var service = Service();
        var desired = Assembly();
        SeedAssembly(service, desired);
        var assemblyId = All(service, PluginAssembly.EntityLogicalName).Single().Id;
        var identityId = service.Create(new ManagedIdentity { ApplicationId = Guid.NewGuid() });
        service.Update(new PluginAssembly(assemblyId) { ManagedIdentityId = new EntityReference(ManagedIdentity.EntityLogicalName, identityId) });
        desired = desired with { Content = "bmV3", ContentHash = Convert.ToHexString(SHA256.HashData("new"u8)) };
        var deployment = new PluginDeployment(service);

        var id = await deployment.ApplyAsync(await deployment.PrepareAsync(desired));

        var actual = service.Retrieve(PluginAssembly.EntityLogicalName, id, new ColumnSet(true)).ToEntity<PluginAssembly>();
        await Assert.That(id).IsEqualTo(assemblyId);
        await Assert.That(actual.Content).IsEqualTo(desired.Content);
        await Assert.That(actual.ManagedIdentityId!.Id).IsEqualTo(identityId);
        await Assert.That(All(service, ManagedIdentity.EntityLogicalName)).Count().IsEqualTo(1);
        await Assert.That((await deployment.PrepareAsync(desired)).HasChanges).IsFalse();
    }

    [Test]
    public async Task Apply_UpdateStepAndImage_PreservesIdsAndEnvironmentFields()
    {
        var service = Service();
        var step = Step();
        var desired = Assembly(step);
        SeedAssembly(service, desired);
        var deployment = new PluginDeployment(service);
        await deployment.ApplyAsync(await deployment.PrepareAsync(desired));
        var existing = All(service, "sdkmessageprocessingstep").Single();
        var imageId = All(service, "sdkmessageprocessingstepimage").Single().Id;
        var secureId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        service.Update(new Entity("sdkmessageprocessingstep", existing.Id)
        {
            ["configuration"] = "environment config",
            ["sdkmessageprocessingstepsecureconfigid"] = new EntityReference("sdkmessageprocessingstepsecureconfig", secureId),
            ["statecode"] = new OptionSetValue(1),
            ["impersonatinguserid"] = new EntityReference("systemuser", userId)
        });
        var updated = step with { Name = "new name", ExecutionOrder = 200, Images = [step.Images[0] with { Attributes = ["telephone1"] }] };
        var changes = await deployment.PrepareAsync(Assembly(updated));

        await deployment.ApplyAsync(changes);

        var actual = All(service, "sdkmessageprocessingstep").Single();
        await Assert.That(actual.Id).IsEqualTo(existing.Id);
        await Assert.That(actual.GetAttributeValue<string>("configuration")).IsEqualTo("environment config");
        await Assert.That(actual.GetAttributeValue<EntityReference>("sdkmessageprocessingstepsecureconfigid").Id).IsEqualTo(secureId);
        await Assert.That(actual.GetAttributeValue<OptionSetValue>("statecode").Value).IsEqualTo(1);
        await Assert.That(actual.GetAttributeValue<EntityReference>("impersonatinguserid").Id).IsEqualTo(userId);
        await Assert.That(All(service, "sdkmessageprocessingstepimage").Single().Id).IsEqualTo(imageId);
        await Assert.That((await deployment.PrepareAsync(Assembly(updated))).HasChanges).IsFalse();
    }

    [Test]
    public async Task Apply_RemovedStep_DeletesOnlyAfterPreparingChanges()
    {
        var service = Service();
        var desired = Assembly(Step());
        SeedAssembly(service, desired);
        var deployment = new PluginDeployment(service);
        await deployment.ApplyAsync(await deployment.PrepareAsync(desired));

        var changes = await deployment.PrepareAsync(Assembly());
        using var console = new TestConsole();
        new DeploymentRenderer(console).Render(changes);

        await Assert.That(All(service, "sdkmessageprocessingstep")).Count().IsEqualTo(1);
        await Assert.That(console.Output).Contains("Destructive operations: 1");
        await deployment.ApplyAsync(changes);
        await Assert.That(All(service, "sdkmessageprocessingstep")).Count().IsEqualTo(0);
    }

    [Test]
    public async Task Prepare_UnresolvedMessage_FailsBeforeWrites()
    {
        var service = Service();
        var deployment = new PluginDeployment(service);

        await Assert.That(async () => await deployment.PrepareAsync(Assembly(Step() with { MessageName = "missing" })))
            .ThrowsExactly<UnresolvedPluginStepMessageException>();
        await Assert.That(All(service, "pluginassembly")).Count().IsEqualTo(0);
    }

    [Test]
    public async Task Apply_Cancelled_DoesNotWrite()
    {
        var service = Service();
        var deployment = new PluginDeployment(service);
        var changes = await deployment.PrepareAsync(Assembly(Step()));
        using var cancellation = new CancellationTokenSource();
        await cancellation.CancelAsync();

        await Assert.That(async () => await deployment.ApplyAsync(changes, cancellationToken: cancellation.Token)).Throws<OperationCanceledException>();
        await Assert.That(All(service, "pluginassembly")).Count().IsEqualTo(0);
    }

    [Test]
    public async Task Prepare_Upgrade_PreparesReplacementWithoutWrites()
    {
        var service = Service();
        var desired = Assembly(Step());
        SeedAssembly(service, desired);
        var deployment = new PluginDeployment(service);

        var changes = await deployment.PrepareAsync(desired with { Version = new Version(2, 0, 0, 0) });
        await Assert.That(changes.ExistingAssemblyId).IsNull();
        await Assert.That(changes.AssemblyToDelete).IsNotNull();
        await Assert.That(All(service, "pluginassembly")).Count().IsEqualTo(1);
    }

    [Test]
    public async Task Apply_IdenticalStepValuesInDifferentTypes_UsesScopedReferences()
    {
        var service = Service();
        var step = Step();
        var desired = Assembly(step);
        desired = desired with { PluginTypes = [desired.PluginTypes[0], desired.PluginTypes[0] with { TypeName = "Other", Name = "Other" }] };
        var deployment = new PluginDeployment(service);

        await deployment.ApplyAsync(await deployment.PrepareAsync(desired));

        var steps = All(service, "sdkmessageprocessingstep");
        var images = All(service, "sdkmessageprocessingstepimage");
        await Assert.That(steps).Count().IsEqualTo(2);
        await Assert.That(steps.Select(item => item.GetAttributeValue<EntityReference>("eventhandler").Id).Distinct()).Count().IsEqualTo(2);
        await Assert.That(images.Select(item => item.GetAttributeValue<EntityReference>("sdkmessageprocessingstepid").Id).Distinct()).Count().IsEqualTo(2);
    }

    [Test]
    public async Task Apply_ChangedContentInSameVersionTrain_UpdatesExistingAssembly()
    {
        var service = Service();
        var desired = Assembly();
        SeedAssembly(service, desired);
        var existingId = All(service, "pluginassembly").Single().Id;
        desired = desired with
        {
            Version = new Version(1, 0, 1, 0), Content = "bmV3",
            ContentHash = Convert.ToHexString(SHA256.HashData("new"u8))
        };
        var deployment = new PluginDeployment(service);
        var changes = await deployment.PrepareAsync(desired);

        var id = await deployment.ApplyAsync(changes);

        await Assert.That(changes.UpdateContent).IsTrue();
        await Assert.That(id).IsEqualTo(existingId);
        await Assert.That(All(service, "pluginassembly").Single().GetAttributeValue<string>("content")).IsEqualTo(desired.Content);
    }

    [Test]
    public async Task Prepare_AmbiguousRemoteSteps_ThrowsWithoutChangingContent()
    {
        var service = Service();
        var desired = Assembly(Step());
        SeedAssembly(service, desired);
        var deployment = new PluginDeployment(service);
        await deployment.ApplyAsync(await deployment.PrepareAsync(desired));
        var original = All(service, "sdkmessageprocessingstep").Single();
        var duplicate = new Entity(original.LogicalName, Guid.NewGuid());
        foreach (var (key, value) in original.Attributes.Where(pair => pair.Key != "sdkmessageprocessingstepid"))
        {
            duplicate[key] = value;
        }
        service.Create(duplicate);

        await Assert.That(async () => await deployment.PrepareAsync(desired with { Content = "bmV3", ContentHash = "changed" }))
            .ThrowsExactly<InvalidPluginStepException>();
        await Assert.That(All(service, "pluginassembly").Single().GetAttributeValue<string>("content")).IsEqualTo(desired.Content);
    }

    [Test]
    public async Task Apply_RemovedType_DeletesOwnedStepsBeforeType()
    {
        var service = Service();
        var desired = Assembly(Step());
        SeedAssembly(service, desired);
        var deployment = new PluginDeployment(service);
        await deployment.ApplyAsync(await deployment.PrepareAsync(desired));

        var changes = await deployment.PrepareAsync(desired with { PluginTypes = [] });
        await deployment.ApplyAsync(changes);
        await Assert.That(All(service, "plugintype")).Count().IsEqualTo(0);
        await Assert.That(All(service, "sdkmessageprocessingstep")).Count().IsEqualTo(0);
    }

    [Test]
    public async Task Prepare_MissingCustomApi_FailsBeforeWrites()
    {
        var service = Service();
        var desired = Assembly();
        desired = desired with { PluginTypes = [desired.PluginTypes[0] with { CustomApi = "my_api" }] };

        await Assert.That(async () => await new PluginDeployment(service).PrepareAsync(desired)).ThrowsExactly<MissingCustomApiException>();
        await Assert.That(All(service, "pluginassembly")).Count().IsEqualTo(0);
    }
}
