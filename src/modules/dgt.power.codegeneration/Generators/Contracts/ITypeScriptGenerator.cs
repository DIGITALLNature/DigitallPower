// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using dgt.power.codegeneration.Base;

namespace dgt.power.codegeneration.Generators.Contracts;

public interface ITypeScriptGenerator
{
    /// <summary>
    ///     Runs the TypeScript code generation pipeline for a typed configuration.
    /// </summary>
    Task<bool> GenerateAsync(CodeGenerationVerb args, TypeScriptCodeGenerationConfig config, CancellationToken cancellationToken = default);
}
