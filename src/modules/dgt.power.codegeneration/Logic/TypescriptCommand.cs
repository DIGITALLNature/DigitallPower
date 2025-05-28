// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Generators;
using dgt.power.common;
using dgt.power.common.Logic;

namespace dgt.power.codegeneration.Logic;

public class TypescriptCommand : PowerLogic<CodeGenerationVerb>
{
    private readonly ITypescriptGenerator _generator;

    public TypescriptCommand(ITracer tracer, IXrmConnectionFactory xrmConnectionFactory, IConfigResolver configResolver,
        ITypescriptGenerator generator) : base(tracer, xrmConnectionFactory, configResolver)
    {
        _generator = generator;
    }

    protected override bool Invoke(CodeGenerationVerb args)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        if (!ConfigResolver.TryGetConfigFile<CodeGenerationConfig>(args.Config, out var config))
        {
            return Tracer.End(this, false);
        }

        _generator.PrepareDirectory(args);
        _generator.GenerateEntities(args, config);
        _generator.GenerateEntityForms(args, config);
        _generator.GenerateOptionSets(args, config);
        _generator.GenerateSdkMessages(args, config);

        if (config.TypescriptGeneratorVersion == TypescriptGeneratorVersion.Full)
        {
            _generator.GenerateBoilerPlateFull(args, config);
            _generator.GenerateEntityRefsFull(args, config);
            _generator.GenerateBusinessProcessFlowsFull(args, config);
        }


        return true;
    }
}
