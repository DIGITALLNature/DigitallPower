// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

namespace dgt.power.webresource.Remote;

public sealed record RemoteWebResource(
    Guid Id,
    int Type,
    string Name,
    string? Content,
    bool IsManaged);
