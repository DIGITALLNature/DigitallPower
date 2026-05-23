// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using Digitall.Dataverse.Testing;
using Digitall.Dataverse.Testing.OrganizationRequests;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;

namespace dgt.power.tests.FakeExecutor;

public class BulkDeleteExecutor : IOrganizationRequestFake
{
    public int ExpectedStatusCode { get; set; } = AsyncOperation.Options.StatusCode.Succeeded;

    public Type ForType => typeof(BulkDeleteRequest);

    public OrganizationResponse Execute(OrganizationRequest organizationRequest, FakeOrganizationService state)
    {
        var typed = (BulkDeleteRequest)organizationRequest;
        var asyncOperationId = state.Create(new AsyncOperation
        {
            Name = typed.JobName,
            OperationType = new OptionSetValue(AsyncOperation.Options.OperationType.BulkDelete),
            RecurrenceStartTime = typed.StartDateTime,
            StatusCode = new OptionSetValue(AsyncOperation.Options.StatusCode.WaitingForResources),
            RecurrencePattern = typed.RecurrencePattern,
            OwnerId = new EntityReference("systemuser", state.Options.UserId),
            Data =
                "<string>&lt;fetch version=\"1.0\" output-format=\"xml-platform\" mapping=\"logical\" &gt;&lt;entity name=\"testentity\" &gt;&lt;attribute name=\"name\" /&gt;&lt;/entity&gt;&lt;/fetch&gt;</string>"
        });

        Task.Run(() =>
        {
            Thread.Sleep(TestFixtures.FakeCallDurations);
            state.Update(new AsyncOperation
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
