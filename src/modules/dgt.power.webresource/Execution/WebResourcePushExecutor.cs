// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

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
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(plan);
        return ExecuteCoreAsync(plan, cancellationToken);
    }

    private async Task<int> ExecuteCoreAsync(WebResourcePushPlan plan, CancellationToken cancellationToken)
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
            await executionReporter.RunAsync(
                "Publishing WebResources",
                $"{ids.Length} resource(s)",
                () => webResourceRepository.PublishAsync(ids, cancellationToken));
            executionReporter.ReportPublished(ids.Length);
            completedOperationCount++;
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
