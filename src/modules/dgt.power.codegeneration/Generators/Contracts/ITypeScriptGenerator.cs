// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Base;

namespace dgt.power.codegeneration.Generators.Contracts;

public interface ITypeScriptGenerator
{
    /// <summary>
    ///     Runs the TypeScript code generation pipeline for a typed configuration.
    /// </summary>
    Task<bool> GenerateAsync(CodeGenerationVerb args, TypeScriptCodeGenerationConfig config, CancellationToken cancellationToken = default);
}
