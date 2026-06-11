// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Base;

namespace dgt.power.codegeneration.Generators.Contracts;

/// <summary>
///     Internal strategy contract for TypeScript code generation.
///     Each strategy (Full / Light) encapsulates its own generation pipeline.
/// </summary>
internal interface ITypescriptGenerationStrategy
{
    /// <summary>
    ///     Runs the full TypeScript code generation pipeline for a V1 legacy config.
    /// </summary>
    bool Generate(CodeGenerationVerb args, CodeGenerationConfig config);
}
