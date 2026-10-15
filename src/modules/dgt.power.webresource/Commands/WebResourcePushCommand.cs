// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common;
using dgt.power.webresource.Execution;
using dgt.power.webresource.Local;
using dgt.power.webresource.Output;
using dgt.power.webresource.Planning;
using dgt.power.webresource.Repositories;
using Spectre.Console;

namespace dgt.power.webresource.Commands;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class WebResourcePushCommand(ICommandSpanFactory spans, IDataverseConnection connection, IAnsiConsole console)
    : PowerCommand<WebResourcePushSettings>(spans, console)
{
    protected override async Task<bool> RunAsync(WebResourcePushSettings settings, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(settings);

        if (settings.DryRun)
        {
            console.Write(new Panel("[yellow]No changes will be written to Dataverse.[/]")
            {
                Header = new PanelHeader("DRY RUN"), Border = BoxBorder.Rounded, BorderStyle = new Style(Color.Yellow), Padding = new Padding(1, 0, 1, 0)
            });
        }

        var mapping = settings.MappingFile is null ? null : WebResourceMappingFile.Load(settings.MappingFile);
        var local = WebResourceDiscovery.Discover(settings.Target, settings.PublisherPrefix, mapping?.Mappings, settings.Name);
        if (local.Count == 0)
        {
            console.MarkupLine($"[yellow]No supported webresources found in '{Markup.Escape(settings.Target)}'[/]");
            return true;
        }

        var service = await connection.GetServiceAsync();
        var webResourceRepository = new WebResourceRepository(service);
        var solutionRepository = new SolutionRepository(service);
        var solutionResources = settings.Solution is null
            ? null
            : await solutionRepository.ListWebResourcesAsync(settings.Solution, cancellationToken);
        var remote = await webResourceRepository.FindByNamesAsync(
            local.Select(resource => resource.Name).ToArray(),
            cancellationToken);
        var plan = WebResourcePushPlanner.Plan(
            local,
            remote,
            solutionResources,
            settings.DeleteObsolete,
            settings.Solution);

        console.MarkupLine("[bold blue]Plan[/]");
        var targetDirectory = Directory.Exists(settings.Target)
            ? Path.GetFullPath(settings.Target)
            : Path.GetDirectoryName(Path.GetFullPath(settings.Target))!;
        new WebResourcePlanRenderer(console).Render(plan, targetDirectory);
        if (settings.DryRun)
        {
            return true;
        }

        if (WebResourcePushConfirmation.ShouldPrompt(
                settings.Confirm,
                settings.NonInteractive,
                ExecutionEnvironment.IsCiAgent) &&
            WebResourcePushConfirmation.HasChanges(plan) &&
            !WebResourcePushConfirmation.Confirm(console, Path.GetFileName(Path.GetFullPath(settings.Target))))
        {
            console.MarkupLine("[yellow]Deployment cancelled.[/]");
            return true;
        }

        console.MarkupLine("[bold green]Execution[/]");
        var completedOperationCount = 0;
        var executionReporter = new WebResourceExecutionReporter(console);
        var executor = new WebResourcePushExecutor(webResourceRepository, solutionRepository);

        void ReportCompletedOperation(WebResourceDeploymentProgress progress)
        {
            completedOperationCount++;
            executionReporter.ReportCompleted(progress);
        }

        await console.Status()
            .Spinner(Spinner.Known.Dots)
            .SpinnerStyle(Style.Parse("green bold"))
            .StartAsync(
                "Applying deployment plan...",
                _ => executor.ExecuteAsync(plan, ReportCompletedOperation, cancellationToken));
        console.MarkupLine(completedOperationCount == 0
            ? $"[green]{Emoji.Known.CheckMark}[/] No changes applied"
            : $"[green]{Emoji.Known.CheckMark}[/] Deployment completed");

        return true;
    }
}
