// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.webresource.Remote;

public sealed record RemoteSolutionWebResource(
    Guid Id,
    int Type,
    string Name,
    bool IsManaged);
