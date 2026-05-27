// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Generators.Contracts;
using dgt.power.common;
using Microsoft.Xrm.Sdk;

namespace dgt.power.codegeneration.Logic;

public class TypescriptWorker(
    ITracer tracer,
    IOrganizationService connection,
    IConfigResolver configResolver,
    ITypescriptGeneratorFascade generatorFascade)
    : PowerWorker<CodeGenerationVerb>(tracer, connection, configResolver)
{
    protected override bool InvokeCore(CodeGenerationVerb args)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        if (!ConfigResolver.TryGetConfigFile<CodeGenerationConfig>(args.Config, out var config))
        {
            return Tracer.End(this, false);
        }

        generatorFascade.SetGenerationVersion(config.TypescriptGeneratorVersion);
        generatorFascade.PrepareDirectory(args);
        generatorFascade.GenerateEntities(args, config);
        generatorFascade.GenerateEntityForms(args, config);
        generatorFascade.GenerateOptionSets(args, config);
        generatorFascade.GenerateSdkMessages(args, config);

        if (config.TypescriptGeneratorVersion == TypescriptGeneratorVersion.Full)
        {
            generatorFascade.GenerateBoilerPlateFull(args, config);
            generatorFascade.GenerateEntityRefsFull(args, config);
            generatorFascade.GenerateBusinessProcessFlowsFull(args, config);
        }

        if (config.TypescriptGeneratorVersion == TypescriptGeneratorVersion.Light)
        {
            generatorFascade.GenerateCustomApis(args, config);
        }

        return true;
    }
}
