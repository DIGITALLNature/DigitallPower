// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common;
using dgt.power.dataverse;
using dgt.power.dto;
using dgt.power.import.Base;
using Microsoft.Xrm.Sdk;
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

        var service = (IOrganizationService)await connection.GetServiceAsync();
        using var context = new DataContext(service);
        var pluginStep = (from ps in context.SdkMessageProcessingStepSet
            where ps.Name == config.PluginStep
            select ps).SingleOrDefault();

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
                CreateSecureConfig(service, secureConfig, pluginStep, out created);
            }
            catch (Exception e) when (e is not OutOfMemoryException and not StackOverflowException)
            {
                if (created)
                {
                    service.Delete(SdkMessageProcessingStepSecureConfig.EntityLogicalName, secureConfig.Id);
                }

                Console.MarkupLine($"[red]{Markup.Escape(e.Message)}[/]");
                Console.MarkupLine("[red]ERROR: Could not create SecureConfig![/]");
                return false;
            }
        }
        else if (pluginStep.SdkMessageProcessingStepSecureConfigId != null)
        {
            secureConfig = service.Retrieve(
                pluginStep.SdkMessageProcessingStepSecureConfigId.LogicalName,
                pluginStep.SdkMessageProcessingStepSecureConfigId.Id,
                new ColumnSet(true)).ToEntity<SdkMessageProcessingStepSecureConfig>();
            if (data != secureConfig.SecureConfig)
            {
                secureConfig.SecureConfig = data;
                try
                {
                    service.Update(secureConfig);
                    Console.WriteLine("updated SecureConfig!");
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

    private void CreateSecureConfig(IOrganizationService service, SdkMessageProcessingStepSecureConfig secureConfig, SdkMessageProcessingStep pluginStep,
        out bool created)
    {
        created = false;
        secureConfig.Id = service.Create(secureConfig);
        created = true;
        service.Update(new SdkMessageProcessingStep(pluginStep.Id)
        {
            SdkMessageProcessingStepSecureConfigId = secureConfig.ToEntityReference()
        });


        Console.WriteLine("created SecureConfig!");
    }
}
