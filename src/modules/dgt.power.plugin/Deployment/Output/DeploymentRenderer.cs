// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Deployment.Local;
using dgt.power.plugin.Deployment.Reconciliation;
using Spectre.Console;

namespace dgt.power.plugin.Deployment.Output;

internal sealed class DeploymentRenderer(IAnsiConsole console)
{
    internal void Render(StandaloneDeploymentChanges changes)
    {
        ArgumentNullException.ThrowIfNull(changes);
        var registrations = changes.Registrations;
        var artifactAction = Action(changes.ExistingAssemblyId, changes.UpdateContent);
        var tree = new Tree($"{Markup.Escape(changes.Desired.Identity)} [bold]{artifactAction}[/]");
        AddRegistrations(tree, changes.Desired, registrations, packageUpload: false);
        AddCustomApis(tree, [changes.Desired], changes.CustomApis);
        if (changes.AssemblyToDelete is { } oldAssembly)
        {
            tree.AddNode($"Superseded assembly {Markup.Escape(oldAssembly.Identity)} [red]Delete[/]");
        }

        console.Write(tree);
        var deletionCount = registrations.ImagesToDelete.Count + registrations.StepsToDelete.Count + registrations.TypesToDelete.Count +
            changes.CustomApis.Count(api => api.PreviousHandlerId is not null);
        if (changes.AssemblyToDelete is not null)
        {
            deletionCount++;
        }
        if (deletionCount > 0)
        {
            console.MarkupLine($"[bold red]Destructive operations: {deletionCount}[/]");
        }
    }

    internal void RenderPackage(PackageDeploymentChanges changes)
    {
        ArgumentNullException.ThrowIfNull(changes);
        var upload = changes.Existing is null || changes.UpdateContent;
        var tree = new Tree($"{Markup.Escape(changes.DataverseName)} [bold]{Action(changes.Existing?.Id, changes.UpdateContent)}[/]");
        foreach (var assembly in changes.Assemblies)
        {
            var action = upload ? "Managed by package upload" : "Unchanged";
            if (assembly.Existing is null)
            {
                action = "Create via package upload";
            }
            var node = tree.AddNode($"{Markup.Escape(assembly.Desired.Identity)} [bold]{action}[/]");
            AddRegistrations(node, assembly.Desired, assembly.Registrations, packageUpload: true);
        }

        AddCustomApis(tree, changes.Desired.Assemblies, changes.CustomApis);
        console.Write(tree);
        var destructiveCount = changes.CustomApis.Count(api => api.PreviousHandlerId is not null);
        if (destructiveCount > 0)
        {
            console.MarkupLine($"[bold red]Destructive operations: {destructiveCount}[/]");
        }
        if (upload)
        {
            console.MarkupLine("Assembly and plugin type records are managed by Dataverse during package upload; their IDs are validated before registration writes.");
        }
    }

    private static void AddCustomApis(IHasTreeNodes parent, IReadOnlyList<LocalAssembly> desired, IReadOnlyList<CustomApiChange> changes)
    {
        foreach (var assembly in desired)
        {
            foreach (var type in assembly.PluginTypes.Where(type => type.CustomApi.Length > 0))
            {
                var handler = new PluginTypeReference(assembly.Name, type.TypeName);
                var change = changes.SingleOrDefault(api => api.Handler == handler);
                var action = "Unchanged";
                if (change is not null)
                {
                    action = change.PreviousHandlerId is null ? "Link" : $"Reassign from {change.PreviousHandlerId}";
                }

                parent.AddNode($"Custom API {Markup.Escape(type.CustomApi)} [bold]{action}[/] to " +
                    $"{Markup.Escape(assembly.Name)} / {Markup.Escape(type.TypeName)}");
            }
        }

        foreach (var change in changes.Where(api => api.Handler is null))
        {
            parent.AddNode($"Custom API {Markup.Escape(change.Name)} [red]Unlink[/]");
        }
    }

    private static void AddRegistrations(IHasTreeNodes parent, LocalAssembly desired, RegistrationChanges registrations, bool packageUpload)
    {
        foreach (var type in desired.PluginTypes)
        {
            var typeAction = "Unchanged";
            if (registrations.TypesToCreate.Contains(type))
            {
                typeAction = packageUpload ? "Create via package upload" : "Create";
            }

            var typeNode = parent.AddNode($"{Markup.Escape(type.TypeName)} [bold]{typeAction}[/]");
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
            parent.AddNode($"Step image {Markup.Escape(image.Name)} [red]Delete[/]");
        }

        foreach (var step in registrations.StepsToDelete)
        {
            parent.AddNode($"Step {Markup.Escape(step.Name)} [red]Delete[/]");
        }

        foreach (var type in registrations.TypesToDelete)
        {
            parent.AddNode($"Plugin type {Markup.Escape(type.TypeName)} [red]Delete[/]");
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
