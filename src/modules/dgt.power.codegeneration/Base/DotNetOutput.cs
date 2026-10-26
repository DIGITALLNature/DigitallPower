// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

// Configuration properties are populated by System.Text.Json.
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace dgt.power.codegeneration.Base;

/// <summary>Output configuration for .NET code generation.</summary>
public class DotNetOutput
{
    /// <summary>
    ///     Target framework. 'Modern' = net8.0+ (nullable, implicit usings).
    ///     'Framework' = .NET Framework 4.6.2 (plugins, no nullable).
    /// </summary>
    public DotNetTarget Target { get; init; } = DotNetTarget.Modern;

    /// <summary>Make generated entity properties virtual (for mocking/proxying).</summary>
    public bool Virtual { get; init; }

    /// <summary>Generate setters even for read-only attributes.</summary>
    public bool EditableReadOnly { get; init; }

    /// <summary>Controls which optional sections are included in generated entity classes.</summary>
    public DotNetInclude Include { get; init; } = new();
}
