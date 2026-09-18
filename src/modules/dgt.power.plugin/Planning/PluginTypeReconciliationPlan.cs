// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.plugin.Planning;

/// <summary>
/// The result of matching local plugin types against the types already registered on an assembly:
/// what to do with each local type, and which remote types have no local match anymore and should
/// be purged.
/// </summary>
public sealed record PluginTypeReconciliationPlan(
    IReadOnlyList<PluginTypePlan> Plans, IReadOnlyList<RemotePluginType> Purge);
