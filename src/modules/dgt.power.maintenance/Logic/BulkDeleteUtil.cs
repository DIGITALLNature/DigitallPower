// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using dgt.power.common;
using dgt.power.common.Extensions;
using dgt.power.dataverse;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using Spectre.Console;

namespace dgt.power.maintenance.Logic;

public sealed class BulkDeleteUtil(
    ICommandSpanFactory spans,
    IDataverseConnection connection,
    TimeProvider timeProvider,
    IAnsiConsole console)
    : PowerCommand<BulkDeleteSettings>(spans, console)
{
    protected override async Task<bool> RunAsync(BulkDeleteSettings args, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(args);

        if (string.IsNullOrWhiteSpace(args.InlineData))
        {
            return true;
        }

        var orgAsync = await connection.GetServiceAsync();

        var (success, query) = await GetQueryExpressionAsync(orgAsync, args.InlineData, cancellationToken);
        if (success)
        {
            console.WriteLine("Retrieved query expression from inlineData");

            var asyncOperationId = await ExecuteBulkDeleteJobAsync(orgAsync, query!, cancellationToken);
            console.WriteLine($"bulk delete job '{asyncOperationId}' started.");

            if (await WaitAsync(orgAsync, asyncOperationId, args.PollInterval, cancellationToken))
            {
                console.WriteLine($"bulk delete job '{asyncOperationId}' finished.");
                return true;
            }
        }

        return false;
    }

    private async Task<bool> WaitAsync(
        IOrganizationServiceAsync2 orgAsync,
        Guid asyncOperationId,
        int pollIntervalSeconds,
        CancellationToken cancellationToken)
    {
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
            console.WriteLine($"bulk delete job '{asyncOperationId}' running...");
        } while (IsInExecution(asyncOperation));

        if (asyncOperation.StatusCode!.Value == AsyncOperation.Options.StatusCode.Succeeded)
        {
            return true;
        }

        console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[red]bulk delete job '{asyncOperationId}' failed![/]");
        return false;
    }

    private static bool IsInExecution(AsyncOperation asyncOperation) =>
        asyncOperation.StatusCode!.Value != AsyncOperation.Options.StatusCode.Canceled
        && asyncOperation.StatusCode.Value != AsyncOperation.Options.StatusCode.Failed
        && asyncOperation.StatusCode.Value != AsyncOperation.Options.StatusCode.Succeeded;

    private async Task<Guid> ExecuteBulkDeleteJobAsync(IOrganizationServiceAsync2 orgAsync, QueryExpression query, CancellationToken cancellationToken)
    {
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

    private async Task<(bool Success, QueryExpression? Query)> GetQueryExpressionAsync(IOrganizationServiceAsync2 orgAsync, string fetchXml, CancellationToken cancellationToken)
    {
        try
        {
            var response = (FetchXmlToQueryExpressionResponse)await orgAsync.ExecuteAsync(new FetchXmlToQueryExpressionRequest
            {
                FetchXml = fetchXml
            }, cancellationToken);
            return (true, response.Query);
        }
        catch (Exception e) when (e is not OutOfMemoryException and not StackOverflowException)
        {
            console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[red]Invalid fetch-xml: {e.RootMessage()}[/]");
            return (false, null);
        }
    }
}
