// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Models;

namespace dgt.power.plugin.Deployment.Planning.Plans;

public sealed record PluginTypeDeletion(RemotePluginType Type, IReadOnlyList<Guid> DependentStepIds, IReadOnlyList<Guid> LinkedCustomApiIds);
