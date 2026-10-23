# Tasks

The public contract is approved. This is a planning artifact, not authorization to implement. Technical evidence gates below must pass before their dependent implementation. Do not add cleanup, legacy parsing, configurable status, or delete/recreate.

## 1. Technical evidence gates

- [ ] 1.1 Verify regular Dataverse content/metadata updates and activation for Word and Excel while preserving target IDs; obtain explicit approval for any live test writes, verify by read-back, and stop for design review if replacement would be required.
- [ ] 1.2 Review and sanitize the approved supplied Word fixture before repository inclusion; verify that retained bindings include the account/contact collection and repeating section while unnecessary business text and values are removed.
- [ ] 1.3 Obtain Dataverse-generated N:1 and N:N binding examples and establish metadata resolution for all three relationship types and display fields; verify concrete XML paths against metadata and return unresolved mappings to design review rather than inventing heuristics.

## 2. Resource and portable package

- [ ] 2.1 Add documenttemplate source/test projects, move owned models and OpenXML dependency, and synchronize affected lock files; verify focused build and locked restore after dependency changes.
- [ ] 2.2 Implement strict V1 manifest models and matching JSON schema with explicit required metadata and optional root `$schema`; verify valid examples, missing/unknown properties, unsupported versions, empty GUIDs, and rejection of legacy status/force fields.
- [ ] 2.3 Implement manifest-relative document resolution and package/type checks without fallbacks; verify nested paths, absolute/traversal/symlink escapes, missing files, malformed packages, and declared-type mismatches.
- [ ] 2.4 Implement semantic matching and source-ID collision checks; verify case-insensitive names, manifest display spelling, whitespace rejection, duplicate keys/IDs, ambiguous targets, new source-ID creation, existing target-ID preservation, and identity changes without inferred renames.
- [ ] 2.5 Document the manifest, identity, explicit null-description behavior, language, paths, and active-only contract in README; verify sample manifests against the schema and examples against parsing tests.

## 3. Document preparation

- [ ] 3.1 After gates 1.2 and 1.3, implement used CRM binding extraction and target field/relationship validation; verify 1:N/N:1/N:N, repeating collections, display projections, missing/ambiguous references, unsupported CRM syntax, and ignored unused fields/foreign bindings.
- [ ] 3.2 Implement targeted CRM namespace adaptation before writes; verify different target object type codes in main/custom XML/header/footer parts, consistent related bindings sharing a root namespace, valid resulting packages, and preservation of unrelated content.
- [ ] 3.3 Implement Excel preparation and primary-table validation without adaptation; verify invalid input fails and uploaded bytes equal input, including unchanged formulas/data-source content.
- [ ] 3.4 Document supported Word bindings and strict validation, Excel pass-through, and the absence of rendering/data-access guarantees; verify documentation agrees with representative fixtures and negative tests.

## 4. Import lifecycle

- [ ] 4.1 Implement local-first preflight and target-side preparation of the full input before writes; verify a later invalid entry leaves all targets untouched, preparation stops at the first error, and an empty import succeeds without connecting.
- [ ] 4.2 After gate 1.1, implement regular creates/updates and mandatory activation using prepared payloads; verify metadata/content changes, explicit description clearing and language, ID rules, active/draft-to-active transitions, repeat imports without duplicates, unchanged unlisted active/draft templates, and no delete calls.
- [ ] 4.3 Implement sequential best-effort execution across templates and explicit partial-effect reporting; verify content-write and activation failures skip dependent steps, subsequent independent templates run, and any failure makes the command fail.
- [ ] 4.4 Implement dry-run and cancellation handling; verify dry-run performs the same preparation with zero writes, cancellation does not continue as an item error, and completed writes are never reported as rolled back.
- [ ] 4.5 Document preflight, dry-run, best-effort failures, cancellation, and non-atomic behavior in README; verify examples and reported outcomes agree with failure tests.

## 5. Export

- [ ] 5.1 Implement active-only selection with AND-combined optional entity/type filters; verify both Word and Excel, draft exclusion even when filters match, and successful empty export.
- [ ] 5.2 Implement source-GUID document filenames and status-free `documenttemplates.json`; verify explicit source IDs/metadata, filename uniqueness, valid exported schema, and import of the exported package under the approved active deployment contract.
- [ ] 5.3 Prepare output and check all collisions before file writes; verify default collision failure leaves existing files untouched, explicit overwrite affects only generated destinations, unrelated files remain, and write failures report partial output.
- [ ] 5.4 Document export filters, filenames, empty results, and overwrite behavior in README; verify command examples against settings tests and export fixtures.

## 6. CLI replacement and integration

- [ ] 6.1 Register positional `documenttemplates import/export`, settings, DI, and completion metadata, including dry-run and overwrite; remove legacy command paths and verify command-tree smoke tests, every changed settings type, help/completion, invalid options, and old-path rejection.
- [ ] 6.2 Remove proven obsolete legacy document code/DTOs/dependencies after consumer checks; update README and `docs/migrations/2.x-to-3.x.md` with CLI/format changes, source/target ID rules, active-only behavior, removal of inline directives and status/force/missing fields, and absence of cleanup/replacement; verify no remaining legacy consumers.
- [ ] 6.3 Run focused resource/CLI tests, `dotnet build`, and `pnpm exec openspec validate --all`; verify success and review acceptance scenarios against actual implemented behavior.

## Workflow follow-up

- After implementation approval and successful verification, synchronize implemented requirements into main specs and archive using the corresponding OpenSpec workflows.
- Refresh generated Copilot instructions with `pnpm run openspec:update` as required by the repository workflow.
