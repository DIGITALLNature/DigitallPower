// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

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
    protected override Task<bool> InvokeAsync(PluginStepConfigSetSettings settings, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(settings);
        return InvokeCoreAsync(settings, cancellationToken);
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
        Console.MarkupLine("[yellow]Multiple plugin steps match. Refine the criteria or use --step-id.[/]");

        foreach (var step in matches)
        {
            Console.WriteLine($"  {step.Name} ({step.Id})");
        }
    }
}
