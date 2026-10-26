// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

namespace dgt.power.codegeneration.Base;

/// <summary>
///     V2 configuration for TypeScript code generation.
///     Produces entity definitions, form typings, OptionSet constants,
///     SDK message names, and Custom API wrappers.
/// </summary>
public class TypeScriptCodeGenerationConfig : CodeGenerationConfigBase
{
    /// <summary>Output configuration — controls which artifacts are generated.</summary>
    public TypeScriptOutput Output { get; init; } = new();
}
