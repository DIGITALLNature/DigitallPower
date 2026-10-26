// Copyright (c) DIGITALL Nature. All rights reserved

using Digitall.Dataverse.Testing;
using Digitall.Dataverse.Testing.OrganizationRequests;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;

namespace dgt.power.duplicaterules.tests.Fakes;

public sealed class TeamPrivilegesFake(ISet<Guid> grantedPrincipals, Guid privilegeId) : IOrganizationRequestFake
{
    public Type ForType => typeof(RetrieveTeamPrivilegesRequest);

    public List<Guid> Requested { get; } = [];

    public OrganizationResponse Execute(OrganizationRequest organizationRequest, FakeOrganizationService fakeOrganizationService)
    {
        var teamId = ((RetrieveTeamPrivilegesRequest)organizationRequest).TeamId;
        Requested.Add(teamId);
        var response = new RetrieveTeamPrivilegesResponse
        {
            Results =
            {
                ["RolePrivileges"] = grantedPrincipals.Contains(teamId)
                    ? new[] { new RolePrivilege { PrivilegeId = privilegeId } }
                    : Array.Empty<RolePrivilege>()
            }
        };
        return response;
    }
}
