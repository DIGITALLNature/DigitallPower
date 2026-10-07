// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using Digitall.Dataverse.Testing;
using Digitall.Dataverse.Testing.OrganizationRequests;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.plugin.tests.Deployment.Reconciliation;

internal sealed class SolutionMembershipExecutor : IOrganizationRequestFake
{
    public Type ForType => typeof(AddSolutionComponentRequest);
    internal List<AddSolutionComponentRequest> Requests { get; } = [];
    internal bool FailWrites { get; set; }

    public OrganizationResponse Execute(OrganizationRequest organizationRequest, FakeOrganizationService fakeOrganizationService)
    {
        var request = (AddSolutionComponentRequest)organizationRequest;
        if (FailWrites)
        {
            throw new InvalidOperationException("Injected membership failure before write.");
        }

        Requests.Add(request);
        var query = new QueryExpression(Solution.EntityLogicalName)
        {
            Criteria = new FilterExpression
            {
                Conditions = { new ConditionExpression(Solution.LogicalNames.UniqueName, ConditionOperator.Equal, request.SolutionUniqueName) }
            }
        };
        var solutionId = fakeOrganizationService.RetrieveMultiple(query).Entities.Single().Id;
        fakeOrganizationService.Create(new SolutionComponent
        {
            Attributes =
            {
                [SolutionComponent.LogicalNames.ObjectId] = request.ComponentId,
                [SolutionComponent.LogicalNames.SolutionId] = new EntityReference(Solution.EntityLogicalName, solutionId),
                [SolutionComponent.LogicalNames.ComponentType] = new OptionSetValue(request.ComponentType)
            }
        });
        return new AddSolutionComponentResponse();
    }
}
