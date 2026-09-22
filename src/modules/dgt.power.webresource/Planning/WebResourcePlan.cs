// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.webresource.Local;
using dgt.power.webresource.Remote;

namespace dgt.power.webresource.Planning;

public sealed record WebResourcePlanItem(
    LocalWebResource Local,
    WebResourceAction Action,
    RemoteWebResource? Remote);

public sealed record WebResourcePushPlan(
    IReadOnlyList<WebResourcePlanItem> Resources,
    IReadOnlyList<RemoteSolutionWebResource> Obsolete);
