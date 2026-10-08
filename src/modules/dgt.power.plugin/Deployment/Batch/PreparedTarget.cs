// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Deployment.Execution;
using dgt.power.plugin.Deployment.Planning.Plans;

namespace dgt.power.plugin.Deployment.Batch;

/// <summary>A target whose remote state has been prepared and previewed, ready to be applied.</summary>
internal sealed record PreparedTarget(bool HasChanges, Func<Action<PluginDeploymentProgress>, CancellationToken, Task> Apply);
