// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics.CodeAnalysis;
using dgt.power.common;
using dgt.power.common.Extensions;
using dgt.power.dataverse;
using dgt.power.import.Base;
using Microsoft.Xrm.Sdk;
using Spectre.Console;
using Calendar = dgt.power.dataverse.Calendar;
using CalendarRule = dgt.power.dataverse.CalendarRule;

namespace dgt.power.import.Logic;

public sealed class CalendarImport(
    ICommandSpanFactory spans,
    IDataverseConnection connection,
    IConfigResolver configResolver,
    IAnsiConsole console)
    : PowerCommand<ImportVerb>(spans, console)
{
    protected override async Task<bool> RunAsync([NotNull] ImportVerb args, CancellationToken cancellationToken)
    {
        var fileName = string.IsNullOrWhiteSpace(args.FileName) ? "calendar.json" : args.FileName;


        if (!configResolver.TryGetConfigFile<List<dto.Calendar>>(args.FileDir, fileName, out var calendars))
        {
            Console.MarkupLine("[red]Calendar configuration not found.[/]");
            return false;
        }

        //anything to do?
        if (calendars.Count == 0)
        {
            Console.MarkupLine("[red]Calendar configuration is empty.[/]");
            return false;
        }

        var result = true;

        var service = (IOrganizationService)await connection.GetServiceAsync();
        using var context = new DataContext(service);

        var defaultBusinessUnit = context.BusinessUnitSet.SingleOrDefault(bu => bu.ParentBusinessUnitId == null)
            ?.ToEntityReference();

        foreach (var calendar in calendars.OrderByDescending(c => c.Type))
        {
            Console.WriteLine($"Check {calendar.CalendarId} - Name {calendar.Name} - Type: {calendar.Type}");

            var calendarCurrent = context.CalendarSet.SingleOrDefault(c => c.Id == calendar.CalendarId);

            if (calendarCurrent == null)
            {
                Console.WriteLine($"Create {calendar.CalendarId} - Name {calendar.Name} - Type: {calendar.Type}");

                var calendarEntity = CreateCalendarStructure(calendar, defaultBusinessUnit);
                result = service.TryCreate(calendarEntity, out _);
            }
            else if (calendar.IsVaryByDay)
            {
                Console.WriteLine($"Skip {calendar.CalendarId} - Name {calendar.Name} - Type: {calendar.Type}");
            }
            else
            {
                Console.WriteLine($"Update {calendar.CalendarId} - Name {calendar.Name} - Type: {calendar.Type}");

                var calendarEntity = UpdateCalendarStructure(calendar);
                result = service.TryUpdate(calendarEntity);
            }
        }

        return result;
    }

    private static Calendar UpdateCalendarStructure(dto.Calendar calendar)
    {
        var calendarEntity = new Calendar
        {
            Id = calendar.CalendarId,
            CalendarId = calendar.CalendarId,
            Name = calendar.Name,
            Description = calendar.Description,
            Type = new OptionSetValue(calendar.Type),
            HolidayScheduleCalendarId = calendar.HolidayScheduleId != null ? new EntityReference(Calendar.EntityLogicalName, calendar.HolidayScheduleId.Value) : null
        };

        var entityCollection = new EntityCollection
        {
            EntityName = CalendarRule.EntityLogicalName
        };

        foreach (var rule in calendar.Rules)
        {
            var ruleEntity = new CalendarRule
            {
                Id = rule.CalendarRuleId,
                CalendarRuleId = rule.CalendarRuleId,
                Name = rule.Name,
                Description = rule.Description,
                StartTime = rule.StartDate,
                EndTime = rule.EndDate,
                Pattern = rule.Pattern,
                Duration = rule.Duration,
                Rank = rule.Rank,
                Offset = rule.Offset,
                SubCode = rule.SubCode,
                TimeCode = rule.TimeCode,
                TimeZoneCode = rule.TimeZoneCode,
                ExtentCode = rule.ExtendCode,
                IsSimple = rule.IsSimple,
                IsVaried = rule.IsVaried,
                IsSelected = rule.IsSelected,
                EffectiveIntervalStart = rule.EffectiveIntervalStart,
                EffectiveIntervalEnd = rule.EffectiveIntervalEnd,
                Effort = rule.Effort,
                GroupDesignator = rule.Groupdesignator
            };


            if (rule.InnerCalendar != null)
            {
                var innerCalendar = UpdateCalendarStructure(rule.InnerCalendar);

                ruleEntity.RelatedEntities.Add(new Relationship(CalendarRule.Relations.ManyToOne.InnerCalendarCalendarRules), new EntityCollection
                {
                    EntityName = Calendar.EntityLogicalName,
                    Entities = { innerCalendar }
                });
            }

            entityCollection.Entities.Add(ruleEntity);
        }

        calendarEntity["calendarrules"] = entityCollection;

        return calendarEntity;
    }

    private static Calendar CreateCalendarStructure(dto.Calendar calendar, EntityReference? defaultBu)
    {
        var calendarEntity = new Calendar
        {
            Id = calendar.CalendarId,
            CalendarId = calendar.CalendarId,
            Name = calendar.Name,
            Description = calendar.Description,
            Type = new OptionSetValue(calendar.Type),
            HolidayScheduleCalendarId = calendar.HolidayScheduleId != null ? new EntityReference(Calendar.EntityLogicalName, calendar.HolidayScheduleId.Value) : null,
            BusinessUnitId = defaultBu
        };

        var entityCollection = new EntityCollection
        {
            EntityName = CalendarRule.EntityLogicalName
        };

        foreach (var rule in calendar.Rules)
        {
            var ruleEntity = new CalendarRule
            {
                Id = rule.CalendarRuleId,
                CalendarRuleId = rule.CalendarRuleId,
                Name = rule.Name,
                Description = rule.Description,
                StartTime = rule.StartDate,
                EndTime = rule.EndDate,
                Pattern = rule.Pattern,
                Duration = rule.Duration,
                Rank = rule.Rank,
                Offset = rule.Offset,
                SubCode = rule.SubCode,
                TimeCode = rule.TimeCode,
                TimeZoneCode = rule.TimeZoneCode,
                ExtentCode = rule.ExtendCode,
                IsSimple = rule.IsSimple,
                IsVaried = rule.IsVaried,
                IsSelected = rule.IsSelected,
                EffectiveIntervalStart = rule.EffectiveIntervalStart,
                EffectiveIntervalEnd = rule.EffectiveIntervalEnd
            };


            if (rule.InnerCalendar != null)
            {
                var innerCalendar = CreateCalendarStructure(rule.InnerCalendar, defaultBu);


                ruleEntity.RelatedEntities.Add(new Relationship(CalendarRule.Relations.ManyToOne.InnerCalendarCalendarRules), new EntityCollection
                {
                    EntityName = Calendar.EntityLogicalName,
                    Entities = { innerCalendar }
                });
            }

            entityCollection.Entities.Add(ruleEntity);
        }

        calendarEntity.RelatedEntities.Add(new Relationship(Calendar.Relations.OneToMany.CalendarCalendarRules), entityCollection);

        return calendarEntity;
    }
}
