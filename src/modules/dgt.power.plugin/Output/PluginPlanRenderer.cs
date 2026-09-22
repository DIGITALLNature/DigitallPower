// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Planning;
using Spectre.Console;

namespace dgt.power.plugin.Output;

public sealed class PluginPlanRenderer(IAnsiConsole console)
{
    public void Render(PluginDeploymentPlan plan)
    {
        ArgumentNullException.ThrowIfNull(plan);

        switch (plan)
        {
            case AssemblyDeploymentPlan assembly:
                console.Write(BuildAssemblyTree(assembly));
                break;

            case PackageDeploymentPlan package:
                console.Write(BuildPackageTree(package));
                break;

            default:
                throw new ArgumentOutOfRangeException(
                    nameof(plan),
                    plan.GetType(),
                    "Unknown plugin deployment plan type.");
        }
    }

    private static Tree BuildPackageTree(PackageDeploymentPlan plan)
    {
        var name = plan.DataverseName;
        var tree = new Tree(
            $"{Emoji.Known.Package} [bold]{Markup.Escape(name)}[/] {ActionMarkup(plan.PackageAction.Action.ToString())}");
        foreach (var assembly in plan.Assemblies)
        {
            var node = tree.AddNode(AssemblyLabel(assembly));
            AddAssemblyChildren(node, assembly);
        }

        if (plan.LinkManagedIdentity)
        {
            tree.AddNode($"Managed identity {ActionMarkup("Link")}");
        }

        AddSolutionNode(tree, plan.Solution);
        return tree;
    }

    private static Tree BuildAssemblyTree(AssemblyDeploymentPlan plan)
    {
        var tree = new Tree(AssemblyLabel(plan));
        AddAssemblyChildren(tree, plan);
        return tree;
    }

    private static string AssemblyLabel(AssemblyDeploymentPlan plan) =>
        $"{Emoji.Known.PuzzlePiece} [bold]{Markup.Escape(plan.Assembly.Local.Name)}[/] " +
        ActionMarkup(plan.Assembly.Action.ToString());

    private static void AddAssemblyChildren(IHasTreeNodes parent, AssemblyDeploymentPlan plan)
    {
        if (plan.PluginTypes is not null)
        {
            AddPluginTypes(parent, plan.PluginTypes);
        }

        if (plan.OutdatedAssemblies.Assemblies.Count > 0)
        {
            var outdatedRoot = parent.AddNode($"Outdated assemblies {ActionMarkup("Plan")}");
            foreach (var assembly in plan.OutdatedAssemblies.Assemblies)
            {
                var assemblyNode = outdatedRoot.AddNode(
                    $"{assembly.Assembly.Id} {ActionMarkup("Purge")}");
                foreach (var type in assembly.Types)
                {
                    var typeNode = assemblyNode.AddNode(
                        $"{Markup.Escape(type.Migration.TypeName)} " +
                        ActionMarkup(type.Migration.HasReplacement ? "Migrate" : "Delete"));
                    foreach (var customApiId in type.CustomApiIds)
                    {
                        typeNode.AddNode($"Custom API {customApiId} {ActionMarkup("Migrate")}");
                    }

                    foreach (var stepId in type.MigrateStepIds)
                    {
                        typeNode.AddNode($"Step {stepId} {ActionMarkup("Migrate")}");
                    }

                    foreach (var stepId in type.DeleteStepIds)
                    {
                        typeNode.AddNode($"Step {stepId} {ActionMarkup("Delete")}");
                    }
                }
            }
        }

        if (plan.LinkManagedIdentity)
        {
            parent.AddNode($"Managed identity {ActionMarkup("Link")}");
        }

        AddSolutionNode(parent, plan.Solution);
    }

    private static void AddPluginTypes(IHasTreeNodes parent, PluginTypeDeploymentPlan plan)
    {
        foreach (var type in plan.Types)
        {
            var typeNode = parent.AddNode(
                $"{Markup.Escape(type.Type.Local.TypeName)} {ActionMarkup(type.Type.Action.ToString())}");
            foreach (var step in type.Steps)
            {
                var stepNode = typeNode.AddNode(
                    $"{Markup.Escape(step.Step.Local.Name)} {ActionMarkup(step.Step.Action.ToString())}");
                foreach (var image in step.Images.Plans)
                {
                    stepNode.AddNode(
                        $"{Markup.Escape(image.Local.Name)} {ActionMarkup(image.Action.ToString())}");
                }

                foreach (var image in step.Images.Purge)
                {
                    stepNode.AddNode($"{Markup.Escape(image.Name)} {ActionMarkup("Delete")}");
                }

                foreach (var image in step.UnchangedImages)
                {
                    stepNode.AddNode($"{Markup.Escape(image.Name)} {ActionMarkup("Unchanged")}");
                }
            }

            foreach (var step in type.DeleteSteps)
            {
                typeNode.AddNode($"{Markup.Escape(step.Step.Name)} {ActionMarkup("Delete")}");
            }

            foreach (var customApiId in type.CustomApi.UnlinkIds)
            {
                typeNode.AddNode($"Custom API link {customApiId} {ActionMarkup("Unlink")}");
            }

            if (type.CustomApi.Link)
            {
                typeNode.AddNode(
                    $"Custom API {Markup.Escape(type.CustomApi.UniqueName)} {ActionMarkup("Link")}");
            }
            else if (type.CustomApi.Unchanged)
            {
                typeNode.AddNode(
                    $"Custom API {Markup.Escape(type.CustomApi.UniqueName)} {ActionMarkup("Unchanged")}");
            }
        }

        foreach (var type in plan.Delete)
        {
            var typeNode = parent.AddNode(
                $"{Markup.Escape(type.Type.TypeName)} {ActionMarkup("Delete")}");
            foreach (var stepId in type.DependentStepIds)
            {
                typeNode.AddNode($"Step {stepId} {ActionMarkup("Delete")}");
            }
        }
    }

    private static void AddSolutionNode(IHasTreeNodes parent, SolutionLinkPlan? solution)
    {
        if (solution is not null)
        {
            parent.AddNode(
                $"Solution {Markup.Escape(solution.SolutionUniqueName)} {ActionMarkup("Link")}");
        }
    }

    private static string ActionMarkup(string action) => action switch
    {
        "Create" or "Link" or "Migrate" => $"[green]{action}[/]",
        "Update" => "[blue]Update[/]",
        "Unchanged" => "[grey]Unchanged[/]",
        "OwnedByPackage" => "[grey]Managed by package[/]",
        "Delete" or "Unlink" or "Purge" => $"[red]{action}[/]",
        _ => $"[yellow]{Markup.Escape(action)}[/]"
    };
}
