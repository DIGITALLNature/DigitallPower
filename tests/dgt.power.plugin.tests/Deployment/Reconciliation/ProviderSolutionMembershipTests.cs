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

public class ProviderSolutionMembershipTests
{
    private const int ProviderComponentType = 10401;

    private static LocalAssembly Desired(bool declared = true) => new()
    {
        Name = "Plugins", Version = new Version(1, 0, 0, 0), Content = "Y29udGVudA==",
        ContentHash = Convert.ToHexString(SHA256.HashData("content"u8)),
        PluginTypes = [new LocalPluginType("Plugin", "Plugin", string.Empty, true, [])
        {
            DataProviders = declared
                ? [new LocalDataProviderRegistration("dgt_Source", DataProviderOperation.Retrieve, "Provider", null, null, null)]
                : []
        }]
    };

    private static (FakeOrganizationServiceAsync Store, SolutionMembershipExecutor Membership) Setup(int definitions = 1)
    {
        var store = new FakeOrganizationServiceAsync();
        store.AddDefaultRequests();
        store.AddRequests(new RetrieveDependenciesForDeleteExecutor());
        ProviderTableFixture.Seed(store);
        store.Create(new Solution { UniqueName = "target" });
        for (var i = 0; i < definitions; i++)
        {
            store.Create(new SolutionComponentDefinition
            {
                PrimaryEntityName = EntityDataProvider.EntityLogicalName, SolutionComponentType = ProviderComponentType
            });
        }
        var desired = Desired();
        store.Create(new PluginAssembly
        {
            Name = desired.Name, Version = desired.Version.ToString(), Content = desired.Content,
            SourceType = new OptionSetValue(0), IsolationMode = new OptionSetValue(2)
        });
        var membership = new SolutionMembershipExecutor();
        store.AddRequests(membership);
        return (store, membership);
    }

    private static IReadOnlyList<Entity> All(FakeOrganizationServiceAsync store, string name) =>
        store.RetrieveMultiple(new QueryExpression(name) { ColumnSet = new ColumnSet(true) }).Entities;

    [Test]
    public async Task Apply_NewProvider_AddsOnlyResolvedProviderAndAssemblyAndIsIdempotent()
    {
        var (store, membership) = Setup();
        var deployment = new PluginDeployment(store);
        var changes = await deployment.PrepareAsync(Desired(), "target");
        using var console = new TestConsole();
        new DeploymentRenderer(console).Render(changes);

        await Assert.That(console.Output.Contains("Data-source table")).IsFalse();
        await Assert.That(console.Output).Contains("Data provider dgt_source + Add");
        await Assert.That(membership.Requests).IsEmpty();
        await deployment.ApplyAsync(changes);

        var provider = All(store, EntityDataProvider.EntityLogicalName).Single(entity => entity.GetAttributeValue<string>("name") == "Provider");
        await Assert.That(membership.Requests.Select(request => request.ComponentType)).IsEquivalentTo([ProviderComponentType, 91]);
        await Assert.That(membership.Requests.Single(request => request.ComponentType == ProviderComponentType).ComponentId).IsEqualTo(provider.Id);
        await Assert.That(membership.Requests.All(request => !request.AddRequiredComponents)).IsTrue();
        var retry = await deployment.PrepareAsync(Desired(), "target");
        await Assert.That(retry.HasChanges).IsFalse();
        await deployment.ApplyAsync(retry);
        await Assert.That(membership.Requests).Count().IsEqualTo(2);
    }

    [Test]
    [Arguments(1)]
    [Arguments(ProviderComponentType)]
    public async Task Apply_MembershipOnly_DoesNotRewriteProviderAndPreservesUnrelatedMembership(int unrelatedType)
    {
        var (store, _) = Setup();
        var deployment = new PluginDeployment(store);
        await deployment.ApplyAsync(await deployment.PrepareAsync(Desired()));
        var unrelated = store.Create(new SolutionComponent
        {
            Attributes =
            {
                ["objectid"] = Guid.NewGuid(), ["componenttype"] = new OptionSetValue(unrelatedType),
                ["solutionid"] = new EntityReference(Solution.EntityLogicalName, All(store, Solution.EntityLogicalName).Single().Id)
            }
        });
        var changes = await deployment.PrepareAsync(Desired(), "target");

        await Assert.That(changes.Providers).IsEmpty();
        await Assert.That(changes.Registrations.HasChanges).IsFalse();
        await Assert.That(changes.HasChanges).IsTrue();
        var progress = new List<PluginDeploymentProgress>();
        await deployment.ApplyAsync(changes, progress.Add);

        await Assert.That(progress.Any(item => item.Resource == "data provider" && item.Operation != PluginDeploymentOperation.AddedToSolution)).IsFalse();
        await Assert.That(All(store, SolutionComponent.EntityLogicalName).Any(entity => entity.Id == unrelated)).IsTrue();
    }

    [Test]
    public async Task Apply_CancelledAfterProviderMembership_RetryAddsOnlyRemainingComponents()
    {
        var (store, membership) = Setup();
        var deployment = new PluginDeployment(store);
        using var cancellation = new CancellationTokenSource();
        var token = cancellation.Token;

        await Assert.That(async () => await deployment.ApplyAsync(await deployment.PrepareAsync(Desired(), "target", token), progress =>
        {
            if (progress is { Operation: PluginDeploymentOperation.AddedToSolution, Resource: "data provider" })
            {
                // The assertion is awaited before the cancellation source is disposed.
                // ReSharper disable once AccessToDisposedClosure
                cancellation.Cancel();
            }
        }, token)).Throws<OperationCanceledException>();

        await Assert.That(membership.Requests).Count().IsEqualTo(1);
        var retry = await deployment.PrepareAsync(Desired(), "target", CancellationToken.None);
        await Assert.That(retry.Solution!.Providers!.Additions).IsEmpty();
        await Assert.That(retry.Providers).IsEmpty();
        await deployment.ApplyAsync(retry, cancellationToken: CancellationToken.None);
        await Assert.That(membership.Requests).Count().IsEqualTo(2);
        await Assert.That((await deployment.PrepareAsync(Desired(), "target", CancellationToken.None)).HasChanges).IsFalse();
    }

    [Test]
    public async Task Apply_MembershipFailure_StopsReplacementCleanupAndFreshRetryCompletes()
    {
        var (store, membership) = Setup();
        var deployment = new PluginDeployment(store);
        await deployment.ApplyAsync(await deployment.PrepareAsync(Desired()));
        var desired = Desired() with { Version = new Version(2, 0, 0, 0) };
        membership.FailWrites = true;

        await Assert.That(async () => await deployment.ApplyAsync(await deployment.PrepareAsync(desired, "target")))
            .ThrowsExactly<InvalidOperationException>();
        await Assert.That(All(store, PluginAssembly.EntityLogicalName)).Count().IsEqualTo(2);
        var target = All(store, PluginAssembly.EntityLogicalName).Single(entity => entity.GetAttributeValue<string>("version") is null);
        store.Update(new PluginAssembly(target.Id)
        {
            Version = desired.Version.ToString(), SourceType = new OptionSetValue(0), IsolationMode = new OptionSetValue(2)
        });
        membership.FailWrites = false;
        await deployment.ApplyAsync(await deployment.PrepareAsync(desired, "target"));

        await Assert.That(All(store, PluginAssembly.EntityLogicalName)).Count().IsEqualTo(1);
        await Assert.That((await deployment.PrepareAsync(desired, "target")).HasChanges).IsFalse();
    }

    [Test]
    [Arguments(0)]
    [Arguments(2)]
    public async Task Prepare_MissingOrAmbiguousProviderDefinition_FailsBeforeWrites(int definitions)
    {
        var (store, membership) = Setup(definitions);

        await Assert.That(async () => await new PluginDeployment(store).PrepareAsync(Desired(), "target"))
            .ThrowsExactly<InvalidOperationException>();
        await Assert.That(All(store, PluginType.EntityLogicalName)).IsEmpty();
        await Assert.That(membership.Requests).IsEmpty();
    }

    [Test]
    public async Task Apply_WholeProviderOmission_DoesNotAddOrRemoveProviderMembership()
    {
        var (store, membership) = Setup();
        var deployment = new PluginDeployment(store);
        await deployment.ApplyAsync(await deployment.PrepareAsync(Desired(), "target"));
        var before = All(store, SolutionComponent.EntityLogicalName).Select(entity => entity.Id).ToArray();
        var changes = await deployment.PrepareAsync(Desired(false), "target");

        await Assert.That(changes.Solution!.Providers).IsNull();
        await deployment.ApplyAsync(changes);
        await Assert.That(All(store, SolutionComponent.EntityLogicalName).Select(entity => entity.Id)).IsEquivalentTo(before);
        await Assert.That(membership.Requests).Count().IsEqualTo(2);
    }

    [Test]
    public async Task Prepare_ExistingProviderMembership_DoesNotAddProviderAgainOrPullInTable()
    {
        var (store, membership) = Setup();
        var deployment = new PluginDeployment(store);
        await deployment.ApplyAsync(await deployment.PrepareAsync(Desired()));
        var initial = await deployment.PrepareAsync(Desired(), "target");
        var addition = initial.Solution!.Providers!.Additions.Single();
        store.Create(new SolutionComponent
        {
            Attributes =
            {
                ["objectid"] = addition.ExistingProviderId!.Value,
                ["componenttype"] = new OptionSetValue(ProviderComponentType),
                ["solutionid"] = new EntityReference(Solution.EntityLogicalName, All(store, Solution.EntityLogicalName).Single().Id)
            }
        });

        var changes = await deployment.PrepareAsync(Desired(), "target");
        await Assert.That(changes.Solution!.Providers!.Additions).IsEmpty();
        await deployment.ApplyAsync(changes);
        await Assert.That(membership.Requests.Select(request => request.ComponentType))
            .IsEquivalentTo([91]);
    }

    [Test]
    [Arguments(0)]
    [Arguments(2)]
    public async Task Prepare_PackageMissingOrAmbiguousProviderDefinition_FailsBeforeUpload(int definitions)
    {
        var (store, membership) = Setup(definitions);
        store.Delete(PluginAssembly.EntityLogicalName, All(store, PluginAssembly.EntityLogicalName).Single().Id);
        store.Create(new SolutionComponentDefinition { PrimaryEntityName = PluginPackage.EntityLogicalName, SolutionComponentType = 10119 });
        var desired = new LocalPluginPackage(new LocalPackage("Package", "1.0.0", "Y29udGVudA==",
            Convert.ToHexString(SHA256.HashData("content"u8))), [Desired()]);
        var service = new PackageUploadService(store, desired);

        await Assert.That(async () => await new PluginDeployment(service).PreparePackageAsync(desired, "dgt", "target"))
            .ThrowsExactly<InvalidOperationException>();
        await Assert.That(service.UploadCount).IsEqualTo(0);
        await Assert.That(membership.Requests).IsEmpty();
    }

    [Test]
    public async Task Apply_PackageProviderMembershipFailure_RetryReusesUploadAndAddsOnlyMissingMembership()
    {
        var (store, membership) = Setup();
        store.Delete(PluginAssembly.EntityLogicalName, All(store, PluginAssembly.EntityLogicalName).Single().Id);
        store.Create(new SolutionComponentDefinition { PrimaryEntityName = PluginPackage.EntityLogicalName, SolutionComponentType = 10119 });
        var desired = new LocalPluginPackage(new LocalPackage("Package", "1.0.0", "Y29udGVudA==",
            Convert.ToHexString(SHA256.HashData("content"u8))), [Desired()]);
        var service = new PackageUploadService(store, desired);
        var deployment = new PluginDeployment(service);
        var changes = await deployment.PreparePackageAsync(desired, "dgt", "target");
        using var console = new TestConsole();
        new DeploymentRenderer(console).RenderPackage(changes);
        await Assert.That(console.Output).Contains("Data provider dgt_source + Add");
        membership.FailWrites = true;

        await Assert.That(async () => await deployment.ApplyPackageAsync(changes)).ThrowsExactly<InvalidOperationException>();
        membership.FailWrites = false;
        var retry = await deployment.PreparePackageAsync(desired, "dgt", "target");
        await Assert.That(retry.UpdateContent).IsFalse();
        await Assert.That(retry.Providers).IsEmpty();
        await deployment.ApplyPackageAsync(retry);

        var providerId = All(store, EntityDataProvider.EntityLogicalName).Single(entity => entity.GetAttributeValue<string>("name") == "Provider").Id;
        await Assert.That(membership.Requests.Single(request => request.ComponentType == ProviderComponentType).ComponentId).IsEqualTo(providerId);
        await Assert.That(membership.Requests.Select(request => request.ComponentType)).IsEquivalentTo([ProviderComponentType, 10119]);
        await Assert.That(service.UploadCount).IsEqualTo(1);
        await Assert.That((await deployment.PreparePackageAsync(desired, "dgt", "target")).HasChanges).IsFalse();
    }

    [Test]
    public async Task Apply_PackageCancelledAfterProviderMembership_RetrySkipsProviderAndUpload()
    {
        var (store, membership) = Setup();
        store.Delete(PluginAssembly.EntityLogicalName, All(store, PluginAssembly.EntityLogicalName).Single().Id);
        store.Create(new SolutionComponentDefinition { PrimaryEntityName = PluginPackage.EntityLogicalName, SolutionComponentType = 10119 });
        var desired = new LocalPluginPackage(new LocalPackage("Package", "1.0.0", "Y29udGVudA==",
            Convert.ToHexString(SHA256.HashData("content"u8))), [Desired()]);
        var service = new PackageUploadService(store, desired);
        var deployment = new PluginDeployment(service);
        using var cancellation = new CancellationTokenSource();
        var token = cancellation.Token;

        await Assert.That(async () => await deployment.ApplyPackageAsync(await deployment.PreparePackageAsync(desired, "dgt", "target", token), progress =>
        {
            if (progress is { Operation: PluginDeploymentOperation.AddedToSolution, Resource: "data provider" })
            {
                // The assertion is awaited before the cancellation source is disposed.
                // ReSharper disable once AccessToDisposedClosure
                cancellation.Cancel();
            }
        }, token)).Throws<OperationCanceledException>();

        var retry = await deployment.PreparePackageAsync(desired, "dgt", "target", CancellationToken.None);
        await Assert.That(retry.Solution!.Providers!.Additions).IsEmpty();
        await Assert.That(retry.Providers).IsEmpty();
        await deployment.ApplyPackageAsync(retry, cancellationToken: CancellationToken.None);
        await Assert.That(membership.Requests.Select(request => request.ComponentType)).IsEquivalentTo([ProviderComponentType, 10119]);
        await Assert.That(service.UploadCount).IsEqualTo(1);
    }
}
