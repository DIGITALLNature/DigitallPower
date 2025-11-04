// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Base;

namespace dgt.power.codegeneration.Generators.Contracts;

public interface ITypescriptGeneratorFascade : ITypescriptGenerator
{ 
    void SetGenerationVersion(TypescriptGeneratorVersion generatorVersion);
}
