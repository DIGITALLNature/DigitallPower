// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.webresource.Execution;

public sealed record WebResourcePushOptions(
    string? Solution,
    bool Publish,
    bool DeleteObsolete,
    bool DryRun);
