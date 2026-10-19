// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.maintenance.Base;
using dgt.power.maintenance.Logic;
using dgt.power.maintenance.tests.Base;
using dgt.power.dataverse;
using dgt.power.tests;
using Digitall.Dataverse.Testing;
using Digitall.Dataverse.Testing.OrganizationRequests;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.maintenance.tests;

public class FilterPowerFxPluginStepsTests : MaintenanceTestsBase<FilterPowerFxPluginSteps>
{
    [Test]
    public async Task RunAsync_UpdatesFilteringAttributesUsingAsyncDataverseCalls()
    {
        var retrieveExecutor = new RetrieveStepExecutor();
        var updateExecutor = new UpdateStepExecutor();
        var context = GetBuilder()
            .WithFakeMessageExecutor(retrieveExecutor)
            .WithFakeMessageExecutor(updateExecutor)
            .Build();

        var succeeded = context.Execute(new MaintenanceVerb
        {
            Config = GetResourcePath("powerfx.json")
        });

        await Assert.That(succeeded).IsTrue();
        await Assert.That(retrieveExecutor.Query).IsNotNull();
        await Assert.That(retrieveExecutor.Query!.TopCount).IsEqualTo(2);
        await Assert.That(updateExecutor.UpdatedStep).IsNotNull();
        await Assert.That(updateExecutor.UpdatedStep!.ToEntity<SdkMessageProcessingStep>().FilteringAttributesField)
            .IsEqualTo("emailaddress1,name");
    }

    private sealed class RetrieveStepExecutor : IOrganizationRequestFake
    {
        public Type ForType => typeof(RetrieveMultipleRequest);

        public QueryExpression? Query { get; private set; }

        public OrganizationResponse Execute(
            OrganizationRequest organizationRequest,
            FakeOrganizationService fakeOrganizationService)
        {
            Query = (QueryExpression)((RetrieveMultipleRequest)organizationRequest).Query;
            var steps = new EntityCollection([new Entity("sdkmessageprocessingstep", Guid.NewGuid())]);
            return new RetrieveMultipleResponse
            {
                [nameof(RetrieveMultipleResponse.EntityCollection)] = steps
            };
        }
    }

    private sealed class UpdateStepExecutor : IOrganizationRequestFake
    {
        public Type ForType => typeof(UpdateRequest);

        public Entity? UpdatedStep { get; private set; }

        public OrganizationResponse Execute(
            OrganizationRequest organizationRequest,
            FakeOrganizationService fakeOrganizationService)
        {
            UpdatedStep = ((UpdateRequest)organizationRequest).Target;
            return new UpdateResponse();
        }
    }
}
