// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using dgt.power.common;
using dgt.power.common.Exceptions;
using dgt.power.plugin.Repositories;
using dgt.power.plugin.Execution;
using dgt.power.plugin.Output;
using dgt.power.plugin.Local;
using dgt.power.plugin.Planning;
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

        var service = (IOrganizationServiceAsync2)Connection;
        var assemblyRepository = new PluginAssemblyRepository(service);
        var packageRepository = new PluginPackageRepository(service);
        var solutionRepository = new SolutionComponentRepository(service);
        var managedIdentityRepository = new ManagedIdentityRepository(service);
        var typeRepository = new PluginTypeRepository(service);
        var stepRepository = new SdkMessageProcessingStepRepository(service);
        var imageRepository = new SdkMessageProcessingStepImageRepository(service);
        var messageRepository = new SdkMessageRepository(service);
        var customApiRepository = new CustomApiRepository(service);
        var planner = new PluginDeploymentPlanner(new PluginPlanningRepositories
        {
            Assemblies = assemblyRepository,
            Packages = packageRepository,
            Types = typeRepository,
            Steps = stepRepository,
            Images = imageRepository,
            Messages = messageRepository,
            CustomApis = customApiRepository,
            Solutions = solutionRepository
        });
        var executor = new PluginPushExecutor(
            assemblyRepository,
            packageRepository,
            solutionRepository,
            managedIdentityRepository,
            new PluginTypeDeploymentExecutor(
                typeRepository,
                stepRepository,
                imageRepository,
                customApiRepository),
            new OutdatedAssemblyMigrator(
                assemblyRepository,
                typeRepository,
                stepRepository,
                customApiRepository));
        var renderer = new PluginPlanRenderer(Console);
        var pipeline = new PluginDeploymentPipeline(planner, renderer, executor);

        var assemblyReader = new AssemblyReflectionReader(Console);
        var packageReader = new PluginPackageReader(Console);
        var options = new PluginPushOptions(settings.Solution, settings.DryRun, settings.PublisherPrefix);

        var hadFailure = await Console.Status()
            .Spinner(Spinner.Known.Dots)
            .SpinnerStyle(Style.Parse("green bold"))
            .StartAsync("Connect to XRM...", async ctx =>
            {
                var failed = false;
                foreach (var target in targets)
                {
                    try
                    {
                        ctx.Status(string.Format(CultureInfo.InvariantCulture, "Processing {0}", Path.GetFileName(target)));
                        if (!await ProcessTargetAsync(
                                target,
                                pipeline,
                                assemblyReader,
                                packageReader,
                                options,
                                cancellationToken))
                        {
                            failed = true;
                        }
                    }
                    catch (Exception e) when (e is not OutOfMemoryException and not StackOverflowException and not AbstractPowerException)
                    {
                        failed = true;
                        Console.MarkupLine(CultureInfo.InvariantCulture, "[red]Failed processing '{0}': {1}[/]", target, e.Message);
                    }
                }

                ctx.Status("Finishing");
                return failed;
            });

        return Tracer.End(this, !hadFailure);
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

    private async Task<bool> ProcessTargetAsync(
        string target,
        PluginDeploymentPipeline pipeline,
        AssemblyReflectionReader assemblyReader,
        PluginPackageReader packageReader,
        PluginPushOptions options,
        CancellationToken cancellationToken)
    {
        if (target.EndsWith(".nupkg", StringComparison.OrdinalIgnoreCase))
        {
            var package = packageReader.Read(target);
            if (package is null)
            {
                Console.MarkupLine("[red]Failed to read package - aborting[/]");
                return false;
            }

            await pipeline.ProcessPackageAsync(package, options, cancellationToken);
            return true;
        }

        var env = Directory.GetFiles(RuntimeEnvironment.GetRuntimeDirectory(), "*.dll")
            .Concat(Directory.GetFiles(Path.GetDirectoryName(typeof(PluginPushCommand).Assembly.Location)!, "*.dll"))
            .ToList();
        using var loadContext = new MetadataLoadContext(new PathAssemblyResolver(env));

        var assembly = assemblyReader.Read(target, loadContext);
        if (assembly is null)
        {
            Console.MarkupLine("[red]Failed to read assembly - aborting[/]");
            return false;
        }

        if (assembly.Kind == LocalAssemblyKind.None)
        {
            return true;
        }

        await pipeline.ProcessAssemblyAsync(assembly, options, cancellationToken);
        return true;
    }
}
