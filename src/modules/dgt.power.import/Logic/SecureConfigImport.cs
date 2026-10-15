// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
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
    IDiagnostics diagnostics,
    IConfigResolver configResolver,
    IAnsiConsole console)
    : PowerCommand<ImportVerb>(spans, console)
{
    protected override async Task<bool> RunAsync(ImportVerb args, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(args);
        var fileName = string.IsNullOrWhiteSpace(args.FileName) ? "secureconfig.json" : args.FileName;


        if (!configResolver.TryGetConfigFile<SecureConfig>(args.FileDir, fileName, out var config) &&
            string.IsNullOrWhiteSpace(args.InlineData))
        {
            console.MarkupLine("[red]Secure config not found.[/]");
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
            diagnostics.Log("Can't find PluginStep...", TraceEventType.Warning);
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

                diagnostics.Log(e.Message, TraceEventType.Error);
                diagnostics.Log("ERROR: Could not create SecureConfig!", TraceEventType.Error);
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
                    diagnostics.Log("updated SecureConfig!", TraceEventType.Information);
                }
                catch (Exception e) when (e is not OutOfMemoryException and not StackOverflowException)
                {
                    diagnostics.Log(e.Message, TraceEventType.Error);
                    diagnostics.Log("ERROR: Could not update SecureConfig!", TraceEventType.Error);
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


        diagnostics.Log("created SecureConfig!", TraceEventType.Information);
    }
}
