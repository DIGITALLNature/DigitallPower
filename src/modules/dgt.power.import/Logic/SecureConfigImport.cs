// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using dgt.power.common;
using dgt.power.dataverse;
using dgt.power.dto;
using dgt.power.import.Base;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

// ReSharper disable ClassNeverInstantiated.Global

namespace dgt.power.import.Logic;

public sealed class SecureConfigImport : BaseImport
{
    public SecureConfigImport(ITracer tracer, IOrganizationService connection, IConfigResolver configResolver) : base(tracer,
        connection, configResolver)
    {
    }

    protected override bool Invoke(ImportVerb args)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Tracer.Start(this);
        var fileName = string.IsNullOrWhiteSpace(args.FileName) ? "secureconfig.json" : args.FileName;


        if (!ConfigResolver.TryGetConfigFile<SecureConfig>(args.FileDir, fileName, out var config) &&
            string.IsNullOrWhiteSpace(args.InlineData))
        {
            return Tracer.NotConfigured(this);
        }

        var data = config.Data ?? args.InlineData;

        using var context = new DataContext(Connection);
        var pluginStep = (from ps in context.SdkMessageProcessingStepSet
            where ps.Name == config.PluginStep
            select ps).SingleOrDefault();

        if (pluginStep == default)
        {
            Tracer.Log("Can't find PluginStep...", TraceEventType.Warning);
            return Tracer.NotConfigured(this);
        }

        SdkMessageProcessingStepSecureConfig secureConfig;
        //create versus update
        if (pluginStep.SdkMessageProcessingStepSecureConfigId == null && !string.IsNullOrWhiteSpace(data))
        {
            secureConfig = new SdkMessageProcessingStepSecureConfig { SecureConfig = data };
            var created = false;
            try
            {
                CreateSecureConfig(secureConfig, pluginStep, out created);
            }
            catch (Exception e)
            {
                if (created)
                {
                    Connection.Delete(SdkMessageProcessingStepSecureConfig.EntityLogicalName, secureConfig.Id);
                }

                Tracer.Log(e.Message, TraceEventType.Error);
                Tracer.Log("ERROR: Could not create SecureConfig!", TraceEventType.Error);
                return Tracer.End(this, false);
            }
        }
        else if (pluginStep.SdkMessageProcessingStepSecureConfigId != null)
        {
            secureConfig = Connection.Retrieve(
                pluginStep.SdkMessageProcessingStepSecureConfigId.LogicalName,
                pluginStep.SdkMessageProcessingStepSecureConfigId.Id,
                new ColumnSet(true)).ToEntity<SdkMessageProcessingStepSecureConfig>();
            if (data != secureConfig.SecureConfig)
            {
                secureConfig.SecureConfig = data;
                try
                {
                    Connection.Update(secureConfig);
                    Tracer.Log("updated SecureConfig!", TraceEventType.Information);
                }
                catch (Exception e)
                {
                    Tracer.Log(e.Message, TraceEventType.Error);
                    Tracer.Log("ERROR: Could not update SecureConfig!", TraceEventType.Error);
                    return Tracer.End(this, false);
                }
            }
        }

        return Tracer.End(this, true);
    }

    private void CreateSecureConfig(SdkMessageProcessingStepSecureConfig secureConfig, SdkMessageProcessingStep pluginStep, out bool created)
    {
        created = false;
        secureConfig.Id = Connection.Create(secureConfig);
        created = true;
        Connection.Update(new SdkMessageProcessingStep(pluginStep.Id)
        {
            SdkMessageProcessingStepSecureConfigId = secureConfig.ToEntityReference()
        });


        Tracer.Log("created SecureConfig!", TraceEventType.Information);
    }
}
