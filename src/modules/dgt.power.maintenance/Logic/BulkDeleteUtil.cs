// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using dgt.power.common;
using dgt.power.common.Extensions;
using dgt.power.dataverse;
using dgt.power.maintenance.Base;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.maintenance.Logic;

public sealed class BulkDeleteUtil : BaseMaintenance
{
    private readonly int _sleepTime = 10000;

    public BulkDeleteUtil(ITracer tracer, IOrganizationService connection, IConfigResolver configResolver) : base(tracer, connection, configResolver)
    {
    }

    protected override bool Invoke(MaintenanceVerb args)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Tracer.Start(this);

        if (string.IsNullOrWhiteSpace(args.InlineData))
        {
            return Tracer.Skipped(this);
        }

        if (GetQueryExpression(args.InlineData, out var query))
        {
            Tracer.Log("Retrieved query expression from inlineData", TraceEventType.Information);

            var asyncOperationId = ExecuteBulkDeleteJob(query);
            Tracer.Log($"bulk delete job '{asyncOperationId}' started.", TraceEventType.Information);

            if (Wait(asyncOperationId))
            {
                Tracer.Log($"bulk delete job '{asyncOperationId}' finished.", TraceEventType.Information);
                return Tracer.End(this, true);
            }
        }

        return Tracer.End(this, false);
    }

    private bool Wait(Guid asyncOperationId)
    {
        AsyncOperation asyncOperation;
        do
        {
            Thread.Sleep(_sleepTime);
            asyncOperation = Connection.Retrieve(
                AsyncOperation.EntityLogicalName,
                asyncOperationId,
                new ColumnSet(AsyncOperation.LogicalNames.StatusCode)).ToEntity<AsyncOperation>();
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

    private Guid ExecuteBulkDeleteJob(QueryExpression query)
    {
        var response = (BulkDeleteResponse)Connection.Execute(new BulkDeleteRequest
        {
            JobName = "Maintenance BulkDelete job",
            QuerySet = new[] { query },
            //RunNow = true, forces a sync call, but this may run to long and you'll get a timeout!
            StartDateTime = DateTime.UtcNow,
            RecurrencePattern = string.Empty,
            SendEmailNotification = false,
            ToRecipients = Array.Empty<Guid>(),
            CCRecipients = Array.Empty<Guid>()
        });
        return response.JobId;
    }

    private bool GetQueryExpression(string fetchXml, out QueryExpression query)
    {
        query = default!;
        try
        {
            var response = (FetchXmlToQueryExpressionResponse)Connection.Execute(new FetchXmlToQueryExpressionRequest
            {
                FetchXml = fetchXml
            });
            query = response.Query;
            return true;
        }
        catch (Exception e)
        {
            Tracer.Log($"Invalid fetch-xml: {e.RootMessage()}", TraceEventType.Error);
            return false;
        }
    }
}
