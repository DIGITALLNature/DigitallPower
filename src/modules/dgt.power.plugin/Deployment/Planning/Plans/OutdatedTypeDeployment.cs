// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Deployment.Planning.Comparison;

namespace dgt.power.plugin.Deployment.Planning.Plans;

public sealed record OutdatedTypeDeployment(
    OutdatedTypeMigration Migration,
    IReadOnlyList<Guid> UnlinkCustomApiIds,
    IReadOnlyList<MigratedPluginStep> MigrateSteps,
    IReadOnlyList<Guid> DeleteStepIds);
