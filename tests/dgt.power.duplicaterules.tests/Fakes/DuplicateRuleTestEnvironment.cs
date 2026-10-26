// Copyright (c) DIGITALL Nature. All rights reserved

using dgt.power.common.Ownership;
using dgt.power.dataverse;
using dgt.power.duplicaterules.Logic;
using Digitall.Dataverse.Testing;
using Digitall.Dataverse.Testing.OrganizationRequests;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.duplicaterules.tests.Fakes;

public sealed class DuplicateRuleTestEnvironment
{
    public DuplicateRuleTestEnvironment(bool includePrivilege = true, params IOrganizationRequestFake[] overrides)
    {
        Service = new FakeOrganizationServiceAsync();
        TeamPrivileges = new TeamPrivilegesFake(GrantedPrincipals, PrivilegeId);
        Service.AddRequests(overrides);
        Service.AddRequests(
            Publish,
            Unpublish,
            new UserPrivilegesFake(GrantedPrincipals, PrivilegeId),
            TeamPrivileges);
        Service.AddDefaultRequests();
        if (includePrivilege)
        {
            Service.Create(new Privilege(PrivilegeId) { Name = DuplicateRulePreflight.ReadPrivilegeName });
        }

        Connection = new TestDataverseConnection(Service);
    }

    public FakeOrganizationServiceAsync Service { get; }

    public TestDataverseConnection Connection { get; }

    public PublishDuplicateRuleFake Publish { get; } = new();

    public UnpublishDuplicateRuleFake Unpublish { get; } = new();

    public TeamPrivilegesFake TeamPrivileges { get; }

    public HashSet<Guid> GrantedPrincipals { get; } = [];

    private Guid PrivilegeId { get; } = Guid.NewGuid();

    public Guid AddRule(
        string table,
        DuplicateRulePublicationState state,
        EntityReference owner,
        string? name = null,
        Guid? id = null)
    {
        var ruleId = id ?? Guid.NewGuid();
        var (stateCode, statusCode) = state switch
        {
            DuplicateRulePublicationState.Published => (DuplicateRule.Options.StateCode.Active, DuplicateRule.Options.StatusCode.Published),
            DuplicateRulePublicationState.Publishing => (DuplicateRule.Options.StateCode.Inactive, DuplicateRule.Options.StatusCode.Publishing),
            DuplicateRulePublicationState.Unpublished => (DuplicateRule.Options.StateCode.Inactive, DuplicateRule.Options.StatusCode.Unpublished),
            _ => (DuplicateRule.Options.StateCode.Active, DuplicateRule.Options.StatusCode.Unpublished)
        };
        Service.Create(new Entity(DuplicateRule.EntityLogicalName, ruleId)
        {
            ["name"] = name ?? $"Rule {ruleId:N}",
            ["baseentityname"] = table,
            ["statecode"] = new OptionSetValue(stateCode),
            ["statuscode"] = new OptionSetValue(statusCode),
            ["ownerid"] = owner
        });
        return ruleId;
    }

    public EntityReference AddUser(string domainName, bool disabled = false, bool granted = true)
    {
        var id = Guid.NewGuid();
        Service.Create(new Entity("systemuser", id)
        {
            ["domainname"] = domainName,
            ["isdisabled"] = disabled
        });
        if (granted)
        {
            GrantedPrincipals.Add(id);
        }

        return new EntityReference("systemuser", id);
    }

    public Entity GetRule(Guid id) => Service.Retrieve(DuplicateRule.EntityLogicalName, id, new ColumnSet(true));

    public DuplicateRulePublicationState StateOf(Guid id) => DuplicateRuleStates.Classify(GetRule(id));

    public EntityReference OwnerOf(Guid id) => GetRule(id).GetAttributeValue<EntityReference>("ownerid");

    public static OwnerMapping UserMapping(params (string Key, string DomainName)[] owners) => new(
        1,
        owners.ToDictionary(
            owner => owner.Key,
            owner => new OwnerMappingEntry { Kind = OwnerKind.User, DomainName = owner.DomainName },
            StringComparer.Ordinal));
}
