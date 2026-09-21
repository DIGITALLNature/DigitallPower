// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Remote;

namespace dgt.power.plugin.Planning;

/// <summary>
/// The result of matching local plugin steps against the steps already registered under an owning
/// plugin type: what to do with each local step, and which remote steps have no local match
/// anymore and should be purged.
/// </summary>
public sealed record PluginStepReconciliationPlan(
    IReadOnlyList<PluginStepPlan> Plans, IReadOnlyList<RemotePluginStep> Purge);
