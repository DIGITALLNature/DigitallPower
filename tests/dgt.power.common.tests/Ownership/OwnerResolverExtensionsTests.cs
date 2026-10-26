// Copyright (c) DIGITALL Nature. All rights reserved

using dgt.power.common.Ownership;
using Digitall.Dataverse.Testing;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;

namespace dgt.power.common.tests.Ownership;

public class OwnerResolverExtensionsTests
{
    private static readonly OwnerMapping s_emptyMapping = new(1, new Dictionary<string, OwnerMappingEntry>(StringComparer.Ordinal));

    [Test]
    public async Task CaptureRecordOwnersAsync_SplitsRecordsIntoKeysAndSkippedPrincipals()
    {
        var mapped = new EntityReference("team", Guid.NewGuid());
        var platform = new EntityReference("systemuser", Guid.NewGuid());
        var firstRecord = Guid.NewGuid();
        var secondRecord = Guid.NewGuid();
        var thirdRecord = Guid.NewGuid();
        var resolver = new CaptureStub(new OwnerCaptureResult(
            [new OwnerCapture(mapped, "team")],
            [new SkippedOwnerCapture(platform, OwnerCaptureSkipReason.PlatformPrincipal, "SYSTEM")]));

        var result = await resolver.CaptureRecordOwnersAsync(
            new FakeOrganizationServiceAsync(),
            s_emptyMapping,
            new Dictionary<Guid, EntityReference>
            {
                [firstRecord] = mapped,
                [secondRecord] = platform,
                [thirdRecord] = new("systemuser", platform.Id)
            },
            UnmappedOwnerHandling.Fail);

        await Assert.That(result.OwnerKeys[firstRecord]).IsEqualTo("team");
        await Assert.That(result.SkippedRecords.Keys).IsEquivalentTo([secondRecord, thirdRecord]);
        await Assert.That(resolver.RequestedOwners).Count().IsEqualTo(2);
    }

    [Test]
    public async Task CaptureRecordOwnersAsync_FailsForPrincipalMissingFromResolverResult()
    {
        var resolver = new CaptureStub(new OwnerCaptureResult([], []));

        var exception = await Assert.That(async () => await resolver.CaptureRecordOwnersAsync(
                new FakeOrganizationServiceAsync(),
                s_emptyMapping,
                new Dictionary<Guid, EntityReference> { [Guid.NewGuid()] = new("team", Guid.NewGuid()) },
                UnmappedOwnerHandling.Skip))
            .Throws<OwnerResolutionException>();

        await Assert.That(exception!.Failure).IsEqualTo(OwnerResolutionFailure.MissingCaptureMapping);
    }

    [Test]
    public async Task CaptureRecordOwnersAsync_EmptyInputDoesNotCallResolver()
    {
        var resolver = new CaptureStub(new OwnerCaptureResult([], []));

        var result = await resolver.CaptureRecordOwnersAsync(
            new FakeOrganizationServiceAsync(),
            s_emptyMapping,
            new Dictionary<Guid, EntityReference>(),
            UnmappedOwnerHandling.Fail);

        await Assert.That(result.OwnerKeys).IsEmpty();
        await Assert.That(resolver.RequestedOwners).IsEmpty();
    }

    private sealed class CaptureStub(OwnerCaptureResult result) : IOwnerResolver
    {
        public List<EntityReference> RequestedOwners { get; } = [];

        public Task<IReadOnlyDictionary<string, ResolvedOwner>> ResolveAsync(
            IOrganizationServiceAsync2 service,
            OwnerMapping mapping,
            IEnumerable<string> requestedKeys,
            CancellationToken cancellationToken = default) =>
            throw new NotSupportedException();

        public Task<OwnerCaptureResult> ResolveForCaptureAsync(
            IOrganizationServiceAsync2 service,
            OwnerMapping mapping,
            IEnumerable<EntityReference> capturedOwners,
            UnmappedOwnerHandling unmappedOwners = UnmappedOwnerHandling.Fail,
            CancellationToken cancellationToken = default)
        {
            RequestedOwners.AddRange(capturedOwners);
            return Task.FromResult(result);
        }
    }
}
