// Copyright (c) DIGITALL Nature. All rights reserved

namespace dgt.power.common.Ownership;

public static class SkippedOwnerCaptureReport
{
    public static IReadOnlyList<string> Format(IReadOnlyDictionary<Guid, SkippedOwnerCapture> skippedRecords, string recordNoun)
    {
        ArgumentNullException.ThrowIfNull(skippedRecords);
        ArgumentException.ThrowIfNullOrWhiteSpace(recordNoun);
        return skippedRecords
            .GroupBy(entry => (entry.Value.Principal.LogicalName, entry.Value.Principal.Id, entry.Value.Reason, entry.Value.DisplayName))
            .OrderBy(group => group.Key.Reason)
            .ThenBy(group => group.Key.LogicalName, StringComparer.Ordinal)
            .ThenBy(group => group.Key.Id)
            .Select(group =>
            {
                var principal = $"{group.Key.LogicalName}({group.Key.Id:D})";
                var description = group.Key.Reason == OwnerCaptureSkipReason.PlatformPrincipal
                    ? $"platform principal {group.Key.DisplayName} {principal}"
                    : $"unmapped principal {principal}";
                var records = string.Join(", ", group.Select(entry => entry.Key).Order().Select(id => id.ToString("D")));
                return $"Owner not captured for {group.Count()} {recordNoun}(s) owned by {description}: {records}";
            })
            .ToArray();
    }
}
