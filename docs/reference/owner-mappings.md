# Portable owner mappings

Ownership-aware consumers use a V1 environment-specific mapping to translate stable logical keys
into existing Dataverse users, application users, or owning teams. Keep resource intent portable:
refer to keys such as `salesOwner`, and keep tenant-specific Entra IDs and names in the mapping for
the selected environment.

```json
{
  "$schema": "https://digitallpower.dev/schemas/ownership/v1/schema.json",
  "schemaVersion": 1,
  "owners": {
    "salesOwner": {
      "kind": "team",
      "namePattern": "Sales-*",
      "businessUnitName": "Sales"
    },
    "groupOwner": {
      "kind": "team",
      "entraObjectId": "11111111-1111-1111-1111-111111111111",
      "businessUnitName": "Sales",
      "membershipType": "members"
    },
    "humanOwner": {
      "kind": "user",
      "domainName": "deployment@example.org"
    },
    "entraUser": {
      "kind": "user",
      "entraObjectId": "33333333-3333-3333-3333-333333333333"
    },
    "automationOwner": {
      "kind": "applicationUser",
      "applicationId": "22222222-2222-2222-2222-222222222222"
    }
  }
}
```

The V1 schema is [`schemas/ownership/v1/schema.json`](../../schemas/ownership/v1/schema.json).
Ownership-aware consumers use `--owner-mapping <File>`; the [`process`](commands/process.md) commands
support this shared contract for regular and application users; the
[`duplicaterules`](commands/duplicaterules.md) commands also support owning teams.
Relative mapping paths are resolved from the working directory. There is no implicit file search,
default owner, connection-identity fallback, or selector fallback. Explicitly supplied files are
validated even when a consumer invocation has no owner intent; state-only operations do not need a
mapping. Logical keys are ordinal case-sensitive; keys and selector strings must be nonblank and
must not have leading or trailing whitespace.

`user` selectors use exactly one of `entraObjectId` or `domainName` (matched to
`systemuser.domainname`); `applicationUser` uses `applicationId`. A `team` uses either
`entraObjectId`, optionally qualified by `businessUnitName` and `membershipType`, or a
`namePattern` together with `businessUnitName`. GUID selectors must be nonempty. Names and
domain names are compared as whole values using ordinal, case-insensitive comparison. Team
patterns match the whole name and only `*` expands to zero or more characters; `?`, `%`, `_`, and
brackets are literal. Business-unit names must identify exactly one unit, and team qualifiers
combine with AND. Membership values are `membersAndGuests`, `members`, `owners`, and `guests`.
An Entra group-ID selector resolves only group-backed security or Microsoft 365 teams; a name
pattern can also resolve an owner team.

Apply resolution validates the entire file and requested keys locally, then looks up only the
requested keys; unused entries do not cause Dataverse queries. Multiple logical keys may alias the
same principal for apply. Capture is deliberately stricter: it resolves the entire source mapping
and requires exactly one key for each captured principal. Aliases are allowed for principals not
being captured, but aliases for a captured principal are an ambiguity error. Captured owners
without a key fail by default. The built-in platform users SYSTEM and INTEGRATION (exact full
name, no domain name, Entra object ID, or application ID) are always skipped instead, because they
own platform-provided records and cannot be transported. Consumers can opt into skipping other
unmapped owners (`--unmapped-owners skip`); ambiguity still fails. Skipped records are written
without `owner`, so apply preserves the target owner, and each command warns per skipped principal.
Consumers must finish capture validation before writing configuration output.

Resolution returns principal identity, principal kind, and user-disabled/team-type metadata; it
does not assign records or claim resource-specific assignment eligibility. Disabled users remain
resolvable so a consumer can apply its own policy before writing. Owner, security-group, and
Microsoft 365 group teams can own records; access teams and unclassified teams are rejected.
Missing, ambiguous, wrong-kind, and ineligible matches fail explicitly with selector context.
Lookups use complete Dataverse paging, do not choose a first match, and do not retry or substitute
another selector. The [portable mapping example](../../tests/dgt.power.common.tests/Ownership/Fixtures/owner-mapping.example.json)
is shared with loader tests.

Resource configuration refers to the logical key rather than embedding a tenant identity, for
example `{ "owner": "salesOwner" }`. Process and duplicate-rule commands use this contract; additional resources
may adopt it with their own eligibility policies. Use the source environment's mapping file during
capture and the destination environment's
mapping file during apply. The same logical key can intentionally resolve to different principals
in those files. Apply permits aliases, while capture rejects aliases only when multiple keys resolve
to an owner being captured; aliases for uncaptured principals are harmless.

For resources that allow team owners (such as duplicate rules, but not process commands), the source mapping can pin the source
team's Entra group identity while the target mapping uses a target-specific name and business unit:

```json
{
  "schemaVersion": 1,
  "owners": {
    "salesOwner": {
      "kind": "team",
      "entraObjectId": "11111111-1111-1111-1111-111111111111"
    }
  }
}
```

```json
{
  "schemaVersion": 1,
  "owners": {
    "salesOwner": {
      "kind": "team",
      "namePattern": "Sales-*",
      "businessUnitName": "Sales"
    }
  }
}
```

The source file is resolved and reverse-matched during capture; the target file is lazily resolved
for requested keys during apply. Each consumer must validate resource-specific admissibility before
its first write and must finish capture validation before creating or replacing its output file.
