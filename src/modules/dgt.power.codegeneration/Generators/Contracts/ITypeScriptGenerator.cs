// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Base;

namespace dgt.power.codegeneration.Generators.Contracts;

public interface ITypeScriptGenerator
{
    /// <summary>
    ///     Runs the TypeScript code generation pipeline for a V2 config (Light mode only).
    /// </summary>
    bool Generate(CodeGenerationVerb args, TypeScriptCodeGenerationConfig config);

    /// <summary>
    ///     Runs the TypeScript code generation pipeline for a V1 legacy config (Full or Light mode).
    /// </summary>
    [Obsolete("Use Generate(CodeGenerationVerb, TypeScriptCodeGenerationConfig) with V2 config. V1 support will be removed in a future major version.")]
    bool Generate(CodeGenerationVerb args, CodeGenerationConfig config);
}
