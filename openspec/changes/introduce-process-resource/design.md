# Design

## Context

See proposal.md for motivation. `CreateWorkflowStateConfig`, `UpdateWorkflowState`, and `WorkflowStateManager` currently combine selection, category-specific name matching, state transitions, ownership, console progress, and retry rounds. Existing tests cover flows, actions, and business rules under `tests/dgt.power.maintenance.tests/`.

Observed debt: display-name matching, a most-common-owner default, missing entries implying activation, single-page retrieval, name-key collisions during capture, OR-combined LIKE filters, per-table metadata requests for indirect business rules, and reflection-based impersonation with a silent fallback. These are not behaviors to preserve.

Evidence from a customer dev/acceptance pair (5000-row export, so cloud flow and classic workflow counts are incomplete):

| Category | `uniquename` populated | Distinct names / total | Same `workflowid` in both envs |
| --- | --- | --- | --- |
| Action (3) | all | 883 / 889 | 889 of 891, 130 with different localized names |
| Business rule (2) | none | 173 / 186 | 186 of 187 |
| BPF (4) | all | 17 / 17 | 17 of 18 |
| Cloud flow (5) | none | 1141 / 4742 | all deployed managed flows |
| Classic workflow (0) | none | 112 / 112 | not in export |

No process existed under a different ID with the same name in the target.

## Goals / Non-Goals

**Goals:** Make intent portable and explicit; one identity rule for all categories; isolate process behavior from the host and other resources; follow the `init`/`apply` conventions of the sibling duplicate-rule resource.

**Non-Goals:** Deploy process definitions, manage connection references, implement an arbitrary workflow engine or dependency-graph analysis, invent a generic state-management framework, or preflight impersonation privileges.

## Decisions

### Module and contract

One `dgt.power.process` assembly owns commands, configuration, selection, and execution. It depends on the common ownership resolver and the connection's impersonation overload, not on maintenance internals. One init/apply contract with independently optional properties; no separate state and ownership pipelines.

### Identity and categories

`processId` (the Dataverse `workflowid`) is the only identity for every supported category. A display `name` is optional, written by init for review, and ignored by apply. No name, unique-name, or table-scoped fallback; no duplicate-name diagnostics are needed.

Supported categories: classic workflow (0), business rule (2), action (3), business process flow (4), cloud flow (5), restricted to definitions (`type = 1`). A configured ID that is missing in the target, is not a definition, or has an unsupported category (dialog, AI flow, desktop flow, unknown) fails preflight.

Alternatives rejected: `uniquename` (absent for business rules, cloud flows, and classic workflows), display names (not unique, localized), and a hybrid key (two rules for no measured benefit).

### Selection (init only)

`--solution <UniqueName>` is required and repeatable. Solutions match by exact unique name, case-insensitive ordinal; no wildcards. Multiple solutions form a union. A solution that does not exist fails before output is written. Publisher filters and an unmanaged-only filter are not offered; whether "unmanaged" means "ours" depends on the environment.

The selection contains processes that are solution components of the selected solutions, plus business rules of tables that a selected solution includes with all subcomponents (root component behavior "include subcomponents"). Resolve those table IDs to logical names with one metadata request rather than one request per table. Retrieval is fully paged. Apply never re-selects; it acts only on IDs in the file.

### Configuration format

```json
{
  "$schema": "https://.../schemas/process/v1/schema.json",
  "schemaVersion": 1,
  "processes": [
    {
      "processId": "265a263f-958b-4d32-b89d-6b3fdeac0458",
      "name": "Create additional Document Location",
      "active": true,
      "owner": "integrationUser"
    }
  ]
}
```

Require `schemaVersion: 1` and a `processes` array. Each entry requires a nonempty GUID `processId` and at least one of boolean `active` or logical-key string `owner`; `name` is optional. Omitted properties preserve target values; explicit null is invalid. Reject duplicate IDs, duplicate JSON properties, unknown fields, unsupported versions, and blank or whitespace-padded owner keys. No root-level defaults for owner, state, or impersonation. Publish the schema at `schemas/process/v1/schema.json`; enforce duplicate-property checks at runtime as well. Init writes entries ordered by `processId`.

### Commands

```text
process init <File> --solution <UniqueName> [--solution ...]
    [--capture state|owner|both] [--owner-mapping <File>] [--overwrite]

process apply <File> [--owner-mapping <File>]
    [--activate-as <OwnerKey> | --activate-as-owner] [--dry-run]
```

- Paths are positional, working-directory-relative when not absolute, with no default file.
- `--capture` defaults to `state`. `owner`/`both` require `--owner-mapping`. An explicitly supplied mapping is always loaded and structurally validated.
- Existing output fails unless `--overwrite`. Output is never created or replaced after a selection or reverse-mapping error.
- Zero selected processes is a successful capture of an empty array. Applying an empty array is a successful no-op without authentication.
- `--activate-as` and `--activate-as-owner` are mutually exclusive. `--activate-as` requires `--owner-mapping`.
- The report is always printed; legacy `--tablereport` and `--detailed` are not carried over.

### Ownership

Use the shared resolver from `introduce-portable-owner-resolution`. Regular users and application users are admissible owners; teams fail preflight. Capture reverse-maps every captured owner to exactly one logical key; missing or ambiguous mappings fail capture. Disabled users fail preflight only for an actual owner change, and when used as an impersonation identity.

### Impersonation

Impersonation is a run-level option, not a per-process field:

- `--activate-as <OwnerKey>` runs every state transition as the mapped principal (typical case: a service principal with whom connections are shared).
- `--activate-as-owner` runs every state transition of a process as its configured target owner. Each entry with `active` must then also have `owner`; otherwise preflight fails. The current target owner is never looked up as an implicit substitute.

All state transitions, both activation and deactivation, are impersonated when an option is set; owner assignment always runs as the connection identity. The impersonated service comes from `IDataverseConnection.GetServiceAsync(Guid callerId)`. If it cannot be obtained, apply fails before the first write. Missing `prvActOnBehalfOfAnotherUser` surfaces as a per-object runtime failure.

### Ordering and failure handling

```text
owner change on active process (desired active or omitted):
  deactivate (impersonated) -> assign (connection) -> activate (impersonated)
owner change on inactive process, or with active=false:
  [deactivate if needed] -> assign
state only:
  activate or deactivate (impersonated)
```

- Skip writes whose desired value already matches.
- If assignment fails after an intermediate deactivation, try exactly one reactivation to restore the original state, then report the assignment failure and whether the restoration succeeded.
- Failed activations are retried in rounds while each round reduces the number of failures (child flows must be active before parents). Assignment, deactivation, and restoration failures are not retried.
- Independent processes continue after a final failure. Exit unsuccessfully if any requested operation fails. Cancellation stops further work and reports completed transitions. No transactions or rollback beyond the single restoration attempt.

### Preflight and dry-run

Validate the file, options, and mapping locally before connecting. Then resolve all processes, owner keys, and the impersonation identity before any write. Missing, unsupported, or ambiguous references fail with zero writes. Dry-run uses the same resolution and preflight, reports per process the planned transitions or "unchanged", is visibly marked, writes nothing, and performs no retry rounds.

## Risks / Trade-offs

- [GUID-only configuration is hard to read] -> init writes display names; apply ignores them.
- [Processes recreated in a target under a new ID cannot be matched] -> preflight reports them as missing; no name-based guess.
- [Restoration can itself fail and leave a production flow inactive] -> report it explicitly as an unrecovered intermediate change.
- [Activation order of dependent flows is unknown] -> bounded retry rounds instead of dependency analysis.
- [Impersonation depends on a connection-contract change under separate review] -> process apply without impersonation options does not depend on it; tasks for impersonation are blocked until that change is implemented.
- [Hidden legacy defaults in tests] -> retain useful fixtures but replace assertions for intentionally changed behavior.

## Migration Plan

Introduce the module, schema, and focused tests; switch host registration and remove the two old commands, their configuration models, and `schemas/maintenance/workflow`. Document replacement examples and breaking changes (GUID identity, mandatory solution, no publisher/wildcard filters, no implicit activation or default owner, run-level impersonation) in README and `docs/migrations/2.x-to-3.x.md`. No compatibility aliases or old-format reader. Rollback means reverting a consistent code slice; it does not undo Dataverse mutations.
