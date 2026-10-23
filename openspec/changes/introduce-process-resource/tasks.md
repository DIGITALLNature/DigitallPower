# Tasks

The product contract is approved. Implementation requires a separate apply request. Ownership depends on the implemented `introduce-portable-owner-resolution` contract; impersonation depends on the implemented `introduce-connection-caller-impersonation` contract. Do not implement definition deployment, connection-reference management, owner teams, another owner resolver, dependency-graph analysis, or a generic state engine.

## 1. Resource and V1 contract

- [ ] 1.1 Add `dgt.power.process` source and test projects, host references, and module-owned models; verify a focused build succeeds without a dependency on maintenance internals.
- [ ] 1.2 Add strict V1 config and `schemas/process/v1/schema.json`; verify required GUID `processId`, at least one of `active`/`owner`, optional informational `name`, omitted effects, null/unknown/duplicate properties, duplicate IDs, root-level defaults rejected, owner-key rules, unsupported versions, and valid empty arrays.
- [ ] 1.3 Add settings and local validation; verify positional working-directory-relative paths, required repeatable `--solution`, `--capture` default `state`, mapping required for owner/both capture and `--activate-as`, mutually exclusive impersonation options, and supplied-mapping validation before connecting.
- [ ] 1.4 Document format, GUID-only identity, supported categories, and command examples in README; verify examples against schema and settings fixtures.

## 2. Capture

- [ ] 2.1 Implement solution selection by exact unique names with union semantics and full paging, restricted to supported categories and definitions; verify multiple solutions, unknown solution failure, unsupported categories excluded, and multi-page results.
- [ ] 2.2 Implement indirect business rules for tables included with all subcomponents using one metadata resolution; verify included, excluded (delta-only table), and deduplicated rules.
- [ ] 2.3 Implement state/owner/both capture with shared reverse mapping and informational names; verify ID ordering, exact requested properties, state-only without mapping, and missing/ambiguous logical keys.
- [ ] 2.4 Implement output protection and empty capture; verify no overwrite by default, overwrite only after complete validation, unchanged output on capture failure, and surfaced write failures.
- [ ] 2.5 Document capture options, indirect business rules, and overwrite behavior in README; verify examples against capture tests.

## 3. Apply preflight and dry-run

- [ ] 3.1 Implement local validation then full resolution of process IDs, categories, owner keys, and impersonation identity before writes; verify missing ID, non-definition, unsupported category, team owner, disabled user only for actual owner change, `--activate-as-owner` without `owner`, and zero writes on failure.
- [ ] 3.2 Implement dry-run using the same resolved plan; verify reported transitions and unchanged entries, visible dry-run marking, no writes, no retry rounds, and empty-array no-op without authentication.

## 4. Apply execution

- [ ] 4.1 Implement explicit transitions and ordering (deactivate, assign, restore/activate) with no-op detection; verify owner-only preservation of active state, state-only changes, idempotent reapply, and omitted intent.
- [ ] 4.2 Integrate run-level impersonation through `IDataverseConnection.GetServiceAsync(Guid)`; verify `--activate-as` and `--activate-as-owner` apply to activations and deactivations, assignment uses the connection identity, and unavailable impersonation fails before writes. Blocked until `introduce-connection-caller-impersonation` is implemented.
- [ ] 4.3 Implement bounded activation retry and failure handling; verify child-before-parent success in later rounds, stop without progress, no retry for other failures, single restoration after failed assignment with reported outcome, independent continuation, cancellation reporting, and unsuccessful exit.
- [ ] 4.4 Document impersonation options, ordering, dry-run limits, and partial effects in README; verify examples and exit behavior agree with the spec.

## 5. CLI replacement and integration

- [ ] 5.1 Register new commands and DI; remove `maintenance createworkflowstate`, `maintenance workflowstate`, their models, logic, tests, and `schemas/maintenance/workflow` after consumer checks; verify command-tree smoke, settings parsing, and old-path rejection tests.
- [ ] 5.2 Update README command tables and `docs/migrations/2.x-to-3.x.md` with old-to-new paths and breaking changes (GUID identity, mandatory solution, removed publisher/wildcard filters, no implicit activation or default owner, run-level impersonation, removed `--tablereport`/`--detailed`); verify no aliases are documented.
- [ ] 5.3 Run focused process and CLI tests, `dotnet build`, and `pnpm exec openspec validate --all`; verify success and update main specs through the repository workflow before archive.
