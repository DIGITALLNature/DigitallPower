// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using dgt.power.plugin.Deployment.Reconciliation;

namespace dgt.power.plugin.Deployment.Batch;

/// <summary>A target whose remote state has been prepared and previewed, ready to be applied.</summary>
internal sealed record PreparedTarget(bool HasChanges, Func<Action<PluginDeploymentProgress>, CancellationToken, Task> Apply);
