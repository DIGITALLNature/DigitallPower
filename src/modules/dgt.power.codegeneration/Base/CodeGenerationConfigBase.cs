// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable CollectionNeverUpdated.Global
// ReSharper disable UnusedMember.Global

namespace dgt.power.codegeneration.Base;

/// <summary>
///     V2 base configuration shared between all output types.
///     Discriminated by <c>"type"</c> in JSON. Polymorphic routing handled by
///     <see cref="CodeGenerationConfigFactory"/>.
/// </summary>
public abstract class CodeGenerationConfigBase
{
    /// <summary>Config version. V2 configs use the typed discriminator model.</summary>
    public int Version { get; init; } = 2;

    /// <summary>
    ///     Namespace for generated code. For .NET: C# namespace. For TypeScript: reserved for future module prefix.
    ///     Defaults to null; concrete types supply type-specific defaults.
    /// </summary>
    public string? Namespace { get; init; }

    /// <summary>
    ///     Language code for label localization. null = organization base language (default).
    ///     Explicit LCID (e.g. 1033) overrides org language.
    /// </summary>
    public int? Language { get; init; }

    /// <summary>Defines which entities to include via additive mechanisms (names, solutions, mask).</summary>
    public EntityScopeConfig Entities { get; init; } = new();

    /// <summary>
    ///     SDK message names to generate (actions, custom APIs, built-in messages like WhoAmI).
    ///     Presence drives generation of SDK message name constants.
    ///     The generator auto-detects whether each entry is a custom action, custom API, or built-in message.
    /// </summary>
    public IReadOnlyCollection<string> Requests { get; init; } = new HashSet<string>();

    /// <summary>Global OptionSet logical names to generate as standalone constants.</summary>
    public IReadOnlyCollection<string> OptionSets { get; init; } = new HashSet<string>();
}
