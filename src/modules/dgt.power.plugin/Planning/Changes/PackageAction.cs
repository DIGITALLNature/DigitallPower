// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.plugin.Planning.Changes;

/// <summary>
/// Decision for how a local plugin package should be deployed to a Dataverse environment.
/// Dataverse plugin packages cannot have their version changed after creation, so - unlike plugin
/// assemblies - there is no Upgrade action: a package either does not exist yet (Create), or it
/// does and its content is replaced in place (Update), regardless of version differences.
/// </summary>
public enum PackageAction
{
    /// <summary>No matching package exists in the environment yet; a new one must be registered.</summary>
    Create,

    /// <summary>A matching package exists; its content is replaced in place.</summary>
    Update
}
