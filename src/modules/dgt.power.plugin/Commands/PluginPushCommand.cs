// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using dgt.power.common.Extensions;
using dgt.power.common;
using dgt.power.plugin.Deployment.Execution;
using dgt.power.plugin.Deployment.Local;
using dgt.power.plugin.Deployment.Output;
using dgt.power.plugin.Deployment.Planning.Plans;
using dgt.power.plugin.Deployment.Planning;
using dgt.power.plugin.Repositories;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Spectre.Console;

namespace dgt.power.plugin.Commands;

// ReSharper disable once ClassNeverInstantiated.Global
public class PluginPushCommand(ICommandSpanFactory spans, IDataverseConnection connection, IAnsiConsole console)
    : PowerCommand<PluginPushSettings>(spans, console)
{
    protected override async Task<bool> RunAsync(PluginPushSettings settings, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(settings);

        if (settings.DryRun)
        {
            console.Write(new Panel("[yellow]No changes will be written to Dataverse.[/]")
            {
                Header = new PanelHeader("DRY RUN"), Border = BoxBorder.Rounded, BorderStyle = new Style(Color.Yellow), Padding = new Padding(1, 0, 1, 0)
            });
        }

        var targets = ResolveTargets(settings.Target);
        if (targets is null)
        {
            return false;
        }

        if (targets.Count == 0)
        {
            console.MarkupLine(CultureInfo.InvariantCulture, "[yellow]No plugin assemblies (*.dll) or packages (*.nupkg) found in '{0}'[/]", settings.Target);
            return true;
        }

        if (targets.Exists(target => target.EndsWith(".nupkg", StringComparison.OrdinalIgnoreCase)) && string.IsNullOrWhiteSpace(settings.PublisherPrefix))
        {
            console.MarkupLine("[red]--publisher-prefix is required when processing a plugin package (.nupkg)[/]");
            return false;
        }

        var options = new PluginPushOptions(settings.Solution, settings.DryRun, settings.PublisherPrefix, settings.Confirm, settings.NonInteractive);
        var service = await connection.GetServiceAsync();

        var hadFailure = false;
        foreach (var target in targets)
        {
            try
            {
                if (!await ProcessTargetAsync(service, target, options, cancellationToken))
                {
                    hadFailure = true;
                }
            }
            catch (Exception e) when (ShouldContinueAfterTargetFailure(e))
            {
                hadFailure = true;
                console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[red]Failed processing '{target}': {e.DiagnosticMessage()}[/]");
            }
        }

        return !hadFailure;
    }

    internal static bool ShouldContinueAfterTargetFailure(Exception exception) => exception is not OutOfMemoryException and not StackOverflowException and not OperationCanceledException;

    private async Task<bool> ProcessTargetAsync(IOrganizationServiceAsync2 service, string target, PluginPushOptions options, CancellationToken cancellationToken)
    {
        var assemblyRepository = new PluginAssemblyRepository(service);
        var packageRepository = new PluginPackageRepository(service);
        var solutionRepository = new SolutionComponentRepository(service);
        var typeRepository = new PluginTypeRepository(service);
        var stepRepository = new SdkMessageProcessingStepRepository(service);
        var imageRepository = new SdkMessageProcessingStepImageRepository(service);
        var customApiRepository = new CustomApiRepository(service);
        var dataProviderRepository = new EntityDataProviderRepository(service);
        var planner = new PluginDeploymentPlanner(new PluginPlanningRepositories
        {
            Assemblies = assemblyRepository,
            Packages = packageRepository,
            Types = typeRepository,
            Steps = stepRepository,
            Images = imageRepository,
            Messages = new SdkMessageRepository(service),
            CustomApis = customApiRepository,
            ManagedIdentities = new ManagedIdentityRepository(service),
            Solutions = solutionRepository,
            DataProviders = dataProviderRepository
        });
        var executor = new PluginPushExecutor(assemblyRepository, packageRepository, solutionRepository, new ManagedIdentityRepository(service),
            new PluginTypeDeploymentExecutor(typeRepository, stepRepository, imageRepository, customApiRepository, solutionRepository, dataProviderRepository),
            new OutdatedAssemblyMigrator(assemblyRepository, typeRepository, stepRepository, customApiRepository));
        var renderer = new PluginPlanRenderer(console);

        if (target.EndsWith(".nupkg", StringComparison.OrdinalIgnoreCase))
        {
            var package = new PluginPackageReader(console).Read(target);
            if (package is null)
            {
                console.MarkupLine("[red]Failed to read package - aborting[/]");
                return false;
            }

            await BuildRenderAndExecuteAsync(Path.GetFileName(target), async () => await planner.BuildPackageAsync(package, options, cancellationToken), renderer, executor, options,
                cancellationToken);
            return true;
        }

        var fullTargetPath = Path.GetFullPath(target);
        var targetDirectory = Path.GetDirectoryName(fullTargetPath)!;
        using var loadContext = MetadataLoadContextFactory.Create(targetDirectory);

        var assembly = new AssemblyReflectionReader(console).Read(fullTargetPath, loadContext);
        if (assembly is null)
        {
                    console.MarkupLine("[red]Failed to read assembly - aborting[/]");
            return false;
        }

        if (assembly.Kind == LocalAssemblyKind.None)
        {
            return true;
        }

        await BuildRenderAndExecuteAsync(Path.GetFileName(target), async () => await planner.BuildAssemblyAsync(assembly, options, cancellationToken), renderer, executor, options, cancellationToken);
        return true;
    }

    private async Task BuildRenderAndExecuteAsync(string targetName, Func<Task<PluginDeploymentPlan>> buildPlan, PluginPlanRenderer renderer, PluginPushExecutor executor, PluginPushOptions options,
        CancellationToken cancellationToken)
    {
        console.MarkupLine("[bold blue]Plan[/]");
        var plan = await console.Status().Spinner(Spinner.Known.Dots).SpinnerStyle(Style.Parse("green bold")).StartAsync($"Processing {targetName}...", _ => buildPlan());
        renderer.Render(plan);
        if (options.DryRun)
        {
            return;
        }

        if (PluginPushConfirmation.ShouldPrompt(options.Confirm, options.NonInteractive, ExecutionEnvironment.IsCiAgent) && plan.HasChanges() && !PluginPushConfirmation.Confirm(console, targetName))
        {
                    console.MarkupLine("[yellow]Deployment cancelled.[/]");
            return;
        }

        console.MarkupLine("[bold green]Execution[/]");
        var completedOperationCount = 0;

        void ReportCompletedOperation(PluginDeploymentProgress progress)
        {
            completedOperationCount++;
            console.MarkupLine($"[green]{Emoji.Known.CheckMark}[/] {progress.Operation} {Markup.Escape(progress.Resource)} {Markup.Escape(progress.Name)}");
        }

        await console.Status().Spinner(Spinner.Known.Dots).SpinnerStyle(Style.Parse("green bold"))
            .StartAsync("Applying deployment plan...", _ => executor.ExecuteAsync(plan, ReportCompletedOperation, cancellationToken));

        console.MarkupLine(completedOperationCount == 0 ? $"[green]{Emoji.Known.CheckMark}[/] No changes applied" : $"[green]{Emoji.Known.CheckMark}[/] Deployment completed");
    }

    private List<string>? ResolveTargets(string target)
    {
        if (File.Exists(target))
        {
            if (!target.EndsWith(".dll", StringComparison.OrdinalIgnoreCase) && !target.EndsWith(".nupkg", StringComparison.OrdinalIgnoreCase))
            {
                console.MarkupLine(CultureInfo.InvariantCulture, "[red]Unsupported file '{0}' - expected a .dll or .nupkg file[/]", target);
                return null;
            }

            return [target];
        }

        if (Directory.Exists(target))
        {
            return Directory.EnumerateFiles(target, "*", SearchOption.TopDirectoryOnly)
                .Where(file => file.EndsWith(".dll", StringComparison.OrdinalIgnoreCase) || file.EndsWith(".nupkg", StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        console.MarkupLine(CultureInfo.InvariantCulture, "[red]Path '{0}' not found[/]", target);
        return null;
    }
}
