// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Deployment.Reconciliation;
using Spectre.Console;

namespace dgt.power.plugin.Deployment.Output;

internal sealed class StandaloneDeploymentRenderer(IAnsiConsole console)
{
    internal void Render(StandaloneDeploymentChanges changes)
    {
        ArgumentNullException.ThrowIfNull(changes);
        var registrations = changes.Registrations;
        var artifactAction = Action(changes.ExistingAssemblyId, changes.UpdateContent);
        var tree = new Tree($"{Markup.Escape(changes.Desired.Identity)} [bold]{artifactAction}[/]");
        foreach (var type in changes.Desired.PluginTypes)
        {
            var typeAction = registrations.TypesToCreate.Contains(type) ? "Create" : "Unchanged";
            var typeNode = tree.AddNode($"{Markup.Escape(type.TypeName)} [bold]{typeAction}[/]");
            foreach (var step in type.Steps)
            {
                var write = registrations.StepsToWrite.SingleOrDefault(item => item.TypeName == type.TypeName && item.Desired == step);
                var action = write is null ? "Unchanged" : Action(write.ExistingId, true);
                var stepNode = typeNode.AddNode($"{Markup.Escape(step.Name)} [bold]{action}[/]");
                foreach (var image in step.Images)
                {
                    var imageWrite = registrations.ImagesToWrite.SingleOrDefault(item => item.TypeName == type.TypeName && item.Step == step && item.Desired == image);
                    var imageAction = imageWrite is null ? "Unchanged" : Action(imageWrite.ExistingId, true);
                    stepNode.AddNode($"{Markup.Escape(image.Name)} [bold]{imageAction}[/]");
                }
            }
        }

        foreach (var image in registrations.ImagesToDelete)
        {
            tree.AddNode($"Step image {Markup.Escape(image.Name)} [red]Delete[/]");
        }

        foreach (var step in registrations.StepsToDelete)
        {
            tree.AddNode($"Step {Markup.Escape(step.Name)} [red]Delete[/]");
        }

        foreach (var type in registrations.TypesToDelete)
        {
            tree.AddNode($"Plugin type {Markup.Escape(type.TypeName)} [red]Delete[/]");
        }

        if (changes.AssemblyToDelete is { } oldAssembly)
        {
            tree.AddNode($"Superseded assembly {Markup.Escape(oldAssembly.Identity)} [red]Delete[/]");
        }

        console.Write(tree);
        var deletionCount = registrations.ImagesToDelete.Count + registrations.StepsToDelete.Count + registrations.TypesToDelete.Count;
        if (changes.AssemblyToDelete is not null)
        {
            deletionCount++;
        }
        if (deletionCount > 0)
        {
            console.MarkupLine($"[bold red]Destructive operations: {deletionCount}[/]");
        }
    }

    private static string Action(Guid? existingId, bool update)
    {
        if (existingId is null)
        {
            return "Create";
        }

        return update ? "Update" : "Unchanged";
    }
}
