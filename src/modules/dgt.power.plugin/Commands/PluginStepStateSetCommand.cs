// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using System.Text.Json;
using dgt.power.common;
using dgt.power.dataverse;
using dgt.power.plugin.Remote;
using dgt.power.plugin.Repositories;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
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

        if (!configResolver.TryGetConfigFile<PluginStepStateConfigRoot>(settings.Config, out var config))
        {
            Console.MarkupLine("[red]Failed to load or parse config file.[/]");
            return Tracer.End(this, false);
        }

        if (config.Steps.Count == 0)
        {
            Console.MarkupLine("[yellow]No steps defined in config file.[/]");
            return Tracer.End(this, true);
        }

        var service = (IOrganizationServiceAsync2)Connection;
        var stepRepository = new SdkMessageProcessingStepRepository(service);

        var success = true;
        var enabledCount = 0;
        var disabledCount = 0;
        var errorCount = 0;

        foreach (var stepConfig in config.Steps)
        {
            var result = await ProcessStepAsync(stepConfig, stepRepository, settings.PersistReason, cancellationToken);
            if (result == StepProcessResult.Success)
            {
                if (stepConfig.Enabled)
                {
                    enabledCount++;
                }
                else
                {
                    disabledCount++;
                }
            }
            else if (result == StepProcessResult.NotFound)
            {
                errorCount++;
                success = false;
            }
            else if (result == StepProcessResult.MultipleMatches)
            {
                errorCount++;
                success = false;
            }
        }

        Console.MarkupLine(string.Empty);
        Console.MarkupLine($"[green]{Emoji.Known.CheckMark}[/] {enabledCount} steps enabled");
        Console.MarkupLine($"[green]{Emoji.Known.CheckMark}[/] {disabledCount} steps disabled");

        if (errorCount > 0)
        {
            Console.MarkupLine($"[red]{Emoji.Known.CrossMark}[/] {errorCount} steps failed");
        }

        return Tracer.End(this, success && errorCount == 0);
    }

    private async Task<StepProcessResult> ProcessStepAsync(
        PluginStepStateConfig stepConfig,
        ISdkMessageProcessingStepRepository stepRepository,
        bool persistReason,
        CancellationToken cancellationToken)
    {
        // Resolve step id
        Guid stepId;
        if (stepConfig.StepId.HasValue)
        {
            stepId = stepConfig.StepId.Value;

            // Verify step exists by trying to retrieve it
            try
            {
                await ((IOrganizationServiceAsync2)Connection).RetrieveAsync(
                    dgt.power.dataverse.SdkMessageProcessingStep.EntityLogicalName,
                    stepId,
                    new ColumnSet(dgt.power.dataverse.SdkMessageProcessingStep.LogicalNames.Name),
                    cancellationToken);
            }
            catch
            {
                Console.MarkupLine(CultureInfo.InvariantCulture, "[red]Step with id '{0}' not found[/]", stepId);
                return StepProcessResult.NotFound;
            }
        }
        else
        {
            var matches = await stepRepository.FindByCompositeKeyAsync(
                stepConfig.PluginType,
                stepConfig.Message,
                stepConfig.Stage,
                stepConfig.Entity,
                stepConfig.SecondaryEntity,
                stepConfig.ExecutionOrder,
                cancellationToken);

            switch (matches.Count)
            {
                case 0:
                    var compositeKey = BuildCompositeKeyDescription(stepConfig);
                    Console.MarkupLine(CultureInfo.InvariantCulture, "[red]No plugin step matches: {0}[/]", compositeKey);
                    return StepProcessResult.NotFound;
                case > 1:
                    Console.MarkupLine("[yellow]Multiple plugin steps match. Refine the criteria or use stepId.[/]");
                    foreach (var step in matches)
                    {
                        Console.WriteLine($"  {step.Name} ({step.Id})");
                    }
                    return StepProcessResult.MultipleMatches;
                default:
                    stepId = matches[0].Id;
                    break;
            }
        }

        // Apply the state change
        if (stepConfig.Enabled)
        {
            await stepRepository.EnableAsync(stepId, cancellationToken);
            Console.MarkupLine($"[green]Enabled step {stepId}[/]" + (stepConfig.Reason is not null ? $" - {stepConfig.Reason}" : string.Empty));
        }
        else
        {
            await stepRepository.DisableAsync(stepId, cancellationToken);
            Console.MarkupLine($"[green]Disabled step {stepId}[/]" + (stepConfig.Reason is not null ? $" - {stepConfig.Reason}" : string.Empty));
        }

        // Optionally persist reason to description
        if (persistReason && stepConfig.Reason != null)
        {
            var step = new dgt.power.dataverse.SdkMessageProcessingStep(stepId)
            {
                Description = stepConfig.Reason
            };
            await ((IOrganizationServiceAsync2)Connection).UpdateAsync(step, cancellationToken);
        }

        return StepProcessResult.Success;
    }

    private static string BuildCompositeKeyDescription(PluginStepStateConfig stepConfig)
    {
        var parts = new List<string>();
        if (!string.IsNullOrEmpty(stepConfig.PluginType))
        {
            parts.Add(stepConfig.PluginType);
        }
        if (!string.IsNullOrEmpty(stepConfig.Message))
        {
            parts.Add(stepConfig.Message);
        }
        if (stepConfig.Stage.HasValue)
        {
            parts.Add(stepConfig.Stage.Value.ToString(CultureInfo.InvariantCulture));
        }
        if (!string.IsNullOrEmpty(stepConfig.Entity))
        {
            parts.Add(stepConfig.Entity);
        }
        if (!string.IsNullOrEmpty(stepConfig.SecondaryEntity))
        {
            parts.Add(stepConfig.SecondaryEntity);
        }
        if (stepConfig.ExecutionOrder.HasValue)
        {
            parts.Add(stepConfig.ExecutionOrder.Value.ToString(CultureInfo.InvariantCulture));
        }
        return string.Join(", ", parts);
    }

    private enum StepProcessResult
    {
        Success,
        NotFound,
        MultipleMatches
    }
}
