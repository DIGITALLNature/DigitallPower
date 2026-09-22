// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using Microsoft.Xrm.Sdk.Metadata;

namespace dgt.power.solution.Base;

/// <summary>
/// What an Entity's solutioncomponent row in a given solution effectively means for its
/// subcomponents (attributes today, other component types can be added the same way later).
/// Needed because "include subcomponents" entities never get their own attribute/form/view rows -
/// the components are implied, not individually listed.
/// </summary>
public sealed class EntityComponentMembership
{
    public required string SolutionUniqueName { get; init; }

    public required string EntityLogicalName { get; init; }

    public required SolutionComponent EntityComponent { get; init; }

    /// <summary>One of <see cref="SolutionComponent.Options.RootComponentBehavior"/>.</summary>
    public required int RootComponentBehavior { get; init; }

    /// <summary>Whether the table itself (not the solution) is managed, e.g. an ISV-owned table.</summary>
    public required bool IsTableManaged { get; init; }

    /// <summary>Child solutioncomponent rows that explicitly exist for this entity, grouped by componenttype. Generic across component types.</summary>
    public required IReadOnlyDictionary<int, IReadOnlyList<SolutionComponent>> ExplicitSubcomponentsByType { get; init; }

    /// <summary>
    /// The attributes a rule should treat as "in this solution" for this entity: all of them when
    /// <see cref="SolutionComponent.Options.RootComponentBehavior.IncludeSubcomponents"/>, only the explicitly listed ones
    /// when <see cref="SolutionComponent.Options.RootComponentBehavior.DoNotIncludeSubcomponents"/>, none for
    /// <see cref="SolutionComponent.Options.RootComponentBehavior.IncludeAsShellOnly"/>.
    /// </summary>
    public required IReadOnlyList<AttributeMetadata> EffectiveAttributes { get; init; }
}
