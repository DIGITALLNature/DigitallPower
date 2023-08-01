// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using FakeXrmEasy.Abstractions;
using FakeXrmEasy.Abstractions.FakeMessageExecutors;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;

namespace dgt.power.tests.FakeExecutor;

public class UnpublishDuplicateRuleExecutor : IFakeMessageExecutor
{
    public bool CanExecute(OrganizationRequest request)
    {
        return request is UnpublishDuplicateRuleRequest;
    }

    public Type GetResponsibleRequestType()
    {
        return typeof(UnpublishDuplicateRuleRequest);
    }

    public OrganizationResponse Execute(OrganizationRequest request, IXrmFakedContext ctx)
    {
        var typed = (UnpublishDuplicateRuleRequest)request;

        ctx.GetOrganizationService().Update(new DuplicateRule(typed.DuplicateRuleId)
        {
            Attributes =
            {
                {"statecode",new OptionSetValue(DuplicateRule.Options.StateCode.Inactive)},
                {"statuscode",new OptionSetValue(DuplicateRule.Options.StatusCode.Unpublished)},
            }
        });

        return new UnpublishDuplicateRuleResponse();
    }
}