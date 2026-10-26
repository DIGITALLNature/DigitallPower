// Copyright (c) DIGITALL Nature. All rights reserved

using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;

namespace dgt.power.common.Ownership;

public static class OwnerResolverExtensions
{
    public static Task<RecordOwnerCapture> CaptureRecordOwnersAsync(
        this IOwnerResolver resolver,
        IOrganizationServiceAsync2 service,
        OwnerMapping mapping,
        IReadOnlyDictionary<Guid, EntityReference> ownersByRecord,
        UnmappedOwnerHandling unmappedOwners,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(resolver);
        ArgumentNullException.ThrowIfNull(ownersByRecord);
        return CaptureRecordOwnersCoreAsync(resolver, service, mapping, ownersByRecord, unmappedOwners, cancellationToken);
    }

    private static async Task<RecordOwnerCapture> CaptureRecordOwnersCoreAsync(
        IOwnerResolver resolver,
        IOrganizationServiceAsync2 service,
        OwnerMapping mapping,
        IReadOnlyDictionary<Guid, EntityReference> ownersByRecord,
        UnmappedOwnerHandling unmappedOwners,
        CancellationToken cancellationToken)
    {
        var ownerKeys = new Dictionary<Guid, string>();
        var skippedRecords = new Dictionary<Guid, SkippedOwnerCapture>();
        if (ownersByRecord.Count == 0)
        {
            return new RecordOwnerCapture(ownerKeys, skippedRecords);
        }

        var result = await resolver.ResolveForCaptureAsync(
            service,
            mapping,
            ownersByRecord.Values.DistinctBy(owner => (owner.LogicalName, owner.Id)),
            unmappedOwners,
            cancellationToken);
        foreach (var (recordId, principal) in ownersByRecord)
        {
            if (result.TryGetLogicalKey(principal, out var key))
            {
                ownerKeys.Add(recordId, key);
            }
            else if (result.FindSkipped(principal) is { } skipped)
            {
                skippedRecords.Add(recordId, skipped);
            }
            else
            {
                throw new OwnerResolutionException(
                    OwnerResolutionFailure.MissingCaptureMapping,
                    $"No owner-mapping key was resolved for record '{recordId:D}'.",
                    capturedOwner: principal);
            }
        }

        return new RecordOwnerCapture(ownerKeys, skippedRecords);
    }
}
