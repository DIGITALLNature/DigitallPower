// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Deployment.Local;
using dgt.power.plugin.Models;

namespace dgt.power.plugin.Deployment.Planning.Comparison;

public sealed record PluginStepImageComparison(
    LocalPluginStepImage Local,
    RemotePluginStepImage? Remote,
    bool RequiresUpdate)
{
    public bool RequiresCreate => Remote is null;
}
