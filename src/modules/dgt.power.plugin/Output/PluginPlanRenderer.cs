// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Planning;
using Spectre.Console;

namespace dgt.power.plugin.Output;

public sealed class PluginPlanRenderer(IAnsiConsole console)
{
    public void Render(PluginPlanNode root)
    {
        ArgumentNullException.ThrowIfNull(root);

        var tree = new Tree($"[bold]{FormatLabel(root)}[/]");
        AddChildren(tree, root.Children);
        console.Write(tree);
    }

    private static void AddChildren(IHasTreeNodes parent, IReadOnlyList<PluginPlanNode> children)
    {
        foreach (var child in children)
        {
            var node = parent.AddNode($"{FormatLabel(child)} {ActionMarkup(child.Action)}");
            AddChildren(node, child.Children);
        }
    }

    private static string FormatLabel(PluginPlanNode node) =>
        $"{node.Icon}{(node.Icon is null ? string.Empty : " ")}{Markup.Escape(node.Label)}";

    private static string ActionMarkup(string action) => action switch
    {
        "Create" or "Link" or "Migrate" => $"[green]{action}[/]",
        "Update" => "[blue]Update[/]",
        "Reconcile" => "[grey]Keep[/]",
        "Keep" => $"[grey]{action}[/]",
        "OwnedByPackage" => "[grey]Managed by package[/]",
        "Delete" or "Unlink" or "Purge" => $"[red]{action}[/]",
        _ => $"[yellow]{Markup.Escape(action)}[/]"
    };
}
