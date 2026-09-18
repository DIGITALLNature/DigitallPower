// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.plugin.Execution;

/// <summary>Options controlling how <see cref="PluginPushExecutor"/> reconciles local targets.</summary>
/// <param name="Solution">Unique name of the solution new/updated components are added to; <see langword="null"/> to skip.</param>
/// <param name="DryRun">When <see langword="true"/>, only reports what would happen - no Dataverse writes are performed.</param>
/// <param name="PurgeOutdated">
/// When <see langword="true"/>, and a local assembly's major/minor version differs from the currently
/// registered one, steps on the outdated (superseded) assembly's plugin types are migrated to their
/// same-named replacement and the outdated assembly is then deleted. Custom API links are always
/// migrated to the replacement type regardless of this setting.
/// </param>
public sealed record PluginPushOptions(string? Solution, bool DryRun, bool PurgeOutdated = false);
