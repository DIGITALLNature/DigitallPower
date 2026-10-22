# Proposal

## Why

Duplicate-rule DTOs and publish/unpublish test executors exist, but the current CLI has no productive duplicate-rule command. A dedicated resource should manage existing rules without mixing their lifecycle with process behavior.

## What Changes

- Introduce a `dgt.power.duplicaterules` module with resource-oriented `init` and `apply` operations for existing rules.
- Capture and apply publish/unpublish state and ownership; specify independent optional properties without implicit changes to omitted rules.
- Provide preflight validation, dry-run, and explicit partial-failure reporting.
- Identify rules by GUID only, relying on solution-stable IDs; do not fall back to name matching.
- Consume the shared owner-resolution contract from `introduce-portable-owner-resolution`; keep rule-specific owner admissibility and assignment sequencing here.
- Out of scope: creating rules, transporting definitions or conditions, and deleting rules.

## Capabilities

### New Capabilities

- `duplicate-rule-management`: Capture and application of existing duplicate-rule state and ownership.

### Modified Capabilities

None.

## Impact

Touches host registration and DI, duplicate-rule DTO ownership, new schemas and module tests, and existing publish/unpublish test helpers. README documents the new commands. Independent of process implementation; the ownership portion depends on approval and availability of `introduce-portable-owner-resolution`, not on a generic state engine.
