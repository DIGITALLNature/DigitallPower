// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.codegeneration.Base;

/// <summary>
///     V2 configuration for TypeScript code generation (Light mode only).
///     Produces entity definitions, form typings, OptionSet constants,
///     SDK message names, and Custom API wrappers.
/// </summary>
public class TypeScriptCodeGenerationConfig : CodeGenerationConfigBase
{
    /// <summary>Output configuration — controls which artifacts are generated.</summary>
    public TypeScriptOutput Output { get; init; } = new();
}
