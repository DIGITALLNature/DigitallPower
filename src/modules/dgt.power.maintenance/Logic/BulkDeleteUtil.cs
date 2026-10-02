// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using dgt.power.common;
using dgt.power.common.Extensions;
using dgt.power.dataverse;
using dgt.power.maintenance.Base;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using Spectre.Console;

namespace dgt.power.maintenance.Logic;

public sealed class BulkDeleteUtil(
    ITracer tracer,
    IOrganizationService connection,
    IConfigResolver configResolver,
    TimeProvider timeProvider,
    IAnsiConsole console)
    : PowerLogic<BulkDeleteSettings>(tracer, connection, configResolver, console)
{
    protected override Task<bool> InvokeAsync(BulkDeleteSettings args, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(args);
        return InvokeCoreAsync(args, cancellationToken);
    }

    private async Task<bool> InvokeCoreAsync(BulkDeleteSettings args, CancellationToken cancellationToken)
    {
        Tracer.Start(this);

        if (string.IsNullOrWhiteSpace(args.InlineData))
        {
            return Tracer.Skipped(this);
        }

        var (success, query) = await GetQueryExpressionAsync(args.InlineData, cancellationToken);
        if (success)
        {
            Tracer.Log("Retrieved query expression from inlineData", TraceEventType.Information);

            var asyncOperationId = await ExecuteBulkDeleteJobAsync(query!, cancellationToken);
            Tracer.Log($"bulk delete job '{asyncOperationId}' started.", TraceEventType.Information);

            if (await WaitAsync(asyncOperationId, args.PollInterval, cancellationToken))
            {
                Tracer.Log($"bulk delete job '{asyncOperationId}' finished.", TraceEventType.Information);
                return Tracer.End(this, true);
            }
        }

        return Tracer.End(this, false);
    }

    private async Task<bool> WaitAsync(
        Guid asyncOperationId,
        int pollIntervalSeconds,
        CancellationToken cancellationToken)
    {
        var orgAsync = (IOrganizationServiceAsync2)Connection;
        AsyncOperation asyncOperation;
        do
        {
            await Task.Delay(TimeSpan.FromSeconds(pollIntervalSeconds), timeProvider, cancellationToken);
            var retrieveResponse = (RetrieveResponse)await orgAsync.ExecuteAsync(new RetrieveRequest
            {
                Target = new EntityReference(AsyncOperation.EntityLogicalName, asyncOperationId),
                ColumnSet = new ColumnSet(AsyncOperation.LogicalNames.StatusCode)
            }, cancellationToken);
            asyncOperation = retrieveResponse.Entity.ToEntity<AsyncOperation>();
            Tracer.Log($"bulk delete job '{asyncOperationId}' running...", TraceEventType.Information);
        } while (IsInExecution(asyncOperation));

        if (asyncOperation.StatusCode!.Value == AsyncOperation.Options.StatusCode.Succeeded)
        {
            return true;
        }

        Tracer.Log($"bulk delete job '{asyncOperationId}' failed!", TraceEventType.Error);
        return false;
    }

    private static bool IsInExecution(AsyncOperation asyncOperation) =>
        asyncOperation.StatusCode!.Value != AsyncOperation.Options.StatusCode.Canceled
        && asyncOperation.StatusCode.Value != AsyncOperation.Options.StatusCode.Failed
        && asyncOperation.StatusCode.Value != AsyncOperation.Options.StatusCode.Succeeded;

    private async Task<Guid> ExecuteBulkDeleteJobAsync(QueryExpression query, CancellationToken cancellationToken)
    {
        var orgAsync = (IOrganizationServiceAsync2)Connection;
        var response = (BulkDeleteResponse)await orgAsync.ExecuteAsync(new BulkDeleteRequest
        {
            JobName = "Maintenance BulkDelete job",
            QuerySet = [query],
            //RunNow = true, forces a sync call, but this may run to long and you'll get a timeout!
            StartDateTime = DateTime.UtcNow,
            RecurrencePattern = string.Empty,
            SendEmailNotification = false,
            ToRecipients = [],
            CCRecipients = []
        }, cancellationToken);
        return response.JobId;
    }

    private async Task<(bool Success, QueryExpression? Query)> GetQueryExpressionAsync(string fetchXml, CancellationToken cancellationToken)
    {
        try
        {
            var orgAsync = (IOrganizationServiceAsync2)Connection;
            var response = (FetchXmlToQueryExpressionResponse)await orgAsync.ExecuteAsync(new FetchXmlToQueryExpressionRequest
            {
                FetchXml = fetchXml
            }, cancellationToken);
            return (true, response.Query);
        }
        catch (Exception e) when (e is not OutOfMemoryException and not StackOverflowException)
        {
            Tracer.Log($"Invalid fetch-xml: {e.RootMessage()}", TraceEventType.Error);
            return (false, null);
        }
    }
}
