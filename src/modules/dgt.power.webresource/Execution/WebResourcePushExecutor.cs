// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.webresource.Planning;
using dgt.power.webresource.Repositories;

namespace dgt.power.webresource.Execution;

public sealed class WebResourcePushExecutor(
    IWebResourceRepository webResourceRepository,
    ISolutionRepository solutionRepository)
{
    public Task<int> ExecuteAsync(
        WebResourcePushPlan plan,
        Action<WebResourceDeploymentProgress>? reportProgress = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(plan);
        return ExecuteCoreAsync(plan, reportProgress, cancellationToken);
    }

    private async Task<int> ExecuteCoreAsync(
        WebResourcePushPlan plan,
        Action<WebResourceDeploymentProgress>? reportProgress,
        CancellationToken cancellationToken)
    {
        var completedOperationCount = 0;
        var changedResources = new List<(WebResourcePlanItem Item, Guid Id)>();

        foreach (var item in plan.Resources.Where(item => item.Action != WebResourceAction.Unchanged))
        {
            var id = await ApplyResourceAsync(item, cancellationToken);
            changedResources.Add((item, id));
            Report(
                reportProgress,
                item.Action == WebResourceAction.Create ? "Created" : "Updated",
                "WebResource",
                item.Local.Name);
            completedOperationCount++;
        }

        foreach (var (item, id) in changedResources.Where(resource => resource.Item.AddToSolution))
        {
            await AddToSolutionAsync(id, plan.SolutionUniqueName!, cancellationToken);
            Report(reportProgress, "Added", "WebResource", $"{item.Local.Name} to solution {plan.SolutionUniqueName}");
            completedOperationCount++;
        }

        foreach (var item in plan.Resources.Where(item => item.Action == WebResourceAction.Unchanged && item.AddToSolution))
        {
            await AddToSolutionAsync(item.Remote!.Id, plan.SolutionUniqueName!, cancellationToken);
            Report(reportProgress, "Added", "WebResource", $"{item.Local.Name} to solution {plan.SolutionUniqueName}");
            completedOperationCount++;
        }

        if (changedResources.Count > 0)
        {
            completedOperationCount += await PublishAsync(changedResources, reportProgress, cancellationToken);
        }

        foreach (var obsolete in plan.Obsolete)
        {
            await webResourceRepository.DeleteAsync(obsolete.Id, cancellationToken);
            Report(reportProgress, "Deleted", "WebResource", obsolete.Name);
            completedOperationCount++;
        }

        return completedOperationCount;
    }

    private async Task<int> PublishAsync(
        List<(WebResourcePlanItem Item, Guid Id)> changedResources,
        Action<WebResourceDeploymentProgress>? reportProgress,
        CancellationToken cancellationToken)
    {
        foreach (var (item, id) in changedResources)
        {
            await webResourceRepository.PublishAsync([id], cancellationToken);
            Report(reportProgress, "Published", "WebResource", item.Local.Name);
        }

        return changedResources.Count;
    }

    private async Task<Guid> ApplyResourceAsync(
        WebResourcePlanItem item,
        CancellationToken cancellationToken)
    {
        Guid id;
        if (item.Action == WebResourceAction.Create)
        {
            id = await webResourceRepository.CreateAsync(item.Local, cancellationToken);
        }
        else
        {
            id = item.Remote!.Id;
            await webResourceRepository.UpdateAsync(item.Local, id, cancellationToken);
        }

        return id;
    }

    private Task AddToSolutionAsync(
        Guid id,
        string solutionUniqueName,
        CancellationToken cancellationToken) =>
        solutionRepository.AddWebResourceAsync(id, solutionUniqueName, cancellationToken);

    private static void Report(
        Action<WebResourceDeploymentProgress>? reportProgress,
        string operation,
        string resource,
        string name) =>
        reportProgress?.Invoke(new WebResourceDeploymentProgress(operation, resource, name));
}
