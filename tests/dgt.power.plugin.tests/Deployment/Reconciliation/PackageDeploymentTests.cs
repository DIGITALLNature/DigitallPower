// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Security.Cryptography;
using System.Text;
using dgt.power.dataverse;
using dgt.power.plugin.Deployment.Execution;
using dgt.power.plugin.Deployment.Local;
using dgt.power.plugin.Deployment.Output;
using dgt.power.plugin.Deployment.Planning.Plans;
using dgt.power.plugin.Deployment.Reconciliation;
using dgt.power.plugin.Exceptions;
using dgt.power.plugin.tests.Repositories;
using dgt.power.tests.FakeExecutor;
using Digitall.Dataverse.Testing;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Spectre.Console.Testing;

namespace dgt.power.plugin.tests.Deployment.Reconciliation;

public class PackageDeploymentTests
{
    private static readonly byte[] s_initialPackage = "initial-package"u8.ToArray();

    private static LocalPluginType CreateLocalType(string typeName, params LocalPluginStep[] steps) =>
        new(typeName, typeName, string.Empty, true, steps);

    private static LocalAssembly Assembly(string name, params LocalPluginType[] types) => new()
    {
        Name = name,
        Version = new Version(1, 0, 0, 0),
        Content = Convert.ToBase64String(Encoding.UTF8.GetBytes($"assembly-{name}")),
        ContentHash = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes($"assembly-{name}"))),
        PluginTypes = types
    };

    private static LocalPluginStep Step(string name) => new(name, 0, "Update", 20, "none", "none", null, 100, []);

    private static LocalPluginPackage Package(string content, params LocalAssembly[] assemblies)
    {
        var bytes = Convert.FromBase64String(content);
        return new LocalPluginPackage(
            new LocalPackage("Contoso.Plugins", "1.0.0", content, Convert.ToHexString(SHA256.HashData(bytes))),
            assemblies);
    }

    private static FakeOrganizationServiceAsync CreateStore()
    {
        var service = new FakeOrganizationServiceAsync();
        service.AddDefaultRequests();
        service.AddRequests(new RetrieveDependenciesForDeleteExecutor());
        service.Create(new SdkMessage(Guid.NewGuid()) { Name = "Update" });
        return service;
    }

    private static IReadOnlyList<Entity> All(FakeOrganizationServiceAsync service, string entityName) =>
        service.RetrieveMultiple(new QueryExpression(entityName) { ColumnSet = new ColumnSet(true) }).Entities;

    [Test]
    [Arguments(false)]
    [Arguments(true)]
    public async Task Apply_ExistingPackageAndAssemblyBindings_PreservesBothWithoutProvisioning(bool updateContent)
    {
        var desired = Package(Convert.ToBase64String(s_initialPackage), Assembly("First", CreateLocalType("First.Plugin")));
        var store = CreateStore();
        var service = new PackageUploadService(store, desired);
        var packageId = service.SeedPackage("new_Contoso.Plugins", desired, updateContent ? "older-package"u8.ToArray() : s_initialPackage);
        service.MaterializeAssemblies(packageId, desired);
        var assemblyId = All(store, PluginAssembly.EntityLogicalName).Single().Id;
        var packageIdentity = store.Create(new ManagedIdentity { ApplicationId = Guid.NewGuid() });
        var assemblyIdentity = store.Create(new ManagedIdentity { ApplicationId = Guid.NewGuid() });
        store.Update(new PluginPackage(packageId) { Managedidentityid = new EntityReference(ManagedIdentity.EntityLogicalName, packageIdentity) });
        store.Update(new PluginAssembly(assemblyId) { ManagedIdentityId = new EntityReference(ManagedIdentity.EntityLogicalName, assemblyIdentity) });
        var deployment = new PluginDeployment(service);

        var changes = await deployment.PreparePackageAsync(desired, "new");
        await Assert.That(changes.HasChanges).IsEqualTo(updateContent);
        await deployment.ApplyPackageAsync(changes);

        await Assert.That(store.Retrieve(PluginPackage.EntityLogicalName, packageId, new ColumnSet(true)).ToEntity<PluginPackage>().Managedidentityid!.Id).IsEqualTo(packageIdentity);
        await Assert.That(store.Retrieve(PluginAssembly.EntityLogicalName, assemblyId, new ColumnSet(true)).ToEntity<PluginAssembly>().ManagedIdentityId!.Id).IsEqualTo(assemblyIdentity);
        await Assert.That(All(store, ManagedIdentity.EntityLogicalName)).Count().IsEqualTo(2);
        await Assert.That(service.UploadCount).IsEqualTo(updateContent ? 1 : 0);
        await Assert.That((await deployment.PreparePackageAsync(desired, "new")).HasChanges).IsFalse();
    }

    [Test]
    public async Task Apply_CustomApisWithSameClrTypeAcrossAssemblies_UsesScopedHandlers()
    {
        const string firstApi = "new_First";
        const string secondApi = "new_Second";
        var desired = Package(Convert.ToBase64String(s_initialPackage),
            Assembly("First", CreateLocalType("Shared.Plugin") with { CustomApi = firstApi }),
            Assembly("Second", CreateLocalType("Shared.Plugin") with { CustomApi = secondApi }));
        var store = CreateStore();
        var firstId = store.Create(new CustomAPI { UniqueName = firstApi });
        var secondId = store.Create(new CustomAPI { UniqueName = secondApi });
        var deployment = new PluginDeployment(new PackageUploadService(store, desired));
        var changes = await deployment.PreparePackageAsync(desired, "new");
        using var console = new TestConsole();
        new DeploymentRenderer(console).RenderPackage(changes);

        await Assert.That(console.Output).Contains(firstApi);
        await Assert.That(console.Output).Contains(secondApi);
        await Assert.That(All(store, PluginPackage.EntityLogicalName)).Count().IsEqualTo(0);
        await deployment.ApplyPackageAsync(changes);

        var types = All(store, PluginType.EntityLogicalName);
        var assemblies = All(store, PluginAssembly.EntityLogicalName);
        foreach (var (name, apiId) in new[] { ("First", firstId), ("Second", secondId) })
        {
            var assemblyId = assemblies.Single(assembly => assembly.GetAttributeValue<string>("name") == name).Id;
            var typeId = types.Single(type => type.GetAttributeValue<EntityReference>("pluginassemblyid").Id == assemblyId).Id;
            await Assert.That(store.Retrieve(CustomAPI.EntityLogicalName, apiId, new ColumnSet(true)).ToEntity<CustomAPI>().PluginTypeId!.Id).IsEqualTo(typeId);
        }

        await Assert.That((await deployment.PreparePackageAsync(desired, "new")).HasChanges).IsFalse();
    }

    [Test]
    public async Task Apply_ApiMovesBetweenExistingPackageTypes_ReassignsWithoutUnlinkingItAgain()
    {
        var first = CreateLocalType("Shared.Plugin") with { CustomApi = "new_Api" };
        var second = CreateLocalType("Shared.Plugin");
        var original = Package(Convert.ToBase64String(s_initialPackage), Assembly("First", first), Assembly("Second", second));
        var store = CreateStore();
        var apiId = store.Create(new CustomAPI { UniqueName = "new_Api" });
        var omittedId = store.Create(new CustomAPI { UniqueName = "new_Omitted" });
        var service = new PackageUploadService(store, original);
        var deployment = new PluginDeployment(service);
        await deployment.ApplyPackageAsync(await deployment.PreparePackageAsync(original, "new"));
        var oldHandler = store.Retrieve(CustomAPI.EntityLogicalName, apiId, new ColumnSet(true)).ToEntity<CustomAPI>().PluginTypeId!;
        store.Update(new CustomAPI(omittedId) { PluginTypeId = oldHandler });
        var desired = original with
        {
            Assemblies = [Assembly("First", first with { CustomApi = string.Empty }), Assembly("Second", second with { CustomApi = "new_Api" })]
        };

        await deployment.ApplyPackageAsync(await deployment.PreparePackageAsync(desired, "new"));

        var newHandler = store.Retrieve(CustomAPI.EntityLogicalName, apiId, new ColumnSet(true)).ToEntity<CustomAPI>().PluginTypeId;
        await Assert.That(newHandler).IsNotNull();
        await Assert.That(newHandler!.Id).IsNotEqualTo(oldHandler.Id);
        await Assert.That(store.Retrieve(CustomAPI.EntityLogicalName, omittedId, new ColumnSet(true)).ToEntity<CustomAPI>().PluginTypeId).IsNull();
        await Assert.That(service.UploadCount).IsEqualTo(1);
        await Assert.That((await deployment.PreparePackageAsync(desired, "new")).HasChanges).IsFalse();
    }

    [Test]
    public async Task Prepare_DuplicateApiAcrossAssemblies_RejectsBeforeUpload()
    {
        var desired = Package(Convert.ToBase64String(s_initialPackage),
            Assembly("First", CreateLocalType("First.Plugin") with { CustomApi = "new_Api" }),
            Assembly("Second", CreateLocalType("Second.Plugin") with { CustomApi = "NEW_API" }));
        var store = CreateStore();
        store.Create(new CustomAPI { UniqueName = "new_Api" });
        var service = new PackageUploadService(store, desired);

        await Assert.That(async () => await new PluginDeployment(service).PreparePackageAsync(desired, "new")).ThrowsExactly<InvalidOperationException>();
        await Assert.That(service.UploadCount).IsEqualTo(0);
    }

    [Test]
    public async Task Prepare_MissingApiInLaterAssembly_RejectsBeforeAnyUpload()
    {
        var desired = Package(Convert.ToBase64String(s_initialPackage),
            Assembly("First", CreateLocalType("First.Plugin") with { CustomApi = "new_Api" }),
            Assembly("Second", CreateLocalType("Second.Plugin") with { CustomApi = "new_Missing" }));
        var store = CreateStore();
        var apiId = store.Create(new CustomAPI { UniqueName = "new_Api" });
        var service = new PackageUploadService(store, desired);

        await Assert.That(async () => await new PluginDeployment(service).PreparePackageAsync(desired, "new")).ThrowsExactly<MissingCustomApiException>();
        await Assert.That(service.UploadCount).IsEqualTo(0);
        await Assert.That(store.Retrieve(CustomAPI.EntityLogicalName, apiId, new ColumnSet(true)).ToEntity<CustomAPI>().PluginTypeId).IsNull();
    }

    [Test]
    public async Task Apply_InterruptedApiLink_RetryReusesPackageAndAssociation()
    {
        var desired = Package(Convert.ToBase64String(s_initialPackage),
            Assembly("First", CreateLocalType("First.Plugin") with { CustomApi = "new_Api" }));
        var store = CreateStore();
        var apiId = store.Create(new CustomAPI { UniqueName = "new_Api" });
        var service = new PackageUploadService(store, desired);
        var deployment = new PluginDeployment(service);

        await Assert.That(async () => await deployment.ApplyPackageAsync(await deployment.PreparePackageAsync(desired, "new"), progress =>
        {
            if (progress.Resource == "Custom API")
            {
                throw new InvalidOperationException("Interrupted after linking.");
            }
        })).ThrowsExactly<InvalidOperationException>();
        var retry = await deployment.PreparePackageAsync(desired, "new");
        var writes = new List<PluginDeploymentProgress>();
        await deployment.ApplyPackageAsync(retry, writes.Add);

        await Assert.That(retry.HasChanges).IsFalse();
        await Assert.That(writes).IsEmpty();
        await Assert.That(service.UploadCount).IsEqualTo(1);
        await Assert.That(store.Retrieve(CustomAPI.EntityLogicalName, apiId, new ColumnSet(true)).ToEntity<CustomAPI>().PluginTypeId).IsNotNull();
    }

    [Test]
    public async Task PrepareAndApply_NewPackage_ResolvesServerOwnedAssembliesAndTypes()
    {
        var store = CreateStore();
        var firstType = CreateLocalType("Shared.Plugin", Step("first"));
        var secondType = CreateLocalType("Shared.Plugin", Step("second"));
        var desired = Package(Convert.ToBase64String(s_initialPackage),
            Assembly("First", firstType), Assembly("Second", secondType));
        var service = new PackageUploadService(store, desired);
        var deployment = new PluginDeployment(service);

        var changes = await deployment.PreparePackageAsync(desired, "new");
        await Assert.That(changes.HasChanges).IsTrue();
        await Assert.That(All(store, PluginPackage.EntityLogicalName)).Count().IsEqualTo(0);
        await Assert.That(All(store, PluginAssembly.EntityLogicalName)).Count().IsEqualTo(0);

        var packageId = await deployment.ApplyPackageAsync(changes);
        var assemblies = All(store, PluginAssembly.EntityLogicalName);
        var types = All(store, PluginType.EntityLogicalName);
        var steps = All(store, SdkMessageProcessingStep.EntityLogicalName);

        await Assert.That(All(store, PluginPackage.EntityLogicalName)).Count().IsEqualTo(1);
        await Assert.That(assemblies).Count().IsEqualTo(2);
        await Assert.That(assemblies.All(assembly =>
            assembly.GetAttributeValue<EntityReference>(PluginAssembly.LogicalNames.PackageId).Id == packageId)).IsTrue();
        await Assert.That(types).Count().IsEqualTo(2);
        await Assert.That(types.Select(type => type.GetAttributeValue<EntityReference>(PluginType.LogicalNames.PluginAssemblyId).Id).Distinct()).Count().IsEqualTo(2);
        await Assert.That(steps).Count().IsEqualTo(2);
        await Assert.That(steps.Select(step => step.GetAttributeValue<EntityReference>(SdkMessageProcessingStep.LogicalNames.EventHandler).Id).Distinct()).Count().IsEqualTo(2);
    }

    [Test]
    public async Task RenderPackage_NewPackage_ShowsServerOwnedResourcesWithoutWrites()
    {
        var image = new LocalPluginStepImage(0, "Pre[Image]", "PreImage", "Target", ["name"]);
        var desired = Package(Convert.ToBase64String(s_initialPackage),
            Assembly("First", CreateLocalType("Shared.Plugin", Step("first[step]") with { Images = [image] })),
            Assembly("Second", CreateLocalType("Shared.Plugin", Step("second"))));
        var store = CreateStore();
        var deployment = new PluginDeployment(new PackageUploadService(store, desired));
        var changes = await deployment.PreparePackageAsync(desired, "new");
        using var console = new TestConsole();
        console.Profile.Width = 160;

        new DeploymentRenderer(console).RenderPackage(changes);

        await Assert.That(console.Output).Contains("new_Contoso.Plugins Create");
        await Assert.That(console.Output).Contains("First v1.0.0.0 Create via package upload");
        await Assert.That(console.Output).Contains("Second v1.0.0.0 Create via package upload");
        await Assert.That(console.Output).Contains("Shared.Plugin Create via package upload");
        await Assert.That(console.Output).Contains("first[step] Create");
        await Assert.That(console.Output).Contains("Pre[Image] Create");
        await Assert.That(console.Output).Contains("IDs are validated before registration writes");
        await Assert.That(All(store, PluginPackage.EntityLogicalName)).Count().IsEqualTo(0);
        await Assert.That(All(store, PluginAssembly.EntityLogicalName)).Count().IsEqualTo(0);
        await Assert.That(All(store, PluginType.EntityLogicalName)).Count().IsEqualTo(0);
        await Assert.That(All(store, SdkMessageProcessingStep.EntityLogicalName)).Count().IsEqualTo(0);
        await Assert.That(All(store, SdkMessageProcessingStepImage.EntityLogicalName)).Count().IsEqualTo(0);
    }

    [Test]
    public async Task PrepareAndApply_IdenticalPackage_IsUnchanged()
    {
        var desired = Package(Convert.ToBase64String(s_initialPackage), Assembly("First", CreateLocalType("First.Plugin")));
        var store = CreateStore();
        var service = new PackageUploadService(store, desired);
        var packageId = service.SeedPackage("new_Contoso.Plugins", desired, s_initialPackage);
        service.MaterializeAssemblies(packageId, desired);
        var deployment = new PluginDeployment(service);

        var changes = await deployment.PreparePackageAsync(desired, "new");
        using var console = new TestConsole();
        console.Profile.Width = 160;
        new DeploymentRenderer(console).RenderPackage(changes);
        var progress = new List<PluginDeploymentProgress>();
        await deployment.ApplyPackageAsync(changes, progress.Add);

        await Assert.That(changes.HasChanges).IsFalse();
        await Assert.That(progress).Count().IsEqualTo(0);
        await Assert.That(All(store, PluginPackage.EntityLogicalName)).Count().IsEqualTo(1);
        await Assert.That(All(store, PluginAssembly.EntityLogicalName)).Count().IsEqualTo(1);
        await Assert.That(console.Output).Contains("new_Contoso.Plugins Unchanged");
        await Assert.That(console.Output).Contains("First v1.0.0.0 Unchanged");
        await Assert.That(console.Output).DoesNotContain("package upload");
    }

    [Test]
    public async Task Apply_UpdatedPackage_AddsRegistrationWithoutReplacingExistingType()
    {
        var oldDesired = Package(Convert.ToBase64String(s_initialPackage), Assembly("First", CreateLocalType("First.Plugin")));
        var newBytes = "updated-package"u8.ToArray();
        var desired = Package(Convert.ToBase64String(newBytes), Assembly("First", CreateLocalType("First.Plugin", Step("new step"))));
        var store = CreateStore();
        var service = new PackageUploadService(store, desired);
        var packageId = service.SeedPackage("new_Contoso.Plugins", oldDesired, s_initialPackage);
        service.MaterializeAssemblies(packageId, oldDesired);
        var originalAssembly = All(store, PluginAssembly.EntityLogicalName).Single();
        var originalType = All(store, PluginType.EntityLogicalName).Single();
        var deployment = new PluginDeployment(service);

        var changes = await deployment.PreparePackageAsync(desired, "new");
        await Assert.That(changes.UpdateContent).IsTrue();
        await deployment.ApplyPackageAsync(changes);

        await Assert.That(All(store, PluginPackage.EntityLogicalName).Single().Id).IsEqualTo(packageId);
        await Assert.That(All(store, PluginAssembly.EntityLogicalName).Single().Id).IsEqualTo(originalAssembly.Id);
        await Assert.That(All(store, PluginType.EntityLogicalName).Single().Id).IsEqualTo(originalType.Id);
        await Assert.That(All(store, SdkMessageProcessingStep.EntityLogicalName)).Count().IsEqualTo(1);
        await Assert.That(service.PackageContent(packageId)).IsEquivalentTo(newBytes);
    }

    [Test]
    public async Task Apply_UpdatedPackage_ReconcilesStepAndImageWhilePreservingEnvironmentState()
    {
        var oldImage = new LocalPluginStepImage(0, "PreImage", "PreImage", "Target", ["name"]);
        var oldStep = Step("old name") with { Images = [oldImage] };
        var oldDesired = Package(Convert.ToBase64String(s_initialPackage),
            Assembly("First", CreateLocalType("First.Plugin", oldStep)));
        var newBytes = "updated-package-with-registration-changes"u8.ToArray();
        var updatedStep = oldStep with
        {
            Name = "new name",
            ExecutionOrder = 200,
            FilterAttributes = ["telephone1"],
            Images = [oldImage with { Attributes = ["telephone1"] }]
        };
        var desired = Package(Convert.ToBase64String(newBytes),
            Assembly("First", CreateLocalType("First.Plugin", updatedStep)));
        var store = CreateStore();
        var service = new PackageUploadService(store, oldDesired);
        var packageId = service.SeedPackage("new_Contoso.Plugins", oldDesired, s_initialPackage);
        service.MaterializeAssemblies(packageId, oldDesired);
        var deployment = new PluginDeployment(service);

        await deployment.ApplyPackageAsync(await deployment.PreparePackageAsync(oldDesired, "new"));
        var originalStep = All(store, SdkMessageProcessingStep.EntityLogicalName).Single();
        var originalImage = All(store, SdkMessageProcessingStepImage.EntityLogicalName).Single();
        var secureConfigId = Guid.NewGuid();
        var impersonatingUserId = Guid.NewGuid();
        store.Update(new Entity(SdkMessageProcessingStep.EntityLogicalName, originalStep.Id)
        {
            ["configuration"] = "environment configuration",
            ["sdkmessageprocessingstepsecureconfigid"] = new EntityReference("sdkmessageprocessingstepsecureconfig", secureConfigId),
            ["statecode"] = new OptionSetValue(1),
            ["impersonatinguserid"] = new EntityReference("systemuser", impersonatingUserId)
        });

        var preparedUpdate = await deployment.PreparePackageAsync(desired, "new");
        using var console = new TestConsole();
        console.Profile.Width = 160;
        new DeploymentRenderer(console).RenderPackage(preparedUpdate);
        await Assert.That(console.Output).Contains("new_Contoso.Plugins Update");
        await Assert.That(console.Output).Contains("First v1.0.0.0 Managed by package upload");
        await Assert.That(console.Output).Contains("First.Plugin Unchanged");
        await Assert.That(console.Output).Contains("new name Update");
        await Assert.That(console.Output).Contains("PreImage Update");
        await deployment.ApplyPackageAsync(preparedUpdate);

        var stepAfterUpdate = All(store, SdkMessageProcessingStep.EntityLogicalName).Single();
        var imageAfterUpdate = All(store, SdkMessageProcessingStepImage.EntityLogicalName).Single();
        await Assert.That(stepAfterUpdate.Id).IsEqualTo(originalStep.Id);
        await Assert.That(stepAfterUpdate.GetAttributeValue<string>("name")).IsEqualTo("new name");
        await Assert.That(stepAfterUpdate.GetAttributeValue<int>("rank")).IsEqualTo(200);
        await Assert.That(stepAfterUpdate.GetAttributeValue<string>("filteringattributes")).IsEqualTo("telephone1");
        await Assert.That(stepAfterUpdate.GetAttributeValue<string>("configuration")).IsEqualTo("environment configuration");
        await Assert.That(stepAfterUpdate.GetAttributeValue<EntityReference>("sdkmessageprocessingstepsecureconfigid").Id).IsEqualTo(secureConfigId);
        await Assert.That(stepAfterUpdate.GetAttributeValue<OptionSetValue>("statecode").Value).IsEqualTo(1);
        await Assert.That(stepAfterUpdate.GetAttributeValue<EntityReference>("impersonatinguserid").Id).IsEqualTo(impersonatingUserId);
        await Assert.That(imageAfterUpdate.Id).IsEqualTo(originalImage.Id);
        await Assert.That(imageAfterUpdate.GetAttributeValue<string>("attributes")).IsEqualTo("telephone1");
    }

    [Test]
    public async Task Apply_WhenServerDoesNotCreatePackageAssemblies_FailsWithoutStandaloneFallback()
    {
        var desired = Package(Convert.ToBase64String(s_initialPackage), Assembly("First", CreateLocalType("First.Plugin")));
        var store = CreateStore();
        var service = new PackageUploadService(store, desired, materializeOnUpload: false);
        var deployment = new PluginDeployment(service);
        var changes = await deployment.PreparePackageAsync(desired, "new");

        await Assert.That(async () => await deployment.ApplyPackageAsync(changes)).ThrowsExactly<InvalidOperationException>();

        await Assert.That(All(store, PluginPackage.EntityLogicalName)).Count().IsEqualTo(1);
        await Assert.That(All(store, PluginAssembly.EntityLogicalName)).Count().IsEqualTo(0);
        await Assert.That(All(store, PluginType.EntityLogicalName)).Count().IsEqualTo(0);
    }

    [Test]
    public async Task Prepare_ExistingPackageAtDifferentImmutableVersion_FailsBeforeWrites()
    {
        var desired = Package(Convert.ToBase64String(s_initialPackage), Assembly("First", CreateLocalType("First.Plugin")));
        var store = CreateStore();
        var service = new PackageUploadService(store, desired);
        service.SeedPackage("new_Contoso.Plugins", desired with
        {
            Package = desired.Package with { Version = "2.0.0" }
        }, s_initialPackage);
        var deployment = new PluginDeployment(service);

        await Assert.That(async () => await deployment.PreparePackageAsync(desired, "new")).ThrowsExactly<InvalidOperationException>();

        await Assert.That(All(store, PluginPackage.EntityLogicalName)).Count().IsEqualTo(1);
        await Assert.That(All(store, PluginAssembly.EntityLogicalName)).Count().IsEqualTo(0);
    }

    [Test]
    public async Task Prepare_PackageAssemblyRemoval_FailsBeforeUpload()
    {
        var oldDesired = Package(Convert.ToBase64String(s_initialPackage),
            Assembly("First", CreateLocalType("First.Plugin")), Assembly("Second", CreateLocalType("Second.Plugin")));
        var desired = Package(Convert.ToBase64String(s_initialPackage), Assembly("First", CreateLocalType("First.Plugin")));
        var store = CreateStore();
        var service = new PackageUploadService(store, desired);
        var packageId = service.SeedPackage("new_Contoso.Plugins", oldDesired, s_initialPackage);
        service.MaterializeAssemblies(packageId, oldDesired);
        var deployment = new PluginDeployment(service);

        await Assert.That(async () => await deployment.PreparePackageAsync(desired, "new")).ThrowsExactly<NotSupportedException>();

        await Assert.That(All(store, PluginAssembly.EntityLogicalName)).Count().IsEqualTo(2);
    }

    [Test]
    public async Task Prepare_PackageStepRemoval_FailsBeforeUpload()
    {
        var desired = Package(Convert.ToBase64String(s_initialPackage), Assembly("First", CreateLocalType("First.Plugin", Step("registered"))));
        var store = CreateStore();
        var service = new PackageUploadService(store, desired);
        var deployment = new PluginDeployment(service);
        await deployment.ApplyPackageAsync(await deployment.PreparePackageAsync(desired, "new"));
        var removal = desired with { Assemblies = [Assembly("First", CreateLocalType("First.Plugin"))] };

        await Assert.That(async () => await deployment.PreparePackageAsync(removal, "new")).ThrowsExactly<NotSupportedException>();

        await Assert.That(All(store, SdkMessageProcessingStep.EntityLogicalName)).Count().IsEqualTo(1);
    }

    [Test]
    public async Task Apply_UnexpectedServerPluginType_FailsBeforeRegistrationWrites()
    {
        var desired = Package(Convert.ToBase64String(s_initialPackage), Assembly("First", CreateLocalType("First.Plugin", Step("new"))));
        var serverPackage = Package(Convert.ToBase64String(s_initialPackage),
            Assembly("First", CreateLocalType("First.Plugin", Step("new")), CreateLocalType("Unexpected.Plugin")));
        var store = CreateStore();
        var service = new PackageUploadService(store, desired, uploadedPackage: serverPackage);
        var deployment = new PluginDeployment(service);
        var changes = await deployment.PreparePackageAsync(desired, "new");

        await Assert.That(async () => await deployment.ApplyPackageAsync(changes)).ThrowsExactly<InvalidOperationException>();

        await Assert.That(All(store, PluginPackage.EntityLogicalName)).Count().IsEqualTo(1);
        await Assert.That(All(store, PluginType.EntityLogicalName)).Count().IsEqualTo(2);
        await Assert.That(All(store, SdkMessageProcessingStep.EntityLogicalName)).Count().IsEqualTo(0);
    }

    [Test]
    public async Task Apply_AdditiveUpload_UsesServerTypesAndRetainsExistingRegistrations()
    {
        var original = Package(Convert.ToBase64String(s_initialPackage),
            Assembly("First", CreateLocalType("Shared.Plugin", Step("first"))));
        var desired = Package(Convert.ToBase64String("additive-package"u8),
            Assembly("First", CreateLocalType("Shared.Plugin", Step("first")), CreateLocalType("Added.Plugin", Step("added"))),
            Assembly("Second", CreateLocalType("Shared.Plugin", Step("second"))));
        var store = CreateStore();
        var service = new PackageUploadService(store, desired);
        var packageId = service.SeedPackage("new_Contoso.Plugins", original, s_initialPackage);
        service.MaterializeAssemblies(packageId, original);
        var deployment = new PluginDeployment(service);
        await deployment.ApplyPackageAsync(await deployment.PreparePackageAsync(original, "new"));
        var originalStepId = All(store, SdkMessageProcessingStep.EntityLogicalName).Single().Id;

        var changes = await deployment.PreparePackageAsync(desired, "new");
        await deployment.ApplyPackageAsync(changes);

        await Assert.That(All(store, PluginAssembly.EntityLogicalName)).Count().IsEqualTo(2);
        await Assert.That(All(store, PluginType.EntityLogicalName)).Count().IsEqualTo(3);
        var steps = All(store, SdkMessageProcessingStep.EntityLogicalName);
        await Assert.That(steps).Count().IsEqualTo(3);
        await Assert.That(steps.Single(step => step.GetAttributeValue<string>("name") == "first").Id).IsEqualTo(originalStepId);
        await Assert.That(service.UploadCount).IsEqualTo(1);
        await Assert.That((await deployment.PreparePackageAsync(desired, "new")).HasChanges).IsFalse();
    }

    [Test]
    [Arguments("package", false)]
    [Arguments("step", false)]
    [Arguments("step image", false)]
    [Arguments("package", true)]
    [Arguments("step", true)]
    [Arguments("step image", true)]
    public async Task Apply_InterruptedAfterWrite_FreshPreparationResumesWithoutDuplicateRegistrations(string resource, bool existingPackage)
    {
        var image = new LocalPluginStepImage(0, "PreImage", "PreImage", "Target", ["name"]);
        var desired = Package(Convert.ToBase64String(s_initialPackage),
            Assembly("First", CreateLocalType("Shared.Plugin", Step("first") with { Images = [image] })),
            Assembly("Second", CreateLocalType("Shared.Plugin", Step("second") with { Images = [image] })));
        var store = CreateStore();
        var service = new PackageUploadService(store, desired);
        if (existingPackage)
        {
            var id = service.SeedPackage("new_Contoso.Plugins", desired, "older-package"u8.ToArray());
            service.MaterializeAssemblies(id, desired);
        }

        var deployment = new PluginDeployment(service);
        var changes = await deployment.PreparePackageAsync(desired, "new");

        await Assert.That(async () => await deployment.ApplyPackageAsync(changes, progress =>
        {
            if (progress.Resource == resource)
            {
                throw new InvalidOperationException("Simulated interruption after a successful write.");
            }
        })).ThrowsExactly<InvalidOperationException>();
        var completedStepIds = All(store, SdkMessageProcessingStep.EntityLogicalName).Select(step => step.Id).ToList();
        var completedImageIds = All(store, SdkMessageProcessingStepImage.EntityLogicalName).Select(imageRecord => imageRecord.Id).ToList();

        var retry = await deployment.PreparePackageAsync(desired, "new");
        await Assert.That(retry.UpdateContent).IsFalse();
        await deployment.ApplyPackageAsync(retry);

        var steps = All(store, SdkMessageProcessingStep.EntityLogicalName);
        var images = All(store, SdkMessageProcessingStepImage.EntityLogicalName);
        await Assert.That(All(store, PluginPackage.EntityLogicalName)).Count().IsEqualTo(1);
        await Assert.That(steps).Count().IsEqualTo(2);
        await Assert.That(images).Count().IsEqualTo(2);
        await Assert.That(completedStepIds.All(id => steps.Any(step => step.Id == id))).IsTrue();
        await Assert.That(completedImageIds.All(id => images.Any(imageRecord => imageRecord.Id == id))).IsTrue();
        await Assert.That(service.UploadCount).IsEqualTo(1);
        await Assert.That((await deployment.PreparePackageAsync(desired, "new")).HasChanges).IsFalse();
    }

    [Test]
    public async Task Apply_CancelledAfterStepWrite_StopsBeforeImagesAndCanResume()
    {
        var image = new LocalPluginStepImage(0, "PreImage", "PreImage", "Target", ["name"]);
        var desired = Package(Convert.ToBase64String(s_initialPackage),
            Assembly("First", CreateLocalType("First.Plugin", Step("first") with { Images = [image] })));
        var store = CreateStore();
        var service = new PackageUploadService(store, desired);
        var deployment = new PluginDeployment(service);
        var changes = await deployment.PreparePackageAsync(desired, "new");
        using var cancellation = new CancellationTokenSource();

        await Assert.That(async () => await deployment.ApplyPackageAsync(changes, progress =>
        {
            if (progress.Resource == "step")
            {
                cancellation.Cancel();
            }
        }, cancellation.Token)).Throws<OperationCanceledException>();

        var stepId = All(store, SdkMessageProcessingStep.EntityLogicalName).Single().Id;
        await Assert.That(All(store, SdkMessageProcessingStepImage.EntityLogicalName)).Count().IsEqualTo(0);
        await deployment.ApplyPackageAsync(await deployment.PreparePackageAsync(desired, "new"));
        await Assert.That(All(store, SdkMessageProcessingStep.EntityLogicalName).Single().Id).IsEqualTo(stepId);
        await Assert.That(All(store, SdkMessageProcessingStepImage.EntityLogicalName)).Count().IsEqualTo(1);
        await Assert.That(service.UploadCount).IsEqualTo(1);
    }

    [Test]
    public async Task Apply_UnexpectedServerAssemblyVersion_FailsBeforeRegistrationWrites()
    {
        var assembly = Assembly("First", CreateLocalType("First.Plugin", Step("first")));
        var desired = Package(Convert.ToBase64String(s_initialPackage), assembly);
        var uploaded = desired with { Assemblies = [assembly with { Version = new Version(9, 0, 0, 0) }] };
        var store = CreateStore();
        var service = new PackageUploadService(store, desired, uploadedPackage: uploaded);
        var deployment = new PluginDeployment(service);
        var changes = await deployment.PreparePackageAsync(desired, "new");

        await Assert.That(async () => await deployment.ApplyPackageAsync(changes)).ThrowsExactly<InvalidOperationException>();

        await Assert.That(All(store, SdkMessageProcessingStep.EntityLogicalName)).Count().IsEqualTo(0);
    }

    [Test]
    [Arguments(false)]
    [Arguments(true)]
    public async Task Apply_UploadFailsBeforeWrite_FreshPreparationCanRetry(bool existingPackage)
    {
        var desired = Package(Convert.ToBase64String(s_initialPackage),
            Assembly("First", CreateLocalType("First.Plugin", Step("first"))));
        var store = CreateStore();
        var service = new PackageUploadService(store, desired) { FailUpload = true };
        if (existingPackage)
        {
            var id = service.SeedPackage("new_Contoso.Plugins", desired, "older-package"u8.ToArray());
            service.MaterializeAssemblies(id, desired);
        }

        var deployment = new PluginDeployment(service);
        var changes = await deployment.PreparePackageAsync(desired, "new");
        await Assert.That(async () => await deployment.ApplyPackageAsync(changes)).ThrowsExactly<InvalidOperationException>();
        await Assert.That(service.UploadCount).IsEqualTo(0);
        await Assert.That(All(store, SdkMessageProcessingStep.EntityLogicalName)).Count().IsEqualTo(0);

        service.FailUpload = false;
        await deployment.ApplyPackageAsync(await deployment.PreparePackageAsync(desired, "new"));

        await Assert.That(All(store, PluginPackage.EntityLogicalName)).Count().IsEqualTo(1);
        await Assert.That(All(store, SdkMessageProcessingStep.EntityLogicalName)).Count().IsEqualTo(1);
        await Assert.That(service.UploadCount).IsEqualTo(1);
    }

    [Test]
    public async Task Apply_PackageContentChangedAfterPreparation_RejectsBeforeUpload()
    {
        var desired = Package(Convert.ToBase64String(s_initialPackage),
            Assembly("First", CreateLocalType("First.Plugin", Step("first"))));
        var store = CreateStore();
        var service = new PackageUploadService(store, desired);
        var packageId = service.SeedPackage("new_Contoso.Plugins", desired, "older-package"u8.ToArray());
        service.MaterializeAssemblies(packageId, desired);
        var deployment = new PluginDeployment(service);
        var changes = await deployment.PreparePackageAsync(desired, "new");
        await service.UpdateAsync(new PluginPackage(packageId) { Content = Convert.ToBase64String("concurrent-package"u8) }, CancellationToken.None);

        await Assert.That(async () => await deployment.ApplyPackageAsync(changes)).ThrowsExactly<InvalidOperationException>();

        await Assert.That(service.UploadCount).IsEqualTo(1);
        await Assert.That(All(store, SdkMessageProcessingStep.EntityLogicalName)).Count().IsEqualTo(0);
        await Assert.That(service.PackageContent(packageId)).IsEquivalentTo("concurrent-package"u8.ToArray());
    }

    [Test]
    public async Task Apply_LastAssemblyMissingTypes_RejectsBeforeWritingFirstAssemblyRegistrations()
    {
        var first = Assembly("First", CreateLocalType("First.Plugin", Step("first")));
        var second = Assembly("Second", CreateLocalType("Second.Plugin", Step("second")));
        var desired = Package(Convert.ToBase64String(s_initialPackage), first, second);
        var uploaded = desired with { Assemblies = [first, second with { PluginTypes = [] }] };
        var store = CreateStore();
        var service = new PackageUploadService(store, desired, uploadedPackage: uploaded);
        var deployment = new PluginDeployment(service);
        var changes = await deployment.PreparePackageAsync(desired, "new");

        await Assert.That(async () => await deployment.ApplyPackageAsync(changes)).ThrowsExactly<InvalidOperationException>();

        await Assert.That(All(store, PluginAssembly.EntityLogicalName)).Count().IsEqualTo(2);
        await Assert.That(All(store, SdkMessageProcessingStep.EntityLogicalName)).Count().IsEqualTo(0);
    }

    [Test]
    public async Task Apply_EnvironmentConfigurationChangedAfterPreview_PreservesNewConfiguration()
    {
        var original = Package(Convert.ToBase64String(s_initialPackage),
            Assembly("First", CreateLocalType("First.Plugin", Step("original"))));
        var store = CreateStore();
        var service = new PackageUploadService(store, original);
        var deployment = new PluginDeployment(service);
        await deployment.ApplyPackageAsync(await deployment.PreparePackageAsync(original, "new"));
        var desired = original with { Assemblies = [Assembly("First", CreateLocalType("First.Plugin", Step("desired")))] };
        var changes = await deployment.PreparePackageAsync(desired, "new");
        var stepId = All(store, SdkMessageProcessingStep.EntityLogicalName).Single().Id;
        store.Update(new Entity(SdkMessageProcessingStep.EntityLogicalName, stepId)
        {
            ["configuration"] = "changed after preview", ["statecode"] = new OptionSetValue(1)
        });

        await deployment.ApplyPackageAsync(changes);

        var actual = All(store, SdkMessageProcessingStep.EntityLogicalName).Single();
        await Assert.That(actual.GetAttributeValue<string>("name")).IsEqualTo("desired");
        await Assert.That(actual.GetAttributeValue<string>("configuration")).IsEqualTo("changed after preview");
        await Assert.That(actual.GetAttributeValue<OptionSetValue>("statecode").Value).IsEqualTo(1);
    }

    private sealed class PackageUploadService : IOrganizationServiceAsync2
    {
        private readonly FakeOrganizationServiceAsync service;
        private readonly LocalPluginPackage target;
        private readonly bool materializeOnUpload;
        private readonly Dictionary<Guid, byte[]> _packageFiles = [];

        public int UploadCount { get; private set; }
        public bool FailUpload { get; set; }

        public PackageUploadService(FakeOrganizationServiceAsync service, LocalPluginPackage target,
            bool materializeOnUpload = true, LocalPluginPackage? uploadedPackage = null)
        {
            this.service = service;
            this.target = uploadedPackage ?? target;
            this.materializeOnUpload = materializeOnUpload;
            service.AddRequests(new InitializePackageFileDownloadExecutor(_packageFiles));
            service.AddRequests(new DownloadPackageFileBlockExecutor(_packageFiles));
        }

        public byte[] PackageContent(Guid packageId)
            => _packageFiles[packageId];

        public Guid SeedPackage(string name, LocalPluginPackage package, byte[] bytes)
        {
            var fileId = Guid.NewGuid();
            var id = service.Create(new PluginPackage
            {
                Name = name,
                Version = package.Package.Version,
                Attributes = { [PluginPackage.LogicalNames.Package] = fileId }
            });
            _packageFiles.Add(id, bytes);
            return id;
        }

        public void MaterializeAssemblies(Guid packageId, LocalPluginPackage package) =>
            Materialize(packageId, package);

        private void Materialize(Guid packageId, LocalPluginPackage package)
        {
            foreach (var assembly in package.Assemblies)
            {
                var existing = service.RetrieveMultiple(new QueryExpression(PluginAssembly.EntityLogicalName)
                {
                    ColumnSet = new ColumnSet(PluginAssembly.LogicalNames.PluginAssemblyId),
                    Criteria = new FilterExpression
                    {
                        Conditions =
                        {
                            new ConditionExpression(PluginAssembly.LogicalNames.PackageId, ConditionOperator.Equal, packageId),
                            new ConditionExpression(PluginAssembly.LogicalNames.Name, ConditionOperator.Equal, assembly.Name)
                        }
                    }
                }).Entities.SingleOrDefault();
                var assemblyId = existing?.Id ?? service.Create(new PluginAssembly
                {
                    Name = assembly.Name,
                    Version = assembly.Version.ToString(),
                    SourceType = new OptionSetValue(PluginAssembly.Options.SourceType.Database),
                    IsolationMode = new OptionSetValue(PluginAssembly.Options.IsolationMode.Sandbox),
                    PackageId = new EntityReference(PluginPackage.EntityLogicalName, packageId)
                });

                var existingTypes = service.RetrieveMultiple(new QueryExpression(PluginType.EntityLogicalName)
                {
                    ColumnSet = new ColumnSet(PluginType.LogicalNames.TypeName),
                    Criteria = new FilterExpression
                    {
                        Conditions = { new ConditionExpression(PluginType.LogicalNames.PluginAssemblyId, ConditionOperator.Equal, assemblyId) }
                    }
                }).Entities.Select(entity => entity.GetAttributeValue<string>(PluginType.LogicalNames.TypeName)).ToHashSet(StringComparer.Ordinal);
                foreach (var pluginType in assembly.PluginTypes.Where(pluginType => !existingTypes.Contains(pluginType.TypeName)))
                {
                    service.Create(new PluginType
                    {
                        PluginAssemblyId = new EntityReference(PluginAssembly.EntityLogicalName, assemblyId),
                        TypeName = pluginType.TypeName,
                        Name = pluginType.Name,
                        FriendlyName = pluginType.Name
                    });
                }
            }
        }

        public Task AssociateAsync(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities, CancellationToken cancellationToken) =>
            service.AssociateAsync(entityName, entityId, relationship, relatedEntities, cancellationToken);
        public Task AssociateAsync(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities) =>
            service.AssociateAsync(entityName, entityId, relationship, relatedEntities);
        public void Associate(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities) =>
            service.Associate(entityName, entityId, relationship, relatedEntities);
        public Guid Create(Entity entity) => service.Create(entity);
        public Task<Guid> CreateAsync(Entity entity, CancellationToken cancellationToken)
        {
            if (entity is not PluginPackage package)
            {
                return service.CreateAsync(entity, cancellationToken);
            }

            var fileId = Guid.NewGuid();
            var content = Convert.FromBase64String(package.Content!);
            package[PluginPackage.LogicalNames.Package] = fileId;
            return CreatePackageAsync(package, content, cancellationToken);
        }

        public Task<Guid> CreateAsync(Entity entity) => CreateAsync(entity, CancellationToken.None);
        private async Task<Guid> CreatePackageAsync(PluginPackage package, byte[] content, CancellationToken cancellationToken)
        {
            EnsureUploadAllowed();
            var id = await service.CreateAsync(package, cancellationToken);
            UploadCount++;
            _packageFiles.Add(id, content);
            if (materializeOnUpload)
            {
                Materialize(id, target);
            }

            return id;
        }

        public Task<Entity> CreateAndReturnAsync(Entity entity, CancellationToken cancellationToken) => service.CreateAndReturnAsync(entity, cancellationToken);
        public Task DeleteAsync(string entityName, Guid id, CancellationToken cancellationToken) => service.DeleteAsync(entityName, id, cancellationToken);
        public Task DeleteAsync(string entityName, Guid id) => service.DeleteAsync(entityName, id);
        public void Delete(string entityName, Guid id) => service.Delete(entityName, id);
        public Task DisassociateAsync(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities, CancellationToken cancellationToken) =>
            service.DisassociateAsync(entityName, entityId, relationship, relatedEntities, cancellationToken);
        public Task DisassociateAsync(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities) =>
            service.DisassociateAsync(entityName, entityId, relationship, relatedEntities);
        public void Disassociate(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities) =>
            service.Disassociate(entityName, entityId, relationship, relatedEntities);
        public Task<OrganizationResponse> ExecuteAsync(OrganizationRequest request, CancellationToken cancellationToken) =>
            service.ExecuteAsync(request, cancellationToken);
        public Task<OrganizationResponse> ExecuteAsync(OrganizationRequest request) => service.ExecuteAsync(request);
        public OrganizationResponse Execute(OrganizationRequest request) => service.Execute(request);
        public Entity Retrieve(string entityName, Guid id, ColumnSet columnSet) => service.Retrieve(entityName, id, columnSet);
        public Task<Entity> RetrieveAsync(string entityName, Guid id, ColumnSet columnSet, CancellationToken cancellationToken) =>
            service.RetrieveAsync(entityName, id, columnSet, cancellationToken);
        public Task<Entity> RetrieveAsync(string entityName, Guid id, ColumnSet columnSet) => service.RetrieveAsync(entityName, id, columnSet);
        public EntityCollection RetrieveMultiple(QueryBase query) => service.RetrieveMultiple(query);
        public Task<EntityCollection> RetrieveMultipleAsync(QueryBase query, CancellationToken cancellationToken) =>
            service.RetrieveMultipleAsync(query, cancellationToken);
        public Task<EntityCollection> RetrieveMultipleAsync(QueryBase query) => service.RetrieveMultipleAsync(query);
        public void Update(Entity entity) => service.Update(entity);

        public async Task UpdateAsync(Entity entity, CancellationToken cancellationToken)
        {
            if (entity is not PluginPackage package)
            {
                await service.UpdateAsync(entity, cancellationToken);
                return;
            }

            var fileId = Guid.NewGuid();
            var content = Convert.FromBase64String(package.Content!);
            EnsureUploadAllowed();
            await service.UpdateAsync(package, cancellationToken);
            UploadCount++;
            await service.UpdateAsync(new PluginPackage(package.Id) { Attributes = { [PluginPackage.LogicalNames.Package] = fileId } }, cancellationToken);
            _packageFiles[package.Id] = content;
            if (materializeOnUpload)
            {
                Materialize(package.Id, target);
            }
        }

        public Task UpdateAsync(Entity entity) => UpdateAsync(entity, CancellationToken.None);

        private void EnsureUploadAllowed()
        {
            if (FailUpload)
            {
                throw new InvalidOperationException("Simulated upload failure before the package write.");
            }
        }
    }
}
