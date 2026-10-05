// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common;
using dgt.power.webresource.Execution;
using dgt.power.webresource.Local;
using dgt.power.webresource.Output;
using dgt.power.webresource.Planning;
using dgt.power.webresource.Repositories;
using Microsoft.PowerPlatform.Dataverse.Client;
using Spectre.Console;

namespace dgt.power.webresource.Commands;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class WebResourcePushCommand(ITracer tracer, Microsoft.Xrm.Sdk.IOrganizationService connection, IConfigResolver configResolver, IAnsiConsole console)
    : PowerLogic<WebResourcePushSettings>(tracer, connection, configResolver, console)
{
    protected override Task<bool> InvokeAsync(WebResourcePushSettings settings, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(settings);
        return InvokeCoreAsync(settings, cancellationToken);
    }

    private async Task<bool> InvokeCoreAsync(WebResourcePushSettings settings, CancellationToken cancellationToken)
    {
        Tracer.Start(this);
        try
        {
            if (settings.DryRun)
            {
                Console.Write(new Panel("[yellow]No changes will be written to Dataverse.[/]")
                {
                    Header = new PanelHeader("DRY RUN"), Border = BoxBorder.Rounded, BorderStyle = new Style(Color.Yellow), Padding = new Padding(1, 0, 1, 0)
                });
            }

            var mapping = settings.MappingFile is null ? null : WebResourceMappingFile.Load(settings.MappingFile);
            var local = WebResourceDiscovery.Discover(settings.Target, settings.PublisherPrefix, mapping?.Mappings, settings.Name);
            if (local.Count == 0)
            {
                Console.MarkupLine($"[yellow]No supported webresources found in '{Markup.Escape(settings.Target)}'[/]");
                return Tracer.End(this, true);
            }

            var service = (IOrganizationServiceAsync2)Connection;
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
            new WebResourcePlanRenderer(Console).Render(plan, targetDirectory);
            if (settings.DryRun)
            {
                return Tracer.End(this, true);
            }

            if (WebResourcePushConfirmation.ShouldPrompt(
                    settings.Confirm,
                    settings.NonInteractive,
                    ExecutionEnvironment.IsCiAgent) &&
                WebResourcePushConfirmation.HasChanges(plan) &&
                !WebResourcePushConfirmation.Confirm(Console, Path.GetFileName(Path.GetFullPath(settings.Target))))
            {
                Console.MarkupLine("[yellow]Deployment cancelled.[/]");
                return Tracer.End(this, true);
            }

            Console.MarkupLine("[bold green]Execution[/]");
            var completedOperationCount = 0;
            var executionReporter = new WebResourceExecutionReporter(Console);
            var executor = new WebResourcePushExecutor(webResourceRepository, solutionRepository);

            void ReportCompletedOperation(WebResourceDeploymentProgress progress)
            {
                completedOperationCount++;
                executionReporter.ReportCompleted(progress);
            }

            await Console.Status()
                .Spinner(Spinner.Known.Dots)
                .SpinnerStyle(Style.Parse("green bold"))
                .StartAsync(
                    "Applying deployment plan...",
                    _ => executor.ExecuteAsync(plan, ReportCompletedOperation, cancellationToken));
            Console.MarkupLine(completedOperationCount == 0
                ? $"[green]{Emoji.Known.CheckMark}[/] No changes applied"
                : $"[green]{Emoji.Known.CheckMark}[/] Deployment completed");

            return Tracer.End(this, true);
        }
        catch
        {
            Tracer.End(this, false);
            throw;
        }
    }
}
