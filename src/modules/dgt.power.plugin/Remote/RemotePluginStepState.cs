// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.plugin.Remote;

/// <summary>
/// Name and enabled state of a plugin step already registered on the target environment.
/// </summary>
public sealed record RemotePluginStepState(Guid Id, string Name, bool IsEnabled);
