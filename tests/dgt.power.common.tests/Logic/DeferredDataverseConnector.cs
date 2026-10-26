// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using Microsoft.PowerPlatform.Dataverse.Client;

namespace dgt.power.common.tests.Logic;

internal sealed class DeferredDataverseConnector : IDataverseConnector
{
    private readonly TaskCompletionSource<ServiceClient> _pending =
        new(TaskCreationOptions.RunContinuationsAsynchronously);

    internal int Calls { get; private set; }

    internal void Complete(ServiceClient client) => _pending.SetResult(client);

    public Task<ServiceClient> CreateServiceClientAsync()
    {
        Calls++;
#pragma warning disable VSTHRD003 // This fixture owns the completion source; the test completes it without a UI-thread dependency.
        return _pending.Task;
#pragma warning restore VSTHRD003
    }
}
