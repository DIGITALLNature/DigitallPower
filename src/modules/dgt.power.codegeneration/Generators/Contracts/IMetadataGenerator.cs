using dgt.power.codegeneration.Base;

namespace dgt.power.codegeneration.Generators;

public interface IMetadataGenerator
{
    void PrepareDirectory(CodeGenerationVerb args);
    void GenerateEntities(CodeGenerationVerb args, CodeGenerationConfig config);
}
