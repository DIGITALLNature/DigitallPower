// Copyright (c) DIGITALL Nature. All rights reserved

using System.ServiceModel;
using Microsoft.Xrm.Sdk;

namespace dgt.power.duplicaterules.tests.Fakes;

public static class DuplicateRuleFaultFactory
{
    public static FaultException<OrganizationServiceFault> Create(int errorCode, string message) =>
        new(new OrganizationServiceFault { ErrorCode = errorCode, Message = message }, new FaultReason(message));
}
