# Proposal

## Why

The previous `plugin push` pipeline built a nested plan whose state the renderer, the executors, and the change check each interpreted separately. Adding a write meant touching all of them, a failed standalone upgrade could block later pushes, and ownership of shared resources (Custom APIs, data providers, managed identities) was implicit.

## What Changes

- Replace the plan/executor pipeline with a snapshot, change-set, apply core: validated local declarations plus a scoped remote snapshot produce flat change collections that are rendered and then applied without re-deciding.
- Accept a path, directory, or glob as target and process each matched plugin artifact as part of one batch.
- Support standalone major/minor replacement with conservative recovery from an interrupted replacement.
- Define explicit ownership for Custom API handler associations, data-provider handlers, solution membership, and managed identities.
- Read every Dataverse result set that reconciliation depends on across all pages.

## Capabilities

### New Capabilities

None.

### Modified Capabilities

- `plugin-deployment`: adds batch, reconciliation, replacement/recovery, and resource-ownership requirements.

## Impact

`Planning/` and `Execution/` in `dgt.power.plugin` are replaced by `Deployment/Batch` and `Deployment/Reconciliation`, and the plan renderer by `Deployment/Output/DeploymentRenderer`. Managed-identity handling is removed from push. Platform behaviors listed in `design.md` still need verification against a live Dataverse environment.
