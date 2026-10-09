// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Security.Cryptography;
using System.Text;
using dgt.power.dataverse;
using dgt.power.plugin.Deployment.Local;
using dgt.power.plugin.Deployment.Output;
using dgt.power.plugin.Deployment.Reconciliation;
using dgt.power.plugin.Exceptions;
using dgt.power.plugin.Models;
using dgt.power.tests.FakeExecutor;
using Digitall.Dataverse.Testing;
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

    private static SolutionMembershipExecutor SeedSolution(FakeOrganizationServiceAsync store)
    {
        var membership = new SolutionMembershipExecutor();
        store.AddRequests(membership);
        store.Create(new Solution { UniqueName = "target" });
        store.Create(new SolutionComponentDefinition { PrimaryEntityName = PluginPackage.EntityLogicalName, SolutionComponentType = 10119 });
        return membership;
    }

    private static LocalPluginType ProviderType(DataProviderOperation operation) => CreateLocalType("Shared.Plugin") with
    {
        DataProviders = [new LocalDataProviderRegistration("dgt_Source", operation, "Provider", null, null, null)]
    };

    [Test]
    public async Task Apply_PackageProvider_ResolvesHandlersAcrossAssembliesWithSameClrName()
    {
        var desired = Package(Convert.ToBase64String(s_initialPackage),
            Assembly("First", ProviderType(DataProviderOperation.Retrieve)),
            Assembly("Second", ProviderType(DataProviderOperation.RetrieveMultiple)));
        var store = CreateStore();
        ProviderTableFixture.Seed(store);
        var service = new PackageUploadService(store, desired);
        var deployment = new PluginDeployment(service);
        var changes = await deployment.PreparePackageAsync(desired, "new");
        using var console = new TestConsole();
        new DeploymentRenderer(console).RenderPackage(changes);

        await Assert.That(console.Output).Contains("Retrieve -> First / Shared.Plugin");
        await Assert.That(console.Output).Contains("RetrieveMultiple -> Second / Shared.Plugin");
        await Assert.That(service.UploadCount).IsEqualTo(0);
        await deployment.ApplyPackageAsync(changes);

        var provider = All(store, EntityDataProvider.EntityLogicalName).Single(entity => entity.GetAttributeValue<string>("name") == "Provider");
        foreach (var (assemblyName, operation) in new[] { ("First", DataProviderOperation.Retrieve), ("Second", DataProviderOperation.RetrieveMultiple) })
        {
            var assemblyId = All(store, PluginAssembly.EntityLogicalName).Single(entity => entity.GetAttributeValue<string>("name") == assemblyName).Id;
            var typeId = All(store, PluginType.EntityLogicalName).Single(entity => entity.GetAttributeValue<EntityReference>("pluginassemblyid").Id == assemblyId).Id;
            await Assert.That(provider.GetAttributeValue<Guid>(operation.HandlerField())).IsEqualTo(typeId);
        }
        await Assert.That((await deployment.PreparePackageAsync(desired, "new")).HasChanges).IsFalse();
    }

    [Test]
    public async Task Prepare_PackageProviderCompetingScopedHandlers_RejectsBeforeUpload()
    {
        var desired = Package(Convert.ToBase64String(s_initialPackage),
            Assembly("First", ProviderType(DataProviderOperation.Retrieve)),
            Assembly("Second", ProviderType(DataProviderOperation.Retrieve)));
        var store = CreateStore();
        ProviderTableFixture.Seed(store);
        var service = new PackageUploadService(store, desired);

        await Assert.That(async () => await new PluginDeployment(service).PreparePackageAsync(desired, "new")).ThrowsExactly<AssemblyException>();
        await Assert.That(service.UploadCount).IsEqualTo(0);
    }

    [Test]
    public async Task Apply_PackageProviderWriteFails_RetryReusesUploadAndRegistersProvider()
    {
        var desired = Package(Convert.ToBase64String(s_initialPackage), Assembly("First", ProviderType(DataProviderOperation.Retrieve)));
        var store = CreateStore();
        ProviderTableFixture.Seed(store);
        var service = new PackageUploadService(store, desired) { FailProviderWrites = true };
        var deployment = new PluginDeployment(service);

        await Assert.That(async () => await deployment.ApplyPackageAsync(await deployment.PreparePackageAsync(desired, "new"))).ThrowsExactly<InvalidOperationException>();

        await Assert.That(All(store, EntityDataProvider.EntityLogicalName)).Count().IsEqualTo(1);
        var typeId = All(store, PluginType.EntityLogicalName).Single().Id;
        service.FailProviderWrites = false;
        await deployment.ApplyPackageAsync(await deployment.PreparePackageAsync(desired, "new"));
        var provider = All(store, EntityDataProvider.EntityLogicalName).Single(entity => entity.GetAttributeValue<string>("name") == "Provider");
        await Assert.That(provider.GetAttributeValue<Guid>("retrieveplugin")).IsEqualTo(typeId);
        await Assert.That(service.UploadCount).IsEqualTo(1);
    }

    [Test]
    public async Task Apply_PackageProviderInterruptedAfterWrite_RetrySkipsCompletedProviderWrite()
    {
        var desired = Package(Convert.ToBase64String(s_initialPackage), Assembly("First", ProviderType(DataProviderOperation.Retrieve)));
        var store = CreateStore();
        ProviderTableFixture.Seed(store);
        var service = new PackageUploadService(store, desired);
        var deployment = new PluginDeployment(service);

        await Assert.That(async () => await deployment.ApplyPackageAsync(await deployment.PreparePackageAsync(desired, "new"), progress =>
        {
            if (progress.Resource == "data provider")
            {
                throw new InvalidOperationException("Interrupted after provider write.");
            }
        })).ThrowsExactly<InvalidOperationException>();

        var retry = await deployment.PreparePackageAsync(desired, "new");
        await Assert.That(retry.HasChanges).IsFalse();
        var writes = new List<PluginDeploymentProgress>();
        await deployment.ApplyPackageAsync(retry, writes.Add);
        await Assert.That(writes).IsEmpty();
        await Assert.That(service.UploadCount).IsEqualTo(1);
    }

    [Test]
    public async Task PrepareAndRender_PackageSolution_ShowsScopedAdditionsWithoutWrites()
    {
        var desired = Package(Convert.ToBase64String(s_initialPackage),
            Assembly("First", CreateLocalType("Shared.Plugin", Step("same"))),
            Assembly("Second", CreateLocalType("Shared.Plugin", Step("same"))));
        var store = CreateStore();
        var membership = SeedSolution(store);
        var service = new PackageUploadService(store, desired);
        var changes = await new PluginDeployment(service).PreparePackageAsync(desired, "new", "target");
        using var console = new TestConsole();

        new DeploymentRenderer(console).RenderPackage(changes);

        await Assert.That(console.Output).Contains("Solution membership: target");
        await Assert.That(console.Output).Contains("Package new_Contoso.Plugins + Add");
        await Assert.That(console.Output).Contains("Step same + Add");
        await Assert.That(membership.Requests).IsEmpty();
        await Assert.That(service.UploadCount).IsEqualTo(0);
    }

    [Test]
    public async Task Apply_PackageSolution_AddsOnlyPackageAndScopedStepsAndIsIdempotent()
    {
        var desired = Package(Convert.ToBase64String(s_initialPackage),
            Assembly("First", CreateLocalType("Shared.Plugin", Step("same"))),
            Assembly("Second", CreateLocalType("Shared.Plugin", Step("same"))));
        var store = CreateStore();
        var membership = SeedSolution(store);
        var service = new PackageUploadService(store, desired);
        var deployment = new PluginDeployment(service);

        var id = await deployment.ApplyPackageAsync(await deployment.PreparePackageAsync(desired, "new", "target"));

        await Assert.That(membership.Requests.Select(request => request.ComponentType)).IsEquivalentTo([10119, 92, 92]);
        await Assert.That(membership.Requests.Single(request => request.ComponentType == 10119).ComponentId).IsEqualTo(id);
        await Assert.That(membership.Requests.Where(request => request.ComponentType == 92).Select(request => request.ComponentId))
            .IsEquivalentTo(All(store, SdkMessageProcessingStep.EntityLogicalName).Select(step => step.Id));
        await Assert.That(membership.Requests.All(request => !request.AddRequiredComponents)).IsTrue();
        var repeated = await deployment.PreparePackageAsync(desired, "new", "target");
        await Assert.That(repeated.HasChanges).IsFalse();
        using var console = new TestConsole();
        new DeploymentRenderer(console).RenderPackage(repeated);
        await Assert.That(console.Output).Contains("All components are already in the solution");
        await deployment.ApplyPackageAsync(repeated);
        await Assert.That(membership.Requests).Count().IsEqualTo(3);
        await Assert.That(service.UploadCount).IsEqualTo(1);
    }

    [Test]
    public async Task Apply_PackageMembershipOnly_PreservesOtherMembershipsAndSkipsUpload()
    {
        var desired = Package(Convert.ToBase64String(s_initialPackage),
            Assembly("First", CreateLocalType("First.Plugin", Step("first"))));
        var store = CreateStore();
        var membership = SeedSolution(store);
        var service = new PackageUploadService(store, desired);
        var deployment = new PluginDeployment(service);
        var packageId = await deployment.ApplyPackageAsync(await deployment.PreparePackageAsync(desired, "new"));
        var otherSolution = store.Create(new Solution { UniqueName = "other" });
        var otherMembership = store.Create(new SolutionComponent
        {
            Attributes =
            {
                ["objectid"] = packageId, ["solutionid"] = new EntityReference(Solution.EntityLogicalName, otherSolution),
                ["componenttype"] = new OptionSetValue(10119)
            }
        });
        var changes = await deployment.PreparePackageAsync(desired, "new", "target");

        await Assert.That(changes.UpdateContent).IsFalse();
        await Assert.That(changes.Assemblies.All(assembly => !assembly.Registrations.HasChanges)).IsTrue();
        await Assert.That(changes.HasChanges).IsTrue();
        await deployment.ApplyPackageAsync(changes);

        await Assert.That(service.UploadCount).IsEqualTo(1);
        await Assert.That(membership.Requests).Count().IsEqualTo(2);
        await Assert.That(All(store, SolutionComponent.EntityLogicalName).Any(component => component.Id == otherMembership)).IsTrue();
        await Assert.That((await deployment.PreparePackageAsync(desired, "new", "target")).HasChanges).IsFalse();
    }

    [Test]
    public async Task Prepare_PackageSolutionWithoutComponentDefinition_FailsBeforeUpload()
    {
        var desired = Package(Convert.ToBase64String(s_initialPackage), Assembly("First", CreateLocalType("First.Plugin")));
        var store = CreateStore();
        store.Create(new Solution { UniqueName = "target" });
        var service = new PackageUploadService(store, desired);

        await Assert.That(async () => await new PluginDeployment(service).PreparePackageAsync(desired, "new", "target")).ThrowsExactly<InvalidOperationException>();
        await Assert.That(service.UploadCount).IsEqualTo(0);
    }

    [Test]
    public async Task Apply_PackageAlreadyInSolution_AddsOnlyMissingStepMembership()
    {
        var desired = Package(Convert.ToBase64String(s_initialPackage),
            Assembly("First", CreateLocalType("First.Plugin", Step("first"), Step("second") with { Stage = 40 })));
        var store = CreateStore();
        var membership = SeedSolution(store);
        var service = new PackageUploadService(store, desired);
        var deployment = new PluginDeployment(service);
        await deployment.ApplyPackageAsync(await deployment.PreparePackageAsync(desired, "new", "target"));
        var secondStep = All(store, SdkMessageProcessingStep.EntityLogicalName).Single(step => step.GetAttributeValue<string>("name") == "second");
        var missingMembership = All(store, SolutionComponent.EntityLogicalName).Single(component => component.GetAttributeValue<Guid>("objectid") == secondStep.Id);
        store.Delete(SolutionComponent.EntityLogicalName, missingMembership.Id);

        var changes = await deployment.PreparePackageAsync(desired, "new", "target");
        await Assert.That(changes.Solution!.AddPackage).IsFalse();
        await Assert.That(changes.Solution.Steps).Count().IsEqualTo(1);
        await deployment.ApplyPackageAsync(changes);

        await Assert.That(membership.Requests).Count().IsEqualTo(4);
        await Assert.That(membership.Requests.Last().ComponentId).IsEqualTo(secondStep.Id);
        await Assert.That(service.UploadCount).IsEqualTo(1);
    }

    [Test]
    public async Task Prepare_PackageMissingSolution_FailsBeforeUpload()
    {
        var desired = Package(Convert.ToBase64String(s_initialPackage), Assembly("First", CreateLocalType("First.Plugin")));
        var store = CreateStore();
        SeedSolution(store);
        var service = new PackageUploadService(store, desired);

        await Assert.That(async () => await new PluginDeployment(service).PreparePackageAsync(desired, "new", "missing")).ThrowsExactly<MissingSolutionException>();
        await Assert.That(service.UploadCount).IsEqualTo(0);
    }

    [Test]
    public async Task Apply_PackageMembershipFailure_FreshPreparationRetriesWithoutUploadOrDuplicateRegistrations()
    {
        var desired = Package(Convert.ToBase64String(s_initialPackage),
            Assembly("First", CreateLocalType("First.Plugin", Step("first"))));
        var store = CreateStore();
        var membership = SeedSolution(store);
        membership.FailWrites = true;
        var service = new PackageUploadService(store, desired);
        var deployment = new PluginDeployment(service);

        await Assert.That(async () => await deployment.ApplyPackageAsync(await deployment.PreparePackageAsync(desired, "new", "target"))).ThrowsExactly<InvalidOperationException>();

        var stepId = All(store, SdkMessageProcessingStep.EntityLogicalName).Single().Id;
        membership.FailWrites = false;
        var retry = await deployment.PreparePackageAsync(desired, "new", "target");
        await deployment.ApplyPackageAsync(retry);

        await Assert.That(service.UploadCount).IsEqualTo(1);
        await Assert.That(All(store, SdkMessageProcessingStep.EntityLogicalName).Single().Id).IsEqualTo(stepId);
        await Assert.That(membership.Requests).Count().IsEqualTo(2);
        await Assert.That((await deployment.PreparePackageAsync(desired, "new", "target")).HasChanges).IsFalse();
    }

    [Test]
    [Arguments(false)]
    [Arguments(true)]
    public async Task Apply_InterruptedAfterPackageMembership_RetryAddsOnlyMissingSteps(bool cancel)
    {
        var desired = Package(Convert.ToBase64String(s_initialPackage),
            Assembly("First", CreateLocalType("First.Plugin", Step("first"))));
        var store = CreateStore();
        var membership = SeedSolution(store);
        var service = new PackageUploadService(store, desired);
        var deployment = new PluginDeployment(service);
        using var cancellation = new CancellationTokenSource();

        async Task Apply()
        {
            await deployment.ApplyPackageAsync(await deployment.PreparePackageAsync(desired, "new", "target"), progress =>
            {
                if (progress.Operation != PluginDeploymentOperation.AddedToSolution)
                {
                    return;
                }
                if (cancel)
                {
                    cancellation.Cancel();
                }
                else
                {
                    throw new InvalidOperationException("Interrupted after membership write.");
                }
            }, cancellation.Token);
        }

        if (cancel)
        {
            await Assert.That(Apply).Throws<OperationCanceledException>();
        }
        else
        {
            await Assert.That(Apply).ThrowsExactly<InvalidOperationException>();
        }
        await Assert.That(membership.Requests).Count().IsEqualTo(1);
        var retry = await deployment.PreparePackageAsync(desired, "new", "target");
        await Assert.That(retry.Solution!.AddPackage).IsFalse();
        await Assert.That(retry.Solution.Steps).Count().IsEqualTo(1);
        await deployment.ApplyPackageAsync(retry);

        await Assert.That(membership.Requests).Count().IsEqualTo(2);
        await Assert.That(service.UploadCount).IsEqualTo(1);
    }

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

        await Assert.That(console.Output).Contains("new_Contoso.Plugins + Create");
        await Assert.That(console.Output).Contains("First v1.0.0.0");
        await Assert.That(console.Output).Contains("Second v1.0.0.0");
        await Assert.That(console.Output).Contains("Shared.Plugin");
        await Assert.That(console.Output).DoesNotContain("package upload");
        await Assert.That(console.Output).Contains("first[step] + Create");
        await Assert.That(console.Output).Contains("Pre[Image] + Create");
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
        await Assert.That(console.Output).Contains("new_Contoso.Plugins").And.DoesNotContain("Unchanged");
        await Assert.That(console.Output).Contains("First v1.0.0.0");
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
        await Assert.That(console.Output).Contains("new_Contoso.Plugins ~ Update");
        await Assert.That(console.Output).Contains("First v1.0.0.0");
        await Assert.That(console.Output).DoesNotContain("package upload");
        await Assert.That(console.Output).Contains("First.Plugin");
        await Assert.That(console.Output).Contains("new name ~ Update");
        await Assert.That(console.Output).Contains("PreImage ~ Update");
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
    public async Task Prepare_ExistingPackageAtDifferentVersion_LeavesValidationToDataverse()
    {
        var desired = Package(Convert.ToBase64String(s_initialPackage), Assembly("First", CreateLocalType("First.Plugin")));
        var store = CreateStore();
        var service = new PackageUploadService(store, desired);
        service.SeedPackage("new_Contoso.Plugins", desired with
        {
            Package = desired.Package with { Version = "2.0.0" }
        }, s_initialPackage);
        var deployment = new PluginDeployment(service);

        var changes = await deployment.PreparePackageAsync(desired, "new");

        await Assert.That(changes.Existing).IsNotNull();
        await Assert.That(service.UploadCount).IsEqualTo(0);
        await Assert.That(All(store, PluginAssembly.EntityLogicalName)).Count().IsEqualTo(0);
    }

    [Test]
    public async Task Prepare_PackageAssemblyRemoval_PlansAssemblyDeletionAsDestructive()
    {
        var oldDesired = Package(Convert.ToBase64String(s_initialPackage),
            Assembly("First", CreateLocalType("First.Plugin")), Assembly("Second", CreateLocalType("Second.Plugin")));
        var desired = Package(Convert.ToBase64String(s_initialPackage), Assembly("First", CreateLocalType("First.Plugin")));
        var store = CreateStore();
        var service = new PackageUploadService(store, desired);
        var packageId = service.SeedPackage("new_Contoso.Plugins", oldDesired, s_initialPackage);
        service.MaterializeAssemblies(packageId, oldDesired);
        var deployment = new PluginDeployment(service);

        var changes = await deployment.PreparePackageAsync(desired, "new");

        await Assert.That(changes.HasChanges).IsTrue();
        await Assert.That(changes.RemovedAssemblies).Count().IsEqualTo(1);
        await Assert.That(changes.DestructiveCount).IsEqualTo(2);
        await Assert.That(All(store, PluginAssembly.EntityLogicalName)).Count().IsEqualTo(2);
        using var console = new TestConsole();
        console.Profile.Width = 160;
        new DeploymentRenderer(console).RenderPackage(changes);
        await Assert.That(console.Output).Contains("Second v").And.Contains("- Delete");
        await Assert.That(console.Output).Contains("Plugin type Second.Plugin - Delete");
    }

    [Test]
    public async Task Apply_PackageAssemblyRemoval_DeletesStepsBeforeUploadSoServerCanRemoveAssembly()
    {
        var oldDesired = Package(Convert.ToBase64String(s_initialPackage),
            Assembly("First", CreateLocalType("First.Plugin")), Assembly("Second", CreateLocalType("Second.Plugin", Step("registered"))));
        var removal = Package(Convert.ToBase64String("smaller-package"u8), Assembly("First", CreateLocalType("First.Plugin")));
        var store = CreateStore();
        var service = new PackageUploadService(store, oldDesired);
        var deployment = new PluginDeployment(service);
        await deployment.ApplyPackageAsync(await deployment.PreparePackageAsync(oldDesired, "new"));
        service.RetargetUpload(removal);
        var changes = await deployment.PreparePackageAsync(removal, "new");
        var progress = new List<PluginDeploymentProgress>();

        await deployment.ApplyPackageAsync(changes, progress.Add);

        await Assert.That(progress.Any(item => item.Resource == "step" && item.Operation == PluginDeploymentOperation.Deleted)).IsTrue();
        await Assert.That(All(store, PluginAssembly.EntityLogicalName)).Count().IsEqualTo(1);
        await Assert.That(All(store, PluginType.EntityLogicalName)).Count().IsEqualTo(1);
        await Assert.That(All(store, SdkMessageProcessingStep.EntityLogicalName)).Count().IsEqualTo(0);
    }

    [Test]
    public async Task Prepare_PackageTypeRemoval_PlansTypeDeletionAsDestructive()
    {
        var desired = Package(Convert.ToBase64String(s_initialPackage),
            Assembly("First", CreateLocalType("First.Plugin"), CreateLocalType("First.Removed")));
        var store = CreateStore();
        var service = new PackageUploadService(store, desired);
        var deployment = new PluginDeployment(service);
        await deployment.ApplyPackageAsync(await deployment.PreparePackageAsync(desired, "new"));
        var removal = desired with { Assemblies = [Assembly("First", CreateLocalType("First.Plugin"))] };

        var changes = await deployment.PreparePackageAsync(removal, "new");

        await Assert.That(changes.HasChanges).IsTrue();
        await Assert.That(changes.DestructiveCount).IsEqualTo(1);
        await Assert.That(All(store, PluginType.EntityLogicalName)).Count().IsEqualTo(2);
        using var console = new TestConsole();
        console.Profile.Width = 160;
        new DeploymentRenderer(console).RenderPackage(changes);
        await Assert.That(console.Output).Contains("First.Plugin").And.DoesNotContain("Unchanged");
        await Assert.That(console.Output).Contains("Plugin type First.Removed - Delete");
    }

    [Test]
    public async Task Apply_PackageTypeRemoval_DeletesStepsBeforeUploadSoServerCanRemoveType()
    {
        var oldDesired = Package(Convert.ToBase64String(s_initialPackage),
            Assembly("First", CreateLocalType("First.Plugin"), CreateLocalType("First.Removed", Step("registered"))));
        var removal = Package(Convert.ToBase64String("smaller-package"u8),
            Assembly("First", CreateLocalType("First.Plugin")));
        var store = CreateStore();
        var service = new PackageUploadService(store, oldDesired);
        var deployment = new PluginDeployment(service);
        await deployment.ApplyPackageAsync(await deployment.PreparePackageAsync(oldDesired, "new"));
        service.RetargetUpload(removal);
        var changes = await deployment.PreparePackageAsync(removal, "new");
        await Assert.That(changes.DestructiveCount).IsEqualTo(2);
        await Assert.That(All(store, SdkMessageProcessingStep.EntityLogicalName)).Count().IsEqualTo(1);
        var progress = new List<PluginDeploymentProgress>();
        await deployment.ApplyPackageAsync(changes, progress.Add);

        await Assert.That(progress.Any(item => item.Resource == "step" && item.Operation == PluginDeploymentOperation.Deleted)).IsTrue();
        await Assert.That(progress.Any(item => item.Resource == "plugin type")).IsFalse();
        await Assert.That(All(store, PluginType.EntityLogicalName)).Count().IsEqualTo(1);
        await Assert.That(All(store, SdkMessageProcessingStep.EntityLogicalName)).Count().IsEqualTo(0);
    }

    [Test]
    public async Task Apply_PackageStepRemoval_PreparesAndDeletesStepKeepingType()
    {
        var desired = Package(Convert.ToBase64String(s_initialPackage), Assembly("First", CreateLocalType("First.Plugin", Step("registered"))));
        var store = CreateStore();
        var service = new PackageUploadService(store, desired);
        var deployment = new PluginDeployment(service);
        await deployment.ApplyPackageAsync(await deployment.PreparePackageAsync(desired, "new"));
        var removal = desired with { Assemblies = [Assembly("First", CreateLocalType("First.Plugin"))] };

        var changes = await deployment.PreparePackageAsync(removal, "new");

        await Assert.That(changes.HasChanges).IsTrue();
        using var console = new TestConsole();
        console.Profile.Width = 160;
        new DeploymentRenderer(console).RenderPackage(changes);
        await Assert.That(console.Output).Contains("        └── Step registered - Delete");
        await Assert.That(All(store, SdkMessageProcessingStep.EntityLogicalName)).Count().IsEqualTo(1);

        await deployment.ApplyPackageAsync(changes);

        await Assert.That(All(store, SdkMessageProcessingStep.EntityLogicalName)).Count().IsEqualTo(0);
        await Assert.That(All(store, PluginType.EntityLogicalName)).Count().IsEqualTo(1);
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

}
