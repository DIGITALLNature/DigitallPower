// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.common;

public interface IXrmConnection
{
    IOrganizationService Connect();
}
