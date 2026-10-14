// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using dgt.power.common;
using dgt.power.plugin.Deployment.Batch;
using dgt.power.plugin.Deployment.Reconciliation;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Spectre.Console;

namespace dgt.power.plugin.Commands;

// ReSharper disable once ClassNeverInstantiated.Global
public class PluginPushCommand(ITracer tracer, IOrganizationService connection, IConfigResolver configResolver, IAnsiConsole console)
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
                Header = new PanelHeader("DRY RUN"), Border = BoxBorder.Rounded, BorderStyle = new Style(Color.Yellow), Padding = new Padding(1, 0, 1, 0)
            });
        }

        if (!TryResolve(settings.Target, out var paths))
        {
            return Tracer.End(this, false);
        }

        var read = new DeploymentTargetReader(Console).Read(paths, cancellationToken);
        if (read.Failures.Count > 0)
        {
            ReportReadFailures(read);
            return Tracer.End(this, false);
        }

        if (read.Targets.Count == 0)
        {
            Console.MarkupLineInterpolated(CultureInfo.InvariantCulture,
                $"[red]{paths.Count} file(s) matched '{settings.Target}', but none contain plugins. Narrow the target to the plugin artifacts.[/]");
            return Tracer.End(this, false);
        }

        var options = new BatchOptions(settings.Solution, settings.PublisherPrefix, settings.DryRun, settings.Confirm, settings.NonInteractive, ExecutionEnvironment.IsCiAgent);
        var runner = new DeploymentBatchRunner(new PluginDeployment((IOrganizationServiceAsync2)Connection), Console);
        var result = await runner.RunAsync(read.Targets, options, cancellationToken);
        foreach (var error in result.PreflightErrors)
        {
            Console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[red]{error}[/]");
        }

        return Tracer.End(this, result.Success);
    }

    private bool TryResolve(string target, out IReadOnlyList<string> paths)
    {
        paths = [];
        try
        {
            paths = ArtifactPathResolver.Resolve(target);
            return true;
        }
        catch (ArtifactPathException e)
        {
            Console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[red]{e.Message}[/]");
            return false;
        }
    }

    private void ReportReadFailures(TargetReadResult read)
    {
        foreach (var failure in read.Failures)
        {
            Console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[red]Failed to read '{failure.FilePath}': {failure.Message}[/]");
        }

        Console.MarkupLine("[red]Nothing was deployed. Narrow the target to the plugin artifacts and retry.[/]");
    }
}
