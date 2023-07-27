// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using FakeXrmEasy.Abstractions;
using FakeXrmEasy.Abstractions.FakeMessageExecutors;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;

namespace dgt.power.tests.FakeExecutor;

public class PublishDuplicateRuleExecutor : IFakeMessageExecutor
{
    public bool CanExecute(OrganizationRequest request)
    {
        return request is PublishDuplicateRuleRequest;
    }

    public Type GetResponsibleRequestType()
    {
        return typeof(PublishDuplicateRuleRequest);
    }

    public OrganizationResponse Execute(OrganizationRequest request, IXrmFakedContext ctx)
    {
        var typed = (PublishDuplicateRuleRequest)request;

        ctx.GetOrganizationService().Update(new DuplicateRule(typed.DuplicateRuleId)
        {
            Attributes =
            {
                {"statecode",new OptionSetValue(DuplicateRule.Options.StateCode.Active)},
                {"statuscode",new OptionSetValue(DuplicateRule.Options.StatusCode.Published)},
            }
        });

        return new PublishDuplicateRuleResponse();
    }
}