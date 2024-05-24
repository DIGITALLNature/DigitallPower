// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Base;

namespace dgt.power.codegeneration.Generators;

public interface IMetadataGenerator
{
    void PrepareDirectory(CodeGenerationVerb args);
    void GenerateEntities(CodeGenerationVerb args, CodeGenerationConfig config);
}
