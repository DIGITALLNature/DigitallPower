// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dto;
using dgt.power.export.Base;
using dgt.power.export.Logic;
using dgt.power.export.tests.Base;
using dgt.power.tests;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Calendar = dgt.power.dataverse.Calendar;
using CalendarRule = dgt.power.dataverse.CalendarRule;

namespace dgt.power.export.tests;

public class CalendarExportTests : ExportTestBase<CalendarExport>
{
    protected override CommandTestContext<CalendarExport, ExportVerb> GetContext()
    {
        var calendar1 = new Calendar(Guid.Parse("5d2fc991-a347-4e67-ad8b-6d6c517d510a"))
        {
            Name = "Test Calendar",
            Type = new OptionSetValue(Calendar.Options.Type.HolidaySchedule)
        };
        var innerCalendar = new Calendar(Guid.NewGuid())
        {
            Name = "Inner Customer Service Calendar",
            Type = new OptionSetValue(Calendar.Options.Type.InnerCalendarType)
        };
        var calendar2 = new Calendar(Guid.NewGuid())
        {
            Name = "Customer Service Calendar",
            Type = new OptionSetValue(Calendar.Options.Type.CustomerService)

        };
        return GetBuilder()
            .WithRelationship(new OneToManyRelationshipMetadata
            {
                SchemaName = Calendar.Relations.OneToMany.CalendarCalendarRules,
                ReferencingEntity = Calendar.EntityLogicalName,
                ReferencingAttribute = Calendar.LogicalNames.CalendarId,
                ReferencedEntity = CalendarRule.EntityLogicalName,
                ReferencedAttribute = CalendarRule.LogicalNames.CalendarId
            })
            .WithData(new Entity[]
            {
                calendar1,
                calendar2,
                innerCalendar,
                new CalendarRule(Guid.Parse("0f56b2f4-1b0f-43ab-9575-6309713820dd"))
                {
                    Name = "New Year",
                    CalendarId = calendar1.ToEntityReference(),
                    StartTime = DateTime.Parse("2020-01-01T00:00:00Z"),
                    Duration = 1440,
                    Description = "Holiday Rule",
                    Pattern = "FREQ=DAILY;INTERVAL=1;COUNT=1",
                    Rank = 0,
                    SubCode = 5,
                    TimeCode = 2,
                    TimeZoneCode = -1,
                    ExtentCode = 2,
                    IsSimple = false,
                    EffectiveIntervalStart = DateTime.Parse("2020-01-01T00:00:00Z"),
                    EffectiveIntervalEnd = DateTime.Parse("2020-01-02T00:00:00Z")
                },
                new CalendarRule(Guid.Parse("2a17af31-25c6-4c8d-adae-7ac26e3899e8"))
                {
                    Name = "Easter Sunday",
                    CalendarId = calendar1.ToEntityReference(),
                    StartTime = DateTime.Parse("2020-04-12T00:00:00Z"),
                    Duration = 1440,
                    Description = "Holiday Rule",
                    Pattern = "FREQ=DAILY;INTERVAL=1;COUNT=1",
                    Rank = 0,
                    SubCode = 5,
                    TimeCode = 2,
                    TimeZoneCode = -1,
                    ExtentCode = 2,
                    IsSimple = false,
                    EffectiveIntervalStart = DateTime.Parse("2020-04-12T00:00:00Z"),
                    EffectiveIntervalEnd = DateTime.Parse("2020-04-13T00:00:00Z")
                },
                new CalendarRule(Guid.NewGuid())
                {
                    Name = "New Year",
                    CalendarId = calendar2.ToEntityReference(),
                    StartTime = DateTime.Parse("2020-01-01T00:00:00Z"),
                    Duration = 1440,
                    Description = "Weekly Rec Rule",
                    Pattern = "FREQ=DAILY;INTERVAL=1;COUNT=1",
                    InnerCalendarId = innerCalendar.ToEntityReference(),
                    Rank = 0,
                    SubCode = 5,
                    TimeCode = 2,
                    TimeZoneCode = -1,
                    ExtentCode = 2,
                    IsSimple = false,
                    EffectiveIntervalStart = DateTime.Parse("2020-01-01T00:00:00Z"),
                    EffectiveIntervalEnd = DateTime.Parse("2020-01-02T00:00:00Z")
                },
                new CalendarRule(Guid.NewGuid())
                {
                    Name = "Easter Sunday",
                    CalendarId = calendar2.ToEntityReference(),
                    StartTime = DateTime.Parse("2020-04-12T00:00:00Z"),
                    Duration = 1440,
                    Description = "Holiday Rule",
                    Pattern = "FREQ=DAILY;INTERVAL=1;COUNT=1",
                    Rank = 0,
                    SubCode = 5,
                    TimeCode = 2,
                    TimeZoneCode = -1,
                    ExtentCode = 2,
                    IsSimple = false,
                    EffectiveIntervalStart = DateTime.Parse("2020-04-12T00:00:00Z"),
                    EffectiveIntervalEnd = DateTime.Parse("2020-04-13T00:00:00Z")
                }
            }).Build();
    }

    [Test]
    public async Task ShouldExportCalendar()
    {
        await Assert.That(GetContext().Execute(new ExportVerb
            {
                FileName = GetTestFileName(),
                FileDir = ArtifactDirectory
            }
        )).IsTrue();
        var calendars = GetConfigurationTestArtifact<Calendars>(GetTestFileName());
        await Assert.That(calendars).Count().EqualTo(2);
        await Assert.That(calendars.Single(x => x.IsVaryByDay == false).Rules).Count().EqualTo(2);
        await Assert.That(calendars.Single(x => x.IsVaryByDay).Rules).IsEmpty();
    }

    [Test]
    public async Task ShouldUseDefaultOnEmptyFileName()
    {
        await Assert.That(GetContext().Execute(new ExportVerb
            {
                FileName = string.Empty,
                FileDir = ArtifactDirectory
            }
        )).IsTrue();
        var calendars = GetConfigurationTestArtifact<Calendars>("calendar.json");
        await Assert.That(calendars).Count().EqualTo(2);
        await Assert.That(calendars.Single(x => x.IsVaryByDay == false).Rules).Count().EqualTo(2);
        await Assert.That(calendars.Single(x => x.IsVaryByDay).Rules).IsEmpty();
    }
}
