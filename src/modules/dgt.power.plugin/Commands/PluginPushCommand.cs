// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using dgt.power.common;
using dgt.power.common.Exceptions;
using dgt.power.plugin.Dataverse;
using dgt.power.plugin.Execution;
using dgt.power.plugin.Local;
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
    protected override async Task<bool> InvokeAsync(PluginPushSettings settings, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(settings);
        Tracer.Start(this);

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

        var service = (IOrganizationServiceAsync2)Connection;
        var executor = new PluginPushExecutor(
            new PluginAssemblyRepository(service),
            new PluginPackageRepository(service),
            new SolutionComponentRepository(service),
            new ManagedIdentityRepository(service),
            new PluginTypeReconciler(
                new PluginTypeRepository(service),
                new SdkMessageProcessingStepRepository(service),
                new SdkMessageProcessingStepImageRepository(service),
                new SdkMessageRepository(service),
                new CustomApiRepository(service),
                Console),
            new OutdatedAssemblyMigrator(
                new PluginAssemblyRepository(service),
                new PluginTypeRepository(service),
                new SdkMessageProcessingStepRepository(service),
                new CustomApiRepository(service),
                Console),
            Console);

        var assemblyReader = new AssemblyReflectionReader(Console);
        var packageReader = new PluginPackageReader(Console);
        var options = new PluginPushOptions(settings.Solution, settings.DryRun, settings.PurgeOutdated);

        var hadFailure = await Console.Status()
            .Spinner(Spinner.Known.Pong)
            .SpinnerStyle(Style.Parse("green bold"))
            .StartAsync("Connect to XRM...", async ctx =>
            {
                var failed = false;
                foreach (var target in targets)
                {
                    try
                    {
                        ctx.Status(string.Format(CultureInfo.InvariantCulture, "Processing {0}", Path.GetFileName(target)));
                        await ProcessTargetAsync(target, executor, assemblyReader, packageReader, options, cancellationToken);
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

    private async Task ProcessTargetAsync(
        string target,
        PluginPushExecutor executor,
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
                return;
            }

            await executor.ProcessPackageAsync(package, options, cancellationToken);
            return;
        }

        var env = Directory.GetFiles(RuntimeEnvironment.GetRuntimeDirectory(), "*.dll")
            .Concat(Directory.GetFiles(Path.GetDirectoryName(typeof(PluginPushCommand).Assembly.Location)!, "*.dll"))
            .ToList();
        using var loadContext = new MetadataLoadContext(new PathAssemblyResolver(env));

        var assembly = assemblyReader.Read(target, loadContext);
        if (assembly is null)
        {
            Console.MarkupLine("[red]Failed to read assembly - aborting[/]");
            return;
        }

        if (assembly.Kind == LocalAssemblyKind.Undefined)
        {
            Console.MarkupLine(CultureInfo.InvariantCulture,
                "Assembly [bold green]{0} ({1})[/] [bold red]does not contain[/] any plugins - skipping",
                assembly.Name, assembly.Version);
            return;
        }

        Console.MarkupLine(CultureInfo.InvariantCulture, "Check Assembly [bold green]{0} ({1})[/]", assembly.Name, assembly.Version);
        await executor.ProcessAssemblyAsync(assembly, options, cancellationToken);
    }
}
