// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using dgt.power.webresource.Local;
using dgt.power.webresource.Planning;
using dgt.power.webresource.Remote;
using dgt.power.webresource.Repositories;
using Spectre.Console;

namespace dgt.power.webresource.Execution;

public sealed class WebResourcePushExecutor(IWebResourceRepository webResourceRepository, ISolutionRepository solutionRepository, IAnsiConsole console)
{
    public Task ExecuteAsync(IReadOnlyList<LocalWebResource> local, WebResourcePushOptions options, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(local);
        ArgumentNullException.ThrowIfNull(options);
        return ExecuteCoreAsync(local, options, cancellationToken);
    }

    private async Task ExecuteCoreAsync(IReadOnlyList<LocalWebResource> local, WebResourcePushOptions options, CancellationToken cancellationToken)
    {
        var remote = await webResourceRepository.FindByNamesAsync(local.Select(resource => resource.Name).ToArray(), cancellationToken);
        IReadOnlyList<RemoteSolutionWebResource>? solutionResources = null;
        if (options.Solution is not null)
        {
            solutionResources = await solutionRepository.ListWebResourcesAsync(options.Solution, cancellationToken);
        }

        var plan = WebResourcePushPlanner.Plan(local, remote, solutionResources, options.DeleteObsolete);
        var solutionIds = solutionResources?.Select(resource => resource.Id).ToHashSet() ?? [];

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
            else
            {
                console.MarkupLine(CultureInfo.InvariantCulture, "Keep WebResource: [green]{0}[/]", item.Local.Name);
            }
        }

        foreach (var obsolete in plan.Obsolete)
        {
            console.MarkupLine(CultureInfo.InvariantCulture, "Delete obsolete WebResource: [red]{0}[/]", obsolete.Name);
            if (!options.DryRun)
            {
                await webResourceRepository.DeleteAsync(obsolete.Id, cancellationToken);
            }
        }
    }

    private async Task ApplyResourceAsync(WebResourcePlanItem item, WebResourcePushOptions options, HashSet<Guid> solutionIds, CancellationToken cancellationToken)
    {
        Guid id;
        if (item.Action == WebResourceAction.Create)
        {
            console.MarkupLine(CultureInfo.InvariantCulture, "Create WebResource: [green]{0}[/]", item.Local.Name);
            id = options.DryRun ? Guid.Empty : await webResourceRepository.CreateAsync(item.Local, cancellationToken);
        }
        else
        {
            id = item.Remote!.Id;
            console.MarkupLine(CultureInfo.InvariantCulture, "Update WebResource: [green]{0}[/]", item.Local.Name);
            if (!options.DryRun)
            {
                await webResourceRepository.UpdateAsync(item.Local, id, cancellationToken);
            }
        }

        if (options.Solution is not null)
        {
            if (id == Guid.Empty)
            {
                console.MarkupLine(CultureInfo.InvariantCulture, "Add WebResource [green]{0}[/] to solution [green]{1}[/]", item.Local.Name, options.Solution);
            }
            else
            {
                await AddToSolutionAsync(id, item.Local.Name, options, solutionIds, cancellationToken);
            }
        }

        if (options.Publish && (item.Action == WebResourceAction.Create || item.Action == WebResourceAction.Update))
        {
            console.MarkupLine(CultureInfo.InvariantCulture, "Publish WebResource: [green]{0}[/]", item.Local.Name);
            if (!options.DryRun)
            {
                await webResourceRepository.PublishAsync(id, cancellationToken);
            }
        }
    }

    private async Task AddToSolutionAsync(Guid id, string resourceName, WebResourcePushOptions options, HashSet<Guid> solutionIds, CancellationToken cancellationToken)
    {
        if (options.Solution is null || solutionIds.Contains(id))
        {
            return;
        }

        console.MarkupLine(CultureInfo.InvariantCulture, "Add WebResource [green]{0}[/] to solution [green]{1}[/]", resourceName, options.Solution);
        if (!options.DryRun)
        {
            await solutionRepository.AddWebResourceAsync(id, options.Solution, cancellationToken);
        }

        solutionIds.Add(id);
    }
}
