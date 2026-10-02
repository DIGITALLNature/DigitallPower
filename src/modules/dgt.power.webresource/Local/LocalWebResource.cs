// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.webresource.Local;

public sealed record LocalWebResource(
    int Type,
    string Name,
    string DisplayName,
    string Content,
    string Hash,
    string RelativePath);
