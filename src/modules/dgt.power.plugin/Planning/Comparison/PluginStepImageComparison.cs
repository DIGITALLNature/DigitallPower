// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Local;
using dgt.power.plugin.Remote;

namespace dgt.power.plugin.Planning.Comparison;

public sealed record PluginStepImageComparison(
    LocalPluginStepImage Local,
    RemotePluginStepImage? Remote,
    bool RequiresUpdate)
{
    public bool RequiresCreate => Remote is null;
}
