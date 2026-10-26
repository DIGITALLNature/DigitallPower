// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

// Configuration properties are populated by System.Text.Json.
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace dgt.power.codegeneration.Base;

/// <summary>
///     Controls which optional sections are included in generated .NET entity classes.
///     All default to true (opt-in model: everything included unless explicitly disabled).
/// </summary>
public class DotNetInclude
{
    /// <summary>
    ///     Generate OrganizationServiceContext (DataContext) partial class with IQueryable sets.
    /// </summary>
    public bool Context { get; init; } = true;

    /// <summary>
    ///     Generate a nested Options class with OptionSet value constants.
    /// </summary>
    public bool Options { get; init; } = true;

    /// <summary>
    ///     Generate a nested LogicalNames class with attribute logical name constants.
    /// </summary>
    public bool LogicalNames { get; init; } = true;

    /// <summary>
    ///     Generate a nested Relations class with relationship schema name constants.
    /// </summary>
    public bool Relations { get; init; } = true;

    /// <summary>
    ///     Generate navigation props for 1:N relationships.
    /// </summary>
    public bool NavigationProps { get; init; } = true;

    /// <summary>
    ///     Generate EntityTypeCode constant.
    /// </summary>
    public bool EntityTypeCode { get; init; } = true;

    /// <summary>
    ///     Generate a nested AlternateKeys class with alternate key logical name constants.
    /// </summary>
    public bool AlternateKeys { get; init; } = true;

    /// <summary>
    ///     Generate XML metadata files (DataContractSerializer dumps of EntityMetadata).
    ///     Off by default — most projects don't need raw metadata XML.
    /// </summary>
    public bool Metadata { get; init; }
}
