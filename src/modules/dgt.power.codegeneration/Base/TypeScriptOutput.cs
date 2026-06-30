// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.codegeneration.Base;

/// <summary>Output configuration for TypeScript code generation.</summary>
public class TypeScriptOutput
{
    /// <summary>
    ///     Form generation configuration. When null, all forms for all scoped entities are generated.
    /// </summary>
    public TypeScriptFormsOutput? Forms { get; init; }

    /// <summary>
    ///     Generate typed Custom API wrappers for requests that have parameters. Defaults to true.
    /// </summary>
    public bool CustomApis { get; init; } = true;
}
