// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.plugin.Planning;

/// <summary>
/// Decision for how a local plugin assembly should be reconciled against a Dataverse environment.
/// </summary>
public enum AssemblyAction
{
    /// <summary>No matching assembly exists in the environment yet; a new one must be registered.</summary>
    Create,

    /// <summary>
    /// A matching assembly exists with the same major.minor version; its content is updated in place
    /// and its plugin types/steps are reconciled against the same assembly record.
    /// </summary>
    Update,

    /// <summary>
    /// A matching assembly exists but with a different major.minor version. A new assembly record is
    /// registered alongside the existing one(s); the outdated assembly(ies) are left in place unless
    /// the caller opts into deleting/migrating them.
    /// </summary>
    Upgrade,

    /// <summary>
    /// A matching assembly exists but is already owned by a plugin package. Standalone assembly
    /// reconciliation must not touch it - the owning package deployment manages its lifecycle.
    /// </summary>
    OwnedByPackage
}
