// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.plugin.Deployment.Batch;
using dgt.power.plugin.Deployment.Output;
using dgt.power.plugin.Deployment.Reconciliation;
using dgt.power.tests.FakeExecutor;
using Digitall.Dataverse.Testing;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Spectre.Console.Testing;

namespace dgt.power.plugin.tests.Deployment.Batch;

public class DeploymentBatchRunnerTests
{
    private static readonly BatchOptions Default = new(null, null, false, false, false, false);

    private static FakeOrganizationServiceAsync Store(params string[] assemblies)
    {
        var store = new FakeOrganizationServiceAsync();
        store.AddDefaultRequests();
        store.AddRequests(new RetrieveDependenciesForDeleteExecutor());
        foreach (var name in assemblies)
        {
            store.Create(new PluginAssembly
            {
                Name = name, Version = "1.0.0.0", Content = "Y29udGVudA==", SourceType = new OptionSetValue(0), IsolationMode = new OptionSetValue(2)
            });
        }

        return store;
    }

    private static int Count(FakeOrganizationServiceAsync store, string entity) =>
        store.RetrieveMultiple(new QueryExpression(entity) { ColumnSet = new ColumnSet(false) }).Entities.Count;

    private static LocalDeploymentTarget Target(string name) => BatchPreflightTests.Standalone($"{name}.dll", BatchPreflightTests.Assembly(name));

    [Test]
    public async Task Run_ChangedThenUnchanged_ReportsAppliedThenUnchanged()
    {
        var store = Store("A");
        using var console = new TestConsole();
        var runner = new DeploymentBatchRunner(new PluginDeployment(store), console);

        var first = await runner.RunAsync([Target("A")], Default);
        var second = await runner.RunAsync([Target("A")], Default);

        await Assert.That(first.Targets.Single().Outcome).IsEqualTo(TargetOutcome.Applied);
        await Assert.That(second.Targets.Single().Outcome).IsEqualTo(TargetOutcome.Unchanged);
        await Assert.That(first.Success && second.Success).IsTrue();
        await Assert.That(Count(store, PluginType.EntityLogicalName)).IsEqualTo(1);
    }

    [Test]
    public async Task Run_ChangedThenUnchanged_ReportsDeployedThenUpToDate()
    {
        var store = Store("A");
        using var console = new TestConsole();
        var runner = new DeploymentBatchRunner(new PluginDeployment(store), console);

        await runner.RunAsync([Target("A")], Default);
        await Assert.That(console.Output).Contains("A.dll deployed").And.DoesNotContain("up to date");

        await runner.RunAsync([Target("A")], Default);
        await Assert.That(console.Output).Contains("A.dll is up to date");
    }

    [Test]
    [Arguments("Create", "[green]+ Create[/]")]
    [Arguments("Add", "[green]+ Add[/]")]
    [Arguments("Update", "[yellow]~ Update[/]")]
    [Arguments("Set", "[yellow]~ Set[/]")]
    [Arguments("Delete", "[red]- Delete[/]")]
    [Arguments("Unlink", "[red]- Unlink[/]")]
    [Arguments("Clear", "[red]- Clear[/]")]
    [Arguments("Unchanged", "[dim]= Unchanged[/]")]
    public async Task Tag_Action_MapsToMarkerAndColor(string action, string expected) =>
        await Assert.That(DeploymentRenderer.Tag(action)).IsEqualTo(expected);

    [Test]
    public async Task Run_DryRun_PreviewsWithoutWriting()
    {
        var store = Store("A");
        using var console = new TestConsole();

        var result = await new DeploymentBatchRunner(new PluginDeployment(store), console).RunAsync([Target("A")], Default with { DryRun = true, Confirm = true });

        await Assert.That(result.Targets.Single().Outcome).IsEqualTo(TargetOutcome.DryRun);
        await Assert.That(Count(store, PluginType.EntityLogicalName)).IsEqualTo(0);
        await Assert.That(console.Output).Contains("A.dll");
    }

    [Test]
    public async Task Run_ConfirmDeclined_DoesNotWriteAndContinuesWithNextTarget()
    {
        var store = Store("A", "B");
        using var console = new TestConsole();
        console.Interactive();
        console.Input.PushTextWithEnter("n");
        console.Input.PushTextWithEnter("y");

        var result = await new DeploymentBatchRunner(new PluginDeployment(store), console).RunAsync([Target("B"), Target("A")], Default with { Confirm = true });

        await Assert.That(result.Targets.Select(target => target.Outcome)).IsEquivalentTo([TargetOutcome.Declined, TargetOutcome.Applied]);
        await Assert.That(Count(store, PluginType.EntityLogicalName)).IsEqualTo(1);
        await Assert.That(result.Success).IsTrue();
    }

    [Test]
    [Arguments(true, false)]
    [Arguments(false, true)]
    public async Task Run_NonInteractiveOrCi_SkipsPrompt(bool nonInteractive, bool ci)
    {
        var store = Store("A");
        using var console = new TestConsole();

        var result = await new DeploymentBatchRunner(new PluginDeployment(store), console)
            .RunAsync([Target("A")], Default with { Confirm = true, NonInteractive = nonInteractive, IsCiAgent = ci });

        await Assert.That(result.Targets.Single().Outcome).IsEqualTo(TargetOutcome.Applied);
    }

    [Test]
    public async Task Run_FailedTarget_ContinuesButBatchIsNotSuccessful()
    {
        var store = Store("A", "Z");
        for (var minor = 0; minor < 3; minor++)
        {
            store.Create(new PluginAssembly
            {
                Name = "Bad", Version = $"1.{minor}.0.0", Content = "Y29udGVudA==", SourceType = new OptionSetValue(0), IsolationMode = new OptionSetValue(2)
            });
        }

        using var console = new TestConsole();
        var result = await new DeploymentBatchRunner(new PluginDeployment(store), console).RunAsync([Target("Z"), Target("Bad"), Target("A")], Default);

        await Assert.That(result.Targets.Select(target => (Path.GetFileName(target.FilePath), target.Outcome)))
            .IsEquivalentTo([("A.dll", TargetOutcome.Applied), ("Bad.dll", TargetOutcome.Failed), ("Z.dll", TargetOutcome.Applied)]);
        await Assert.That(result.Targets[1].Message).IsNotNull();
        await Assert.That(result.Success).IsFalse();
    }

    [Test]
    public async Task Run_TargetsAreProcessedInPathOrder()
    {
        var store = Store("A", "B", "C");
        using var console = new TestConsole();

        var result = await new DeploymentBatchRunner(new PluginDeployment(store), console).RunAsync([Target("C"), Target("a"), Target("B")], Default);

        await Assert.That(result.Targets.Select(target => Path.GetFileName(target.FilePath))).IsEquivalentTo(["a.dll", "B.dll", "C.dll"]);
    }

    [Test]
    public async Task Run_Conflict_RejectsBatchBeforeAnyWrite()
    {
        var store = Store("A");
        using var console = new TestConsole();
        var targets = new[] { BatchPreflightTests.Standalone("1/A.dll", BatchPreflightTests.Assembly("A")), BatchPreflightTests.Standalone("2/A.dll", BatchPreflightTests.Assembly("A")) };

        var result = await new DeploymentBatchRunner(new PluginDeployment(store), console).RunAsync(targets, Default);

        await Assert.That(result.Success).IsFalse();
        await Assert.That(result.Targets).IsEmpty();
        await Assert.That(result.PreflightErrors).Count().IsEqualTo(1);
        await Assert.That(Count(store, PluginType.EntityLogicalName)).IsEqualTo(0);
    }

    [Test]
    public async Task Run_PackageWithoutPublisherPrefix_RejectsBatch()
    {
        using var console = new TestConsole();
        var package = BatchPreflightTests.Package("p.nupkg", "P", BatchPreflightTests.Assembly("A"));

        var result = await new DeploymentBatchRunner(new PluginDeployment(Store()), console).RunAsync([package], Default);

        await Assert.That(result.Success).IsFalse();
        await Assert.That(result.PreflightErrors.Single()).Contains("--publisher-prefix");
    }

    [Test]
    public async Task Run_Cancelled_PropagatesCancellationAndIsNotAFailure()
    {
        using var console = new TestConsole();
        using var cts = new CancellationTokenSource();
        await cts.CancelAsync();

        await Assert.That(async () => await new DeploymentBatchRunner(new PluginDeployment(Store("A")), console).RunAsync([Target("A")], Default, cts.Token))
            .Throws<OperationCanceledException>();
        await Assert.That(console.Output).Contains("Cancelled");
    }
}
