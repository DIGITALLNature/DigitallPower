// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.plugin.Planning;

/// <summary>
/// The action to take for a local <see cref="Local.LocalPluginStepImage"/> during a push.
/// </summary>
public enum PluginStepImageAction
{
    /// <summary>No matching image exists on the target environment yet.</summary>
    Create,

    /// <summary>A matching image exists but its attributes differ and must be updated.</summary>
    Update
}
