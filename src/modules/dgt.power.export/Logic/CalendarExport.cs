// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common;
using dgt.power.common.Extensions;
using dgt.power.common.FileAccess;
using dgt.power.dataverse;
using dgt.power.export.Base;
using Microsoft.PowerPlatform.Dataverse.Client;
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

        var service = await connection.GetServiceAsync();
        var scheduleQuery = new QueryExpression(dataverse.Calendar.EntityLogicalName)
        {
            NoLock = true,
            ColumnSet = new ColumnSet(dataverse.Calendar.LogicalNames.CalendarId, dataverse.Calendar.LogicalNames.Name),
            Criteria = new FilterExpression
            {
                Conditions =
                {
                    new ConditionExpression(dataverse.Calendar.LogicalNames.Type, ConditionOperator.In,
                        dataverse.Calendar.Options.Type.CustomerService,
                        dataverse.Calendar.Options.Type.HolidaySchedule)
                }
            },
            Orders = { new OrderExpression(dataverse.Calendar.LogicalNames.Name, OrderType.Ascending) }
        };
        var scheduleIds = (await service.RetrieveAllAsync(scheduleQuery, cancellationToken: cancellationToken))
            .Select(entity => entity.Id).ToList();
        var calendars = new List<Calendar>();
        foreach (var id in scheduleIds)
        {
            calendars.Add(await RetrieveCalendarStructureAsync(service, id, cancellationToken));
        }

        var json = ExportJson.GetJson(calendars);

        Console.WriteLine($"Export {fileService.ExportFile(fileDir, fileName, json)} ");

        return true;
    }

    private static async Task<Calendar> RetrieveCalendarStructureAsync(IOrganizationServiceAsync2 service, Guid calendarid, CancellationToken cancellationToken)
    {
        var calendar = await GetCalendarWithRulesAsync(service, calendarid, cancellationToken);
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
                ? await RetrieveCalendarStructureAsync(service, calendarRule.InnerCalendarId.Id, cancellationToken)
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

    private static async Task<dataverse.Calendar> GetCalendarWithRulesAsync(IOrganizationServiceAsync2 service, Guid calendarid, CancellationToken cancellationToken)
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
                        NoLock = true,
                        ColumnSet = new ColumnSet(true)
                    }
                }
            }
        };

        var response = (RetrieveResponse)await service.ExecuteAsync(request, cancellationToken);
        var calendar = response.Entity.ToEntity<dataverse.Calendar>();
        return calendar;
    }
}
