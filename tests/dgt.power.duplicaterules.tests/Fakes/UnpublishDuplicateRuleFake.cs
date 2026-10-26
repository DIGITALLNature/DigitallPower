// Copyright (c) DIGITALL Nature. All rights reserved

using dgt.power.dataverse;
using Digitall.Dataverse.Testing;
using Digitall.Dataverse.Testing.OrganizationRequests;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.duplicaterules.tests.Fakes;

/// <summary>
/// Mirrors observed Dataverse behavior: unpublish is synchronous and cancels a running publish job.
/// </summary>
public sealed class UnpublishDuplicateRuleFake : IOrganizationRequestFake
{
    public Type ForType => typeof(UnpublishDuplicateRuleRequest);

    public HashSet<Guid> FailingRules { get; } = [];

    public HashSet<Guid> UnconfirmedRules { get; } = [];

    public List<Guid> Requested { get; } = [];

    public OrganizationResponse Execute(OrganizationRequest organizationRequest, FakeOrganizationService fakeOrganizationService)
    {
        var ruleId = ((UnpublishDuplicateRuleRequest)organizationRequest).DuplicateRuleId;
        Requested.Add(ruleId);
        if (FailingRules.Contains(ruleId))
        {
            throw new InvalidOperationException("Unpublish denied.");
        }

        if (!UnconfirmedRules.Contains(ruleId))
        {
            PublishDuplicateRuleFake.SetState(
                fakeOrganizationService,
                ruleId,
                DuplicateRule.Options.StateCode.Inactive,
                DuplicateRule.Options.StatusCode.Unpublished);
            var query = new QueryExpression("asyncoperation") { ColumnSet = new ColumnSet(true) };
            query.Criteria.AddCondition("regardingobjectid", ConditionOperator.Equal, ruleId);
            query.Criteria.AddCondition("statecode", ConditionOperator.NotEqual, 3);
            foreach (var job in fakeOrganizationService.RetrieveMultiple(query).Entities)
            {
                fakeOrganizationService.Update(new Entity("asyncoperation", job.Id)
                {
                    ["statecode"] = new OptionSetValue(3),
                    ["statuscode"] = new OptionSetValue(32)
                });
            }
        }

        return new UnpublishDuplicateRuleResponse();
    }
}
