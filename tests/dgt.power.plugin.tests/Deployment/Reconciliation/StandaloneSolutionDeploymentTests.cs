// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using System.Security.Cryptography;
using dgt.power.dataverse;
using dgt.power.plugin.Deployment.Local;
using dgt.power.plugin.Deployment.Output;
using dgt.power.plugin.Deployment.Reconciliation;
using dgt.power.plugin.Exceptions;
using dgt.power.plugin.Repositories;
using dgt.power.tests.FakeExecutor;
using Digitall.Dataverse.Testing;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Spectre.Console.Testing;

namespace dgt.power.plugin.tests.Deployment.Reconciliation;

public class StandaloneSolutionDeploymentTests
{
    private static LocalAssembly Desired() => new()
    {
        Name = "Plugins", Version = new Version(1, 0, 0, 0), Content = "Y29udGVudA==",
        ContentHash = Convert.ToHexString(SHA256.HashData("content"u8)),
        PluginTypes = [new LocalPluginType("Plugin", "Plugin", string.Empty, true,
            [new LocalPluginStep("step", 0, "Update", 20, "none", "none", null, 100, [])])]
    };

    private static (FakeOrganizationServiceAsync Service, SolutionMembershipExecutor Membership) Service()
    {
        var service = new FakeOrganizationServiceAsync();
        var membership = new SolutionMembershipExecutor();
        service.AddRequests(membership, new RetrieveDependenciesForDeleteExecutor());
        service.AddDefaultRequests();
        service.Create(new Solution { UniqueName = "target" });
        service.Create(new SdkMessage { Name = "Update" });
        return (service, membership);
    }

    private static IReadOnlyList<Entity> All(FakeOrganizationServiceAsync service, string name) =>
        service.RetrieveMultiple(new QueryExpression(name) { ColumnSet = new ColumnSet(true) }).Entities;

    private static void SetAssemblyMetadata(FakeOrganizationServiceAsync service, Guid id, Version version) =>
        service.Update(new PluginAssembly(id) { Version = version.ToString(), SourceType = new OptionSetValue(0), IsolationMode = new OptionSetValue(2) });

    [Test]
    public async Task PrepareAndRender_NewAssembly_ShowsMembershipWithoutWrites()
    {
        var (service, membership) = Service();
        var changes = await new PluginDeployment(service).PrepareAsync(Desired(), "target");
        using var console = new TestConsole();

        new DeploymentRenderer(console).Render(changes);

        await Assert.That(console.Output).Contains("Solution membership: target");
        await Assert.That(console.Output).Contains("Assembly Plugins v1.0.0.0 + Add");
        await Assert.That(console.Output).Contains("Step step + Add");
        await Assert.That(membership.Requests).IsEmpty();
        await Assert.That(All(service, PluginAssembly.EntityLogicalName)).IsEmpty();
    }

    [Test]
    public async Task Prepare_MissingSolution_FailsBeforeWrites()
    {
        var (service, membership) = Service();

        await Assert.That(async () => await new PluginDeployment(service).PrepareAsync(Desired(), "missing")).ThrowsExactly<MissingSolutionException>();
        await Assert.That(membership.Requests).IsEmpty();
        await Assert.That(All(service, PluginAssembly.EntityLogicalName)).IsEmpty();
    }

    [Test]
    public async Task Apply_NewAssembly_AddsOnlyAssemblyAndStepsAndIsIdempotent()
    {
        var (service, membership) = Service();
        var deployment = new PluginDeployment(service);
        var id = await deployment.ApplyAsync(await deployment.PrepareAsync(Desired(), "target"));
        SetAssemblyMetadata(service, id, Desired().Version);

        await Assert.That(membership.Requests.Select(request => request.ComponentType)).IsEquivalentTo(
            [IPluginAssemblyRepository.ComponentType, ISdkMessageProcessingStepRepository.ComponentType]);
        await Assert.That(membership.Requests.All(request => !request.AddRequiredComponents)).IsTrue();
        await Assert.That(membership.Requests.Single(request => request.ComponentType == ISdkMessageProcessingStepRepository.ComponentType).ComponentId)
            .IsEqualTo(All(service, SdkMessageProcessingStep.EntityLogicalName).Single().Id);
        var repeated = await deployment.PrepareAsync(Desired(), "target");
        await Assert.That(repeated.HasChanges).IsFalse();
        using var console = new TestConsole();
        new DeploymentRenderer(console).Render(repeated);
        await Assert.That(console.Output).Contains("All components are already in the solution");
        await deployment.ApplyAsync(repeated);
        await Assert.That(membership.Requests).Count().IsEqualTo(2);
    }

    [Test]
    public async Task Apply_MembershipOnly_AddsUnchangedRegistrationsWithoutRemovingOtherMemberships()
    {
        var (service, membership) = Service();
        var deployment = new PluginDeployment(service);
        var id = await deployment.ApplyAsync(await deployment.PrepareAsync(Desired()));
        SetAssemblyMetadata(service, id, Desired().Version);
        var otherSolution = service.Create(new Solution { UniqueName = "other" });
        var otherMembership = service.Create(new SolutionComponent
        {
            Attributes =
            {
                ["objectid"] = id, ["solutionid"] = new EntityReference(Solution.EntityLogicalName, otherSolution),
                ["componenttype"] = new OptionSetValue(IPluginAssemblyRepository.ComponentType)
            }
        });

        var changes = await deployment.PrepareAsync(Desired(), "target");
        await Assert.That(changes.Registrations.HasChanges).IsFalse();
        await Assert.That(changes.HasChanges).IsTrue();
        await deployment.ApplyAsync(changes);

        await Assert.That(membership.Requests).Count().IsEqualTo(2);
        await Assert.That(All(service, SolutionComponent.EntityLogicalName).Any(component => component.Id == otherMembership)).IsTrue();
        await Assert.That(All(service, SdkMessageProcessingStep.EntityLogicalName)).Count().IsEqualTo(1);
    }

    [Test]
    public async Task Apply_IdenticalStepsInDifferentTypes_AddsEachResolvedStep()
    {
        var (service, membership) = Service();
        var desired = Desired();
        desired = desired with
        {
            PluginTypes = [desired.PluginTypes[0], desired.PluginTypes[0] with { TypeName = "Other", Name = "Other" }]
        };
        var deployment = new PluginDeployment(service);

        await deployment.ApplyAsync(await deployment.PrepareAsync(desired, "target"));

        await Assert.That(membership.Requests.Where(request => request.ComponentType == ISdkMessageProcessingStepRepository.ComponentType)
            .Select(request => request.ComponentId)).IsEquivalentTo(All(service, SdkMessageProcessingStep.EntityLogicalName).Select(step => step.Id));
        await Assert.That(membership.Requests).Count().IsEqualTo(3);
    }

    [Test]
    public async Task Apply_Upgrade_RetainsStepMembershipAndAddsReplacementAssembly()
    {
        var (service, membership) = Service();
        var deployment = new PluginDeployment(service);
        var oldId = await deployment.ApplyAsync(await deployment.PrepareAsync(Desired(), "target"));
        SetAssemblyMetadata(service, oldId, Desired().Version);
        var stepId = All(service, SdkMessageProcessingStep.EntityLogicalName).Single().Id;
        var desired = Desired() with { Version = new Version(2, 0, 0, 0) };

        var changes = await deployment.PrepareAsync(desired, "target");
        await Assert.That(changes.Solution!.Steps).IsEmpty();
        var newId = await deployment.ApplyAsync(changes);

        await Assert.That(membership.Requests).Count().IsEqualTo(3);
        await Assert.That(membership.Requests.Last().ComponentId).IsEqualTo(newId);
        await Assert.That(All(service, SdkMessageProcessingStep.EntityLogicalName).Single().Id).IsEqualTo(stepId);
        await Assert.That(All(service, PluginAssembly.EntityLogicalName).Single().Id).IsEqualTo(newId);
    }

    [Test]
    public async Task Apply_MembershipFailure_StopsBeforeCleanupAndRetryCompletes()
    {
        var (service, membership) = Service();
        var deployment = new PluginDeployment(service);
        var oldId = await deployment.ApplyAsync(await deployment.PrepareAsync(Desired(), "target"));
        SetAssemblyMetadata(service, oldId, Desired().Version);
        var desired = Desired() with { Version = new Version(2, 0, 0, 0) };
        membership.FailWrites = true;

        await Assert.That(async () => await deployment.ApplyAsync(await deployment.PrepareAsync(desired, "target"))).ThrowsExactly<InvalidOperationException>();

        await Assert.That(All(service, PluginAssembly.EntityLogicalName)).Count().IsEqualTo(2);
        await Assert.That(All(service, PluginType.EntityLogicalName)).Count().IsEqualTo(2);
        var target = All(service, PluginAssembly.EntityLogicalName).Single(assembly => assembly.Id != oldId);
        SetAssemblyMetadata(service, target.Id, desired.Version);
        membership.FailWrites = false;
        await deployment.ApplyAsync(await deployment.PrepareAsync(desired, "target"));

        await Assert.That(All(service, PluginAssembly.EntityLogicalName).Single().Id).IsEqualTo(target.Id);
        await Assert.That((await deployment.PrepareAsync(desired, "target")).HasChanges).IsFalse();
    }

    [Test]
    public async Task Apply_CancelledAfterAssemblyMembership_RetryAddsOnlyMissingStepMembership()
    {
        var (service, membership) = Service();
        var deployment = new PluginDeployment(service);
        using var cancellation = new CancellationTokenSource();
        var token = cancellation.Token;

        await Assert.That(async () => await deployment.ApplyAsync(await deployment.PrepareAsync(Desired(), "target", token), progress =>
        {
            if (progress.Operation == PluginDeploymentOperation.AddedToSolution)
            {
                // The assertion is awaited before the cancellation source is disposed.
                // ReSharper disable once AccessToDisposedClosure
                cancellation.Cancel();
            }
        }, token)).Throws<OperationCanceledException>();

        await Assert.That(membership.Requests).Count().IsEqualTo(1);
        var target = All(service, PluginAssembly.EntityLogicalName).Single();
        SetAssemblyMetadata(service, target.Id, Desired().Version);
        var retry = await deployment.PrepareAsync(Desired(), "target", CancellationToken.None);
        await Assert.That(retry.Solution!.AddAssembly).IsFalse();
        await Assert.That(retry.Solution.Steps).Count().IsEqualTo(1);
        await deployment.ApplyAsync(retry, cancellationToken: CancellationToken.None);
        await Assert.That(membership.Requests).Count().IsEqualTo(2);
    }
}
