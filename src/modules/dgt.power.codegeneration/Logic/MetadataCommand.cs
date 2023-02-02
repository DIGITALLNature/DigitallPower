using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Generators;
using dgt.power.common;
using Microsoft.Xrm.Sdk;

namespace dgt.power.codegeneration.Logic;

public class MetadataCommand : PowerLogic<CodeGenerationVerb>
{
    private readonly IMetadataGenerator _generator;

    public MetadataCommand(ITracer tracer, IOrganizationService connection, IConfigResolver configResolver,
        IMetadataGenerator generator)
        : base(tracer,connection, configResolver)
    {
        _generator = generator;
    }


    protected override bool Invoke(CodeGenerationVerb settings)
    {
        if (!ConfigResolver.GetConfigFile<CodeGenerationConfig>(settings.Config, out var config))
        {
            return Tracer.End(this, false);
        }

        _generator.PrepareDirectory(settings);
        _generator.GenerateEntities(settings, config);

        return true;
    }
}
