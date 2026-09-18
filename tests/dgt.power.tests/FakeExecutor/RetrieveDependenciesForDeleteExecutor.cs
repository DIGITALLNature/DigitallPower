// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using Digitall.Dataverse.Testing;
using Digitall.Dataverse.Testing.OrganizationRequests;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.tests.FakeExecutor;

/// <summary>
/// Minimal fake for <see cref="RetrieveDependenciesForDeleteRequest"/>, scoped to what
/// <c>PluginTypeRepository.GetDependentStepIdsAsync</c> needs: for a <c>plugintype</c>
/// (component type 90), it returns the <c>sdkmessageprocessingstep</c> records (component type
/// 92) whose <c>eventhandler</c> points at it.
/// </summary>
public class RetrieveDependenciesForDeleteExecutor : IOrganizationRequestFake
{
    public Type ForType => typeof(RetrieveDependenciesForDeleteRequest);

    public OrganizationResponse Execute(OrganizationRequest organizationRequest, FakeOrganizationService fakeOrganizationService)
    {
        var typed = (RetrieveDependenciesForDeleteRequest)organizationRequest;
        var dependencies = new EntityCollection();

        if (typed.ComponentType == 90)
        {
            var query = new QueryExpression(SdkMessageProcessingStep.EntityLogicalName)
            {
                ColumnSet = new ColumnSet(SdkMessageProcessingStep.LogicalNames.SdkMessageProcessingStepId),
                Criteria = new FilterExpression
                {
                    Conditions = { new ConditionExpression(SdkMessageProcessingStep.LogicalNames.EventHandler, ConditionOperator.Equal, typed.ObjectId) }
                }
            };

            foreach (var step in fakeOrganizationService.RetrieveMultiple(query).Entities)
            {
                var dependency = new Entity(Dependency.EntityLogicalName)
                {
                    Attributes =
                    {
                        [Dependency.LogicalNames.DependentComponentType] = new OptionSetValue(92),
                        [Dependency.LogicalNames.DependentComponentObjectId] = step.Id
                    }
                };
                dependencies.Entities.Add(dependency);
            }
        }

        return new RetrieveDependenciesForDeleteResponse
        {
            [nameof(RetrieveDependenciesForDeleteResponse.EntityCollection)] = dependencies
        };
    }
}
