// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using dgt.power.common;
using dgt.power.common.Extensions;
using dgt.power.dataverse;
using dgt.power.dto;
using dgt.power.import.Base;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Spectre.Console;

namespace dgt.power.import.Logic;

public sealed class BulkDeleteImport(
    ICommandSpanFactory spans,
    IDataverseConnection connection,
    IConfigResolver configResolver,
    IAnsiConsole console)
    : PowerCommand<ImportVerb>(spans, console)
{
    protected override async Task<bool> RunAsync(ImportVerb args, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(args);
        var fileName = string.IsNullOrWhiteSpace(args.FileName) ? "bulkdelete.json" : args.FileName;


        if (!configResolver.TryGetConfigFile<BulkDeletes>(args.FileDir, fileName, out var bulkDeletes))
        {
            console.MarkupLine("[red]Bulk delete configuration not found.[/]");
            return false;
        }

        //anything to do?
        if (bulkDeletes.Deletes.Count == 0)
        {
            console.MarkupLine("[red]Bulk delete configuration is empty.[/]");
            return false;
        }

        var result = true;

        var service = (IOrganizationService)await connection.GetServiceAsync();
        using var context = new DataContext(service);

        var (system, techUser, operations) = GetUserAndExistingBulkDeleteJobs(service, context);


        //find bulk deletes which are missing
        console.WriteLine("missing check");
        foreach (var missing in operations.Where(e => bulkDeletes.Deletes.TrueForAll(c => e.Name != c.Name)))
        {
            console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[red]bulk delete {missing.Name} not configured[/]");
            result = false;
        }

        if (!result)
        {
            return false;
        }

        //find bulk deletes which need to be disabled
        console.WriteLine("disable check");
        foreach (var disable in operations.Where(e => bulkDeletes.Deletes.Exists(c => e.Name == c.Name && c.Disable)))
        {
            result = DisableBulkDeleteJobs(service, disable) && result;
        }

        //find bulk deletes which need to be updated
        console.WriteLine("update check");
        foreach (var xupdate in operations.Where(e => bulkDeletes.Deletes.Exists(c => e.Name == c.Name && !c.Disable)))
        {
            result = UpdateBulkDeleteJobs(service, xupdate, bulkDeletes, context, system, techUser) && result;
        }

        //find bulk deletes which need to be created
        console.WriteLine("create check");
        foreach (var create in bulkDeletes.Deletes.Where(e => operations.All(c => e.Name != c.Name)))
        {
            result = DeleteBulkDeleteJobs(service, create) && result;
        }

        return result;
    }

    private static (SystemUser system, SystemUser techUser, ICollection<AsyncOperation> operations)
        GetUserAndExistingBulkDeleteJobs(IOrganizationService service, DataContext context)
    {
        var system = (from su in context.SystemUserSet
            where su.LastName == "SYSTEM"
            where su.IsDisabled == true
            where su.FirstName == null || su.FirstName == ""
            select su).Single();

        var userId = ((WhoAmIResponse)service.Execute(new WhoAmIRequest())).UserId;
        var techUser = (from su in context.SystemUserSet
            where su.Id == userId
            select su).Single();

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var operations = (from ao in context.AsyncOperationSet
            where ao.OperationType != null
            where ao.OperationType.Value == AsyncOperation.Options.OperationType.BulkDelete
            where ao.RecurrenceStartTime != null
            orderby ao.Name
            select ao).ToList();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        return (system, techUser, operations);
    }

    private bool DeleteBulkDeleteJobs(IOrganizationService service, BulkDelete create)
    {
        if (create.Disable)
        {
            return true;
        }

        if (string.IsNullOrWhiteSpace(create.FetchXml))
        {
            console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[red]bulk delete '{create.Name}' has no fetch xml ...[/]");
            return false;
        }

        var startTime = DateTime
            .ParseExact($"{DateTime.UtcNow.AddDays(1):yyyy-MM-dd} {create.RecurrenceStartTime}:00 UTC",
                "yyyy-MM-dd HH:mm:ss UTC", CultureInfo.CurrentCulture, DateTimeStyles.AssumeUniversal)
            .ToUniversalTime();
        var request = new FetchXmlToQueryExpressionRequest
        {
            FetchXml = create.FetchXml
        };
        var response = (FetchXmlToQueryExpressionResponse)service.Execute(request);
        var bulkDeleteRequest = new BulkDeleteRequest
        {
            JobName = create.Name,
            QuerySet = [response.Query],
            StartDateTime = startTime,
            RecurrencePattern = create.RecurrencePattern,
            SendEmailNotification = false,
            ToRecipients = [],
            CCRecipients = []
        };
        console.WriteLine(
            $"create bulk delete '{create.Name}'; StartTime:{create.RecurrenceStartTime}; Pattern:{create.RecurrencePattern}");
        return service.TryExecute<BulkDeleteRequest, BulkDeleteResponse>(bulkDeleteRequest,
            out _);
    }

    private bool UpdateBulkDeleteJobs(IOrganizationService service, AsyncOperation xupdate, BulkDeletes bulkDeletes, DataContext context, SystemUser system,
        SystemUser techUser)
    {
        var update = xupdate;
        var bulkDelete = bulkDeletes.Deletes.Single(e => e.Name == update.Name);
        var result = true;

        if (!bulkDelete.RecurrencePattern.Equals(update.RecurrencePattern, StringComparison.Ordinal) ||
            !bulkDelete.RecurrenceStartTime.Equals($"{update.RecurrenceStartTime:HH:mm}", StringComparison.Ordinal))
        {
            console.WriteLine(
                $"update bulk delete '{update.Name}'; StartTime:{bulkDelete.RecurrenceStartTime}; Pattern:{bulkDelete.RecurrencePattern}!");
            console.WriteLine("HINT: update of fetch-xml is neither checked nor supported!");
            var startTime = DateTime
                .ParseExact($"{DateTime.UtcNow.AddDays(1):yyyy-MM-dd} {bulkDelete.RecurrenceStartTime}:00 UTC",
                    "yyyy-MM-dd HH:mm:ss UTC", CultureInfo.CurrentCulture, DateTimeStyles.AssumeUniversal)
                .ToUniversalTime();
            if (string.IsNullOrWhiteSpace(update.RecurrencePattern))
            {
                console.WriteLine("disabled once cannot be reactivated, but copied!");
                result = CopyBulkDeleteJob(service, update, startTime, bulkDelete.RecurrencePattern);
            }
            else
            {
                result = service.TryUpdate(new AsyncOperation(update.Id)
                {
                    RecurrencePattern = bulkDelete.RecurrencePattern,
                    RecurrenceStartTime = startTime
                });
            }

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            update = (from ao in context.AsyncOperationSet
                where ao.OperationType != null
                where ao.OperationType.Value == AsyncOperation.Options.OperationType.BulkDelete
                where ao.RecurrenceStartTime != null
                where ao.Name == xupdate.Name
                select ao).Single(); //reload after update
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        if (update.OwnerId!.Id != system.Id && update.OwnerId.Id != techUser.Id)
        {
            console.WriteLine($"update bulk delete {update.Name}; new owner: {techUser.FullName}!");

            if (!string.IsNullOrWhiteSpace(update.RecurrencePattern))
            {
                console.WriteLine("bulk deletes cannot be assigned, but copied!");
                result = service.TryUpdate(new AsyncOperation(update.Id)
                {
                    RecurrencePattern = "",
                    RecurrenceStartTime = DateTime.UtcNow.Add(TimeSpan.FromMinutes(-10))
                }) && result;
                result = CopyBulkDeleteJob(service, update, update.RecurrenceStartTime.GetValueOrDefault(DateTime.UtcNow.AddDays(1)), update.RecurrencePattern) && result;
            }
        }

        console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[grey]bulk delete '{update.Name}' checked![/]");
        return result;
    }

    private bool DisableBulkDeleteJobs(IOrganizationService service, AsyncOperation disable)
    {
        var result = true;
        if (!string.IsNullOrEmpty(disable.RecurrencePattern))
        {
            //set to run once, delete afterwards, nice hack
            console.WriteLine($"disable bulk delete '{disable.Name}'");
            result = service.TryUpdate(new AsyncOperation(disable.Id)
            {
                RecurrencePattern = "",
                RecurrenceStartTime = DateTime.UtcNow.Add(TimeSpan.FromMinutes(10))
            });
        }
        else
        {
            console.WriteLine($"bulk delete '{disable.Name}' already disabled");
        }

        return result;
    }

    private bool CopyBulkDeleteJob(IOrganizationService service, AsyncOperation bulkDeleteJob, DateTime startTime, string recurrencePattern)
    {
        var result = true;
        var data = bulkDeleteJob.Data!;
        var start = data.IndexOf("<string>&lt;fetch", StringComparison.InvariantCultureIgnoreCase) + 8;
        var end = data.IndexOf("fetch&gt;</string>", StringComparison.InvariantCultureIgnoreCase) + 9;
        var fetchXml = data.Substring(start, end - start).Replace("&lt;", "<", StringComparison.Ordinal).Replace("&gt;", ">", StringComparison.Ordinal);
        var fetchXmlToQueryExpressionRequest = new FetchXmlToQueryExpressionRequest
        {
            FetchXml = fetchXml
        };
        var fetchXmlToQueryExpressionResponse = (FetchXmlToQueryExpressionResponse)
            service.Execute(fetchXmlToQueryExpressionRequest);
        var bulkDeleteRequest = new BulkDeleteRequest
        {
            JobName = bulkDeleteJob.Name,
            QuerySet = [fetchXmlToQueryExpressionResponse.Query],
            StartDateTime = startTime,
            RecurrencePattern = recurrencePattern,
            SendEmailNotification = false,
            ToRecipients = [],
            CCRecipients = []
        };
        if (service.TryExecute<BulkDeleteRequest, BulkDeleteResponse>(bulkDeleteRequest,
                out _))
        {
            console.WriteLine($"bulk delete '{bulkDeleteJob.Name}' copied!");
            console.WriteLine($"delete old bulk delete '{bulkDeleteJob.Name}'!");
            if (service.TryRetrieve(AsyncOperation.EntityLogicalName, bulkDeleteJob.Id,
                    new ColumnSet(AsyncOperation.LogicalNames.AsyncOperationId),
                    out AsyncOperation reloaded))
            {
                result = service.TryDelete(AsyncOperation.EntityLogicalName, reloaded.Id);
            }
        }
        else
        {
            result = false;
        }

        return result;
    }
}
