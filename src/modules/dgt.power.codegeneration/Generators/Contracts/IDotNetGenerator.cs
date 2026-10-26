// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using dgt.power.codegeneration.Base;

namespace dgt.power.codegeneration.Generators.Contracts;

public interface IDotNetGenerator
{
    /// <summary>
    ///     Runs the full .NET code generation pipeline for the given config.
    /// </summary>
    Task<bool> GenerateAsync(CodeGenerationVerb args, DotNetCodeGenerationConfig config, CancellationToken cancellationToken = default);
}
