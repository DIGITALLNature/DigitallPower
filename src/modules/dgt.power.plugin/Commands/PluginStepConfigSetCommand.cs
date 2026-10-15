// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common;
using dgt.power.plugin.Models;
using dgt.power.plugin.Repositories;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Spectre.Console;

namespace dgt.power.plugin.Commands;

// ReSharper disable once ClassNeverInstantiated.Global
public class PluginStepConfigSetCommand(
    ICommandSpanFactory spans,
    IDataverseConnection connection,
    IAnsiConsole console)
    : PowerCommand<PluginStepConfigSetSettings>(spans, console)
{
    protected override async Task<bool> RunAsync(PluginStepConfigSetSettings settings, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(settings);

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

        var service = await connection.GetServiceAsync();

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
                settings.Stage is { } stage ? (int)stage : null,
                settings.Entity,
                settings.SecondaryEntity,
                settings.ExecutionOrder,
                cancellationToken);

            switch (matches.Count)
            {
                case 0:
                    console.MarkupLine("[red]No plugin step matches the supplied criteria.[/]");
                    return false;
                case > 1:
                    PrintMultipleMatches(matches);
                    return false;
                default:
                    stepId = matches[0].Id;
                    break;
            }
        }

        if (unsecureValue is not null)
        {
            await stepRepository.UpdateConfigurationAsync(stepId, unsecureValue, cancellationToken);
            console.MarkupLine($"[green]{Emoji.Known.CheckMark}[/] Updated unsecure configuration");
        }

        if (secureValue is not null)
        {
            await secureConfigRepository.UpsertAsync(stepId, secureValue, cancellationToken);
            console.MarkupLine($"[green]{Emoji.Known.CheckMark}[/] Updated secure configuration");
        }

        return true;
    }

    private void PrintMultipleMatches(IReadOnlyList<RemotePluginStep> matches)
    {
        console.MarkupLine("[yellow]Multiple plugin steps match. Refine the criteria or use --step-id.[/]");

        foreach (var step in matches)
        {
            console.WriteLine($"  {step.Name} ({step.Id})");
        }
    }
}
