// Copyright (c) DIGITALL Nature. All rights reserved

using dgt.power.common.Ownership;
using Digitall.Dataverse.Testing;
using Digitall.Dataverse.Testing.OrganizationRequests;

namespace dgt.power.common.tests.Ownership;

public class OwnerResolutionConsumerBoundaryTests
{
    private readonly IOwnerResolver _resolver = new OwnerResolver();

    [Test]
    public async Task StateOnlyOperationWithoutOwnerIntent_DoesNotRequireMappingOrTargetLookup()
    {
        var service = new FakeOrganizationServiceAsync();
        var boundary = new BoundaryHarness(_resolver, service);

        await boundary.RunAsync(mappingPath: null, requestedKeys: [], capturedOwners: [], beforeOutputWrite: () => { });

        await Assert.That(boundary.LoadedMapping).IsNull();
        await Assert.That(boundary.TargetLookups).IsEqualTo(0);
    }

    [Test]
    public async Task SuppliedMappingIsValidatedEvenWhenNoOwnerWasRequested()
    {
        var path = FixturePath("invalid-unused-owner-mapping.json");
        var boundary = new BoundaryHarness(_resolver, new FakeOrganizationServiceAsync());

        var exception = await Assert.That(async () => await boundary.RunAsync(
                path,
                requestedKeys: [],
                capturedOwners: [],
                beforeOutputWrite: () => { }))
            .Throws<OwnerResolutionException>();

        await Assert.That(exception!.Failure).IsEqualTo(OwnerResolutionFailure.InvalidMapping);
        await Assert.That(boundary.TargetLookups).IsEqualTo(0);
    }

    [Test]
    public async Task CaptureFailureOccursBeforeConsumerOutputWrite()
    {
        var service = new FakeOrganizationServiceAsync();
        service.AddRequests(new EmptyExecutor());
        var boundary = new BoundaryHarness(_resolver, service);
        var outputWrites = 0;

        var exception = await Assert.That(async () => await boundary.RunAsync(
                FixturePath("owner-mapping.example.json"),
                requestedKeys: [],
                capturedOwners: [new Microsoft.Xrm.Sdk.EntityReference("team", Guid.NewGuid())],
                beforeOutputWrite: () => outputWrites++))
            .Throws<OwnerResolutionException>();

        await Assert.That(exception!.Failure).IsEqualTo(OwnerResolutionFailure.NotFound);
        await Assert.That(outputWrites).IsEqualTo(0);
    }

    [Test]
    public async Task OwnerApplyResolvesOnlyRequestedLogicalKeys()
    {
        var user = new Microsoft.Xrm.Sdk.Entity("systemuser", Guid.NewGuid())
        {
            ["domainname"] = "deployment@example.org",
            ["isdisabled"] = false
        };
        var service = new FakeOrganizationServiceAsync();
        service.AddRequests(new UserExecutor(user));
        var boundary = new BoundaryHarness(_resolver, service);

        await boundary.RunAsync(
            FixturePath("owner-mapping.example.json"),
            requestedKeys: ["humanOwner"],
            capturedOwners: [],
            beforeOutputWrite: () => { });

        await Assert.That(boundary.ResolvedOwners!["humanOwner"].Principal.Id).IsEqualTo(user.Id);
        await Assert.That(boundary.ResolvedOwners!.ContainsKey("salesOwner")).IsFalse();
    }

    [Test]
    public async Task ResolvedPrincipalMetadataIsReturnedWithoutConsumerEligibilityPolicy()
    {
        var disabledUser = new Microsoft.Xrm.Sdk.Entity("systemuser", Guid.NewGuid())
        {
            ["domainname"] = "disabled@example.org",
            ["isdisabled"] = true
        };
        var service = new FakeOrganizationServiceAsync();
        var executor = new UserExecutor(disabledUser);
        service.AddRequests(executor);
        var mapping = OwnerMappingLoader.Parse("""{"schemaVersion":1,"owners":{"owner":{"kind":"user","domainName":"disabled@example.org"}}}""");

        var resolved = await _resolver.ResolveAsync(service, mapping, ["owner"]);

        await Assert.That(resolved["owner"].IsDisabled).IsTrue();
        await Assert.That(resolved["owner"].Kind).IsEqualTo(OwnerKind.User);
    }

    private static string FixturePath(string name) =>
        Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, $"../../../Ownership/Fixtures/{name}"));

    private sealed class BoundaryHarness(IOwnerResolver ownerResolver, FakeOrganizationServiceAsync service)
    {
        public OwnerMapping? LoadedMapping { get; private set; }

        public int TargetLookups { get; private set; }

        public IReadOnlyDictionary<string, ResolvedOwner>? ResolvedOwners { get; private set; }

        public async Task RunAsync(
            string? mappingPath,
            IReadOnlyCollection<string> requestedKeys,
            IReadOnlyCollection<Microsoft.Xrm.Sdk.EntityReference> capturedOwners,
            Action beforeOutputWrite)
        {
            if (mappingPath is null && requestedKeys.Count == 0 && capturedOwners.Count == 0)
            {
                return;
            }

            if (mappingPath is null)
            {
                throw new OwnerResolutionException(OwnerResolutionFailure.InvalidMapping, "An owner mapping is required.");
            }

            LoadedMapping = OwnerMappingLoader.Load(mappingPath);
            if (capturedOwners.Count > 0)
            {
                TargetLookups++;
                await ownerResolver.ResolveForCaptureAsync(service, LoadedMapping, capturedOwners);
            }
            else if (requestedKeys.Count > 0)
            {
                TargetLookups++;
                ResolvedOwners = await ownerResolver.ResolveAsync(service, LoadedMapping, requestedKeys);
            }

            beforeOutputWrite();
        }
    }

    private sealed class UserExecutor(Microsoft.Xrm.Sdk.Entity user) : IOrganizationRequestFake
    {
        public Type ForType => typeof(Microsoft.Xrm.Sdk.Messages.RetrieveMultipleRequest);

        public Microsoft.Xrm.Sdk.OrganizationResponse Execute(
            Microsoft.Xrm.Sdk.OrganizationRequest organizationRequest,
            FakeOrganizationService fakeOrganizationService) =>
            new Microsoft.Xrm.Sdk.Messages.RetrieveMultipleResponse
            {
                [nameof(Microsoft.Xrm.Sdk.Messages.RetrieveMultipleResponse.EntityCollection)] =
                    new Microsoft.Xrm.Sdk.EntityCollection([user])
            };
    }

    private sealed class EmptyExecutor : IOrganizationRequestFake
    {
        public Type ForType => typeof(Microsoft.Xrm.Sdk.Messages.RetrieveMultipleRequest);

        public Microsoft.Xrm.Sdk.OrganizationResponse Execute(
            Microsoft.Xrm.Sdk.OrganizationRequest organizationRequest,
            FakeOrganizationService fakeOrganizationService) =>
            new Microsoft.Xrm.Sdk.Messages.RetrieveMultipleResponse
            {
                [nameof(Microsoft.Xrm.Sdk.Messages.RetrieveMultipleResponse.EntityCollection)] =
                    new Microsoft.Xrm.Sdk.EntityCollection()
            };
    }
}
