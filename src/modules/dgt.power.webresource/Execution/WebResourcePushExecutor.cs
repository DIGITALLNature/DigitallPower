// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using dgt.power.webresource.Output;
using dgt.power.webresource.Planning;
using dgt.power.webresource.Repositories;

namespace dgt.power.webresource.Execution;

public sealed class WebResourcePushExecutor(
    IWebResourceRepository webResourceRepository,
    ISolutionRepository solutionRepository,
    WebResourceExecutionReporter executionReporter)
{
    public Task<int> ExecuteAsync(
        WebResourcePushPlan plan,
        WebResourcePublishMode publishMode,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(plan);
        return ExecuteCoreAsync(plan, publishMode, cancellationToken);
    }

    private async Task<int> ExecuteCoreAsync(
        WebResourcePushPlan plan,
        WebResourcePublishMode publishMode,
        CancellationToken cancellationToken)
    {
        var completedOperationCount = 0;
        var changedResources = new List<(WebResourcePlanItem Item, Guid Id)>();

        foreach (var item in plan.Resources.Where(item => item.Action != WebResourceAction.Unchanged))
        {
            var id = await ApplyResourceAsync(item, cancellationToken);
            changedResources.Add((item, id));
            completedOperationCount++;
        }

        foreach (var (item, id) in changedResources.Where(resource => resource.Item.AddToSolution))
        {
            await AddToSolutionAsync(id, item.Local.Name, plan.SolutionUniqueName!, cancellationToken);
            completedOperationCount++;
        }

        foreach (var item in plan.Resources.Where(item => item.Action == WebResourceAction.Unchanged && item.AddToSolution))
        {
            await AddToSolutionAsync(item.Remote!.Id, item.Local.Name, plan.SolutionUniqueName!, cancellationToken);
            completedOperationCount++;
        }

        if (changedResources.Count > 0)
        {
            var ids = changedResources.Select(resource => resource.Id).ToArray();
            completedOperationCount += await PublishAsync(ids, publishMode, cancellationToken);
        }

        foreach (var obsolete in plan.Obsolete)
        {
            await executionReporter.RunAsync(
                "Deleting obsolete WebResource",
                obsolete.Name,
                () => webResourceRepository.DeleteAsync(obsolete.Id, cancellationToken));
            executionReporter.ReportCompleted("Deleted", obsolete.Name);
            completedOperationCount++;
        }

        return completedOperationCount;
    }

    private async Task<int> PublishAsync(
        IReadOnlyList<Guid> ids,
        WebResourcePublishMode publishMode,
        CancellationToken cancellationToken)
    {
        switch (publishMode)
        {
            case WebResourcePublishMode.Batch:
                await PublishBatchAsync(ids, cancellationToken);
                return 1;

            case WebResourcePublishMode.Single:
                var totalElapsed = TimeSpan.Zero;
                foreach (var id in ids)
                {
                    totalElapsed += await PublishBatchAsync([id], cancellationToken);
                }

                executionReporter.ReportPublishTotal(ids.Count, totalElapsed);
                return ids.Count;

            default:
                throw new ArgumentOutOfRangeException(
                    nameof(publishMode),
                    publishMode,
                    "Unknown webresource publish mode.");
        }
    }

    private async Task<TimeSpan> PublishBatchAsync(
        IReadOnlyCollection<Guid> ids,
        CancellationToken cancellationToken)
    {
        var stopwatch = Stopwatch.StartNew();
        await executionReporter.RunAsync(
            ids.Count == 1 ? "Publishing WebResource" : "Publishing WebResources",
            $"{ids.Count} resource(s)",
            () => webResourceRepository.PublishAsync(ids, cancellationToken));
        stopwatch.Stop();
        executionReporter.ReportPublished(ids.Count, stopwatch.Elapsed);
        return stopwatch.Elapsed;
    }

    private async Task<Guid> ApplyResourceAsync(
        WebResourcePlanItem item,
        CancellationToken cancellationToken)
    {
        Guid id;
        if (item.Action == WebResourceAction.Create)
        {
            id = Guid.Empty;
            await executionReporter.RunAsync(
                "Creating WebResource",
                item.Local.Name,
                async () => id = await webResourceRepository.CreateAsync(item.Local, cancellationToken));
            executionReporter.ReportCompleted("Created", item.Local.Name);
        }
        else
        {
            id = item.Remote!.Id;
            await executionReporter.RunAsync(
                "Updating WebResource",
                item.Local.Name,
                () => webResourceRepository.UpdateAsync(item.Local, id, cancellationToken));
            executionReporter.ReportCompleted("Updated", item.Local.Name);
        }

        return id;
    }

    private async Task AddToSolutionAsync(
        Guid id,
        string resourceName,
        string solutionUniqueName,
        CancellationToken cancellationToken)
    {
        await executionReporter.RunAsync(
            "Adding WebResource to solution",
            resourceName,
            () => solutionRepository.AddWebResourceAsync(id, solutionUniqueName, cancellationToken));
        executionReporter.ReportAddedToSolution(resourceName, solutionUniqueName);
    }
}
