// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Planning.Comparison;

namespace dgt.power.plugin.Planning.Deployment;

public sealed record PluginTypeDeploymentItem(
    PluginTypeComparison Comparison,
    IReadOnlyList<PluginStepDeployment> Steps,
    IReadOnlyList<PluginStepDeletion> StepDeletions,
    PluginCustomApiDeployment CustomApi,
    IReadOnlyList<Guid> MigratedCustomApiIds,
    IReadOnlyList<MigratedPluginStep> MigratedSteps);
