// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common;
using Microsoft.PowerPlatform.Dataverse.Client;

namespace dgt.power.connection.tests;

internal sealed class FakeXrmConnection : IXrmConnection
{
    public bool CheckAuthResult { get; init; } = true;

    public bool RefreshThrows { get; init; }

    private static readonly Exception s_refreshException = new InvalidOperationException("refresh failed");

    public int RefreshCalls { get; private set; }

    public Task<IOrganizationServiceAsync2> ConnectAsync() =>
        Task.FromException<IOrganizationServiceAsync2>(new NotSupportedException());

    public Task<bool> CheckAuthAsync() => Task.FromResult(CheckAuthResult);

    public Task RefreshAuthAsync()
    {
        RefreshCalls++;
        return RefreshThrows ? Task.FromException(s_refreshException) : Task.CompletedTask;
    }
}
