// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Planning.Changes;

namespace dgt.power.plugin.Planning.Deployment;

public sealed record OutdatedTypeDeployment(
    OutdatedTypeMigration Migration,
    IReadOnlyList<Guid> CustomApiIds,
    IReadOnlyList<Guid> MigrateStepIds,
    IReadOnlyList<Guid> DeleteStepIds);
