# Design

## Context

`UpdateWorkflowState.ResolveSystemUserAsync` queries `systemuser.domainname` with `TopCount = 1` and caches by the supplied string. `CreateWorkflowStateConfig` chooses the most frequent owner as a default and emits domain-name overrides. `_Assignee.cs` supplies only an optional owner string. None is an adequate portable multi-principal contract.

The process and duplicate-rule changes both need owner resolution but have different lifecycle and assignment constraints. Duplicate rules use solution-stable rule GUIDs; those IDs are independent of owner identity. Documenttemplate ownership is not part of the approved scope.

## Goals / Non-Goals

**Goals:** One small read-only resolution boundary, explicit target mappings, deterministic errors, and enough principal classification for resource-level validation.

**Non-Goals:** Assignment execution, lifecycle orchestration, impersonation, authorization guarantees, tenant directory synchronization, principal creation, migration of every legacy owner string, or a generic identity provider/plugin system.

## Decisions

### Approved boundary

- Resources refer to logical owner keys. Environment-specific mappings identify existing Dataverse principals. Keep resource intent separate from target identities so the same resource configuration can be applied across environments.
- Cover regular users, application users, and teams capable of owning records. This is a resolver coverage requirement, not a promise that every resource accepts every kind.
- Return the Dataverse principal reference, classified kind, and relevant eligibility metadata, including disabled status for users and team type for teams. Disabled users remain resolvable for capture; the consuming resource decides whether they are admissible for assignment before its first write.
- Missing mappings, missing targets, ambiguous targets, and wrong principal kinds are explicit errors. Never use a first match, most-common-owner heuristic, display-name guess, or connection-identity fallback.
- Resolution performs reads only and uses the command's shared asynchronous connection. Constructors must not perform I/O. Cancellation propagates.
- Resolve principals in the target Dataverse environment; do not introduce a Microsoft Graph dependency or separate directory authentication.
- Process and duplicate-rule integrations belong to their own changes. Do not migrate existing productive workflow commands here.

### Placement and API

Place all new mapping, resolver, result, and error types together in `src/dgt.power.common/Ownership`, using namespace `dgt.power.common.Ownership`. Mirror tests in `tests/dgt.power.common.tests/Ownership`. Do not spread these types across legacy DTO and Logic folders or put them beside generated Dataverse entities in DotNet. No new assembly or package dependency is needed.

Use one `IOwnerResolver` interface and implementation, one mapping loader, and typed mapping/result/error models. The loader handles file/JSON validation without Dataverse access. The resolver exposes forward resolution for requested keys and reverse resolution for captured owner references. Keep concrete type names other than the interface implementation details, not extra extension points.

Domain failures distinguish invalid mapping, missing key, not found, ambiguous match, unsupported/wrong kind, and missing/ambiguous capture mapping, with logical key or captured reference and selector context. Shared code does not print console messages or return a successful default. Consumers render failures through their normal command error handling. Do not wrap authentication/Dataverse failures as not-found results; propagate those failures and cancellation. No application retry layer or automatic assignment recovery is introduced.

Use the run's shared async Dataverse service. No global/static cache; reuse resolved entries only within a resolution call. No repository, factory, provider abstraction, command base class, or new DI-framework dependency.

### Mapping format and loading

Store one target-environment mapping per file. The file requires `schemaVersion: 1` and an `owners` object; optional root `$schema` is a URI for editor tooling, not runtime schema selection. Publish the matching JSON schema at `schemas/ownership/v1/schema.json`.

```json
{
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
    "automationOwner": {
      "kind": "applicationUser",
      "applicationId": "22222222-2222-2222-2222-222222222222"
    }
  }
}
```

Logical keys are nonblank, ordinal case-sensitive, have no leading/trailing whitespace, and have no wildcard semantics. The same whitespace restrictions apply to selector strings; do not silently normalize them. GUID selectors must be nonempty GUIDs. Reject unknown or duplicate JSON properties, duplicate owner keys, null entries, unsupported versions, and invalid selector combinations. JSON-schema validation alone does not detect duplicate properties; the loader must enforce this too. An empty owners object is valid when no owner references need resolving.

Resource configurations refer only to logical keys, for example `"owner": "salesOwner"`. Intended consumers expose `--owner-mapping <File>`; relative CLI paths resolve against the working directory. Do not search default locations, embed connection settings, auto-select an environment, or fall back to an owner identity in resource configuration. Mapping is required for requested ownership resolution and owner capture, not state-only operations. An explicitly supplied file is always loaded and structurally validated, even for a run without owner intent.

Command-option registration, resource owner-property parsing, and lifecycle integration belong to consumer changes. No standalone owner command is added here.

### Selectors and comparison

| Kind | Required selector | Allowed qualifiers |
| --- | --- | --- |
| `user` | Exactly one of `entraObjectId` or `domainName` | None |
| `applicationUser` | `applicationId` | None |
| `team` | `entraObjectId` | Optional `businessUnitName` and `membershipType` |
| `team` | `namePattern` and `businessUnitName` | None |

No Dataverse systemuser/team ID selector is exposed. User `entraObjectId` queries `systemuser.azureactivedirectoryobjectid`; `domainName` queries `systemuser.domainname`, not email or an independently resolved directory UPN. `applicationId` queries `systemuser.applicationid`. Team `entraObjectId` queries `team.azureactivedirectoryobjectid`. GUIDs identify principals in the target mapping and may differ between tenants.

First resolve any BU qualifier by exact name without case sensitivity; no wildcards. Missing or multiple BUs are errors even if only one candidate team would otherwise remain. Apply the resolved BU and all other supplied qualifiers with AND. Membership strings are `membersAndGuests`, `members`, `owners`, and `guests`, corresponding to Dataverse values 0, 1, 2, and 3. Membership filtering is allowed only for the Entra team selector.

Match domain names and BU names exactly using ordinal case-insensitive comparison. Team patterns match the entire name using ordinal case-insensitive comparison; only `*` means zero or more characters. Every other character, including `?`, `%`, `_`, and brackets, is literal. Names without `*` are exact names. No regular expressions, SQL-LIKE passthrough, implicit substring search, or fallback selectors.

Candidate retrieval must preserve this matching contract regardless of database collation. Use complete paging where needed and perform the promised comparisons on retrieved candidates. Server filters may narrow only without excluding valid candidates under the documented semantics. Never use `TopCount = 1` or truncate before applying literal/glob matching.

Require exactly one final match before validating its principal kind or team ownership capability. Ineligible candidates must not be silently filtered out to manufacture uniqueness. For example, two name matches remain ambiguous even if one is an access team. Selectors are exact constraints, not priorities.

Classify application users using Dataverse application identity metadata rather than display-name conventions, and reject them for `kind: user`. An Entra team selector must resolve to a matching group-backed team. Support owner teams (teamtype 0), security-group teams (2), and Microsoft 365 group teams (3); reject access teams (1) and unknown kinds. Disabled user status is returned, not interpreted as a universal assignment prohibition. Missing required classification metadata is a resolution error, not an assumed eligible default.

### Forward and reverse resolution

For apply, structurally validate the entire mapping first, validate all requested keys exist, and resolve only requested entries. Unused valid entries do not trigger target lookups. Different logical keys may resolve to the same `(logical entity name, Dataverse ID)`; this permits separate business roles to share a principal in one environment.

For ownership capture, require and fully resolve the source-environment mapping. Any invalid target mapping prevents capture even if it would be unused. Match each captured owner reference by logical entity name and ID against those results. Exactly one logical key must match each captured owner. No match or multiple aliases for that captured owner is an error; aliases for principals not captured do not by themselves make capture ambiguous.

State-only capture does not require owner mapping. The shared reverse resolver never invents keys, emits GUID/domain-name substitutes, chooses a default owner, or writes an output file. Consumers must finish capture validation before creating/replacing their configuration output. Do not provide a mapping generator or capture key-selection option in V1.

## Platform Evidence

The existing generated Team model defines membership types 0 through 3 and owner/access/security-group/Office-group team types. Microsoft documents that Entra group teams can own records and that separate Dataverse teams may represent owners, members, guests, or members and guests of the same Entra group. Therefore group object ID alone is not assumed unique.

- https://learn.microsoft.com/en-us/power-platform/admin/manage-group-teams

## Open Questions

None blocking the agreed contract. Implementation must verify the classification and query behavior above with focused metadata-backed tests; a platform contradiction requires design review rather than a fallback.

## Risks / Trade-offs

- Portable labels can conceal tenant-specific identities -> keep selectors in per-environment mappings and document tenant-specific GUIDs.
- Shared resolution can expand into lifecycle management -> return principal identity/kind only; leave assignment and state restoration in each resource.
- Multiple users/teams may match an apparent unique name -> detect ambiguity and fail rather than choosing the first.
- Principal resolution does not prove assignment permissions -> validate known constraints in consumers and report server failures without claiming permission preflight is complete.
- Capture requires complete source mapping and rejects aliases for captured owners -> document the stricter capture contract and test it independently from lazy apply resolution.

## Migration Plan

Add the shared capability without altering current commands. Document the approved owner contract and examples in README when implemented. The process and duplicate-rule changes adopt it through their own settings, schemas, tests, and migration guidance. Existing `_Assignee` consumers remain unchanged until explicitly migrated; do not remove shared DTOs here.

Rollback of this additive change means reverting its own unused contract/resolver slice. Once consumers depend on it, changes require coordinated compatibility review. No resource or owner records are mutated by the resolver.
