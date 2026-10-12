// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common;
using dgt.power.plugin.Remote;
using dgt.power.plugin.Repositories;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Spectre.Console;

namespace dgt.power.plugin.Commands;

/// <summary>
/// Base class for plugin step enable/disable commands.
/// </summary>
// ReSharper disable once ClassNeverInstantiated.Global
public abstract class PluginStepEnableDisableCommandBase(
    ITracer tracer,
    IOrganizationService connection,
    IConfigResolver configResolver,
    IAnsiConsole console)
    : PowerLogic<PluginStepEnableDisableSettings>(tracer, connection, configResolver, console)
{
    /// <summary>
    /// Returns true to enable, false to disable.
    /// </summary>
    protected abstract bool ShouldEnable { get; }

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

        // Resolve step id
        Guid stepId;
        if (settings.StepId.HasValue)
        {
            stepId = settings.StepId.Value;
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
                    stepId = matches[0].Id;
                    break;
            }
        }

        if (ShouldEnable)
        {
            await stepRepository.EnableAsync(stepId, cancellationToken);
            Console.MarkupLine($"[green]{Emoji.Known.CheckMark}[/] Plugin step enabled");
        }
        else
        {
            await stepRepository.DisableAsync(stepId, cancellationToken);
            Console.MarkupLine($"[green]{Emoji.Known.CheckMark}[/] Plugin step disabled");
        }

        return Tracer.End(this, true);
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

// ReSharper disable once ClassNeverInstantiated.Global
public class PluginStepEnableCommand(
    ITracer tracer,
    IOrganizationService connection,
    IConfigResolver configResolver,
    IAnsiConsole console)
    : PluginStepEnableDisableCommandBase(tracer, connection, configResolver, console)
{
    protected override bool ShouldEnable => true;
}

// ReSharper disable once ClassNeverInstantiated.Global
public class PluginStepDisableCommand(
    ITracer tracer,
    IOrganizationService connection,
    IConfigResolver configResolver,
    IAnsiConsole console)
    : PluginStepEnableDisableCommandBase(tracer, connection, configResolver, console)
{
    protected override bool ShouldEnable => false;
}
