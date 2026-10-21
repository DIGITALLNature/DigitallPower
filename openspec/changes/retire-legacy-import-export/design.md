# Design

## Context

The host registers nine resources in each transfer direction. Teamtemplates and documenttemplates have dedicated changes; the other seven are undecided. Calendar paths currently differ (`import calendar` versus `export calendars`). Secure-config import is already prohibited by the CLI spec.

## Goals / Non-Goals

**Goals:** Decide both directions of each resource together, then retire the verb-oriented containers.

**Non-Goals:** Automatically preserve every old function or delete functions based solely on their current module.

## Decisions

- Review import and export as a resource pair, while allowing an explicit decision to retain only one direction.
- Each retained resource needing migration gets a separately reviewed change with its own behavior contract.
- Coordinate bulk-delete transfer with maintenance job execution. Do not create competing resource owners across retirement changes.
- Modify the existing secure-config requirement as a complete block: preserve the prohibition and `plugin step config set`, but remove its promise that other old import commands remain available.
- Move genuinely shared helpers only after consumer inspection; do not leave resource modules referencing legacy verb modules.

## Feature Decision Gates

Every row is pending user approval. Record value, tool overlap, retained behavior, replacement, migration change, and explicitly removed behavior.

| Resource | Current CLI suffix | Import disposition | Export disposition | Destination / change |
|---|---|---|---|---|
| Bulk-delete jobs | `bulkdeletes` | Pending | Pending | Joint maintenance review |
| Queues | `queues` | Pending | Pending | Pending |
| Calendars | `calendar` / `calendars` | Pending | Pending | Pending |
| SLA configurations | `slaconfigs` | Pending | Pending | Pending |
| Routing-rule configurations | `routingruleconfigs` | Pending | Pending | Pending |
| User-role assignments | `userroles` | Pending | Pending | Pending |
| Outlook templates | `outlooktemplates` | Pending | Pending | Pending |

The template migrations are confirmed separately; their detailed semantics remain gated in their designs. No pending row is authorized for deletion by approval of this umbrella change.

## Dependencies and Sequencing

Review starts independently. Final branch/module removal depends on `introduce-teamtemplates-resource`, `introduce-documenttemplates-resource`, and all approved additional migrations. Re-read the current CLI spec before implementation/archive because the maintenance retirement also adds to that capability; preserve both independent deltas.

## Risks / Trade-offs

- An import/export pair has asymmetric value -> decide per direction, not by project count.
- Deleting a shared base breaks migrated resources -> remove dependencies before deleting the base.
- Resource data formats become stranded -> document each approved format break and any supported migration procedure.

## Migration Plan

Implement approved removals and independently completed migrations first. Remove top-level transfer branches, projects, DI, and orphaned helpers only when every resource is accounted for. Update README and the major-version guide with old/new commands or explicit retirement. Preserve secure-config negative tests and replacement-positive tests.
