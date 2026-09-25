// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using dgt.power.common;
using dgt.power.common.Exceptions;
using dgt.power.plugin.Repositories;
using dgt.power.plugin.Execution;
using dgt.power.plugin.Output;
using dgt.power.plugin.Local;
using dgt.power.plugin.Planning;
using dgt.power.plugin.Planning.Deployment;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Spectre.Console;

namespace dgt.power.plugin.Commands;

// ReSharper disable once ClassNeverInstantiated.Global
public class PluginPushCommand(
    ITracer tracer,
    IOrganizationService connection,
    IConfigResolver configResolver,
    IAnsiConsole console)
    : PowerLogic<PluginPushSettings>(tracer, connection, configResolver, console)
{
    protected override Task<bool> InvokeAsync(PluginPushSettings settings, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(settings);
        return InvokeCoreAsync(settings, cancellationToken);
    }

    private async Task<bool> InvokeCoreAsync(PluginPushSettings settings, CancellationToken cancellationToken)
    {
        Tracer.Start(this);

        if (settings.DryRun)
        {
            Console.Write(new Panel("[yellow]No changes will be written to Dataverse.[/]")
            {
                Header = new PanelHeader("DRY RUN"),
                Border = BoxBorder.Rounded,
                BorderStyle = new Style(Color.Yellow),
                Padding = new Padding(1, 0, 1, 0)
            });
        }

        var targets = ResolveTargets(settings.Target);
        if (targets is null)
        {
            return Tracer.End(this, false);
        }

        if (targets.Count == 0)
        {
            Console.MarkupLine(CultureInfo.InvariantCulture,
                "[yellow]No plugin assemblies (*.dll) or packages (*.nupkg) found in '{0}'[/]", settings.Target);
            return Tracer.End(this, true);
        }

        if (targets.Any(target => target.EndsWith(".nupkg", StringComparison.OrdinalIgnoreCase)) &&
            string.IsNullOrWhiteSpace(settings.PublisherPrefix))
        {
            Console.MarkupLine("[red]--publisher-prefix is required when processing a plugin package (.nupkg)[/]");
            return Tracer.End(this, false);
        }

        var options = new PluginPushOptions(settings.Solution, settings.DryRun, settings.PublisherPrefix);

        var hadFailure = false;
        foreach (var target in targets)
        {
            try
            {
                if (!await ProcessTargetAsync(target, options, cancellationToken))
                {
                    hadFailure = true;
                }
            }
            catch (Exception e) when (e is not OutOfMemoryException and not StackOverflowException and not AbstractPowerException)
            {
                hadFailure = true;
                Console.MarkupLine(CultureInfo.InvariantCulture, "[red]Failed processing '{0}': {1}[/]", target, e.Message);
            }
        }

        return Tracer.End(this, !hadFailure);
    }

    private async Task<bool> ProcessTargetAsync(
        string target,
        PluginPushOptions options,
        CancellationToken cancellationToken)
    {
        var service = (IOrganizationServiceAsync2)Connection;
        var assemblyRepository = new PluginAssemblyRepository(service);
        var packageRepository = new PluginPackageRepository(service);
        var solutionRepository = new SolutionComponentRepository(service);
        var typeRepository = new PluginTypeRepository(service);
        var stepRepository = new SdkMessageProcessingStepRepository(service);
        var imageRepository = new SdkMessageProcessingStepImageRepository(service);
        var customApiRepository = new CustomApiRepository(service);
        var planner = new PluginDeploymentPlanner(new PluginPlanningRepositories
        {
            Assemblies = assemblyRepository,
            Packages = packageRepository,
            Types = typeRepository,
            Steps = stepRepository,
            Images = imageRepository,
            Messages = new SdkMessageRepository(service),
            CustomApis = customApiRepository,
            Solutions = solutionRepository
        });
        var executor = new PluginPushExecutor(
            assemblyRepository,
            packageRepository,
            solutionRepository,
            new ManagedIdentityRepository(service),
            new PluginTypeDeploymentExecutor(
                typeRepository,
                stepRepository,
                imageRepository,
                customApiRepository,
                solutionRepository),
            new OutdatedAssemblyMigrator(
                assemblyRepository,
                typeRepository,
                stepRepository,
                customApiRepository));
        var renderer = new PluginPlanRenderer(Console);

        if (target.EndsWith(".nupkg", StringComparison.OrdinalIgnoreCase))
        {
            var package = new PluginPackageReader(Console).Read(target);
            if (package is null)
            {
                Console.MarkupLine("[red]Failed to read package - aborting[/]");
                return false;
            }

            await BuildRenderAndExecuteAsync(
                Path.GetFileName(target),
                async () => await planner.BuildPackageAsync(package, options, cancellationToken),
                renderer,
                executor,
                options,
                cancellationToken);
            return true;
        }

        var targetDirectory = Path.GetDirectoryName(Path.GetFullPath(target))!;
        using var loadContext = MetadataLoadContextFactory.Create(targetDirectory);

        var assembly = new AssemblyReflectionReader(Console).Read(target, loadContext);
        if (assembly is null)
        {
            Console.MarkupLine("[red]Failed to read assembly - aborting[/]");
            return false;
        }

        if (assembly.Kind == LocalAssemblyKind.None)
        {
            return true;
        }

        await BuildRenderAndExecuteAsync(
            Path.GetFileName(target),
            async () => await planner.BuildAssemblyAsync(assembly, options, cancellationToken),
            renderer,
            executor,
            options,
            cancellationToken);
        return true;
    }

    private async Task BuildRenderAndExecuteAsync(
        string targetName,
        Func<Task<PluginDeploymentPlan>> buildPlan,
        PluginPlanRenderer renderer,
        PluginPushExecutor executor,
        PluginPushOptions options,
        CancellationToken cancellationToken)
    {
        Console.MarkupLine("[bold blue]Plan[/]");
        var plan = await Console.Status()
            .Spinner(Spinner.Known.Dots)
            .SpinnerStyle(Style.Parse("green bold"))
            .StartAsync($"Processing {targetName}...", _ => buildPlan());
        renderer.Render(plan);
        if (options.DryRun)
        {
            return;
        }

        Console.MarkupLine("[bold green]Execution[/]");
        var completedOperationCount = 0;
        void ReportCompletedOperation(PluginDeploymentProgress progress)
        {
            completedOperationCount++;
            Console.MarkupLine(
                $"[green]{Emoji.Known.CheckMark}[/] {progress.Operation} {Markup.Escape(progress.Resource)} {Markup.Escape(progress.Name)}");
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
    }

    private IReadOnlyList<string>? ResolveTargets(string target)
    {
        if (File.Exists(target))
        {
            if (!target.EndsWith(".dll", StringComparison.OrdinalIgnoreCase) &&
                !target.EndsWith(".nupkg", StringComparison.OrdinalIgnoreCase))
            {
                Console.MarkupLine(CultureInfo.InvariantCulture,
                    "[red]Unsupported file '{0}' - expected a .dll or .nupkg file[/]", target);
                return null;
            }

            return [target];
        }

        if (Directory.Exists(target))
        {
            return [
                .. Directory.EnumerateFiles(target, "*.dll", SearchOption.TopDirectoryOnly),
                .. Directory.EnumerateFiles(target, "*.nupkg", SearchOption.TopDirectoryOnly)
            ];
        }

        Console.MarkupLine(CultureInfo.InvariantCulture, "[red]Path '{0}' not found[/]", target);
        return null;
    }

}
