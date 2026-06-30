// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.codegeneration.Base;

/// <summary>
///     Result of config resolution. Either a single V2 typed config or a V1 legacy config
///     (which may produce both .NET and TypeScript output in a single run).
/// </summary>
#pragma warning disable CA1034 // Nested types — intentional discriminated union: private ctor prevents external subclassing
public abstract record CodeGenerationConfigResult
{
    private CodeGenerationConfigResult() { }

    /// <summary>
    ///     A V2 typed config targeting a single output (dotnet or typescript).
    /// </summary>
    public sealed record V2(CodeGenerationConfigBase Config) : CodeGenerationConfigResult;

    /// <summary>
    ///     A V1 legacy config that may target multiple outputs (dotnet + typescript).
    /// </summary>
    public sealed record V1(CodeGenerationConfig Config) : CodeGenerationConfigResult;
}
#pragma warning restore CA1034

