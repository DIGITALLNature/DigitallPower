// Copyright (c) DIGITALL Nature. All rights reserved

using dgt.power.common.Ownership;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;

namespace dgt.power.duplicaterules.tests.Fakes;

public sealed class StubOwnerResolver(params ResolvedOwner[] owners) : IOwnerResolver
{
    private readonly IReadOnlyDictionary<string, ResolvedOwner> _owners =
        owners.ToDictionary(owner => owner.LogicalKey, StringComparer.Ordinal);

    public Task<IReadOnlyDictionary<string, ResolvedOwner>> ResolveAsync(
        IOrganizationServiceAsync2 service,
        OwnerMapping mapping,
        IEnumerable<string> requestedKeys,
        CancellationToken cancellationToken = default) =>
        Task.FromResult<IReadOnlyDictionary<string, ResolvedOwner>>(
            requestedKeys.Distinct(StringComparer.Ordinal).ToDictionary(key => key, key => _owners[key], StringComparer.Ordinal));

    public Task<OwnerCaptureResult> ResolveForCaptureAsync(
        IOrganizationServiceAsync2 service,
        OwnerMapping mapping,
        IEnumerable<EntityReference> capturedOwners,
        UnmappedOwnerHandling unmappedOwners = UnmappedOwnerHandling.Fail,
        CancellationToken cancellationToken = default) =>
        Task.FromResult(new OwnerCaptureResult([], []));
}
