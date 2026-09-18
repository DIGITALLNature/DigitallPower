// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.plugin.Planning;

/// <summary>
/// The action to take for a local <see cref="Local.LocalPluginType"/> during a push.
/// </summary>
public enum PluginTypeAction
{
    /// <summary>The type does not exist on the target environment yet.</summary>
    Create,

    /// <summary>
    /// The type already exists. It is never content-updated (its identity fields are immutable),
    /// but its Custom API link must still be reconciled (linked, relinked, or unlinked).
    /// </summary>
    Reconcile
}
