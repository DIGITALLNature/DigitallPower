// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.solution.Base;

/// <summary>
/// The two <c>solutioncomponentdefinition</c> columns copy-components needs per componenttype:
/// <see cref="Name"/> matches <c>msdyn_componentlayer.msdyn_solutioncomponentname</c> (layer lookup),
/// <see cref="PrimaryEntityName"/> is the backing table for a record-based managed-state lookup.
/// Both are null for metadata-only componenttypes that have no <c>solutioncomponentdefinition</c> row.
/// </summary>
public readonly record struct SolutionComponentDefinitionInfo(string? Name, string? PrimaryEntityName);
