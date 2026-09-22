// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.webresource.Planning;
using Spectre.Console;

namespace dgt.power.webresource.Output;

public sealed class WebResourcePlanRenderer(IAnsiConsole console)
{
    public void Render(WebResourcePushPlan plan)
    {
        ArgumentNullException.ThrowIfNull(plan);

        var tree = new Tree("[bold]WebResource deployment plan[/]");
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
                _ => "[grey]Keep[/]"
            };
            parent.AddNode(
                $"{Markup.Escape(pathParts[^1])} [grey]→[/] " +
                $"{Markup.Escape(item.Local.Name)} {action}");
        }

        console.Write(tree);

        if (plan.Obsolete.Count == 0)
        {
            return;
        }

        console.Write(new Rule("[red]Obsolete webresources[/]"));
        foreach (var obsolete in plan.Obsolete.OrderBy(resource => resource.Name, StringComparer.OrdinalIgnoreCase))
        {
            console.MarkupLine($"Delete: [red]{Markup.Escape(obsolete.Name)}[/]");
        }
    }
}
