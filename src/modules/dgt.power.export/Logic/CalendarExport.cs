// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using dgt.power.common;
using dgt.power.common.FileAccess;
using dgt.power.dataverse;
using dgt.power.export.Base;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using Calendar = dgt.power.dto.Calendar;

namespace dgt.power.export.Logic;

public sealed class CalendarExport : BaseExport
{
    public CalendarExport(ITracer tracer, IOrganizationService connection, IConfigResolver configResolver, IFileService fileService) : base(tracer,
        connection, configResolver, fileService)
    {
    }

    protected override bool Invoke(ExportVerb args)
    {
        Tracer.Start(this);

        var fileDir = args.FileDir;
        var fileName = string.IsNullOrWhiteSpace(args.FileName) ? "calendar.json" : args.FileName;

        using var context = new DataContext(Connection);
        var scheduleIds = GetCalendarIds(context);

        var calendars = scheduleIds.Select(RetrieveCalendarStructure);

        var json = GetJson(calendars);

        Tracer.Log($"Export {HandleExportFile(fileDir, fileName, json)} ", TraceEventType.Information);

        return Tracer.End(this, true);
    }

    private static IEnumerable<Guid> GetCalendarIds(DataContext context)
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

    private Calendar RetrieveCalendarStructure(Guid calendarid)
    {
        var calendar = GetCalendarWithRules(calendarid);
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

        if (calendarRules.Any(cr => cr.Description is "Weekly Rec Rule" or "Weekly Sub Rule"))
        {
            export.IsVaryByDay = true;
            return export;
        }

        foreach (var calendarRule in calendarRules)
        {
            var innerCalendar = calendarRule.InnerCalendarId != null
                ? RetrieveCalendarStructure(calendarRule.InnerCalendarId.Id)
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

    private dataverse.Calendar GetCalendarWithRules(Guid calendarid)
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

        var response = (RetrieveResponse)Connection.Execute(request);
        var calendar = response.Entity.ToEntity<dataverse.Calendar>();
        return calendar;
    }
}
