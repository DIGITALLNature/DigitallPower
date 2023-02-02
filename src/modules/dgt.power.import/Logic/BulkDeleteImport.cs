using System.Diagnostics;
using System.Globalization;
using dgt.power.common;
using dgt.power.common.Extensions;
using dgt.power.dataverse;
using dgt.power.dto;
using dgt.power.import.Base;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.import.Logic;

public sealed class BulkDeleteImport : BaseImport
{
    public BulkDeleteImport(ITracer tracer, IOrganizationService connection, IConfigResolver configResolver) : base(tracer,
        connection, configResolver)
    {
    }

    protected override bool Invoke(ImportVerb args)
    {
        Tracer.Start(this);
        var fileName = string.IsNullOrWhiteSpace(args.FileName) ? "bulkdelete.json" : args.FileName;


        if (!ConfigResolver.GetConfigFile<BulkDeletes>(args.FileDir, fileName, out var bulkDeletes))
        {
            return Tracer.NotConfigured(this);
        }

        //anything to do?
        if (!bulkDeletes.Deletes.Any())
        {
            return Tracer.NotConfigured(this);
        }

        var result = true;

        using var context = new DataContext(Connection);

        var (system, techUser, operations) = GetUserAndExistingBulkDeleteJobs(context);


        //find bulk deletes which are missing
        Tracer.Log("missing check", TraceEventType.Information);
        foreach (var missing in operations.Where(e => bulkDeletes.Deletes.All(c => e.Name != c.Name)))
        {
            Tracer.Log($"bulk delete {missing.Name} not configured", TraceEventType.Error);
            result = false;
        }

        if (!result)
        {
            return Tracer.End(this, false);
        }

        //find bulk deletes which need to be disabled
        Tracer.Log("disable check", TraceEventType.Information);
        foreach (var disable in operations.Where(e => bulkDeletes.Deletes.Any(c => e.Name == c.Name && c.Disable)))
        {
            result = DisableBulkDeleteJobs(disable, result);
        }

        //find bulk deletes which need to be updated
        Tracer.Log("update check", TraceEventType.Information);
        foreach (var xupdate in operations.Where(e => bulkDeletes.Deletes.Any(c => e.Name == c.Name && !c.Disable)))
        {
            result = UpdateBulkDeleteJobs(xupdate, bulkDeletes, result, context, system, techUser);
        }

        //find bulk deletes which need to be created
        Tracer.Log("create check", TraceEventType.Information);
        foreach (var create in bulkDeletes.Deletes.Where(e => operations.All(c => e.Name != c.Name)))
        {
            result = DeleteBulkDeleteJobs(create, result);
        }

        return Tracer.End(this, result);
    }

    private (SystemUser system, SystemUser techUser, ICollection<AsyncOperation> operations)
        GetUserAndExistingBulkDeleteJobs(DataContext context)
    {
        var system = (from su in context.SystemUserSet
            where su.LastName == "SYSTEM"
            where su.IsDisabled == true
            where su.FirstName == null || su.FirstName == ""
            select su).Single();

        var userId = ((WhoAmIResponse)Connection.Execute(new WhoAmIRequest())).UserId;
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

    private bool DeleteBulkDeleteJobs(BulkDelete create, bool result)
    {
        if (create.Disable)
        {
            return result;
        }

        if (string.IsNullOrWhiteSpace(create.FetchXml))
        {
            Tracer.Log($"bulk delete '{create.Name}' has no fetch xml ...", TraceEventType.Error);
            result = false;
            return result;
        }

        var startTime = DateTime
            .ParseExact($"{DateTime.UtcNow.AddDays(1):yyyy-MM-dd} {create.RecurrenceStartTime}:00 UTC",
                "yyyy-MM-dd HH:mm:ss UTC", CultureInfo.CurrentCulture, DateTimeStyles.AssumeUniversal)
            .ToUniversalTime();
        var request = new FetchXmlToQueryExpressionRequest
        {
            FetchXml = create.FetchXml
        };
        var response = (FetchXmlToQueryExpressionResponse)Connection.Execute(request);
        var bulkDeleteRequest = new BulkDeleteRequest
        {
            JobName = create.Name,
            QuerySet = new[] { response.Query },
            StartDateTime = startTime,
            RecurrencePattern = create.RecurrencePattern,
            SendEmailNotification = false,
            ToRecipients = Array.Empty<Guid>(),
            CCRecipients = Array.Empty<Guid>()
        };
        Tracer.Log(
            $"create bulk delete '{create.Name}'; StartTime:{create.RecurrenceStartTime}; Pattern:{create.RecurrencePattern}",
            TraceEventType.Information);
        result = Connection.TryExecute<BulkDeleteRequest, BulkDeleteResponse>(bulkDeleteRequest,
            out _) & result;
        return result;
    }

    private bool UpdateBulkDeleteJobs(AsyncOperation xupdate, BulkDeletes bulkDeletes, bool result,
        DataContext context, SystemUser system, SystemUser techUser)
    {
        var update = xupdate;
        var bulkDelete = bulkDeletes.Deletes.Single(e => e.Name == update.Name);


        if (!bulkDelete.RecurrencePattern.Equals(update.RecurrencePattern, StringComparison.Ordinal) ||
            !bulkDelete.RecurrenceStartTime.Equals($"{update.RecurrenceStartTime:HH:mm}", StringComparison.Ordinal))
        {
            Tracer.Log(
                $"update bulk delete '{update.Name}'; StartTime:{bulkDelete.RecurrenceStartTime}; Pattern:{bulkDelete.RecurrencePattern}!",
                TraceEventType.Information);
            Tracer.Log("HINT: update of fetch-xml is neither checked nor supported!", TraceEventType.Information);
            var startTime = DateTime
                .ParseExact($"{DateTime.UtcNow.AddDays(1):yyyy-MM-dd} {bulkDelete.RecurrenceStartTime}:00 UTC",
                    "yyyy-MM-dd HH:mm:ss UTC", CultureInfo.CurrentCulture, DateTimeStyles.AssumeUniversal)
                .ToUniversalTime();
            if (string.IsNullOrWhiteSpace(update.RecurrencePattern))
            {
                Tracer.Log("disabled once cannot be reactivated, but copied!", TraceEventType.Information);
                CopyBulkDeleteJob(update, result, startTime, bulkDelete.RecurrencePattern);
            }
            else
            {
                result = Connection.TryUpdate(new AsyncOperation(update.Id)
                {
                    RecurrencePattern = bulkDelete.RecurrencePattern,
                    RecurrenceStartTime = startTime
                }) & result;
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
            Tracer.Log($"update bulk delete {update.Name}; new owner: {techUser.FullName}!",
                TraceEventType.Information);

            if (!string.IsNullOrWhiteSpace(update.RecurrencePattern))
            {
                Tracer.Log("bulk deletes cannot be assigned, but copied!", TraceEventType.Information);
                result = Connection.TryUpdate(new AsyncOperation(update.Id)
                {
                    RecurrencePattern = "",
                    RecurrenceStartTime = DateTime.UtcNow.Add(TimeSpan.FromMinutes(-10))
                }) & result;
                result = CopyBulkDeleteJob(update, result,
                    update.RecurrenceStartTime.GetValueOrDefault(DateTime.UtcNow.AddDays(1)), update.RecurrencePattern);
            }
        }

        Tracer.Log($"bulk delete '{update.Name}' checked!", TraceEventType.Verbose);
        return result;
    }

    private bool DisableBulkDeleteJobs(AsyncOperation disable, bool result)
    {
        if (!string.IsNullOrEmpty(disable.RecurrencePattern))
        {
            //set to run once, delete afterwards, nice hack
            Tracer.Log($"disable bulk delete '{disable.Name}'", TraceEventType.Information);
            result = Connection.TryUpdate(new AsyncOperation(disable.Id)
            {
                RecurrencePattern = "",
                RecurrenceStartTime = DateTime.UtcNow.Add(TimeSpan.FromMinutes(10))
            }) & result;
        }
        else
        {
            Tracer.Log($"bulk delete '{disable.Name}' already disabled", TraceEventType.Information);
        }

        return result;
    }

    private bool CopyBulkDeleteJob(AsyncOperation bulkDeleteJob, bool result, DateTime startTime, string recurrencePattern)
    {
        var data = bulkDeleteJob.Data!;
        var start = data.IndexOf("<string>&lt;fetch", StringComparison.InvariantCultureIgnoreCase) + 8;
        var end = data.IndexOf("fetch&gt;</string>", StringComparison.InvariantCultureIgnoreCase) + 9;
        var fetchXml = data.Substring(start, end - start).Replace("&lt;", "<").Replace("&gt;", ">");
        var fetchXmlToQueryExpressionRequest = new FetchXmlToQueryExpressionRequest
        {
            FetchXml = fetchXml
        };
        var fetchXmlToQueryExpressionResponse = (FetchXmlToQueryExpressionResponse)
            Connection.Execute(fetchXmlToQueryExpressionRequest);
        var bulkDeleteRequest = new BulkDeleteRequest
        {
            JobName = bulkDeleteJob.Name,
            QuerySet = new[] { fetchXmlToQueryExpressionResponse.Query },
            StartDateTime = startTime,
            RecurrencePattern = recurrencePattern,
            SendEmailNotification = false,
            ToRecipients = Array.Empty<Guid>(),
            CCRecipients = Array.Empty<Guid>()
        };
        if (Connection.TryExecute<BulkDeleteRequest, BulkDeleteResponse>(bulkDeleteRequest,
                out _))
        {
            Tracer.Log($"bulk delete '{bulkDeleteJob.Name}' copied!", TraceEventType.Information);
            Tracer.Log($"delete old bulk delete '{bulkDeleteJob.Name}'!", TraceEventType.Information);
            if (Connection.TryRetrieve(AsyncOperation.EntityLogicalName, bulkDeleteJob.Id,
                    new ColumnSet(AsyncOperation.LogicalNames.AsyncOperationId),
                    out AsyncOperation reloaded))
            {
                result = Connection.TryDelete(AsyncOperation.EntityLogicalName, reloaded.Id) &
                         result;
            }
        }
        else
        {
            result = false;
        }

        return result;
    }
}
