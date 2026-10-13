// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common;
using dgt.power.common.Extensions;
using dgt.power.plugin.Repositories;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Spectre.Console;

namespace dgt.power.plugin.Commands;

// ReSharper disable once ClassNeverInstantiated.Global
public class PluginStepStateSetCommand(
    ITracer tracer,
    IOrganizationService connection,
    IConfigResolver configResolver,
    IAnsiConsole console)
    : PowerLogic<PluginStepStateSetSettings>(tracer, connection, configResolver, console)
{
    protected override Task<bool> InvokeAsync(PluginStepStateSetSettings settings, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(settings);
        return InvokeCoreAsync(settings, cancellationToken);
    }

    private async Task<bool> InvokeCoreAsync(PluginStepStateSetSettings settings, CancellationToken cancellationToken)
    {
        Tracer.Start(this);

        if (!ConfigResolver.TryGetConfigFile<PluginStepStateConfigRoot>(settings.Config, out var config))
        {
            Console.MarkupLine("[red]Failed to load or parse config file.[/]");
            return Tracer.End(this, false);
        }

        if (config.Steps.Count == 0)
        {
            Console.MarkupLine("[yellow]No steps defined in config file.[/]");
            return Tracer.End(this, true);
        }

        if (settings.DryRun)
        {
            PluginStepStateHandler.WriteDryRunBanner(Console);
        }

        var handler = new PluginStepStateHandler(Console, new SdkMessageProcessingStepRepository((IOrganizationServiceAsync2)Connection), settings.DryRun);

        for (var index = 0; index < config.Steps.Count; index++)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await ProcessEntryAsync(handler, config.Steps[index], index + 1, cancellationToken);
        }

        handler.PrintSummary();
        return Tracer.End(this, handler.FailedCount == 0);
    }

    private static async Task ProcessEntryAsync(PluginStepStateHandler handler, PluginStepStateConfig entry, int position, CancellationToken cancellationToken)
    {
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
