// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using dgt.power.common;
using dgt.power.dataverse;
using dgt.power.plugin.Remote;
using dgt.power.plugin.Repositories;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
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

        // Handle solution-scoped mode
        if (!string.IsNullOrWhiteSpace(settings.Solution))
        {
            return await HandleSolutionScopedAsync(settings, stepRepository, service, cancellationToken);
        }

        // Resolve step id and get step name
        Guid stepId;
        string? stepName = null;

        if (settings.StepId.HasValue)
        {
            stepId = settings.StepId.Value;
            try
            {
                var entity = await service.RetrieveAsync(
                    SdkMessageProcessingStep.EntityLogicalName,
                    stepId,
                    new ColumnSet(SdkMessageProcessingStep.LogicalNames.Name),
                    cancellationToken);
                stepName = entity.GetAttributeValue<string>(SdkMessageProcessingStep.LogicalNames.Name);
            }
            catch
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
                    stepId = matches[0].Id;
                    stepName = matches[0].Name;
                    break;
            }
        }

        if (ShouldEnable)
        {
            await stepRepository.EnableAsync(stepId, cancellationToken);
            var message = settings.Reason is not null
                ? $"[green]●[/] Enabled {Markup.Escape(stepName ?? stepId.ToString())} - {settings.Reason}"
                : $"[green]●[/] Enabled {Markup.Escape(stepName ?? stepId.ToString())}";
            Console.MarkupLine(message);
        }
        else
        {
            await stepRepository.DisableAsync(stepId, cancellationToken);
            var message = settings.Reason is not null
                ? $"[red]●[/] Disabled {Markup.Escape(stepName ?? stepId.ToString())} - {settings.Reason}"
                : $"[red]●[/] Disabled {Markup.Escape(stepName ?? stepId.ToString())}";
            Console.MarkupLine(message);
        }

        // Persist reason to description if provided
        if (settings.Reason != null)
        {
            var step = new SdkMessageProcessingStep(stepId)
            {
                Description = settings.Reason
            };
            await service.UpdateAsync(step, cancellationToken);
        }

        return Tracer.End(this, true);
    }

    private async Task<bool> HandleSolutionScopedAsync(
        PluginStepEnableDisableSettings settings,
        ISdkMessageProcessingStepRepository stepRepository,
        IOrganizationServiceAsync2 service,
        CancellationToken cancellationToken)
    {
        var steps = await RetrieveStepsFromSolutionAsync(settings.Solution!, service, cancellationToken);

        if (steps.Length == 0)
        {
            Console.MarkupLine(CultureInfo.InvariantCulture, "[yellow]No plugin steps found in solution '{0}'[/]", settings.Solution!);
            return Tracer.End(this, true);
        }

        var success = true;
        foreach (var step in steps)
        {
            try
            {
                if (ShouldEnable)
                {
                    await stepRepository.EnableAsync(step.Id, cancellationToken);
                    var message = settings.Reason is not null
                        ? $"[green]●[/] Enabled {Markup.Escape(step.Name ?? step.Id.ToString())} - {settings.Reason}"
                        : $"[green]●[/] Enabled {Markup.Escape(step.Name ?? step.Id.ToString())}";
                    Console.MarkupLine(message);
                }
                else
                {
                    await stepRepository.DisableAsync(step.Id, cancellationToken);
                    var message = settings.Reason is not null
                        ? $"[red]●[/] Disabled {Markup.Escape(step.Name ?? step.Id.ToString())} - {settings.Reason}"
                        : $"[red]●[/] Disabled {Markup.Escape(step.Name ?? step.Id.ToString())}";
                    Console.MarkupLine(message);
                }

                // Persist reason to description if provided
                if (settings.Reason != null)
                {
                    var stepEntity = new SdkMessageProcessingStep(step.Id)
                    {
                        Description = settings.Reason
                    };
                    await service.UpdateAsync(stepEntity, cancellationToken);
                }
            }
            catch (Exception ex)
            {
                Console.MarkupLine(CultureInfo.InvariantCulture, "[red]Failed to update step {0}: {1}[/]", step.Name ?? step.Id.ToString(), ex.Message);
                success = false;
            }
        }

        return Tracer.End(this, success);
    }

    private async Task<SdkMessageProcessingStep[]> RetrieveStepsFromSolutionAsync(
        string solutionName,
        IOrganizationServiceAsync2 service,
        CancellationToken cancellationToken)
    {
        var query = new QueryExpression(SdkMessageProcessingStep.EntityLogicalName)
        {
            ColumnSet = new ColumnSet(
                SdkMessageProcessingStep.LogicalNames.Name,
                SdkMessageProcessingStep.LogicalNames.StateCode
            )
        };

        var componentLink = query.AddLink(
            SolutionComponent.EntityLogicalName,
            SdkMessageProcessingStep.LogicalNames.SdkMessageProcessingStepId,
            SolutionComponent.LogicalNames.ObjectId);
        var solutionLink = componentLink.AddLink(
            Solution.EntityLogicalName,
            SolutionComponent.LogicalNames.SolutionId,
            Solution.LogicalNames.SolutionId);
        solutionLink.EntityAlias = "solution";

        query.Criteria.AddCondition("solution", Solution.LogicalNames.UniqueName, ConditionOperator.Like, solutionName);

        var retrieveMultipleRequest = new RetrieveMultipleRequest { Query = query };
        var response = await service.ExecuteAsync(retrieveMultipleRequest, cancellationToken);

        return ((RetrieveMultipleResponse)response).EntityCollection.Entities
            .Cast<SdkMessageProcessingStep>()
            .ToArray();
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
