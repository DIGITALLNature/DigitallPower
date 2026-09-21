// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Remote;

namespace dgt.power.plugin.Planning;

/// <summary>
/// The result of matching local plugin step images against the images already registered under an
/// owning step: what to do with each local image, and which remote images have no local match
/// anymore and should be purged.
/// </summary>
public sealed record PluginStepImageReconciliationPlan(
    IReadOnlyList<PluginStepImagePlan> Plans, IReadOnlyList<RemotePluginStepImage> Purge);
