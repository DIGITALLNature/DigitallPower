// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Deployment.Local;
using dgt.power.plugin.Deployment.Reconciliation;
using Spectre.Console;

namespace dgt.power.plugin.Deployment.Output;

internal sealed class DeploymentRenderer(IAnsiConsole console)
{
    private const string Create = "Create";
    private const string Update = "Update";
    private const string Delete = "Delete";
    private const string Unchanged = "Unchanged";
    private const string Add = "Add";
    private const string Set = "Set";
    private const string Clear = "Clear";
    private const string Link = "Link";
    private const string Unlink = "Unlink";

    /// <summary>Marks an action with a trailing, colored marker so it stands out without altering the tree layout.</summary>
    internal static string Tag(string action)
    {
        if (action == Unchanged)
        {
            return $"[dim]= {action}[/]";
        }

        if (action is Create or Add or Link)
        {
            return $"[green]+ {action}[/]";
        }

        if (action is Delete or Unlink or Clear)
        {
            return $"[red]- {action}[/]";
        }

        return $"[yellow]~ {action}[/]";
    }

    internal static string UpToDate(string text) => $"[green]{Emoji.Known.CheckMark}[/] {text}";

    internal void Render(StandaloneDeploymentChanges changes)
    {
        ArgumentNullException.ThrowIfNull(changes);
        var registrations = changes.Registrations;
        var artifactAction = Action(changes.ExistingAssemblyId, changes.UpdateContent);
        var tree = new Tree($"{Emoji.Known.PuzzlePiece} {Markup.Escape(changes.Desired.Identity)} {Tag(artifactAction)}");
        AddRegistrations(tree, changes.Desired, registrations, packageUpload: false);
        AddCustomApis(tree, [changes.Desired], changes.CustomApis);
        if (changes.AssemblyToDelete is { } oldAssembly)
        {
            tree.AddNode($"{Emoji.Known.PuzzlePiece} Superseded assembly {Markup.Escape(oldAssembly.Identity)} {Tag(Delete)}");
        }

        console.Write(tree);
        if (changes.Solution is { } solution)
        {
            RenderMembershipHeading(solution.UniqueName);
            RenderProviderMembership(solution.Providers);
            if (!solution.HasChanges)
            {
                console.MarkupLine(UpToDate("All components are already in the solution"));
            }
            if (solution.AddAssembly)
            {
                console.MarkupLine($"Assembly {Markup.Escape(changes.Desired.Identity)} {Tag(Add)}");
            }
            foreach (var step in solution.Steps)
            {
                console.MarkupLine($"Step {Markup.Escape(step.Step.Name)} {Tag(Add)}");
            }
        }
        RenderProviders([changes.Desired], changes.Providers);
        if (changes.DestructiveCount > 0)
        {
            console.WriteLine();
            console.MarkupLine($"[bold red]Destructive operations: {changes.DestructiveCount}[/]");
        }
    }

    internal void RenderPackage(PackageDeploymentChanges changes)
    {
        ArgumentNullException.ThrowIfNull(changes);
        var tree = new Tree($"{Emoji.Known.Package} {Markup.Escape(changes.DataverseName)} {Tag(Action(changes.Existing?.Id, changes.UpdateContent))}");
        foreach (var assembly in changes.Assemblies)
        {
            var node = tree.AddNode($"{Emoji.Known.PuzzlePiece} {Markup.Escape(assembly.Desired.Identity)}");
            AddRegistrations(node, assembly.Desired, assembly.Registrations, packageUpload: true);
        }

        AddCustomApis(tree, changes.Desired.Assemblies, changes.CustomApis);
        console.Write(tree);
        if (changes.Solution is { } solution)
        {
            RenderMembershipHeading(solution.UniqueName);
            RenderProviderMembership(solution.Providers);
            if (!solution.HasChanges)
            {
                console.MarkupLine(UpToDate("All components are already in the solution"));
            }
            if (solution.AddPackage)
            {
                console.MarkupLine($"Package {Markup.Escape(changes.DataverseName)} {Tag(Add)}");
            }
            foreach (var step in solution.Steps)
            {
                console.MarkupLine($"Step {Markup.Escape(step.Step.Name)} {Tag(Add)}");
            }
        }
        RenderProviders(changes.Desired.Assemblies, changes.Providers);
        var destructiveCount = changes.DestructiveCount;
        if (destructiveCount > 0)
        {
            console.WriteLine();
            console.MarkupLine($"[bold red]Destructive operations: {destructiveCount}[/]");
        }
    }

    private void RenderMembershipHeading(string solutionName)
    {
        console.WriteLine();
        console.MarkupLine($"[bold]Solution membership: {Markup.Escape(solutionName)}[/]");
    }

    private void RenderProviderMembership(ProviderSolutionChanges? changes)
    {
        if (changes is null)
        {
            return;
        }
        foreach (var addition in changes.Additions)
        {
            console.MarkupLine($"Data provider {Markup.Escape(addition.DataSourceLogicalName)} {Tag(Add)}");
        }
    }

    private void RenderProviders(IReadOnlyList<LocalAssembly> desired, IReadOnlyList<DataProviderChange> changes)
    {
        var declarations = DataProviderRegistrationGrouper.Group(desired.SelectMany(assembly => assembly.PluginTypes).ToList());
        if (declarations.Count == 0 && changes.Count == 0)
        {
            return;
        }

        var tree = new Tree("Data providers");
        foreach (var provider in declarations)
        {
            var change = changes.SingleOrDefault(item => item.Desired?.DataSourceLogicalName == provider.DataSourceLogicalName);
            var action = change is null ? Unchanged : Action(change.Existing?.Id, true);
            var node = tree.AddNode($"{Markup.Escape(provider.ProviderName)} ({Markup.Escape(provider.DataSourceLogicalName)}) {Tag(action)}");
            if (change is not null)
            {
                AddHandlerChanges(node, change);
            }
        }

        foreach (var change in changes.Where(change => change.Desired is null))
        {
            var node = tree.AddNode($"{Markup.Escape(change.Name)} {Tag(Update)}");
            AddHandlerChanges(node, change);
        }

        console.WriteLine();
        console.Write(tree);

        static void AddHandlerChanges(IHasTreeNodes parent, DataProviderChange change)
        {
            foreach (var (operation, handler) in change.Handlers)
            {
                parent.AddNode(handler is null
                    ? $"{operation} {Tag(Clear)}"
                    : $"{operation} -> {Markup.Escape(handler.AssemblyName)} / {Markup.Escape(handler.TypeName)} {Tag(Set)}");
            }
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
                var action = Unchanged;
                if (change is not null)
                {
                    action = change.PreviousHandlerId is null ? Link : $"Reassign from {change.PreviousHandlerId}";
                }

                parent.AddNode($"Custom API {Markup.Escape(type.CustomApi)} -> " +
                    $"{Markup.Escape(assembly.Name)} / {Markup.Escape(type.TypeName)} {Tag(action)}");
            }
        }

        foreach (var change in changes.Where(api => api.Handler is null))
        {
            parent.AddNode($"Custom API {Markup.Escape(change.Name)} {Tag(Unlink)}");
        }
    }

    private static void AddRegistrations(IHasTreeNodes parent, LocalAssembly desired, RegistrationChanges registrations, bool packageUpload)
    {
        foreach (var type in desired.PluginTypes)
        {
            // Plugin types inside a package are created by the platform during upload, so they carry no action.
            var typeLabel = Markup.Escape(type.TypeName);
            if (!packageUpload)
            {
                typeLabel += $" {Tag(registrations.TypesToCreate.Contains(type) ? Create : Unchanged)}";
            }

            var typeNode = parent.AddNode(typeLabel);
            foreach (var step in type.Steps)
            {
                var write = registrations.StepsToWrite.SingleOrDefault(item => item.TypeName == type.TypeName && item.Desired == step);
                var action = write is null ? Unchanged : Action(write.ExistingId, true);
                var stepNode = typeNode.AddNode($"{Markup.Escape(step.Name)} {Tag(action)}");
                foreach (var image in step.Images)
                {
                    var imageWrite = registrations.ImagesToWrite.SingleOrDefault(item => item.TypeName == type.TypeName && item.Step == step && item.Desired == image);
                    var imageAction = imageWrite is null ? Unchanged : Action(imageWrite.ExistingId, true);
                    stepNode.AddNode($"{Markup.Escape(image.Name)} {Tag(imageAction)}");
                }
            }
        }

        foreach (var image in registrations.ImagesToDelete)
        {
            parent.AddNode($"Step image {Markup.Escape(image.Name)} {Tag(Delete)}");
        }

        foreach (var step in registrations.StepsToDelete)
        {
            parent.AddNode($"Step {Markup.Escape(step.Name)} {Tag(Delete)}");
        }

        foreach (var type in registrations.TypesToDelete)
        {
            parent.AddNode($"Plugin type {Markup.Escape(type.TypeName)} {Tag(Delete)}");
        }
    }

    private static string Action(Guid? existingId, bool update)
    {
        if (existingId is null)
        {
            return Create;
        }

        return update ? Update : Unchanged;
    }
}
