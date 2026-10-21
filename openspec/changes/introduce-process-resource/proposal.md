# Proposal

## Why

Process state, ownership, and impersonation currently live in maintenance commands with implicit defaults and inconsistent identifiers. A resource-owned contract should make cross-environment changes explicit and safe.

## What Changes

- Introduce a `dgt.power.process` module covering the process categories handled by the existing workflow implementation: classic workflows, cloud flows, actions, business process flows, and business rules.
- Introduce `process init` and `process apply` with independently optional state and ownership, selectable capture scope, logical owner keys, and target-environment mappings.
- Support regular users and application users, explicit impersonation, preflight validation, dry-run, and reporting of partial failures.
- **BREAKING**: Replace `maintenance createworkflowstate` and `maintenance workflowstate`; retire their configuration format without compatibility aliases.
- Resolve the listed design gates before implementing the new contract.
- Out of scope: process-definition deployment, owner teams, unrelated maintenance functions, and a generic cross-resource state engine.

## Capabilities

### New Capabilities

- `process-management`: Portable capture and application of explicit process state and ownership.

### Modified Capabilities

None. Existing command-execution and command-tree contracts remain applicable.

## Impact

Touches workflow logic and configuration in `dgt.power.maintenance`, host registration and DI, schemas, common DTO ownership where relevant, and maintenance/CLI tests. Update README and `docs/migrations/2.x-to-3.x.md` during implementation. Independent of the other resource introductions; required before maintenance retirement.
