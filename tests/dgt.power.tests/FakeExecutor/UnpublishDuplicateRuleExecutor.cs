// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using Digitall.Dataverse.Testing;
using Digitall.Dataverse.Testing.OrganizationRequests;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;

namespace dgt.power.tests.FakeExecutor;

public class UnpublishDuplicateRuleExecutor : IOrganizationRequestFake
{
    public Type ForType => typeof(UnpublishDuplicateRuleRequest);

    public OrganizationResponse Execute(OrganizationRequest organizationRequest, FakeOrganizationService fakeOrganizationService)
    {
        var typed = (UnpublishDuplicateRuleRequest)organizationRequest;

        fakeOrganizationService.Update(new DuplicateRule(typed.DuplicateRuleId)
        {
            Attributes =
            {
                {"statecode",new OptionSetValue(DuplicateRule.Options.StateCode.Inactive)},
                {"statuscode",new OptionSetValue(DuplicateRule.Options.StatusCode.Unpublished)}
            }
        });

        return new UnpublishDuplicateRuleResponse();
    }
}
