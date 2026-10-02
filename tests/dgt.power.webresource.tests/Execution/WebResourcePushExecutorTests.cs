// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.webresource.Execution;
using dgt.power.webresource.Local;
using dgt.power.webresource.Output;
using dgt.power.webresource.Planning;
using dgt.power.webresource.Remote;
using dgt.power.webresource.Repositories;
using Spectre.Console.Testing;

namespace dgt.power.webresource.tests.Execution;

public class WebResourcePushExecutorTests
{
    [Test]
    public async Task ReportCompleted_RendersPluginStyleProgressLine()
    {
        using var console = new TestConsole();
        var reporter = new WebResourceExecutionReporter(console);

        reporter.ReportCompleted(new WebResourceDeploymentProgress("Created", "WebResource", "contoso_/main.js"));

        await Assert.That(console.Output).Contains("✔ Created WebResource contoso_/main.js");
    }

    [Test]
    public async Task ExecuteAsync_PublishesChangedResourcesIndividuallyAfterWritesAndMembership()
    {
        var operations = new List<string>();
        var progress = new List<WebResourceDeploymentProgress>();
        var repository = new RecordingWebResourceRepository(operations);
        var solutionRepository = new RecordingSolutionRepository(operations);
        var executor = new WebResourcePushExecutor(repository, solutionRepository);
        var local = CreateLocal("contoso_/main.js");
        var updatedLocal = CreateLocal("contoso_/updated.js");
        var updatedId = Guid.NewGuid();
        var plan = new WebResourcePushPlan(
            [
                new WebResourcePlanItem(local, WebResourceAction.Create, null, true),
                new WebResourcePlanItem(
                    updatedLocal,
                    WebResourceAction.Update,
                    new RemoteWebResource(updatedId, updatedLocal.Type, updatedLocal.Name, "old", false),
                    false)
            ],
            [],
            "ContosoCore");

        var operationCount = await executor.ExecuteAsync(plan, progress.Add);

        await Assert.That(operationCount).IsEqualTo(5);
        await Assert.That(repository.Created).IsEquivalentTo([local.Name]);
        await Assert.That(repository.Updated).IsEquivalentTo([(updatedLocal.Name, updatedId)]);
        await Assert.That(repository.PublishBatches).Count().IsEqualTo(2);
        await Assert.That(repository.PublishBatches.All(batch => batch.Count == 1)).IsTrue();
        await Assert.That(repository.PublishBatches.SelectMany(batch => batch))
            .IsEquivalentTo([repository.CreatedId, updatedId]);
        await Assert.That(solutionRepository.Added).IsEquivalentTo([(repository.CreatedId, "ContosoCore")]);
        await Assert.That(operations).IsEquivalentTo(["Create", "Update", "Add", "Publish", "Publish"]);
        await Assert.That(progress.Select(item => (item.Operation, item.Resource, item.Name))).IsEquivalentTo(
        [
            ("Created", "WebResource", local.Name),
            ("Updated", "WebResource", updatedLocal.Name),
            ("Added", "WebResource", $"{local.Name} to solution ContosoCore"),
            ("Published", "WebResource", local.Name),
            ("Published", "WebResource", updatedLocal.Name)
        ]);
    }

    [Test]
    public async Task ExecuteAsync_LeavesUnchangedResourceUntouched()
    {
        var operations = new List<string>();
        var repository = new RecordingWebResourceRepository(operations);
        var solutionRepository = new RecordingSolutionRepository(operations);
        var executor = new WebResourcePushExecutor(repository, solutionRepository);
        var local = CreateLocal("contoso_/main.js");
        var plan = new WebResourcePushPlan(
            [new WebResourcePlanItem(
                local,
                WebResourceAction.Unchanged,
                new RemoteWebResource(Guid.NewGuid(), local.Type, local.Name, local.Content, false),
                false)],
            [],
            null);

        var operationCount = await executor.ExecuteAsync(plan);

        await Assert.That(operationCount).IsZero();
        await Assert.That(repository.Created).IsEmpty();
        await Assert.That(repository.PublishBatches).IsEmpty();
        await Assert.That(solutionRepository.Added).IsEmpty();
    }

    private static LocalWebResource CreateLocal(string name) =>
        new(3, name, Path.GetFileName(name), Convert.ToBase64String("content"u8.ToArray()), "hash", name);

    private sealed class RecordingWebResourceRepository(List<string> operations) : IWebResourceRepository
    {
        public Guid CreatedId { get; } = Guid.NewGuid();
        public List<string> Created { get; } = [];
        public List<(string Name, Guid Id)> Updated { get; } = [];
        public List<IReadOnlyCollection<Guid>> PublishBatches { get; } = [];

        public Task<IReadOnlyList<RemoteWebResource>> FindByNamesAsync(
            IReadOnlyCollection<string> names,
            CancellationToken cancellationToken = default) =>
            Task.FromResult<IReadOnlyList<RemoteWebResource>>([]);

        public Task<Guid> CreateAsync(LocalWebResource resource, CancellationToken cancellationToken = default)
        {
            operations.Add("Create");
            Created.Add(resource.Name);
            return Task.FromResult(CreatedId);
        }

        public Task UpdateAsync(LocalWebResource resource, Guid id, CancellationToken cancellationToken = default)
        {
            operations.Add("Update");
            Updated.Add((resource.Name, id));
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            operations.Add("Delete");
            return Task.CompletedTask;
        }

        public Task PublishAsync(IReadOnlyCollection<Guid> ids, CancellationToken cancellationToken = default)
        {
            operations.Add("Publish");
            PublishBatches.Add(ids);
            return Task.CompletedTask;
        }
    }

    private sealed class RecordingSolutionRepository(List<string> operations) : ISolutionRepository
    {
        public List<(Guid Id, string Solution)> Added { get; } = [];

        public Task<IReadOnlyList<RemoteSolutionWebResource>> ListWebResourcesAsync(
            string solutionUniqueName,
            CancellationToken cancellationToken = default) =>
            Task.FromResult<IReadOnlyList<RemoteSolutionWebResource>>([]);

        public Task AddWebResourceAsync(
            Guid webresourceId,
            string solutionUniqueName,
            CancellationToken cancellationToken = default)
        {
            operations.Add("Add");
            Added.Add((webresourceId, solutionUniqueName));
            return Task.CompletedTask;
        }
    }
}
