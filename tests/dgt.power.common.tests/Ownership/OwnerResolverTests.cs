// Copyright (c) DIGITALL Nature. All rights reserved

using dgt.power.common.Ownership;
using Digitall.Dataverse.Testing;
using Digitall.Dataverse.Testing.OrganizationRequests;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using System.Runtime.CompilerServices;

namespace dgt.power.common.tests.Ownership;

public class OwnerResolverTests
{
    private static readonly ConditionalWeakTable<FakeOrganizationServiceAsync, EntityPageExecutor> s_executors = new();
    private readonly IOwnerResolver _resolver = new OwnerResolver();

    [Test]
    public async Task ResolveAsync_DomainName_IsOrdinalCaseInsensitiveAndReturnsDisabledStatus()
    {
        var user = User(Guid.NewGuid(), "DEPLOYMENT@example.org", disabled: true);
        var service = CreateService(user);
        var mapping = Mapping(("operator", new OwnerMappingEntry { Kind = OwnerKind.User, DomainName = "deployment@example.org" }));

        var result = await _resolver.ResolveAsync(service, mapping, ["operator"]);

        await Assert.That(result["operator"].Principal.Id).IsEqualTo(user.Id);
        await Assert.That(result["operator"].Kind).IsEqualTo(OwnerKind.User);
        await Assert.That(result["operator"].IsDisabled).IsTrue();
    }

    [Test]
    public async Task ResolveAsync_EntraUserIdUsesExplicitSelectorWithoutDomainNameFallback()
    {
        var expectedObjectId = Guid.NewGuid();
        var service = CreateService(
            new Entity("systemuser", Guid.NewGuid())
            {
                ["azureactivedirectoryobjectid"] = Guid.NewGuid(),
                ["domainname"] = "same@example.org",
                ["isdisabled"] = false
            });
        var mapping = Mapping(("user", new OwnerMappingEntry { Kind = OwnerKind.User, EntraObjectId = expectedObjectId }));

        var exception = await Assert.That(async () => await _resolver.ResolveAsync(service, mapping, ["user"]))
            .Throws<OwnerResolutionException>();

        await Assert.That(exception!.Failure).IsEqualTo(OwnerResolutionFailure.NotFound);
    }

    [Test]
    public async Task ResolveAsync_EntraUserIdResolvesExactDataverseAttribute()
    {
        var objectId = Guid.NewGuid();
        var user = User(Guid.NewGuid(), "user@example.org", disabled: false);
        user["azureactivedirectoryobjectid"] = objectId;
        var service = CreateService(user);
        var mapping = Mapping(("user", new OwnerMappingEntry { Kind = OwnerKind.User, EntraObjectId = objectId }));

        var result = await _resolver.ResolveAsync(service, mapping, ["user"]);

        await Assert.That(result["user"].Principal.Id).IsEqualTo(user.Id);
    }

    [Test]
    public async Task ResolveAsync_ApplicationUser_UsesApplicationIdAndClassifiesExplicitly()
    {
        var applicationId = Guid.NewGuid();
        var user = User(Guid.NewGuid(), "app@example.org", disabled: false, applicationId);
        var service = CreateService(user);
        var mapping = Mapping(("automation", new OwnerMappingEntry { Kind = OwnerKind.ApplicationUser, ApplicationId = applicationId }));

        var result = await _resolver.ResolveAsync(service, mapping, ["automation"]);

        await Assert.That(result["automation"].Kind).IsEqualTo(OwnerKind.ApplicationUser);
        await Assert.That(result["automation"].Principal.Id).IsEqualTo(user.Id);
        await Assert.That(result["automation"].IsDisabled).IsFalse();
    }

    [Test]
    public async Task ResolveAsync_RegularUserMatchingApplicationIdentity_FailsWrongKind()
    {
        var applicationId = Guid.NewGuid();
        var service = CreateService(User(Guid.NewGuid(), "app@example.org", disabled: false, applicationId));
        var mapping = Mapping(("human", new OwnerMappingEntry { Kind = OwnerKind.User, DomainName = "app@example.org" }));

        var exception = await Assert.That(async () => await _resolver.ResolveAsync(service, mapping, ["human"]))
            .Throws<OwnerResolutionException>();

        await Assert.That(exception!.Failure).IsEqualTo(OwnerResolutionFailure.UnsupportedKind);
    }

    [Test]
    public async Task ResolveAsync_AmbiguityIsReportedBeforeEligibilityClassification()
    {
        var first = new Entity("systemuser", Guid.NewGuid()) { ["domainname"] = "same@example.org" };
        var second = new Entity("systemuser", Guid.NewGuid()) { ["domainname"] = "SAME@example.org" };
        var service = CreateService(first, second);
        var mapping = Mapping(("human", new OwnerMappingEntry { Kind = OwnerKind.User, DomainName = "same@example.org" }));

        var exception = await Assert.That(async () => await _resolver.ResolveAsync(service, mapping, ["human"]))
            .Throws<OwnerResolutionException>();

        await Assert.That(exception!.Failure).IsEqualTo(OwnerResolutionFailure.Ambiguous);
    }

    [Test]
    public async Task ResolveAsync_TeamGlobUsesWholeNameAndTreatsOtherMetacharactersLiterally()
    {
        var businessUnitId = Guid.NewGuid();
        var match = Team(Guid.NewGuid(), "Sales?_%[Group]", businessUnitId, (int)OwnerTeamType.Owner);
        var nonMatch = Team(Guid.NewGuid(), "SalesX_%[Group]", businessUnitId, (int)OwnerTeamType.Owner);
        var service = CreateService(BusinessUnit(businessUnitId, "Sales"), match, nonMatch);
        var mapping = Mapping(("literal", new OwnerMappingEntry
        {
            Kind = OwnerKind.Team,
            NamePattern = "Sales?_%[Group]",
            BusinessUnitName = "Sales"
        }));

        var result = await _resolver.ResolveAsync(service, mapping, ["literal"]);

        await Assert.That(result["literal"].Principal.Id).IsEqualTo(match.Id);
        await Assert.That(result["literal"].TeamType).IsEqualTo(OwnerTeamType.Owner);
    }

    [Test]
    public async Task ResolveAsync_GlobIsWholeNameAndReportsLaterPageAmbiguityBeforeTeamEligibility()
    {
        var businessUnitId = Guid.NewGuid();
        var ownerTeam = Team(Guid.NewGuid(), "Sales-East", businessUnitId, (int)OwnerTeamType.Owner);
        var accessTeam = Team(Guid.NewGuid(), "sales-West", businessUnitId, (int)OwnerTeamType.Access);
        var embeddedPrefix = Team(Guid.NewGuid(), "Other Sales-East", businessUnitId, (int)OwnerTeamType.Owner);
        var service = CreateService(BusinessUnit(businessUnitId, "Sales"), ownerTeam, accessTeam, embeddedPrefix);
        var mapping = Mapping(("sales", new OwnerMappingEntry
        {
            Kind = OwnerKind.Team,
            NamePattern = "Sales-*",
            BusinessUnitName = "Sales"
        }));

        var exception = await Assert.That(async () => await _resolver.ResolveAsync(service, mapping, ["sales"]))
            .Throws<OwnerResolutionException>();

        await Assert.That(exception!.Failure).IsEqualTo(OwnerResolutionFailure.Ambiguous);
        await Assert.That(Queries(service).Count(query => query.EntityName == "team")).IsEqualTo(3);
    }

    [Test]
    public async Task ResolveAsync_StarCanMatchZeroCharacters()
    {
        var businessUnitId = Guid.NewGuid();
        var team = Team(Guid.NewGuid(), "Sales", businessUnitId, (int)OwnerTeamType.Owner);
        var service = CreateService(BusinessUnit(businessUnitId, "Sales"), team);
        var mapping = Mapping(("sales", new OwnerMappingEntry
        {
            Kind = OwnerKind.Team,
            NamePattern = "Sales*",
            BusinessUnitName = "Sales"
        }));

        var result = await _resolver.ResolveAsync(service, mapping, ["sales"]);

        await Assert.That(result["sales"].Principal.Id).IsEqualTo(team.Id);
    }

    [Test]
    [Arguments("membersAndGuests", OwnerMembershipType.MembersAndGuests)]
    [Arguments("members", OwnerMembershipType.Members)]
    [Arguments("owners", OwnerMembershipType.Owners)]
    [Arguments("guests", OwnerMembershipType.Guests)]
    public async Task ResolveAsync_EntraTeamMembershipQualifierUsesDataverseValues(string membership, OwnerMembershipType expectedMembership)
    {
        var groupId = Guid.NewGuid();
        var businessUnitId = Guid.NewGuid();
        var team = Team(Guid.NewGuid(), "Group team", businessUnitId, (int)OwnerTeamType.SecurityGroup, groupId, (int)expectedMembership);
        var service = CreateService(BusinessUnit(businessUnitId, "Sales"), team);
        var mapping = OwnerMappingLoader.Parse(
            $"{{\"schemaVersion\":1,\"owners\":{{\"teamOwner\":{{\"kind\":\"team\",\"entraObjectId\":\"{groupId:D}\",\"businessUnitName\":\"Sales\",\"membershipType\":\"{membership}\"}}}}}}");

        var result = await _resolver.ResolveAsync(service, mapping, ["teamOwner"]);

        await Assert.That(result["teamOwner"].TeamType).IsEqualTo(OwnerTeamType.SecurityGroup);
        await Assert.That(result["teamOwner"].MembershipType).IsEqualTo(expectedMembership);
    }

    [Test]
    public async Task ResolveAsync_GroupSelectorCombinesBusinessUnitAndMembershipQualifiers()
    {
        var groupId = Guid.NewGuid();
        var salesBu = Guid.NewGuid();
        var financeBu = Guid.NewGuid();
        var selected = Team(Guid.NewGuid(), "Sales group", salesBu, (int)OwnerTeamType.Microsoft365Group, groupId, 1);
        var otherBu = Team(Guid.NewGuid(), "Finance group", financeBu, (int)OwnerTeamType.Owner, groupId, 1);
        var otherMembership = Team(Guid.NewGuid(), "Sales guests", salesBu, (int)OwnerTeamType.Owner, groupId, 3);
        var service = CreateService(BusinessUnit(salesBu, "Sales"), BusinessUnit(financeBu, "Finance"), selected, otherBu, otherMembership);
        var mapping = Mapping(("group", new OwnerMappingEntry
        {
            Kind = OwnerKind.Team,
            EntraObjectId = groupId,
            BusinessUnitName = "Sales",
            MembershipType = OwnerMembershipType.Members
        }));

        var result = await _resolver.ResolveAsync(service, mapping, ["group"]);

        await Assert.That(result["group"].Principal.Id).IsEqualTo(selected.Id);
    }

    [Test]
    public async Task ResolveAsync_UnqualifiedSharedGroupIsAmbiguous()
    {
        var groupId = Guid.NewGuid();
        var service = CreateService(
            Team(Guid.NewGuid(), "Security", Guid.NewGuid(), (int)OwnerTeamType.SecurityGroup, groupId, 1),
            Team(Guid.NewGuid(), "Microsoft 365", Guid.NewGuid(), (int)OwnerTeamType.Microsoft365Group, groupId, 1));
        var mapping = Mapping(("group", new OwnerMappingEntry { Kind = OwnerKind.Team, EntraObjectId = groupId }));

        var exception = await Assert.That(async () => await _resolver.ResolveAsync(service, mapping, ["group"]))
            .Throws<OwnerResolutionException>();

        await Assert.That(exception!.Failure).IsEqualTo(OwnerResolutionFailure.Ambiguous);
    }

    [Test]
    public async Task ResolveAsync_GroupAmbiguityPrecedesMembershipClassification()
    {
        var groupId = Guid.NewGuid();
        var service = CreateService(
            Team(Guid.NewGuid(), "Unclassified membership", Guid.NewGuid(), (int)OwnerTeamType.SecurityGroup, groupId),
            Team(Guid.NewGuid(), "Classified", Guid.NewGuid(), (int)OwnerTeamType.Microsoft365Group, groupId, 1));
        var mapping = Mapping(("group", new OwnerMappingEntry { Kind = OwnerKind.Team, EntraObjectId = groupId }));

        var exception = await Assert.That(async () => await _resolver.ResolveAsync(service, mapping, ["group"]))
            .Throws<OwnerResolutionException>();

        await Assert.That(exception!.Failure).IsEqualTo(OwnerResolutionFailure.Ambiguous);
    }

    [Test]
    public async Task ResolveAsync_BusinessUnitNameMustBeUniqueBeforeTeamLookup()
    {
        var service = CreateService(BusinessUnit(Guid.NewGuid(), "Sales"), BusinessUnit(Guid.NewGuid(), "sales"));
        var mapping = Mapping(("team", new OwnerMappingEntry
        {
            Kind = OwnerKind.Team,
            NamePattern = "Sales*",
            BusinessUnitName = "Sales"
        }));

        var exception = await Assert.That(async () => await _resolver.ResolveAsync(service, mapping, ["team"]))
            .Throws<OwnerResolutionException>();

        await Assert.That(exception!.Failure).IsEqualTo(OwnerResolutionFailure.Ambiguous);
        await Assert.That(Queries(service).Any(query => query.EntityName == "team")).IsFalse();
    }

    [Test]
    public async Task ResolveAsync_MissingBusinessUnitIsNotFoundBeforeTeamLookup()
    {
        var service = CreateService();
        var mapping = Mapping(("team", new OwnerMappingEntry
        {
            Kind = OwnerKind.Team,
            NamePattern = "Sales*",
            BusinessUnitName = "Missing"
        }));

        var exception = await Assert.That(async () => await _resolver.ResolveAsync(service, mapping, ["team"]))
            .Throws<OwnerResolutionException>();

        await Assert.That(exception!.Failure).IsEqualTo(OwnerResolutionFailure.NotFound);
        await Assert.That(Queries(service).Any(query => query.EntityName == "team")).IsFalse();
    }

    [Test]
    public async Task ResolveAsync_AccessTeamIsRejectedOnlyAfterUniqueMatch()
    {
        var groupId = Guid.NewGuid();
        var service = CreateService(Team(Guid.NewGuid(), "Access team", Guid.NewGuid(), (int)OwnerTeamType.Access, groupId));
        var mapping = Mapping(("access", new OwnerMappingEntry { Kind = OwnerKind.Team, EntraObjectId = groupId }));

        var exception = await Assert.That(async () => await _resolver.ResolveAsync(service, mapping, ["access"]))
            .Throws<OwnerResolutionException>();

        await Assert.That(exception!.Failure).IsEqualTo(OwnerResolutionFailure.UnsupportedKind);
    }

    [Test]
    public async Task ResolveAsync_MissingTeamClassificationMetadataFailsExplicitly()
    {
        var businessUnitId = Guid.NewGuid();
        var team = new Entity("team", Guid.NewGuid())
        {
            ["name"] = "Sales team",
            ["businessunitid"] = new EntityReference("businessunit", businessUnitId)
        };
        var service = CreateService(BusinessUnit(businessUnitId, "Sales"), team);
        var mapping = Mapping(("team", new OwnerMappingEntry
        {
            Kind = OwnerKind.Team,
            NamePattern = "Sales*",
            BusinessUnitName = "Sales"
        }));

        var exception = await Assert.That(async () => await _resolver.ResolveAsync(service, mapping, ["team"]))
            .Throws<OwnerResolutionException>();

        await Assert.That(exception!.Failure).IsEqualTo(OwnerResolutionFailure.UnsupportedKind);
    }

    [Test]
    public async Task ResolveAsync_UnknownTeamTypeFailsExplicitly()
    {
        var businessUnitId = Guid.NewGuid();
        var team = Team(Guid.NewGuid(), "Unknown", businessUnitId, 99);
        var service = CreateService(BusinessUnit(businessUnitId, "Sales"), team);
        var mapping = Mapping(("team", new OwnerMappingEntry
        {
            Kind = OwnerKind.Team,
            NamePattern = "Unknown",
            BusinessUnitName = "Sales"
        }));

        var exception = await Assert.That(async () => await _resolver.ResolveAsync(service, mapping, ["team"]))
            .Throws<OwnerResolutionException>();

        await Assert.That(exception!.Failure).IsEqualTo(OwnerResolutionFailure.UnsupportedKind);
    }

    [Test]
    public async Task ResolveAsync_EntraTeamRequiresMembershipClassificationMetadata()
    {
        var groupId = Guid.NewGuid();
        var team = Team(Guid.NewGuid(), "Group", Guid.NewGuid(), (int)OwnerTeamType.SecurityGroup, groupId);
        var service = CreateService(team);
        var mapping = Mapping(("group", new OwnerMappingEntry { Kind = OwnerKind.Team, EntraObjectId = groupId }));

        var exception = await Assert.That(async () => await _resolver.ResolveAsync(service, mapping, ["group"]))
            .Throws<OwnerResolutionException>();

        await Assert.That(exception!.Failure).IsEqualTo(OwnerResolutionFailure.UnsupportedKind);
    }

    [Test]
    public async Task ResolveAsync_ValidatesUnusedEntriesAndRequestedKeysBeforeAnyTargetAccess()
    {
        var service = CreateService();
        var invalid = Mapping(
            ("requested", new OwnerMappingEntry { Kind = OwnerKind.User, DomainName = "valid@example.org" }),
            ("unused", new OwnerMappingEntry { Kind = OwnerKind.User, DomainName = " bad@example.org " }));

        var invalidException = await Assert.That(async () => await _resolver.ResolveAsync(service, invalid, ["requested"]))
            .Throws<OwnerResolutionException>();
        await Assert.That(invalidException!.Failure).IsEqualTo(OwnerResolutionFailure.InvalidMapping);
        await Assert.That(Queries(service)).IsEmpty();

        var missingKeyException = await Assert.That(async () => await _resolver.ResolveAsync(
                service,
                Mapping(("known", new OwnerMappingEntry { Kind = OwnerKind.User, DomainName = "known@example.org" })),
                ["missing"]))
            .Throws<OwnerResolutionException>();
        await Assert.That(missingKeyException!.Failure).IsEqualTo(OwnerResolutionFailure.MissingKey);
        await Assert.That(Queries(service)).IsEmpty();
    }

    [Test]
    public async Task ResolveAsync_LogicalKeysAreCaseSensitive()
    {
        var service = CreateService();
        var mapping = Mapping(("salesOwner", new OwnerMappingEntry { Kind = OwnerKind.User, DomainName = "sales@example.org" }));

        var exception = await Assert.That(async () => await _resolver.ResolveAsync(service, mapping, ["SalesOwner"]))
            .Throws<OwnerResolutionException>();

        await Assert.That(exception!.Failure).IsEqualTo(OwnerResolutionFailure.MissingKey);
        await Assert.That(Queries(service)).IsEmpty();
    }

    [Test]
    public async Task ResolveAsync_EmptyRequestedKeysDoesNotRequireTargetMappingEntries()
    {
        var service = CreateService();
        var result = await _resolver.ResolveAsync(service, Mapping(), []);

        await Assert.That(result).IsEmpty();
        await Assert.That(Queries(service)).IsEmpty();
    }

    [Test]
    public async Task ResolveAsync_RepeatedCallsDoNotReuseGlobalCache()
    {
        var service = CreateService(User(Guid.NewGuid(), "owner@example.org", disabled: false));
        var mapping = Mapping(("owner", new OwnerMappingEntry { Kind = OwnerKind.User, DomainName = "owner@example.org" }));

        await _resolver.ResolveAsync(service, mapping, ["owner"]);
        await _resolver.ResolveAsync(service, mapping, ["owner"]);

        await Assert.That(Queries(service).Count(query => query.EntityName == "systemuser")).IsEqualTo(2);
    }

    [Test]
    public async Task ResolveAsync_AllowsApplyAliasesButCaptureRejectsCapturedAliases()
    {
        var sharedGroup = Guid.NewGuid();
        var uniqueGroup = Guid.NewGuid();
        var sharedPrincipal = Team(Guid.NewGuid(), "Shared", Guid.NewGuid(), (int)OwnerTeamType.SecurityGroup, sharedGroup, 1);
        var uniquePrincipal = Team(Guid.NewGuid(), "Unique", Guid.NewGuid(), (int)OwnerTeamType.Microsoft365Group, uniqueGroup, 1);
        var service = CreateService(sharedPrincipal, uniquePrincipal);
        var mapping = Mapping(
            ("sharedA", new OwnerMappingEntry { Kind = OwnerKind.Team, EntraObjectId = sharedGroup }),
            ("sharedB", new OwnerMappingEntry { Kind = OwnerKind.Team, EntraObjectId = sharedGroup }),
            ("unique", new OwnerMappingEntry { Kind = OwnerKind.Team, EntraObjectId = uniqueGroup }));

        var applied = await _resolver.ResolveAsync(service, mapping, ["sharedA", "sharedB"]);
        await Assert.That(applied["sharedA"].Principal.Id).IsEqualTo(applied["sharedB"].Principal.Id);

        var captured = await _resolver.ResolveForCaptureAsync(
            service,
            mapping,
            [new EntityReference("team", uniquePrincipal.Id)]);
        await Assert.That(captured.Captures[0].LogicalKey).IsEqualTo("unique");
        await Assert.That(captured.Captures[0].Principal.LogicalName).IsEqualTo("team");
        await Assert.That(captured.Captures[0].Principal.Id).IsEqualTo(uniquePrincipal.Id);

        var exception = await Assert.That(async () => await _resolver.ResolveForCaptureAsync(
                service,
                mapping,
                [new EntityReference("team", sharedPrincipal.Id)]))
            .Throws<OwnerResolutionException>();
        await Assert.That(exception!.Failure).IsEqualTo(OwnerResolutionFailure.AmbiguousCaptureMapping);
    }

    [Test]
    public async Task ResolveForCaptureAsync_FailsIfAnyUnusedSourceEntryCannotResolve()
    {
        var presentGroup = Guid.NewGuid();
        var missingGroup = Guid.NewGuid();
        var present = Team(Guid.NewGuid(), "Present", Guid.NewGuid(), (int)OwnerTeamType.SecurityGroup, presentGroup, 1);
        var service = CreateService(present);
        var mapping = Mapping(
            ("captured", new OwnerMappingEntry { Kind = OwnerKind.Team, EntraObjectId = presentGroup }),
            ("unused", new OwnerMappingEntry { Kind = OwnerKind.Team, EntraObjectId = missingGroup }));

        var exception = await Assert.That(async () => await _resolver.ResolveForCaptureAsync(
                service,
                mapping,
                [new EntityReference("team", present.Id)]))
            .Throws<OwnerResolutionException>();

        await Assert.That(exception!.Failure).IsEqualTo(OwnerResolutionFailure.NotFound);
    }

    [Test]
    public async Task ResolveForCaptureAsync_ReportsMissingCapturedOwnerKey()
    {
        var groupId = Guid.NewGuid();
        var service = CreateService(Team(Guid.NewGuid(), "Mapped", Guid.NewGuid(), (int)OwnerTeamType.SecurityGroup, groupId, 1));
        var mapping = Mapping(("mapped", new OwnerMappingEntry { Kind = OwnerKind.Team, EntraObjectId = groupId }));

        var exception = await Assert.That(async () => await _resolver.ResolveForCaptureAsync(
                service,
                mapping,
                [new EntityReference("team", Guid.NewGuid())]))
            .Throws<OwnerResolutionException>();

        await Assert.That(exception!.Failure).IsEqualTo(OwnerResolutionFailure.MissingCaptureMapping);
    }

    [Test]
    [Arguments(UnmappedOwnerHandling.Fail)]
    [Arguments(UnmappedOwnerHandling.Skip)]
    public async Task ResolveForCaptureAsync_SkipsPlatformPrincipalsRegardlessOfHandling(UnmappedOwnerHandling handling)
    {
        var groupId = Guid.NewGuid();
        var mapped = Team(Guid.NewGuid(), "Mapped", Guid.NewGuid(), (int)OwnerTeamType.SecurityGroup, groupId, 1);
        var system = PlatformUser(Guid.NewGuid(), "SYSTEM");
        var integration = PlatformUser(Guid.NewGuid(), "INTEGRATION");
        var service = CreateService(mapped, system, integration);
        var mapping = Mapping(("mapped", new OwnerMappingEntry { Kind = OwnerKind.Team, EntraObjectId = groupId }));

        var result = await _resolver.ResolveForCaptureAsync(
            service,
            mapping,
            [
                new EntityReference("team", mapped.Id),
                new EntityReference("systemuser", system.Id),
                new EntityReference("systemuser", integration.Id)
            ],
            handling);

        await Assert.That(result.Captures.Count).IsEqualTo(1);
        await Assert.That(result.Captures[0].LogicalKey).IsEqualTo("mapped");
        await Assert.That(result.Skipped.Count).IsEqualTo(2);
        await Assert.That(result.Skipped.All(skipped => skipped.Reason == OwnerCaptureSkipReason.PlatformPrincipal)).IsTrue();
        await Assert.That(result.FindSkipped(new EntityReference("systemuser", system.Id))!.DisplayName).IsEqualTo("SYSTEM");
        await Assert.That(result.FindSkipped(new EntityReference("systemuser", integration.Id))!.DisplayName).IsEqualTo("INTEGRATION");
    }

    [Test]
    public async Task ResolveForCaptureAsync_UserNamedSystemWithDirectoryIdentityIsNotPlatformPrincipal()
    {
        var impostor = PlatformUser(Guid.NewGuid(), "SYSTEM");
        impostor["domainname"] = "system@example.org";
        var service = CreateService(impostor);

        var exception = await Assert.That(async () => await _resolver.ResolveForCaptureAsync(
                service,
                Mapping(),
                [new EntityReference("systemuser", impostor.Id)]))
            .Throws<OwnerResolutionException>();

        await Assert.That(exception!.Failure).IsEqualTo(OwnerResolutionFailure.MissingCaptureMapping);
    }

    [Test]
    public async Task ResolveForCaptureAsync_SkipHandlingReportsUnmappedPrincipals()
    {
        var unmappedUser = User(Guid.NewGuid(), "someone@example.org", disabled: false);
        var unmappedTeam = new EntityReference("team", Guid.NewGuid());
        var service = CreateService(unmappedUser);

        var result = await _resolver.ResolveForCaptureAsync(
            service,
            Mapping(),
            [new EntityReference("systemuser", unmappedUser.Id), unmappedTeam],
            UnmappedOwnerHandling.Skip);

        await Assert.That(result.Captures).IsEmpty();
        await Assert.That(result.Skipped.Count).IsEqualTo(2);
        await Assert.That(result.Skipped.All(skipped => skipped.Reason == OwnerCaptureSkipReason.Unmapped)).IsTrue();
        await Assert.That(result.FindSkipped(unmappedTeam)).IsNotNull();
    }

    [Test]
    public async Task ResolveForCaptureAsync_SkipHandlingStillRejectsAmbiguousCapture()
    {
        var groupId = Guid.NewGuid();
        var team = Team(Guid.NewGuid(), "Shared", Guid.NewGuid(), (int)OwnerTeamType.SecurityGroup, groupId, 1);
        var service = CreateService(team);
        var mapping = Mapping(
            ("a", new OwnerMappingEntry { Kind = OwnerKind.Team, EntraObjectId = groupId }),
            ("b", new OwnerMappingEntry { Kind = OwnerKind.Team, EntraObjectId = groupId }));

        var exception = await Assert.That(async () => await _resolver.ResolveForCaptureAsync(
                service,
                mapping,
                [new EntityReference("team", team.Id)],
                UnmappedOwnerHandling.Skip))
            .Throws<OwnerResolutionException>();

        await Assert.That(exception!.Failure).IsEqualTo(OwnerResolutionFailure.AmbiguousCaptureMapping);
    }

    [Test]
    public async Task ResolveForCaptureAsync_FullyMappedCaptureDoesNotQueryPlatformPrincipals()
    {
        var groupId = Guid.NewGuid();
        var team = Team(Guid.NewGuid(), "Mapped", Guid.NewGuid(), (int)OwnerTeamType.SecurityGroup, groupId, 1);
        var service = CreateService(team);
        var mapping = Mapping(("mapped", new OwnerMappingEntry { Kind = OwnerKind.Team, EntraObjectId = groupId }));

        await _resolver.ResolveForCaptureAsync(service, mapping, [new EntityReference("team", team.Id)]);

        await Assert.That(Queries(service).Any(query => query.EntityName == "systemuser")).IsFalse();
    }

    [Test]
    public async Task ResolveAsync_CancelledBeforeLookupDoesNotQuery()
    {
        using var cancellation = new CancellationTokenSource();
        await cancellation.CancelAsync();
        var cancellationToken = cancellation.Token;
        var service = CreateService();
        var mapping = Mapping(("user", new OwnerMappingEntry { Kind = OwnerKind.User, DomainName = "user@example.org" }));

        await Assert.That(async () => await _resolver.ResolveAsync(service, mapping, ["user"], cancellationToken))
            .Throws<OperationCanceledException>();
        await Assert.That(Queries(service)).IsEmpty();
    }

    [Test]
    public async Task ResolveAsync_PropagatesServiceFailures()
    {
        var service = CreateService(failure: new InvalidOperationException("service failure"));
        var mapping = Mapping(("user", new OwnerMappingEntry { Kind = OwnerKind.User, DomainName = "user@example.org" }));

        var exception = await Assert.That(async () => await _resolver.ResolveAsync(service, mapping, ["user"]))
            .Throws<InvalidOperationException>();

        await Assert.That(exception!.Message).IsEqualTo("service failure");
    }

    private static FakeOrganizationServiceAsync CreateService(params Entity[] entities) => CreateServiceCore(null, entities);

    private static FakeOrganizationServiceAsync CreateService(Exception? failure, params Entity[] entities) =>
        CreateServiceCore(failure, entities);

    private static FakeOrganizationServiceAsync CreateServiceCore(Exception? failure, params Entity[] entities)
    {
        var service = new FakeOrganizationServiceAsync();
        var executor = new EntityPageExecutor(entities, failure);
        service.AddRequests(executor);
        s_executors.Add(service, executor);
        return service;
    }

    private static List<QueryExpression> Queries(FakeOrganizationServiceAsync service) =>
        s_executors.GetValue(service, _ => throw new InvalidOperationException()).RecordedQueries;

    private static OwnerMapping Mapping(params (string Key, OwnerMappingEntry Entry)[] entries) =>
        new(1, entries.ToDictionary(entry => entry.Key, entry => entry.Entry, StringComparer.Ordinal));

    private static Entity User(Guid id, string domainName, bool disabled, Guid? applicationId = null)
    {
        var user = new Entity("systemuser", id)
        {
            ["domainname"] = domainName,
            ["isdisabled"] = disabled
        };
        if (applicationId.HasValue)
        {
            user["applicationid"] = applicationId.Value;
        }

        return user;
    }

    private static Entity BusinessUnit(Guid id, string name) => new("businessunit", id) { ["name"] = name };

    private static Entity PlatformUser(Guid id, string fullName) =>
        new("systemuser", id) { ["fullname"] = fullName, ["isdisabled"] = true };

    private static Entity Team(
        Guid id,
        string name,
        Guid businessUnitId,
        int teamType,
        Guid? groupId = null,
        int? membershipType = null)
    {
        var team = new Entity("team", id)
        {
            ["name"] = name,
            ["businessunitid"] = new EntityReference("businessunit", businessUnitId),
            ["teamtype"] = new OptionSetValue(teamType)
        };
        if (groupId.HasValue)
        {
            team["azureactivedirectoryobjectid"] = groupId.Value;
        }

        if (membershipType.HasValue)
        {
            team["membershiptype"] = new OptionSetValue(membershipType.Value);
        }

        return team;
    }

    private sealed class EntityPageExecutor(IReadOnlyCollection<Entity> entities, Exception? failure) : IOrganizationRequestFake
    {
        public Type ForType => typeof(RetrieveMultipleRequest);

        public List<QueryExpression> RecordedQueries { get; } = [];

        public OrganizationResponse Execute(OrganizationRequest organizationRequest, FakeOrganizationService fakeOrganizationService)
        {
            if (failure is not null)
            {
                throw failure;
            }

            var query = (QueryExpression)((RetrieveMultipleRequest)organizationRequest).Query;
            RecordedQueries.Add(query);
            var candidates = entities.Where(entity => entity.LogicalName == query.EntityName).ToArray();
            var pageNumber = query.PageInfo.PageNumber;
            var page = new EntityCollection(candidates.Skip(pageNumber - 1).Take(1).ToList())
            {
                MoreRecords = pageNumber < candidates.Length,
                PagingCookie = pageNumber < candidates.Length ? $"page-{pageNumber + 1}" : null
            };
            return new RetrieveMultipleResponse { [nameof(RetrieveMultipleResponse.EntityCollection)] = page };
        }
    }
}
