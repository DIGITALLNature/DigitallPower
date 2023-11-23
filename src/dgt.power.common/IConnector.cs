// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using Microsoft.Xrm.Sdk;

namespace dgt.power.common;

public interface IConnector
{
    IOrganizationService GetOrganizationServiceProxy();
}
