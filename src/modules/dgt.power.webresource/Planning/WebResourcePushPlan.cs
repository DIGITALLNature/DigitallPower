// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using dgt.power.webresource.Remote;

namespace dgt.power.webresource.Planning;

public sealed record WebResourcePushPlan(
    IReadOnlyList<WebResourcePlanItem> Resources,
    IReadOnlyList<RemoteSolutionWebResource> Obsolete,
    string? SolutionUniqueName);
