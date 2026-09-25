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
    public async Task ExecuteAsync_BatchesCreatedAndUpdatedResourcesForPublishing()
    {
        var repository = new RecordingWebResourceRepository();
        var solutionRepository = new RecordingSolutionRepository();
        using var console = new TestConsole();
        var executor = new WebResourcePushExecutor(
            repository,
            solutionRepository,
            new WebResourceExecutionReporter(console));
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

        var operationCount = await executor.ExecuteAsync(plan, WebResourcePublishMode.Batch);

        await Assert.That(operationCount).IsEqualTo(4);
        await Assert.That(repository.Created).IsEquivalentTo([local.Name]);
        await Assert.That(repository.Updated).IsEquivalentTo([(updatedLocal.Name, updatedId)]);
        await Assert.That(repository.PublishBatches).Count().IsEqualTo(1);
        await Assert.That(repository.PublishBatches[0]).IsEquivalentTo([repository.CreatedId, updatedId]);
        await Assert.That(solutionRepository.Added).IsEquivalentTo([(repository.CreatedId, "ContosoCore")]);
        await Assert.That(console.Output).Contains("Published 2 WebResource(s) in");
    }

    [Test]
    public async Task ExecuteAsync_LeavesUnchangedResourceUntouched()
    {
        var repository = new RecordingWebResourceRepository();
        var solutionRepository = new RecordingSolutionRepository();
        using var console = new TestConsole();
        var executor = new WebResourcePushExecutor(
            repository,
            solutionRepository,
            new WebResourceExecutionReporter(console));
        var local = CreateLocal("contoso_/main.js");
        var plan = new WebResourcePushPlan(
            [new WebResourcePlanItem(
                local,
                WebResourceAction.Unchanged,
                new RemoteWebResource(Guid.NewGuid(), local.Type, local.Name, local.Content, false),
                false)],
            [],
            null);

        var operationCount = await executor.ExecuteAsync(plan, WebResourcePublishMode.Batch);

        await Assert.That(operationCount).IsZero();
        await Assert.That(repository.Created).IsEmpty();
        await Assert.That(repository.PublishBatches).IsEmpty();
        await Assert.That(solutionRepository.Added).IsEmpty();
    }

    [Test]
    public async Task ExecuteAsync_PublishesEachChangedResourceInSingleMode()
    {
        var repository = new RecordingWebResourceRepository();
        var solutionRepository = new RecordingSolutionRepository();
        using var console = new TestConsole();
        var executor = new WebResourcePushExecutor(
            repository,
            solutionRepository,
            new WebResourceExecutionReporter(console));
        var first = CreateLocal("contoso_/first.js");
        var second = CreateLocal("contoso_/second.js");
        var firstId = Guid.NewGuid();
        var secondId = Guid.NewGuid();
        var plan = new WebResourcePushPlan(
            [
                new WebResourcePlanItem(
                    first,
                    WebResourceAction.Update,
                    new RemoteWebResource(firstId, first.Type, first.Name, "old", false),
                    false),
                new WebResourcePlanItem(
                    second,
                    WebResourceAction.Update,
                    new RemoteWebResource(secondId, second.Type, second.Name, "old", false),
                    false)
            ],
            [],
            null);

        var operationCount = await executor.ExecuteAsync(plan, WebResourcePublishMode.Single);

        await Assert.That(operationCount).IsEqualTo(4);
        await Assert.That(repository.PublishBatches).Count().IsEqualTo(2);
        await Assert.That(repository.PublishBatches[0]).Count().IsEqualTo(1);
        await Assert.That(repository.PublishBatches[1]).Count().IsEqualTo(1);
        await Assert.That(repository.PublishBatches.SelectMany(batch => batch)).IsEquivalentTo([firstId, secondId]);
        await Assert.That(console.Output).Contains("Total publish time for 2 WebResource(s)");
    }

    private static LocalWebResource CreateLocal(string name) =>
        new(3, name, Path.GetFileName(name), Convert.ToBase64String("content"u8.ToArray()), "hash", name);

    private sealed class RecordingWebResourceRepository : IWebResourceRepository
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
            Created.Add(resource.Name);
            return Task.FromResult(CreatedId);
        }

        public Task UpdateAsync(LocalWebResource resource, Guid id, CancellationToken cancellationToken = default)
        {
            Updated.Add((resource.Name, id));
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default) => Task.CompletedTask;

        public Task PublishAsync(IReadOnlyCollection<Guid> ids, CancellationToken cancellationToken = default)
        {
            PublishBatches.Add(ids);
            return Task.CompletedTask;
        }
    }

    private sealed class RecordingSolutionRepository : ISolutionRepository
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
            Added.Add((webresourceId, solutionUniqueName));
            return Task.CompletedTask;
        }
    }
}
