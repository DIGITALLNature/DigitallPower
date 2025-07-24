// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Generators;
using dgt.power.codegeneration.Generators.Contracts;
using dgt.power.common;
using Microsoft.Xrm.Sdk;

namespace dgt.power.codegeneration.Logic;

public class TypescriptCommand : PowerLogic<CodeGenerationVerb>
{
    private readonly ITypescriptGeneratorFascade _generatorFascade;

    public TypescriptCommand(ITracer tracer, IOrganizationService connection, IConfigResolver configResolver,
        ITypescriptGeneratorFascade generatorFascade) : base(tracer,connection, configResolver)
    {
        _generatorFascade = generatorFascade;
    }

    protected override bool Invoke(CodeGenerationVerb args)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        if (!ConfigResolver.TryGetConfigFile<CodeGenerationConfig>(args.Config, out var config))
        {
            return Tracer.End(this, false);
        }

        _generatorFascade.SetGenerationVersion(config.TypescriptGeneratorVersion);
        _generatorFascade.PrepareDirectory(args);
        _generatorFascade.GenerateEntities(args, config);
        _generatorFascade.GenerateEntityForms(args, config);
        _generatorFascade.GenerateOptionSets(args, config);
        _generatorFascade.GenerateSdkMessages(args, config);

        if (config.TypescriptGeneratorVersion == TypescriptGeneratorVersion.Full)
        {
            _generatorFascade.GenerateBoilerPlateFull(args, config);
            _generatorFascade.GenerateEntityRefsFull(args, config);
            _generatorFascade.GenerateBusinessProcessFlowsFull(args, config);
        }


        return true;
    }
}
