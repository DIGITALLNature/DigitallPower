// Copyright (c) DIGITALL Nature. All rights reserved

using dgt.power.common.Ownership;
using Microsoft.Xrm.Sdk;

namespace dgt.power.common.tests.Ownership;

public class SkippedOwnerCaptureReportTests
{
    [Test]
    public async Task Format_GroupsRecordsPerPrincipalAndOrdersPlatformFirst()
    {
        var system = new EntityReference("systemuser", Guid.Parse("00000000-0000-0000-0000-0000000000aa"));
        var team = new EntityReference("team", Guid.Parse("00000000-0000-0000-0000-0000000000bb"));
        var first = Guid.Parse("00000000-0000-0000-0000-000000000001");
        var second = Guid.Parse("00000000-0000-0000-0000-000000000002");
        var third = Guid.Parse("00000000-0000-0000-0000-000000000003");
        var skipped = new Dictionary<Guid, SkippedOwnerCapture>
        {
            [third] = new(team, OwnerCaptureSkipReason.Unmapped),
            [second] = new(system, OwnerCaptureSkipReason.PlatformPrincipal, "SYSTEM"),
            [first] = new(system, OwnerCaptureSkipReason.PlatformPrincipal, "SYSTEM")
        };

        var lines = SkippedOwnerCaptureReport.Format(skipped, "process");

        await Assert.That(lines).IsEquivalentTo(
        [
            $"Owner not captured for 2 process(s) owned by platform principal SYSTEM systemuser({system.Id:D}): {first:D}, {second:D}",
            $"Owner not captured for 1 process(s) owned by unmapped principal team({team.Id:D}): {third:D}"
        ]);
        await Assert.That(lines[0]).Contains("platform principal");
    }

    [Test]
    public async Task UnmappedOwnerHandlingOption_ParsesOnlyExactValues()
    {
        await Assert.That(UnmappedOwnerHandlingOption.Parse("fail")).IsEqualTo(UnmappedOwnerHandling.Fail);
        await Assert.That(UnmappedOwnerHandlingOption.Parse("skip")).IsEqualTo(UnmappedOwnerHandling.Skip);
        await Assert.That(UnmappedOwnerHandlingOption.TryParse("SKIP", out _)).IsFalse();
        await Assert.That(() => UnmappedOwnerHandlingOption.Parse(null)).Throws<ArgumentException>();
    }
}
