// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

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

public class StandaloneReplacementTests
{
    private static LocalAssembly Desired() => new()
    {
        Name = "Plugins", Version = new Version(2, 0, 0, 0), Content = "bmV3",
        ContentHash = Convert.ToHexString(SHA256.HashData("new"u8)),
        PluginTypes = [new LocalPluginType("Plugin", "Plugin", string.Empty, true,
            [new LocalPluginStep("step", 0, "Update", 20, "none", "none", null, 100,
                [new LocalPluginStepImage(0, "PreImage", "PreImage", "Target", ["name"])])])]
    };

    private static IReadOnlyList<Entity> All(FakeOrganizationServiceAsync service, string name) =>
        service.RetrieveMultiple(new QueryExpression(name) { ColumnSet = new ColumnSet(true) }).Entities;

    private static (FakeOrganizationServiceAsync Service, Guid OldAssembly, Guid OldType, Guid Step, Guid Image) Seed()
    {
        var service = new FakeOrganizationServiceAsync();
        service.AddDefaultRequests();
        service.AddRequests(new RetrieveDependenciesForDeleteExecutor());
        var assemblyId = SeedAssembly(service, "1.0.0.0", "b2xk");
        var typeId = Guid.NewGuid();
        var stepId = Guid.NewGuid();
        var messageId = Guid.NewGuid();
        var imageId = Guid.NewGuid();
        service.Create(new SdkMessage(messageId) { Name = "Update" });
        service.Create(new PluginType(typeId) { TypeName = "Plugin", PluginAssemblyId = new EntityReference("pluginassembly", assemblyId) });
        service.Create(new Entity("sdkmessageprocessingstep", stepId)
        {
            ["name"] = "step", ["eventhandler"] = new EntityReference("plugintype", typeId),
            ["sdkmessageid"] = new EntityReference("sdkmessage", messageId), ["stage"] = new OptionSetValue(20),
            ["mode"] = new OptionSetValue(0), ["rank"] = 100, ["configuration"] = "keep",
            ["statecode"] = new OptionSetValue(1), ["impersonatinguserid"] = new EntityReference("systemuser", Guid.NewGuid()),
            ["sdkmessageprocessingstepsecureconfigid"] = new EntityReference("sdkmessageprocessingstepsecureconfig", Guid.NewGuid())
        });
        service.Create(new Entity("sdkmessageprocessingstepimage", imageId)
        {
            ["name"] = "PreImage", ["imagetype"] = new OptionSetValue(0), ["attributes"] = "telephone1",
            ["sdkmessageprocessingstepid"] = new EntityReference("sdkmessageprocessingstep", stepId)
        });
        return (service, assemblyId, typeId, stepId, imageId);
    }

    private static Guid SeedAssembly(FakeOrganizationServiceAsync service, string version, string content)
    {
        var id = Guid.NewGuid();
        service.Create(new PluginAssembly(id)
        {
            Name = "Plugins", Version = version, Content = content, SourceType = new OptionSetValue(0), IsolationMode = new OptionSetValue(2)
        });
        return id;
    }

    [Test]
    public async Task Apply_Replacement_PreservesStepImageAndEnvironmentStateBeforeCleanup()
    {
        var (service, oldAssembly, oldType, stepId, imageId) = Seed();
        var environmentState = service.Retrieve("sdkmessageprocessingstep", stepId, new ColumnSet(true));
        var deployment = new PluginDeployment(service);
        var changes = await deployment.PrepareAsync(Desired());
        using var console = new TestConsole();
        new DeploymentRenderer(console).Render(changes);
        var progress = new List<PluginDeploymentProgress>();

        var targetId = await deployment.ApplyAsync(changes, progress.Add);

        var step = All(service, "sdkmessageprocessingstep").Single();
        await Assert.That(step.Id).IsEqualTo(stepId);
        await Assert.That(step.GetAttributeValue<string>("configuration")).IsEqualTo("keep");
        await Assert.That(step.GetAttributeValue<OptionSetValue>("statecode").Value).IsEqualTo(1);
        await Assert.That(step.GetAttributeValue<EntityReference>("impersonatinguserid").Id)
            .IsEqualTo(environmentState.GetAttributeValue<EntityReference>("impersonatinguserid").Id);
        await Assert.That(step.GetAttributeValue<EntityReference>("sdkmessageprocessingstepsecureconfigid").Id)
            .IsEqualTo(environmentState.GetAttributeValue<EntityReference>("sdkmessageprocessingstepsecureconfigid").Id);
        await Assert.That(step.GetAttributeValue<EntityReference>("eventhandler").Id).IsNotEqualTo(oldType);
        await Assert.That(All(service, "sdkmessageprocessingstepimage").Single().Id).IsEqualTo(imageId);
        await Assert.That(All(service, "pluginassembly").Single().Id).IsEqualTo(targetId);
        await Assert.That(targetId).IsNotEqualTo(oldAssembly);
        await Assert.That(console.Output).Contains("Plugins v1.0.0.0 - Delete");
        await Assert.That(console.Output).DoesNotContain("Superseded");
        await Assert.That(console.Output).Contains("\n🧩 Plugins v1.0.0.0");
        await Assert.That(progress.FindIndex(item => item is { Resource: "step", Operation: PluginDeploymentOperation.Updated }))
            .IsLessThan(progress.FindIndex(item => item is { Resource: "plugin type", Operation: PluginDeploymentOperation.Deleted }));
    }

    [Test]
    [Arguments("assembly", PluginDeploymentOperation.Created)]
    [Arguments("plugin type", PluginDeploymentOperation.Created)]
    [Arguments("step", PluginDeploymentOperation.Updated)]
    [Arguments("step image", PluginDeploymentOperation.Updated)]
    [Arguments("plugin type", PluginDeploymentOperation.Deleted)]
    public async Task Apply_FailureBetweenPhases_FreshPreparationCompletesRecovery(string resource, PluginDeploymentOperation operation)
    {
        var (service, oldAssembly, _, stepId, imageId) = Seed();
        var desired = Desired();
        var deployment = new PluginDeployment(service);
        var changes = await deployment.PrepareAsync(desired);

        // The callback deliberately interrupts a write; it is not an argument precondition.
        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
        await Assert.That(async () => await deployment.ApplyAsync(changes, progress =>
        {
            if (progress.Resource == resource && progress.Operation == operation)
            {
                throw new InvalidOperationException("Injected interruption after completed write.");
            }
        })).ThrowsExactly<InvalidOperationException>();

        var target = All(service, "pluginassembly").Single(assembly => assembly.Id != oldAssembly);
        // The SDK fake does not extract assembly identity from uploaded bytes.
        service.Update(new PluginAssembly(target.Id) { Version = desired.Version.ToString(), SourceType = new OptionSetValue(0), IsolationMode = new OptionSetValue(2) });
        var recovered = await deployment.PrepareAsync(desired);
        await deployment.ApplyAsync(recovered);

        await Assert.That(All(service, "pluginassembly").Single().Id).IsEqualTo(target.Id);
        await Assert.That(All(service, "plugintype")).Count().IsEqualTo(1);
        await Assert.That(All(service, "sdkmessageprocessingstep").Single().Id).IsEqualTo(stepId);
        await Assert.That(All(service, "sdkmessageprocessingstepimage").Single().Id).IsEqualTo(imageId);
        await Assert.That((await deployment.PrepareAsync(desired)).HasChanges).IsFalse();
    }

    [Test]
    public async Task Prepare_RecoveryContentMismatch_RejectsBeforeWrites()
    {
        var (service, _, _, _, _) = Seed();
        SeedAssembly(service, "2.0.0.0", "b3RoZXI=");

        await Assert.That(async () => await new PluginDeployment(service).PrepareAsync(Desired())).ThrowsExactly<InvalidOperationException>();
        await Assert.That(All(service, "pluginassembly")).Count().IsEqualTo(2);
    }

    [Test]
    [Arguments("2.0.0.0", "2.0.1.0")]
    [Arguments("3.0.0.0", "4.0.0.0")]
    public async Task Prepare_AmbiguousTrains_Rejects(string first, string second)
    {
        var (service, _, _, _, _) = Seed();
        foreach (var assembly in All(service, "pluginassembly"))
        {
            service.Delete("pluginassembly", assembly.Id);
        }
        SeedAssembly(service, first, "bmV3");
        SeedAssembly(service, second, "bmV3");

        await Assert.That(async () => await new PluginDeployment(service).PrepareAsync(Desired())).ThrowsExactly<InvalidOperationException>();
    }

    [Test]
    public async Task Prepare_MatchingStepOnBothAssemblies_RejectsAmbiguousConfiguration()
    {
        var (service, _, _, stepId, _) = Seed();
        var target = SeedAssembly(service, "2.0.0.0", "bmV3");
        var typeId = Guid.NewGuid();
        service.Create(new PluginType(typeId) { TypeName = "Plugin", PluginAssemblyId = new EntityReference("pluginassembly", target) });
        var old = service.Retrieve("sdkmessageprocessingstep", stepId, new ColumnSet(true));
        var duplicate = new Entity(old.LogicalName, Guid.NewGuid());
        foreach (var (key, value) in old.Attributes.Where(pair => pair.Key != "sdkmessageprocessingstepid"))
        {
            duplicate[key] = value;
        }
        duplicate["eventhandler"] = new EntityReference("plugintype", typeId);
        service.Create(duplicate);

        await Assert.That(async () => await new PluginDeployment(service).PrepareAsync(Desired())).ThrowsExactly<InvalidPluginStepException>();
    }

    [Test]
    [Arguments(30)]
    [Arguments(80)]
    [Arguments(90)]
    public async Task Apply_ReplacementWithRemainingPlatformStep_StopsBeforeDeletingOldType(int stage)
    {
        var (service, oldAssembly, oldType, stepId, _) = Seed();
        service.Update(new Entity("sdkmessageprocessingstep", stepId) { ["stage"] = new OptionSetValue(stage) });
        var deployment = new PluginDeployment(service);

        var changes = await deployment.PrepareAsync(Desired());

        await Assert.That(async () => await deployment.ApplyAsync(changes)).ThrowsExactly<InvalidOperationException>();
        await Assert.That(All(service, "pluginassembly").Any(assembly => assembly.Id == oldAssembly)).IsTrue();
        await Assert.That(All(service, "plugintype").Any(type => type.Id == oldType)).IsTrue();
    }

    [Test]
    public async Task Apply_OmittedProviderReferencesOldType_ClearsOwnedHandlerBeforeCleanup()
    {
        var (service, _, typeId, _, _) = Seed();
        var providerId = service.Create(new Entity("entitydataprovider", Guid.NewGuid()) { ["name"] = "Provider", ["retrieveplugin"] = typeId });

        var deployment = new PluginDeployment(service);
        await deployment.ApplyAsync(await deployment.PrepareAsync(Desired()));
        await Assert.That(service.Retrieve("entitydataprovider", providerId, new ColumnSet(true)).GetAttributeValue<Guid?>("retrieveplugin")).IsNull();
    }

    [Test]
    public async Task Apply_ArtifactChangedSincePreparation_RequiresFreshPreview()
    {
        var (service, oldAssembly, _, _, _) = Seed();
        var deployment = new PluginDeployment(service);
        var changes = await deployment.PrepareAsync(Desired());
        service.Update(new PluginAssembly(oldAssembly) { Content = "Y2hhbmdlZA==" });

        await Assert.That(async () => await deployment.ApplyAsync(changes)).ThrowsExactly<InvalidOperationException>();
        await Assert.That(All(service, "pluginassembly")).Count().IsEqualTo(1);
    }

    [Test]
    public async Task Prepare_MoreThanTwoAssemblies_Rejects()
    {
        var (service, _, _, _, _) = Seed();
        SeedAssembly(service, "2.0.0.0", "bmV3");
        SeedAssembly(service, "3.0.0.0", "bmV3");

        await Assert.That(async () => await new PluginDeployment(service).PrepareAsync(Desired())).ThrowsExactly<InvalidOperationException>();
    }

    [Test]
    public async Task Apply_ManagedIdentityOnSupersededAssembly_DoesNotTransferOrDeleteSharedIdentity()
    {
        var (service, oldAssembly, _, _, _) = Seed();
        var identityId = service.Create(new ManagedIdentity { ApplicationId = Guid.NewGuid() });
        service.Update(new PluginAssembly(oldAssembly) { ManagedIdentityId = new EntityReference("managedidentity", identityId) });
        var deployment = new PluginDeployment(service);

        var id = await deployment.ApplyAsync(await deployment.PrepareAsync(Desired()));

        await Assert.That(service.Retrieve(PluginAssembly.EntityLogicalName, id, new ColumnSet(true)).ToEntity<PluginAssembly>().ManagedIdentityId).IsNull();
        await Assert.That(All(service, ManagedIdentity.EntityLogicalName).Single().Id).IsEqualTo(identityId);
        await Assert.That(All(service, PluginAssembly.EntityLogicalName)).Count().IsEqualTo(1);
    }

    [Test]
    public async Task Apply_RecoveryWithDifferentIdentityOnTarget_PreservesTargetBinding()
    {
        var (service, oldAssembly, _, _, _) = Seed();
        var oldIdentity = service.Create(new ManagedIdentity { ApplicationId = Guid.NewGuid() });
        var targetIdentity = service.Create(new ManagedIdentity { ApplicationId = Guid.NewGuid() });
        service.Update(new PluginAssembly(oldAssembly) { ManagedIdentityId = new EntityReference(ManagedIdentity.EntityLogicalName, oldIdentity) });
        var targetId = SeedAssembly(service, Desired().Version.ToString(), Desired().Content);
        service.Update(new PluginAssembly(targetId) { ManagedIdentityId = new EntityReference(ManagedIdentity.EntityLogicalName, targetIdentity) });
        var deployment = new PluginDeployment(service);

        var id = await deployment.ApplyAsync(await deployment.PrepareAsync(Desired()));

        await Assert.That(id).IsEqualTo(targetId);
        await Assert.That(service.Retrieve(PluginAssembly.EntityLogicalName, id, new ColumnSet(true)).ToEntity<PluginAssembly>().ManagedIdentityId!.Id).IsEqualTo(targetIdentity);
        await Assert.That(All(service, ManagedIdentity.EntityLogicalName)).Count().IsEqualTo(2);
        await Assert.That(All(service, PluginAssembly.EntityLogicalName)).Count().IsEqualTo(1);
    }

    [Test]
    public async Task Apply_CancellationAfterTypeCreation_FreshPreparationRecovers()
    {
        var (service, oldAssembly, _, stepId, _) = Seed();
        var deployment = new PluginDeployment(service);
        var desired = Desired();
        var changes = await deployment.PrepareAsync(desired);
        using var cancellation = new CancellationTokenSource();
        var token = cancellation.Token;

        await Assert.That(async () => await deployment.ApplyAsync(changes, progress =>
        {
            if (progress is { Resource: "plugin type", Operation: PluginDeploymentOperation.Created })
            {
                // The assertion is awaited before the cancellation source is disposed.
                // ReSharper disable once AccessToDisposedClosure
                cancellation.Cancel();
            }
        }, token)).Throws<OperationCanceledException>();

        var target = All(service, "pluginassembly").Single(assembly => assembly.Id != oldAssembly);
        service.Update(new PluginAssembly(target.Id) { Version = desired.Version.ToString(), SourceType = new OptionSetValue(0), IsolationMode = new OptionSetValue(2) });
        await deployment.ApplyAsync(await deployment.PrepareAsync(desired, cancellationToken: CancellationToken.None), cancellationToken: CancellationToken.None);

        await Assert.That(All(service, "pluginassembly")).Count().IsEqualTo(1);
        await Assert.That(All(service, "sdkmessageprocessingstep").Single().Id).IsEqualTo(stepId);
    }

    [Test]
    public async Task Apply_NewDependencyAfterPreparation_StopsBeforeOldTypeAndAssemblyDeletion()
    {
        var (service, oldAssembly, oldType, stepId, _) = Seed();
        var deployment = new PluginDeployment(service);
        var changes = await deployment.PrepareAsync(Desired());
        var message = All(service, "sdkmessage").Single();
        service.Create(new Entity("sdkmessageprocessingstep", Guid.NewGuid())
        {
            ["name"] = "unexpected dependency", ["eventhandler"] = new EntityReference("plugintype", oldType),
            ["sdkmessageid"] = new EntityReference("sdkmessage", message.Id), ["stage"] = new OptionSetValue(30),
            ["mode"] = new OptionSetValue(0), ["rank"] = 100
        });

        await Assert.That(async () => await deployment.ApplyAsync(changes)).ThrowsExactly<InvalidOperationException>();

        await Assert.That(All(service, "pluginassembly").Any(assembly => assembly.Id == oldAssembly)).IsTrue();
        await Assert.That(All(service, "plugintype").Any(type => type.Id == oldType)).IsTrue();
        await Assert.That(service.Retrieve("sdkmessageprocessingstep", stepId, new ColumnSet("eventhandler"))
            .GetAttributeValue<EntityReference>("eventhandler").Id).IsNotEqualTo(oldType);
    }

    [Test]
    public async Task Prepare_StepNotVisibleInJoinedSnapshot_RefusesCleanup()
    {
        var (service, _, oldType, _, _) = Seed();
        service.Create(new Entity("sdkmessageprocessingstep", Guid.NewGuid())
        {
            ["name"] = "unresolved message", ["eventhandler"] = new EntityReference("plugintype", oldType),
            ["sdkmessageid"] = new EntityReference("sdkmessage", Guid.NewGuid()),
            ["stage"] = new OptionSetValue(20), ["mode"] = new OptionSetValue(0), ["rank"] = 100
        });

        await Assert.That(async () => await new PluginDeployment(service).PrepareAsync(Desired())).ThrowsExactly<InvalidOperationException>();
        await Assert.That(All(service, "pluginassembly")).Count().IsEqualTo(1);
    }

    [Test]
    public async Task Apply_TupleChanged_CreatesNewStepWithoutTransferringConfiguration()
    {
        var (service, _, _, oldStep, _) = Seed();
        var desired = Desired();
        var type = desired.PluginTypes[0];
        desired = desired with { PluginTypes = [type with { Steps = [type.Steps[0] with { Stage = 40 }] }] };
        var deployment = new PluginDeployment(service);

        await deployment.ApplyAsync(await deployment.PrepareAsync(desired));

        var step = All(service, "sdkmessageprocessingstep").Single();
        await Assert.That(step.Id).IsNotEqualTo(oldStep);
        await Assert.That(step.GetAttributeValue<string>("configuration")).IsNull();
        await Assert.That(step.GetAttributeValue<EntityReference>("sdkmessageprocessingstepsecureconfigid")).IsNull();
    }

    [Test]
    public async Task Apply_OmittedCustomApiOnOldType_UnlinksBeforeCleanup()
    {
        var (service, _, typeId, _, _) = Seed();
        var apiId = service.Create(new CustomAPI(Guid.NewGuid()) { UniqueName = "my_api", PluginTypeId = new EntityReference("plugintype", typeId) });

        var deployment = new PluginDeployment(service);
        await deployment.ApplyAsync(await deployment.PrepareAsync(Desired()));

        await Assert.That(service.Retrieve(CustomAPI.EntityLogicalName, apiId, new ColumnSet(true)).ToEntity<CustomAPI>().PluginTypeId).IsNull();
        await Assert.That(All(service, PluginType.EntityLogicalName).Any(type => type.Id == typeId)).IsFalse();
    }
}
