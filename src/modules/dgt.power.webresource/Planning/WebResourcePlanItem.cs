// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using dgt.power.webresource.Local;
using dgt.power.webresource.Remote;

namespace dgt.power.webresource.Planning;

public sealed record WebResourcePlanItem(
    LocalWebResource Local,
    WebResourceAction Action,
    RemoteWebResource? Remote,
    bool AddToSolution);
