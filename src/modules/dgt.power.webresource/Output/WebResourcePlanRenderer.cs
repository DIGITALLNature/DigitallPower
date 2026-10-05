// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.webresource.Planning;
using Spectre.Console;

namespace dgt.power.webresource.Output;

public sealed class WebResourcePlanRenderer(IAnsiConsole console)
{
    public void Render(WebResourcePushPlan plan, string targetDirectory)
    {
        ArgumentNullException.ThrowIfNull(plan);
        ArgumentException.ThrowIfNullOrWhiteSpace(targetDirectory);

        var tree = new Tree($"[bold]{Markup.Escape(targetDirectory)}[/]");
        var directoryNodes = new Dictionary<string, IHasTreeNodes>(StringComparer.OrdinalIgnoreCase)
        {
            [string.Empty] = tree
        };

        foreach (var item in plan.Resources.OrderBy(item => item.Local.RelativePath, StringComparer.OrdinalIgnoreCase))
        {
            var pathParts = item.Local.RelativePath.Split('/', StringSplitOptions.RemoveEmptyEntries);
            var directoryPath = string.Empty;
            IHasTreeNodes parent = tree;
            for (var index = 0; index < pathParts.Length - 1; index++)
            {
                directoryPath = string.IsNullOrEmpty(directoryPath)
                    ? pathParts[index]
                    : $"{directoryPath}/{pathParts[index]}";
                if (!directoryNodes.TryGetValue(directoryPath, out var directoryNode))
                {
                    directoryNode = parent.AddNode(Markup.Escape(pathParts[index]));
                    directoryNodes[directoryPath] = directoryNode;
                }

                parent = directoryNode;
            }

            var action = item.Action switch
            {
                WebResourceAction.Create => "[green]Create[/]",
                WebResourceAction.Update => "[blue]Update[/]",
                _ => "[grey]Unchanged[/]"
            };
            parent.AddNode(
                $"{Markup.Escape(pathParts[^1])} [grey]→[/] " +
                $"{Markup.Escape(item.Local.Name)} {action}");
        }

        console.Write(tree);

        if (!string.IsNullOrWhiteSpace(plan.SolutionUniqueName))
        {
            RenderSolutionMembership(plan);
        }

        if (plan.Obsolete.Count > 0)
        {
            RenderObsoleteResources(plan);
        }
    }

    private void RenderSolutionMembership(WebResourcePushPlan plan)
    {
        var missingMembership = plan.Resources
            .Where(resource => resource.AddToSolution)
            .OrderBy(resource => resource.Local.Name, StringComparer.OrdinalIgnoreCase)
            .ToList();
        console.MarkupLine(
            $"[bold]Solution membership: {Markup.Escape(plan.SolutionUniqueName!)}[/]");
        if (missingMembership.Count == 0)
        {
            console.MarkupLine($"  [green]{Emoji.Known.CheckMark}[/] All managed components are already present");
            return;
        }

        foreach (var resource in missingMembership)
        {
            console.MarkupLine(
                $"  [green]{Emoji.Known.Plus}[/] WebResource {Markup.Escape(resource.Local.Name)}");
        }
    }

    private void RenderObsoleteResources(WebResourcePushPlan plan)
    {
        console.MarkupLine("[bold red]Obsolete webresources[/]");
        foreach (var obsolete in plan.Obsolete.OrderBy(resource => resource.Name, StringComparer.OrdinalIgnoreCase))
        {
            console.MarkupLine(
                $"  [red]{Emoji.Known.Minus}[/] WebResource {Markup.Escape(obsolete.Name)}");
        }
    }
}
