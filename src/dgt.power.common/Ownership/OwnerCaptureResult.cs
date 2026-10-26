// Copyright (c) DIGITALL Nature. All rights reserved

using Microsoft.Xrm.Sdk;

namespace dgt.power.common.Ownership;

public sealed record OwnerCaptureResult(IReadOnlyList<OwnerCapture> Captures, IReadOnlyList<SkippedOwnerCapture> Skipped)
{
    public bool TryGetLogicalKey(EntityReference principal, out string logicalKey)
    {
        ArgumentNullException.ThrowIfNull(principal);
        var capture = Captures.FirstOrDefault(candidate => Matches(candidate.Principal, principal));
        logicalKey = capture?.LogicalKey ?? string.Empty;
        return capture is not null;
    }

    public SkippedOwnerCapture? FindSkipped(EntityReference principal)
    {
        ArgumentNullException.ThrowIfNull(principal);
        return Skipped.FirstOrDefault(candidate => Matches(candidate.Principal, principal));
    }

    private static bool Matches(EntityReference left, EntityReference right) =>
        left.Id == right.Id && string.Equals(left.LogicalName, right.LogicalName, StringComparison.Ordinal);
}
