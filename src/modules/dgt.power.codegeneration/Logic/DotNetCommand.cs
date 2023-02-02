using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Generators;
using dgt.power.common;
using Microsoft.Xrm.Sdk;

namespace dgt.power.codegeneration.Logic;

public class DotNetCommand : PowerLogic<CodeGenerationVerb>
{
    private readonly IDotNetGenerator _generator;

    public DotNetCommand(ITracer tracer, IOrganizationService connection, IConfigResolver configResolver, IDotNetGenerator generator)
        : base(tracer, connection, configResolver)
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
        _generator.GenerateActions(args, config);
        _generator.GenerateSdkMessages(args, config);
        _generator.GenerateOptionSets(args, config);
        _generator.GenerateContext(args, config);
        _generator.GenerateEntities(args, config);
        return true;
    }
}
