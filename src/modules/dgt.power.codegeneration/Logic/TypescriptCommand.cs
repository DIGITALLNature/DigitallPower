using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Generators;
using dgt.power.common;
using Microsoft.Xrm.Sdk;

namespace dgt.power.codegeneration.Logic;

public class TypescriptCommand : PowerLogic<CodeGenerationVerb>
{
    private readonly ITypescriptGenerator _generator;

    public TypescriptCommand(ITracer tracer, IOrganizationService connection, IConfigResolver configResolver,
        ITypescriptGenerator generator) : base(tracer,connection, configResolver)
    {
        _generator = generator;
    }

    protected override bool Invoke(CodeGenerationVerb args)
    {
        if (!ConfigResolver.GetConfigFile<CodeGenerationConfig>(args.Config, out var config))
        {
            return Tracer.End(this, false);
        }

        _generator.PrepareDirectory(args);
        _generator.GenerateBoilerPlate(args, config);
        _generator.GenerateEntities(args, config);
        _generator.GenerateEntityRefs(args, config);
        _generator.GenerateEntityForms(args, config);
        _generator.GenerateSdkMessages(args, config);
        _generator.GenerateOptionSets(args, config);
        _generator.GenerateBusinessProcessFlows(args, config);

        return true;
    }
}
