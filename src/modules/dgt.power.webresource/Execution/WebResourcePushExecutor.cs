// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.webresource.Local;
using dgt.power.webresource.Output;
using dgt.power.webresource.Planning;
using dgt.power.webresource.Remote;
using dgt.power.webresource.Repositories;

namespace dgt.power.webresource.Execution;

public sealed class WebResourcePushExecutor(
    IWebResourceRepository webResourceRepository,
    ISolutionRepository solutionRepository,
    WebResourcePlanRenderer planRenderer,
    WebResourceExecutionReporter executionReporter)
{
    public Task ExecuteAsync(
        IReadOnlyList<LocalWebResource> local,
        WebResourcePushOptions options,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(local);
        ArgumentNullException.ThrowIfNull(options);
        return ExecuteCoreAsync(local, options, cancellationToken);
    }

    private async Task ExecuteCoreAsync(
        IReadOnlyList<LocalWebResource> local,
        WebResourcePushOptions options,
        CancellationToken cancellationToken)
    {
        var remote = await webResourceRepository.FindByNamesAsync(
            local.Select(resource => resource.Name).ToArray(),
            cancellationToken);
        IReadOnlyList<RemoteSolutionWebResource>? solutionResources = null;
        if (options.Solution is not null)
        {
            solutionResources = await solutionRepository.ListWebResourcesAsync(
                options.Solution,
                cancellationToken);
        }

        var plan = WebResourcePushPlanner.Plan(
            local,
            remote,
            solutionResources,
            options.DeleteObsolete);
        var solutionIds = solutionResources?.Select(resource => resource.Id).ToHashSet() ?? [];

        planRenderer.Render(plan);
        if (options.DryRun)
        {
            return;
        }

        executionReporter.ReportSeparator();

        foreach (var item in plan.Resources.Where(item => item.Action != WebResourceAction.Keep))
        {
            await ApplyResourceAsync(item, options, solutionIds, cancellationToken);
        }

        foreach (var item in plan.Resources.Where(item => item.Action == WebResourceAction.Keep))
        {
            if (options.Solution is not null && item.Remote is not null && !solutionIds.Contains(item.Remote.Id))
            {
                await AddToSolutionAsync(item.Remote.Id, item.Local.Name, options, solutionIds, cancellationToken);
            }
        }

        foreach (var obsolete in plan.Obsolete)
        {
            await executionReporter.RunAsync(
                "Deleting obsolete WebResource",
                obsolete.Name,
                () => webResourceRepository.DeleteAsync(obsolete.Id, cancellationToken));
            executionReporter.ReportCompleted("Deleted", obsolete.Name);
        }
    }

    private async Task ApplyResourceAsync(
        WebResourcePlanItem item,
        WebResourcePushOptions options,
        HashSet<Guid> solutionIds,
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

        if (options.Solution is not null)
        {
            await AddToSolutionAsync(id, item.Local.Name, options, solutionIds, cancellationToken);
        }

        if (options.Publish && (item.Action == WebResourceAction.Create || item.Action == WebResourceAction.Update))
        {
            await executionReporter.RunAsync(
                "Publishing WebResource",
                item.Local.Name,
                () => webResourceRepository.PublishAsync(id, cancellationToken));
            executionReporter.ReportCompleted("Published", item.Local.Name);
        }
    }

    private async Task AddToSolutionAsync(
        Guid id,
        string resourceName,
        WebResourcePushOptions options,
        HashSet<Guid> solutionIds,
        CancellationToken cancellationToken)
    {
        if (options.Solution is null || solutionIds.Contains(id))
        {
            return;
        }

        await executionReporter.RunAsync(
            "Adding WebResource to solution",
            resourceName,
            () => solutionRepository.AddWebResourceAsync(id, options.Solution, cancellationToken));
        executionReporter.ReportAddedToSolution(resourceName, options.Solution);
        solutionIds.Add(id);
    }
}
