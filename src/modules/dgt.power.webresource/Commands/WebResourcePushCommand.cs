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
        if (settings.DryRun)
        {
            DryRunBanner.Write(Console);
        }

        var mapping = settings.MappingFile is null ? null : WebResourceMappingFile.Load(settings.MappingFile);
        var local = WebResourceDiscovery.Discover(settings.Target, settings.PublisherPrefix, mapping?.Mappings, settings.Name);
        if (local.Count == 0)
        {
            Console.MarkupLine($"[yellow]No supported webresources found in '{Markup.Escape(settings.Target)}'[/]");
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

        Console.MarkupLine("[bold blue]Plan[/]");
        var targetDirectory = Directory.Exists(settings.Target)
            ? Path.GetFullPath(settings.Target)
            : Path.GetDirectoryName(Path.GetFullPath(settings.Target))!;
        var targetName = Path.GetFileName(Path.TrimEndingDirectorySeparator(Path.GetFullPath(settings.Target)));
        var targetMarkup = Markup.Escape(targetName);
        new WebResourcePlanRenderer(Console).Render(plan, targetDirectory);
        if (!WebResourcePushConfirmation.HasChanges(plan))
        {
            Console.WriteLine();
            Console.MarkupLine($"[green]{Emoji.Known.CheckMark}[/] {targetMarkup} is up to date");
            return true;
        }

        if (settings.DryRun)
        {
            Console.WriteLine();
            Console.MarkupLine($"[yellow]{Emoji.Known.Information}[/] Dry run for {targetMarkup}: no changes were applied");
            return true;
        }

        if (WebResourcePushConfirmation.ShouldPrompt(
                settings.Confirm,
                settings.NonInteractive,
                ExecutionEnvironment.IsCiAgent))
        {
            Console.WriteLine();
            if (!WebResourcePushConfirmation.Confirm(Console, targetName))
            {
                Console.WriteLine();
                Console.MarkupLine($"[yellow]{Emoji.Known.CrossMark}[/] Deployment of {targetMarkup} declined: no changes were applied");
                return true;
            }
        }

        Console.WriteLine();
        Console.MarkupLine("[bold]Execution:[/]");
        var executionReporter = new WebResourceExecutionReporter(Console);
        var executor = new WebResourcePushExecutor(webResourceRepository, solutionRepository);

        await Console.Status()
            .Spinner(Spinner.Known.Dots)
            .SpinnerStyle(Style.Parse("green bold"))
            .StartAsync(
                $"Applying changes to {targetMarkup}",
                _ => executor.ExecuteAsync(plan, executionReporter.ReportCompleted, cancellationToken));
        Console.WriteLine();
        Console.MarkupLine($"[green]{Emoji.Known.CheckMark}[/] {targetMarkup} deployed");

        return true;
    }
}
