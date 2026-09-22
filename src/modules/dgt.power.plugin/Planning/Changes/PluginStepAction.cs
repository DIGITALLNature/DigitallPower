// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.plugin.Planning.Changes;

/// <summary>
/// The action to take for a local <see cref="Local.LocalPluginStep"/> during a push.
/// </summary>
public enum PluginStepAction
{
    /// <summary>No matching step exists on the target environment yet.</summary>
    Create,

    /// <summary>A matching step exists but its content differs and must be updated.</summary>
    Update,

    /// <summary>
    /// A matching step exists and is already up to date. Still returned (with its existing id) so
    /// its images can be reconciled independently of whether the step body itself changed.
    /// </summary>
    Unchanged
}
