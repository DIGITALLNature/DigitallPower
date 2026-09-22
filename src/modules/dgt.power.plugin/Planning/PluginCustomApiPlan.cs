// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.plugin.Planning;

public sealed record PluginCustomApiPlan(
    string UniqueName,
    Guid? DesiredId,
    IReadOnlyList<Guid> UnlinkIds,
    bool Link,
    bool Unchanged);
