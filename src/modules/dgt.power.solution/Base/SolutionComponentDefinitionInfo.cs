// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.solution.Base;

/// <summary>
/// The two <c>solutioncomponentdefinition</c> columns copy-components needs per componenttype:
/// <see cref="Name"/> matches <c>msdyn_componentlayer.msdyn_solutioncomponentname</c> (layer lookup),
/// <see cref="PrimaryEntityName"/> is the backing table for a record-based managed-state lookup.
/// Both are null for metadata-only componenttypes that have no <c>solutioncomponentdefinition</c> row.
/// </summary>
public readonly record struct SolutionComponentDefinitionInfo(string? Name, string? PrimaryEntityName)
{
    /// <summary>
    /// True for components that only make sense together with a model-driven app (app settings, app module
    /// components, app elements). Componenttypes above 10000 are assigned per environment, so this is derived from the
    /// definition's name/backing table instead of a hard-coded componenttype number.
    /// </summary>
    public bool IsAppBound => IsAppBoundName(Name) || IsAppBoundName(PrimaryEntityName);

    private static bool IsAppBoundName(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        var normalized = value.Replace(" ", string.Empty, StringComparison.Ordinal);
        return normalized.Equals("appsetting", StringComparison.OrdinalIgnoreCase)
            || normalized.StartsWith("appmodulecomponent", StringComparison.OrdinalIgnoreCase)
            || normalized.StartsWith("appelement", StringComparison.OrdinalIgnoreCase);
    }
}
