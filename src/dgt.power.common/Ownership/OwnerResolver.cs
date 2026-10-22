// Copyright (c) DIGITALL Nature. All rights reserved

using dgt.power.common.Extensions;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.common.Ownership;

public sealed class OwnerResolver : IOwnerResolver
{
    private const string SystemUser = "systemuser";
    private const string Team = "team";
    private const string BusinessUnit = "businessunit";

    public Task<IReadOnlyDictionary<string, ResolvedOwner>> ResolveAsync(
        IOrganizationServiceAsync2 service,
        OwnerMapping mapping,
        IEnumerable<string> requestedKeys,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(service);
        ArgumentNullException.ThrowIfNull(mapping);
        ArgumentNullException.ThrowIfNull(requestedKeys);
        OwnerMappingValidator.Validate(mapping);

        var keys = requestedKeys.Distinct(StringComparer.Ordinal).ToArray();
        foreach (var key in keys)
        {
            if (string.IsNullOrWhiteSpace(key) || !mapping.Owners.ContainsKey(key))
            {
                throw new OwnerResolutionException(
                    OwnerResolutionFailure.MissingKey,
                    $"No owner entry is defined for logical key '{key}'.",
                    logicalKey: key);
            }
        }

        return ResolveRequestedAsync(service, mapping, keys, cancellationToken);
    }

    private static async Task<IReadOnlyDictionary<string, ResolvedOwner>> ResolveRequestedAsync(
        IOrganizationServiceAsync2 service,
        OwnerMapping mapping,
        IReadOnlyCollection<string> keys,
        CancellationToken cancellationToken)
    {
        var result = new Dictionary<string, ResolvedOwner>(StringComparer.Ordinal);
        foreach (var key in keys)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var entry = mapping.Owners[key];
            result.Add(key, await ResolveEntryAsync(service, key, entry, cancellationToken));
        }

        return result;
    }

    public Task<IReadOnlyList<OwnerCapture>> ResolveForCaptureAsync(
        IOrganizationServiceAsync2 service,
        OwnerMapping mapping,
        IEnumerable<EntityReference> capturedOwners,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(service);
        ArgumentNullException.ThrowIfNull(mapping);
        ArgumentNullException.ThrowIfNull(capturedOwners);
        OwnerMappingValidator.Validate(mapping);
        var ownerReferences = capturedOwners.ToArray();
        foreach (var owner in ownerReferences)
        {
            ArgumentNullException.ThrowIfNull(owner);
        }

        return CaptureCoreAsync(service, mapping, ownerReferences, cancellationToken);
    }

    private static async Task<IReadOnlyList<OwnerCapture>> CaptureCoreAsync(
        IOrganizationServiceAsync2 service,
        OwnerMapping mapping,
        IReadOnlyCollection<EntityReference> capturedOwners,
        CancellationToken cancellationToken)
    {
        var allKeys = mapping.Owners.Keys.ToArray();
        var allOwners = await ResolveRequestedAsync(service, mapping, allKeys, cancellationToken);
        var captures = new List<OwnerCapture>();
        foreach (var capturedOwner in capturedOwners)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var matches = allOwners.Values
                .Where(owner => owner.Principal.Id == capturedOwner.Id &&
                                string.Equals(owner.Principal.LogicalName, capturedOwner.LogicalName, StringComparison.Ordinal))
                .ToArray();

            if (matches.Length == 0)
            {
                throw new OwnerResolutionException(
                    OwnerResolutionFailure.MissingCaptureMapping,
                    $"No source owner key resolves captured principal '{capturedOwner.LogicalName}({capturedOwner.Id:D})'.",
                    capturedOwner: capturedOwner);
            }

            if (matches.Length > 1)
            {
                throw new OwnerResolutionException(
                    OwnerResolutionFailure.AmbiguousCaptureMapping,
                    $"Multiple source owner keys resolve captured principal '{capturedOwner.LogicalName}({capturedOwner.Id:D})': {string.Join(", ", matches.Select(match => match.LogicalKey))}.",
                    capturedOwner: capturedOwner);
            }

            captures.Add(new OwnerCapture(capturedOwner, matches[0].LogicalKey));
        }

        return captures;
    }

    private static async Task<ResolvedOwner> ResolveEntryAsync(
        IOrganizationServiceAsync2 service,
        string key,
        OwnerMappingEntry entry,
        CancellationToken cancellationToken) =>
        entry.Kind switch
        {
            OwnerKind.User or OwnerKind.ApplicationUser =>
                await ResolveUserAsync(service, key, entry, cancellationToken),
            OwnerKind.Team => await ResolveTeamAsync(service, key, entry, cancellationToken),
            _ => throw Failure(OwnerResolutionFailure.UnsupportedKind, key, entry.Kind.ToString(), "The requested owner kind is unsupported.")
        };

    private static async Task<ResolvedOwner> ResolveUserAsync(
        IOrganizationServiceAsync2 service,
        string key,
        OwnerMappingEntry entry,
        CancellationToken cancellationToken)
    {
        var attribute = entry.Kind switch
        {
            OwnerKind.User when entry.EntraObjectId.HasValue => "azureactivedirectoryobjectid",
            OwnerKind.User => "domainname",
            OwnerKind.ApplicationUser => "applicationid",
            _ => throw Failure(OwnerResolutionFailure.UnsupportedKind, key, entry.Kind.ToString(), "The requested owner kind is unsupported.")
        };
        var value = entry.Kind switch
        {
            OwnerKind.User when entry.EntraObjectId.HasValue => (object)entry.EntraObjectId.Value,
            OwnerKind.User => entry.DomainName!,
            OwnerKind.ApplicationUser => entry.ApplicationId!.Value,
            _ => throw new InvalidOperationException()
        };
        var query = new QueryExpression(SystemUser)
        {
            ColumnSet = new ColumnSet("applicationid", "isdisabled", "azureactivedirectoryobjectid", "domainname")
        };
        if (entry.Kind == OwnerKind.ApplicationUser || entry.EntraObjectId.HasValue)
        {
            query.Criteria.AddCondition(attribute, ConditionOperator.Equal, value);
        }

        var candidates = (await service.RetrieveAllAsync(query, cancellationToken: cancellationToken))
            .Where(entity => entry.Kind switch
            {
                OwnerKind.User when entry.EntraObjectId.HasValue =>
                    entity.GetAttributeValue<Guid?>("azureactivedirectoryobjectid") == entry.EntraObjectId,
                OwnerKind.User => string.Equals(
                    entity.GetAttributeValue<string>("domainname"),
                    entry.DomainName,
                    StringComparison.OrdinalIgnoreCase),
                OwnerKind.ApplicationUser => entity.GetAttributeValue<Guid?>("applicationid") == entry.ApplicationId,
                _ => false
            })
            .ToArray();

        var candidate = RequireUnique(candidates, key, $"{attribute}={value}");
        if (!candidate.Attributes.TryGetValue("isdisabled", out var disabledValue) || disabledValue is not bool isDisabled)
        {
            throw Failure(OwnerResolutionFailure.UnsupportedKind, key, attribute, "The target user is missing disabled-status classification metadata.");
        }

        var applicationId = candidate.GetAttributeValue<Guid?>("applicationid");
        var isApplicationUser = applicationId is { } id && id != Guid.Empty;
        if ((entry.Kind == OwnerKind.ApplicationUser) != isApplicationUser)
        {
            throw Failure(
                OwnerResolutionFailure.UnsupportedKind,
                key,
                attribute,
                entry.Kind == OwnerKind.ApplicationUser
                    ? "The target system user is not classified as an application user."
                    : "The target system user is an application user, not a regular user.");
        }

        return new ResolvedOwner(
            key,
            new EntityReference(SystemUser, candidate.Id),
            entry.Kind,
            IsDisabled: isDisabled);
    }

    private static async Task<ResolvedOwner> ResolveTeamAsync(
        IOrganizationServiceAsync2 service,
        string key,
        OwnerMappingEntry entry,
        CancellationToken cancellationToken)
    {
        Guid? businessUnitId = null;
        if (entry.BusinessUnitName is not null)
        {
            var businessUnit = await ResolveBusinessUnitAsync(service, key, entry.BusinessUnitName, cancellationToken);
            businessUnitId = businessUnit.Id;
        }

        var query = new QueryExpression(Team)
        {
            ColumnSet = new ColumnSet("azureactivedirectoryobjectid", "businessunitid", "membershiptype", "name", "teamtype")
        };

        if (businessUnitId.HasValue)
        {
            query.Criteria.AddCondition("businessunitid", ConditionOperator.Equal, businessUnitId.Value);
        }

        if (entry.EntraObjectId.HasValue)
        {
            query.Criteria.AddCondition("azureactivedirectoryobjectid", ConditionOperator.Equal, entry.EntraObjectId.Value);
        }

        var candidates = (await service.RetrieveAllAsync(query, cancellationToken: cancellationToken))
            .Where(entity => MatchesTeamSelector(entity, entry, businessUnitId))
            .ToArray();

        if (entry.MembershipType is not null)
        {
            foreach (var team in candidates)
            {
                if (!team.Attributes.TryGetValue("membershiptype", out var candidateMembershipValue) ||
                    candidateMembershipValue is not OptionSetValue candidateMembership ||
                    !Enum.IsDefined(typeof(OwnerMembershipType), candidateMembership.Value))
                {
                    throw Failure(OwnerResolutionFailure.UnsupportedKind, key, "membershipType", "A candidate team is missing membership classification metadata.");
                }
            }
        }

        if (entry.MembershipType is { } requestedMembership)
        {
            candidates = candidates
                .Where(team => team.GetAttributeValue<OptionSetValue>("membershiptype").Value == (int)requestedMembership)
                .ToArray();
        }

        var selector = entry.EntraObjectId?.ToString("D") ?? entry.NamePattern!;
        var candidate = RequireUnique(candidates, key, selector);
        var rawTeamType = candidate.GetAttributeValue<OptionSetValue>("teamtype");
        if (rawTeamType is null || !Enum.IsDefined(typeof(OwnerTeamType), rawTeamType.Value))
        {
            throw Failure(OwnerResolutionFailure.UnsupportedKind, key, selector, "The target team is missing or has an unsupported team-type classification.");
        }

        var teamType = (OwnerTeamType)rawTeamType.Value;
        if (teamType == OwnerTeamType.Access)
        {
            throw Failure(OwnerResolutionFailure.UnsupportedKind, key, selector, "Access teams cannot own records.");
        }

        if (entry.EntraObjectId.HasValue &&
            teamType is not (OwnerTeamType.SecurityGroup or OwnerTeamType.Microsoft365Group))
        {
            throw Failure(OwnerResolutionFailure.UnsupportedKind, key, selector, "An Entra team selector must resolve to a group-backed team.");
        }

        var membershipValue = candidate.GetAttributeValue<OptionSetValue>("membershiptype");
        if (entry.EntraObjectId.HasValue && membershipValue is null)
        {
            throw Failure(OwnerResolutionFailure.UnsupportedKind, key, "membershipType", "The target group team is missing membership classification metadata.");
        }

        if (membershipValue is not null && !Enum.IsDefined(typeof(OwnerMembershipType), membershipValue.Value))
        {
            throw Failure(OwnerResolutionFailure.UnsupportedKind, key, "membershipType", "The target team has unsupported membership classification metadata.");
        }

        var resolvedMembership = membershipValue?.Value;
        return new ResolvedOwner(
            key,
            new EntityReference(Team, candidate.Id),
            OwnerKind.Team,
            TeamType: teamType,
            MembershipType: resolvedMembership.HasValue ? (OwnerMembershipType)resolvedMembership.Value : null);
    }

    private static async Task<Entity> ResolveBusinessUnitAsync(
        IOrganizationServiceAsync2 service,
        string key,
        string name,
        CancellationToken cancellationToken)
    {
        var query = new QueryExpression(BusinessUnit) { ColumnSet = new ColumnSet("name") };
        var matches = (await service.RetrieveAllAsync(query, cancellationToken: cancellationToken))
            .Where(entity => string.Equals(entity.GetAttributeValue<string>("name"), name, StringComparison.OrdinalIgnoreCase))
            .ToArray();
        return RequireUnique(matches, key, $"businessUnitName={name}");
    }

    private static bool MatchesTeamSelector(Entity team, OwnerMappingEntry entry, Guid? businessUnitId)
    {
        if (businessUnitId.HasValue &&
            team.GetAttributeValue<EntityReference>("businessunitid")?.Id != businessUnitId.Value)
        {
            return false;
        }

        if (entry.EntraObjectId.HasValue)
        {
            return team.GetAttributeValue<Guid?>("azureactivedirectoryobjectid") == entry.EntraObjectId;
        }

        var name = team.GetAttributeValue<string>("name");
        return name is not null && MatchesNamePattern(entry.NamePattern!, name);
    }

    private static bool MatchesNamePattern(string pattern, string value)
    {
        var memo = new Dictionary<(int Pattern, int Value), bool>();
        return Match(0, 0);

        bool Match(int patternIndex, int valueIndex)
        {
            if (memo.TryGetValue((patternIndex, valueIndex), out var cached))
            {
                return cached;
            }

            if (patternIndex == pattern.Length)
            {
                return memo[(patternIndex, valueIndex)] = valueIndex == value.Length;
            }

            if (pattern[patternIndex] == '*')
            {
                var nextPatternIndex = patternIndex + 1;
                for (var nextValueIndex = valueIndex; nextValueIndex <= value.Length; nextValueIndex++)
                {
                    if (Match(nextPatternIndex, nextValueIndex))
                    {
                        return memo[(patternIndex, valueIndex)] = true;
                    }
                }

                return memo[(patternIndex, valueIndex)] = false;
            }

            var literalEnd = patternIndex;
            while (literalEnd < pattern.Length && pattern[literalEnd] != '*')
            {
                literalEnd++;
            }

            var literalLength = literalEnd - patternIndex;
            var matches = valueIndex + literalLength <= value.Length &&
                          string.Compare(pattern, patternIndex, value, valueIndex, literalLength, StringComparison.OrdinalIgnoreCase) == 0 &&
                          Match(literalEnd, valueIndex + literalLength);
            return memo[(patternIndex, valueIndex)] = matches;
        }
    }

    private static Entity RequireUnique(Entity[] candidates, string key, string selector)
    {
        if (candidates.Length == 0)
        {
            throw Failure(OwnerResolutionFailure.NotFound, key, selector, "No target principal matched this owner reference.");
        }

        if (candidates.Length > 1)
        {
            throw Failure(OwnerResolutionFailure.Ambiguous, key, selector, $"This owner reference matched {candidates.Length} target principals.");
        }

        return candidates[0];
    }

    private static OwnerResolutionException Failure(
        OwnerResolutionFailure category,
        string key,
        string selector,
        string detail) =>
        new(category, $"Owner '{key}' ({selector}): {detail}", logicalKey: key, selector: selector);
}
