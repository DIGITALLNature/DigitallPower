// Copyright (c) DIGITALL Nature. All rights reserved

using dgt.power.common;
using Microsoft.PowerPlatform.Dataverse.Client;

namespace dgt.power.duplicaterules.tests.Fakes;

public sealed class TestDataverseConnection(IOrganizationServiceAsync2 service) : IDataverseConnection
{
    public int StandardCalls { get; private set; }

    public List<Guid> CallerIds { get; } = [];

    public Task<IOrganizationServiceAsync2> GetServiceAsync()
    {
        StandardCalls++;
        return Task.FromResult(service);
    }

    public Task<IOrganizationServiceAsync2> GetServiceAsync(Guid callerId)
    {
        CallerIds.Add(callerId);
        return Task.FromResult(service);
    }

    public Task<bool> CheckAuthAsync() => Task.FromResult(true);

    public Task RefreshAuthAsync() => Task.CompletedTask;
}
