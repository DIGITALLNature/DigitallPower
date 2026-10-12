// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using dgt.power.common;
using dgt.power.dataverse;
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
            var result = await ProcessStepAsync(stepConfig, stepRepository, cancellationToken);
            if (result.IsSuccess)
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
            else
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
        SdkMessageProcessingStepRepository stepRepository,
        CancellationToken cancellationToken)
    {
        string? stepName;
        Guid stepId;

        if (stepConfig.StepId.HasValue)
        {
            stepId = stepConfig.StepId.Value;

            // Retrieve step to verify it exists and get the name
            try
            {
                var entity = await ((IOrganizationServiceAsync2)Connection).RetrieveAsync(
                    SdkMessageProcessingStep.EntityLogicalName,
                    stepId,
                    new ColumnSet(SdkMessageProcessingStep.LogicalNames.Name),
                    cancellationToken);
                stepName = entity.GetAttributeValue<string>(SdkMessageProcessingStep.LogicalNames.Name);
            }
            catch (Exception)
            {
                Console.MarkupLine(CultureInfo.InvariantCulture, "[red]Step with id '{0}' not found[/]", stepId);
                return StepProcessResult.Failure;
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
                    return StepProcessResult.Failure;
                case > 1:
                    Console.MarkupLine("[yellow]Multiple plugin steps match. Refine the criteria or use stepId.[/]");
                    foreach (var step in matches)
                    {
                        Console.WriteLine($"  {step.Name} ({step.Id})");
                    }
                    return StepProcessResult.Failure;
                default:
                    stepId = matches[0].Id;
                    stepName = matches[0].Name;
                    break;
            }
        }

        // Apply the state change
        if (stepConfig.Enabled)
        {
            await stepRepository.EnableAsync(stepId, cancellationToken);
            var message = stepConfig.Reason is not null
                ? $"[green]●[/] Enabled {Markup.Escape(stepName ?? stepId.ToString())} - {stepConfig.Reason}"
                : $"[green]●[/] Enabled {Markup.Escape(stepName ?? stepId.ToString())}";
            Console.MarkupLine(message);
        }
        else
        {
            await stepRepository.DisableAsync(stepId, cancellationToken);
            var message = stepConfig.Reason is not null
                ? $"[red]●[/] Disabled {Markup.Escape(stepName ?? stepId.ToString())} - {stepConfig.Reason}"
                : $"[red]●[/] Disabled {Markup.Escape(stepName ?? stepId.ToString())}";
            Console.MarkupLine(message);
        }

        // Persist reason to description if provided
        if (stepConfig.Reason != null)
        {
            var step = new SdkMessageProcessingStep(stepId)
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

    private struct StepProcessResult
    {
        public static readonly StepProcessResult Success = new(true);
        public static readonly StepProcessResult Failure = new(false);

        public bool IsSuccess { get; }

        private StepProcessResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }
}
