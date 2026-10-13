// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Deployment.Local;
using dgt.power.plugin.Models;

namespace dgt.power.plugin.Deployment.Reconciliation;

internal sealed record PackageAssemblyChanges(
    LocalAssembly Desired,
    RemoteAssembly? Existing,
    RegistrationChanges Registrations,
    IReadOnlyDictionary<PluginStepReference, ResolvedSdkMessage> Messages);
