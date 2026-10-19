// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using dgt.power.common;
using dgt.power.common.Extensions;
using dgt.power.dataverse;
using dgt.power.dto;
using dgt.power.import.Base;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.PowerPlatform.Dataverse.Client;
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
        var fileName = string.IsNullOrWhiteSpace(args.FileName) ? "bulkdelete.json" : args.FileName;


        if (!configResolver.TryGetConfigFile<BulkDeletes>(args.FileDir, fileName, out var bulkDeletes))
        {
            Console.MarkupLine("[red]Bulk delete configuration not found.[/]");
            return false;
        }

        //anything to do?
        if (bulkDeletes.Deletes.Count == 0)
        {
            Console.MarkupLine("[red]Bulk delete configuration is empty.[/]");
            return false;
        }

        var result = true;

        var service = await connection.GetServiceAsync();
        var (system, techUser, operations) = await GetUserAndExistingBulkDeleteJobsAsync(service, cancellationToken);


        //find bulk deletes which are missing
        Console.WriteLine("missing check");
        foreach (var missing in operations.Where(e => bulkDeletes.Deletes.TrueForAll(c => e.Name != c.Name)))
        {
            Console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[red]bulk delete {missing.Name} not configured[/]");
            result = false;
        }

        if (!result)
        {
            return false;
        }

        //find bulk deletes which need to be disabled
        Console.WriteLine("disable check");
        foreach (var disable in operations.Where(e => bulkDeletes.Deletes.Exists(c => e.Name == c.Name && c.Disable)))
        {
            result = await DisableBulkDeleteJobsAsync(service, disable, cancellationToken) && result;
        }

        //find bulk deletes which need to be updated
        Console.WriteLine("update check");
        foreach (var xupdate in operations.Where(e => bulkDeletes.Deletes.Exists(c => e.Name == c.Name && !c.Disable)))
        {
            result = await UpdateBulkDeleteJobsAsync(service, xupdate, bulkDeletes, system, techUser, cancellationToken) && result;
        }

        //find bulk deletes which need to be created
        Console.WriteLine("create check");
        foreach (var create in bulkDeletes.Deletes.Where(e => operations.All(c => e.Name != c.Name)))
        {
            result = await DeleteBulkDeleteJobsAsync(service, create, cancellationToken) && result;
        }

        return result;
    }

    private static async Task<(SystemUser system, SystemUser techUser, IReadOnlyList<AsyncOperation> operations)>
        GetUserAndExistingBulkDeleteJobsAsync(IOrganizationServiceAsync2 service, CancellationToken cancellationToken)
    {
        var systemQuery = new QueryExpression(SystemUser.EntityLogicalName)
        {
            NoLock = true,
            ColumnSet = new ColumnSet(SystemUser.LogicalNames.SystemUserId, SystemUser.LogicalNames.LastName,
                SystemUser.LogicalNames.IsDisabled, SystemUser.LogicalNames.FirstName),
            Criteria =
            {
                Conditions =
                {
                    new ConditionExpression(SystemUser.LogicalNames.LastName, ConditionOperator.Equal, "SYSTEM"),
                    new ConditionExpression(SystemUser.LogicalNames.IsDisabled, ConditionOperator.Equal, true)
                },
                Filters =
                {
                    new FilterExpression(LogicalOperator.Or)
                    {
                        Conditions =
                        {
                            new ConditionExpression(SystemUser.LogicalNames.FirstName, ConditionOperator.Null),
                            new ConditionExpression(SystemUser.LogicalNames.FirstName, ConditionOperator.Equal, string.Empty)
                        }
                    }
                }
            }
        };
        var system = (await service.RetrieveAllAsync(systemQuery, cancellationToken: cancellationToken))
            .Single().ToEntity<SystemUser>();

        var whoAmIResponse = (WhoAmIResponse)await service.ExecuteAsync(new WhoAmIRequest(), cancellationToken);
        var techUser = (await service.RetrieveAsync(SystemUser.EntityLogicalName, whoAmIResponse.UserId,
            new ColumnSet(SystemUser.LogicalNames.SystemUserId, SystemUser.LogicalNames.FullName), cancellationToken))
            .ToEntity<SystemUser>();

        var operationQuery = new QueryExpression(AsyncOperation.EntityLogicalName)
        {
            NoLock = true,
            ColumnSet = new ColumnSet(AsyncOperation.LogicalNames.AsyncOperationId, AsyncOperation.LogicalNames.Name,
                AsyncOperation.LogicalNames.OperationType, AsyncOperation.LogicalNames.RecurrenceStartTime,
                AsyncOperation.LogicalNames.RecurrencePattern, AsyncOperation.LogicalNames.OwnerId, AsyncOperation.LogicalNames.Data),
            Criteria =
            {
                Conditions =
                {
                    new ConditionExpression(AsyncOperation.LogicalNames.OperationType, ConditionOperator.Equal,
                        AsyncOperation.Options.OperationType.BulkDelete),
                    new ConditionExpression(AsyncOperation.LogicalNames.RecurrenceStartTime, ConditionOperator.NotNull)
                }
            },
            Orders = { new OrderExpression(AsyncOperation.LogicalNames.Name, OrderType.Ascending) }
        };
        var operations = (await service.RetrieveAllAsync(operationQuery, cancellationToken: cancellationToken))
            .Select(entity => entity.ToEntity<AsyncOperation>()).ToList();
        return (system, techUser, operations);
    }

    private async Task<bool> DeleteBulkDeleteJobsAsync(IOrganizationServiceAsync2 service, BulkDelete create,
        CancellationToken cancellationToken)
    {
        if (create.Disable)
        {
            return true;
        }

        if (string.IsNullOrWhiteSpace(create.FetchXml))
        {
            Console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[red]bulk delete '{create.Name}' has no fetch xml ...[/]");
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
        var response = (FetchXmlToQueryExpressionResponse)await service.ExecuteAsync(request, cancellationToken);
        response.Query.NoLock = true;
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
        Console.WriteLine(
            $"create bulk delete '{create.Name}'; StartTime:{create.RecurrenceStartTime}; Pattern:{create.RecurrencePattern}");
        var (succeeded, _) = await service.TryExecuteAsync<BulkDeleteRequest, BulkDeleteResponse>(bulkDeleteRequest, cancellationToken);
        return succeeded;
    }

    private async Task<bool> UpdateBulkDeleteJobsAsync(IOrganizationServiceAsync2 service, AsyncOperation xupdate,
        BulkDeletes bulkDeletes, SystemUser system, SystemUser techUser, CancellationToken cancellationToken)
    {
        var update = xupdate;
        var bulkDelete = bulkDeletes.Deletes.Single(e => e.Name == update.Name);
        var result = true;

        if (!bulkDelete.RecurrencePattern.Equals(update.RecurrencePattern, StringComparison.Ordinal) ||
            !bulkDelete.RecurrenceStartTime.Equals($"{update.RecurrenceStartTime:HH:mm}", StringComparison.Ordinal))
        {
            Console.WriteLine(
                $"update bulk delete '{update.Name}'; StartTime:{bulkDelete.RecurrenceStartTime}; Pattern:{bulkDelete.RecurrencePattern}!");
            Console.WriteLine("HINT: update of fetch-xml is neither checked nor supported!");
            var startTime = DateTime
                .ParseExact($"{DateTime.UtcNow.AddDays(1):yyyy-MM-dd} {bulkDelete.RecurrenceStartTime}:00 UTC",
                    "yyyy-MM-dd HH:mm:ss UTC", CultureInfo.CurrentCulture, DateTimeStyles.AssumeUniversal)
                .ToUniversalTime();
            if (string.IsNullOrWhiteSpace(update.RecurrencePattern))
            {
                Console.WriteLine("disabled once cannot be reactivated, but copied!");
                result = await CopyBulkDeleteJobAsync(service, update, startTime, bulkDelete.RecurrencePattern, cancellationToken);
            }
            else
            {
                result = await service.TryUpdateAsync(new AsyncOperation(update.Id)
                {
                    RecurrencePattern = bulkDelete.RecurrencePattern,
                    RecurrenceStartTime = startTime
                }, cancellationToken);
            }

            var operationQuery = new QueryExpression(AsyncOperation.EntityLogicalName)
            {
                NoLock = true,
                ColumnSet = new ColumnSet(AsyncOperation.LogicalNames.AsyncOperationId, AsyncOperation.LogicalNames.Name,
                    AsyncOperation.LogicalNames.OperationType, AsyncOperation.LogicalNames.RecurrenceStartTime,
                    AsyncOperation.LogicalNames.RecurrencePattern, AsyncOperation.LogicalNames.OwnerId, AsyncOperation.LogicalNames.Data),
                Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression(AsyncOperation.LogicalNames.OperationType, ConditionOperator.Equal,
                            AsyncOperation.Options.OperationType.BulkDelete),
                        new ConditionExpression(AsyncOperation.LogicalNames.RecurrenceStartTime, ConditionOperator.NotNull),
                        new ConditionExpression(AsyncOperation.LogicalNames.Name, ConditionOperator.Equal, xupdate.Name)
                    }
                }
            };
            update = (await service.RetrieveAllAsync(operationQuery, cancellationToken: cancellationToken))
                .Single().ToEntity<AsyncOperation>();
        }

        if (update.OwnerId!.Id != system.Id && update.OwnerId.Id != techUser.Id)
        {
            Console.WriteLine($"update bulk delete {update.Name}; new owner: {techUser.FullName}!");

            if (!string.IsNullOrWhiteSpace(update.RecurrencePattern))
            {
                Console.WriteLine("bulk deletes cannot be assigned, but copied!");
                result = await service.TryUpdateAsync(new AsyncOperation(update.Id)
                {
                    RecurrencePattern = "",
                    RecurrenceStartTime = DateTime.UtcNow.Add(TimeSpan.FromMinutes(-10))
                }, cancellationToken) && result;
                result = await CopyBulkDeleteJobAsync(service, update,
                    update.RecurrenceStartTime.GetValueOrDefault(DateTime.UtcNow.AddDays(1)), update.RecurrencePattern,
                    cancellationToken) && result;
            }
        }

        Console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[grey]bulk delete '{update.Name}' checked![/]");
        return result;
    }

    private async Task<bool> DisableBulkDeleteJobsAsync(IOrganizationServiceAsync2 service, AsyncOperation disable,
        CancellationToken cancellationToken)
    {
        var result = true;
        if (!string.IsNullOrEmpty(disable.RecurrencePattern))
        {
            //set to run once, delete afterwards, nice hack
            Console.WriteLine($"disable bulk delete '{disable.Name}'");
            result = await service.TryUpdateAsync(new AsyncOperation(disable.Id)
            {
                RecurrencePattern = "",
                RecurrenceStartTime = DateTime.UtcNow.Add(TimeSpan.FromMinutes(10))
            }, cancellationToken);
        }
        else
        {
            Console.WriteLine($"bulk delete '{disable.Name}' already disabled");
        }

        return result;
    }

    private async Task<bool> CopyBulkDeleteJobAsync(IOrganizationServiceAsync2 service, AsyncOperation bulkDeleteJob, DateTime startTime,
        string recurrencePattern, CancellationToken cancellationToken)
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
            await service.ExecuteAsync(fetchXmlToQueryExpressionRequest, cancellationToken);
        fetchXmlToQueryExpressionResponse.Query.NoLock = true;
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
        var (succeeded, _) = await service.TryExecuteAsync<BulkDeleteRequest, BulkDeleteResponse>(bulkDeleteRequest, cancellationToken);
        if (succeeded)
        {
            Console.WriteLine($"bulk delete '{bulkDeleteJob.Name}' copied!");
            Console.WriteLine($"delete old bulk delete '{bulkDeleteJob.Name}'!");
            var (retrieved, reloaded) = await service.TryRetrieveAsync<AsyncOperation>(AsyncOperation.EntityLogicalName,
                bulkDeleteJob.Id, new ColumnSet(AsyncOperation.LogicalNames.AsyncOperationId), cancellationToken);
            if (retrieved)
            {
                result = await service.TryDeleteAsync(AsyncOperation.EntityLogicalName, reloaded!.Id, cancellationToken);
            }
        }
        else
        {
            result = false;
        }

        return result;
    }
}
