# Design

## Context

Process capture/application have a dedicated migration in `introduce-process-resource`. All six other maintenance commands were individually approved for removal. According to the user, current telemetry shows no usage. Two underlying ideas remain deferred Solution feature requests, not promised replacements.

## Goals / Non-Goals

**Goals:** Review each remaining capability with the user and retire the container only after retained behavior has a destination.

**Non-Goals:** Treat deprecated placement as evidence that a feature is useless or implement multiple unrelated resource migrations here.

## Decisions

- Remove all six reviewed standalone commands without modifying existing Dataverse data or metadata.
- Keep deferred Solution feature requests separate from retirement; neither implementation nor an immediate replacement is approved.
- Coordinate bulk-delete decisions with `retire-legacy-import-export`; job execution and scheduled-job transfer must be evaluated together without assuming one DLL per verb.
- Use command-tree rejection tests for removed paths, and positive tests for replacements. Prune only orphaned DTOs, schemas, helpers, and tests.

## Feature Decision Gates

All rows have an approved disposition. No feature-level review gate remains.

| Current command | Initial code owner | Disposition | Destination / change |
|---|---|---|---|
| `maintenance bulkdelete` | `BulkDeleteUtil` | Remove without replacement | Coordinated removal with `retire-legacy-import-export`; no new owner or migration change |
| `maintenance autonumber` | `AutoNumberFormatAction` | Remove without replacement | No new owner |
| `maintenance protectfields` | `ProtectCalculatedFields` | Remove standalone command | Deferred request: `protect-calculated-fields-on-solution-copy` |
| `maintenance carrierinfo` | `ExportCarrierInfo` | Remove without replacement | No new owner |
| `maintenance removeredundantcomponents` | `RemoveRedundantComponents` | Remove standalone command | Deferred request: `prune-redundant-solution-components` |
| `maintenance filterfxplugins` | `FilterPowerFxPluginSteps` | Remove without replacement | No new owner |

### Approved rationale and deferred requests

- Auto number changes only existing-column formats, not seeds. Solution metadata transport covers the general ALM concern; no current need justifies a separate environment-specific override command.
- Calculated-field protection changes `IsCustomizable`, not field-level security. Remove the environment-wide maintenance scan. Evaluate scoped best-practice protection within `solution copy-components` separately in [the field-protection proposal](../protect-calculated-fields-on-solution-copy/proposal.md), with defaults and mutation guarantees still open.
- Carrier information depends on custom `dgt_carrier`/`ec4u_carrier` tables and transport ordering. With no current use, no core-CLI replacement is required.
- Redundant-component removal compares solution memberships and removes overlap from the target, rather than deleting underlying components. Preserve only a separately reviewed Solution use case in [the component-pruning proposal](../prune-redundant-solution-components/proposal.md).
- PowerFx filtering changes `FilteringAttributes` on expression-backed plugin steps. Despite possible deployment value, no current use justifies retaining this specialist command or introducing a replacement.

No exact PAC substitute is claimed for these specialist operations. Retiring them does not reset auto-number formats, managed properties, carrier data, solution memberships, or plugin filters. The deferred requests do not block this change and must not be presented as available replacements.

### Approved bulk-delete removal

The user approved complete removal of bulk-delete functionality: one-off execution and scheduled-job import/export. `BulkDeleteUtil` currently submits a one-off job from inline FetchXML and waits for its terminal state. This execution and polling behavior will be removed without a resource module or replacement command.

Retiring the CLI does not cancel, disable, or delete existing Dataverse jobs. Coordinate shared artifact removal with `retire-legacy-import-export` after consumer checks, and document all three retired command paths and the absence of a replacement in the major-version migration guide.

## Dependencies and Sequencing

Verify the completed process migration before deleting shared dependencies. All dispositions are resolved; no additional retained-feature migration is required. The two deferred requests may be rejected, refined, or implemented later without blocking retirement.

## Risks / Trade-offs

- An operationally useful command is deleted -> require explicit per-feature approval.
- A replacement covers only part of the old behavior -> compare options, config, defaults, and errors before marking migration complete.
- Premature module removal breaks unrelated consumers -> inspect project references and helper/schema consumers before pruning.

## Migration Plan

Update the migration guide with one row per removed command, no currently supported replacement, and explicit deferred-only status for the two Solution ideas. Remove registrations and obsolete implementation in consistent slices. Finally remove the maintenance project, DI, and proven orphaned artifacts, update README, and verify help/completion and retained resource paths.
