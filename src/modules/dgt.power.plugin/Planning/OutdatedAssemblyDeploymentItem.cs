// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Remote;

namespace dgt.power.plugin.Planning;

public sealed record OutdatedAssemblyDeploymentItem(
    RemoteAssembly Assembly,
    IReadOnlyList<OutdatedTypeDeploymentPlan> Types);
