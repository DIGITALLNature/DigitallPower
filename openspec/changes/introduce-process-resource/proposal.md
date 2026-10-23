# Proposal

## Why

Process state, ownership, and impersonation currently live in maintenance commands with implicit defaults and display-name identifiers. Measured data shows names are neither unique (4742 cloud flows, 1141 distinct names) nor stable across environments (localized action names), while `workflowid` is preserved by solution import. A resource-owned contract should make cross-environment changes explicit and safe.

## What Changes

- Introduce a `dgt.power.process` module covering classic workflows, cloud flows, actions, business process flows, and business rules. Dialogs, AI flows, and desktop flows are not supported.
- Introduce `process init` and `process apply` with GUID (`workflowid`) identity, independently optional `active` and `owner` intent, selectable capture scope, and a mandatory exact-name solution selection for capture, including business rules of tables included with all components.
- Resolve owners through the shared logical-key mappings of `introduce-portable-owner-resolution` for regular and application users.
- Add run-level activation impersonation (`--activate-as <OwnerKey>` or `--activate-as-owner`) built on `introduce-connection-caller-impersonation`; never fall back to the connection identity.
- Provide preflight, dry-run, bounded activation retry for dependent flows, restoration of the original state after a failed owner assignment, and reporting of partial failures.
- **BREAKING**: Replace `maintenance createworkflowstate` and `maintenance workflowstate`; retire their configuration format, name keys, publisher and wildcard filters, default owner/impersonation, and implicit activation without compatibility aliases.
- Out of scope: process-definition deployment, owner teams, connection-reference management, impersonation-privilege preflight, unrelated maintenance functions, and a generic cross-resource state engine.

## Capabilities

### New Capabilities

- `process-management`: Portable capture and application of explicit process state and ownership.

### Modified Capabilities

None. Existing command-execution and command-tree contracts remain applicable.

## Impact

Touches workflow logic and configuration in `dgt.power.maintenance`, a new `dgt.power.process` module, host registration and DI, `schemas/process/v1`, removal of `schemas/maintenance/workflow`, and maintenance/CLI tests. Update README and `docs/migrations/2.x-to-3.x.md`. Depends on `introduce-portable-owner-resolution` (owner mappings) and `introduce-connection-caller-impersonation` (impersonated service). Required before maintenance retirement.
