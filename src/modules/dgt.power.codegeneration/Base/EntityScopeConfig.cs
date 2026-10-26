// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

// Configuration properties are populated by System.Text.Json.
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace dgt.power.codegeneration.Base;

/// <summary>Entity scope configuration — defines which entities to include via additive mechanisms.</summary>
#pragma warning disable CA2227
public class EntityScopeConfig
{
    /// <summary>Explicit entity logical names. Expanded at runtime by <see cref="FromSolutions"/> and <see cref="Mask"/>.</summary>
    public ICollection<string> Names { get; set; } = new HashSet<string>();

    /// <summary>Solution unique names. Entities from these solutions are added to <see cref="Names"/> at runtime.</summary>
    public IReadOnlyCollection<string> FromSolutions { get; init; } = new HashSet<string>();

    /// <summary>Wildcard pattern (e.g. "contoso_*") to auto-include matching entities.</summary>
    public string? Mask { get; init; }
}
#pragma warning restore CA2227
