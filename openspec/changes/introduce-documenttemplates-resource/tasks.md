# Tasks

The public contract is approved. This is a planning artifact, not authorization to implement. Technical evidence gates below must pass before their dependent implementation. Do not add cleanup, legacy parsing, configurable status, or delete/recreate.

## 1. Technical evidence gates

- [x] 1.1 Verify regular Dataverse content/metadata updates and activation for Word and Excel while preserving target IDs; obtain explicit approval for any live test writes, verify by read-back, and stop for design review if replacement would be required.

  Evidence: approved live checks on 2026-10-25 verified creates, draft and active content updates, name/description updates, language changes from 1031 to 1033, explicit description clearing, and activation for both formats with stable IDs and no deletes. Word test ID: `8a3902e8-0111-46b5-9b83-1136b2336e64`; Excel test ID: `445b8a1a-ba95-45cf-a441-513401bd90d1`. Both test records remain. Status is update-only (`true` = draft, `false` = active). Excel creation normalized package XML server-side; subsequent tested updates returned byte-identical content. This verifies regular lifecycle support, not rendering or byte-preserving server storage.

- [x] 1.2 Review and sanitize the approved supplied Word fixture before repository inclusion; verify that retained bindings include the account/contact collection and repeating section while unnecessary business text and values are removed.

  Evidence: `tests/dgt.power.documenttemplates.tests/Resources/AccountContactRepeating.docx` retains all five supplied binding XPaths, including three `w15:dataBinding` elements, the account/contact collection, and its repeating section/item. Visible text and sample values are neutral, unused custom XML fields and document properties are removed, and the store ID is replaced consistently. `DocumentTemplateFixtureTests` verifies the retained bindings, namespaces, store references, minimal data, and sanitized properties. The supplied original remains unchanged.

- [x] 1.3 Obtain Dataverse-generated N:1 and N:N binding examples and establish metadata resolution for all three relationship types and display fields; verify concrete XML paths against metadata and return unresolved mappings to design review rather than inventing heuristics.

  Evidence and approved design review (2026-10-26): the additional document supplies `account_primary_contact/fullname` and lookup display projections resolved through source `AttributeOf` metadata. An isolated export selecting only N:N `accountleads_association` produces `account_leads/name` and `lead_accounts/fullname`, establishing the bounded alias mapping described in design and specs. Direct relationship requests confirm that the aliases themselves are not metadata relationship names. The user approved explicit metadata-checked support for these two aliases and rejection of other unknown/ambiguous aliases, without adding a manifest option or a naming heuristic. Sanitized `AccountRelationships.docx` and `AccountLeadManyToMany.docx` preserve all six/two generated bindings respectively; fixture tests pass and both originals remain unchanged. The lifecycle test environment lacks lead, so positive alias validation uses source metadata and offline tests rather than writes there.

## 2. Resource and portable package

- [x] 2.1 Add documenttemplate source/test projects, move owned models and OpenXML dependency, and synchronize affected lock files; verify focused build and locked restore after dependency changes.
- [x] 2.2 Implement strict V1 manifest models and matching JSON schema with explicit required metadata and optional root `$schema`; verify valid examples, missing/unknown properties, unsupported versions, empty GUIDs, and rejection of legacy status/force fields.
- [x] 2.3 Implement manifest-relative document resolution and package/type checks without fallbacks; verify nested paths, absolute/traversal/symlink escapes, missing files, malformed packages, and declared-type mismatches.
- [x] 2.4 Implement semantic matching and source-ID collision checks; verify case-insensitive names, manifest display spelling, whitespace rejection, duplicate keys/IDs, ambiguous targets, new source-ID creation, existing target-ID preservation, and identity changes without inferred renames.
- [x] 2.5 Document the manifest, identity, explicit null-description behavior, language, paths, and active-only contract in README; verify sample manifests against the schema and examples against parsing tests.

## 3. Document preparation

- [x] 3.1 After gates 1.2 and 1.3, implement used CRM binding extraction and target field/relationship validation; verify 1:N/N:1/N:N, repeating collections, display projections, missing/ambiguous references, unsupported CRM syntax, and ignored unused fields/foreign bindings.
- [x] 3.2 Implement targeted CRM namespace adaptation before writes; verify different target object type codes in main/custom XML/header/footer parts, consistent related bindings sharing a root namespace, valid resulting packages, and preservation of unrelated content.
- [x] 3.3 Implement Excel preparation and primary-table validation without adaptation; verify invalid input fails and uploaded bytes equal input, including unchanged formulas/data-source content.
- [x] 3.4 Document supported Word bindings and strict validation, Excel pass-through, and the absence of rendering/data-access guarantees; verify documentation agrees with representative fixtures and negative tests.

## 4. Import lifecycle

- [x] 4.1 Implement local-first preflight and target-side preparation of the full input before writes; verify a later invalid entry leaves all targets untouched, preparation stops at the first error, and an empty import succeeds without connecting.
- [x] 4.2 After gate 1.1, implement regular creates/updates and mandatory activation using prepared payloads; verify metadata/content changes, explicit description clearing and language, ID rules, active/draft-to-active transitions, repeat imports without duplicates, unchanged unlisted active/draft templates, and no delete calls.
- [x] 4.3 Implement sequential best-effort execution across templates and explicit partial-effect reporting; verify content-write and activation failures skip dependent steps, subsequent independent templates run, and any failure makes the command fail.
- [x] 4.4 Implement dry-run and cancellation handling; verify dry-run performs the same preparation with zero writes, cancellation does not continue as an item error, and completed writes are never reported as rolled back.
- [x] 4.5 Document preflight, dry-run, best-effort failures, cancellation, and non-atomic behavior in README; verify examples and reported outcomes agree with failure tests.

## 5. Export

- [x] 5.1 Implement active-only selection with AND-combined optional entity/type filters; verify both Word and Excel, draft exclusion even when filters match, and successful empty export.
- [x] 5.2 Implement source-GUID document filenames and status-free `documenttemplates.json`; verify explicit source IDs/metadata, filename uniqueness, valid exported schema, and import of the exported package under the approved active deployment contract.
- [x] 5.3 Prepare output and check all collisions before file writes; verify default collision failure leaves existing files untouched, explicit overwrite affects only generated destinations, unrelated files remain, and write failures report partial output.
- [x] 5.4 Document export filters, filenames, empty results, and overwrite behavior in README; verify command examples against settings tests and export fixtures.

## 6. CLI replacement and integration

- [x] 6.1 Register positional `documenttemplates import/export`, settings, DI, and completion metadata, including dry-run and overwrite; remove legacy command paths and verify command-tree smoke tests, every changed settings type, help/completion, invalid options, and old-path rejection.
- [x] 6.2 Remove proven obsolete legacy document code/DTOs/dependencies after consumer checks; update README and `docs/migrations/2.x-to-3.x.md` with CLI/format changes, source/target ID rules, active-only behavior, removal of inline directives and status/force/missing fields, and absence of cleanup/replacement; verify no remaining legacy consumers.
- [x] 6.3 Run focused resource/CLI tests, `dotnet build`, and `pnpm exec openspec validate --all`; verify success and review acceptance scenarios against actual implemented behavior.

## Workflow follow-up

- After implementation approval and successful verification, synchronize implemented requirements into main specs. Keep this change active until its implementation PR is verified as merged into `beta` or `main`; archival is a separate post-merge follow-up, not part of a PR into the planning branch.
- Refresh generated Copilot instructions with `pnpm exec openspec update` as required by the repository workflow.
