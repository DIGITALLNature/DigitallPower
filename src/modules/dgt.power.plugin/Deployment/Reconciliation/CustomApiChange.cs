// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.plugin.Deployment.Reconciliation;

internal sealed record CustomApiChange(Guid Id, string Name, PluginTypeReference? Handler, Guid? PreviousHandlerId);
