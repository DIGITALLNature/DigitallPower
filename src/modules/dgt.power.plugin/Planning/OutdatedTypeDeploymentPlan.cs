// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.plugin.Planning;

public sealed record OutdatedTypeDeploymentPlan(
    OutdatedTypeMigration Migration,
    IReadOnlyList<Guid> CustomApiIds,
    IReadOnlyList<Guid> MigrateStepIds,
    IReadOnlyList<Guid> DeleteStepIds);
