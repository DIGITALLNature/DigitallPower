// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.plugin.Local;

/// <summary>
/// A plugin assembly parsed from a local .dll file (or extracted from a .nupkg), independent of
/// any Dataverse state. This is the "desired state" input to planning.
/// </summary>
public sealed record LocalAssembly
{
    public required string Name { get; init; }

    public required Version Version { get; init; }

    public required string Content { get; init; }

    public LocalAssemblyKind Kind { get; init; } = LocalAssemblyKind.Undefined;

    public IReadOnlyList<LocalPluginType> PluginTypes { get; init; } = [];

    /// <summary>
    /// Client ID from a ManagedIdentityRegistrationAttribute on the assembly, if present.
    /// </summary>
    public string? ManagedIdentityClientId { get; init; }

    /// <summary>
    /// Optional tenant ID from a ManagedIdentityRegistrationAttribute; defaults to the
    /// environment's tenant when not provided.
    /// </summary>
    public string? ManagedIdentityTenantId { get; init; }
}
