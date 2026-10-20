// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common;
using dgt.power.common.Extensions;
using dgt.power.dataverse;
using dgt.power.dto;
using dgt.power.import.Base;
using Microsoft.Xrm.Sdk.Query;
using Spectre.Console;

// ReSharper disable ClassNeverInstantiated.Global

namespace dgt.power.import.Logic;

public sealed class SecureConfigImport(
    ICommandSpanFactory spans,
    IDataverseConnection connection,
    IConfigResolver configResolver,
    IAnsiConsole console)
    : PowerCommand<ImportVerb>(spans, console)
{
    protected override async Task<bool> RunAsync(ImportVerb args, CancellationToken cancellationToken)
    {
        var fileName = string.IsNullOrWhiteSpace(args.FileName) ? "secureconfig.json" : args.FileName;


        if (!configResolver.TryGetConfigFile<SecureConfig>(args.FileDir, fileName, out var config) &&
            string.IsNullOrWhiteSpace(args.InlineData))
        {
            Console.MarkupLine("[red]Secure config not found.[/]");
            return false;
        }

        var data = config.Data ?? args.InlineData;
        if (string.IsNullOrWhiteSpace(config.PluginStep))
        {
            Console.MarkupLine("[yellow]Can't find PluginStep...[/]");
            return false;
        }

        var service = await connection.GetServiceAsync();
        var pluginStepQuery = new QueryExpression(SdkMessageProcessingStep.EntityLogicalName)
        {
            NoLock = true,
            ColumnSet = new ColumnSet(
                SdkMessageProcessingStep.LogicalNames.SdkMessageProcessingStepId,
                SdkMessageProcessingStep.LogicalNames.Name,
                SdkMessageProcessingStep.LogicalNames.SdkMessageProcessingStepSecureConfigId),
            Criteria =
            {
                Conditions =
                {
                    new ConditionExpression(SdkMessageProcessingStep.LogicalNames.Name, ConditionOperator.Equal, config.PluginStep)
                }
            }
        };
        var pluginStep = (await service.RetrieveAllAsync(pluginStepQuery, cancellationToken: cancellationToken))
            .SingleOrDefault()?.ToEntity<SdkMessageProcessingStep>();

        if (pluginStep == null)
        {
            Console.MarkupLine("[yellow]Can't find PluginStep...[/]");
            return false;
        }

        SdkMessageProcessingStepSecureConfig secureConfig;
        //create versus update
        if (pluginStep.SdkMessageProcessingStepSecureConfigId == null && !string.IsNullOrWhiteSpace(data))
        {
            secureConfig = new SdkMessageProcessingStepSecureConfig { SecureConfig = data };
            var created = false;
            try
            {
                secureConfig.Id = await service.CreateAsync(secureConfig, cancellationToken);
                created = true;
                await service.UpdateAsync(new SdkMessageProcessingStep(pluginStep.Id)
                {
                    SdkMessageProcessingStepSecureConfigId = secureConfig.ToEntityReference()
                }, cancellationToken);
                Console.WriteLine("created SecureConfig!");
            }
            catch (OperationCanceledException)
            {
                if (created)
                {
                    await service.DeleteAsync(SdkMessageProcessingStepSecureConfig.EntityLogicalName, secureConfig.Id, CancellationToken.None);
                }

                throw;
            }
            catch (Exception e) when (e is not OutOfMemoryException and not StackOverflowException)
            {
                if (created)
                {
                    await service.DeleteAsync(SdkMessageProcessingStepSecureConfig.EntityLogicalName, secureConfig.Id, cancellationToken);
                }

                Console.MarkupLine($"[red]{Markup.Escape(e.Message)}[/]");
                Console.MarkupLine("[red]ERROR: Could not create SecureConfig![/]");
                return false;
            }
        }
        else if (pluginStep.SdkMessageProcessingStepSecureConfigId != null)
        {
            secureConfig = (await service.RetrieveAsync(
                pluginStep.SdkMessageProcessingStepSecureConfigId.LogicalName,
                pluginStep.SdkMessageProcessingStepSecureConfigId.Id,
                new ColumnSet(true),
                cancellationToken)).ToEntity<SdkMessageProcessingStepSecureConfig>();
            if (data != secureConfig.SecureConfig)
            {
                secureConfig.SecureConfig = data;
                try
                {
                    await service.UpdateAsync(secureConfig, cancellationToken);
                    Console.WriteLine("updated SecureConfig!");
                }
                catch (OperationCanceledException)
                {
                    throw;
                }
                catch (Exception e) when (e is not OutOfMemoryException and not StackOverflowException)
                {
                    Console.MarkupLine($"[red]{Markup.Escape(e.Message)}[/]");
                    Console.MarkupLine("[red]ERROR: Could not update SecureConfig![/]");
                    return false;
                }
            }
        }

        return true;
    }

}
