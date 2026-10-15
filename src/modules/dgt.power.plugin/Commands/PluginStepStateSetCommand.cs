// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics.CodeAnalysis;
using dgt.power.common.Extensions;
using dgt.power.common;
using dgt.power.plugin.Repositories;
using dgt.power.plugin.StepManagement;
using Spectre.Console;

namespace dgt.power.plugin.Commands;

// ReSharper disable once ClassNeverInstantiated.Global
public class PluginStepStateSetCommand(
    ICommandSpanFactory spans,
    IDataverseConnection connection,
    IConfigResolver configResolver,
    IAnsiConsole console)
    : PowerCommand<PluginStepStateSetSettings>(spans, console)
{
    protected override async Task<bool> RunAsync([NotNull] PluginStepStateSetSettings settings, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(settings);

        if (!configResolver.TryGetConfigFile<PluginStepStateConfigRoot>(settings.Config, out var config))
        {
            Console.MarkupLine("[red]Failed to load or parse config file.[/]");
            return false;
        }

        if (config.Steps is not { Count: > 0 })
        {
            Console.MarkupLine("[yellow]No steps defined in config file.[/]");
            return true;
        }

        if (settings.DryRun)
        {
            DryRunBanner.Write(Console);
        }

        var service = await connection.GetServiceAsync();
        var handler = new PluginStepStateHandler(Console, new SdkMessageProcessingStepRepository(service), settings.DryRun);

        for (var index = 0; index < config.Steps.Count; index++)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await ProcessEntryAsync(handler, config.Steps[index], index + 1, cancellationToken);
        }

        handler.PrintSummary();
        return handler.FailedCount == 0;
    }

    private static async Task ProcessEntryAsync(PluginStepStateHandler handler,     PluginStepStateConfig? entry, int position, CancellationToken cancellationToken)
    {
            if (entry is null)
            {
                handler.ReportFailure($"Invalid entry #{position}: entry must not be null.");
                return;
            }

            var validationError = entry.Validate();
        if (validationError is not null)
        {
            handler.ReportFailure($"Invalid entry #{position}: {validationError}");
            return;
        }

        try
        {
            var resolution = await handler.ResolveAsync(
                new PluginStepSelector(entry.StepId, entry.PluginType, entry.Message, entry.Stage, entry.Entity, entry.SecondaryEntity, entry.ExecutionOrder), cancellationToken);

            if (resolution.IsAmbiguous)
            {
                handler.ReportFailure($"Entry #{position}: multiple plugin steps match {entry.Describe()}. Refine the criteria or use stepId.");
                handler.PrintAmbiguousMatches(resolution.Matches, "stepId");
                return;
            }

            if (resolution.Step is null)
            {
                handler.ReportFailure($"Entry #{position}: no plugin step matches {entry.Describe()}");
                return;
            }

            await handler.ApplyAsync(resolution.Step, entry.Enabled!.Value, entry.Reason, cancellationToken);
        }
        catch (Exception e) when (e is not (OutOfMemoryException or StackOverflowException or OperationCanceledException))
        {
            handler.ReportFailure($"Entry #{position} ({entry.Describe()}): {e.DiagnosticMessage()}");
        }
    }
}
