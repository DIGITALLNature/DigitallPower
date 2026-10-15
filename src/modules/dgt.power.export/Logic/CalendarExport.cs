// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common;
using dgt.power.common.FileAccess;
using dgt.power.dataverse;
using dgt.power.export.Base;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using Spectre.Console;
using Calendar = dgt.power.dto.Calendar;

namespace dgt.power.export.Logic;

public sealed class CalendarExport(
    ICommandSpanFactory spans,
    IDataverseConnection connection,
    IFileService fileService,
    IAnsiConsole console)
    : PowerCommand<ExportVerb>(spans, console)
{
    protected override async Task<bool> RunAsync(ExportVerb args, CancellationToken cancellationToken)
    {
        var fileDir = args.FileDir;
        var fileName = string.IsNullOrWhiteSpace(args.FileName) ? "calendar.json" : args.FileName;

        var service = (IOrganizationService)await connection.GetServiceAsync();
        using var context = new DataContext(service);
        var scheduleIds = GetCalendarIds(context);

        var calendars = scheduleIds.Select(id => RetrieveCalendarStructure(service, id));

        var json = ExportJson.GetJson(calendars);

        Console.WriteLine($"Export {fileService.ExportFile(fileDir, fileName, json)} ");

        return true;
    }

    private static List<Guid> GetCalendarIds(DataContext context)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        return (from rec in context.CalendarSet
            where rec.Type != null
            where rec.Type.Value == dataverse.Calendar.Options.Type.CustomerService
                  || rec.Type.Value == dataverse.Calendar.Options.Type.HolidaySchedule
            orderby rec.Name
            select rec.Id).ToList();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
    }

    private static Calendar RetrieveCalendarStructure(IOrganizationService service, Guid calendarid)
    {
        var calendar = GetCalendarWithRules(service, calendarid);
        var calendarRules = calendar
            .RelatedEntities[new Relationship(dataverse.Calendar.Relations.OneToMany.CalendarCalendarRules)]?.Entities
            .Select(e => e.ToEntity<CalendarRule>()).ToList();

        var export = new Calendar
        {
            CalendarId = calendar.Id,
            Name = calendar.Name!,
            Type = calendar.Type!.Value,
            Description = calendar.Description,
            HolidayScheduleId = calendar.HolidayScheduleCalendarId?.Id
        };

        if (calendarRules == null)
        {
            return export;
        }

        if (calendarRules.Exists(cr => cr.Description is "Weekly Rec Rule" or "Weekly Sub Rule"))
        {
            export.IsVaryByDay = true;
            return export;
        }

        foreach (var calendarRule in calendarRules)
        {
            var innerCalendar = calendarRule.InnerCalendarId != null
                ? RetrieveCalendarStructure(service, calendarRule.InnerCalendarId.Id)
                : null;

            export.Rules.Add(new dto.CalendarRule
            {
                CalendarRuleId = calendarRule.Id,
                Name = calendarRule.Name!,
                StartDate = calendarRule.StartTime,
                EndDate = calendarRule.EndTime,
                Duration = calendarRule.Duration,
                Description = calendarRule.Description,
                Pattern = calendarRule.Pattern,
                InnerCalendar = innerCalendar,
                Rank = calendarRule.Rank,
                SubCode = calendarRule.SubCode,
                TimeCode = calendarRule.TimeCode,
                TimeZoneCode = calendarRule.TimeZoneCode,
                ExtendCode = calendarRule.ExtentCode,
                Offset = calendarRule.Offset,
                IsSelected = calendarRule.IsSelected,
                IsSimple = calendarRule.IsSimple,
                IsVaried = calendarRule.IsVaried,
                EffectiveIntervalEnd = calendarRule.EffectiveIntervalEnd,
                EffectiveIntervalStart = calendarRule.EffectiveIntervalStart,
                Effort = calendarRule.Effort,
                Groupdesignator = calendarRule.GroupDesignator
            });
        }

        return export;
    }

    private static dataverse.Calendar GetCalendarWithRules(IOrganizationService service, Guid calendarid)
    {
        var request = new RetrieveRequest
        {
            Target = new EntityReference(dataverse.Calendar.EntityLogicalName, calendarid),
            ColumnSet = new ColumnSet(true),
            RelatedEntitiesQuery = new RelationshipQueryCollection
            {
                {
                    new Relationship(dataverse.Calendar.Relations.OneToMany.CalendarCalendarRules),
                    new QueryExpression(CalendarRule.EntityLogicalName)
                    {
                        ColumnSet = new ColumnSet(true)
                    }
                }
            }
        };

        var response = (RetrieveResponse)service.Execute(request);
        var calendar = response.Entity.ToEntity<dataverse.Calendar>();
        return calendar;
    }
}
