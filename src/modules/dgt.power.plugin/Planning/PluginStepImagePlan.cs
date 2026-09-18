// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Local;

namespace dgt.power.plugin.Planning;

/// <summary>
/// A decision on what to do with a local plugin step image during a push.
/// </summary>
public sealed record PluginStepImagePlan(
    LocalPluginStepImage Local,
    PluginStepImageAction Action,
    RemotePluginStepImage? Existing);
