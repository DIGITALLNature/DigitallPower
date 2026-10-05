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
public class PluginStepConfigSetCommand(
    ITracer tracer,
    IOrganizationService connection,
    IConfigResolver configResolver,
    IAnsiConsole console)
    : PowerLogic<PluginStepConfigSetSettings>(tracer, connection, configResolver, console)
{
    protected override async Task<bool> InvokeAsync(PluginStepConfigSetSettings settings, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(settings);
        return await InvokeCoreAsync(settings, cancellationToken);
    }

    private async Task<bool> InvokeCoreAsync(PluginStepConfigSetSettings settings, CancellationToken cancellationToken)
    {
        Tracer.Start(this);

        // Read file contents if specified
        var unsecureValue = settings.UnsecureConfig;
        if (settings.UnsecureConfigFile != null)
        {
            unsecureValue = await File.ReadAllTextAsync(settings.UnsecureConfigFile.FullName, cancellationToken);
        }

        var secureValue = settings.SecureConfig;
        if (settings.SecureConfigFile != null)
        {
            secureValue = await File.ReadAllTextAsync(settings.SecureConfigFile.FullName, cancellationToken);
        }

        var service = (IOrganizationServiceAsync2)Connection;

        // Create repositories
        var stepRepository = new SdkMessageProcessingStepRepository(service);
        var secureConfigRepository = new SdkMessageProcessingStepSecureConfigRepository(service);

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
                settings.Stage,
                settings.Entity,
                settings.SecondaryEntity,
                settings.ExecutionOrder,
                cancellationToken);

            switch (matches.Count)
            {
                case 0:
                    {
                        var criteria = new List<string>();
                        if (settings.PluginType != null) criteria.Add($"plugin-type={settings.PluginType}");
                        if (settings.Message != null) criteria.Add($"message={settings.Message}");
                        if (settings.Stage != null) criteria.Add($"stage={settings.Stage}");
                        if (settings.Entity != null) criteria.Add($"entity={settings.Entity}");
                        if (settings.SecondaryEntity != null) criteria.Add($"secondary-entity={settings.SecondaryEntity}");
                        if (settings.ExecutionOrder.HasValue) criteria.Add($"execution-order={settings.ExecutionOrder}");

                        Console.MarkupLine(CultureInfo.InvariantCulture,
                            "[red]No step found matching {0}. Verify the step exists, or use --step-id.[/]",
                            string.Join(", ", criteria));
                        return Tracer.End(this, false);
                    }
                case > 1:
                    PrintMultipleMatches(matches);
                    return Tracer.End(this, false);
                default:
                    stepId = matches[0].Id;
                    break;
            }
        }

        if (unsecureValue is not null)
        {
            await stepRepository.UpdateConfigurationAsync(stepId, unsecureValue, cancellationToken);
            Console.MarkupLine($"[green]{Emoji.Known.CheckMark}[/] Updated unsecure configuration");
        }

        if (secureValue is not null)
        {
            await secureConfigRepository.UpsertAsync(stepId, secureValue, cancellationToken);
            Console.MarkupLine($"[green]{Emoji.Known.CheckMark}[/] Updated secure configuration");
        }

        return Tracer.End(this, true);
    }

    private void PrintMultipleMatches(IReadOnlyList<RemotePluginStep> matches)
    {
        Console.MarkupLine("[yellow]Multiple steps match the provided criteria. Use --step-id or refine using --execution-order/--secondary-entity:[/]");
        Console.WriteLine();

        foreach (var step in matches)
        {
            Console.MarkupLine(CultureInfo.InvariantCulture,
                "  StepId: {0}, PluginType: {1}, Message: {2}, Stage: {3}, Entity: {4}/{5}, ExecutionOrder: {6}",
                step.Id,
                step.Name,
                step.MessageName,
                StageTypeConverter.GetStageDisplayName(step.Stage),
                step.PrimaryEntityName,
                step.SecondaryEntityName,
                step.ExecutionOrder?.ToString(CultureInfo.InvariantCulture) ?? string.Empty);
        }
    }
}
