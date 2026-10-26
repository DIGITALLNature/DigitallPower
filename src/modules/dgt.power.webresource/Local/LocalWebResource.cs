// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

namespace dgt.power.webresource.Local;

public sealed record LocalWebResource(
    int Type,
    string Name,
    string DisplayName,
    string Content,
    string Hash,
    string RelativePath);
