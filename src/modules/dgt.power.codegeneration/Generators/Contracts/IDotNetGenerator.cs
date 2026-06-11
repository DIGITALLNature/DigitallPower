// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Base;

namespace dgt.power.codegeneration.Generators.Contracts;

public interface IDotNetGenerator
{
    void PrepareDirectory(CodeGenerationVerb args);
    void GenerateRequests(CodeGenerationVerb args, DotNetCodeGenerationConfig config);
    void GenerateSdkMessageNames(CodeGenerationVerb args, DotNetCodeGenerationConfig config);
    void GenerateOptionSets(CodeGenerationVerb args, DotNetCodeGenerationConfig config);
    void GenerateContext(CodeGenerationVerb args, DotNetCodeGenerationConfig config);
    void GenerateEntities(CodeGenerationVerb args, DotNetCodeGenerationConfig config);
    void GenerateMetadata(CodeGenerationVerb args, DotNetCodeGenerationConfig config);
}
