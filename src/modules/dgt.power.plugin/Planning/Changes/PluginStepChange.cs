// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Local;
using dgt.power.plugin.Remote;

namespace dgt.power.plugin.Planning.Changes;

/// <summary>
/// A decision on what to do with a local plugin step during a push.
/// </summary>
public sealed record PluginStepChange(LocalPluginStep Local, PluginStepAction Action, RemotePluginStep? Existing);
