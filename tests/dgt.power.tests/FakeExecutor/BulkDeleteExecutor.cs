// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using FakeXrmEasy.Abstractions;
using FakeXrmEasy.Abstractions.FakeMessageExecutors;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;

namespace dgt.power.tests.FakeExecutor;

public class BulkDeleteExecutor : IFakeMessageExecutor
{
    private static readonly Random Random = new Random(12345);

    public int ExpectedStatusCode { get; set; } = AsyncOperation.Options.StatusCode.Succeeded;
    
    public bool CanExecute(OrganizationRequest request)
    {
        return request is BulkDeleteRequest;
    }

    public Type GetResponsibleRequestType()
    {
        return typeof(BulkDeleteRequest);
    }

    public OrganizationResponse Execute(OrganizationRequest request, IXrmFakedContext ctx)
    {
        var typed = (BulkDeleteRequest) request;
        var asyncOperationId = ctx.GetOrganizationService().Create(new AsyncOperation
        {
            Name = typed.JobName,
            OperationType = new OptionSetValue(AsyncOperation.Options.OperationType.BulkDelete),
            RecurrenceStartTime = typed.StartDateTime,
            StatusCode = new OptionSetValue(AsyncOperation.Options.StatusCode.WaitingForResources),
            RecurrencePattern = typed.RecurrencePattern,
            Data =
                "<string>&lt;fetch version=\"1.0\" output-format=\"xml-platform\" mapping=\"logical\" &gt;&lt;entity name=\"testentity\" &gt;&lt;attribute name=\"name\" /&gt;&lt;/entity&gt;&lt;/fetch&gt;</string>"
        });

        Task.Run(() =>
        {
            Thread.Sleep(Random.Next(500, 2000));
            ctx.GetOrganizationService().Update(new AsyncOperation
            {
                Id = asyncOperationId,
                StatusCode = new OptionSetValue(ExpectedStatusCode)
            });
        });

        return new BulkDeleteResponse
        {
            [nameof(BulkDeleteResponse.JobId)] = asyncOperationId
        };
    }
}