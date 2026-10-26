// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using dgt.power.plugin.Deployment.Local;

namespace dgt.power.plugin.Deployment.Reconciliation;

internal sealed record ImageWrite(string TypeName, LocalPluginStep Step, LocalPluginStepImage Desired, Guid? ExistingId);
