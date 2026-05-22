// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dto;
using dgt.power.import.Base;
using dgt.power.import.Logic;
using dgt.power.import.tests.Base;
using dgt.power.tests;
using Digitall.Dataverse.Testing;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Calendar = dgt.power.dataverse.Calendar;
using CalendarRule = dgt.power.dataverse.CalendarRule;
#pragma warning disable CS8602

namespace dgt.power.import.tests;

public class CalendarImportTests : ImportTestBase<CalendarImport>
{
    protected override CommandTestContextBuilder<CalendarImport, ImportVerb> GetBuilder() =>
        base.GetBuilder()
            .WithRelationship(new OneToManyRelationshipMetadata
            {
                SchemaName = Calendar.Relations.OneToMany.InnerCalendarCalendarRules,
                ReferencingEntity = CalendarRule.EntityLogicalName,
                ReferencingAttribute = CalendarRule.LogicalNames.InnerCalendarId,
                ReferencedEntity = Calendar.EntityLogicalName,
                ReferencedAttribute = Calendar.LogicalNames.CalendarId
            })
            .WithRelationship(new OneToManyRelationshipMetadata
            {
                SchemaName = Calendar.Relations.OneToMany.CalendarCalendarRules,
                ReferencingEntity = CalendarRule.EntityLogicalName,
                ReferencingAttribute = CalendarRule.LogicalNames.CalendarId,
                ReferencedEntity = Calendar.EntityLogicalName,
                ReferencedAttribute = Calendar.LogicalNames.CalendarId
            });

    [Test]
    public async Task ShouldFailOnEmptyConfiguration() =>
        await Assert.That(GetContext().Execute(new ImportVerb
            {
                FileName = WriteConfigurationArtifact(new Calendars()).Name,
                FileDir = ArtifactDirectory
            }
        )).IsFalse();

    [Test]
    public async Task ShouldFailOnWrongConfiguration() =>
        await Assert.That(GetContext().Execute(new ImportVerb
            {
                FileName = string.Empty,
                FileDir = ArtifactDirectory
            }
        )).IsFalse();

    [Test]
    public async Task ShouldUpdateExistingCalendar()
    {
        var calendarConfiguration = GetConfigurationResource<Calendars>("update-calendar.json");
        var calendarToBeUpdated = calendarConfiguration.Single();
        var innerCalendarRule = calendarToBeUpdated.Rules.Single(x => x.InnerCalendar != null);
        var existingCalendar = new Calendar(calendarToBeUpdated.CalendarId)
        {
            Name = calendarToBeUpdated.Name,
            Type = new OptionSetValue(Calendar.Options.Type.HolidaySchedule),
        };
        var existingInnerCalendar = new Calendar(innerCalendarRule.InnerCalendar.CalendarId)
        {
            Name = innerCalendarRule.InnerCalendar.Name,
            Type = new OptionSetValue(Calendar.Options.Type.InnerCalendarType)
        };
        var existingCalendarRules = calendarToBeUpdated.Rules.Select(x => new CalendarRule(x.CalendarRuleId)
        {
            CalendarId = existingCalendar.ToEntityReference()
        });
        var context = GetBuilder()
            .WithData(existingCalendar)
            .WithData(existingInnerCalendar)
            .WithData(existingCalendarRules)
            .Build();

        await Assert.That(context.Execute(new ImportVerb
            {
                FileName = "update-calendar.json",
                FileDir = ResourceDirectory
            }
        )).IsTrue();

        var updatedCalendars = context.GetSingle<Calendar>(c => c.Id == existingCalendar.Id);
        await Assert.That(updatedCalendars.Name).IsEqualTo(existingCalendar.Name);

        var calendarRules = context.Get<CalendarRule>(x => x.CalendarId.Id == calendarToBeUpdated.CalendarId);
        await Assert.That(calendarRules).HasCount().EqualTo(calendarToBeUpdated.Rules.Count);

        var innerCalendar = context.GetById<Calendar>(innerCalendarRule.InnerCalendar.CalendarId);
        await Assert.That(innerCalendar.Name).IsEqualTo(innerCalendarRule.InnerCalendar.Name);
    }

    [Test]
    public async Task ShouldCreateNewCalendar()
    {
        var calendarImportConfig = GetConfigurationResource<Calendars>("create-calendar.json");
        var calendarToBeCreated = calendarImportConfig.Single();
        var innerCalendarRule = calendarToBeCreated.Rules.Single(x => x.InnerCalendar != null);
        var context = GetBuilder().Build();

        await Assert.That(context.Execute(new ImportVerb
            {
                FileName = "create-calendar.json",
                FileDir = ResourceDirectory
            }
        )).IsTrue();

        var createdCalendar = context.GetById<Calendar>(calendarToBeCreated.CalendarId);
        await Assert.That(createdCalendar).IsNotNull();
        await Assert.That(createdCalendar.Name).IsEqualTo(calendarToBeCreated.Name);

        var calendarRules = context.Get<CalendarRule>(x => x.CalendarId.Id == createdCalendar.Id);
        await Assert.That(calendarRules).HasCount().EqualTo(calendarToBeCreated.Rules.Count);

        var innerCalendar = context.GetById<Calendar>(innerCalendarRule.InnerCalendar.CalendarId);
        await Assert.That(innerCalendar.Name).IsEqualTo(innerCalendarRule.InnerCalendar.Name);
    }
}
