// Copyright (c) DIGITALL Nature. All rights reserved

using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;

namespace dgt.power.common.Ownership;

public interface IOwnerResolver
{
    Task<IReadOnlyDictionary<string, ResolvedOwner>> ResolveAsync(
        IOrganizationServiceAsync2 service,
        OwnerMapping mapping,
        IEnumerable<string> requestedKeys,
        CancellationToken cancellationToken = default);

    Task<OwnerCaptureResult> ResolveForCaptureAsync(
        IOrganizationServiceAsync2 service,
        OwnerMapping mapping,
        IEnumerable<EntityReference> capturedOwners,
        UnmappedOwnerHandling unmappedOwners = UnmappedOwnerHandling.Fail,
        CancellationToken cancellationToken = default);
}
