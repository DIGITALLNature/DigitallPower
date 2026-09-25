// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Planning.Comparison;
using dgt.power.plugin.Planning.Deployment;
using dgt.power.plugin.Repositories;
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
                RenderOutdatedAssemblyLifecycle(assembly.OutdatedAssemblies);
                RenderSolutionMembership(assembly.SolutionMembership, CollectSolutionLinks(assembly));
                break;

            case PackageDeploymentPlan package:
                console.Write(BuildPackageTree(package));
                foreach (var assemblyPlan in package.Assemblies)
                {
                    RenderOutdatedAssemblyLifecycle(assemblyPlan.OutdatedAssemblies);
                }

                RenderSolutionMembership(package.SolutionMembership, CollectSolutionLinks(package));
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
            $"{Emoji.Known.Package} [bold]{Markup.Escape(name)}[/] {ActionMarkup(PackageStatus(plan.Comparison))}");
        foreach (var assembly in plan.Assemblies)
        {
            var node = tree.AddNode(AssemblyLabel(assembly));
            AddAssemblyChildren(node, assembly);
        }

        if (plan.LinkManagedIdentity)
        {
            tree.AddNode($"Managed identity {ActionMarkup("Link")}");
        }

        return tree;
    }

    private static Tree BuildAssemblyTree(AssemblyDeploymentPlan plan)
    {
        var tree = new Tree(AssemblyLabel(plan));
        AddAssemblyChildren(tree, plan);
        return tree;
    }

    private static string AssemblyLabel(AssemblyDeploymentPlan plan)
    {
        var comparison = plan.Comparison;
        var version = comparison.IsPackageOwned ? string.Empty : $" v{comparison.Local.Version}";
        return $"{Emoji.Known.PuzzlePiece} [bold]{Markup.Escape(comparison.Local.Name)}[/]{version}" +
               AssemblyStatusMarkup(comparison);
    }

    private static string AssemblyStatusMarkup(AssemblyComparison comparison)
    {
        if (comparison.IsPackageOwned)
        {
            return string.Empty;
        }

        return $" {ActionMarkup(AssemblyStatus(comparison))}";
    }

    private static string AssemblyStatus(AssemblyComparison comparison)
    {
        if (comparison.RequiresCreate)
        {
            return "Create";
        }

        if (comparison.RequiresUpgrade)
        {
            return "Upgrade";
        }

        return comparison.RequiresUpdate ? "Update" : "Unchanged";
    }

    private static string PackageStatus(PackageComparison comparison)
    {
        if (comparison.RequiresCreate)
        {
            return "Create";
        }

        return comparison.RequiresUpdate ? "Update" : "Unchanged";
    }

    private static string StepStatus(PluginStepComparison comparison)
    {
        if (comparison.RequiresCreate)
        {
            return "Create";
        }

        return comparison.RequiresUpdate ? "Update" : "Unchanged";
    }

    private static string ImageStatus(PluginStepImageComparison comparison)
    {
        if (comparison.RequiresCreate)
        {
            return "Create";
        }

        return comparison.RequiresUpdate ? "Update" : "Unchanged";
    }

    private static void AddAssemblyChildren(IHasTreeNodes parent, AssemblyDeploymentPlan plan)
    {
        if (plan.PluginTypes is not null)
        {
            AddPluginTypes(parent, plan.PluginTypes);
        }

        if (plan.LinkManagedIdentity)
        {
            parent.AddNode($"Managed identity {ActionMarkup("Link")}");
        }

    }

    private static void AddPluginTypes(IHasTreeNodes parent, PluginTypeDeployment plan)
    {
        foreach (var type in plan.Types)
        {
            var typeNode = parent.AddNode(
                $"{Markup.Escape(type.Comparison.Local.TypeName)} {ActionMarkup(type.Comparison.RequiresCreate ? "Create" : "Unchanged")}");
            foreach (var step in type.Steps)
            {
                var stepNode = typeNode.AddNode(
                    $"{Markup.Escape(step.Comparison.Local.Name)} {ActionMarkup(step.MigrationSource is null ? StepStatus(step.Comparison) : "Migrate")}");
                foreach (var image in step.Images.Comparisons)
                {
                    stepNode.AddNode(
                        $"{Markup.Escape(image.Local.Name)} {ActionMarkup(ImageStatus(image))}");
                }

                foreach (var image in step.Images.Deletions)
                {
                    stepNode.AddNode($"{Markup.Escape(image.Name)} {ActionMarkup("Delete")}");
                }

                foreach (var image in step.UnchangedImages)
                {
                    stepNode.AddNode($"{Markup.Escape(image.Name)} {ActionMarkup("Unchanged")}");
                }
            }

            foreach (var step in type.StepDeletions)
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

        foreach (var type in plan.Deletions)
        {
            var typeNode = parent.AddNode(
                $"{Markup.Escape(type.Type.TypeName)} {ActionMarkup("Delete")}");
            foreach (var stepId in type.DependentStepIds)
            {
                typeNode.AddNode($"Step {stepId} {ActionMarkup("Delete")}");
            }
        }
    }

    private void RenderSolutionMembership(
        SolutionMembershipPlan? membershipPlan,
        IEnumerable<SolutionLink> links)
    {
        if (membershipPlan is null)
        {
            return;
        }

        var memberships = links
            .DistinctBy(link => (link.ComponentType, link.ComponentName, link.SolutionUniqueName))
            .ToList();

        console.MarkupLine(
            $"[bold]Solution membership: {Markup.Escape(membershipPlan.SolutionUniqueName)}[/]");
        if (memberships.Count == 0)
        {
            console.MarkupLine($"  [green]{Emoji.Known.CheckMark}[/] All managed components are already present");
            return;
        }

        foreach (var membership in memberships)
        {
            console.MarkupLine(
                $"  [green]{Emoji.Known.Plus}[/] {ComponentLabel(membership.ComponentType)} {Markup.Escape(membership.ComponentName)}");
        }
    }

    private void RenderOutdatedAssemblyLifecycle(OutdatedAssemblyDeployment outdatedAssemblies)
    {
        foreach (var assembly in outdatedAssemblies.Assemblies)
        {
            console.MarkupLine(
                $"[red]{Emoji.Known.Minus} Outdated assembly {Markup.Escape(assembly.Assembly.Identity)} will be deleted[/]");
        }
    }

    private static IEnumerable<SolutionLink> CollectSolutionLinks(AssemblyDeploymentPlan plan)
    {
        if (plan.Solution is not null)
        {
            yield return plan.Solution;
        }

        if (plan.PluginTypes is null)
        {
            yield break;
        }

        foreach (var type in plan.PluginTypes.Types)
        {
            foreach (var step in type.Steps)
            {
                if (step.Solution is not null)
                {
                    yield return step.Solution;
                }
            }
        }
    }

    private static IEnumerable<SolutionLink> CollectSolutionLinks(PackageDeploymentPlan plan)
    {
        if (plan.Solution is not null)
        {
            yield return plan.Solution;
        }

        foreach (var assembly in plan.Assemblies)
        {
            foreach (var link in CollectSolutionLinks(assembly))
            {
                yield return link;
            }
        }
    }

    private static string ComponentLabel(int componentType) => componentType switch
    {
        IPluginAssemblyRepository.ComponentType => "Assembly",
        ISdkMessageProcessingStepRepository.ComponentType => "Step",
        _ => "Package"
    };

    private static string ActionMarkup(string action) => action switch
    {
        "Create" or "Link" or "Migrate" => $"[green]{action}[/]",
        "Update" => "[blue]Update[/]",
        "Unchanged" => "[grey]Unchanged[/]",
        "Delete" or "Unlink" or "Purge" => $"[red]{action}[/]",
        _ => $"[yellow]{Markup.Escape(action)}[/]"
    };
}
