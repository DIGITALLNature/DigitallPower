// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Remote;

namespace dgt.power.plugin.Commands;

/// <summary>
/// Outcome of resolving a step by id or composite key: a single <see cref="Step"/>, or none/several <see cref="Matches"/>.
/// </summary>
internal sealed record PluginStepResolution(RemotePluginStepState? Step, IReadOnlyList<RemotePluginStep> Matches)
{
    public bool IsAmbiguous => Step is null && Matches.Count > 1;
}
