# Tasks

The contract is approved for planning. Implementation requires a separate apply request. Keep changes additive: no existing command migration, assignment/lifecycle execution, new assembly, package dependency, or standalone owner CLI.

## 1. Mapping contract and loading

- [ ] 1.1 Add cohesive typed mapping/result/error contracts and `IOwnerResolver` in `src/dgt.power.common/Ownership`, with mirrored common tests; verify focused build, one top-level type per file, no new assembly/dependency, and no I/O during construction.
- [ ] 1.2 Add `schemas/ownership/v1/schema.json` matching V1 models; verify all user/application/team selector alternatives, membership enums, optional `$schema`, empty mappings, required fields, nonempty GUIDs, whitespace rules, forbidden combinations and unknown fields with valid/invalid fixtures.
- [ ] 1.3 Implement explicit file loading and strict parsing before Dataverse access; verify working-directory-relative paths, missing/malformed files, unsupported versions, duplicate JSON properties/owner keys, null entries, and case-sensitive logical keys.
- [ ] 1.4 Document V1 mapping examples, tenant-specific identities, `--owner-mapping` loading convention, no implicit defaults, and schema location in README; verify examples validate against the schema and loader.

## 2. Target principal resolution

- [ ] 2.1 Implement user lookups by Entra object ID or domainname and application-user lookup by application ID; verify exact comparison semantics, explicit kind classification, missing/multiple matches, wrong-kind errors, no selector fallback, and disabled status in results.
- [ ] 2.2 Implement unique BU-name resolution and team lookups by Entra group ID with optional AND-combined BU/membership filters; verify all four membership values, shared-group ambiguity, missing/ambiguous BUs, owning group-team kinds, and rejection of access/unknown teams or missing classification metadata.
- [ ] 2.3 Implement BU-scoped team name matching with only `*`; verify whole-name ordinal case-insensitive matching, zero-length expansion, no-star exact matching, literal `?`/`%`/`_`/brackets, complete candidate paging, and ambiguity before eligibility filtering.
- [ ] 2.4 Implement requested-key-only forward resolution through the shared async service; verify whole-file local validation, requested-key existence before lookups, no target access for unused entries, allowed aliases, no global cache, typed errors, no console/mutation calls, cancellation, and propagated service/authentication failures.
- [ ] 2.5 Document exact selectors, globbing/BU/membership semantics, eligibility boundaries, disabled-user policy, ambiguity diagnostics, and absence of retry/fallback behavior in README; verify examples against resolution tests.

## 3. Owner capture and consumer boundary

- [ ] 3.1 Implement full-source-mapping resolution and reverse lookup by principal entity name plus ID; verify unique captured keys, missing keys, captured-owner alias ambiguity, aliases for uncaptured principals, and failure on unresolved unused source entries.
- [ ] 3.2 Add consumer-boundary tests for state-only/no-mapping, owner apply, and owner capture contracts without registering commands; verify explicitly supplied mappings are always locally validated, resource admissibility remains independent, and capture failures occur before consumer output writes.
- [ ] 3.3 Document forward-versus-capture differences, source/target mapping examples, aliases, and integration responsibilities in README; verify process/duplicate-rule examples use logical keys and do not imply current command availability.

## 4. Verification

- [ ] 4.1 Run focused common ownership tests, relevant existing common tests, `dotnet build`, and `pnpm exec openspec validate --all`; verify no legacy owner DTO, workflow behavior, or existing CLI surface is incidentally changed.

## Workflow follow-up

- Reconcile process and duplicate-rule planning with this owner contract in their own changes; command flag registration, resource schemas, assignment policy, and lifecycle execution belong there.
- After implementation and verification, synchronize the implemented requirements into main specs and archive through the corresponding OpenSpec workflows.
