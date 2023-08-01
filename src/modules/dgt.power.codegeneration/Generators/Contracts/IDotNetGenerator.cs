// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

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
