// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using dgt.power.common.Extensions;
using dgt.power.plugin.Commands;
using dgt.power.plugin.Deployment.Output;
using dgt.power.plugin.Deployment.Reconciliation;
using Spectre.Console;

namespace dgt.power.plugin.Deployment.Batch;

/// <summary>
/// Deploys a validated batch target by target. Remote state is prepared for each target only after the
/// previous target has been applied, so later targets never act on stale snapshots. A failing target does
/// not stop independent targets; cancellation stops the batch and is never reported as a target failure.
/// </summary>
internal sealed class DeploymentBatchRunner(PluginDeployment deployment, IAnsiConsole console)
{
    private readonly DeploymentRenderer _renderer = new(console);

    internal Task<BatchResult> RunAsync(IReadOnlyList<LocalDeploymentTarget> targets, BatchOptions options, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(targets);
        ArgumentNullException.ThrowIfNull(options);
        return RunCoreAsync(targets, options, cancellationToken);
    }

    private async Task<BatchResult> RunCoreAsync(IReadOnlyList<LocalDeploymentTarget> targets, BatchOptions options, CancellationToken cancellationToken)
    {
        var ordered = targets.OrderBy(target => target.FilePath, StringComparer.OrdinalIgnoreCase).ToList();
        var preflight = new List<string>();
        if (options.PublisherPrefix is null or { Length: 0 } && ordered.Exists(target => target.Package is not null))
        {
            preflight.Add("--publisher-prefix is required when processing a plugin package (.nupkg)");
        }

        preflight.AddRange(BatchPreflight.FindConflicts(ordered));
        if (preflight.Count > 0)
        {
            return new BatchResult([], preflight);
        }

        var results = new List<TargetResult>();
        foreach (var target in ordered)
        {
            try
            {
                results.Add(await ProcessAsync(target, options, cancellationToken));
            }
            catch (OperationCanceledException)
            {
                RenderSummary(results);
                console.MarkupLine("[yellow]Cancelled; remaining targets were not processed.[/]");
                throw;
            }
            catch (Exception e) when (e is not OutOfMemoryException and not StackOverflowException)
            {
                console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[red]Failed processing '{target.DisplayName}': {e.DiagnosticMessage()}[/]");
                results.Add(new TargetResult(target.FilePath, TargetOutcome.Failed, e.DiagnosticMessage()));
            }
        }

        RenderSummary(results);
        return new BatchResult(results, []);
    }

    private async Task<TargetResult> ProcessAsync(LocalDeploymentTarget target, BatchOptions options, CancellationToken cancellationToken)
    {
        console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[bold blue]{target.DisplayName}[/]");
        var prepared = await PrepareAsync(target, options, cancellationToken);
        if (!prepared.HasChanges)
        {
            console.MarkupLine(DeploymentRenderer.UpToDate($"{Markup.Escape(target.DisplayName)} is up to date"));
            return new TargetResult(target.FilePath, TargetOutcome.Unchanged);
        }

        if (options.DryRun)
        {
            return new TargetResult(target.FilePath, TargetOutcome.DryRun);
        }

        if (PluginPushConfirmation.ShouldPrompt(options.Confirm, options.NonInteractive, options.IsCiAgent) && !PluginPushConfirmation.Confirm(console, target.DisplayName))
        {
            return new TargetResult(target.FilePath, TargetOutcome.Declined);
        }

        // Progress lines are written through the same console and scroll above the spinner.
        await console.Status().StartAsync($"Applying changes to {Markup.Escape(target.DisplayName)}",
            async _ => await prepared.Apply(ReportProgress, cancellationToken));
        console.MarkupLine(DeploymentRenderer.UpToDate($"{Markup.Escape(target.DisplayName)} deployed"));
        return new TargetResult(target.FilePath, TargetOutcome.Applied);
    }

    private async Task<PreparedTarget> PrepareAsync(LocalDeploymentTarget target, BatchOptions options, CancellationToken cancellationToken)
    {
        var status = $"Processing {Markup.Escape(target.DisplayName)}";
        if (target.Package is { } package)
        {
            var changes = await console.Status().StartAsync(status,
                _ => deployment.PreparePackageAsync(package, options.PublisherPrefix!, options.Solution, cancellationToken));
            _renderer.RenderPackage(changes);
            return new PreparedTarget(changes.HasChanges, (progress, token) => deployment.ApplyPackageAsync(changes, progress, token));
        }

        var standalone = await console.Status().StartAsync(status,
            _ => deployment.PrepareAsync(target.Assembly!, options.Solution, cancellationToken));
        _renderer.Render(standalone);
        return new PreparedTarget(standalone.HasChanges, (progress, token) => deployment.ApplyAsync(standalone, progress, token));
    }

    private void ReportProgress(PluginDeploymentProgress progress)
    {
        var text = progress.Operation == PluginDeploymentOperation.AddedToSolution
            ? $"Added {progress.Resource} {progress.Name} to solution"
            : $"{progress.Operation} {progress.Resource} {progress.Name}";
        console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[green]{Emoji.Known.CheckMark}[/] {text}");
    }

    private void RenderSummary(List<TargetResult> results)
    {
        if (results.Count == 0)
        {
            return;
        }

        var table = new Table().AddColumn("Target").AddColumn("Result").AddColumn("Details");
        foreach (var result in results)
        {
            var color = result.Outcome switch
            {
                TargetOutcome.Failed => "red",
                TargetOutcome.Declined => "yellow",
                TargetOutcome.Applied => "green",
                _ => "grey"
            };
            table.AddRow(Markup.Escape(Path.GetFileName(result.FilePath)), $"[{color}]{result.Outcome}[/]", Markup.Escape(result.Message ?? string.Empty));
        }

        console.Write(table);
    }
}
