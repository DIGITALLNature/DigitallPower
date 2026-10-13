// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using dgt.power.common.Extensions;
using dgt.power.plugin.Models;
using dgt.power.plugin.Repositories;
using Spectre.Console;

namespace dgt.power.plugin.StepManagement;

/// <summary>
/// Resolves plugin steps and applies enable/disable state changes for the <c>plugin step enable|disable|state set</c>
/// commands. Changes are idempotent (steps already in the desired state are not written), honor dry-run and
/// report failures per step so a bulk run always completes with a summary.
/// </summary>
internal sealed class PluginStepStateHandler(IAnsiConsole console, SdkMessageProcessingStepRepository repository, bool dryRun)
{
    private int EnabledCount { get; set; }
    private int DisabledCount { get; set; }
    private int UnchangedCount { get; set; }
    public int FailedCount { get; private set; }

    public static void WriteDryRunBanner(IAnsiConsole console)
    {
        ArgumentNullException.ThrowIfNull(console);

        console.Write(new Panel("[yellow]No changes will be written to Dataverse.[/]")
        {
            Header = new PanelHeader("DRY RUN"), Border = BoxBorder.Rounded, BorderStyle = new Style(Color.Yellow), Padding = new Padding(1, 0, 1, 0)
        });
    }

    public Task<PluginStepResolution> ResolveAsync(PluginStepSelector selector, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(selector);
        return ResolveCoreAsync(selector, cancellationToken);
    }

    private async Task<PluginStepResolution> ResolveCoreAsync(PluginStepSelector selector, CancellationToken cancellationToken)
    {
        if (selector.StepId.HasValue)
        {
            return new PluginStepResolution(await repository.GetStateAsync(selector.StepId.Value, cancellationToken), []);
        }

        var matches = await repository.FindByCompositeKeyAsync(
            selector.PluginType, selector.Message, selector.Stage, selector.Entity, selector.SecondaryEntity, selector.ExecutionOrder, cancellationToken);
        return matches.Count == 1
            ? new PluginStepResolution(await repository.GetStateAsync(matches[0].Id, cancellationToken), matches)
            : new PluginStepResolution(null, matches);
    }

    public void PrintAmbiguousMatches(IReadOnlyList<RemotePluginStep> matches, string stepIdHint)
    {
        console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[yellow]Multiple plugin steps match. Refine the criteria or use {stepIdHint}.[/]");

        foreach (var step in matches)
        {
            console.WriteLine($"  {step.Name} ({step.Id})");
        }
    }

    public void ReportFailure(string text)
    {
        FailedCount++;
        console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[red]{Emoji.Known.CrossMark}[/] {text}");
    }

    /// <summary>
    /// Applies the desired state to a step. Never throws for per-step failures (except cancellation); returns false when the step could not be changed.
    /// </summary>
    public Task<bool> ApplyAsync(RemotePluginStepState step, bool enable, string? reason, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(step);
        return ApplyCoreAsync(step, enable, reason, cancellationToken);
    }

    private async Task<bool> ApplyCoreAsync(RemotePluginStepState step, bool enable, string? reason, CancellationToken cancellationToken)
    {
        var verb = enable ? "enable" : "disable";
        var reasonSuffix = string.IsNullOrWhiteSpace(reason) ? string.Empty : $" - {reason}";

        if (step.IsEnabled == enable)
        {
            UnchangedCount++;
            console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[grey]○[/] Already {verb}d {step.Name}");
            return true;
        }

        if (dryRun)
        {
            Count(enable);
            console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[yellow]○[/] Would {verb} {step.Name}{reasonSuffix}");
            return true;
        }

        try
        {
            if (enable)
            {
                await repository.EnableAsync(step.Id, cancellationToken);
            }
            else
            {
                await repository.DisableAsync(step.Id, cancellationToken);
            }

            // Re-applying a reason to an unchanged step would only rewrite the description, so it is persisted with real changes only.
            if (!string.IsNullOrWhiteSpace(reason))
            {
                await repository.UpdateDescriptionAsync(step.Id, reason, cancellationToken);
            }
        }
        catch (Exception e) when (e is not (OutOfMemoryException or StackOverflowException or OperationCanceledException))
        {
            ReportFailure($"Failed to {verb} {step.Name}: {e.DiagnosticMessage()}");
            return false;
        }

        Count(enable);
        var color = enable ? "green" : "red";
        console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[{color}]●[/] {(enable ? "Enabled" : "Disabled")} {step.Name}{reasonSuffix}");
        return true;
    }

    public void PrintSummary()
    {
        console.WriteLine();
        var (enabled, disabled) = dryRun ? ("would be enabled", "would be disabled") : ("enabled", "disabled");
        console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[green]{Emoji.Known.CheckMark}[/] {EnabledCount} steps {enabled}");
        console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[green]{Emoji.Known.CheckMark}[/] {DisabledCount} steps {disabled}");

        if (UnchangedCount > 0)
        {
            console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[grey]○[/] {UnchangedCount} steps already in the desired state");
        }

        if (FailedCount > 0)
        {
            console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[red]{Emoji.Known.CrossMark}[/] {FailedCount} steps failed");
        }
    }

    private void Count(bool enable)
    {
        if (enable)
        {
            EnabledCount++;
        }
        else
        {
            DisabledCount++;
        }
    }
}
