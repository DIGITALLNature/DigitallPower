// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.plugin.Execution;

/// <summary>Options controlling how <see cref="PluginPushExecutor"/> reconciles local targets.</summary>
/// <param name="Solution">Unique name of the solution new/updated components are added to; <see langword="null"/> to skip.</param>
/// <param name="DryRun">When <see langword="true"/>, only reports what would happen - no Dataverse writes are performed.</param>
/// <param name="PublisherPrefix">Publisher customization prefix used to name plugin packages.</param>
public sealed record PluginPushOptions(string? Solution, bool DryRun, string? PublisherPrefix = null);
