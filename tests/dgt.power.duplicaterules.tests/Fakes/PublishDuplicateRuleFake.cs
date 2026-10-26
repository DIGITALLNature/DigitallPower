// Copyright (c) DIGITALL Nature. All rights reserved

using dgt.power.dataverse;
using dgt.power.duplicaterules.Logic;
using Digitall.Dataverse.Testing;
using Digitall.Dataverse.Testing.OrganizationRequests;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.duplicaterules.tests.Fakes;

/// <summary>
/// Mirrors observed Dataverse behavior: publish is accepted synchronously, the rule moves to Publishing,
/// and an asyncoperation completes later. Faults use the observed error codes.
/// </summary>
public sealed class PublishDuplicateRuleFake : IOrganizationRequestFake
{
    public Type ForType => typeof(PublishDuplicateRuleRequest);

    public HashSet<Guid> RulesWithoutCriteria { get; } = [];

    public Dictionary<Guid, Guid> JobsByRule { get; } = [];

    public List<Guid> Submitted { get; } = [];

    public Action<Guid>? OnSubmit { get; set; }

    public OrganizationResponse Execute(OrganizationRequest organizationRequest, FakeOrganizationService fakeOrganizationService)
    {
        var ruleId = ((PublishDuplicateRuleRequest)organizationRequest).DuplicateRuleId;
        Submitted.Add(ruleId);
        var rule = fakeOrganizationService.Retrieve(DuplicateRule.EntityLogicalName, ruleId, new ColumnSet(true));
        var state = DuplicateRuleStates.Classify(rule);
        if (state == DuplicateRulePublicationState.Publishing)
        {
            throw DuplicateRuleFaultFactory.Create(DuplicateRuleFaults.AlreadyPublishing, "The duplicate rule is already being published.");
        }

        var response = new PublishDuplicateRuleResponse();
        if (state == DuplicateRulePublicationState.Published)
        {
            response.Results["JobId"] = Guid.Empty;
            return response;
        }

        if (RulesWithoutCriteria.Contains(ruleId))
        {
            throw DuplicateRuleFaultFactory.Create(DuplicateRuleFaults.NoCriteria, "The duplicate rule has no criteria.");
        }

        var table = rule.GetAttributeValue<string>("baseentityname");
        var query = new QueryExpression(DuplicateRule.EntityLogicalName) { ColumnSet = new ColumnSet(true) };
        query.Criteria.AddCondition("baseentityname", ConditionOperator.Equal, table);
        var occupied = fakeOrganizationService.RetrieveMultiple(query).Entities
            .Count(existing => DuplicateRuleStates.Classify(existing) is DuplicateRulePublicationState.Published or DuplicateRulePublicationState.Publishing);
        if (occupied >= DuplicateRulePreflight.MaximumPublishedRulesPerTable)
        {
            throw DuplicateRuleFaultFactory.Create(DuplicateRuleFaults.MaximumPublishedRules, "The maximum number of published rules was reached.");
        }

        SetState(fakeOrganizationService, ruleId, DuplicateRule.Options.StateCode.Inactive, DuplicateRule.Options.StatusCode.Publishing);
        var jobId = fakeOrganizationService.Create(new Entity("asyncoperation")
        {
            ["statecode"] = new OptionSetValue(0),
            ["statuscode"] = new OptionSetValue(0),
            ["regardingobjectid"] = new EntityReference(DuplicateRule.EntityLogicalName, ruleId)
        });
        JobsByRule[ruleId] = jobId;
        response.Results["JobId"] = jobId;
        OnSubmit?.Invoke(ruleId);
        return response;
    }

    public void Complete(IOrganizationService service, Guid ruleId, int status = 30, string? message = null)
    {
        ArgumentNullException.ThrowIfNull(service);
        var job = new Entity("asyncoperation", JobsByRule[ruleId])
        {
            ["statecode"] = new OptionSetValue(3),
            ["statuscode"] = new OptionSetValue(status)
        };
        if (message is not null)
        {
            job["friendlymessage"] = message;
        }

        service.Update(job);
        SetState(
            service,
            ruleId,
            status == 30 ? DuplicateRule.Options.StateCode.Active : DuplicateRule.Options.StateCode.Inactive,
            status == 30 ? DuplicateRule.Options.StatusCode.Published : DuplicateRule.Options.StatusCode.Unpublished);
    }

    public void CompleteAll(IOrganizationService service, int status = 30)
    {
        foreach (var ruleId in JobsByRule.Keys.ToArray())
        {
            Complete(service, ruleId, status);
        }
    }

    internal static void SetState(IOrganizationService service, Guid ruleId, int stateCode, int statusCode) =>
        service.Update(new Entity(DuplicateRule.EntityLogicalName, ruleId)
        {
            ["statecode"] = new OptionSetValue(stateCode),
            ["statuscode"] = new OptionSetValue(statusCode)
        });
}
