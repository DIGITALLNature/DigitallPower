// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using System.Globalization;
using dgt.power.common.Extensions;
using dgt.power.plugin.Commands;
using dgt.power.plugin.Deployment.Output;
using dgt.power.plugin.Deployment.Reconciliation;
using Spectre.Console;

namespace dgt.power.plugin.Deployment.Batch;

/// <summary>
/// Deploys a batch that passed <see cref="BatchPreflight.Validate" /> target by target. Remote state is prepared for each target only after the
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
        var results = new List<TargetResult>();
        foreach (var target in ordered)
        {
            if (results.Count > 0)
            {
                console.WriteLine();
            }

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
                WriteResult(TargetOutcome.Failed, Markup.Escape($"Failed processing '{target.DisplayName}': {e.DiagnosticMessage()}"));
                results.Add(new TargetResult(target.FilePath, TargetOutcome.Failed, e.DiagnosticMessage()));
            }
        }

        RenderSummary(results);
        return new BatchResult(results);
    }

    private async Task<TargetResult> ProcessAsync(LocalDeploymentTarget target, BatchOptions options, CancellationToken cancellationToken)
    {
        console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[bold blue]{target.DisplayName}[/]");
        var prepared = await PrepareAsync(target, options, cancellationToken);
        if (!prepared.HasChanges)
        {
            console.WriteLine();
            WriteResult(TargetOutcome.Unchanged, $"{Markup.Escape(target.DisplayName)} is up to date");
            return new TargetResult(target.FilePath, TargetOutcome.Unchanged);
        }

        if (options.DryRun)
        {
            console.WriteLine();
            WriteResult(TargetOutcome.DryRun, $"Dry run for {Markup.Escape(target.DisplayName)}: no changes were applied");
            return new TargetResult(target.FilePath, TargetOutcome.DryRun);
        }

        if (PluginPushConfirmation.ShouldPrompt(options.Confirm, options.NonInteractive, options.IsCiAgent))
        {
            console.WriteLine();
            if (!PluginPushConfirmation.Confirm(console, target.DisplayName))
            {
                console.WriteLine();
                WriteResult(TargetOutcome.Declined, $"Deployment of {Markup.Escape(target.DisplayName)} declined: no changes were applied");
                return new TargetResult(target.FilePath, TargetOutcome.Declined);
            }
        }

        // Progress lines are written through the same console and scroll above the spinner.
        console.WriteLine();
        console.MarkupLine("[bold]Execution:[/]");
        await console.Status().StartAsync($"Applying changes to {Markup.Escape(target.DisplayName)}",
            async _ => await prepared.Apply(ReportProgress, cancellationToken));
        console.WriteLine();
        WriteResult(TargetOutcome.Applied, $"{Markup.Escape(target.DisplayName)} deployed");
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
            var color = OutcomeColor(result.Outcome);
            table.AddRow(Markup.Escape(Path.GetFileName(result.FilePath)), $"[{color}]{OutcomeIcon(result.Outcome)} {result.Outcome}[/]", Markup.Escape(result.Message ?? string.Empty));
        }

        console.WriteLine();
        console.Write(table);
    }

    private void WriteResult(TargetOutcome outcome, string markupText) =>
        console.MarkupLine($"[{OutcomeColor(outcome)}]{OutcomeIcon(outcome)}[/] {markupText}");

    // Green: desired state reached; yellow: nothing applied by choice; red: error.
    internal static string OutcomeColor(TargetOutcome outcome) => outcome switch
    {
        TargetOutcome.Applied or TargetOutcome.Unchanged => "green",
        TargetOutcome.Failed => "red",
        _ => "yellow"
    };

    internal static string OutcomeIcon(TargetOutcome outcome) => outcome switch
    {
        TargetOutcome.Applied or TargetOutcome.Unchanged => Emoji.Known.CheckMark,
        TargetOutcome.DryRun => Emoji.Known.Information,
        _ => Emoji.Known.CrossMark
    };
}
