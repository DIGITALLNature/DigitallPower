// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable CollectionNeverUpdated.Global

namespace dgt.power.codegeneration.Base;

/// <summary>
///     V2 base configuration for code generation. Discriminated by <c>"type"</c> in JSON.
///     Contains only properties shared between all output targets.
///     Polymorphic routing is handled by <see cref="CodeGenerationConfigFactory"/>.
/// </summary>
#pragma warning disable CA2227 // Entities is assigned post-construction by MetadataService.PopulateEntitiesAndSolutions
public abstract class CodeGenerationConfigBase
{
    /// <summary>
    ///     Config version. V2 configs use the typed discriminator model.
    /// </summary>
    public int Version { get; init; } = 2;

    /// <summary>
    ///     Entity logical names to include in generation.
    ///     Expanded at runtime by <see cref="EntityMask"/> and <see cref="Solutions"/>.
    /// </summary>
    public ICollection<string> Entities { get; set; } = new HashSet<string>();

    /// <summary>
    ///     Solution unique names. Entities from these solutions are added to <see cref="Entities"/>.
    /// </summary>
    public IReadOnlyCollection<string> Solutions { get; init; } = new HashSet<string>();

    /// <summary>
    ///     Wildcard pattern (e.g. "contoso_*") to auto-include matching entities.
    /// </summary>
    public string? EntityMask { get; init; }

    /// <summary>
    ///     Language code for label localization. <c>null</c> = organization base language (default),
    ///     or an explicit LCID (e.g. 1033).
    ///     Defaults to the organization's base language to ensure deterministic output
    ///     regardless of which user runs the generation.
    /// </summary>
    public int? Language { get; init; }

    /// <summary>
    ///     Global OptionSet logical names to generate as standalone constants.
    /// </summary>
    public IReadOnlyCollection<string> GlobalOptionSets { get; init; } = new HashSet<string>();

    /// <summary>
    ///     SDK message names to generate (actions, custom APIs, built-in messages like WhoAmI).
    ///     The generator auto-detects whether each entry is a custom action, custom API, or built-in message.
    /// </summary>
    public IReadOnlyCollection<string> Requests { get; init; } = new HashSet<string>();
}
#pragma warning restore CA2227
