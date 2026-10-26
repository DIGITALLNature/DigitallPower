// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

namespace dgt.power.plugin.Deployment.Reconciliation;

internal sealed record CustomApiChange(Guid Id, string Name, PluginTypeReference? Handler, Guid? PreviousHandlerId);
