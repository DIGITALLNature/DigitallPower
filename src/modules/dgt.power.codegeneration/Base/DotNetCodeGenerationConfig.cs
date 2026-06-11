// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.codegeneration.Base;

/// <summary>
///     Configuration for .NET code generation. Produces C# early-bound entity classes,
///     action request/response wrappers, SDK message constants, and optionally XML metadata.
/// </summary>
public class DotNetCodeGenerationConfig : CodeGenerationConfigBase
{
    /// <summary>
    ///     C# namespace for generated classes.
    /// </summary>
    public string Namespace { get; init; } = "Digitall.Dataverse.Model";

    /// <summary>
    ///     Target framework. Controls nullable annotations and using directives.
    /// </summary>
    public DotNetTarget Target { get; init; } = DotNetTarget.Modern;

    /// <summary>
    ///     Make generated entity properties virtual (for mocking/proxying).
    /// </summary>
    public bool VirtualProperties { get; init; }

    /// <summary>
    ///     Generate setters even for read-only attributes.
    /// </summary>
    public bool EditableReadOnlyProperties { get; init; }

    /// <summary>
    ///     Controls which optional sections are included in generated entity classes.
    /// </summary>
    public DotNetInclude Include { get; init; } = new();
}

