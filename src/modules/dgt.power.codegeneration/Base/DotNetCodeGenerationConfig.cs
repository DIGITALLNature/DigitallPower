// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

namespace dgt.power.codegeneration.Base;

/// <summary>
///     V2 configuration for .NET code generation. Produces C# early-bound entity classes,
///     typed request/response wrappers, SDK message constants, and optionally XML metadata.
/// </summary>
public class DotNetCodeGenerationConfig : CodeGenerationConfigBase
{
    /// <summary>C# namespace for generated classes.</summary>
    public new string Namespace { get; init; } = "Digitall.Dataverse.Model";

    /// <summary>Output configuration — controls target framework and which artifacts are generated.</summary>
    public DotNetOutput Output { get; init; } = new();
}
