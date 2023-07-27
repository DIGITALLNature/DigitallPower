// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dto;
using dgt.power.import.Base;
using dgt.power.import.Logic;
using dgt.power.import.tests.Base;
using dgt.power.tests;
using FakeXrmEasy.Abstractions;
using FluentAssertions;
using Microsoft.Xrm.Sdk;
using Xunit.Abstractions;
using Calendar = dgt.power.dataverse.Calendar;
using CalendarRule = dgt.power.dataverse.CalendarRule;
#pragma warning disable CS8602

namespace dgt.power.import.tests;

public class CalendarImportTests : ImportTestBase<CalendarImport>
{
    public CalendarImportTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    protected override CommandTestContextBuilder<CalendarImport, ImportVerb> GetBuilder() =>
        base.GetBuilder()
            .WithRelationship(Calendar.Relations.OneToMany.InnerCalendarCalendarRules, new XrmFakedRelationship
            {
                Entity1LogicalName = Calendar.EntityLogicalName,
                Entity1Attribute = Calendar.LogicalNames.CalendarId,
                Entity2LogicalName = CalendarRule.EntityLogicalName,
                Entity2Attribute = CalendarRule.LogicalNames.InnerCalendarId,
                RelationshipType = XrmFakedRelationship.FakeRelationshipType.OneToMany
            })
            .WithRelationship(Calendar.Relations.OneToMany.CalendarCalendarRules, new XrmFakedRelationship
            {
                Entity1LogicalName = Calendar.EntityLogicalName,
                Entity1Attribute = Calendar.LogicalNames.CalendarId,
                Entity2LogicalName = CalendarRule.EntityLogicalName,
                Entity2Attribute = CalendarRule.LogicalNames.CalendarId,
                RelationshipType = XrmFakedRelationship.FakeRelationshipType.OneToMany
            });

    [Fact]
    public void ShouldFailOnEmptyConfiguration() =>
        GetContext().Execute(new ImportVerb
            {
                FileName = WriteConfigurationArtifact(new Calendars()).Name,
                FileDir = ArtifactDirectory
            }
        ).Should().BeFalse();

    [Fact]
    public void ShouldFailOnWrongConfiguration() =>
        GetContext().Execute(new ImportVerb
            {
                FileName = string.Empty,
                FileDir = ArtifactDirectory
            }
        ).Should().BeFalse();

    [Fact]
    public void ShouldUpdateExistingCalendar()
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
        
        context.Execute(new ImportVerb
            {
                FileName = "update-calendar.json",
                FileDir = ResourceDirectory
            }
        ).Should().BeTrue();

        var updatedCalendars = context.GetSingle<Calendar>(c => c.Id == existingCalendar.Id);
        updatedCalendars.Name.Should().Be(existingCalendar.Name);
        
        var calendarRules = context.Get<CalendarRule>(x => x.CalendarId.Id == calendarToBeUpdated.CalendarId);
        calendarRules.Should().HaveCount(calendarToBeUpdated.Rules.Count);
        
        var innerCalendar = context.GetById<Calendar>(innerCalendarRule.InnerCalendar.CalendarId);
        innerCalendar.Name.Should().Be(innerCalendarRule.InnerCalendar.Name);
    }

    [Fact]
    public void ShouldCreateNewCalendar()
    {
        var calendarImportConfig = GetConfigurationResource<Calendars>("create-calendar.json");
        var calendarToBeCreated = calendarImportConfig.Single();
        var innerCalendarRule = calendarToBeCreated.Rules.Single(x => x.InnerCalendar != null);
        var context = GetBuilder().Build();

        context.Execute(new ImportVerb
            {
                FileName = "create-calendar.json",
                FileDir = ResourceDirectory
            }
        ).Should().BeTrue();

        var createdCalendar = context.GetById<Calendar>(calendarToBeCreated.CalendarId);
        createdCalendar.Should().NotBeNull();
        createdCalendar.Name.Should().Be(calendarToBeCreated.Name);

        var calendarRules = context.Get<CalendarRule>(x => x.CalendarId.Id == createdCalendar.Id);
        calendarRules.Should().HaveCount(calendarToBeCreated.Rules.Count);

        var innerCalendar = context.GetById<Calendar>(innerCalendarRule.InnerCalendar.CalendarId);
        innerCalendar.Name.Should().Be(innerCalendarRule.InnerCalendar.Name);
    }
}
