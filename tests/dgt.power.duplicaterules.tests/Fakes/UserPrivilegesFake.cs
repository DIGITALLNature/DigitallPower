// Copyright (c) DIGITALL Nature. All rights reserved

using dgt.power.duplicaterules.Logic;
using Digitall.Dataverse.Testing;
using Digitall.Dataverse.Testing.OrganizationRequests;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;

namespace dgt.power.duplicaterules.tests.Fakes;

public sealed class UserPrivilegesFake(ISet<Guid> grantedPrincipals, Guid privilegeId) : IOrganizationRequestFake
{
    public Type ForType => typeof(RetrieveUserPrivilegesRequest);

    public OrganizationResponse Execute(OrganizationRequest organizationRequest, FakeOrganizationService fakeOrganizationService)
    {
        var userId = ((RetrieveUserPrivilegesRequest)organizationRequest).UserId;
        var response = new RetrieveUserPrivilegesResponse();
        response.Results["RolePrivileges"] = grantedPrincipals.Contains(userId)
            ? new[] { new RolePrivilege { PrivilegeId = privilegeId, PrivilegeName = DuplicateRulePreflight.ReadPrivilegeName } }
            : Array.Empty<RolePrivilege>();
        return response;
    }
}
