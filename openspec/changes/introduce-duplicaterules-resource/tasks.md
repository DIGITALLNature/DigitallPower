# Tasks

The product contract is approved. Implementation requires a separate apply request. Ownership integration depends on the implemented `introduce-portable-owner-resolution` contract. Do not implement definitions, conditions, creation, deletion, another owner resolver, or a generic state engine.

## 1. Platform evidence gate

- [x] 1.1 Verify direct Assign for regular users, application users, and owning teams on Published and Unpublished rules with state/owner read-back; obtain separate approval for live writes and stop for design review if a kind or state-preservation guarantee is unsupported. (Passed; see design "Recorded evidence".)
- [ ] 1.2 Verify real publish/unpublish response, job correlation, intermediate/final states, terminal failures, and five-slot behavior (live evidence recorded in design); add accurate controllable test doubles (Publishing transition with job, synchronous unpublish, faults 0x80048414/0x80048419/0x80048434, job statuses 30/31/32, Guid.Empty for already Published) rather than relying only on instantaneous shared fakes.

## 2. Resource and V1 contract

- [ ] 2.1 Add resource and test projects, references, and module-owned models; verify a focused build succeeds without a process-module dependency.
- [ ] 2.2 Add strict V1 config and `schemas/duplicaterules/v1/schema.json`; verify required nonempty GUIDs, at least one effect, omitted effects, null/unknown/duplicate properties, duplicate IDs, owner-key rules, unsupported versions, and valid empty arrays.
- [ ] 2.3 Add explicit file loading and settings validation; verify positional paths, working-directory-relative resolution, positive timeout default 300, capture default state, repeatable single-GUID selection, dry-run/no-wait combination, and supplied mapping local validation before connecting.
- [ ] 2.4 Document format, explicit intent, GUID-only identity, no definitions/cleanup, and new command examples in README; verify examples against config/schema/settings fixtures.

## 3. Capture

- [ ] 3.1 Implement complete selection by optional base-table filter and repeated GUID set with AND semantics; verify paging, duplicate selection collapse, missing explicit IDs, table/ID conflicts, and rejection of selected Publishing/unknown states.
- [ ] 3.2 Integrate state/owner/both capture and shared full-source reverse mapping; verify GUID ordering, exact requested properties, disabled existing owner capture, missing/ambiguous logical keys, unresolved unused source entries, and state-only without mapping.
- [ ] 3.3 Implement output protection and successful empty capture; verify no overwrite by default, explicit overwrite only after complete validation, unchanged output on capture failure, and surfaced local write failures.
- [ ] 3.4 Document capture filters, mode defaults, source mapping, stable-state requirement, empty results, and overwrite behavior in README; verify examples against capture/schema tests.

## 4. Apply preflight and capacity

- [ ] 4.1 Implement local-first full preflight and shared requested-key owner resolution; verify successful empty apply reporting zero rules without connection, invalid supplied mappings still failing locally, missing GUID without name fallback, any invalid entry preventing all writes, listed Publishing/unknown state rejection, and unchanged omitted owner/state intent.
- [ ] 4.2 Implement disabled-user and `prvReadDuplicateRule` owner policy and direct-assignment preparation; verify disabled new owner rejection, missing read privilege rejection for users and owning teams, already satisfied disabled owner acceptance, supported owning teams/application users, and no reassignment for state-only runs.
- [ ] 4.3 Implement complete Published/Publishing capacity retrieval and projected changes per affected base table; verify five-slot limit, unlisted occupancy, no double-counting, explicit unpublish projection, and preflight failure with zero writes for impossible end states.
- [ ] 4.4 Implement dry-run reporting using the same resolved preparation; verify operation ordering, owners, capacity, mode/timeouts, and no writes even with --no-wait.
- [ ] 4.5 Document preflight/capacity and owner eligibility in README; verify diagnostics and examples agree with negative tests and do not promise complete permission checking.

## 5. Execution and completion

- [ ] 5.1 Implement sequential explicit unpublish execution with immediate read-back confirmation before remaining writes; verify independent failures, unconfirmed read-back keeping the slot occupied, dependent-step skipping, and no unlisted changes.
- [ ] 5.2 Implement direct owner assignment followed by needed publish submission in manifest order; verify stable owner-only state, failure skipping dependent publish, no implicit unpublish/republish, no definition writes, and already satisfied effects producing no writes.
- [ ] 5.3 Map runtime publish faults to item outcomes; verify capacity fault 0x80048419 reported as no available slot without queuing/retry, missing-criteria and already-publishing faults with clear diagnostics, failed unpublish skipping the dependent publish, and continued independent work.
- [ ] 5.4 Implement shared pending-set publish observation with two-second cadence and one deadline after the submission loop; verify all publishes start before final waiting, 300-second default/override, job statuses 31/32 as terminal failures with diagnostics, transient states, mixed outcomes, no per-rule reset, and no mutation retries.
- [ ] 5.5 Implement no-wait outcomes and cancellation/timeout reporting; verify accepted versus Published output, earlier-phase failures still failing, unpublish read-back in no-wait, unresolved jobs after timeout, cancellation stopping further client work, and server jobs explicitly not claimed cancelled.
- [ ] 5.6 Document submission/completion, publish time budget, synchronous unpublish, no-wait, partial effects, failure aggregation, and cancellation in README; verify documentation against stateful async/failure tests.

## 6. Host integration and verification

- [ ] 6.1 Register resource commands, DI, help, and completion; verify command-tree smoke tests and parsing for every new settings type, all capture/filter/wait/overwrite options, and shared mapping path convention.
- [ ] 6.2 Remove only proven orphaned historical duplicate-rule DTOs after consumer checks; verify all remaining Assignee consumers and generated models are preserved and document any retired public types in the major migration guide.
- [ ] 6.3 Run focused resource/CLI tests, relevant shared-fake regression tests, `dotnet build`, and `pnpm exec openspec validate --all`; verify implementation against acceptance scenarios and that no process-module dependency was introduced.

## Workflow follow-up

- After implementation and successful verification, synchronize implemented requirements into main specs and archive through the corresponding OpenSpec workflows.
- Refresh generated Copilot instructions with `pnpm run openspec:update` as required by the repository workflow.
