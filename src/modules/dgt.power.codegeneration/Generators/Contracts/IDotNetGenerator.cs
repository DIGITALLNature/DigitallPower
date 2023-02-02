using dgt.power.codegeneration.Base;

namespace dgt.power.codegeneration.Generators;

public interface IDotNetGenerator
{
    void PrepareDirectory(CodeGenerationVerb args);
    void GenerateActions(CodeGenerationVerb args, CodeGenerationConfig config);
    void GenerateSdkMessages(CodeGenerationVerb args, CodeGenerationConfig config);
    void GenerateOptionSets(CodeGenerationVerb args, CodeGenerationConfig config);
    void GenerateContext(CodeGenerationVerb args, CodeGenerationConfig config);
    void GenerateEntities(CodeGenerationVerb args, CodeGenerationConfig config);
}
