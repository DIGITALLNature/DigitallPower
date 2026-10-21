# Design

## Context

See proposal.md for motivation. `CreateWorkflowStateConfig`, `UpdateWorkflowState`, and `WorkflowStateManager` currently combine selection, category-specific matching, state transitions, ownership, console progress, and retries. Existing tests cover flows, actions, and business rules under `tests/dgt.power.maintenance.tests/`.

Observed debt includes display-name matching, a most-common-owner default, missing entries implying activation, single-page retrieval, name-key collisions during capture, and reflection-based impersonation with a permissive fallback. These are review targets, not behaviors to preserve.

## Goals / Non-Goals

**Goals:** Make intent portable and explicit; keep category-specific constraints visible; isolate process behavior from the host and other resources.

**Non-Goals:** Deploy process definitions, implement an arbitrary workflow engine, or invent a generic state-management framework.

## Decisions

- One `dgt.power.process` assembly owns commands, configuration, selection, and execution for the agreed categories. Splitting per category is rejected; sharing the Dataverse table does not justify erasing category differences.
- Use one init/apply contract with independently optional properties instead of separate state and ownership pipelines. Owner-only changes preserve the original state after any necessary intermediate deactivation.
- Capture state, ownership, or both explicitly. Do not infer default activation or the most common owner.
- Separate logical owner keys from target mappings. Support regular and application users only where allowed by the process category. Avoid source-GUID-only identity.
- Validate locally before acquiring the run's shared async Dataverse connection, then resolve all references before mutation. Dry-run follows the same resolved intent without writes.
- Continue independent objects after final runtime failures, with an unsuccessful aggregate result and explicit intermediate-change reporting. Do not promise transactions or automatic rollback.
- Keep impersonation explicit and fail when it cannot be honored. Do not silently reuse the connection identity.

## Decision Gates Before Implementation

The scope is approved, but the following details are deliberately unresolved. Obtain user decisions and update design/specs/tasks before implementing the affected contract:

1. Define a unique portable key per category, duplicate-name diagnostics, category coverage (including the existing apply-only AIFlow case), and treatment of missing/unsupported metadata.
2. Define selection and cleanup-free scope: solution/publisher filters, their AND/OR semantics, wildcard support, and indirectly included business rules.
3. Define versioned JSON and mapping shapes, regular/application-user lookup keys, capture-time mapping generation, and separation of impersonation identity from owner identity.
4. Confirm operation ordering per category, impersonation boundaries, dependency discovery or bounded retry policy, and recovery/reporting after owner assignment or state restoration fails.
5. Fix exact options, output/overwrite behavior, empty-selection behavior, and dry-run report contract.

These are blocking gates, not assumptions or authorization to pick defaults during apply.

## Risks / Trade-offs

- Ambiguous names or differing environment IDs -> reject ambiguity rather than selecting the first match.
- Platform-specific flow behavior and authentication constraints -> category-specific tests and targeted integration evidence before claiming support.
- State changes survive a later failure -> report actual completed transitions and unrecovered state; no success-shaped fallback.
- Hidden legacy defaults in tests -> retain useful fixtures but replace assertions for intentionally changed behavior.

## Migration Plan

After gate approval, introduce the module, schemas, and focused tests; switch host registration and remove the two old commands and their obsolete configuration. Document replacement examples and breaking format/behavior changes in README and `docs/migrations/2.x-to-3.x.md`. No compatibility aliases or old-format runtime reader. Rollback means reverting a consistent code slice; it does not undo Dataverse mutations.
