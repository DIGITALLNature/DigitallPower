// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Security.Cryptography;
using dgt.power.dataverse;
using dgt.power.plugin.Deployment.Local;
using dgt.power.plugin.Deployment.Output;
using dgt.power.plugin.Deployment.Reconciliation;
using dgt.power.tests.FakeExecutor;
using Digitall.Dataverse.Testing;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Spectre.Console.Testing;

namespace dgt.power.plugin.tests.Deployment.Reconciliation;

public class CustomApiDeploymentTests
{
    private static FakeOrganizationServiceAsync Service()
    {
        var service = new FakeOrganizationServiceAsync();
        service.AddDefaultRequests();
        service.AddRequests(new RetrieveDependenciesForDeleteExecutor());
        return service;
    }

    private static LocalAssembly Desired(string api = "new_Api") => new()
    {
        Name = "Plugins", Version = new Version(1, 0, 0, 0), Content = "Y29udGVudA==",
        ContentHash = Convert.ToHexString(SHA256.HashData("content"u8)),
        PluginTypes = [new LocalPluginType("Plugin", "Plugin", api, true, [])]
    };

    private static CustomAPI ReadApi(FakeOrganizationServiceAsync service, Guid id) =>
        service.Retrieve(CustomAPI.EntityLogicalName, id, new ColumnSet(true)).ToEntity<CustomAPI>();

    private static IReadOnlyList<Entity> All(FakeOrganizationServiceAsync service, string name) =>
        service.RetrieveMultiple(new QueryExpression(name) { ColumnSet = new ColumnSet(true) }).Entities;

    private static void SeedAssembly(FakeOrganizationServiceAsync service)
    {
        var desired = Desired();
        service.Create(new PluginAssembly
        {
            Name = desired.Name, Version = desired.Version.ToString(), Content = desired.Content,
            SourceType = new OptionSetValue(0), IsolationMode = new OptionSetValue(2)
        });
    }

    [Test]
    public async Task PrepareAndRender_NewHandler_PerformsNoWritesAndShowsLink()
    {
        var service = Service();
        var apiId = service.Create(new CustomAPI { UniqueName = "new_Api" });
        var changes = await new PluginDeployment(service).PrepareAsync(Desired());
        using var console = new TestConsole();

        new DeploymentRenderer(console).Render(changes);

        await Assert.That(ReadApi(service, apiId).PluginTypeId).IsNull();
        await Assert.That(All(service, PluginAssembly.EntityLogicalName)).Count().IsEqualTo(0);
        await Assert.That(console.Output).Contains("new_Api");
        await Assert.That(console.Output).Contains("Link");
        var lines = console.Output.Split('\n');
        var apiIndex = Array.FindIndex(lines, line => line.Contains("Custom API new_Api"));
        await Assert.That(apiIndex).IsGreaterThan(0);
        await Assert.That(lines[apiIndex - 1]).Contains("Plugin ").And.DoesNotContain("Custom API");
        await Assert.That(lines[apiIndex]).Contains("└── Custom API new_Api").And.DoesNotContain("->");
    }

    [Test]
    public async Task Apply_DeclaredApi_ReassignsExternalHandlerAndIsIdempotent()
    {
        var service = Service();
        SeedAssembly(service);
        var apiId = service.Create(new CustomAPI
        {
            UniqueName = "new_Api", PluginTypeId = new EntityReference(PluginType.EntityLogicalName, Guid.NewGuid()),
            Description = "external API definition"
        });
        var deployment = new PluginDeployment(service);
        var changes = await deployment.PrepareAsync(Desired());
        using var console = new TestConsole();
        new DeploymentRenderer(console).Render(changes);

        await Assert.That(console.Output).Contains("Reassign from");
        await Assert.That(console.Output).Contains("Destructive operations: 1");
        await deployment.ApplyAsync(changes);

        var typeId = All(service, PluginType.EntityLogicalName).Single().Id;
        await Assert.That(ReadApi(service, apiId).PluginTypeId!.Id).IsEqualTo(typeId);
        await Assert.That(ReadApi(service, apiId).Description).IsEqualTo("external API definition");
        var repeated = await deployment.PrepareAsync(Desired());
        await Assert.That(repeated.HasChanges).IsFalse();
        using var repeatedConsole = new TestConsole();
        new DeploymentRenderer(repeatedConsole).Render(repeated);
        await Assert.That(repeatedConsole.Output).Contains("new_Api").And.DoesNotContain("Unchanged");
    }

    [Test]
    public async Task Apply_OmittedApi_UnlinksOwnedAssociationButPreservesExternalAssociation()
    {
        var service = Service();
        SeedAssembly(service);
        var apiId = service.Create(new CustomAPI { UniqueName = "new_Api" });
        var externalType = Guid.NewGuid();
        var externalApi = service.Create(new CustomAPI
        {
            UniqueName = "new_External", PluginTypeId = new EntityReference(PluginType.EntityLogicalName, externalType)
        });
        var deployment = new PluginDeployment(service);
        await deployment.ApplyAsync(await deployment.PrepareAsync(Desired()));
        var changes = await deployment.PrepareAsync(Desired(string.Empty));
        using var console = new TestConsole();
        new DeploymentRenderer(console).Render(changes);

        await Assert.That(ReadApi(service, apiId).PluginTypeId).IsNotNull();
        await Assert.That(console.Output).Contains("Unlink");
        await Assert.That(console.Output).Contains("Destructive operations: 1");
        await deployment.ApplyAsync(changes);

        await Assert.That(ReadApi(service, apiId).PluginTypeId).IsNull();
        await Assert.That(ReadApi(service, externalApi).PluginTypeId!.Id).IsEqualTo(externalType);
        await Assert.That(All(service, CustomAPI.EntityLogicalName)).Count().IsEqualTo(2);
        await Assert.That((await deployment.PrepareAsync(Desired(string.Empty))).HasChanges).IsFalse();
    }

    [Test]
    public async Task Apply_Upgrade_ReassignsBeforeDeletingOldTypeAndPreservesApiId()
    {
        var service = Service();
        SeedAssembly(service);
        var apiId = service.Create(new CustomAPI { UniqueName = "new_Api" });
        var deployment = new PluginDeployment(service);
        await deployment.ApplyAsync(await deployment.PrepareAsync(Desired()));
        var oldType = All(service, PluginType.EntityLogicalName).Single().Id;
        var upgraded = Desired() with { Version = new Version(2, 0, 0, 0) };

        await deployment.ApplyAsync(await deployment.PrepareAsync(upgraded), progress =>
        {
            if (progress.Resource == "plugin type" && progress.Operation == PluginDeploymentOperation.Deleted)
            {
                if (ReadApi(service, apiId).PluginTypeId?.Id == oldType)
                {
                    throw new InvalidOperationException("API was not reassigned before cleanup.");
                }
            }
        });

        await Assert.That(ReadApi(service, apiId).PluginTypeId!.Id).IsEqualTo(All(service, PluginType.EntityLogicalName).Single().Id);
        await Assert.That(All(service, PluginAssembly.EntityLogicalName)).Count().IsEqualTo(1);
    }

    [Test]
    public async Task Prepare_RemovedTypeWithPlatformManagedStep_LeavesStepToTheUnlink()
    {
        var service = Service();
        SeedAssembly(service);
        service.Create(new CustomAPI { UniqueName = "new_Api" });
        var deployment = new PluginDeployment(service);
        await deployment.ApplyAsync(await deployment.PrepareAsync(Desired()));
        var oldType = All(service, PluginType.EntityLogicalName).Single().Id;
        var messageId = service.Create(new Entity("sdkmessage", Guid.NewGuid()) { ["name"] = "new_Api" });
        service.Create(new Entity("sdkmessageprocessingstep", Guid.NewGuid())
        {
            ["name"] = "platform step", ["eventhandler"] = new EntityReference("plugintype", oldType),
            ["sdkmessageid"] = new EntityReference("sdkmessage", messageId), ["stage"] = new OptionSetValue(30),
            ["mode"] = new OptionSetValue(0), ["rank"] = 1
        });
        var desired = Desired();
        var renamed = desired with { Version = new Version(2, 0, 0, 0), PluginTypes = [desired.PluginTypes[0] with { TypeName = "Renamed" }] };

        var changes = await deployment.PrepareAsync(renamed);

        await Assert.That(changes.AssemblyToDelete).IsNotNull();
        await Assert.That(changes.CustomApis).Count().IsEqualTo(1);
    }

    [Test]
    [Arguments(false)]
    [Arguments(true)]
    public async Task Apply_InterruptedAssociationWrite_FreshPreparationResumesWithoutRepeatingWrite(bool unlink)
    {
        var service = Service();
        SeedAssembly(service);
        var apiId = service.Create(new CustomAPI { UniqueName = "new_Api" });
        var deployment = new PluginDeployment(service);
        await deployment.ApplyAsync(await deployment.PrepareAsync(Desired()));
        var desired = Desired(unlink ? string.Empty : "new_Api") with { Version = new Version(2, 0, 0, 0) };

        await Assert.That(async () => await deployment.ApplyAsync(await deployment.PrepareAsync(desired), progress =>
        {
            if (progress.Resource == "Custom API")
            {
                throw new InvalidOperationException("Interrupted after association write.");
            }
        })).ThrowsExactly<InvalidOperationException>();

        // The SDK fake does not extract assembly identity from uploaded bytes.
        var target = All(service, PluginAssembly.EntityLogicalName).Single(assembly => assembly.GetAttributeValue<string>("version") is null);
        service.Update(new PluginAssembly(target.Id)
        {
            Version = desired.Version.ToString(), SourceType = new OptionSetValue(0), IsolationMode = new OptionSetValue(2)
        });
        var retry = await deployment.PrepareAsync(desired);
        var writes = new List<PluginDeploymentProgress>();
        await deployment.ApplyAsync(retry, writes.Add);

        await Assert.That(writes.Any(write => write.Resource == "Custom API")).IsFalse();
        await Assert.That(ReadApi(service, apiId).PluginTypeId is null).IsEqualTo(unlink);
        await Assert.That(All(service, PluginAssembly.EntityLogicalName)).Count().IsEqualTo(1);
    }

    [Test]
    public async Task Prepare_DuplicateDeclaration_RejectsBeforeArtifactWrites()
    {
        var service = Service();
        service.Create(new CustomAPI { UniqueName = "new_Api" });
        var desired = Desired();
        desired = desired with
        {
            PluginTypes = [desired.PluginTypes[0], desired.PluginTypes[0] with { TypeName = "Other", CustomApi = "NEW_API" }]
        };

        await Assert.That(async () => await new PluginDeployment(service).PrepareAsync(desired)).ThrowsExactly<InvalidOperationException>();
        await Assert.That(All(service, PluginAssembly.EntityLogicalName)).Count().IsEqualTo(0);
    }

    [Test]
    public async Task Apply_RemovedType_UnlinksApiWithoutDeletingItsRecord()
    {
        var service = Service();
        SeedAssembly(service);
        var apiId = service.Create(new CustomAPI { UniqueName = "new_Api" });
        var deployment = new PluginDeployment(service);
        await deployment.ApplyAsync(await deployment.PrepareAsync(Desired()));
        var removal = await deployment.PrepareAsync(Desired() with { PluginTypes = [], Version = new Version(2, 0, 0, 0) });
        using var console = new TestConsole();
        console.Profile.Width = 160;
        new DeploymentRenderer(console).Render(removal);

        await deployment.ApplyAsync(removal);

        await Assert.That(console.Output).Contains("└── Custom API");
        await Assert.That(console.Output).DoesNotContain("\nCustom API");
        await Assert.That(ReadApi(service, apiId).PluginTypeId).IsNull();
    }

    [Test]
    public async Task Apply_CancelledAfterFirstAssociation_DoesNotWriteSecondAssociation()
    {
        var service = Service();
        service.Create(new CustomAPI { UniqueName = "new_Api" });
        var secondApi = service.Create(new CustomAPI { UniqueName = "new_Second" });
        var desired = Desired();
        desired = desired with { PluginTypes = [desired.PluginTypes[0], desired.PluginTypes[0] with { TypeName = "Other", CustomApi = "new_Second" }] };
        var deployment = new PluginDeployment(service);
        using var cancellation = new CancellationTokenSource();

        await Assert.That(async () => await deployment.ApplyAsync(await deployment.PrepareAsync(desired), progress =>
        {
            if (progress.Resource == "Custom API")
            {
                cancellation.Cancel();
            }
        }, cancellation.Token)).Throws<OperationCanceledException>();

        await Assert.That(ReadApi(service, secondApi).PluginTypeId).IsNull();
    }
}
