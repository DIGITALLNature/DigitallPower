// Copyright (c) DIGITALL Nature. All rights reserved

namespace dgt.power.common.Ownership;

internal static class OwnerMappingValidator
{
    public static void Validate(OwnerMapping mapping)
    {
        ArgumentNullException.ThrowIfNull(mapping);

        if (mapping.SchemaVersion != 1)
        {
            throw Invalid("schemaVersion", $"Unsupported schema version '{mapping.SchemaVersion}'.");
        }

        if (mapping.Schema is not null)
        {
            ValidateText(mapping.Schema, "$schema");
            if (!Uri.TryCreate(mapping.Schema, UriKind.Absolute, out _))
            {
                throw Invalid("$schema", "The schema reference must be an absolute URI.");
            }
        }

        foreach (var (key, entry) in mapping.Owners)
        {
            ValidateText(key, $"owners.{key}");
            if (entry is null)
            {
                throw Invalid($"owners.{key}", "Owner entries cannot be null.");
            }

            ValidateEntry(key, entry);
        }
    }

    private static void ValidateEntry(string key, OwnerMappingEntry entry)
    {
        switch (entry.Kind)
        {
            case OwnerKind.User:
                ValidateText(entry.DomainName, $"owners.{key}.domainName", allowNull: true);
                ValidateText(entry.BusinessUnitName, $"owners.{key}.businessUnitName", allowNull: true);
                ValidateGuid(entry.EntraObjectId, $"owners.{key}.entraObjectId");
                EnsureSelectorFields(key, entry, entry.EntraObjectId.HasValue ^ (entry.DomainName is not null),
                    entry.ApplicationId is null && entry.NamePattern is null && entry.BusinessUnitName is null && entry.MembershipType is null,
                    "user requires exactly one of entraObjectId or domainName and no qualifiers");
                break;
            case OwnerKind.ApplicationUser:
                ValidateGuid(entry.ApplicationId, $"owners.{key}.applicationId", required: true);
                ValidateOptionalGuid(entry.EntraObjectId, $"owners.{key}.entraObjectId");
                ValidateText(entry.DomainName, $"owners.{key}.domainName", allowNull: true);
                ValidateText(entry.NamePattern, $"owners.{key}.namePattern", allowNull: true);
                ValidateText(entry.BusinessUnitName, $"owners.{key}.businessUnitName", allowNull: true);
                var hasAdditionalApplicationSelectors = entry.EntraObjectId is not null || entry.DomainName is not null ||
                                                        entry.NamePattern is not null;
                var hasAdditionalApplicationQualifiers = entry.BusinessUnitName is not null || entry.MembershipType is not null;
                hasAdditionalApplicationSelectors |= hasAdditionalApplicationQualifiers;
                EnsureSelectorFields(key, entry, entry.ApplicationId.HasValue && !hasAdditionalApplicationSelectors, true,
                    "applicationUser requires only applicationId");
                break;
            case OwnerKind.Team:
                ValidateText(entry.BusinessUnitName, $"owners.{key}.businessUnitName", allowNull: true);
                ValidateText(entry.NamePattern, $"owners.{key}.namePattern", allowNull: true);
                ValidateOptionalGuid(entry.EntraObjectId, $"owners.{key}.entraObjectId");
                var hasNoUserSelectors = entry is { ApplicationId: null, DomainName: null };
                var groupSelector = entry is { EntraObjectId: not null, NamePattern: null } && hasNoUserSelectors;
                var hasNameAndUnit = entry is { NamePattern: not null, BusinessUnitName: not null };
                var noGroupOrMembership = entry is { EntraObjectId: null, MembershipType: null };
                var nameSelector = hasNameAndUnit && hasNoUserSelectors && noGroupOrMembership;
                EnsureSelectorFields(key, entry, groupSelector || nameSelector,
                    entry.MembershipType is null || groupSelector,
                    "team requires entraObjectId with optional businessUnitName and membershipType, or namePattern with businessUnitName");
                break;
            default:
                throw Invalid($"owners.{key}.kind", $"Unsupported owner kind '{entry.Kind}'.");
        }
    }

    private static void EnsureSelectorFields(string key, OwnerMappingEntry entry, bool selectorsValid, bool qualifiersValid, string message)
    {
        if (!selectorsValid || !qualifiersValid || !Enum.IsDefined(entry.Kind))
        {
            throw Invalid($"owners.{key}", message);
        }

        if (entry.MembershipType is { } membership && !Enum.IsDefined(membership))
        {
            throw Invalid($"owners.{key}", message);
        }
    }

    private static void ValidateGuid(Guid? value, string selector, bool required = false)
    {
        if ((required && value is null) || value == Guid.Empty)
        {
            throw Invalid(selector, required ? "Specify a nonempty GUID." : "GUID selectors must not be empty.");
        }
    }

    private static void ValidateOptionalGuid(Guid? value, string selector) => ValidateGuid(value, selector);

    private static void ValidateText(string? value, string selector, bool allowNull = false)
    {
        if (value is null && allowNull)
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(value) || !string.Equals(value, value.Trim(), StringComparison.Ordinal))
        {
            throw Invalid(selector, "Values must be nonblank and have no leading or trailing whitespace.");
        }
    }

    private static OwnerResolutionException Invalid(string selector, string detail) =>
        new(OwnerResolutionFailure.InvalidMapping, $"Invalid owner mapping at '{selector}': {detail}", selector: selector);
}
