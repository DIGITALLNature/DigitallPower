// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

namespace dgt.power.webresource.Remote;

public sealed record RemoteSolutionWebResource(
    Guid Id,
    int Type,
    string Name,
    bool IsManaged);
