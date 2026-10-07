// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Deployment.Local;

namespace dgt.power.plugin.Deployment.Reconciliation;

internal sealed record ResolvedRegistrations(
    IReadOnlyDictionary<string, Guid> TypeIds,
    IReadOnlyDictionary<(string TypeName, LocalPluginStep Step), Guid> StepIds);
