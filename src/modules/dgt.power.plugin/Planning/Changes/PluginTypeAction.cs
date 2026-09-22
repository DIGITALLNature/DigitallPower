// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.plugin.Planning.Changes;

/// <summary>
/// The action to take for a local <see cref="Local.LocalPluginType"/> during a push.
/// </summary>
public enum PluginTypeAction
{
    /// <summary>The type does not exist on the target environment yet.</summary>
    Create,

    /// <summary>
    /// The type already exists and is unchanged. Its Custom API link may still change as a child
    /// action.
    /// </summary>
    Unchanged
}
