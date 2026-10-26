// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using System.Globalization;
using dgt.power.common;
using dgt.power.plugin.Deployment.Batch;
using dgt.power.plugin.Deployment.Reconciliation;
using Spectre.Console;

namespace dgt.power.plugin.Commands;

// ReSharper disable once ClassNeverInstantiated.Global
public class PluginPushCommand(ICommandSpanFactory spans, IDataverseConnection connection, IAnsiConsole console)
    : PowerCommand<PluginPushSettings>(spans, console)
{
    protected override async Task<bool> RunAsync(PluginPushSettings settings, CancellationToken cancellationToken)
    {
        if (settings.DryRun)
        {
            DryRunBanner.Write(Console);
        }
        if (!TryResolve(settings.Target, out var paths))
        {
            return false;
        }

        var read = new DeploymentTargetReader(Console).Read(paths, cancellationToken);
        if (read.Failures.Count > 0)
        {
            ReportReadFailures(read);
            return false;
        }

        if (read.Targets.Count == 0)
        {
            Console.MarkupLineInterpolated(CultureInfo.InvariantCulture,
                $"[red]{paths.Count} file(s) matched '{settings.Target}', but none contain plugins. Narrow the target to the plugin artifacts.[/]");
            return false;
        }

        var options = new BatchOptions(settings.Solution, settings.PublisherPrefix, settings.DryRun, settings.Confirm, settings.NonInteractive, ExecutionEnvironment.IsCiAgent);
        var errors = BatchPreflight.Validate(read.Targets, options);
        if (errors.Count > 0)
        {
            foreach (var error in errors)
            {
                Console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[red]{error}[/]");
            }

            return false;
        }

        var runner = new DeploymentBatchRunner(new PluginDeployment(await connection.GetServiceAsync()), Console);
        var result = await runner.RunAsync(read.Targets, options, cancellationToken);
        return result.Success;
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
