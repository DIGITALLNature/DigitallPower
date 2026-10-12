// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using dgt.power.common;
using dgt.power.plugin.Remote;
using dgt.power.plugin.Repositories;
using Microsoft.Xrm.Sdk;
using Microsoft.PowerPlatform.Dataverse.Client;
using Spectre.Console;

namespace dgt.power.plugin.Commands;

// ReSharper disable once ClassNeverInstantiated.Global
public class PluginStepDisableCommand(
    ITracer tracer,
    IOrganizationService connection,
    IConfigResolver configResolver,
    IAnsiConsole console)
    : PowerLogic<PluginStepEnableDisableSettings>(tracer, connection, configResolver, console)
{
    protected override Task<bool> InvokeAsync(PluginStepEnableDisableSettings settings, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(settings);
        return InvokeCoreAsync(settings, cancellationToken);
    }

    private async Task<bool> InvokeCoreAsync(PluginStepEnableDisableSettings settings, CancellationToken cancellationToken)
    {
        Tracer.Start(this);

        var service = (IOrganizationServiceAsync2)Connection;
        var stepRepository = new SdkMessageProcessingStepRepository(service);

        // Handle solution-scoped mode
        if (!string.IsNullOrWhiteSpace(settings.Solution))
        {
            return await HandleSolutionScopedAsync(settings, stepRepository, cancellationToken);
        }

        // Resolve step id and get step name
        RemotePluginStep? step;

        if (settings.StepId.HasValue)
        {
            step = await stepRepository.GetByIdAsync(settings.StepId.Value, cancellationToken);
            if (step is null)
            {
                Console.MarkupLine(CultureInfo.InvariantCulture, "[red]No plugin step matches the supplied criteria.[/]");
                return Tracer.End(this, false);
            }
        }
        else
        {
            var matches = await stepRepository.FindByCompositeKeyAsync(
                settings.PluginType,
                settings.Message,
                settings.Stage is { } stage ? (int)stage : null,
                settings.Entity,
                settings.SecondaryEntity,
                settings.ExecutionOrder,
                cancellationToken);

            switch (matches.Count)
            {
                case 0:
                    Console.MarkupLine("[red]No plugin step matches the supplied criteria.[/]");
                    return Tracer.End(this, false);
                case > 1:
                    PrintMultipleMatches(matches);
                    return Tracer.End(this, false);
                default:
                    step = matches[0];
                    break;
            }
        }

        await stepRepository.DisableAsync(step.Id, cancellationToken);
        var message = settings.Reason is not null
            ? $"[red]●[/] Disabled {Markup.Escape(step.Name)} - {settings.Reason}"
            : $"[red]●[/] Disabled {Markup.Escape(step.Name)}";
        Console.MarkupLine(message);

        // Persist reason to description if provided
        if (settings.Reason != null)
        {
            await stepRepository.UpdateDescriptionAsync(step.Id, settings.Reason, cancellationToken);
        }

        return Tracer.End(this, true);
    }

    private async Task<bool> HandleSolutionScopedAsync(
        PluginStepEnableDisableSettings settings,
        SdkMessageProcessingStepRepository stepRepository,
        CancellationToken cancellationToken)
    {
        var steps = await stepRepository.ListBySolutionAsync(settings.Solution!, cancellationToken);

        if (steps.Count == 0)
        {
            Console.MarkupLine(CultureInfo.InvariantCulture, "[yellow]No plugin steps found in solution '{0}'[/]", settings.Solution!);
            return Tracer.End(this, true);
        }

        var success = true;
        foreach (var step in steps)
        {
            try
            {
                await stepRepository.DisableAsync(step.Id, cancellationToken);
                var message = settings.Reason is not null
                    ? $"[red]●[/] Disabled {Markup.Escape(step.Name)} - {settings.Reason}"
                    : $"[red]●[/] Disabled {Markup.Escape(step.Name)}";
                Console.MarkupLine(message);

                // Persist reason to description if provided
                if (settings.Reason != null)
                {
                    await stepRepository.UpdateDescriptionAsync(step.Id, settings.Reason, cancellationToken);
                }
            }
            catch (Exception ex)
            {
                Console.MarkupLine(CultureInfo.InvariantCulture, "[red]Failed to update step {0}: {1}[/]", step.Name, ex.Message);
                success = false;
            }
        }

        return Tracer.End(this, success);
    }

    private void PrintMultipleMatches(IReadOnlyList<RemotePluginStep> matches)
    {
        Console.MarkupLine("[yellow]Multiple plugin steps match. Refine the criteria or use --step-id.[/]");

        foreach (var step in matches)
        {
            Console.WriteLine($"  {step.Name} ({step.Id})");
        }
    }
}
