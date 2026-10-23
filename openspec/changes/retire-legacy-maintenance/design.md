# Design

## Context

The host currently registers eight maintenance commands. Process capture/application are assigned to `introduce-process-resource`; the remaining six commands have not received retention or removal decisions.

## Goals / Non-Goals

**Goals:** Review each remaining capability with the user and retire the container only after retained behavior has a destination.

**Non-Goals:** Treat deprecated placement as evidence that a feature is useless or implement multiple unrelated resource migrations here.

## Decisions

- Review functionality before deletion. Each row needs an approved disposition, rationale, replacement/destination if applicable, and linked change where migration is required.
- Give retained resource migrations their own changes. Approving this retirement plan does not authorize any pending removal.
- Coordinate bulk-delete decisions with `retire-legacy-import-export`; job execution and scheduled-job transfer must be evaluated together without assuming one DLL per verb.
- Use command-tree rejection tests for removed paths, and positive tests for replacements. Prune only orphaned DTOs, schemas, helpers, and tests.

## Feature Decision Gates

All rows are pending user review. Review use cases, overlap with existing tooling, operational value, and maintenance cost for each row before deciding.

| Current command | Initial code owner | Disposition | Destination / change |
|---|---|---|---|
| `maintenance bulkdelete` | `BulkDeleteUtil` | Pending | Pending joint bulk-delete review |
| `maintenance autonumber` | `AutoNumberFormatAction` | Pending | Pending |
| `maintenance protectfields` | `ProtectCalculatedFields` | Pending | Pending |
| `maintenance carrierinfo` | `ExportCarrierInfo` | Pending | Pending |
| `maintenance removeredundantcomponents` | `RemoveRedundantComponents` | Pending | Assess overlap with solution operations |
| `maintenance filterfxplugins` | `FilterPowerFxPluginSteps` | Pending | Assess plugin resource fit |

This matrix is a planning gate, not an invented final disposition. Amend the plan and relevant deltas after decisions.

## Dependencies and Sequencing

Feature review can start immediately. Final removal requires completed process migration and every approved retained-feature migration. Do not archive or claim retirement while any row or destination is unresolved.

## Risks / Trade-offs

- An operationally useful command is deleted -> require explicit per-feature approval.
- A replacement covers only part of the old behavior -> compare options, config, defaults, and errors before marking migration complete.
- Premature module removal breaks unrelated consumers -> inspect project references and helper/schema consumers before pruning.

## Migration Plan

After dispositions, update the migration guide with one row per removed/replaced command, including an explicit no-replacement statement where approved. Remove registrations and obsolete implementation in consistent slices. Finally remove the maintenance project, DI, and unused artifacts, update README, and verify help/completion and retained resource paths.
