// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common;
using dgt.power.dataverse;
using dgt.power.plugin.Remote;
using dgt.power.plugin.Repositories;
using dgt.power.plugin.Steps;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Spectre.Console;
using System.Globalization;

namespace dgt.power.plugin.Commands;

/// <summary>
/// Shared implementation of <c>plugin step enable</c> and <c>plugin step disable</c>; the concrete commands only choose the desired state.
/// </summary>
public abstract class PluginStepEnableDisableCommandBase(
    ITracer tracer,
    IOrganizationService connection,
    IConfigResolver configResolver,
    IAnsiConsole console)
    : PowerLogic<PluginStepEnableDisableSettings>(tracer, connection, configResolver, console)
{
    /// <summary>The desired state: <c>true</c> enables the resolved steps, <c>false</c> disables them.</summary>
    protected abstract bool Enable { get; }

    protected override Task<bool> InvokeAsync(PluginStepEnableDisableSettings settings, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(settings);
        return InvokeCoreAsync(settings, cancellationToken);
    }

    private async Task<bool> InvokeCoreAsync(PluginStepEnableDisableSettings settings, CancellationToken cancellationToken)
    {
        Tracer.Start(this);

        if (settings.DryRun)
        {
            PluginStepStateHandler.WriteDryRunBanner(Console);
        }

        var service = (IOrganizationServiceAsync2)Connection;
        var handler = new PluginStepStateHandler(Console, new SdkMessageProcessingStepRepository(service), settings.DryRun);

        if (!string.IsNullOrWhiteSpace(settings.Solution))
        {
            return Tracer.End(this, await ApplyToSolutionAsync(settings, service, handler, cancellationToken));
        }

        var resolution = await handler.ResolveAsync(
            new PluginStepSelector(
                settings.StepId,
                settings.PluginType,
                settings.Message,
                settings.Stage is { } stage ? (int)stage : null,
                settings.Entity,
                settings.SecondaryEntity,
                settings.ExecutionOrder),
            cancellationToken);

        if (resolution.IsAmbiguous)
        {
            handler.PrintAmbiguousMatches(resolution.Matches, "--step-id");
            return Tracer.End(this, false);
        }

        if (resolution.Step is null)
        {
            Console.MarkupLine("[red]No plugin step matches the supplied criteria.[/]");
            return Tracer.End(this, false);
        }

        return Tracer.End(this, await handler.ApplyAsync(resolution.Step, Enable, settings.Reason, cancellationToken));
    }

    private async Task<bool> ApplyToSolutionAsync(
        PluginStepEnableDisableSettings settings,
        IOrganizationServiceAsync2 service,
        PluginStepStateHandler handler,
        CancellationToken cancellationToken)
    {
        var solutionName = settings.Solution!;
        var solutions = new SolutionComponentRepository(service);

        IReadOnlyList<RemotePluginStepState> steps;
        try
        {
            var componentType = await solutions.GetComponentTypeAsync(SdkMessageProcessingStep.EntityLogicalName, cancellationToken);
            if (componentType is null)
            {
                Console.MarkupLine("[red]The solution component type of plugin steps could not be resolved.[/]");
                return false;
            }

            var stepIds = await solutions.ListComponentIdsAsync(solutionName, componentType.Value, cancellationToken);
            steps = await new SdkMessageProcessingStepRepository(service).ListStatesAsync(stepIds, cancellationToken);
        }
        catch (MissingSolutionException e)
        {
            Console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[red]{e.Message}[/]");
            return false;
        }

        if (steps.Count == 0)
        {
            Console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[yellow]No plugin steps found in solution '{solutionName}'[/]");
            return true;
        }

        foreach (var step in steps)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await handler.ApplyAsync(step, Enable, settings.Reason, cancellationToken);
        }

        handler.PrintSummary();
        return handler.FailedCount == 0;
    }
}
