// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Models;

namespace dgt.power.plugin.Deployment.Planning.Plans;

public sealed record MigratedPluginStep(
    RemotePluginStep Step,
    IReadOnlyList<RemotePluginStepImage> Images);
