// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using Microsoft.PowerPlatform.Dataverse.Client;

namespace dgt.power.common.tests.Logic;

internal sealed class DeferredDataverseConnector(Task<ServiceClient> pending) : IDataverseConnector
{
    internal int Calls { get; private set; }

    public Task<ServiceClient> CreateServiceClientAsync()
    {
        Calls++;
        return pending;
    }
}
