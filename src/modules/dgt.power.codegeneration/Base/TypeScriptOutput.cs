// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

namespace dgt.power.codegeneration.Base;

/// <summary>Output configuration for TypeScript code generation.</summary>
public class TypeScriptOutput
{
    /// <summary>
    ///     Form generation configuration. When null, all forms for all scoped entities are generated.
    /// </summary>
    // ReSharper disable once UnusedAutoPropertyAccessor.Global -- populated by System.Text.Json
    public TypeScriptFormsOutput? Forms { get; init; }

    /// <summary>
    ///     Generate typed Custom API wrappers for requests that have parameters. Defaults to true.
    /// </summary>
    public bool CustomApis { get; init; } = true;
}
