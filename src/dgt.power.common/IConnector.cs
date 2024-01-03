// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using Microsoft.PowerPlatform.Dataverse.Client;

namespace dgt.power.common;

public interface IConnector
{
    IOrganizationServiceAsync2 GetOrganizationServiceProxy();
}
