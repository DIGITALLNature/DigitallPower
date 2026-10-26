# Design

## Context

Teamtemplates and documenttemplates have dedicated migrations. The user approved removing both directions of all seven remaining legacy transfer resources after a brief review. According to the user, current telemetry shows no usage of those legacy features. Calendar paths differ (`import calendar` versus `export calendars`). Secure-config import is already prohibited by the CLI spec.

## Goals / Non-Goals

**Goals:** Decide both directions of each resource together, then retire the verb-oriented containers.

**Non-Goals:** Automatically preserve every old function or delete functions based solely on their current module.

## Decisions

- Remove all seven reviewed legacy resource pairs without replacement; no new migration changes are required.
- Preserve the completed resource-oriented template migrations and other independently introduced resources.
- Coordinate bulk-delete transfer removal with maintenance job execution removal.
- Standard tooling overlap supports reducing scope, but no exact PAC replacement is promised for specialized synchronization behavior or legacy file formats.
- Modify the existing secure-config requirement as a complete block: preserve the prohibition and `plugin step config set`, but remove its promise that other old import commands remain available.
- Move genuinely shared helpers only after consumer inspection; do not leave resource modules referencing legacy verb modules.

## Feature Decision Gates

All rows are approved for removal without replacement. Existing Dataverse data remains unchanged by CLI retirement.

| Resource | Current CLI suffix | Import disposition | Export disposition | Destination / change |
|---|---|---|---|---|
| Bulk-delete jobs | `bulkdeletes` | Remove without replacement | Remove without replacement | Coordinated removal with `retire-legacy-maintenance`; no new owner or migration change |
| Queues | `queues` | Remove without replacement | Remove without replacement | No new owner |
| Calendars | `calendar` / `calendars` | Remove without replacement | Remove without replacement | No new owner |
| SLA configurations | `slaconfigs` | Remove without replacement | Remove without replacement | No new owner |
| Routing-rule configurations | `routingruleconfigs` | Remove without replacement | Remove without replacement | No new owner |
| User-role assignments | `userroles` | Remove without replacement | Remove without replacement | No new owner |
| Outlook templates | `outlooktemplates` | Remove without replacement | Remove without replacement | No new owner |

The template migrations are confirmed separately and are not retired here.

### Approved retirement rationale

- Queues: limited JSON-based queue settings and owner updates do not justify a separate unused resource module; general configuration-data tooling is not claimed to reproduce the exact behavior.
- Calendars: nested service/holiday calendar transfer has incomplete varying-day support; no current use justifies maintaining the specialist implementation.
- SLA configurations: existing-SLA calendar/owner/state adjustment, not full definition transport; no current deployment requirement justifies retaining it.
- Routing-rule configurations: existing-rule owner/state/target adjustment, not full definition transport; no current requirement justifies a new owner.
- User-role assignments: direct-role export and authoritative role/BU synchronization exceed simple provisioning, but the unused synchronization contract is intentionally retired rather than promised through PAC.
- Outlook templates: Outlook synchronization-filter defaults and FetchXML replacement are a narrow feature, not document or email template deployment; no current use justifies retention.

Retire `queue.json`, `calendar.json`, `slaconfig.json`, `routingruleconfig.json`, `userrole.json`, and `outlooktemplate.json` support in these commands. Do not delete existing queues, calendars, SLAs, routing rules, role assignments, or Outlook filters.

### Approved bulk-delete removal

The user approved complete removal of bulk-delete functionality: scheduled-job import/export and one-off execution. No resource module, replacement command, or supported configuration migration will be introduced. Existing jobs in Dataverse are not cancelled, disabled, or deleted by retiring the CLI.

`BulkDeleteImport` currently creates, updates, and disables recurring jobs and rejects input that omits an existing recurring job. `BulkDeleteExport` exports their definitions from asynchronous-operation data. These transfer behaviors and `bulkdelete.json` support are intentionally retired rather than reproduced elsewhere. Coordinate DTO, schema, helper, and test removal with the execution removal after checking remaining consumers. Document the unsupported configuration and absence of a replacement in the major-version migration guide.

## Dependencies and Sequencing

No product disposition remains unresolved. Verify `introduce-teamtemplates-resource` and `introduce-documenttemplates-resource` replacements before deleting dependencies; no additional migration is required. Re-read the current CLI spec before implementation/spec synchronization because maintenance retirement also adds to that capability; preserve both independent deltas.

## Risks / Trade-offs

- An import/export pair has asymmetric value -> decide per direction, not by project count.
- Deleting a shared base breaks migrated resources -> remove dependencies before deleting the base.
- Resource data formats become stranded -> document each approved format break and any supported migration procedure.

## Migration Plan

Remove all approved legacy transfer paths and their implementations, then top-level branches, projects, DI, and proven orphaned helpers. Update README and the major-version guide with all old paths, unsupported file contracts, and explicit no-replacement statements. Preserve migrated template commands, secure-config rejection, and `plugin step config set` coverage.
