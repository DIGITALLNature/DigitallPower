// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.plugin.Repositories;
using Digitall.Dataverse.Testing;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.plugin.tests.Repositories;

public class SdkMessageProcessingStepSecureConfigRepositoryTests
{
    [Test]
    public async Task UpsertAsync_WhenTransactionFails_SubmitsAtomicCreateAndLinkAndPropagatesFailure()
    {
        var service = new FakeOrganizationServiceAsync();
        var executor = new RejectSecureConfigTransactionExecutor();
        service.AddRequests(executor);
        service.AddDefaultRequests();
        var repository = new SdkMessageProcessingStepSecureConfigRepository(service);
        var stepId = Guid.NewGuid();

        await Assert.That(() => repository.UpsertAsync(stepId, "secure-value"))
            .ThrowsExactly<InvalidOperationException>();

        var request = executor.Request ?? throw new InvalidOperationException("No transaction was submitted.");
        await Assert.That(request.ReturnResponses).IsFalse();
        await Assert.That(request.Requests.Count).IsEqualTo(2);
        var create = (CreateRequest)request.Requests[0];
        var update = (UpdateRequest)request.Requests[1];
        var secureConfig = create.Target.ToEntity<SdkMessageProcessingStepSecureConfig>();
        var step = update.Target.ToEntity<SdkMessageProcessingStep>();
        await Assert.That(secureConfig.Id).IsNotEqualTo(Guid.Empty);
        await Assert.That(secureConfig.SecureConfig).IsEqualTo("secure-value");
        await Assert.That(step.Id).IsEqualTo(stepId);
        await Assert.That(step.SdkMessageProcessingStepSecureConfigId?.Id).IsEqualTo(secureConfig.Id);

        var configurations = await service.RetrieveMultipleAsync(
            new QueryExpression(SdkMessageProcessingStepSecureConfig.EntityLogicalName)
            {
                ColumnSet = new ColumnSet(true)
            });

        await Assert.That(configurations.Entities.Count).IsEqualTo(0);
    }
}
